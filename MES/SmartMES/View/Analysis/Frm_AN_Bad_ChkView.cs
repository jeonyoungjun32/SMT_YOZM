using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.PM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Analysis
{
	public partial class Frm_AN_Bad_ChkView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        string FormType  = ""; // 폼로드명 "" 불량관리, "ByBad_Cd" :유형별 불량 현황

        public Frm_AN_Bad_ChkView()
        {
            InitializeComponent();
        }

        public Frm_AN_Bad_ChkView(string formType)
        {
            InitializeComponent();
            this.FormType = formType;
        }

        private void Frm_QT_Bad_ChkView_Load(object sender, EventArgs e)
        {
            barDate.smtStartDateTimeValue = DateTime.Now;
            barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Year;

            Set_SearchLup();
            Set_Grid(Gv_Bad_Chk);
            Search();
        }

        private void Set_SearchLup()
        {
            CM_Item material = new CM_Item();
            material.ItemType = "20"; // 제품
            DataTable dt_Product = material.Select_LupProduct();

            CM_Process process = new CM_Process();
            DataTable dt_Process = process.SelectCM_ProcessLookup();

            cls_Lookup clsLup = new cls_Lookup();
            clsLup.Create_RepositoryItemSearchLookUp(LupItem, dt_Product, "코드", "품명");
            clsLup.Create_RepositoryItemSearchLookUp(LupProcess, dt_Process, "코드", "공정명");

            LupProcess.smtLookUpValue = "ALL";
            LupItem.smtLookUpValue = "ALL";
        }

        private void Set_Grid(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.IndicatorWidth = 50;                     //순번 너비

            gv.OptionsView.ColumnAutoWidth = false;

            grid.Create_GvColumn(gv, "     ", "PART");
            grid.Create_GvColumn(gv, "합계", "TOTAL");

            for (int i = 1; i <= 12; i++)
            {
                grid.Create_GvColumn(gv, i.ToString("D2") + "월", i.ToString("D2"));
                grid.SetCol_Type(gv.Columns[i.ToString("D2")], clsGrid.TypeList.TFloat);
            }

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            grid.SetCol_Type(gv.Columns["TOTAL"], clsGrid.TypeList.TFloat);
            gv.Columns["TOTAL"].AppearanceCell.BackColor = Color.Yellow;

            //이벤트
            grid.RowNumber_Indicator(gv);     //행번호 표기
        }

        private void Search()
        {
            this.Validate();

            string ProcessCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();
            string date = barDate.smtStartDateTimeValue.ToString("yyyy");

            PM_OrderResult pm = new PM_OrderResult();
            pm.ProcessCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();
            pm.ItemCode = LupItem.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.smtLookUpValue.ConvertString_Null_To_Empty();
            DataTable dt = pm.Select_BadTotalByMonth(date);
            // 불량유형 등록 부분 로직이 없어 추가하지 않음.
            Gc_Bad_Chk.DataSource = dt;
            SetChart(barChart, dt);

            Gv_Bad_Chk.BestFitColumns();
        }

        private void SetChart(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                for (int idx = 0; idx < dt.Rows.Count; idx++)
                {
                    string badNm = dt.Rows[idx]["PART"].ToString();
                    //해당 칼럼은 그래프에 표기X
                    if (badNm != "총생산" && badNm != "총양품" && badNm != "총불량" && badNm != "불량률[PPH]" && badNm != "불량률[PPM]")
                    {
                        // 각 불량 유형별로 시리즈 생성
                        Series series = new Series(badNm, ViewType.Bar);

                        // 해당 시리즈에 데이터 포인트 추가
                        for (int j = 1; j < 13; j++)
                        {
                            string strJ = j.ToString("D2");
                            string strAxis = strJ + "월";
                            int value = dt.Rows[idx][strJ].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[idx][strJ].ToString());
                            series.Points.Add(new SeriesPoint(strAxis, value));
                        }

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series);
                        series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                        // 포인트 라벨 설정
                        series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
                    }
                    else
                    {
                        //불량유형이 없으면 표가 없음
                    }
                }
                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가
                foreach (var series in seriesList)
                {
                    chart.Series.Add(series);
                }

                // X축 및 Y축 설정cuu
                XYDiagram diagram = chart.Diagram as XYDiagram;
                if (diagram == null) return; //총생산,양품수량은 있는데 불량이 하나도 없을 경우 차트를 만들지 않는다.

                //diagram.AxisX.VisualRange.SetMinMaxValues("01일", "10일"); // 스크롤 범위
                diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;


                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "불량 유형별 차트", Font = new Font(Font.FontFamily.Name, 12, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            clsExcelDown cls = new clsExcelDown();
            cls.DownGridControl = this.Gc_Bad_Chk;
            cls.Btn_ExcelDown();
            /*
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_Bad_Chk.ExportToXls(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }*/
        }
    }
}