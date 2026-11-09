using DevExpress.PivotGrid.OLAP;
using DevExpress.Schedule;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.BM;
using SmartPOP.Views.POPUP;
using SmartPOP.Views.Work;
using SmartPOP.Views.Work.Popup;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartPOP.Models.IF;
using SmartPOP.Models.QM;
using SmartPOP.Models.CM;
using SmartPOP.Model.EQ;
using SmartPOP.Models.ST;
using SmartPOP.Models.PM;
using SmartPOP.Models.PM;

namespace SmartMES.View.PM
{
	public partial class Frm_PM_OrderResult_TypeB : DevExpress.XtraEditors.XtraForm
    {
        private string SelectType = "Wo"; //조회상태구분 - Wo : 작업지시, Rst : 생산실적

        clsCode code = new clsCode();
        cls_Grid grid = new cls_Grid();
        CM_Process process = new CM_Process();
        cls_Repository rpo = new cls_Repository();

        string[] ItemDiv = new string[] { "20", "30" };
        string Target = ""; //터치패드 입력 대상

        string StrMachineCode = string.Empty;

        string DefaultProcessCode = "";

        Database db = new Database();
        string query = "";

        public Frm_PM_OrderResult_TypeB()
        {
            InitializeComponent();
        }

        private void frmPM_OrderProcessResult_Load(object sender, EventArgs e)
        {
            SetLup();
            Set_GridView_Wo(Gv_PM_ProductPlan);         // 작업지시
            Set_GridView_Rst(Gv_ProcessResult);    // 생산실적

            //로그인별 담당 설비 있을 경우 Chk true
            EQ_Machine machine = new EQ_Machine();
            machine.ModelNo = WrGlobal.LoginID;

            DataTable dtMachine = machine.Select_Machine_ByID();
            ChkMachine.Checked = (dtMachine != null && dtMachine.Rows.Count > 0);

            Search();
		}

        #region setDesign
        private void SetLup()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            Dt_Date.PopMode = true;

            //Lup_Item.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", materialType }, "전체");
            //Lup_Item.smtLookUpDataDisplayMember = "품명";
            //Lup_Item.smtLookUpDataValueMember = "품목코드";
            //Lup_Item.smtLookUpValue = "ALL";

            DataTable dt = code.GetProcess_List(false);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    DefaultProcessCode = dt.Rows[0]["공정코드"].ConvertString_Null_To_Empty();
                }
            }

            Lup_Process.smtLookUpDataSource = code.GetProcess_List(true);
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpDataValueMember = "공정코드";
            Lup_Process.smtLookUpValue = "ALL";
            Lup_Process.PopMode = true;

            Lup_Machine.smtLookUpDataSource = code.GetMachineList(true);
            Lup_Machine.smtLookUpDataDisplayMember = "설비명";
            Lup_Machine.smtLookUpDataValueMember = "설비코드";
            Lup_Machine.smtLookUpValue = "ALL";
            Lup_Machine.PopMode = true;
        }

        private void ChkMachine_EditValueChanged(object sender, EventArgs e)
        {
			if (ChkMachine.Checked)
			{
                MachineByID();
			}
			else
			{
                StrMachineCode = "";
			}

            Search();
        }

        private void Set_GridView_Wo(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            gv.OptionsSelection.MultiSelect = true;

            //라인
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            //품목
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(ItemDiv), "품목코드", "품명");
            //공정
            RepositoryItemSearchLookUpEdit lup_Process_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //거래처
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");
            //작업자
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false, null, "Y"), "코드", "명칭");

            //작업자 룩업만 'Office 2013' 스킨을 사용하도록 지정(스크롤바 굵게)
            lup_Emp.LookAndFeel.UseDefaultLookAndFeel = false;
            lup_Emp.LookAndFeel.SetSkinStyle("Office 2013");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "작업상태", "WorkStatus");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            grid.Create_GvColumn(gv, "지시일자", "OrderDate");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");

            grid.Create_GvColumn(gv, "품목코드", "MaterialCode");
            grid.Create_GvColumn(gv, "품명", "MaterialName");
            grid.Create_GvColumn(gv, "규격", "Size");

            //grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "거래처", "CustCode", lupCust);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_Process_Cd);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "지시", "OrderQty");

            grid.Create_GvColumn(gv, "총생산", "TotalWorkQty");
            grid.Create_GvColumn(gv, "생산", "WorkQty");
            grid.Create_GvColumn(gv, "양품", "GoodQty");
            grid.Create_GvColumn(gv, "불량", "NgQty");
            grid.Create_GvColumn(gv, "작업일자", "WorkDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "작업자", "WorkerID", lup_Emp);
            grid.Create_GvColumn(gv, "시작시간", "StartDate");
            grid.Create_GvColumn(gv, "종료시간", "EndDate");
            grid.Create_GvColumn(gv, "창고수량", "StockQty");
            grid.Create_GvColumn(gv, "비고", "Rmk");

            grid.Create_GvColumn(gv, "등록구분", "RowType");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq"); //실적등록 확인용
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            grid.Create_GvColumn(gv, "카운터", "Counter");

            //시작시간, 종료시간 칼럼
            RepositoryItemDateEdit dateTimeEdit = new RepositoryItemDateEdit();
            dateTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm";
            dateTimeEdit.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;

            gv.Columns["StartDate"].ColumnEdit = dateTimeEdit;
            gv.Columns["EndDate"].ColumnEdit = dateTimeEdit;
            //gv.Columns["MaterialName"].ColumnEdit = lup_material;

            ////Repository
            //if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            //if (gv.Columns.ColumnByFieldName("MaterialClass1") != null) gv.Columns["MaterialClass1"].ColumnEdit = lup_Class_Cd1;
            //if (gv.Columns.ColumnByFieldName("MaterialClass2") != null) gv.Columns["MaterialClass2"].ColumnEdit = lup_Class_Cd2;
            //if (gv.Columns.ColumnByFieldName("MaterialClass3") != null) gv.Columns["MaterialClass3"].ColumnEdit = lup_Class_Cd3;

            //입력
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            //grid.Column_AllowEdit(gv.Columns["WorkQty"]);
            //grid.Column_AllowEdit(gv.Columns["GoodQty"]);
            //grid.Column_AllowEdit(gv.Columns["NgQty"]);
            grid.Column_AllowEdit(gv.Columns["WorkerID"]);
            //grid.Column_AllowEdit(gv.Columns["WorkDate"]);
            //grid.Column_AllowEdit(gv.Columns["StartDate"]);
            //grid.Column_AllowEdit(gv.Columns["EndDate"]);
            grid.Column_AllowEdit(gv.Columns["Rmk"]);

            gv.Columns["WorkQty"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["GoodQty"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["NgQty"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["WorkDate"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["StartDate"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["EndDate"].AppearanceCell.BackColor = WrGlobal.EditBgColor;


            // 포맷
            grid.SetCol_Type(gv.Columns["OrderDate"], cls_Grid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalWorkQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["StockQty"], cls_Grid.TypeList.TInt);
            gv.Columns["WorkStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //visible
            gv.Columns["Status"].Visible = false;
            gv.Columns["OrderNo"].Visible = false;
            gv.Columns["OrderSeq"].Visible = false;
            gv.Columns["CustCode"].Visible = false;
            gv.Columns["ProcessCode"].Visible = false;

            gv.Columns["SdOrderNo"].Visible = false;
            gv.Columns["SdOrderSeq"].Visible = false;
            gv.Columns["StockQty"].Visible = false;
            gv.Columns["SubSeq"].Visible = false;
            gv.Columns["RowType"].Visible = false;
            gv.Columns["SerialNo"].Visible = false;
            //gv.Columns["Cavity"].Visible = false;
            gv.Columns["Counter"].Visible = false;

            //셀 내부 서치룩업 크기 조정
            grid.SetPOPCellLup(lup_material);
            grid.SetPOPCellLup(lupCust);
            grid.SetPOPCellLup(lup_Process_Cd);
            grid.SetPOPCellLup(lupMachine);
            grid.SetPOPCellLup(lup_Emp);

			//줄바꿈
			gv.OptionsView.RowAutoHeight = true;
            RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
            gv.Columns["MaterialName"].ColumnEdit = memoEdit;
            gv.Columns["MaterialName"].AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gv.Columns["MaterialName"].MaxWidth = 300;
            gv.Columns["MaterialName"].AppearanceCell.Options.UseTextOptions = true;

            //이벤트
			grid.SetPOPGrid(Gv_PM_ProductPlan);
            grid.RowNumber_Indicator(gv);
            gv.CellValueChanged += Gv_CellValueChanged;
            gv.RowCellClick += Gv_RowCellClick;                       //POP 용숫자패드
            gv.ValidatingEditor += Gv_ValidatingEditor;
        }

        private void Set_GridView_Rst(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            // 그리드 헤더 줄 바꿈 <Br>
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            //라인
            //RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            //품목
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(ItemDiv), "품목코드", "품명");
			//공정
			RepositoryItemSearchLookUpEdit lup_Process_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //거래처
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");
            //작업자
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false, null), "코드", "명칭");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            EditorButton btn_Move = new EditorButton();
            btn_Move.Kind = ButtonPredefines.Right;

            RepositoryItemButtonEdit btn = new RepositoryItemButtonEdit();
            btn.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            btn.Buttons.RemoveAt(0);
            btn.Buttons.Add(btn_Move);
            btn.ButtonClick += Btn_Bad_Detail_CliCk; //버튼 이벤트  //유형별불량, 투입재료 팝업 호출

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "실적순번", "SubSeq");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            grid.Create_GvColumn(gv, "실적일자", "WorkDate", rpo.Create_rpoDate());
            
            grid.Create_GvColumn(gv, "품목코드", "MaterialCode");
            grid.Create_GvColumn(gv, "품명", "MaterialName");
            grid.Create_GvColumn(gv, "규격", "Size");

            //grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "거래처", "CustCode", lupCust);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_Process_Cd);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);

            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");

            grid.Create_GvColumn(gv, "작업자", "WorkerID", lup_Emp);
            grid.Create_GvColumn(gv, "시작시간", "StartDate");
            grid.Create_GvColumn(gv, "종료시간", "EndDate");

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "비고", "Rmk");
            grid.Create_GvColumn(gv, "유형별<Br>불량수량", "Bad_Qty_Detail", btn);

            //시작시간, 종료시간 칼럼
            RepositoryItemDateEdit dateTimeEdit = new RepositoryItemDateEdit();
            dateTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm";
            dateTimeEdit.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;

            gv.Columns["StartDate"].ColumnEdit = dateTimeEdit;
            gv.Columns["EndDate"].ColumnEdit = dateTimeEdit;
            gv.Columns["MaterialName"].ColumnEdit = lup_material;

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("MaterialClass1") != null) gv.Columns["MaterialClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("MaterialClass2") != null) gv.Columns["MaterialClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("MaterialClass3") != null) gv.Columns["MaterialClass3"].ColumnEdit = lup_Class_Cd3;


            //셀 내부 서치룩업 크기 조정
            grid.SetPOPCellLup(lup_material);
            grid.SetPOPCellLup(lupCust);
            grid.SetPOPCellLup(lup_Process_Cd);
            grid.SetPOPCellLup(lupMachine);
            grid.SetPOPCellLup(lup_Emp);

            //입력부
            grid.Column_AllowEdit(gv.Columns["MaterialName"]);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            grid.Column_AllowEdit(gv.Columns["Bad_Qty_Detail"]);
            //grid.Column_AllowEdit(gv.Columns["WorkQty"]);
            //grid.Column_AllowEdit(gv.Columns["GoodQty"]);
            //grid.Column_AllowEdit(gv.Columns["NgQty"]);
            grid.Column_AllowEdit(gv.Columns["WorkerID"]);
            //grid.Column_AllowEdit(gv.Columns["WorkDate"]);
			//grid.Column_AllowEdit(gv.Columns["StartDate"]);
			//grid.Column_AllowEdit(gv.Columns["EndDate"]);
			grid.Column_AllowEdit(gv.Columns["Rmk"]);

            gv.Columns["WorkQty"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["GoodQty"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["NgQty"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["WorkDate"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["StartDate"].AppearanceCell.BackColor = WrGlobal.EditBgColor;
            gv.Columns["EndDate"].AppearanceCell.BackColor = WrGlobal.EditBgColor;

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderDate"], cls_Grid.TypeList.TDate);
			grid.SetCol_Type(gv.Columns["WorkDate"], cls_Grid.TypeList.TDate);
			grid.SetCol_Type(gv.Columns["StartDate"], cls_Grid.TypeList.TDateTime);
			grid.SetCol_Type(gv.Columns["EndDate"], cls_Grid.TypeList.TDateTime);

            grid.SetCol_Type(gv.Columns["OrderQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], cls_Grid.TypeList.TInt);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //visible
            gv.Columns["OrderQty"].Visible = false;
            gv.Columns["CustCode"].Visible = false;
            gv.Columns["OrderNo"].Visible = false;
            gv.Columns["OrderSeq"].Visible = false;
            gv.Columns["SubSeq"].Visible = false;
            gv.Columns["ProcessCode"].Visible = false;

            //줄바꿈
            gv.OptionsView.RowAutoHeight = true;
            gv.Columns["MaterialName"].AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gv.Columns["MaterialName"].MaxWidth = 300;
            gv.Columns["MaterialName"].AppearanceCell.Options.UseTextOptions = true;

            //이벤트
            grid.SetPOPGrid(gv);

            grid.RowNumber_Indicator(gv);
            gv.CellValueChanged += Gv_CellValueChanged;
            gv.RowCellClick += Gv_RowCellClick;                       //POP 용숫자패드
            gv.ValidatingEditor += Gv_ValidatingEditor;

        }

        private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            DataRow dr = gv.GetFocusedDataRow();
            string ColName = gv.FocusedColumn.FieldName;

            switch (ColName)
            {
                case "Materialname":

                    if (dr["SerialNo"].ConvertString_Null_To_Empty() != "")
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = "등록된 실적 건의 품목을 변경할 수 없습니다.";
                    }
                    break;
            }
        }

        private void Btn_Bad_Detail_CliCk(object sender, ButtonPressedEventArgs e)
        {
            GridView gv = Gc_ProcessResult.MainView as GridView;
            if (gv == null) return;
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            if (SelectType.ToUpper() == "WO")
            {
                MessageBox.Show("지시조회 상태에서는 불량을 등록할 수 없습니다."); return;
            }
            else
            {
                if (dr["WorkQty"] == null || Convert.ToString(dr["WorkQty"]) == "")
                {
                    MessageBox.Show("생산수량이 없습니다."); return;
                }

                string colName = gv.FocusedColumn.FieldName;

                //유형별 불량수량
                if (colName == "Bad_Qty_Detail")
                {
                    Frm_BadQty_Popup frm = new Frm_BadQty_Popup(dr);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();

                    Search();
                }
            }
        }

        private void Gv_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
		{
			//시, 분 터치패드 입력
			GridView gv = sender as GridView;
			if (e.RowHandle < 0) return;

            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            string fieldName = e.Column.FieldName;

			if (fieldName == "WorkQty" || fieldName == "GoodQty" || fieldName == "NgQty")
			{
			    string BtnTag = "Qty";
				Frm_NumPad NumPad = new Frm_NumPad(BtnTag, 0);
				//터치패드로 입력한 값을 넣을 컨트롤명
				Target = fieldName;

				// 방법1: 화면의 오른쪽 중앙 고정
				int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
				int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
				int x = (screenWidth - NumPad.Width) / 4;
				int y = (screenHeight - NumPad.Height) / 2;

				NumPad.StartPosition = FormStartPosition.Manual;
				NumPad.Location = new Point(x, y);

				NumPad.FormSendEvent += new Frm_NumPad.FormSendDataHandler(AddNum_cell);
				NumPad.ShowDialog();
			}
			else if(fieldName == "WorkDate" || fieldName == "StartDate" || fieldName == "EndDate")
			{
                //팝업 키패드에 띄우는 날짜
                string currentValue = drChanged[fieldName].ToString();
                string date = "";
                if (drChanged[fieldName].ToString() == "")
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    date = Convert.ToDateTime(drChanged[fieldName]).ToString("yyyy-MM-dd HH:mm");
                }
                //날짜+시간 입력패드
                string title = fieldName == "StartDate" ? "시작 시간"
                    : fieldName == "EndDate" ? "종료 시간" : "작업 일자";
                Frm_DateTime_POP frm = new Frm_DateTime_POP(date, title);
                frm.ShowDialog();

                Tuple<string, string> result = frm.resultDateTime();
                string newDate = result.Item1;  // 팝업에서 선택한 새 값

                //item1: 일자,시간 item2:시간
                // 값이 실제로 변경되었는지 확인
                if (newDate != currentValue)
                {
                    // 값이 변경된 경우에만 셀 값을 업데이트
                    if (newDate.Trim() == "") return;
                    gv.SetFocusedRowCellValue(fieldName, newDate);
                }
            }
		}

        //숫자패드
        private void AddNum_cell(string _num)
        {
            //수량/값 터치패드 입력
            try
            {

                GridView gv = null;

                if (tabbedControlGroup1.SelectedTabPageIndex == 0) gv = Gv_PM_ProductPlan;
                else gv = Gv_ProcessResult;

                int SaveFocusedRowInx = gv.FocusedRowHandle;

                string searchString = "Qty";
                bool isQty = Target.Contains(searchString);

                if (isQty)
                {
                    gv.SetRowCellValue(SaveFocusedRowInx, Target, _num);
                }
                else
                {
                    string numTime = _num.PadLeft(4, '0');
                    gv.SetRowCellValue(SaveFocusedRowInx, Target, numTime);
                }

            }
            catch { }
        }

        private void MachineByID()
        {
            EQ_Machine machine = new EQ_Machine();
            machine.ModelNo = WrGlobal.LoginID;

            DataTable dtMachine = machine.Select_Machine_ByID();

            if (dtMachine == null)
			{
                StrMachineCode = "";
                return ;
			}

            string[] MachineDiv = dtMachine.AsEnumerable().Select(row => row["MachineCode"].ToString()).ToArray();

            if (MachineDiv.Length > 0)
            {
                StrMachineCode = $"'{string.Join("','", MachineDiv)}'";
            }
        }

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            SimpleButton Btn = sender as SimpleButton;
            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdSearch":
                    //조회
                    Search();
                    break;
                case "cmdStart":
                    //작업시작
                    Start();
                    break;

                case "cmdEnd":
                    //작업종료
                    End();
                    break;

                case "cmdSave":
                    //저장
                    Save();
                    break;

                case "cmdExcel":
                    //엑셀다운
                    cmdExcel_Click();
                    break;

                case "BtnDefect":
                    OpenDefect();
                    break;

                case "cmdReset":
                    //생산수량 초기화
                    count_Reset();
                    break;
            }
        }

        #region 조회

        private void Search()
        {
            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                Search_PM_ProductPlan();     // 지시리스트
                SelectType = "Wo";
                cmdIns.Enabled = false;
                cmdStart.Enabled = true;
                cmdEnd.Enabled = true;
                BtnDefect.Enabled = false;
            }
            else
            {
                Search_Rst();           // 실적리스트
                SelectType = "Rst";
                cmdIns.Enabled = true;
                cmdStart.Enabled = false;
                cmdEnd.Enabled = false;
                BtnDefect.Enabled = true;
            }
        }

        private void Search_PM_ProductPlan()
        {
            PM_OrderResult rst = new PM_OrderResult();

            rst.StartDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            rst.EndDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            rst.ProcessCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;
            //rst.MachineCode = Lup_Machine.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Machine.smtLookUpValue;
            //rst.MaterialCode = Lup_Item.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Item.smtLookUpValue;

            Gc_PM_ProductPlan.DataSource = rst.Select_PM_OrderProcess(StrMachineCode);
            Gv_PM_ProductPlan.BestFitColumns();
        }

        private void Search_Rst()
        {
            PM_OrderResult rst = new PM_OrderResult();

            rst.StartDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            rst.EndDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            rst.ProcessCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;
            rst.MachineCode = Lup_Machine.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Machine.smtLookUpValue;
            //rst.MaterialCode = Lup_Item.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Item.smtLookUpValue;

            Gc_ProcessResult.DataSource = rst.Select_PM_OrderResult(StrMachineCode);
            Gv_ProcessResult.BestFitColumns();
        }

        #endregion 조회

        #region 저장

        private void Save()
        {
            //작성내역 저장
            DataTable dt;

            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
                dt = Gc_PM_ProductPlan.DataSource as DataTable;
            else
                dt = Gc_ProcessResult.DataSource as DataTable;

            bool isPM_ProductPlanTab = (tabbedControlGroup1.SelectedTabPageIndex == 0);
            bool Success = true;

            if (!Check_SaveClick(dt)) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                // 이미 저장된 행이면 건너뛰기
                string status = Convert.ToString(dr["Status"]);
                if (string.IsNullOrEmpty(status)) continue;

                // PM_ProductPlan일 경우 RowType으로 cmd 결정
                string rowStatus = (isPM_ProductPlanTab && dt.Columns.Contains("RowType"))
                                    ? Convert.ToString(dr["RowType"]) : "";

                char cmd = ' ';

                if (isPM_ProductPlanTab)
                {
                    if (rowStatus == "Ins")
                        cmd = 'I';
                    else if (rowStatus == "Upd")
                        cmd = 'U';
                    else
                        cmd = (SelectType == "Wo" && Convert.ToString(dr["SubSeq"]) == "") ? 'I' : 'U';
                }
                else // ProcessResult
                {
                    switch (status)
                    {
                        case "신규":
                            cmd = 'I';
                            break;
                        case "수정":
                            cmd = (SelectType == "Wo" && Convert.ToString(dr["SubSeq"]) == "") ? 'I' : 'U';
                            break;
                        case "삭제":
                            cmd = 'D';
                            break;
                    }
                }

                // DB 저장 실행
                if (cmd == 'I' || cmd == 'U' || cmd == 'D')
                {
                    Success = Save_ToDatabase(dr, cmd);
                    if (!Success) break;

                    // 저장 후 상태 초기화
                    dr["Status"] = "";
                }
            }



            if (Success)
            {
                cls_MessageBox.Show("저장이 정상적으로 완료되었습니다.");

                Search();
            }
            else
            {
                cls_MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //데이터 불러온 상태인지 검사
            if (dt == null) return false;

            string status = "";
            bool isDelete = false;

            foreach (DataRow dr in dt.Rows)
            {
                status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규" || status == "수정")
                {
                      if (Convert.ToString(dr["ProcessCode"]) == "") { cls_MessageBox.Show("공정을 입력해주세요"); return false; }
                      if (Convert.ToString(dr["MaterialName"]) == "") { cls_MessageBox.Show("품명을 입력해주세요"); return false; }
                      if (Convert.ToString(dr["WorkQty"]) == "") { cls_MessageBox.Show("생산수량을 입력해주세요"); return false; }
                      if (Convert.ToString(dr["GoodQty"]) == "") { cls_MessageBox.Show("양품수량을 입력해주세요"); return false; }
                      if (Convert.ToString(dr["WorkDate"]) == "") { cls_MessageBox.Show("작업일자를 입력해주세요"); return false; }
                      if (Convert.ToString(dr["StartDate"]) == "") { cls_MessageBox.Show("시작시간을 입력해주세요"); return false; }
                }
                else if (status == "삭제")
                {
                    isDelete = true;
                }
            }

            if (isDelete)
            {
                DialogResult dialogResult = cls_MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스 반영
            try
            {
                //1. 기본 셋팅
                PM_OrderResult rst = new PM_OrderResult();
                rst.CorpId = WrGlobal.CorpID;
                rst.OrderNo = Convert.ToString(dr["OrderNo"]);                   
                rst.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();         
                rst.SubSeq = dr["SubSeq"].ConvertString_Null_To_Empty() == "" ? rst.GetNextSeq() : dr["SubSeq"].ConvertInt_Null_To_Zero(); // 작업 순번
                rst.ProcessCode = Convert.ToString(dr["ProcessCode"]);           // 공정
                rst.MachineCode = Convert.ToString(dr["MachineCode"]);           // 설비
                rst.MaterialCode = Convert.ToString(dr["MaterialCode"]);         // 품목
                rst.WorkerID = dr["WorkerID"].ConvertString_Null_To_Empty();     // 작업자
                rst.SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty();                                         // 시리얼번호
                rst.WorkDate = dr["WorkDate"].ConvertDate_To_Char10();            // 작업일자
                //시작시간
                if (dr["StartDate"].ToString() == null || dr["StartDate"].ToString() == "") { rst.StartDate = Convert.ToString(dr["StartDate"]); }
                else { rst.StartDate = (Convert.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd HH:mm")); }
                //종료시간
                if (dr["EndDate"].ToString() == null || dr["EndDate"].ToString() == "") { rst.EndDate = Convert.ToString(dr["EndDate"]); }
                else { rst.EndDate = (Convert.ToDateTime(dr["EndDate"]).ToString("yyyy-MM-dd HH:mm")); }

                rst.WorkQty = dr["WorkQty"].ConvertInt_Null_To_Zero();           // 생산수량
                rst.GoodQty = dr["GoodQty"].ConvertInt_Null_To_Zero();           // 양품수량
                rst.NgQty = dr["NgQty"].ConvertInt_Null_To_Zero();               // 불량수량
                rst.MaterialCode = dr["MaterialCode"].ConvertString_Null_To_Empty();
                rst.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();

                //2. DB 적용
                if (div == 'I')
                {
                    rst.SerialNo = "SN" + code.GetAutoNumber("바코드", DateTime.Today);

                    // [POP_UpdateWorkingStatus_NB] 프로시저
                    // 1. 생산실적 등록
                    if (!rst.Ins_PM_OrderResult())
                    {
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    // 1. 생산실적 수정
                    if (!rst.Upd_PM_OrderResult())
                    {
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //수불삭제
                    ST_StockD std = new ST_StockD();
                    std.JobNo = rst.OrderNo;
                    std.JobSeq = rst.OrderSeq;
                    std.JobSeq2 = rst.SubSeq;

                    std.DeleteStockMD_Work();

                    //자재출고내역 삭제
                    BM_SalesD sd = new BM_SalesD();
                    sd.DeleteSalesMD_Work(rst.SerialNo);

                    //불량 삭제
                    QM_WorkDefect defect = new QM_WorkDefect();
                    defect.SerialNo = rst.SerialNo;
                    defect.DelQM_WorkDefect_BySerialNo();

                    // 1. 생산실적 삭제
                    if (!rst.Del_PM_OrderResult())   
                    { 
                        return false; 
                    }
                    // 2. 공정실적 삭제
                    if (!rst.Del_OrderProgress())
                    {
                        return false;
                    }
                    // 3. 공정지시 리셋
                    if (!rst.Reset_PM_OrderProcess())
                    {
                        return false;
                    }
                    // 4. 작업지시 리셋
                    if (!rst.Reset_PM_ProductPlan())
                    {
                        return false;
                    }
                }

                //신규 or 수정
                if (div == 'I' || div == 'U')
                {
                    // 공정지시 업데이트
                    if (!rst.Upd_PM_OrderProcess())
                    {
                        return false;
                    }
                    // 작업지시 업데이트(사내공정 마지막 라우팅 순번)
                    if (!rst.Upd_PM_ProductPlan())
                    {
                        return false;
                    }

                    // 수불등록


                    //기존 재고 등록 내역 삭제
                    StockMaster stm = new StockMaster();
                    ST_StockD std = new ST_StockD();
                    std.JobNo = rst.OrderNo;
                    std.JobSeq = rst.OrderSeq;
                    std.JobSeq2 = rst.SubSeq;

                    std.DeleteStockMD_Work();

                    //생산재고 입고창고, 자재채고 출고창고 구하기 - 기존에 라인에서 입고, 출고창고 구하기에 그대로 둠
                    query = $@"
                    SELECT TOP 1 A.InHouseID, A.OutHouseID
                    FROM CM_Line A
                    LEFT JOIN CM_Process B ON a.CorpId = B.CorpId AND a.ProcessCode = B.ProcessCode
                    WHERE A.CorpId = '{WrGlobal.CorpID}'
                    AND A.ProcessCode = '{rst.ProcessCode}'
                    ";

                    DataRow drLine = db.GetDataRow(query);

                    string fromHouse = "S0001";  //하위자재 출고 창고
                    string toHouse = "S0002";    //생산 입고 창고

                    if (drLine != null)
					{
                       fromHouse = drLine["InHouseID"].ConvertString_Null_To_Empty();
                       toHouse = drLine["OutHouseID"].ConvertString_Null_To_Empty();
					}

                    //자재수불 + 자재출고 처리
                    if (WrGlobal.ProcOption["PD"]["ProcResult_MatOut_AutoYN"] == "Y")
                    {
                        //기존 자재출고 데이터 삭제
                        BM_SalesD sd = new BM_SalesD();
                        sd.DeleteSalesMD_Work(rst.SerialNo);

                        if (rst.OrderNo != "")   //지시있을 경우 SdRoute 기준으로 BOM 조회 
                        {
                            query = $@"
			                    select 
			                    bom.BomMaterialcode,
			                    bom.ProcessCode,
			                    (bom.WorkQty/bom.CalcQty) * {rst.WorkQty} as NeedQty -- (BOM수량/기준수)* 총생산량
			                    from SdRoute sd
			                    Left join SdRoute bom on sd.CorpId = bom.CorpId and sd.OrderNo = bom.OrderNo and sd.ChildID = bom.ParentID 
			                    INNER join CM_Item cmM on cmM.MaterialCode = bom.BomMaterialCode
			                    where sd.CorpId = '{WrGlobal.CorpID}' and sd.OrderNo = '{rst.OrderNo}' and  sd.BomMaterialCode = '{rst.MaterialCode}'
		                        and bom.BomMaterialcode is not null;";
                        }
                        else //지시없을 경우 Bom 테이블 기준으로 조회
                        {
                            //하위에 자재만 있을 경우에 사용 가능, 추후 반제품 생길때는 쿼리 수정 필요 + 자재별 출고창고 다를경우도 수정 필요
                            query = $@"
                            SELECT 
                                BOM.MaterialCode AS BomMaterialcode, ChildID, ParentID 
                                , bom.ProcessCode
                                , (bom.Qty/ ISNULL(bom.CalcQty, 1) ) * {rst.WorkQty} as NeedQty -- (BOM수량/기준수)* 총생산량
                            FROM [FN_Bom] ('{WrGlobal.CorpID}','{rst.MaterialCode}') AS BOM
                            LEFT JOIN CM_Item A ON BOM.CorpId = A.CorpId AND BOM.MaterialCode = A.MaterialCode
                            WHERE ParentID = (SELECT ChildID FROM CmBOm WHERE CorpId = BOM.CorpId AND MaterialCode = '{rst.MaterialCode}')
                            ";
                        }
                        DataTable dtBom = db.GetDataTable(query);

                        if (dtBom != null)
                        {
                            if (dtBom.Rows.Count > 0)
                            {
                                BM_SalesM sm = new BM_SalesM();
                                sm.CorpId = WrGlobal.CorpID;
                                sm.SalesNo = "OG" + code.GetAutoNumber("출고", Convert.ToDateTime(dr["WorkDate"]));
                                sm.SalesDate = rst.WorkDate;
                                sm.OutType = "30"; //30:자재출고
                                sm.WriteDate = rst.WorkDate;
                                sm.InsertBM_SalesM();

                                int seq = 1;

                                foreach (DataRow item in dtBom.Rows)
                                {
                                    stm = new StockMaster();
                                    stm.InOutNo = "ST" + code.GetAutoNumber("재고", Convert.ToDateTime(dr["WorkDate"]));
                                    stm.OutDate = rst.WorkDate;
                                    stm.FromHouseCode = fromHouse;
                                    stm.WriteID = WrGlobal.LoginID;
                                    stm.Ins_StockMaster();

                                    std = new ST_StockD();
                                    std.InOutNo = stm.InOutNo;
                                    std.Seq = 1;
                                    std.InOutDate = rst.WorkDate;
                                    std.FromHouseCode = fromHouse;
                                    std.Type = "O";
                                    std.MaterialCode = item["BomMaterialCode"].ConvertString_Null_To_Empty();
                                    std.JobNo = rst.OrderNo;
                                    std.JobSeq = rst.OrderSeq;
                                    std.JobSeq2 = rst.SubSeq;
                                    std.Qty = item["NeedQty"].ConvertDecimal_Null_To_Zero();
                                    std.LotNo = rst.SerialNo;   //추후 선입선출시에는 LotNo에 자재입고 LotNo를 넣어야할듯?
                                    std.Status = "완료";
                                    std.Ins_ST_StockD();

                                    sd = new BM_SalesD();
                                    sd.CorpId = WrGlobal.CorpID;
                                    sd.SalesNo = sm.SalesNo;
                                    sd.SalesSeq = seq++;
                                    sd.ProductCode = item["BomMaterialCode"].ConvertString_Null_To_Empty();
                                    sd.Qty = item["NeedQty"].ConvertFloat_Null_To_Zero();
                                    sd.ReqType = "자재출고";
                                    sd.Status = "출고완료";
                                    sd.HouseCode = fromHouse;
                                    sd.BarCodeNo = rst.SerialNo;
                                    sd.InsertBM_SalesD();
                                }
                            }
                        }
                    }

                    //생산재고 입고처리
                    stm = new StockMaster();
                    stm.InOutNo = "ST" + code.GetAutoNumber("재고", Convert.ToDateTime(dr["WorkDate"]));
                    stm.OutDate = rst.WorkDate;
                    stm.ToHouseCode = toHouse;
                    stm.WriteID = WrGlobal.LoginID;
                    stm.Ins_StockMaster();

                    std = new ST_StockD();
                    std.InOutNo = stm.InOutNo;
                    std.Seq = 1;
                    std.InOutDate = rst.WorkDate;
                    std.ToHouseCode = toHouse;
                    std.Type = "생산";
                    std.MaterialCode = rst.MaterialCode;
                    std.JobNo = rst.OrderNo;
                    std.JobSeq = rst.OrderSeq;
                    std.JobSeq2 = rst.SubSeq;
                    std.Qty = rst.GoodQty;
                    std.LotNo = rst.SerialNo;
                    std.Status = "완료";
                    std.Ins_ST_StockD();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 

        #endregion

        #region Event
        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
            string colName = e.Column.FieldName;
            string value = e.Value.ConvertString_Null_To_Empty();

            //0. 수정사항 있을시 상태값 수정 표기
            if (colName == gv.Columns["Status"].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            float badQty = gv.GetFocusedRowCellValue("NgQty").ConvertFloat_Null_To_Zero();
            float goodQty = gv.GetFocusedRowCellValue("WorkQty").ConvertFloat_Null_To_Zero() - badQty;
            float workQty = gv.GetFocusedRowCellValue("WorkQty").ConvertFloat_Null_To_Zero();

            if (colName == "WorkQty")
			{
                drChanged["GoodQty"] = drChanged["WorkQty"].ConvertInt_Null_To_Zero() - badQty;
            }
            else if(colName == "GoodQty")
			{
                drChanged["WorkQty"] = drChanged["GoodQty"].ConvertInt_Null_To_Zero() + badQty;
            }
            else if (colName == "NgQty")
            {
                gv.SetFocusedRowCellValue("GoodQty", goodQty);
            }
            else if(colName == "MaterialName")
			{
                gv.SetFocusedRowCellValue("MaterialCode", value);
			}

            gv.BestFitColumns();
        }



        #endregion Event

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1.이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Tag.ConvertString_Null_To_Empty();

            GridView gv = Gv_ProcessResult;

            //비활성화
            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                cls_MessageBox.Show("작업지시 조회상태에서 행조작 작업을 할 수 없습니다.");
                return;
            }

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("WorkDate", DateTime.Now.Date);
                dic.Add("ProcessCode", DefaultProcessCode); //공정 디폴트값 올덴은 PR002, 공정구분 20
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString((gv as GridView).GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_ProcessResult, dic, txt_Act);
        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            Search();
        }

		private void BtnMachineOP_Click(object sender, EventArgs e)
		{
            DataRow dr = null;

            if(tabbedControlGroup1.SelectedTabPageIndex == 0)   //지시 Grid
			{
                dr = Gv_PM_ProductPlan.GetFocusedDataRow();
            }
			else if (tabbedControlGroup1.SelectedTabPageIndex == 1)//실적 Grid
			{
                dr = Gv_ProcessResult.GetFocusedDataRow();
            }

            if (dr == null) return;

			string orderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
			int orderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			int subSeq = dr["SubSeq"].ConvertInt_Null_To_Zero();
			string machineCode = dr["MachineCode"].ConvertString_Null_To_Empty();

            Frm_EQ_Operation_Btn frm = new Frm_EQ_Operation_Btn(orderNo, orderSeq, subSeq, machineCode);
			frm.StartPosition = FormStartPosition.CenterParent;
			frm.ShowDialog();
		}

        private void Start()
        {
            GridView gv = Gv_PM_ProductPlan;

            int[] selectIdx = gv.GetSelectedRows();
            if (selectIdx.Length < 1) return;

            DateTime date = DateTime.Now;

			for (int i = 0; i < selectIdx.Length; i++)
			{
                DataRow dr = gv.GetDataRow(selectIdx[i]);

                string status = Convert.ToString(dr["WorkStatus"]);

                //if (status == "작업중")
                //{
                //    cls_MessageBox.Show("이미 작업중인 공정입니다.");
                //    return;
                //}

                PM_OrderProcess op = new PM_OrderProcess();
                op.OrderNo = Convert.ToString(dr["OrderNo"]);
                op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
                op.Status = "작업중";
                op.Upd_Status();

                if (dr["StartDate"].ConvertString_Null_To_Empty() == "")
				{
                    dr["StartDate"] =  date.ToString("yyyy-MM-dd HH:mm:ss"); //지시 Grid 시작시간 넣어주기
				}
                dr["WorkStatus"] =  "작업중"; //재조회말고 상태값 변경
            }
            gv.BestFitColumns();

            cls_MessageBox.Show("작업시작 되었습니다.");

            /* 한개 행 선택하여 시작
             * GridView gv = Gv_PM_ProductPlan;

              DataRow dr = gv.GetFocusedDataRow();
              if (dr == null) return;

              string status = Convert.ToString(dr["WorkStatus"]);

              if (status == "작업중")
              {
                  cls_MessageBox.Show("이미 작업중인 공정입니다.");
                  return;
              }

              PM_OrderProcess op = new PM_OrderProcess();
              op.OrderNo = Convert.ToString(dr["OrderNo"]);
              op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
              op.Status = "작업중";
              op.Upd_Status();

              cls_MessageBox.Show("작업시작 되었습니다.");
            */
        }

        private void End()
        {
            GridView gv = Gv_PM_ProductPlan;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            string status = Convert.ToString(dr["WorkStatus"]);

            if (status == "작업종료")
            {
                cls_MessageBox.Show("이미 작업종료된 공정입니다.");
                return;
            }

            PM_OrderProcess op = new PM_OrderProcess();
            op.OrderNo = Convert.ToString(dr["OrderNo"]);
            op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
            op.Status = "작업종료";

            op.Upd_Status();

            cls_MessageBox.Show("작업종료 되었습니다.");
            Search();
        }

        private void OpenDefect()
        {
            GridView gv = null;

            //if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            //    gv = Gv_PM_ProductPlan;
            //else
            gv = Gv_ProcessResult;

            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                cls_MessageBox.Show("작업지시 조회상태에서 등록할 수 없습니다.");
                return;
            }

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            if (dr["WorkQty"] == DBNull.Value || Convert.ToDecimal(dr["WorkQty"]) == 0)
            {
                cls_MessageBox.Show("생산수량이 없습니다.");
                return;
            }

            Frm_BadQty_Popup frm = new Frm_BadQty_Popup(dr);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            Search();
        }

        private void tmCount_Tick(object sender, EventArgs e)
        {
            //DataTable dt = Gc_PM_ProductPlan.DataSource as DataTable;

            //if (dt == null) return;

            //foreach (DataRow dr in dt.Rows)
            //{
            //    string ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
            //    string workStatus = dr["WorkStatus"].ConvertString_Null_To_Empty();
            //    string machineCode = dr["MachineCode"].ConvertString_Null_To_Empty();

            //    if (workStatus != "작업중") continue;

            //    IF_DgpCode mc = new IF_DgpCode();
            //    mc.Equip_Name = machineCode;

            //    DataRow dataRow = mc.Select_DGPCounter(machineCode);

            //    if (dataRow == null) continue;

            //    CM_Process mp = new CM_Process();
            //    mp.ProcessCode = ProcessCode;

            //    DataRow CavityRow = mp.Select_Process_Cavity(ProcessCode);

            //    int cavity = CavityRow["Remark"].ConvertInt_Null_To_Zero();    // 캐비티 수
            //    int counter = dataRow["Counter"].ConvertInt_Null_To_Zero();    // 카운터 값

            //    int workQty = counter * cavity.ConvertInt_Null_To_Zero();
            //    int badQty = dr["NgQty"].ConvertInt_Null_To_Zero();
            //    int goodQty = workQty - badQty;

            //    dr["WorkQty"] = workQty;
            //    dr["GoodQty"] = goodQty;
            //    dr["Cavity"] = cavity;
            //    dr["Counter"] = counter;
            //}
            ////Gv_PM_ProductPlan.BestFitColumns();
        }

        private void AutoQty_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
                this.tmCount.Tick += new System.EventHandler(this.tmCount_Tick);
            else
                this.tmCount.Tick -= new System.EventHandler(this.tmCount_Tick);
        }

		private void count_Reset()
        {
            DataRow dr = Gv_PM_ProductPlan.GetFocusedDataRow();

            if (dr == null) return;

            string dgpCode = dr["DgpCode"].ConvertString_Null_To_Empty();

            IF_DgpStatus link = new IF_DgpStatus();
            link.DgpCode = dgpCode;
            link.CounterReset = "ON";

            link.UpdateCounterReset();
        }
        private void cmdExcel_Click()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_ProcessResult.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
