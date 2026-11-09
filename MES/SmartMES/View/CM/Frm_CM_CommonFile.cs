using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.CM;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
	public partial class Frm_CM_CommonFile : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_CM_CommonFile()
        {
            InitializeComponent();
        }

		private void Frm_CM_CommonFile_Load(object sender, EventArgs e)
		{
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정

            SetDesign();

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((s, ev) => {
                ev.Cancel = false;
                if (grid.Check_Gv_FormClosing(GvDocumentFile, "Status"))
                {
                    ev.Cancel = true;
                }
            });

            SearchDocType();
            Search();
        }


        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_GvCmCd(TreeListClass);
            Set_GvDoc(GvDocumentFile);
        }

        #region GridView Setting

        private void Set_GvCmCd(TreeList tree)
        {
            clsTree clstree = new clsTree();
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            tree.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            //tree.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            //tree.OptionsView.ShowAutoFilterRow = true;

            tree.KeyFieldName = "CLASS";
            tree.ParentFieldName = "HIGHCLASS";

            //Column 생성
            clstree.Create_TreeColummn(tree, "상위문서분류코드", "HIGHCLASS");
            clstree.Create_TreeColummn(tree, "문서분류코드", "CLASS");
            clstree.Create_TreeColummn(tree, "문서분류명", "CLASSNAME");

            tree.Columns["HIGHCLASS"].Visible = false;

            //grid.Column_AllowEdit(gv.Columns["Machine_Nm"], false);

            tree.FocusedNodeChanged += Tree_FocusedNodeChanged;
        }

		private void Tree_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
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
            CM_CLASS cmClass = new CM_CLASS();
            cmClass.CLASS = "Z0";
            RepositoryItemLookUpEdit lup_DocType = rpo.Create_RepositoryItem(cmClass.Select_Class_Like_LookUp(), "코드", "명칭") ;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "문서분류", "ClassItemCode", lup_DocType);
            grid.Create_GvColumn(gv, "순번", "FileSeq");
            grid.Create_GvColumn(gv, "문서명", "Title");
            grid.Create_GvColumn(gv, "원본파일명", "OriFileName");
            grid.Create_GvColumn(gv, "파일명", "FileName");
            grid.Create_GvColumn(gv, "경로", "Path");
            grid.Create_GvColumn(gv, "용량", "Capacity");
            grid.Create_GvColumn(gv, "확장자", "FileExt");
            grid.Create_GvColumn(gv, "시작일자", "StartDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "종료일자", "EndDate", rpo.Create_rpoDate());

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록자", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIPP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //
            gv.Columns["OriFileName"].Visible = false;
            gv.Columns["FileName"].Visible = false;
            gv.Columns["Capacity"].Visible = false;

            //
            grid.Column_AllowEdit(gv.Columns["DocType"]);
            grid.Column_AllowEdit(gv.Columns["Title"]);
            grid.Column_AllowEdit(gv.Columns["StartDate"]);
            grid.Column_AllowEdit(gv.Columns["EndDate"]);

            grid.RowNumber_Indicator(gv);       //행순번 표기

            if(WrGlobal.LoginID != "Dev")
			{
                gv.Columns.Remove(gv.Columns["Path"]);
			}

			gv.CellValueChanged += Gv_CellValueChanged;
        }

		private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

		#endregion GridView Setting End

		#endregion 디자인 아이템 생성 종료

		private void SearchDocType()
        {
            CM_CLASS cl = new CM_CLASS();
            cl.CLASS = "Z0";

            TreeListClass.DataSource = cl.Select_Class_Like();
            TreeListClass.ExpandAll();
            TreeListClass.BestFitColumns();
        }


        private void Search()
		{
            TreeListNode selectedNode = TreeListClass.FocusedNode;

            if (selectedNode == null)
            {
                GcDocumentFile.DataSource = null;
                return;
            }

            CM_CommonFile doc = new CM_CommonFile();
            doc.CorpId = WrGlobal.CorpID;
            doc.ClassItemCode = selectedNode["CLASS"].ConvertString_Null_To_Empty();

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
            TreeListNode selectedNode = TreeListClass.FocusedNode;
            if (selectedNode == null) return;

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
            doc.ClassItemCode = selectedNode["CLASS"].ConvertString_Null_To_Empty();
            doc.FileSeq = doc.GetNextSeq();
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
            TreeListNode selectedNode = TreeListClass.FocusedNode;
            if (selectedNode == null) return;

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

		private void BtnDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(cls_MessageBox.DelMsg_Select, "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
			{
				DataRow dr = GvDocumentFile.GetFocusedDataRow();
				if (dr == null) return;
				CM_CommonFile file = new CM_CommonFile();
                file.CorpId = WrGlobal.CorpID;
                file.ClassItemCode = dr["ClassItemCode"].ConvertString_Null_To_Empty();
                file.FileSeq = dr["FileSeq"].ConvertInt_Null_To_Zero();

				file.Del_CM_CommonFile();

				MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Search();
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}