using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_Estimate_File : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        RepositoryItemSearchLookUpEdit lup_Cust;

        public Frm_BM_Estimate_File()
        {
            InitializeComponent();
        }

		private void Frm_BS_Document_Load(object sender, EventArgs e)
		{
            SetDesign();

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((s, ev) => {
                ev.Cancel = false;
                if (grid.Check_Gv_FormClosing(GvDocumentFile, "Status"))
                {
                    ev.Cancel = true;
                }
            });

            SearchClassItemCode();
            //Search();
        }


        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_Btn_Visible();

            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")).AddMonths(-1);
            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            LupCustomer.smtLookUpDataSource = code.GetCustomer_Lup(WrGlobal.CorpID, true);
            LupCustomer.smtLookUpDataDisplayMember = "내용";
            LupCustomer.smtLookUpDataValueMember = "코드";
            LupCustomer.smtLookUpValue = "ALL";

            Set_GvEstimateMater(Gv_EstimateMater);
            Set_GvDoc(GvDocumentFile);
        }

        private void Set_Btn_Visible()
        {
            if (Database.DbName.Contains("KNP"))
            {
                BtnSave.Visible = true;
            }
        }

        #region GridView Setting

        private void Set_GvEstimateMater(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.OptionsView.ShowAutoFilterRow = true;

            lup_Cust = rpo.Create_RepositoryItemSearchLookUp(code.Select_Customer_ByKey(), "거래처코드", "거래처명");
      
            //Column 셋팅
            grid.Create_GvColumn(gv, "견적번호", "EstimateNo");
            grid.Create_GvColumn(gv, "견적일자", "Date");
            grid.Create_GvColumn(gv, "진행상황", "State");
            grid.Create_GvColumn(gv, "견적명", "Name");
            grid.Create_GvColumn(gv, "업체명", "CustCode", lup_Cust);
            //grid.Create_GvColumn(gv, "견적금액", "Price");
            //grid.Create_GvColumn(gv, "부가세", "VatPrice");
            //grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            //grid.Create_GvColumn(gv, "견적등록자", "Manager");
            //grid.Create_GvColumn(gv, "비고", "Remark");
            //grid.Create_GvColumn(gv, "부가세유형", "VatType");
            //grid.Create_GvColumn(gv, "부가세율", "VatRate");
            //grid.Create_GvColumn(gv, "생성자ID", "CreId");
            //grid.Create_GvColumn(gv, "생성자IP ", "CreIP");
            //grid.Create_GvColumn(gv, "생성일자 ", "CreDt");
            //grid.Create_GvColumn(gv, "수정자ID", "ModId");
            //grid.Create_GvColumn(gv, "수정자IP ", "ModIP");
            //grid.Create_GvColumn(gv, "수정일자 ", "ModDt");

            gv.Columns["EstimateNo"].Visible = false;
            gv.Columns["State"].Visible = false;

            //grid.Column_AllowEdit(gv.Columns["Machine_Nm"], false);

            //grid.RowNumber_Indicator(gv);       //행순번 표기
			gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

		private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
            Search();
		}

		private void Set_GvDoc(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.OptionsView.ShowAutoFilterRow = true;
            //
            //RepositoryItemLookUpEdit lup_ClassItemCode = rpo.Create_RepositoryItem(cmCd.Select_CmCd(), "Cm_Cd", "Cm_Nm");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "문서분류", "ClassItemCode");
            grid.Create_GvColumn(gv, "순번", "FileSeq");
            grid.Create_GvColumn(gv, "주문번호", "OrderNo");
            grid.Create_GvColumn(gv, "문서명", "Title");
            grid.Create_GvColumn(gv, "원본파일명", "OriFileName");
            grid.Create_GvColumn(gv, "파일명", "FileName");
            grid.Create_GvColumn(gv, "경로", "Path");
            grid.Create_GvColumn(gv, "용량", "Capacity");
            grid.Create_GvColumn(gv, "확장자", "FileExt");
            grid.Create_GvColumn(gv, "시작일자", "StartDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "종료일자", "EndDate", rpo.Create_rpoDate());

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //
            gv.Columns["ClassItemCode"].Visible = false;
            gv.Columns["FileSeq"].Visible = false;
            gv.Columns["OrderNo"].Visible = false;
            gv.Columns["OriFileName"].Visible = false;
            gv.Columns["FileName"].Visible = false;
            gv.Columns["Capacity"].Visible = false;
            gv.Columns["StartDate"].Visible = false;
            gv.Columns["EndDate"].Visible = false;

            //
            //grid.Column_AllowEdit(gv.Columns["ClassItemCode"]);
            //grid.Column_AllowEdit(gv.Columns["Title"]);
            //grid.Column_AllowEdit(gv.Columns["StartDate"]);
            //grid.Column_AllowEdit(gv.Columns["EndDate"]);

            //grid.RowNumber_Indicator(gv);       //행순번 표기

            if(WrGlobal.LoginID != "Dev")
			{
                gv.Columns.Remove(gv.Columns["Path"]);
			}
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region 행 삽입, 추가, 삭제

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            DataRow drCmCd = Gv_EstimateMater.GetFocusedDataRow();
            if (drCmCd == null) return;

            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);
            GridView gv = GvDocumentFile;

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("ClassItemCode", drCmCd["Cm_Cd"]);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(gv.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gv.GridControl, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void SearchClassItemCode()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");

            BM_EstimateM estM = new BM_EstimateM();
            estM.State = "대기";
            estM.CustCode = LupCustomer.smtLookUpValue;

            Gc_EstimateMater.DataSource = estM.Select_BM_EstimateM(startDate, endDate);
            Gv_EstimateMater.BestFitColumns();
        }


        private void Search()
		{
            //DataRow drCmCd = GvCm_Cd.GetFocusedDataRow();

            //if (drCmCd == null)
            //{
            //    GcDocumentFile.DataSource = null;
            //    return;
            //}

            CM_CommonFile doc = new CM_CommonFile();
            doc.CorpId = WrGlobal.CorpID;
            doc.RefNo = Gv_EstimateMater.GetFocusedRowCellValue("EstimateNo").ConvertString_Null_To_Empty();

            GcDocumentFile.DataSource = doc.Select_CM_CommonFile();
            GvDocumentFile.BestFitColumns();
		}

        private void BtnSave_Click(object sender, EventArgs e)
		{
            Save();
        }

        private void Save()
		{
			//작성내역 저장
			DataTable dt = GcDocumentFile.DataSource as DataTable;

			//1. 데이터 불러온 상태인지 검사
			if (!Check_SaveClick(dt)) return;

			//2. 데이터 저장
			for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
			{
				if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
				{
					if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
				}
				else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
				{
					if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
				}
				else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
				{
					if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
				}
			}

			//3. 정상절차 진행
			Search();

			MessageBox.Show("저장이 정상적으로 완료되었습니다.");
		}

        private bool Check_SaveClick(DataTable dt)
        {
            //Data를 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                switch (status)
                {
                    case "신규":
                        checkCnt++;
                        break;

                    case "수정":
                        checkCnt++;
                        break;

                    case "삭제":
                        isDelete = true;
                        checkCnt++;
                        break;
                }
            }

            //분류 코드 자동 채번이 아닐 경우
            if (WrGlobal.ProcOption["BS"]["ItemClass_AutoNo"] != "Y")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["ItemClass_Cd"]) == "")
                    {
                        MessageBox.Show("분류코드 항목이 비어있습니다.");
                        return false;
                    }
                }
            }

            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    return true;
                }
            }

            return true;
        }

		private bool Save_ToDatabase(DataRow dr, char div)
		{
			//데이터베이스에 반영
			//1. 기본 셋팅
			CM_CommonFile doc = new CM_CommonFile();
            doc.CorpId = WrGlobal.CorpID;
            doc.ClassItemCode = Convert.ToString(dr["ClassItemCode"]);
            doc.FileSeq = dr["FileSeq"].ConvertInt_Null_To_Zero();
            doc.Title           = Convert.ToString(dr["Title"]);
            doc.OriFileName      = Convert.ToString(dr["OriFileName"]);
            doc.FileName         = Convert.ToString(dr["FileName"]);
            doc.Path = Convert.ToString(dr["Path"]);
            doc.Capacity = dr["Capacity"].ConvertInt_Null_To_Zero();
            doc.FileExt = Convert.ToString(dr["FileExt"]);
            doc.StartDate = dr["StartDate"].ConvertDate_To_Char10();
            doc.EndDate = dr["EndDate"].ConvertDate_To_Char10();

			//2. DB 적용
			if (div == 'I')
			{
                doc.FileSeq = doc.GetNextSeq();
				//Insert
				if (!doc.Ins_CM_CommonFile())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'U')
			{
				//Update
				if (!doc.Upd_CM_CommonFile())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'D')
			{
				//Delete
				if (!doc.Del_CM_CommonFile())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}

			//상태값 초기화
			dr[0] = "";

			return true;
		}

		private void BtnUpload_Click(object sender, EventArgs e)
        {
            //DataRow drCmCd = GvCm_Cd.GetFocusedDataRow();
            //if (drCmCd == null) return;

   //         DataRow dr = GvDocumentFile.GetFocusedDataRow();
   //         if (dr == null) return;

   //         if(Convert.ToString(dr["Status"]) == "신규")
			//{
   //             XtraMessageBox.Show("저장 후 업로드 가능");
   //             return;
			//}

            //FTP 이미지 업로드
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG,*.PNG";

            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string localFilePath = Path.GetDirectoryName(ofd.FileName);
            string fileName = Path.GetFileName(ofd.FileName);
            string title = Path.GetFileNameWithoutExtension(ofd.FileName);

            string path = WrGlobal.m_ftpServerip + "/" + WrGlobal.m_ftpFolderPath;
            string serverFileFullPath = path + "/" + fileName;

            cls_FTP clsFtp = new cls_FTP();

            string tableNm = "CM_CommonFile";
            FileInfo file = clsFtp.UploadFile(path, localFilePath, fileName, tableNm);
            if (file == null) return;

            //FTP FilePath UPDATE
            CM_CommonFile doc = new CM_CommonFile();
            doc.CorpId = WrGlobal.CorpID;
            doc.ClassItemCode = "10";             //견적서
            doc.FileSeq = doc.GetNextSeq();
            doc.RefNo = Gv_EstimateMater.GetFocusedRowCellValue("EstimateNo").ConvertString_Null_To_Empty(); //견적서 번호
            doc.Title = title;
            doc.OriFileName = fileName;
            doc.FileName = fileName;
            doc.Path = "/" + WrGlobal.m_ftpFolderPath + "/" + tableNm + "/";
            doc.Capacity = file.Length;
            doc.FileExt = file.Extension;
            //doc.StartDate
            //doc.EndDate

            if (doc.Ins_CM_CommonFile())
            {
                MessageBox.Show("업로드가 완료되었습니다.");
            }

            Search();
        }

		private void BtnDownload_Click(object sender, EventArgs e)
		{
            DataRow drCmCd = Gv_EstimateMater.GetFocusedDataRow();
            if (drCmCd == null) return;

            DataRow dr = GvDocumentFile.GetFocusedDataRow();
            if (dr == null) return;

            if (Convert.ToString(dr["Status"]) == "신규")
            {
                XtraMessageBox.Show("저장 후 업로드 가능");
                return;
            }

            FolderBrowserDialog fb = new FolderBrowserDialog();

            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
				//DataRowView drv = (DataRowView)documentCommonFileBindingSource.Current;
				List<string> selPaths = new List<string>();
				List<string> selOriFiles = new List<string>();
				List<string> selFiles = new List<string>();
				long totalSize = 0;

				//int[] selectedRows = gvCmDevelopmentStdFile.GetSelectedRows();

				//foreach (int rowIndex in selectedRows)
				//{
				//    if (rowIndex < 0) continue; // Group row 인 경우 건너뛰기

				//    DataRowView row = (DataRowView)gvCmDevelopmentStdFile.GetRow(rowIndex);
				//    if (row != null)
				//    {
				selPaths.Add(dr["Path"].ToString());
                        selOriFiles.Add(dr["OriFileName"].ToString());
                        selFiles.Add(dr["FileName"].ToString());
                        totalSize += Convert.ToInt64(dr["Capacity"]);
                    //}
                //}

                FtpFileDownloader downloader = new FtpFileDownloader(fb.SelectedPath, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
                downloader.ShowDialog();
            }
        }

        private void GvDocumentFile_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 수정 시 상태값 수정
            GridView gv = sender as GridView;
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            SearchClassItemCode();
            Search();
        }
    }
}