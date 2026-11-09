using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using System;
using SmartCommon.Common;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Monitoring
{
    public partial class Frm_PM_ItemPerformance : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsGrid grid = new clsGrid();
        Timer tm = new Timer();
        string query = "";
        string Status = "";  //10:가공, 20:조립

        public Frm_PM_ItemPerformance(string param)
        {
            InitializeComponent();

            //if(param == "가공") { this.Status = "10"; smtCommonBar1.smtImageTitleText = "가공생산 모니터링";  LblMainTitle.Text = "가공생산 모니터링"; }
            //else if(param == "조립") { this.Status = "20"; smtCommonBar1.smtImageTitleText = "조립생산 모니터링"; LblMainTitle.Text = "조립생산 모니터링"; }

            smtCommonBar1.smtImageTitleText = param;
            LblMainTitle.Text = param;

            Set_GridView(gvMachine);
        }
        private void Frm_PM_ItemPerformance_Load(object sender, EventArgs e)
        {
            Search(Status);
            SetDesign();

            tm.Start();
            Tm_Search.Start();
            TmTime.Enabled = true;

            LblMainTitle.Visible = false;
        }
        private void TmTime_Tick(object sender, EventArgs e)
        {
            LblTime.Text = DateTime.Now.ToString();
        }

        private void Tm_Search_Tick(object sender, EventArgs e)
        {
            Search(Status);
        }

        private void SetDesign()
        {
            if(Status == "10")
            {
                PnlStatus.Columns[2].Visible = false;
                PnlStatus.Columns[3].Visible = false;
            }
        }

        private void Set_GridView(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
                                                                                                        //gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.RowAutoHeight = true;    //MemoEdit MultiLine
            gv.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            gv.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 18.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "지시일자", "OrderDate");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "설비", "MachineName");
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "실적수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "달성률 (%)", "Rate");

            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Status"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["Rate"], clsGrid.TypeList.TFloat);

            //칼럼Visible
            gv.Columns["OrderDate"].Visible = false;
            gv.Columns["WorkQty"].Visible = false;
            if(Status == "20") { gv.Columns["MachineName"].Visible = false; } //조립일시 설비칼럼 숨김

            //이벤트
            grid.SetPOPGrid(gv);
            gv.CustomDrawCell += RowCellDisplayValue; //진행 상황표기
        }

        private void Search(string processType)
        {
            string FromDate = DateTime.Today.AddDays(-3).ConvertDate_To_Char8();
            string ToDate = DateTime.Today.ConvertDate_To_Char8();

            query = $@"
                    SELECT
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
						, ISNULL(ROUND((CAST(SUM(ORE.GoodQty)  AS FLOAT)/ CAST(SUM(OP.OrderQty) AS FLOAT)) *100, 2), 0) AS Rate
	                    , OP.Status
                    FROM PM_OrderProcess OP
                    LEFT JOIN PM_OrderResult ORE ON OP.OrderNo = ORE.OrderNo AND OP.OrderSeq = ORE.OrderSeq AND OP.CorpId = ORE.CorpId
                    INNER JOIN CM_Item CM  ON OP.ItemCode = cm.ItemCode AND OP.CorpId = CM.CorpId
                    LEFT JOIN EQ_Machine CMC ON OP.MachineCode = CMC.MachineCode AND OP.CorpId = CMC.CorpId
                    --INNER JOIN (SELECT A.CorpId, ProcessCode, ProcessType FROM CM_Process A LEFT JOIN CM_CateItem B  ON B.ItemCode = A.ProcessType AND B.CorpId = A.CorpId WHERE B.CLASS = '401050') CP ON OP.ProcessCode = CP.ProcessCode AND OP.CorpId = CP.CorpId

                    WHERE OP.CorpId = '{WrGlobal.CorpID}'
                         -- AND CP.ProcessType = '{processType}'  -- 10:가공, 20:조립
	                     -- AND CONVERT(Date, SUBSTRING(OP.OrderNo, 3,6),112) BETWEEN '{FromDate}' AND '{ToDate}' --데이터 보이게하려고 우선 적용 X


                    GROUP BY OP.Status, OP.OrderNo, OP.OrderSeq, OP.ItemCode, CM.ItemName, CMC.MachineName, OP.MachineCode

                    ORDER BY CASE WHEN OP.Status = '작업중' THEN 1
								  WHEN OP.Status = '준비' THEN 2
								  WHEN OP.Status = '대기' THEN 3 ELSE 4 END
								  ,OrderNo DESC, ItemCode
            ";

            gcMachine.DataSource = db.GetDataTable(query);
            gvMachine.BestFitColumns();


            /*
            query = $@"EXEC Analysis_ProductPerform '{WrGlobal.CorpID}'
                                                    , '{DateTime.Now.Date:yyyy-MM-dd HH:mm:ss}' 
                                                    , '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                                                    ,'ALL'
                        ";
            */
        }

        private void RowCellDisplayValue(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("Status") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "작업중")
            {
                //진행
                e.Appearance.ForeColor = Color.Green;
                e.DisplayText = "●";
            }
            else if (cellValue == "대기")
            {
                //대기
                e.Appearance.ForeColor = Color.Gray;
                e.DisplayText = "●";
            }
            else if (cellValue.Contains("작업완료"))
            {
                //종료
                e.Appearance.ForeColor = Color.Red;
                e.DisplayText = "●";
            }
            else if (cellValue.Contains("준비"))
            {
                //종료
                e.Appearance.ForeColor = Color.Gold;
                e.DisplayText = "●";
            }
        }

        private void TmSearch_Tick(object sender, EventArgs e)
        {
            Search(Status);
        }

        private void gvMachine_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "QtyRate")
            {
                // 값 그대로 표시하고 %를 붙임
                e.DisplayText = string.Format("{0:0.#}%", e.Value);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}