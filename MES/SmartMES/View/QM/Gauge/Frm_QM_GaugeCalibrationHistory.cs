using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static SmartCommon.Common.clsGrid;

namespace SmartMES.View.QM.Gauge
{
	public partial class Frm_QM_GaugeCalibrationHistory : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();

        public Frm_QM_GaugeCalibrationHistory()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddYears(-2);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            //GridView 데이터 입력
            Set_GvGauge(Gv_GaugeCal);
        }
   
        #region GridView Setting

        private void Set_GvGauge(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            RepositoryItemSearchLookUpEdit lup_Result = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "Q020", false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "계측기코드", "GaugeCode");
            grid.Create_GvColumn(gv, "계측기명", "GaugeName");
            grid.Create_GvColumn(gv, "관리번호", "SerialNo");

            grid.Create_GvColumn(gv, "교정일자", "CalibrationDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "차기교정일", "NextDate");

            grid.Create_GvColumn(gv, "교정업체", "CalibrationCompany");
            grid.Create_GvColumn(gv, "교정금액", "Cost");
            grid.Create_GvColumn(gv, "교정결과", "Result", lup_Result);
            grid.Create_GvColumn(gv, "이미지등록", "CalImage", rpo.Create_rpoPic());
            grid.Create_GvColumn(gv, "비고", "Remark", rpo.Create_rpoMemo());

            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");

            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //VISIBLE
            gv.Columns["Remark"].Visible = false; 
            gv.Columns["CalImage"].Visible = false; 

            gv.Columns["CreId"].Visible = false; 
            gv.Columns["CreIP"].Visible = false; 
            gv.Columns["CreDt"].Visible = false; 
            gv.Columns["ModId"].Visible = false; 
            gv.Columns["ModIP"].Visible = false; 
            gv.Columns["ModDt"].Visible = false; 

            //Repository 설정

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["CalibrationDate"], TypeList.TDate);
            grid.SetCol_Type(gv.Columns["NextDate"], TypeList.TDate);
            grid.SetCol_Type(gv.Columns["CalCycleDay"], TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Cost"], TypeList.TFloat);

            //칼럼 너비 설정

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			//gv.RowCellStyle += Gv_RowCellStyle;
        }

		private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
            GridView gv = sender as GridView;

            if (gv == null || e.RowHandle < 0)
                return;

            if (e.Column.FieldName != "CalibrationStatus")
                return;

            string status = gv.GetRowCellValue(e.RowHandle, "CalibrationStatus")
                              .ConvertString_Null_To_Empty();

            switch (status)
            {
                case "정상":
                    // 연한 초록
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 228);
                    e.Appearance.ForeColor = Color.FromArgb(30, 110, 60);
                    break;

                case "교정예정":
                    // 연한 노랑
                    e.Appearance.BackColor = Color.FromArgb(255, 243, 205);
                    e.Appearance.ForeColor = Color.FromArgb(145, 100, 0);
                    break;

                case "기한초과":
                    // 연한 빨강
                    e.Appearance.BackColor = Color.FromArgb(255, 220, 220);
                    e.Appearance.ForeColor = Color.FromArgb(180, 30, 30);
                    break;

                case "미등록":
                    // 연한 회색
                    e.Appearance.BackColor = Color.FromArgb(235, 235, 235);
                    e.Appearance.ForeColor = Color.FromArgb(90, 90, 90);
                    break;
            }

            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

		#endregion GridView Setting End

		#endregion 디자인 아이템 생성 종료

		#region Event

		private void Frm_QM_GaugeCalibration_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void cmd_Click(object sender, EventArgs e)
        {
            smtSearchButton Btn = (smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdSearch":
                    Search();
                    break;

                case "cmdExcel":
                    clsFunction func = new clsFunction();
                    func.ExportExcel(Gv_GaugeCal);
                    break;

                case "cmdClose":
                    this.Close();
                    break;
            }
        }

        #endregion Event End

        #region 조회 기능

        private void Search()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            QM_GaugeCalibration cal = new QM_GaugeCalibration();
            Gc_GaugeCal.DataSource = cal.Select_QM_GaugeCalibration(startDate, endDate);

			Gv_GaugeCal.BestFitColumns();   //Column 너비 조정
        }
 
        #endregion 조회 기능 Endz
    }
}