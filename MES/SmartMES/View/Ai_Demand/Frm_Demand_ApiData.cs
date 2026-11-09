using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using SmartCommon.Common;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using SmartMES.Models.PM;
using System.Data;
using SmartMES.Models.ChatBot;
using SmartMES.Models.DM;
using DevExpress.XtraGrid.Columns;

namespace SmartMES.View.Ai_Demand
{
    public partial class Frm_Demand_ApiData : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();


        public Frm_Demand_ApiData()
        {
            InitializeComponent();
        }

        private void frmPM_ProductPlanView_Load(object sender, EventArgs e)
        {
            dtSearch.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            dtSearch.smtEndDateTimeValue = DateTime.Now;

            SetLup();
            SetGridView();

            DisplayData();
        }

        #region setDesign
        private void SetLup()
        {
            DM_Weather dw = new DM_Weather();

            lupSectionCode.smtLookUpDataSource = dw.Select_LupSectionCode(true);
            lupSectionCode.smtLookUpDataDisplayMember = "지역";
            lupSectionCode.smtLookUpDataValueMember = "행정코드";
            lupSectionCode.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGv_Weather(gvWeather);
		}

        private void SetGv_Weather(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            DM_Weather dw = new DM_Weather();

            RepositoryItemSearchLookUpEdit lupSection = rpo.Create_RepositoryItemSearchLookUp(dw.Select_LupSectionCode(), "행정코드", "지역");

            grid.Create_GvColumn(gv, "일자", "Date");
            grid.Create_GvColumn(gv, "지역", "SectionCode", lupSection);
            grid.Create_GvColumn(gv, "평균온도", "AvgTemp");
            grid.Create_GvColumn(gv, "최고온도", "MaxTemp");
            grid.Create_GvColumn(gv, "최저온도", "MinTemp");
            grid.Create_GvColumn(gv, "강수량", "Prcp");

            grid.SetCol_Type(gv.Columns["AvgTemp"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["MaxTemp"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["MinTemp"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["Prcp"], clsGrid.TypeList.TFloatN1);

            foreach (GridColumn gc in gv.Columns)
            {
                grid.Column_AllowEdit(gc, false);
            }

            gv.CustomColumnDisplayText += Gv_CustomColumnDisplayText;
        }

        private void Gv_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Prcp" && Convert.ToDecimal(e.Value) < 0)
            {
                e.DisplayText = "데이터 없음";
            }
        }

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            baseSMT.smtSearchButton Btn = (baseSMT.smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                
                case nameof(btnSearch):
                    DisplayData();
                    break;

                //미구현
                case nameof(btnOption):
                    break;

                case nameof(btnClose):
                    this.Close();
                    break;
            }
        }

        #region Event

        #endregion Event

        #region 조회
        private void DisplayData()
        {
            DateTime startDate = dtSearch.smtStartDateTimeValue;
            DateTime endDate = dtSearch.smtEndDateTimeValue;
            string sectionCode = lupSectionCode.smtLookUpValue == "ALL" ? "" : lupSectionCode.smtLookUpValue;

            DM_Weather dw = new DM_Weather();
            gcWeather.DataSource = dw.Select_Weather(startDate, endDate, sectionCode);
            gvWeather.BestFitColumns();
        }
        #endregion 조회
    }
}