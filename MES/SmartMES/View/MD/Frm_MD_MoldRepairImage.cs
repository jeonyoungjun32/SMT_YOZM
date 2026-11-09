using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.MD;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
	public partial class Frm_MD_MoldRepairImage : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();

        string MoldCode = string.Empty;
        string RepairNo = string.Empty;
        string div = string.Empty;

        public Frm_MD_MoldRepairImage()
        {
            InitializeComponent();
        }

        public Frm_MD_MoldRepairImage(string MoldCode, string RepairNo, string div)
        {
            InitializeComponent();
            this.MoldCode = MoldCode;
            this.RepairNo = RepairNo;
            this.div = div;
        }

        private void Frm_MD_MoldRepairImage_Load(object sender, EventArgs e)
        {
            PicImage.Properties.ShowMenu = false;   //오른쪽 클릭 막기

            Set_GvItemImage(Gv_ItemImage);
            Search();
        }

        #region GridView Setting

        private void Set_GvItemImage(GridView gv)
        {
            //거래처 Column 생성
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsView.ShowGroupPanel = false;

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

            if (div == "C")
                grid.Create_GvColumn(gv, "정기점검 번호", "RepairNo");
            else if (div == "R")
                grid.Create_GvColumn(gv, "세척/수리 번호", "RepairNo");

            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            //grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "경로", "Path");
            grid.Create_GvColumn(gv, "파일명", "FileNm");
            grid.Create_GvColumn(gv, "확장자", "FileExt");
            grid.Create_GvColumn(gv, "이미지", "RepairImg");
            grid.Create_GvColumn(gv, "이미지", "Btn_RepairImg", Btn_Img);
            grid.Create_GvColumn(gv, "비고", "Remark");

            //VISIBLE
            gv.Columns["Path"].Visible = false;
            gv.Columns["FileNm"].Visible = false;
            gv.Columns["FileExt"].Visible = false;
            gv.Columns["RepairImg"].Visible = false;

            //사용유무 설정
            grid.Column_AllowEdit(gv.Columns["Btn_RepairImg"]);
            grid.Column_AllowEdit(gv.Columns["RepairImg"]);
            grid.Column_AllowEdit(gv.Columns["Path"]);
            grid.Column_AllowEdit(gv.Columns["FileNm"]);
            grid.Column_AllowEdit(gv.Columns["FileExt"]);
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
            string status = drChanged["Status"].ConvertString_Null_To_Empty();

            string MoldCode = Convert.ToString(drChanged["MoldCode"]);

            /*
             * 프로세스 옵션
             * 제품 사양서 조회(I: 이미지, P: PDF)
             */

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

                    gv.SetFocusedRowCellValue("RepairImg", br.ReadBytes((int)fs.Length));

                    PicImage.Visible = false;
                    pdfViewer1.Visible = true;
                }
                else
                {
                    using (FileStream fs = new FileStream(localFileFullPath, FileMode.Open))
                    {
                        byte[] fileBytes = new byte[fs.Length];
                        fs.Read(fileBytes, 0, fileBytes.Length);

                        gv.SetFocusedRowCellValue("RepairImg", fileBytes);
                        PicImage.EditValue = fileBytes;
                    }

                    PicImage.Visible = true;
                    pdfViewer1.Visible = false;
                }

                drChanged["Path"] = Path.GetDirectoryName(localFileFullPath);
                drChanged["FileNm"] = Path.GetFileName(localFileFullPath);
                drChanged["FileExt"] = Path.GetExtension(localFileFullPath);
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

            if (ext == ".PDF")
            {
                PicImage.Visible = false;
                pdfViewer1.Visible = true;
                ItemPic.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemPdf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                byte[] buff = dr["RepairImg"] as byte[];

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
                PicImage.EditValue = dr["RepairImg"];
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

        public void Search()
        {
            MD_MoldRepairImage im = new MD_MoldRepairImage();
            im.MoldCode = MoldCode;
            im.RepairNo = RepairNo;

            Gc_ItemImage.DataSource = im.Select_MD_MoldRepairImage();
            Gv_ItemImage.BestFitColumns();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_Row_Ins.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): Btn_Row_Del.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("MoldCode", MoldCode);
                dic.Add("RepairNo", RepairNo);
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
            int SaveFocusedRowInx = (Gc_ItemImage.MainView as GridView).FocusedRowHandle;

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            //4. 정상절차 진행
            Search();

            //5. 저장 후 선택한 row를 불러옴
        }


        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    if (Convert.ToString(dr["MoldCode"]) == "")
                    {
                        MessageBox.Show("금형 항목이 비어있습니다.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정") { checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "삭제")
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
            MD_MoldRepairImage im = new MD_MoldRepairImage();

            im.MoldCode = Convert.ToString(dr["MoldCode"]);
            im.RepairNo = Convert.ToString(dr["RepairNo"]);
            im.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
            im.Path = Convert.ToString(dr["Path"]);
            im.FileNm = Convert.ToString(dr["FileNm"]);
            im.FileExt = Convert.ToString(dr["FileExt"]);
            im.RepairImg = dr["RepairImg"] as byte[];
            im.Remark = Convert.ToString(dr["Remark"]);

            //2. DB 적용
            if (div == 'I')
            {
                //Seq 구하기
                im.Seq = im.Select_NextSeq();

                //Insert
                if (!im.Ins_MD_MoldRepairImage())
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
                if (!im.Upd_MD_MoldRepairImage())
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

                //추가 변경사항 private 따로 만들기

            }
            else if (div == 'D')
            {
                //Delete
                if (!im.Del_MD_MoldRepairImage())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                else
                {

                }

                //추가 변경사항 private 따로 만들기
            }

            return true;
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
    }
}