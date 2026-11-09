using DevExpress.XtraCharts;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;

namespace SmartMES.View.Monitoring.Machine
{
	public partial class Frm_PM_MachinePerformance : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        Database db = new Database();
        string query = "";
        SecondaryAxisY priceAxisY = null;

        public Frm_PM_MachinePerformance()
        {
            InitializeComponent();
        }

        private void Frm_PM_MachinePerformance_Load(object sender, EventArgs e)
        {
            grid.SetPOPGrid(gvMachine);
            gvMachine.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

            Display();
        }

        private void Display()
        {
            string FromDate = DateTime.Today.AddDays(-3).ConvertDate_To_Char8();
            string ToDate = DateTime.Today.ConvertDate_To_Char8();

            try
            {
                query = $@"SELECT
                          CONVERT(Date, SUBSTRING(OP.OrderNo, 3,6),112) AS OrderDate
	                    , OP.OrderNo
	                    , OP.OrderSeq
	                    , OP.ItemCode
                        , CM.ItemName
	                    , CMC.MachineName
	                    , OP.MachineCode
	                    , SUM(OP.OrderQty) AS OrderQty
	                    , SUM(OP.WorkQty) AS WorkQty
	                    , ISNULL(SUM(ORE.GoodQty),0) AS GoodQty
                        , ISNULL(SUM(ORE.NgQty),0) AS NgQty
						, ISNULL((CAST(SUM(ORE.GoodQty) AS FLOAT)/ CAST(SUM(OP.OrderQty) AS FLOAT)) *100,0) AS WorkRate
                        , ISNULL((CAST(SUM(ORE.NgQty)  AS FLOAT)/ CAST(SUM(OP.OrderQty) AS FLOAT)) *100,0) AS NgRate
	                    , OP.Status
                    FROM PM_OrderProcess OP
                    LEFT JOIN PM_OrderResult ORE ON OP.OrderNo = ORE.OrderNo AND OP.OrderSeq = ORE.OrderSeq AND OP.CorpId = ORE.CorpId
                    LEFT JOIN CM_Item CM   ON OP.ItemCode = cm.ItemCode AND OP.CorpId = CM.CorpId
                    LEFT JOIN EQ_Machine CMC ON OP.MachineCode = CMC.MachineCode AND OP.CorpId = CMC.CorpId
                    INNER JOIN (SELECT A.CorpId, ProcessCode, ProcessType FROM CM_Process A LEFT JOIN CM_CateItem B  ON B.ClassItemCode = A.ProcessType AND B.CorpId = A.CorpId WHERE B.CLASS = '401050') CP ON OP.ProcessCode = CP.ProcessCode AND OP.CorpId = CP.CorpId

                    WHERE OP.CorpId = '{WrGlobal.CorpID}'
	                     --  AND CONVERT(Date, SUBSTRING(OP.OrderNo, 3,6),112) BETWEEN '{FromDate}' AND '{ToDate}' --데이터 보이게하려고 우선 적용 X

                    GROUP BY OP.Status, OP.OrderNo, OP.OrderSeq, OP.ItemCode, CM.ItemName, CMC.MachineName, OP.MachineCode

                    ORDER BY CASE WHEN OP.Status = '작업중' THEN 1
								  WHEN OP.Status = '준비' THEN 2
								  WHEN OP.Status = '대기' THEN 3 ELSE 4 END
								  ,OrderNo DESC, ItemCode";

                DataView dv = db.GetDataView("센서리스트", query);
                gcMachine.DataSource = dv;

                //Chart
                ChartControl chart = chartControl1;
                chart.Series.Clear();

                Series series = new Series("진행률", ViewType.Bar);

                series.ArgumentDataMember = "MachineName";
                series.ValueDataMembers.AddRange("WorkRate");
                series.DataSource = dv;

                chart.Series.Add(series);

                Series series2 = new Series("불량률", ViewType.Spline);

                series2.ArgumentDataMember = "MachineName";
                series2.ValueDataMembers.AddRange("NgRate");
                series2.DataSource = dv;

                chart.Series.Add(series2);


                if (priceAxisY != null)
                {
                    ((XYDiagram)chart.Diagram).SecondaryAxesY.Clear();
                }

                if (priceAxisY == null)
                {
                    priceAxisY = new SecondaryAxisY("axisY");
                }

                ((XYDiagram)chart.Diagram).SecondaryAxesY.Add(priceAxisY);

                ((SplineSeriesView)series2.View).AxisY = priceAxisY;

                priceAxisY.Title.Text = "불량률";
                priceAxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }
            catch
            { }

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}