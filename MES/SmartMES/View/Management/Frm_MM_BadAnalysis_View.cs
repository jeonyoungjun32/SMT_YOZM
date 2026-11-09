using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Repository;
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

namespace SmartMES.View.Management
{
	public partial class Frm_MM_BadAnalysis_View : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();
        clsRepository rpo = new clsRepository();
        List<string> badMmList = new List<string>();
        string ItemType = "10,20,30";

        public Frm_MM_BadAnalysis_View()
        {
            InitializeComponent();
            SetDesign();
            foreach (GridColumn col in Gv_BadAnalysis.Columns) { grid.Column_AllowEdit(col, false); } //셀 수정 가능여부

            Func_Search();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            barDate.smtStartDateTimeValue= DateTime.Now.AddDays(-7);
            barDate.smtEndDateTimeValue= DateTime.Now;

            //GridView 설정
            Set_GridView(Gv_BadAnalysis);

            //SerchLookUp 설정
            Set_SearchLup();

            //엑셀다운(GV에 보이는 형식 그대로 저장)
            //Btn_ExcelDown_ALL.DownGridControl = this.Gc_BadAnalysis;
        }
        #endregion

        private void Set_SearchLup()
        {
            CM_Item material = new CM_Item();
            material.ItemType = "20"; // 제품
            DataTable dt_Product = material.Select_LupProduct();

            DataTable dt_Line = code.GetLine_List("", true);

            CM_Process process = new CM_Process();
            DataTable dt_Process = process.SelectCM_ProcessLookup();

            cls_Lookup clsLup = new cls_Lookup();
            clsLup.Create_RepositoryItemSearchLookUp(LupItem, dt_Product, "코드", "품명");
            clsLup.Create_RepositoryItemSearchLookUp(LupProcess, dt_Process, "코드", "공정명");
			clsLup.Create_RepositoryItemSearchLookUp(LupLine, dt_Line, "라인코드", "라인명");

			LupProcess.smtLookUpValue = "ALL";
            LupItem.smtLookUpValue = "ALL"; 
            LupLine.smtLookUpValue = "ALL";

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

        private void Set_GridView(GridView gv)
        {
            clsGrid grid = new clsGrid();
            clsRepository rpo = new clsRepository();

            //그리드 멀티셀렉트 설정
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            CM_Process process = new CM_Process();
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(process.SelectCM_ProcessLookup(), "코드", "공정명");

            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "품목분류1", "ItemClass1", lup_Class_Cd1);
            grid.Create_GvColumn(gv, "품목분류2", "ItemClass2", lup_Class_Cd2);
            grid.Create_GvColumn(gv, "품번", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "생산량", "생산량");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "양품수량", "양품수량");
            grid.Create_GvColumn(gv, "불량수량", "불량수량");
            grid.Create_GvColumn(gv, "불량률", "불량률");
            grid.Create_GvColumn(gv, "불량금액", "Bad_Price");

            grid.SetCol_Type(gv.Columns["생산량"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["양품수량"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["불량수량"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["불량율"], clsGrid.TypeList.TFloat);

            gv.Columns["UnitPrice"].Visible = false;
            gv.Columns["Bad_Price"].Visible = false;

            // Bad_Price 총 합계 추가(컨설팅 요청 2024.07.18)
            gv.Columns["Bad_Price"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv.Columns["Bad_Price"].SummaryItem.DisplayFormat = "총 합계: {0:n2}";
            gv.OptionsView.ShowFooter = true;
        }


        private void Frm_PD_ProcessResult_Load(object sender, EventArgs e)
        {
            //Func_Search();
        }

        #region 데이터 조회

        private void Func_Search()
        {
            BadAnalysis_Search();
        }

        private void BadAnalysis_Search()
        {
            if (!Check_Search()) return;

            Gc_BadAnalysis.MainView = Gv_BadAnalysis;

            string RstFrom = barDate.smtStartDateTimeValue.ToString("yyyyMMdd");
            string RstTo = barDate.smtEndDateTimeValue.ToString("yyyyMMdd");

            //불량유형 조회 및 칼럼추가
            //01.삭제하려는 불량유형 칼럼을 badNmList에 담아준다
            List<string> delCol = badMmList;
            for (int i = 0; i < delCol.Count; i++)
            {
                //이전기록 칼럼 제거
                string colNm = delCol[i].ConvertString_Null_To_Empty();
                Gv_BadAnalysis.Columns.Remove(Gv_BadAnalysis.Columns[colNm]);
            }

            //02.badNmList 비우기
            badMmList.Clear();

            string itemClass1 = LupItemClass1.smtLookUpValue.ConvertString_Null_To_Empty();
            string itemClass2 = LupItemClass2.smtLookUpValue.ConvertString_Null_To_Empty();

            PM_OrderResult pm = new PM_OrderResult();
            pm.ProcessCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();
            pm.ItemCode = LupItem.smtLookUpValue.ConvertString_Null_To_Empty();

            DataTable dt = pm.Select_BadAnalysis(RstFrom, RstTo, itemClass1, itemClass2);

            Gc_BadAnalysis.DataSource = dt;
            SetChartDataQtyDay(barChart, dt);
            SetChartDataQtyDay(barChart2, dt);
            SetlabelText(dt);
                    
            Gv_BadAnalysis.BestFitColumns();
        }
        private bool Check_Search()
        {
            //if (LupLine.smtLookUpValue.ConvertString_Null_To_Empty() == "" || LupProcess.smtLookUpValue.ConvertString_Null_To_Empty() == "")
            //{
                //MessageBox.Show("라인 및 공정 미선택");
                //return false;
            //}

            return true;
        }
        #endregion 데이터 조회 End
        private void SetlabelText(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                lbl불량률.Text = null; lbl불량수량.Text = null; lbl생산수량.Text = null; lbl양품수량.Text = null;
                lblTop1.Text = null; lblTop2.Text = null; lblTop3.Text = null; lblTop4.Text = null; lblTop5.Text = null;
                return;
            }
            lblTop1.Text = null; lblTop2.Text = null; lblTop3.Text = null; lblTop4.Text = null; lblTop5.Text = null;
            float a = 0; float b = 0; float c = 0; float d = 0;

            for (int idx = 0; idx < dt1.Rows.Count; idx++) 
            {
                b += dt1.Rows[idx]["불량수량"].ToString() == "" ? 0 : dt1.Rows[idx]["불량수량"].ConvertInt_Null_To_Zero();
                c += dt1.Rows[idx]["양품수량"].ToString() == "" ? 0 : dt1.Rows[idx]["양품수량"].ConvertInt_Null_To_Zero();
                d += dt1.Rows[idx]["생산량"].ToString() == "" ? 0 : dt1.Rows[idx]["생산량"].ConvertInt_Null_To_Zero();
        }
                       
            lbl불량률.Text = (b / d * 100).ToString("0.000");
            lbl불량수량.Text = b.ToString("#,##0");
            lbl양품수량.Text = c.ToString("#,##0");
            lbl생산수량.Text = d.ToString("#,##0");

            int j = 1;
            Label[] labels = { lblTop1, lblTop2, lblTop3, lblTop4, lblTop5 };
            for (int idx = 0; idx < dt1.Rows.Count; idx++)
            {
                string str = dt1.Rows[idx]["index"].ToString();
                for (j = 1; j < 6; j++)
                {
                    if (Convert.ToInt32(str) == j) { labels[j-1].Text = dt1.Rows[idx]["ItemName"].ToString(); };
                }
            }
        }

        private void SetChartDataQtyDay(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                for (int idx = 0; idx < dt.Rows.Count; idx++)
                {
                    string badNm = dt.Rows[idx]["ItemName"].ToString();

                    // 각 불량 유형별로 시리즈 생성
                    Series series = new Series(badNm, ViewType.Bar);
                    if (chart.Name == "barChart2") { series = new Series(badNm, ViewType.Point); }

                    // 해당 시리즈에 데이터 포인트 추가
                    if (chart.Name == "barChart2") { series.Points.Add(new SeriesPoint(badNm, dt.Rows[idx]["불량률"].ConvertFloat_Null_To_Zero())); }
                    else { series.Points.Add(new SeriesPoint(badNm, dt.Rows[idx]["불량수량"].ConvertInt_Null_To_Zero())); };

                    // 생성된 시리즈를 리스트에 추가
                    seriesList.Add(series);

                    // 포인트 라벨 설정
                    series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    // 포인트 값 표시 형식 설정
                    if (chart.Name == "barChart2") { series.Label.TextPattern = "{V:F2}"; }
                    else { series.Label.TextPattern = "{V:#,#}"; };

                }

                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가
                foreach (var series in seriesList)
                {
                    chart.Series.Add(series);
                }
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Title.Text = "불량유형";
                //diagram.AxisY.Title.Text = "불량합계";
                diagram.AxisX.Title.Text = "품목명";
                diagram.AxisX.Title.DXFont = new Font(Font.FontFamily.Name, 8, FontStyle.Bold);

                // 제목 설정
                if (chart.Name == "barChart2") { chart.Titles.Add(new ChartTitle { Text = "품목별 불량률 (%)", Font = new Font(Font.FontFamily.Name, 12, FontStyle.Bold) }); }
                else{ chart.Titles.Add(new ChartTitle { Text = "품목별 불량수량 합계 (개)", Font = new Font(Font.FontFamily.Name, 12, FontStyle.Bold) }); }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            clsExcelDown cls = new clsExcelDown();
            cls.DownGridControl = this.Gc_BadAnalysis;
            cls.Btn_ExcelDown();

            /*
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_BadAnalysis.ExportToXls(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }*/
        }
    }
}