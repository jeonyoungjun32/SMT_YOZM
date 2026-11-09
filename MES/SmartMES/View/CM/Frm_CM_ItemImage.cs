using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SMT_MES.View.CM
{
	public partial class Frm_CM_ItemImage : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        cls_Database cDB = new cls_Database();
        CM_CateItem cm = new CM_CateItem();
        clsCode code = new clsCode();

        private string FormType = "None";    //Product : 제품정보, Material : 원자재정보 - 감리용 화면 분할
        private string[] ItemDiv = null;    //품목구분
        private string ItemCode = "";
        public Frm_CM_ItemImage()
        {
            InitializeComponent();

            // 화면일 때 조회상태로
            cmdInsert.Visible = false;
            cmdSave.Visible = false;
            cmdDelete.Visible = false;
        }

        public Frm_CM_ItemImage(string ItemCode)
        {
            InitializeComponent();
            this.ItemCode = ItemCode;
            layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void Frm_CM_ItemImage_Load(object sender, EventArgs e)
        {
            PicImage.Properties.ShowMenu = false;   //오른쪽 클릭 막기

            //1. 품목구분 데이터 입력(순서 변경 X)
            Set_Item_Div_Rdo(Rdo_Item_Div);

            //GridView 데이터 입력
            Set_ItemGridView(Gv_Product);
            Set_GvItemImage(Gv_ItemImage);

            Search_Item();
        }

        #region 디자인 아이템 생성

        private void Set_Item_Div_Rdo(RadioGroup rdo)
        {
            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(rdo, cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, "A010", null, false), "분류명", "분류코드");

            rdo.SelectedIndex = 0; //초기값
            rdo.EditValueChanged += Rdo_ItemDIv_EditValueChanged;
        }
        #endregion


        #region GridView Setting

        private void Set_ItemGridView(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false; //DataTable 입력시 Column 생성 방지
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            //Column 생성
            grid.Create_GvColumn(gv, "품목구분", "ItemTypeName");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "규격", "Standard");

            gv.Columns["ItemTypeName"].Visible = false;

            grid.RowNumber_Indicator(gv);       //행순번 표기
        }

        private void Set_GvItemImage(GridView gv)
        {
            //거래처 Column 생성
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Repository
            RepositoryItem lup_Img_Div = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "A013"), "분류코드", "분류명");  //이미지구분
            RepositoryItem lup_ExamDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q005"), "분류코드", "분류명");  //검사구분
            RepositoryItem lupProcess = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(null, false), "공정코드", "공정명");//공정구분

            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;

			RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
			Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
			Btn_Img.Buttons.RemoveAt(0);
			Btn_Img.Buttons.Add(Btn_Img_Move);
			Btn_Img.ButtonClick += Btn_Grid_Click;

			//Column 생성
			grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "이미지구분", "ImgCode", lup_Img_Div);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lupProcess);
            grid.Create_GvColumn(gv, "검사구분", "ExamDivCd", lup_ExamDiv);
            grid.Create_GvColumn(gv, "내용", "Title");
            grid.Create_GvColumn(gv, "경로", "Path");
            grid.Create_GvColumn(gv, "확장자", "FileExt");
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "등록일자", "Date", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "이미지", "Pic");
			grid.Create_GvColumn(gv, "이미지", "Btn_Pic", Btn_Img);
            //grid.Create_GvColumn(gv, "비고", "Date");
            //grid.Create_GvColumn(gv, "등록자", "CreId");
            //grid.Create_GvColumn(gv, "등록일시", "CreDt");
            //grid.Create_GvColumn(gv, "수정자", "ModId");
            //grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //VISIBLE
            //gv.Columns["ImgCode"].Visible = false;
            gv.Columns["Pic"].Visible = false;
            gv.Columns["Path"].Visible = false;
            gv.Columns["FileExt"].Visible = false;

            //사용유무 설정
            grid.Column_AllowEdit(gv.Columns["Date"]);
            grid.Column_AllowEdit(gv.Columns["ItemCode"]);
            grid.Column_AllowEdit(gv.Columns["ImgCode"]);
            grid.Column_AllowEdit(gv.Columns["Title"]);
            grid.Column_AllowEdit(gv.Columns["ExamDivCd"]);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["Pic"]);
            grid.Column_AllowEdit(gv.Columns["Btn_Pic"]);
            grid.Column_AllowEdit(gv.Columns["Date"]);

            //정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //Evnet
            grid.RowNumber_Indicator(gv);       //행순번 표기
            Gc_ItemImage.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;   //행 변경시 해당거래처의 납품처리스트조회
            gv.CellValueChanged += Gv_CellValueChanged;
            //gv.FocusedRowChanged += Gv_FocusedRowChanged; ;   //행 변경시 해당거래처의 납품처리스트조회
            //gv.BeforeLeaveRow += Gv_BeforeLeaveRow;
        }
        #endregion

        private void Btn_Grid_Click(object sender, ButtonPressedEventArgs e)
        {
            //제품이미지, 제품사양서 등록

            ButtonEdit btn = sender as ButtonEdit;
            if (btn == null) return;

            GridView gv = Gc_ItemImage.MainView as GridView;
            if (gv == null) return;

            DataRow drChanged = (Gc_ItemImage.MainView as GridView).GetFocusedDataRow();
            string fieldName = gv.FocusedColumn.FieldName;

            /*
             * 프로세스 옵션
             * 제품 사양서 조회(I: 이미지, P: PDF)
             */

            if (fieldName == "Btn_Pic")
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "파일 선택";
                ofd.Filter = "모든파일 ( *.bmp; *.jpg; *.png; *.jpeg; *.pdf) | *.BMP; *.JPG; *.PNG; *.JPEG; *.PDF; | pdf 파일 (*.pdf) | *.PDF; |이미지 파일( *.bmp; *.jpg; *.png; *.jpeg) | *.BMP; *.JPG; *.PNG; *.JPEG;";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string localFileFullPath = ofd.FileName;
                    string fileName = Path.GetFileName(localFileFullPath);
                    string ext = Path.GetExtension(localFileFullPath);

                    if (ext.ToUpper() == ".PDF")
                    {
                        FileStream fs = new FileStream(localFileFullPath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);

                        pdfViewer1.LoadDocument(localFileFullPath);

                        gv.SetFocusedRowCellValue("Pic", br.ReadBytes((int)fs.Length));

                        PicImage.Visible = false;
                        pdfViewer1.Visible = true;

                        ItemPic.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        ItemPdf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else
                    {

                        using (FileStream fs = new FileStream(localFileFullPath, FileMode.Open))
                        {
                            byte[] fileBytes = new byte[fs.Length];
                            fs.Read(fileBytes, 0, fileBytes.Length);

                            gv.SetFocusedRowCellValue("Pic", fileBytes);
                            PicImage.EditValue = fileBytes;
                        }

                        PicImage.Visible = true;
                        pdfViewer1.Visible = false;

                        ItemPic.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        ItemPdf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }

                    drChanged["Path"] = Path.GetDirectoryName(localFileFullPath);
                    drChanged["Title"] = Path.GetFileName(localFileFullPath);
                    drChanged["FileExt"] = Path.GetExtension(localFileFullPath);
                }
            }
        }

        private void Gv_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            SearchImage();
        }

        private void SearchImage()
        {
            DataRow dr = Gv_ItemImage.GetFocusedDataRow();

            if (dr == null)
            {
                PicImage.EditValue = null;
                pdfViewer1.CloseDocument();
                return;
            }

            string ext = Convert.ToString(dr["FileExt"]).ToUpper();

            if(ext == ".PDF")
            {
                PicImage.Visible = false;
                pdfViewer1.Visible = true;

                ItemPic.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemPdf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                byte[] buff = dr["Pic"] as byte[];

                if (buff == null)
                {
                    pdfViewer1.CloseDocument();
                    return;
                }

                MemoryStream mStream = new MemoryStream(buff);

                pdfViewer1.LoadDocument(mStream);
            }
            else
            {
                PicImage.Visible = true;
                pdfViewer1.Visible = false;

                ItemPic.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ItemPdf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                PicImage.EditValue = dr["Pic"];
            }

        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);
            string value = e.Value.ConvertString_Null_To_Empty();

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        public void Search_Item()
        {
            //품목 조회
            CM_Item item = new CM_Item();

            if(ItemCode == "")
			{
                //전체조회
                Gc_Item.DataSource = item.SelectCM_Item(new string[] { Convert.ToString(Rdo_Item_Div.EditValue) });
			}
			else
			{
                //단건 품목 조회
                item.ItemCode = this.ItemCode;
                Gc_Item.DataSource = item.Select_Item_RowInfo().Table;
                (Gc_Item.MainView as GridView).FocusedRowHandle = 0;
			}

            Gv_Product.BestFitColumns();
        }

        public void Search_Image()
        {
            GridView gv = Gc_Item.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null)
            {
                PicImage.EditValue = null;
                pdfViewer1.CloseDocument();
                return;
            }

            CM_ItemImage im = new CM_ItemImage();
            im.CorpId = WrGlobal.CorpID;
            im.ItemCode = Convert.ToString(dr["ItemCode"]);

            Gc_ItemImage.DataSource = im.SelectImage();
            Gv_ItemImage.BestFitColumns();

            //이미지 Grid 데이터 없을 경우 PictureEdit, Viewer Clear 처리
            if(Gc_ItemImage.DataSource == null)
			{
                PicImage.EditValue = null;
                pdfViewer1.CloseDocument();
			}
			else
			{
                if ((Gc_ItemImage.DataSource as DataTable).Rows.Count < 1)
                {
                    PicImage.EditValue = null;
                    pdfViewer1.CloseDocument();
                }
            }
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmdInsert.PerformClick(); break;  //행삽입
                case (Keys.Alt | Keys.D): cmdDelete.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Tag.ToString();

            GridView gv = Gc_Item.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("ItemCode", dr["ItemCode"]);
                dic.Add("Date", DateTime.Today.ToString("yyyy-MM-dd"));
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString((Gc_ItemImage.MainView as GridView).GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_ItemImage, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //작성내역 저장
            Gv_ItemImage.FocusInvalidRow();

            DataTable dtItem = Gc_ItemImage.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'D')) return;
                }
            }

            //3. 선택한 row 저장
            int SaveFocusedRowInx = (Gc_Item.MainView as GridView).FocusedRowHandle;

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            //4. 정상절차 진행
            Search_Image();
        }


        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;
            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);

                if (status == "신규" || status == "수정")
                {
                    if (Convert.ToString(dr["ItemCode"]) == "")
                    {
                        MessageBox.Show("품목 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["ImgCode"]) == "")
                    {
                        MessageBox.Show("이미지구분 항목이 비어있습니다.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (status == "삭제")
                {
                    checkCnt++;
                    isDelete = true;
                }
            }
            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }


        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_ItemImage im = new CM_ItemImage();

            im.ItemCode = Convert.ToString(dr["ItemCode"]);
            im.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
            im.ImgCode = Convert.ToString(dr["ImgCode"]);
            im.Date = dr["Date"].ConvertDate_To_Char10();
            im.ExamDivCd = Convert.ToString(dr["ExamDivCd"]);
            im.ProcessCode = Convert.ToString(dr["ProcessCode"]);
            im.Path = Convert.ToString(dr["Path"]);
            im.Title = Convert.ToString(dr["Title"]);
            im.FileExt = Convert.ToString(dr["FileExt"]);
            im.Pic = dr["Pic"] as byte[];

            //2. DB 적용
            if (div == 'I')
            {
                //Seq 구하기
                im.Seq = im.Sel_NextSeq();

                //Insert
                if (!im.Ins_item_Image())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                else
                {
                    //상태값 초기화
                    dr[0] = "";
                }
            }
            else if (div == 'U')
            {
                //Update
                if (!im.Upd_Item_Image())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                else
                {
                    //상태값 초기화
                    dr[0] = "";
                }
            }
            else if (div == 'D')
            {
                //Delete
                if (!im.Del_Item_Image())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                else
                {

                }
            }

            return true;
        }


        private void Rdo_ItemDIv_EditValueChanged(object sender, EventArgs e)
        {
            Search_Item();
        }

        private void Gv_Product_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            Search_Image();
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}