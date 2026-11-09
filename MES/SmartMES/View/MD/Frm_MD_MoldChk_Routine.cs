using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_MoldChk_Routine : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        private string[] RepairDiv = { "20" };
        clsRepository rpo = new clsRepository();
        private DataTable _dtRepairImg;
        private int _imgIndex = 0;
        private string Part_Div_Cd = "20";     // 화면 타입(EQ(10) : 설비 , MD(20) : 금형) Cm_Class = '086'

        public Frm_MD_MoldChk_Routine()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_SearchLookUp();
            Set_GridView();

            //이미지
            pictureEdit1.Properties.ExportMode = ExportMode.Value;
            pictureEdit1.Properties.PictureStoreMode = PictureStoreMode.ByteArray;
            pictureEdit2.Properties.ExportMode = ExportMode.Value;
            pictureEdit2.Properties.PictureStoreMode = PictureStoreMode.ByteArray;

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Mold, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_SearchLookUp()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            cls_Lookup clsLup = new cls_Lookup();
            MD_Mold mold = new MD_Mold();

            DataTable dt_Mold = mold.Select_Mold_Mgmt_No(true);
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Mold, dt_Mold, "코드", "금형명");
            Lup_Mold.smtLookUpValue = "ALL";
        }

        #region GridView Setting

        private void Set_GridView()
        {
            SetGrid_Mold(Gv_Mold);
        }

        private void SetGrid_Mold(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            //Repository 설정
            CM_Cust cust = new CM_Cust();
            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(cust.Select_Cust_List(new string[] { }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            //금형
            MD_Mold MD_Mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(MD_Mold.Select_Mold_Mgmt_No(), "코드", "금형명");

            CM_CateItem cm = new CM_CateItem();
            RepositoryItem lup_Div = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD40"), "분류코드", "분류명");

            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;

            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);
            Btn_Img.ButtonClick += Btn_Grid_Click;

            RepositoryItemButtonEdit Btn_WorkSheet = new RepositoryItemButtonEdit();
            Btn_WorkSheet.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_WorkSheet.Buttons.RemoveAt(0);
            Btn_WorkSheet.Buttons.Add(Btn_Img_Move);
            Btn_WorkSheet.ButtonClick += Btn_Grid_Click;

            RepositoryItemButtonEdit Btn_WorkSheet_History = new RepositoryItemButtonEdit();
            Btn_WorkSheet_History.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_WorkSheet_History.Buttons.RemoveAt(0);
            Btn_WorkSheet_History.Buttons.Add(Btn_Img_Move);
            Btn_WorkSheet_History.ButtonClick += Btn_Grid_Click;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "구분", "RepairDiv", lup_Div);
            grid.Create_GvColumn(gv, "점검번호", "RepairNo");
            grid.Create_GvColumn(gv, "프로젝트명", "MoldPjtNo");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName", lup_Mold);
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "차수", "Chasu");
            grid.Create_GvColumn(gv, "누적shot", "AccumulateShot");
            grid.Create_GvColumn(gv, "특수부품", "SpecialParts");
            grid.Create_GvColumn(gv, "점검 일자", "RepairDate");
            grid.Create_GvColumn(gv, "이미지", "RepairImg", Btn_Img);

            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Column_AllowEdit(gv.Columns["MoldName"], true);
            grid.Column_AllowEdit(gv.Columns["RepairDate"], true);
            grid.Column_AllowEdit(gv.Columns["RepairImg"]);

            //Repository 세척날짜
            gv.Columns["RepairDate"].ColumnEdit = new RepositoryItemDateEdit();

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;         //상태
            gv.Columns["RepairDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;    //수리일자
            gv.Columns["CreDt"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;  //등록일시
            gv.Columns["ModDt"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;  //수정일시

            gv.Columns["MoldCode"].Visible = false;

            //이벤트 설정
            grid.RowNumber_Indicator(gv);    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            //grid.SetInfo_ByObject(gv);
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;   //Row 변경시 하위항목 조회
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);
            string MoldCode = Convert.ToString(dr["MoldCode"]);
            string RepairNo = Convert.ToString(dr["RepairNo"]);

            //세척내용 memoedit에 표시
            mEdit_Wash.Text = Convert.ToString(dr["RePairCause"]);

            //금형 이미지 조회
            Fun_Search_Image(MoldCode, RepairNo);
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            string fieldName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //금형 수정 시
            if (fieldName == "MoldName")
            {
                grid.SetInfo_Mold(drChanged, Convert.ToString(e.Value));

                string moldCd = Convert.ToString(drChanged["MoldCode"]);
                MD_Mold mold = new MD_Mold();
                mold.MoldCode = moldCd;
                DataTable dt = mold.Select_Mold();

                drChanged["SpecialParts"] = dt.Rows[0]["SpecialParts"].ToString();
            }

            gv.BestFitColumns();
        }

        private void Btn_Grid_Click(object sender, ButtonPressedEventArgs e)
        {
            //제품이미지, 제품사양서 등록

            ButtonEdit btn = sender as ButtonEdit;
            if (btn == null) return;

            GridView gv = Gc_Mold.MainView as GridView;
            if (gv == null) return;

            DataRow drChanged = (Gc_Mold.MainView as GridView).GetFocusedDataRow();
            string fieldName = gv.FocusedColumn.FieldName;
            string status = drChanged["Status"].ConvertString_Null_To_Empty();

            if (status == "신규")
            {
                XtraMessageBox.Show("금형정기점검 이력 선저장 필수");
                return;
            }

            string MoldCode = Convert.ToString(drChanged["MoldCode"]);
            string RepairNo = Convert.ToString(drChanged["RepairNo"]);
            /*
             *
             * 프로세스 옵션
             * 제품 사양서 조회(I: 이미지, P: PDF)
             */

            //Frm_BS_MoldRepairImage frm = new Frm_BS_MoldRepairImage(MoldCode, RepairNo, "C");
            //frm.ShowDialog();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_MoldRow_Ins.PerformClick(); break;   //행삽입
                //case (Keys.Alt | Keys.A): Btn_MoldRow_Add.PerformClick(); break;   //행추가
                case (Keys.Alt | Keys.D): Btn_MoldRow_Del.PerformClick(); ; break; //행삭제
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

            if (txt_Act == "Ins")
            {
                string RepairDate = DateTime.Now.ToString("d");

                GridView gv = Gv_Mold;
                bool existsNewRow = false;
                for (int i = 0; i < gv.RowCount; i++)
                {
                    if (Convert.ToString(gv.GetRowCellValue(i, "Status")) == "신규")
                    {
                        existsNewRow = true;
                        break;
                    }
                }
                if (existsNewRow)
                {
                    MessageBox.Show("이미 추가된 신규 행이 있습니다.\n저장 후 다시 추가하세요.");
                    return;
                }
                DataTable dt = gv.GridControl.DataSource as DataTable;
                string RepairNo;

                dic.Add("Status", "신규");
                dic.Add("RepairDiv", "20");
                dic.Add("RepairDate", RepairDate);

                cls_Database cDB = new cls_Database();
                RepairDate = DateTime.Parse(RepairDate).ToString("yyyyMMdd");
                //string repairDate = dt.Rows[cur_Idx]["RepairDate"].ConvertDate_To_Char8();
                int no_Seq = cDB.Get_Last_NoSeq("MD_MoldRepair", "RepairNo", RepairDate);
                RepairNo = "RP" + RepairDate + "-" + no_Seq.ToString("D4");

                //dt.Rows[cur_Idx]["RepairNo"] = RepairNo;
                dic.Add("RepairNo", RepairNo);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Mold.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            grid.Set_Row(Gc_Mold, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_MoldChk_Routine_Load(object sender, EventArgs e)
        {
            Func_Search_Chk();
        }

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            clsGrid grid = new clsGrid();
            grid.Check_OverLap_CellValue(sender as GridView, e, "MoldCode");
        }

        #endregion Event End

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Func_Search_Chk();
        }

        private void Func_Search_Chk()
        {
            CM_CommonFile cf = new CM_CommonFile();
            cf.ClassItemCode = "M204020";
            DataRow dr = cf.Select_CM_CommonFile_Img();
            if (dr != null)
            {
                pictureEdit1.EditValue = dr["Pic"];
            }
            else
                pictureEdit1.EditValue = null;

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

            MD_MoldRepair Mold = new MD_MoldRepair();
            Mold.FocusMoldCd = string.Empty;
            Mold.rstFrom = startDate;
            Mold.rstTo = endDate;
            Mold.MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_Mold.DataSource = Mold.Search_Repair(RepairDiv);

            Gv_Mold.BestFitColumns();
        }

        private void Fun_Search_Image(string MoldCode, string RepairNo)
        {
            //금형이미지 조회
            MD_MoldRepairImage mold = new MD_MoldRepairImage();
            _dtRepairImg = mold.Select_RepairImg(MoldCode, RepairNo);
            _imgIndex = 0;

            Show_Image();
        }

        #endregion 조회 기능 End

        #region 저장 기능

        #region 금형저장

        private void BtnSave_Mold_Click(object sender, EventArgs e)
        {
            Func_Save_Mold_Chk();
        }

        private void Func_Save_Mold_Chk()
        {
            GridView gv = Gv_Mold;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = Gc_Mold.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!SaveMold_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!SaveMold_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!SaveMold_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            PictureSave();

            //선택한 행 번호 저장
            int SaveFocusedRowInx = Gv_Mold.FocusedRowHandle;

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            Func_Search_Chk();

            // 저장 후 선택한 row를 불러옴
            (Gc_Mold.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;

            //삭제내역 있으면 물어보기

            if (dt.Select("Status = '삭제'").Length > 0)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    checkCnt++;
                    if (dr.Table.Columns.Contains("MoldCode") && Convert.ToString(dr["MoldCode"]) == "")
                    {
                        MessageBox.Show("금형 항목이 비어있습니다.");
                        return false;
                    }
                }
                if (Convert.ToString(dr["Status"]) == "수정")
                {
                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {
                    checkCnt++;
                }
            }

            //if (checkCnt == 0) return false;

            return true;
        }

        private bool SaveMold_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            MD_MoldRepair Mold = new MD_MoldRepair();
            try
            {
                Mold.Seq = Convert.ToString(dr["Seq"]); //Seq
                //Mold.Seq = Convert.ToInt32(dr["Seq"]); //Seq
                Mold.MoldCode = Convert.ToString(dr["MoldCode"]);  //설비코드
                Mold.RepairNo = dr["RepairNo"].ConvertString_Null_To_Empty();  //수리번호

                DateTime startTime = new DateTime();
                DateTime endTime = new DateTime();

                string start = "";
                string end = "";

                if (DateTime.TryParse(Convert.ToString(dr["RepairStartTime"]), out startTime)) start = startTime.ToString("yyyy-MM-dd HH:mm");
                if (DateTime.TryParse(Convert.ToString(dr["RepairEndTime"]), out endTime)) end = endTime.ToString("yyyy-MM-dd HH:mm");

                if (div != 'D')
                {
                    //미리 dr에 입력
                    Create_DispSeq(dr);
                    Mold.MoldCode = dr["MoldCode"].ConvertString_Null_To_Empty();
                    Mold.RepairDiv = dr["RepairDiv"].ConvertString_Null_To_Empty();
                    Mold.DispSeq = dr["DispSeq"].ConvertString_Null_To_Empty();
                    Mold.RepairCause = dr["RepairCause"].ConvertString_Null_To_Empty();
                    Mold.RepairCause = mEdit_Wash.Text;  //고장내용
                    Mold.RepairState = dr["RepairState"].ConvertString_Null_To_Empty();  //수리내용
                    Mold.RepairEmp = dr["RepairEmp"].ConvertString_Null_To_Empty(); //수리 담당자
                    Mold.RepairCust = dr["RepairCust"].ConvertString_Null_To_Empty();  //거래처
                    Mold.RepairAmt = dr["RepairAmt"].ConvertInt_Null_To_Zero(); //수리금액
                    Mold.RepairStartTime = start; //수리시작시간
                    Mold.RepairEndTime = end; //수리종료시간
                    Mold.RepairTime = dr["RepairTime"].ConvertInt_Null_To_Zero(); //수리시간
                    //수리날짜
                    if (Convert.ToString(dr["RepairDate"]) != "")
                    {
                        Mold.RepairDate = Convert.ToDateTime(dr["RepairDate"]).ToString("yyyyMMdd");
                    }
                    else
                    {
                        Mold.RepairDate = "";
                    }
                }
            }
            catch
            {
                MessageBox.Show("값 형태변환 에러");
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!Mold.Ins_Repair())
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
                if (!Mold.Upd_Repair())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else
            if (div == 'D')
            {
                //Delete
                if (!Mold.Del_MD_MoldRepair())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //이미지
                MD_MoldRepairImage bmri = new MD_MoldRepairImage();
                bmri.MoldCode = Mold.MoldCode;
                bmri.RepairNo = Mold.RepairNo;

                if (!bmri.Del_item_Image_ByItemCd())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr[0] = "";

            return true;
        }

        private void Create_DispSeq(DataRow dr)
        {
            //DispSeq 표시순번 검출
            //Seq는 Mssql 테이블에서 처리하도록 설정(ID사양 1씩 증가하게 설정)
            int disp = 1;

            foreach (DataRow drComp in dr.Table.Rows)
            {
                //현재행까지 왔는지 검사
                if (drComp == dr) break;

                string status = Convert.ToString(drComp["Status"]);
                string MoldCode = Convert.ToString(drComp["MoldCode"]);
                string Seq = Convert.ToString(drComp["Seq"]);
                string DispSeq = Convert.ToString(drComp["DispSeq"]);

                if (Convert.ToString(drComp["MoldCode"]) == Convert.ToString(dr["MoldCode"]))
                {
                    if (status != "삭제")
                    {
                        ++disp;
                    }
                }
            }
            dr["DispSeq"] = disp;
        }

        #endregion

        #region 금형이미지 저장

        private void PictureSave()
        {
            DataRow dr = Gv_Mold.GetFocusedDataRow();
            byte[] imageR = pictureEdit2.EditValue as byte[];

            MD_MoldRepairImage mold = new MD_MoldRepairImage();
            mold.RepairNo = dr["RepairNo"].ConvertString_Null_To_Empty();
            mold.MoldCode = Convert.ToString(dr["MoldCode"]);
            mold.Seq = 1;
            mold.RepairImg = imageR;

            if (!mold.SaveMoldRepairImage())
            {
                MessageBox.Show("이미지 저장 중 에러가 발생하였습니다.");
            }
        }

        #endregion 금형이미지 저장End

        #endregion 저장 기능 End

        private void mEdit_Wash_EditValueChanged(object sender, EventArgs e)
        {
            GridView gv = Gv_Mold;
            if (gv.FocusedRowHandle < 0) return;

            DataRow row = gv.GetFocusedDataRow();
            if (row["Status"] == "신규")
                return;
            if (row != null)
            {
                if (row["RepairCause"].ToString() != mEdit_Wash.EditValue)
                {
                    row["RepairCause"] = mEdit_Wash.EditValue;
                    row["Status"] = "수정";
                }
            }

            gv.PostEditor();
            gv.UpdateCurrentRow();
        }

        private void Show_Image()
        {
            if (_dtRepairImg == null || _dtRepairImg.Rows.Count == 0)
            {
                pictureEdit2.EditValue = null;
                pictureEdit2.Properties.NullText = "No image data";
                return;
            }

            if (_imgIndex < 0) _imgIndex = 0;
            if (_imgIndex >= _dtRepairImg.Rows.Count)
                _imgIndex = _dtRepairImg.Rows.Count - 1;

            object imgObj = _dtRepairImg.Rows[_imgIndex]["RepairImg"];

            if (imgObj == DBNull.Value)
            {
                pictureEdit2.EditValue = null;
            }
            else
            {
                pictureEdit2.EditValue = imgObj; // varbinary(max) 그대로 OK
            }
        }

        private void btnNextImage_Click(object sender, EventArgs e)
        {
            if (_dtRepairImg == null || _dtRepairImg.Rows.Count == 0)
                return;

            _imgIndex = (_imgIndex + 1) % _dtRepairImg.Rows.Count;
            Show_Image();
        }

        private void btnPrevImage_Click(object sender, EventArgs e)
        {
            if (_dtRepairImg == null || _dtRepairImg.Rows.Count == 0)
                return;

            _imgIndex = (_imgIndex - 1 + _dtRepairImg.Rows.Count) % _dtRepairImg.Rows.Count;
            Show_Image();
        }

        private void BtnSave_Img_Click(object sender, EventArgs e)
        {
            byte[] imageL = pictureEdit1.EditValue as byte[];

            CM_CommonFile file = new CM_CommonFile();
            file.ClassItemCode = "M204020"; //금형 정기점검
            file.FileSeq = file.GetNextSeq();
            file.Pic = imageL;

            if (!file.Ins_CM_CommonFile_Img())
            {
                MessageBox.Show("이미지저장 중 에러가 발생하였습니다.");
                return;
            }

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_Mold);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}