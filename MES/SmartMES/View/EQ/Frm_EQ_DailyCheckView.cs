using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.EQ;
using SmartMES.Models;
using SmartMES.Models.EQ;
using System;
using System.Data;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_DailyCheckView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        string FormType  = ""; // 폼로드명 "" 불량관리, "ByBad_Cd" :유형별 불량 현황
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //점검항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //점검항목 Repository 숫자입력

        public Frm_EQ_DailyCheckView()
        {
            InitializeComponent();
        }

        public Frm_EQ_DailyCheckView(string formType)
        {
            InitializeComponent();
            this.FormType = formType;
        }

        private void Frm_QT_Bad_ChkView_Load(object sender, EventArgs e)
        {
            barDate.smtStartDateTimeValue = DateTime.Now;

            if (FormType == "Daily")
            {
                barDate.smtStartDateTimeEditingTime = DatetimeType.Month;
            }
            else
            {
                barDate.smtStartDateTimeEditingTime = DatetimeType.Year;
            }

            Set_SearchLup();
            Set_Grid(Gv_Daily_Chk);
            Search();
        }

        private void Set_SearchLup()
        {
            EQ_MachineChkStd Machine = new EQ_MachineChkStd();
            DataTable dtMachine = Machine.Select_Machine_Lup(true);
            DataTable dtManage = Machine.Select_Manage(true);

            cls_Lookup clsLup = new cls_Lookup();
            clsLup.Create_RepositoryItemSearchLookUp(LupMachine, dtMachine, "코드", "명칭");
            LupMachine.smtLookUpValue = Convert.ToString(dtMachine.Rows[0][0]);
            clsLup.Create_RepositoryItemSearchLookUp(LupManage, dtManage, "관리번호", "명칭");
            LupManage.smtLookUpValue = Convert.ToString(dtManage.Rows[0][0]);
        }

        private void Set_Grid(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.IndicatorWidth = 50;                     //순번 너비

            grid.Create_GvColumn(gv, "설비명칭", "Machine_Nm");
            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");

            int colCnt = 0;
            string fieldNm = "";
            string caption = "";

            switch (FormType)
            {
                case "Daily": colCnt = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1).AddDays(-1).Day; break;
                case "Routine": colCnt = 12; break;
            }

            for (int i = 1; i <= colCnt; i++)
            {
                fieldNm = i.ToString("D2");
                caption = fieldNm + (FormType == "Routine" ? "월" : "일");

                grid.Create_GvColumn(gv, caption, fieldNm);
                grid.SetCol_Type(gv.Columns[fieldNm], clsGrid.TypeList.TFloat);
                gv.Columns[fieldNm].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //이벤트
            grid.RowNumber_Indicator(gv);     //행번호 표기
            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기
        }

        private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //점검항목 일정Row의 Repository다르게하기
            GridView gv = sender as GridView;
            if (e.RowHandle < 0) return;

            string fieldName = e.Column.FieldName;
            if (fieldName == "InspectCheck")
            {
                string type = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd"));

                if (type == "10")
                {
                    //양/불 선택일경우
                    e.RepositoryItem = rpoChk;
                }
                else if (type == "20")
                {
                    //측정치 입력일경우
                    e.RepositoryItem = rpoTxt;
                }
            }
        }

        private void Search()
        {
            this.Validate();

            string date = FormType == "Daily" ? barDate.smtStartDateTimeValue.ToString("yyyyMM") : barDate.smtStartDateTimeValue.ToString("yyyy");
            string MachineCode = LupMachine.smtLookUpValue.ConvertString_Null_To_Empty();

            if(MachineCode == "")
            {
                XtraMessageBox.Show("설비 미선택", "조회조건 미선택");
                return;
            }

            EQ_MachineChk machine_Chk = new EQ_MachineChk();
            machine_Chk.InspectDate = date;
            machine_Chk.InspectDivCd = FormType == "Daily" ? "10" : "20";
            machine_Chk.MachineCode = MachineCode;

            Gc_Daily_Chk.DataSource = machine_Chk.Select_Machine_ChkView(date);

            Gv_Daily_Chk.BestFitColumns();
        }

		private void cmdDisplay_Click(object sender, EventArgs e)
		{
            Search();
        }

		private void cmdExcel_Click(object sender, EventArgs e)
		{
            clsFunction func = new clsFunction();
            func.ExportExcel(Gv_Daily_Chk);
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
        }
    }
}