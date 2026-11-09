using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
	public partial class Frm_CM_PartList : DevExpress.XtraEditors.XtraForm
    {
        /*
         * 파트리스트 등록 화면
         * 사용업체 : 에스앤씨 예정
         * 활용화면 : BOM
         */
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        private string[] Base_Item_Div = new string[] {"10", "20" };             //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 원자재, 50 : 부자재, 60 : 소모자재)
        string S_Base_Item_Div = "10, 20";
        private string[] ItemDiv = { "40", "50","60","80" }; //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 원자재, 50 : 부자재, 60 : 소모자재,70:지그,80:자재기타)
        string S_Item_Div = "40,50,60,80";
        private string[] Cust_Div_Cd = new string[] { "40", "20" };   //거래처구분 리스트(05 : 매입/매출, 10 : 매입, 20 : 매출, 30 : 외주, 40 : 납품)
        string S_Cust_Div = "20,40";

        public Frm_CM_PartList()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //RadioGroup 데이터 입력
            Set_BaseItem_RadioGroup(radioGroup_BaseItem);
            Set_Cust_RadioGroup(radioGroup_Cust);

            //GridView 데이터 입력
            Set_GridView();
        }

        #region RadioGroup Setting

        private void Set_BaseItem_RadioGroup(RadioGroup radio)
        {
            //기준품목 RadioGroup 아이템 입력
            clsRadioGroup clsRadio = new clsRadioGroup();
            DataTable dt = code.smtLookUp("품목구분", S_Item_Div, "전체");
            clsRadio.Create_RadioGroupItem(radio, dt, "코드명", "코드");

            radio.EditValue = "ALL";  //초기값

            radio.EditValueChanged += Radio_EditValueChanged_Base;
        }

        private void Radio_EditValueChanged_Base(object sender, EventArgs e)
        {
            Search_BaseItem();
        }

        private void Set_Cust_RadioGroup(RadioGroup radio)
        {
            //구성품목 RadioGroup 아이템 입력
            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(radio, code.smtLookUp("거래처구분", S_Cust_Div, "", ""), "분류명", "분류코드");

            radio.EditValue = "ALL";  //초기값

            radio.EditValueChanged += Radio_EditValueChanged_Cust;
        }

        private void Radio_EditValueChanged_Cust(object sender, EventArgs e)
        {
            Search_Cust();
        }

        #endregion RadioGroup Setting End

        #region GridView Setting

        private void Set_GridView()
        {
            //GridView 셋팅
            Set_GvBaseItem(Gv_BaseItem);      //기준품목
            Set_GvCust(Gv_Cust);    //구성품목
            Set_GvPartList(Gv_PartList);       //작업

            Gv_BaseItem.BeforeLeaveRow += Gv_BeforeLeaveRow_Item; //선택 행 변경 막기
            Gv_BaseItem.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
            Gv_Cust.BeforeLeaveRow += Gv_BeforeLeaveRow_Cust; //선택 행 변경 막기
            Gv_Cust.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

        private void Set_GvBaseItem(GridView gv)
        {
            //기준, 구성품목 Column 생성
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //gv.ShowFindPanel(); //검색창 보이기
            //gv.OptionsFind.FindPanelLocation = GridFindPanelLocation.Panel; //검색창이 GroupPanel이 아닌 단독으로 나오게하기
            //gv.OptionsFind.AlwaysVisible = false;

            gv.OptionsView.ShowAutoFilterRow = true;

            //RepositoryItem 생성
            clsRepository rpo = new clsRepository();
            RepositoryItemLookUpEdit lup_Item_Div_Cd = rpo.Create_RepositoryItem(code.smtLookUp("품목구분", "", ""), "코드", "코드명");

            //Column 생성
            grid.Create_GvColumn(gv, "순번", "DispSeq");
            grid.Create_GvColumn(gv, "분류", "Item_Div_Cd", lup_Item_Div_Cd);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "품번", "Alias");
            grid.Create_GvColumn(gv, "규격", "Standard");
            grid.Create_GvColumn(gv, "등록여부", "PartList_Yn");

            gv.Columns["PartList_Yn"].Visible = false;

            grid.Column_AllowEdit(gv.Columns["ItemName"], false);

            gv.Columns["DispSeq"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //순번 셀값 정렬
            //gv.RowStyle += Gv_RowStyle;    //검사데이터 여부에따른 색상 지정
        }

        private void Set_GvCust(GridView gv)
        {
            //기준, 구성품목 Column 생성
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //gv.ShowFindPanel(); //검색창 보이기
            //gv.OptionsFind.FindPanelLocation = GridFindPanelLocation.Panel; //검색창이 GroupPanel이 아닌 단독으로 나오게하기
            //gv.OptionsFind.AlwaysVisible = false;

            gv.OptionsView.ShowAutoFilterRow = true;

            //RepositoryItem 생성
            //Column 생성
            grid.Create_GvColumn(gv, "거래처구분", "CustTypeName");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");
        }

        private void Gv_BeforeLeaveRow_Item(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (!Check_BaseItem_Click())
            {
                //e.Allow = false; // 선택된 행 변경을 막습니다.
            }
        }

        private void Gv_BeforeLeaveRow_Cust(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (!Check_Cust_Click())
            {
                //e.Allow = false; // 선택된 행 변경을 막습니다.
            }
        }

        private void Set_GvPartList(GridView gv)
        {
            //기준, 구성품목 Column 생성
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //gv.OptionsFind.FindPanelLocation = GridFindPanelLocation.Panel; //검색창이 GroupPanel이 아닌 단독으로 나오게하기
            //gv.OptionsFind.AlwaysVisible = true;

            //RepositoryItem 생성
            clsRepository rpo = new clsRepository();
            clsCode code = new clsCode();
            RepositoryItemLookUpEdit lup_Item = rpo.Create_RepositoryItem(code.smtLookUp("품목", "", "", ""), "품번코드", "품명");
            RepositoryItemLookUpEdit lup_Cust = rpo.Create_RepositoryItem(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "모품명", "Parent_ItemCode", lup_Item);
            //grid.Create_GvColumn(gv, "거래처코드", "Cust_Cd");
            grid.Create_GvColumn(gv, "거래처명", "CustCode", lup_Cust);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "품번", "Alias");
            grid.Create_GvColumn(gv, "규격", "Spec");
            grid.Create_GvColumn(gv, "단위", "Unit");
            grid.Create_GvColumn(gv, "소요량", "Need_Qty");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            DataTable dt = code.smtLookUp("품목", "", S_Item_Div, "", "");    //품목 Type1:거래처, Type2:품목구분
            RepositoryItemSearchLookUpEdit lup_ItemCode = rpo.Create_RepositoryItemSearchLookUp(dt, "품번코드", "품명");
            //grid.SetLupView_Item(lup_ItemCode, "제품코드 초기화");

            //등록,수정자
            //SC_Emp ScEmp = new SC_Emp();
            //RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(ScEmp.Select_Emp_CdNm(), "Emp_No", "Emp_Nm");

            //gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            //gv.Columns["ModId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ItemName"].ColumnEdit = lup_ItemCode;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ItemName"]);
            grid.Column_AllowEdit(gv.Columns["Need_Qty"]);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //순번 셀값 정렬

            grid.SetCol_Type(gv.Columns["Need_Qty"], clsGrid.TypeList.TFloat);

			gv.CellValueChanged += Gv_CellValueChanged;
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
            string ColName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //품목 선택시
            if (ColName == "ItemName")
            {
                grid.SetInfo_Item(drChanged, Convert.ToString(e.Value));
            }

            Gv_PartList.BestFitColumns();
        }


        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                //case (Keys.Alt | Keys.I): Btn_Row_Ins.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): Btn_Row_Del.PerformClick(); break; //행삭제
            }
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search_Click();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (Gv_BaseItem.FocusedRowHandle < 0) return;
            if (Gv_Cust.FocusedRowHandle < 0) return;

            //기준품목 FocusedRowChanged 진행
            Search_PartList(Gv_BaseItem.GetFocusedDataRow()["ItemCode"].ToString(), Gv_Cust.GetFocusedDataRow()["CustCode"].ToString());   //파트리스트 조회
        }

        //private void Gv_RowStyle(object sender, RowStyleEventArgs e)
        //{
        //    //삭제시 배경색 지정
        //    GridView gv = sender as GridView;
        //    if (e.RowHandle == -1) return;
        //    if (gv.Name != "Gv_BaseItem") return;

        //    //검사 등록 내역 있는지 색 표기
        //    DataTable dt = gv.GridControl.DataSource as DataTable;
        //    if (dt == null) return;

        //    if (e.RowHandle > -1 && Convert.ToString(dt.Rows[e.RowHandle]["PartList_Yn"]) == "N")
        //    {
        //        e.Appearance.BackColor = Color.Gray;
        //    }
        //    else
        //    {
        //        e.Appearance.BackColor = Color.Transparent;
        //    }
        //}

        private void Frm_CM_PartList_Load(object sender, EventArgs e)
        {
            Search_BaseItem();      //기준품목 조회
            Search_Cust();    //구성품목 조회
        }

        #endregion Event End

        #region 조회버튼 클릭

        private void Search_Click()
        {
            //조회버튼 클릭
            Search_BaseItem();      //기준품목 조회
            Search_Cust();    //구성품목 조회
        }

        private void Search_BaseItem()
        {
            //기준품목 조회
            CM_Item cm = new CM_Item();

            Gc_BaseItem.DataSource = cm.Select_PartList_BaseItem(Base_Item_Div);

            Gv_BaseItem.BestFitColumns();   //Column 너비 조정
        }

        private void Search_Cust()
        {
            //구성품목 조회
            string cust_Div = Convert.ToString(radioGroup_Cust.EditValue);

            CM_Cust cm = new CM_Cust();
            Gc_Cust.DataSource = cm.SelectCustomer(new string[] { cust_Div});

            Gv_Cust.BestFitColumns(); //Column 너비 조정
        }

        #endregion 조회버튼 클릭 End

        #region 기준품목 더블클릭

        private void Search_PartList(string ItemCode, string cust_Cd)
        {
            //기준품목 더블클릭 기능
            CM_PartList part = new CM_PartList();
            part.Parent_ItemCode = ItemCode;
            part.CustCode = cust_Cd;

            Gc_PartList.DataSource = part.Select_PartList();

            Gv_PartList.BestFitColumns();
        }

        private bool Check_BaseItem_Click()
        {
            //선택된 행이 없으면 종료
            DataRow drBaseItem = Gv_BaseItem.GetFocusedDataRow();
            if (drBaseItem == null) return false;

            //신규생성이나 수정한 행이 있을경우
            if (Gc_PartList.DataSource != null)
            {
                foreach (DataRow dr in (Gc_PartList.DataSource as DataTable).Rows)
                {
                    if (Convert.ToString(dr["Status"]) == "신규" || Convert.ToString(dr["Status"]) == "수정")
                    {
                        if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //다른항목 진행할 경우
                            return true;
                        }
                        else
                        {
                            //다른항목 진행하지 않을경우
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool Check_Cust_Click()
        {
            //선택된 행이 없으면 종료
            DataRow drCust = Gv_Cust.GetFocusedDataRow();
            if (drCust == null) return false;

            //신규생성이나 수정한 행이 있을경우
            if (Gc_PartList.DataSource != null)
            {
                foreach (DataRow dr in (Gc_PartList.DataSource as DataTable).Rows)
                {
                    if (Convert.ToString(dr["Status"]) == "신규" || Convert.ToString(dr["Status"]) == "수정")
                    {
                        if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //다른항목 진행할 경우
                            return true;
                        }
                        else
                        {
                            //다른항목 진행하지 않을경우
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion 기준품목 더블클릭 End

        #region 저장

        private void Save()
        {
            DataTable dt = Gc_PartList.DataSource as DataTable;
            if (dt == null) return;

            bool Success = true;

            // 저장데이터 검사
            if (!Check_SaveClick(dt))
            {
                return;
            }

            //데이터 저장
            foreach (DataRow dr in dt.Rows)
            {
                Success = Test(dr);
            }

            //정상 저장 완료시 진행처리
            if (Success)
            {
                MessageBox.Show("저장이 정상적으로 완료되었습니다.");

                int SaveFocusedRowInx = Gv_BaseItem.FocusedRowHandle;

                Search_BaseItem();  //기준품목 재조회

                Gv_BaseItem.FocusedRowHandle = SaveFocusedRowInx;

                Search_PartList(Convert.ToString(dt.Rows[0]["Parent_ItemCode"]), Convert.ToString(dt.Rows[0]["CustCode"]));
            }
        }

        private bool Test(DataRow dr)
        {

            //상태값에 따른 저장 진행
            string status = Convert.ToString(dr["Status"]);
            bool Success = true;

            switch (status)
            {
                case "신규": Success = Save_ToDatabase(dr, 'I'); break;
                case "수정": Success = Save_ToDatabase(dr, 'U'); break;
                case "삭제": Success = Save_ToDatabase(dr, 'D'); break;
                default: Success = true; break;
            }

            return Success;
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //신규, 수정 내역 확인
            if (dt == null) return false;

            int cnt = 0;
            bool isDelete = false;

            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규" || status == "수정")
                {
                    if(Convert.ToString(dr["ItemCode"]) == "" || Convert.ToString(dr["Need_Qty"]) == "")              //소요량
                    {
                        MessageBox.Show("미입력된 내역이 있습니다.");
                        return false;
                    }

                    cnt++;
                }
                else if (status == "삭제")
                {
                    isDelete = true;
                    cnt++;
                }
            }

            //변경사항 없을 경우 RETURN
            if (cnt == 0)
            {
                MessageBox.Show("변경 사항이 없습니다.");
                return false;
            }

            //삭제내역 있을 경우
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
            CM_PartList part = new CM_PartList();

            part.Parent_ItemCode = Convert.ToString(dr["Parent_ItemCode"]);        //상위품목
            part.CustCode = Convert.ToString(dr["CustCode"]);        //상위품목
            part.Me_ItemCode = Convert.ToString(dr["ItemCode"]);            //현재품목
            part.Need_Qty = dr["Need_Qty"].ConvertDecimal_Null_To_Zero();              //소요량

            if (div == 'I')
            {
                if (!part.Ins_PartList()) return false;
            }
            else if (div == 'U')
            {
				if (!part.Upd_PartList()) return false;
			}
            else if (div == 'D')
            {
                if (!part.Del_PartList()) return false;
            }

            dr["Status"] = "";

            return true;
        }

        #endregion 저장 End

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            string ItemCode = Gv_BaseItem.GetFocusedRowCellValue("ItemCode").ConvertString_Null_To_Empty();
            string cust_Cd = Gv_Cust.GetFocusedRowCellValue("CustCode").ConvertString_Null_To_Empty();

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("Parent_ItemCode", ItemCode);
                dic.Add("CustCode", cust_Cd);
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_PartList.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_PartList, dic, txt_Act);
        }

        private void Gv_PartList_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            clsGrid grid = new clsGrid();
            grid.Check_OverLap_CellValue(sender as GridView, e, "ItemName");
        }
    }
}