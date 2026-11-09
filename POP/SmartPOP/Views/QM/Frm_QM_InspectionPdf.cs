using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using SmartMES.Models;
using System.IO;
using SmartPOP.Models;
using SmartPOP.Common;
using SmartPOP.Views.Common;
using SMT_MES.Controls;
using SmartMES.View.QM;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_InspectionPdf : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        cls_Grid grid = new cls_Grid();
        Database db = new Database();
        string query = "";
        private string fileName;

        public Frm_QM_InspectionPdf()
        {
            InitializeComponent();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));

            endDate = endDate.AddDays(-1);

            barEditStartDate.EditValue = startDate;
            barEditEndDate.EditValue = endDate;

            customerLup.Properties.DataSource = code.GetCustomer(WrGlobal.CorpID, true);
            customerLup.Properties.DisplayMember = "내용";
            customerLup.Properties.ValueMember = "코드";
            customerLup.EditValue = "All";

            MaterialLup.Properties.DataSource = code.GetMaterialList(null, true);
            MaterialLup.Properties.DisplayMember = "품명";
            MaterialLup.Properties.ValueMember = "품목코드";
            MaterialLup.EditValue = "All";

            //
            grid.SetPOPGrid(gvCompleteList);

            DisplayData();
        }

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }

        private Dictionary<string, DataRow> existingInspectionDocuments = new Dictionary<string, DataRow>();

        //검사리스트
        private void DisplayData(string focusQcNo = null)
        {
            string Startdate = string.Format("{0:yyyy-MM-dd}", barEditStartDate.EditValue);
            string Enddate = string.Format("{0:yyyy-MM-dd}", barEditEndDate.EditValue);
            string custCode = customerLup.EditValue.ToString();
            string materialCode = MaterialLup.EditValue.ToString();

            query = $"exec QC_GetInspectionList '{WrGlobal.CorpID}', '{custCode}', '{materialCode}', 'Z030', '{Startdate}', '{Enddate}'";
            gcCompleteList.DataSource = db.GetDataView("검사리스트", query);
            gvCompleteList.BestFitColumns();

            // 기존 검사 성적서 데이터를 캐싱
            existingInspectionDocuments.Clear();

            DataTable codeDocumentFiles = db.GetDataTable(query);

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
                    gvCompleteList.SelectRow(rowHandle);     // 행 선택
                    gvCompleteList.MakeRowVisible(rowHandle); // 해당 행이 보이도록 스크롤
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
                        string materialCode = gvCompleteList.GetFocusedRowCellValue("KeyCode2")?.ToString();
                        string date = gvCompleteList.GetFocusedRowCellValue("KeyCode3")?.ToString();
                        string seq = gvCompleteList.GetFocusedRowCellValue("FileSeq")?.ToString();

                        query = $@"Select TOP 1 * FROM CodeDocumentFile
                                    WHERE CorpID = '{WrGlobal.CorpID}' AND ItemCode = 'Z030'
                                    AND KeyCode1 = '{custCode}'
                                    AND KeyCode2 = '{materialCode}'
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

            Frm_FtpFileDownloader downloader = new Frm_FtpFileDownloader(path, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
            downloader.ShowDialog();

            fileName = drv["OriFileName"].ToString();
            spreadForm.Document.Worksheets[0].Clear(spreadForm.Document.Worksheets[0].GetUsedRange());
            spreadForm.LoadDocument(@"c:\temp\" + drv["OriFileName"].ToString());
        }

        private DataRow IsExistingInspectionDocument(DataRow row)
        {
            string custCode = row["KeyCode1"].ToString();
            string materialCode = row["KeyCode2"].ToString();

            string Startdate = string.Format("{0:yyyy-MM-dd}", barEditStartDate.EditValue);
            string Enddate = string.Format("{0:yyyy-MM-dd}", barEditEndDate.EditValue);

            string query = $@"Select TOP 1 * FROM CodeDocumentFile
                                WHERE CorpID = '{WrGlobal.CorpID}' AND ItemCode = 'Z030'
                                AND KeyCode1 = '{custCode}'
                                AND KeyCode2 = '{materialCode}'
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
        private void Btn_Row_Ins_Click(object sender, EventArgs e)
        {
            Frm_QM_InspectionReport form = new Frm_QM_InspectionReport();
            form.ShowDialog();
        }

        //PDF 변환
        private void button3_Click(object sender, EventArgs e)
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

                    cls_MessageBox.Show("성공적으로 저장되었습니다.", "저장 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show("오류 발생: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}