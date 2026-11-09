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
    public partial class Frm_MD_Mold_Wash : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        private string[] RepairDiv = { "02", "04" }; //02 : 수리, 04 : 금형세척
        private DataTable _dtRepairImg;
        private int _imgIndex = 0;
        private string Part_Div_Cd = "20";     // 화면 타입(EQ(10) : 설비 , MD(20) : 금형) Cm_Class = '086'

        public Frm_MD_Mold_Wash()
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
            SetGrid_PartOut(Gv_PartOut);
        }

        private void SetGrid_PartOut(GridView gv)
        {
            /*//Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            BS_Part part = new BS_Part();
            part.Part_Div_Cd = Part_Div_Cd;
            RepositoryItemSearchLookUpEdit lup_Part_Cd = rpo.Create_RepositoryItemSearchLookUp(part.Select_MoldPartList(), "코드", "명칭", "규격");


            grid.Create_GvColumn(gv, "    ", "Status");
            grid.Create_GvColumn(gv, "출고번호", "Out_No");
            grid.Create_GvColumn(gv, "출고순번", "Out_Row_Seq");
            grid.Create_GvColumn(gv, "조회순번", "DispSeq");
            grid.Create_GvColumn(gv, "출고일자", "Out_Date");
            grid.Create_GvColumn(gv, "부품", "Part_Cd");
            grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "출고량", "Out_Qty");
            grid.Create_GvColumn(gv, "비고", "Rmk");

            grid.Create_GvColumn(gv, "등록자", "Ins_Emp_No");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "Upd_Emp_No");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Column_AllowEdit(gv.Columns["Part_Cd"]);
            grid.Column_AllowEdit(gv.Columns["Out_Date"]);
            grid.Column_AllowEdit(gv.Columns["Out_Qty"]);
            grid.Column_AllowEdit(gv.Columns["Rmk"]);

            gv.Columns["Part_Cd"].ColumnEdit = lup_Part_Cd;

            gv.Columns["DispSeq"].Visible = false;
            gv.Columns["Out_No"].Visible = false;
            gv.Columns["Out_Row_Seq"].Visible = false;
            gv.Columns["Ins_Emp_No"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["Upd_Emp_No"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid.SetCol_Type(gv.Columns["Out_Date"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Out_Qty"], clsGrid.TypeList.TFloat);

            grid.RowNumber_Indicator(gv);    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인*/
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
            RepositoryItem lup_Div = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD30"), "분류코드", "분류명");

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

            //세척시작시간,종료시간 칼럼
            RepositoryItemTimeEdit timeEdit = new RepositoryItemTimeEdit();
            timeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            timeEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            timeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm";
            timeEdit.TimeEditStyle = TimeEditStyle.TouchUI;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "구분", "RepairDiv", lup_Div);
            grid.Create_GvColumn(gv, "세척/수리 번호", "RepairNo");
            grid.Create_GvColumn(gv, "프로젝트명", "MoldPjtNo");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName", lup_Mold);
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "차수", "Chasu");

            grid.Create_GvColumn(gv, "고객사코드", "SupplyCust", lup_Cust);
            grid.Create_GvColumn(gv, "제작처코드", "ProdtCust");

            grid.Create_GvColumn(gv, "누적shot", "AccumulateShot");
            grid.Create_GvColumn(gv, "특수부품", "SpecialParts");
            //grid.Create_GvColumn(gv, "금형명", "SupplyCust", lup_Cust);
            grid.Create_GvColumn(gv, "세척/수리 일자", "RepairDate");
            grid.Create_GvColumn(gv, "세척/수리 내용", "RepairCause");
            grid.Create_GvColumn(gv, "세척/수리 내용", "RepairState");
            grid.Create_GvColumn(gv, "담당자", "RepairEmp", lup_Emp);
            grid.Create_GvColumn(gv, "시작시간", "RepairStartTime", timeEdit);
            grid.Create_GvColumn(gv, "종료시간", "RepairEndTime", timeEdit);
            grid.Create_GvColumn(gv, "소요시간(분)", "RepairTime");
            grid.Create_GvColumn(gv, "수리업체", "RepairCust", lup_Cust);
            grid.Create_GvColumn(gv, "이미지", "RepairImg", Btn_Img);
            grid.Create_GvColumn(gv, "수리금액", "RepairAmt");
            //grid.Create_GvColumn(gv, "최근세척Shot", "RepairShot");
            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Column_AllowEdit(gv.Columns["RepairDiv"], true);
            grid.Column_AllowEdit(gv.Columns["MoldName"], true);
            grid.Column_AllowEdit(gv.Columns["RepairCause"], true);
            grid.Column_AllowEdit(gv.Columns["RepairDate"], true);
            //grid.Column_AllowEdit(gv.Columns["RepairState"], true);
            grid.Column_AllowEdit(gv.Columns["RepairEmp"], true);
            grid.Column_AllowEdit(gv.Columns["RepairStartTime"], true);
            grid.Column_AllowEdit(gv.Columns["RepairEndTime"], true);
            grid.Column_AllowEdit(gv.Columns["RepairCust"], true);
            grid.Column_AllowEdit(gv.Columns["RepairAmt"], true);
            grid.Column_AllowEdit(gv.Columns["RepairImg"]);

            //Repository 세척날짜
            gv.Columns["RepairDate"].ColumnEdit = new RepositoryItemDateEdit();

            //수리금액
            gv.Columns["RepairAmt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gv.Columns["RepairAmt"].DisplayFormat.FormatString = "#,#";

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;         //상태
            gv.Columns["RepairDiv"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["RepairDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;    //수리일자
            gv.Columns["CreDt"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;  //등록일시
            gv.Columns["ModDt"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;  //수정일시
            gv.Columns["RepairTime"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;       //수리소요시간

            gv.Columns["MoldCode"].Visible = false;
            gv.Columns["RepairTime"].Visible = false;
            gv.Columns["RepairCust"].Visible = false;
            gv.Columns["RepairAmt"].Visible = false;
            gv.Columns["RepairState"].Visible = false;
            gv.Columns["RepairStartTime"].Visible = false;
            gv.Columns["RepairEndTime"].Visible = false;
            gv.Columns["RepairEmp"].Visible = false;
            gv.Columns["RepairState"].Visible = false;
            gv.Columns["RepairCause"].Visible = false;
            gv.Columns["SupplyCust"].Visible = false;
            gv.Columns["ProdtCust"].Visible = false;

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
            mEdit_Wash.Text = Convert.ToString(dr["RepairCause"]);

            //금형 이미지 조회
            Fun_Search_Image(MoldCode, RepairNo);
            Fun_Search_Part(RepairNo);
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            string fieldName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //수리시간 계산
            if (fieldName == "RepairStartTime" || fieldName == "RepairEndTime")
            {
                CalcRepairTime();
            }
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

            /*if (fieldName == "Part_Cd")
            {
                string partCd = Convert.ToString(drChanged["Part_Cd"]);
                BS_Part part = new BS_Part();
                part.Part_Div_Cd = Part_Div_Cd;
                part.Part_Cd = partCd;
                DataTable dt = part.Select_MoldPart();

                drChanged["Size"] = dt.Rows[0]["Size"].ToString();
            }*/

            gv.BestFitColumns();
        }

        private void CalcRepairTime()
        {
            string start_ = Gv_Mold.GetFocusedRowCellValue("RepairStartTime").ConvertString_Null_To_Empty();
            string end_ = Gv_Mold.GetFocusedRowCellValue("RepairEndTime").ConvertString_Null_To_Empty();

            DateTime start_Time = new DateTime();
            DateTime end_Time = new DateTime();

            if (DateTime.TryParse(start_, out start_Time) && DateTime.TryParse(end_, out end_Time))
            {
                Gv_Mold.SetFocusedRowCellValue("RepairTime", end_Time.Subtract(start_Time).TotalMinutes);
            }
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
                XtraMessageBox.Show("금형세척/수리 이력 선저장 필수");
                return;
            }

            string MoldCode = Convert.ToString(drChanged["MoldCode"]);
            string RepairNo = Convert.ToString(drChanged["RepairNo"]);
            /*
             * 프로세스 옵션
             * 제품 사양서 조회(I: 이미지, P: PDF)
             */

            Frm_MD_MoldRepairImage frm = new Frm_MD_MoldRepairImage(MoldCode, RepairNo, "R");
            frm.FormClosed += Form_FormClosed;
            frm.ShowDialog();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Func_Search_Wash();
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

        private void Btn_PartRowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (txt_Act == "Ins")
            {
                int rowHandle = Gv_Mold.FocusedRowHandle;

                // 포커스된 행이 있는 경우만 체크
                if (rowHandle >= 0 && !Gv_Mold.IsNewItemRow(rowHandle))
                {
                    object status = Gv_Mold.GetRowCellValue(rowHandle, "Status");

                    if (status != null && status.ToString() == "신규")
                    {
                        MessageBox.Show("수리내역을 저장 후 등록 가능합니다.");
                        return;
                    }
                }

                GridView gv = Gv_PartOut;
                DataTable dt = gv.GridControl.DataSource as DataTable;

                DateTime out_date = DateTime.Now;

                dic.Add("Status", "신규");
                dic.Add("Out_Date", out_date);
                dic.Add("Part_Cd", "");
                dic.Add("Size", "");
                dic.Add("Out_Qty", 0);

            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Mold.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }
            grid.Set_Row(Gc_PartOut, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            clsGrid grid = new clsGrid();
            grid.Check_OverLap_CellValue(sender as GridView, e, "MoldCode");
        }

        #endregion GridView Setting End


        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_Mold_Wash_Load(object sender, EventArgs e)
        {
            Func_Search_Wash();
        }

        #endregion Event End

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Func_Search_Wash();
        }

        private void Func_Search_Wash()
        {
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

        private void Fun_Search_Part(string RepairNo)
        {
            //EQ_PartOut matOut = new EQ_PartOut();
            //Gc_PartOut.DataSource = matOut.SelectPartOut(RepairNo);
            //
            //(Gc_PartOut.MainView as GridView).BestFitColumns();
        }

        #endregion 조회 기능 End

        #region 저장 기능

        #region 금형저장

        private void BtnSave_Mold_Click(object sender, EventArgs e)
        {
            Func_Save_Mold_Wash();
        }

        private void Func_Save_Mold_Wash()
        {
            GridView gv = Gv_Mold;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = Gc_Mold.DataSource as DataTable;

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

            Func_Search_Wash();

            // 저장 후 선택한 row를 불러옴
            (Gc_Mold.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        private void BtnSave_Part_Click(object sender, EventArgs e)
        {
            Func_Save_Mold_Part();
        }

        private void Func_Save_Mold_Part()
        {
            /*//작성내역 저장
            DataTable dt = Gc_PartOut.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Part_Check_SaveClick(dt)) return;

            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!SaveMoldPart_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!SaveMoldPart_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!SaveMoldPart_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            //선택한 행 번호 저장
            int SaveFocusedRowInx = Gv_Mold.FocusedRowHandle;

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            Func_Search_Wash();

            // 저장 후 선택한 row를 불러옴
            (Gc_Mold.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;*/
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

                    if (dr["RepairDiv"].ConvertString_Null_To_Empty() == "")
                    {
                        MessageBox.Show("구분 항목이 비어있습니다.");
                        return false;
                    }
                }
                if (Convert.ToString(dr["Status"]) == "수정")
                {
                    if (dr["RepairDiv"].ConvertString_Null_To_Empty() == "")
                    {
                        MessageBox.Show("구분 항목이 비어있습니다.");
                        return false;
                    }

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

        private bool Part_Check_SaveClick(DataTable dt)
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
                    if (dr.Table.Columns.Contains("Part_Cd") && Convert.ToString(dr["Part_Cd"]) == "")
                    {
                        MessageBox.Show("부품 항목이 비어있습니다.");
                        return false;
                    }

                    if (dr["Out_Qty"].ConvertString_Null_To_Empty() == "" || dr["Out_Qty"].ConvertString_Null_To_Empty() == "0")
                    {
                        MessageBox.Show("출고량 항목이 비어있습니다.");
                        return false;
                    }
                }
                if (Convert.ToString(dr["Status"]) == "수정")
                {
                    if (dr["Out_Qty"].ConvertString_Null_To_Empty() == "" || dr["Out_Qty"].ConvertString_Null_To_Empty() == "0")
                    {
                        MessageBox.Show("출고량 항목이 비어있습니다.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {
                    checkCnt++;
                }
            }

            if (checkCnt == 0) return false;

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

       /* private bool SaveMoldPart_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            MD_Mold Mold = new MD_Mold();
            try
            {
                //미리 dr에 입력
                if (div == 'I')
                {
                    Create_Part_No(dr);
                    Create_Part_DispSeq(dr);
                }

                string repirNo = Gv_Mold.GetFocusedRowCellValue("RepairNo")?.ToString();

                EQ_PartOut matOut = new EQ_PartOut();
                matOut.Out_No = Convert.ToString(dr["Out_No"]);                        //출고번호
                matOut.Out_Row_Seq = Convert.ToInt16(dr["Out_Row_Seq"]);               //출고순번
                matOut.DispSeq = Convert.ToInt16(dr["DispSeq"]);                     //조회순번
                matOut.Out_Date = dr["Out_Date"].ConvertDate_To_Char8();               //출고일자
                matOut.Part_Cd = Convert.ToString(dr["Part_Cd"]);                      //품목
                matOut.Out_Qty = dr["Out_Qty"].ConvertDecimal_Null_To_Zero();          //수량
                matOut.Rmk = Convert.ToString(dr["Rmk"]);                              //비고
                matOut.Out_Div_Cd = "10";                                              //출고구분 10:자재, 29: 사출품
                matOut.Part_Div_Cd = "20";
                matOut.Size = Convert.ToString(dr["Size"]);
                matOut.RepairNo = repirNo;

                //2. DB 적용
                if (div == 'I')
                {
                    //Insert
                    if (!matOut.InsertPartOut()) { return false; }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!matOut.UpdatePartOut()) { return false; }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!matOut.DeletePartOut()) { return false; }
                }
                return true;
            }
            catch
            {
                MessageBox.Show("값 형태변환 에러");
            }

            return true;
        }*/

        private void Create_Part_No(DataRow dr)
        {
            cls_Database cDB = new cls_Database();
            int No_Seq = cDB.Get_Last_NoSeq("EQ_PartOut", "Out_No", dr["Out_Date"].ConvertDate_To_Char8(), "PO");

            /*
             * Date가 같은데이터가 있는가?
             *      Yes : Status = "신규"인 항목이 더 있는가?
             *          Yes : 그중 No가 입력된 Row이 있는가?
             *              Yes : 해당 Row 중 거래처가 dr["Cust_Cd"]가 같은항목이 있는가?
             *                  Yes : dr[No] = drComp[No]
             *                  No : No_Seq = LastSeq
             *              No : No_Seq = LastSeq
             *          No : No_Seq = LastSeq
             *      No: No_Seq = LastSeq
            */

            dr["Out_No"] = "PO" + dr["Out_Date"].ConvertDate_To_Char8() + "-" + No_Seq.ToString("D4");

            foreach (DataRow drComp in dr.Table.Rows)
            {
                string status = Convert.ToString(drComp["Status"]);
                string out_No = Convert.ToString(drComp["Out_No"]);
                if (drComp["Out_Date"].ConvertDate_To_Char8() == dr["Out_Date"].ConvertDate_To_Char8()
                    && status == "신규"
                    && out_No != ""
                    )
                {
                    dr["Out_No"] = drComp["Out_No"];
                }

                //현재행까지 왔는지 검사
                if (drComp == dr) break;
            }
        }

        private void Create_Part_DispSeq(DataRow dr)
        {
            int disp = 1;
            /*
             * Date가 같은데이터가 있는가?
             *      Yes : Status = "신규"인 항목이 더 있는가?
             *          Yes : 그중 No가 입력된 Row이 있는가?
             *              Yes : drComp[In_No] == dr[In_No]
             *                  Yes : disp++;
             *                  No : disp = 유지
             *              No : disp = 유지
             *          No : disp = 유지
             *      No: disp = 유지
            */

            foreach (DataRow drComp in dr.Table.Rows)
            {
                //현재행까지 왔는지 검사
                if (drComp == dr) break;

                string status = Convert.ToString(drComp["Status"]);
                string out_No = Convert.ToString(drComp["Out_No"]);
                if (drComp["Out_Date"].ConvertDate_To_Char8() == dr["Out_Date"].ConvertDate_To_Char8()
                    && status == "신규"
                    && out_No != ""
                    )
                {
                    disp++;
                }
            }

            dr["DispSeq"] = disp;
            dr["Out_Row_Seq"] = disp;
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