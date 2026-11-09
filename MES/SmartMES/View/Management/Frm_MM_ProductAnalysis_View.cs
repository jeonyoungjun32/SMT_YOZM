using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraCharts;
using System.Diagnostics;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Model.Management;
using SmartCommon.Common;

namespace SmartMES.View.Management
{
    public partial class Frm_MM_ProductAnalysis_View : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();
        string[] ItemDiv = new string[] {"20", "30" };
        clsRepository rpo = new clsRepository();
        string ItemType = "10,20,30";

        public Frm_MM_ProductAnalysis_View()
        {
            InitializeComponent();
        }

        private void Frm_MM_ProductAnalysis_View_Load(object sender, EventArgs e)
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now;

            Set_SearchLup();
            Set_Grid(Gv_Product);

            Search();
        }

        private void Set_SearchLup() //Lup 작업
        {
            Lup_Item.smtLookUpDataSource = code.GetMaterialList(ItemDiv, true).ToTable();    //품목
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";

            Lup_Process.smtLookUpDataSource = code.GetProcessList(true).ToTable();    //공정
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpDataValueMember = "공정코드";
            Lup_Process.smtLookUpValue = "ALL";

            //품목분류 1
            code.Class = "A01210";
            LupItemClass1.smtLookUpDataSource = code.GetCM_CateItemALL(WrGlobal.CorpID, "A01210", new string[] { ItemType }, true);
            LupItemClass1.smtLookUpDataDisplayMember = "명칭";
            LupItemClass1.smtLookUpDataValueMember = "코드";
            LupItemClass1.smtLookUpValue = "ALL";

            //품목분류 2
            code.Class = "A01220";
            LupItemClass2.smtLookUpDataSource = code.GetCM_CateItemALL(WrGlobal.CorpID, "A01220", new string[] { ItemType }, true);
            LupItemClass2.smtLookUpDataDisplayMember = "명칭";
            LupItemClass2.smtLookUpDataValueMember = "코드";
            LupItemClass2.smtLookUpValue = "ALL";
        }

        private void Set_Grid(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.OptionsView.ColumnAutoWidth = false;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.IndicatorWidth = 50;                     //순번 너비

            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "라인", "LineName");
			grid.Create_GvColumn(gv, "공정", "ProcessName");
			grid.Create_GvColumn(gv, "품목분류1", "ItemClass1", lup_Class_Cd1);
			grid.Create_GvColumn(gv, "품목분류2", "ItemClass2", lup_Class_Cd2);
            grid.Create_GvColumn(gv, "품명", "ItemName");
			grid.Create_GvColumn(gv, "양품수량 합계", "Good_Qty_SUM");

            for (int i = 1; i <= 31; i++)
            {
                grid.Create_GvColumn(gv, i.ToString() + "일", i.ToString("D2"));
                grid.SetCol_Type(gv.Columns[i.ToString("D2")], clsGrid.TypeList.TInt);
            }

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //컬럼타입
            gv.Columns["Good_Qty_SUM"].AppearanceCell.BackColor = Color.Yellow;
            grid.SetCol_Type(gv.Columns["TOTAL"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Good_Qty_SUM"], clsGrid.TypeList.TInt);

            //밴드 고정
            gv.Columns["LineName"].Fixed = FixedStyle.Left;
            gv.Columns["ProcessName"].Fixed = FixedStyle.Left;
            if (gv.Columns.ColumnByFieldName("Alias") != null) { gv.Columns["Alias"].Fixed = FixedStyle.Left; }
            gv.Columns["ItemClass1"].Fixed = FixedStyle.Left;
            gv.Columns["ItemClass2"].Fixed = FixedStyle.Left;
            gv.Columns["ItemName"].Fixed = FixedStyle.Left;
            gv.Columns["Good_Qty_SUM"].Fixed = FixedStyle.Left;

            //이벤트
            grid.RowNumber_Indicator(gv);     //행번호 표기
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string date = Dt_Date.smtStartDateTimeValue.ToString("yyyyMM");
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-01");
            string endDate = Convert.ToDateTime(startDate).AddMonths(1).AddDays(-1).ToString("d");

            MM_ProductAnalysis bad_Chk = new MM_ProductAnalysis();
            bad_Chk.ProcessCode = Lup_Process.smtLookUpValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue.ConvertString_Null_To_Empty();
            bad_Chk.ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            bad_Chk.MachineCode = "";
            bad_Chk.ItemClass1 = LupItemClass1.smtLookUpValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : LupItemClass1.smtLookUpValue.ConvertString_Null_To_Empty();
            bad_Chk.ItemClass2 = LupItemClass2.smtLookUpValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : LupItemClass2.smtLookUpValue.ConvertString_Null_To_Empty();

            //그리드
            DataTable dt = bad_Chk.Product_View(startDate, endDate);
            Gc_Product.DataSource = dt;

			//차트
			DataTable dt2 = bad_Chk.Product_View_Chart(date);
			SetChart(barChart, dt2);

			Gv_Product.BestFitColumns();
        }

        private void SetChart(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }
                
                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();


                // 각 불량 유형별로 시리즈 생성
                Series series = new Series("양품수량", ViewType.Bar);

                // 해당 시리즈에 데이터 포인트 추가
                for (int j = 1; j < 32; j++)
                {
                    string strJ = j.ToString("D2");
                    string strAxis = strJ + "일";
                    float value = dt.Rows[0][strJ].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[0][strJ].ToString());
                    series.Points.Add(new SeriesPoint(strAxis, value));
                }

                // 생성된 시리즈를 리스트에 추가
                seriesList.Add(series);
                series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음

                // 포인트 라벨 설정
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;  
                series.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정

                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가
 
                chart.Series.Add(series);
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Label.TextPattern = "{A:dd}";
                //diagram.AxisX.WholeRange.MinValue = 1;
                //diagram.AxisX.VisualRange.MinValue = 15;
                //diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;

                //시리즈 색깔
                chart.Series["양품수량"].View.Color = Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(192)))), ((int)(((byte)(247)))));

                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "일별 양품수량 합계", Font = new Font(Font.FontFamily.Name, 12, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(Gv_Product);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LupItemClass1_smtLookUpEditValueChanged(object sender, EventArgs e)
        {
            //Search();
        }
    }
}