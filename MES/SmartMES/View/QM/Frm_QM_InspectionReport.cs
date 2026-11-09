using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using SmartCommon.Common;
using SmartMES.Common;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Spreadsheet;
using SmartMES.Models;
using SmartCommon.View.Common;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_InspectionReport : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsCode code = new clsCode();
        string query = "";
        string masterCode = "";
        private string fileName;
        private string oriFileName;
        private string itemCode;
        private string keyCode3;
        private int fileSeq;
        private bool isNewDocumentExists;
        private RibbonControl ribbonControl;

        private DataRow selectedFileRow;        // 선택한 파일 정보
        //private DataRow selectedInspectionRow; // 선택한 출하 검사 정보
        private string examType; // 검사유형

        public Frm_QM_InspectionReport()
        {
            InitializeComponent();

            customerLup.Properties.DataSource = code.GetCustomer(WrGlobal.CorpID, false);
            customerLup.Properties.DisplayMember = "내용";
            customerLup.Properties.ValueMember = "코드";

            MaterialLup.Properties.DataSource = code.GetMaterialList(null, false);
            MaterialLup.Properties.DisplayMember = "품명";
            MaterialLup.Properties.ValueMember = "품목코드";
        }
        public Frm_QM_InspectionReport(string examType = null, DataRow documentRow = null)
        {
            InitializeComponent();

            this.examType = examType;           // 검사유형 :  제품검사, 출하검사
            this.Text = examType + " 성적서";

            itemCode = "Z030"; // 기본값

            if (examType == "출하검사")
            {
                itemCode = "Z03010";
            }
            else if (examType == "제품검사")
            {
                itemCode = "Z03020";
            }

            customerLup.Properties.DataSource = code.GetCustomer(WrGlobal.CorpID, false);
            customerLup.Properties.DisplayMember = "내용";
            customerLup.Properties.ValueMember = "코드";

            MaterialLup.Properties.DataSource = code.GetMaterialList(null, false);
            MaterialLup.Properties.DisplayMember = "품명";
            MaterialLup.Properties.ValueMember = "품목코드";


            // 수정
            if (documentRow != null)
            {
                // 기존 문서 정보 저장
                selectedFileRow = documentRow;
                isNewDocumentExists = true; // 기존 문서가 있음을 표시

                // LookUp 컨트롤에 값 설정
                customerLup.EditValue = documentRow["KeyCode1"].ConvertString_Null_To_Empty();
                MaterialLup.EditValue = documentRow["KeyCode2"].ConvertString_Null_To_Empty();
                keyCode3 = documentRow["KeyCode3"].ConvertString_Null_To_Empty();
                fileSeq = documentRow["FileSeq"].ConvertInt_Null_To_One();


                // 폼이 로드된 후 데이터 불러오기
                this.Load += (s, e) => LoadData();
            }
        }

        //조회 버튼 클릭
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        //파일 불러오기
        private void LoadData()
        {
            string custCode = customerLup.EditValue.ToString();
            string ItemCode = MaterialLup.EditValue.ToString();

            query = $@"Select TOP 1 * FROM CodeDocumentFile
                            WHERE CorpID = '{WrGlobal.CorpID}' AND ItemCode = 'Z030'
                            AND KeyCode1 = '{custCode}'
                            AND KeyCode2 = '{ItemCode}'
                            AND KeyCode3 = '{keyCode3}'
                            AND FileSeq = {fileSeq}
                            order by FileSeq desc";
            DataView dv = db.GetDataView("월별체크리스트", query);

            if (dv.Count != 0)
            {
                // 검사성적서가 존재하면 (CodeDocumentFile 조회)
                DataRowView item = dv[0];
                FtpDownLoad(item);
            }
            else
            {
                // 저장된 검사성적서가 없으면 양식 불러오기 (DocumentCommonFile 조회)

                query = $@"Select SizeType, * 
                            FROM CM_Item
                            WHERE ItemCode = '{ItemCode}'";

                DataView dvMaterial = db.GetDataView("MaterialSizeType", query);

                if (dvMaterial.Count != 0)
                {
                    DataRowView materialItem = dvMaterial[0];
                    if (materialItem["SizeType"].ToString() != "50") //파우치
                    {
                        query = $"SELECT * FROM DocumentCommonFile WHERE CorpId = '{WrGlobal.CorpID}' AND ItemCode='Z030' AND Title LIKE '%검사성적서%'";    //검사성적서 매칭
                        DataView dvDocument = db.GetDataView("검사성적서 양식", query);

                        if (dvDocument.Count != 0)
                        {
                            DataRowView selectedFileRow = dvDocument[0];
                            FtpDownLoad(selectedFileRow);
                        }
                    }
                    else //자동롤
                    {
                        query = $"SELECT * FROM DocumentCommonFile WHERE CorpId = '{WrGlobal.CorpID}' AND ItemCode='Z030' AND Title LIKE '%자동롤%'";
                        DataView dvDocument = db.GetDataView("자동롤 양식", query);

                        if (dvDocument.Count != 0)
                        {
                            DataRowView selectedFileRow = dvDocument[0];
                            FtpDownLoad(selectedFileRow);
                        }
                    }
                }
                SetData(dvMaterial);
            }
        }

        //기본정보 자동입력
        private void SetData(DataView dv)
        {
            string date = DateTime.Now.ToString("yyyy년 MM월 dd일");

            // EditValue가 null인지 먼저 체크
            if (customerLup.EditValue == null || MaterialLup.EditValue == null ||
                string.IsNullOrEmpty(customerLup.EditValue.ToString()) ||
                string.IsNullOrEmpty(MaterialLup.EditValue.ToString()))
            {
                MessageBox.Show("거래처와 제품을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string custCode = customerLup.EditValue.ToString();
            query = $"SELECT top 1 CustName FROM CM_Cust WHERE CustCode = '{custCode}'";
            string custName = db.ExecuteScalarStringQuery(query).ToString();

            string ItemCode = MaterialLup.EditValue.ToString();
            query = $"SELECT ItemName FROM CM_Item WHERE ItemCode = '{ItemCode}'";
            string ItemName = db.ExecuteScalarStringQuery(query).ToString();

            if (dv.Count != 0)
            {
                DataRowView materialItem = dv[0];
                if (materialItem["SizeType"].ToString() != "50") //파우치
                {
                    Worksheet sheet = spreadForm.Document.Worksheets[0];
                    sheet.Cells["T3"].Value = date;                   //날짜
                    sheet.Cells["E3"].Value = custName;               //거래처명
                    sheet.Cells["E6"].Value = ItemName;           //제품명
                }
                else //자동롤
                {
                    Worksheet sheet = spreadForm.Document.Worksheets[0];
                    sheet.Cells["B5"].Value = date;                   //날짜
                }
            }
        }

        public void FtpDownLoad(object dataRow)
        {
            string path = @"c:\Temp\";

            List<string> selPaths = new List<string>();
            List<string> selOriFiles = new List<string>();
            List<string> selFiles = new List<string>();

            DataRow row;

            // DataRow와 DataRowView를 처리
            if (dataRow is DataRowView)
            {
                row = ((DataRowView)dataRow).Row;
            }
            else if (dataRow is DataRow)
            {
                row = (DataRow)dataRow;
            }
            else
            {
                throw new ArgumentException("Invalid dataRow type. Must be DataRow or DataRowView.");
            }

            selPaths.Add(row["Path"].ToString());
            selOriFiles.Add(row["OriFileName"].ToString());
            selFiles.Add(row["FileName"].ToString());

            long totalSize = Convert.ToInt64(row["Capacity"]);

            FtpFileDownloader downloader = new FtpFileDownloader(path, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
            downloader.ShowDialog();

            fileName = row["OriFileName"].ToString();
            spreadForm.Document.Worksheets[0].Clear(spreadForm.Document.Worksheets[0].GetUsedRange());
            spreadForm.LoadDocument(@"c:\temp\" + row["OriFileName"].ToString());
        }

        #region 미사용
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                //foDialog.FileName = "";
                // 클래스 필드를 사용하여 검사성적서 존재 여부를 확인
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString("D2"); // 두 자리 숫자로 포맷(D2)

                // 파일 이름&확장자 분리
                string fileBaseName = Path.GetFileNameWithoutExtension(fileName);
                string fileExtension = Path.GetExtension(fileName);
                string localFilePath = "";

                if (!this.isNewDocumentExists)
                {
                    // 1) 새로운 문서를 갖고왔을때
                    string fileNameWithDate = $"{fileBaseName}_{year}_{month}{fileExtension}";
                    localFilePath = @"c:\Temp\" + fileNameWithDate;     //파일 경로 설정
                    spreadForm.SaveDocument(localFilePath);             //전체 파일 경로 ( 스프레드시트 컨트롤 내용 temp에 임시 저장)

                }
                else
                {
                    // 2) 기존에 있는 문서를 갖고 왔을때
                    localFilePath = @"c:\Temp\" + fileName;
                    spreadForm.SaveDocument(localFilePath);
                }

                // 파일 시퀀스 번호 가져오기
                string tableName = "CodeDocumentFile";                                          // 테이블 이름
                string filter    = "Gubun = 'CheckSheet' and KeyCode1 = '" + masterCode + "'";
                query     = $"Select isnull(max(FileSeq), 0) + 1 seq From {tableName} Where {filter}";
                DataRowView drv  = db.GetDataRecord(query);
                int fileSeq      = (int)drv[0];


                // FtpFilesUploader 인스턴스 생성 및 파일 업로드
                string uploadTable = "CodeDocumentFile";
                string keyNo = "YourKey";         // 키
                string[] filePaths = new string[] { localFilePath }; //업로드할 파일 경로 배열

                FtpFilesUploader uploader = new FtpFilesUploader(uploadTable, fileSeq, "CheckSheet|" + masterCode, filePaths);
                uploader.ShowDialog();

                if (uploader.UploadSuccesss)
                {
                    // FTP 업로드 성공

                    // Update FaultStatus
                    query = $"Update CodeDocumentFile Set ItemCode='{itemCode}' WHERE Gubun = 'CheckSheet' and KeyCode1 = '" + masterCode + "'";
                    db.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FTP Upload Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // DB에 저장
        private void spreadsheetCommandBarButtonItem3_ItemClick(object sender, EventArgs e)
        {
            try
            {
                // 파일 이름&확장자 분리
                string ItemCode = MaterialLup.EditValue.ToString();
                string query = $"SELECT ItemName FROM CM_Item WHERE ItemCode = '{ItemCode}'";
                string ItemName = db.ExecuteScalarStringQuery(query).ToString();

                string custCode = customerLup.EditValue.ToString();
                query = $"SELECT CustName FROM CM_Cust WHERE CustCode = '{custCode}'";
                string custName = db.ExecuteScalarStringQuery(query).ToString();

                string date;
                int fileSeq;

                // 수정모드인지 신규모드인지 구분
                if (selectedFileRow != null && isNewDocumentExists)
                {
                    // 수정모드는 기본 사용
                    fileSeq = Convert.ToInt32(selectedFileRow["FileSeq"]);
                    date = selectedFileRow["KeyCode3"].ToString();

                    // ⭐ 기존 레코드 삭제
                    query = $@" DELETE FROM CodeDocumentFile
                                 WHERE CorpId = '{WrGlobal.CorpID}'
                                   AND Gubun = 'InspectionReport'  
                                   AND KeyCode1 = '{custCode}' 
                                   AND KeyCode2 = '{ItemCode}'
                                   AND KeyCode3 = '{date}'
                                   AND FileSeq = {fileSeq}";
                    db.ExecuteQuery(query);
                }
                else
                {
                    // 신규 모드
                    date = DateTime.Now.ToString("yyyy-MM-dd");

                    // 새로운 FileSeq 채번
                    query = $@" SELECT ISNULL(MAX(FileSeq), 0) + 1 AS seq
                                 FROM CodeDocumentFile
                                WHERE Gubun = 'InspectionReport'
                                  AND KeyCode1 = '{custCode}'
                                  AND KeyCode2 = '{ItemCode}'
                                  AND KeyCode3 = '{date}'";

                    fileSeq = Convert.ToInt32(db.ExecuteScalarQuery(query));
                }

                // 파일명 생성
                string fileExtension = Path.GetExtension(fileName);
                string fileBaseName = Path.GetFileNameWithoutExtension(fileName);
                string fileNameWithSeq = GetFileName(fileBaseName, custName, ItemName, fileSeq, fileExtension);
                string localFilePath = "";

                if (!this.isNewDocumentExists)
                {
                    // 새로운 문서를 가져왔을 때
                    localFilePath = @"c:\Temp\" + fileNameWithSeq; // 파일 경로 설정
                    spreadForm.SaveDocument(localFilePath); // 파일을 임시 경로에 저장
                }
                else
                {
                    // 기존 문서를 가져왔을 때
                    localFilePath = @"c:\Temp\" + fileName;
                    spreadForm.SaveDocument(localFilePath);
                }

                // FtpFilesUploader 인스턴스 생성 및 파일 업로드
                string uploadTable = "CodeDocumentFile";
                string[] filePaths = new string[] { localFilePath }; // 업로드할 파일 경로 배열

                // 구분: InspectionReport 키1: 거래처코드 키2: 제품코드 키3: 년월일
                FtpFilesUploader uploader = new FtpFilesUploader(uploadTable, fileSeq, "InspectionReport|" + custCode + "|" + ItemCode + "|" + date, filePaths);
                uploader.ShowDialog();

                if (uploader.UploadSuccesss)
                {
                    //FTP 업로드 성공 시 DB 업데이트
                    query = $@" UPDATE CodeDocumentFile SET 
                                    ItemCode = 'Z030'
                                WHERE Gubun = 'InspectionReport'  
                                      AND KeyCode1 = '{custCode}' 
                                      AND KeyCode2 = '{ItemCode}'
                                      AND KeyCode3 = '{date}'
                                      AND FileSeq = {fileSeq}";
                    db.ExecuteQuery(query);
                    MessageBox.Show("파일이 성공적으로 업로드되었습니다.", "업로드 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FTP Upload Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //새로운 파일명 생성
        private string GetFileName(string baseFileName, string custName, string ItemName, int fileSeq, string fileExtension)
        {
            //기존 파일명에서 "시험성적서" 이후의 내용 제거
            int index = baseFileName.IndexOf("시험성적서");
            if (index != -1)
            {
                baseFileName = baseFileName.Substring(0, index + 5); //"시험성적서" 뒤의 '-' 포함해서 잘라냄
            }
            return $"{baseFileName}-{custName}-{ItemName}-{fileSeq}{fileExtension}";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}