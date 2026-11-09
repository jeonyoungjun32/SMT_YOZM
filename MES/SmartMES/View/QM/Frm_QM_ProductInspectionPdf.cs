using DevExpress.Spreadsheet;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using SmartCommon.Common;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_ProductInspectionPdf : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";
        private string fileName;
        string FormType = ""; // 폼로드명 "제품검사" : ProductInspection, "출하검사" : ShippingInspection
        private string itemCode;
        private string formName = ""; // 폼로드명 "제품검사" : ProductInspection, "출하검사" : ShippingInspection

        public Frm_QM_ProductInspectionPdf(string formType)
        {
            InitializeComponent();
            this.FormType = formType;

            if (formType == "ProductInspection")
            {
                this.Text = "제품검사성적서";
                smtCommonBar1.smtImageTitleText = "제품검사성적서";
                itemCode = "Z03020";
                formName = "제품검사";
            }
            else if (formType == "ShippingInspection")
            {
                this.Text = "출하검사성적서";
                smtCommonBar1.smtImageTitleText = "출하검사성적서";
                itemCode = "Z03010";
                formName = "출하검사";
            }
        }

        private void Frm_QM_ProductInspectionPdf_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd"));
            DateTime endDate = DateTime.Now.Date;

            barEdit.smtStartDateTimeValue = startDate;
            barEdit.smtEndDateTimeValue = endDate;

            LupCustomer.smtLookUpDataSource = code.smtLookUp("거래처", "", "전체");

            LupItemType.smtLookUpDataSource = code.smtLookUp("품목", "", "전체");
            LupItemType.smtLookUpDataValueMember = "품목코드";
            LupItemType.smtLookUpDataDisplayMember = "품명";
            LupItemType.smtLookUpSelectdIndex = 0;

            DisplayData();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private Dictionary<string, DataRow> existingInspectionDocuments = new Dictionary<string, DataRow>();

        //검사리스트
        private void DisplayData(string focusQcNo = null)
        {
            string startDate = barEdit.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = barEdit.smtEndDateTimeValue.ConvertDate_To_Char10();
            string custCode = LupCustomer.smtLookUpValue;
            string ItemCode = LupItemType.smtLookUpValue;

            query = $"exec QC_GetInspectionList '{WrGlobal.CorpID}', '{custCode}', '{ItemCode}', '{itemCode}','{startDate}', '{endDate}'";
            gcCompleteList.DataSource = db.GetDataView("검사리스트", query);
            gvCompleteList.BestFitColumns();

            // 기존 검사 성적서 데이터를 캐싱
            existingInspectionDocuments.Clear();

            DataTable codeDocumentFiles = db.GetDataTable(query);

            // 데이터가 없을 경우 스프레드시트 초기화
            if (codeDocumentFiles == null || codeDocumentFiles.Rows.Count == 0)
            {
                // 새 문서 생성
                spreadForm.CreateNewDocument();
            }


            foreach (DataRow row in codeDocumentFiles.Rows)
            {
                string key = $"{row["KeyCode1"]}_{row["KeyCode2"]}_{row["KeyCode3"]}";
                if (!existingInspectionDocuments.ContainsKey(key))
                {
                    existingInspectionDocuments[key] = row;
                }
            }

            if (!string.IsNullOrEmpty(focusQcNo))
            {
                // 그리드뷰의 선택을 초기화
                gvCompleteList.ClearSelection();
                gvCompleteList.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;

                // QCNo에 해당하는 행을 찾아서 포커스
                DataRow[] foundRows = codeDocumentFiles.Select($"KeyCode1 = '{focusQcNo}'");
                if (foundRows.Length > 0)
                {
                    int rowHandle = codeDocumentFiles.Rows.IndexOf(foundRows[0]);
                    gvCompleteList.FocusedRowHandle = rowHandle; // 등록한 행을 포커스
                    gvCompleteList.SelectRow(rowHandle);         // 행 선택
                    gvCompleteList.MakeRowVisible(rowHandle);    // 해당 행이 보이도록 스크롤
                }
            }
        }

        private void gvCompleteList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv != null)
            {
                DataRow row = gv.GetDataRow(e.FocusedRowHandle);
                if (row != null)
                {
                    DataRow document = IsExistingInspectionDocument(row);
                    if (document != null)
                    {
                        // 기존 검사 성적서가 있는 경우 데이터를 로드
                        string custCode = gvCompleteList.GetFocusedRowCellValue("KeyCode1")?.ToString();
                        string ItemCode = gvCompleteList.GetFocusedRowCellValue("KeyCode2")?.ToString();
                        string date = gvCompleteList.GetFocusedRowCellValue("KeyCode3")?.ToString();
                        string seq = gvCompleteList.GetFocusedRowCellValue("FileSeq")?.ToString();

                        query = $@"Select TOP 1 * FROM CodeDocumentFile
                                    WHERE CorpID = '{WrGlobal.CorpID}' AND ItemCode = '{itemCode}'
                                    AND KeyCode1 = '{custCode}'
                                    AND KeyCode2 = '{ItemCode}'
                                    AND KeyCode3 = '{date}'
                                    AND FileSeq = '{seq}'
                                    order by FileSeq desc";
                        DataView dv = db.GetDataView("검사성적서", query);

                        if (dv.Count != 0)
                        {
                            DataRowView item = dv[0];
                            FtpdownLoad(item);
                        }
                    }
                    else
                    {
                        // 기존 검사 성적서가 없는 경우 SpreadForm 초기화
                        spreadForm.Document.BeginUpdate();
                        try
                        {
                            // 새 워크시트를 추가
                            Worksheet newSheet = spreadForm.Document.Worksheets.Add();

                            // 기존 워크시트를 제거
                            while (spreadForm.Document.Worksheets.Count > 1)
                            {
                                Worksheet sheetToRemove = spreadForm.Document.Worksheets[0];
                                if (sheetToRemove != newSheet)
                                {
                                    spreadForm.Document.Worksheets.Remove(sheetToRemove);
                                }
                                else
                                {
                                    spreadForm.Document.Worksheets.Remove(spreadForm.Document.Worksheets[1]);
                                }
                            }
                            // 새 워크시트의 내용 초기화
                            newSheet.Cells.Clear();
                        }
                        finally
                        {
                            spreadForm.Document.EndUpdate();
                        }
                    }
                }
            }
        }

        public void FtpdownLoad(DataRowView drv)
        {

            string path = @"c:\Temp\";

            List<string> selPaths = new List<string>();
            List<string> selOriFiles = new List<string>();
            List<string> selFiles = new List<string>();

            selPaths.Add(drv["Path"].ToString());
            selOriFiles.Add(drv["OriFileName"].ToString());
            selFiles.Add(drv["FileName"].ToString());

            long totalSize = Convert.ToInt64(drv["Capacity"]);

            FtpFileDownloader downloader = new FtpFileDownloader(path, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
            downloader.ShowDialog();

            fileName = drv["OriFileName"].ToString();
            spreadForm.Document.Worksheets[0].Clear(spreadForm.Document.Worksheets[0].GetUsedRange());
            spreadForm.LoadDocument(@"c:\temp\" + drv["OriFileName"].ToString());
        }

        private DataRow IsExistingInspectionDocument(DataRow row)
        {
            string custCode = row["KeyCode1"].ToString();
            string ItemCode = row["KeyCode2"].ToString();

            string Startdate = string.Format("{0:yyyy-MM-dd}", barEdit.smtStartDateTimeValue.ConvertDate_To_Char10());
            string Enddate = string.Format("{0:yyyy-MM-dd}", barEdit.smtEndDateTimeValue.ConvertDate_To_Char10());

            string query = $@"Select TOP 1 * FROM CodeDocumentFile
                                WHERE CorpID = '{WrGlobal.CorpID}' AND ItemCode = '{itemCode}'
                                AND KeyCode1 = '{custCode}'
                                AND KeyCode2 = '{ItemCode}'
                                AND KeyCode3  between '{Startdate}' and '{Enddate}'
                                order by FileSeq desc";

            DataTable existingDocuments = db.GetDataTable(query);

            if (existingDocuments != null && existingDocuments.Rows.Count > 0)
            {
                DataRow document = existingDocuments.Rows[0];
                return document;
            }
            else
            {
                return null;
            }
        }

        //등록
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            Frm_QM_InspectionReport form = new Frm_QM_InspectionReport(formName);
            form.ShowDialog();
            DisplayData();
        }

        //PDF 변환
        private void cmdPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // SaveFileDialog 생성
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf"; // 파일 형식 필터
                saveFileDialog.Title = "저장할 위치를 선택하세요"; // 다이얼로그 제목
                saveFileDialog.InitialDirectory = @"C:\Temp"; // 초기 디렉토리

                // SaveFileDialog 열기
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 사용자가 파일 경로를 선택한 경우
                    string filePath = saveFileDialog.FileName;

                    // PDF로 변환
                    byte[] pdfBytes = ConvertExcelToPdf();

                    // 변환된 PDF를 선택한 경로에 저장
                    File.WriteAllBytes(filePath, pdfBytes);

                    MessageBox.Show("성공적으로 저장되었습니다.", "저장 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] ConvertExcelToPdf()
        {
            MemoryStream pdfStream = new MemoryStream();
            //Excel 파일을 PDF로 변환하여 pdfStream에 저장
            spreadForm.Document.ExportToPdf(pdfStream);
            // 변환된 PDF 데이터를 바이트 배열로 변환하여 반환
            return pdfStream.ToArray();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            // 선택된 행이 있는지 확인
            if (gvCompleteList.FocusedRowHandle < 0)
            {
                MessageBox.Show("수정할 검사성적서를 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 선택된 행에 해당하는 문서가 존재하는지 확인
            DataRow document = IsExistingInspectionDocument(gvCompleteList.GetDataRow(gvCompleteList.FocusedRowHandle));
            if (document == null)
            {
                MessageBox.Show("수정할 검사성적서가 존재하지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // frmInspectionReport 폼 호출
            Frm_QM_InspectionReport form = new Frm_QM_InspectionReport(formName, document);
            form.ShowDialog();
            DisplayData();
        }
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}