using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet.Model;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Drawing;
using static DevExpress.Utils.MVVM.Internal.ILReader;
using System.Xaml;
using DevExpress.DashboardCommon;
using DevExpress.XtraSpreadsheet.UI;
using System.Windows.Forms;
using DevExpress.Charts.Native;
using DevExpress.Utils;
using Legend = DevExpress.XtraCharts.Legend;

namespace SmartMES.View.Management
{
	public partial class Frm_MM_KPI_SdOrderSales : DevExpress.XtraEditors.XtraForm
	{
		clsGrid grid = new clsGrid();
		Database db = new Database();
		string query = "";


		public Frm_MM_KPI_SdOrderSales()
		{
			InitializeComponent();
			InitializeCharts();
			InitializeGrid();
		}

		private void Frm_MM_KPI_SdOrderSales_Load(object sender, EventArgs e)
		{
			// 기본 조회 기간: 오늘 기준 -7일 ~ 오늘
			Dt_Date.smtStartDateTimeValue = DateTime.Today.AddDays(-7);
			Dt_Date.smtEndDateTimeValue = DateTime.Today;

			btn_Search.Click += (s, ev) => Search();
			btn_Close.Click += (s, ev) => this.Close();
			simpleButton1.Click += (s, ev) => ExcelDownload();

		//	Tm_Time.Start();
			Search();
		}

		private void btn_Search_Click(object sender, EventArgs e)
		{
			Search();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 필요 시 시간 표시용 레이블 갱신
		}

		#region ── 메인 조회 ─────────────────────────────────────────────

		private void Search()
		{
			DateTime dtStart = Dt_Date.smtStartDateTimeValue.Date;
			DateTime dtEnd = Dt_Date.smtEndDateTimeValue.Date;

			try
			{
				LoadKpiCards(dtStart, dtEnd);
				LoadTrendCharts(dtStart, dtEnd);
				LoadDetailGrid(dtStart, dtEnd);
				LoadCustomerBarChart(dtStart, dtEnd);
				LoadMaterialBarChart(dtStart, dtEnd);
			}
			catch (Exception ex)
			{
				DevExpress.XtraEditors.XtraMessageBox.Show(
					$"조회 중 오류가 발생했습니다.\n{ex.Message}", "오류",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region ── 쿼리 상수 ────────────────────────────────────────────
		#region ── SQL 쿼리 상수 ─────────────────────────────────────────

		// ▌ KPI 카드: 당일(조회기간) / 전일 / 전주 집계
		string SQL_KPI = @"
		DECLARE @Today DATE = CAST(GETDATE() AS DATE)
		DECLARE @PreD  DATE = DATEADD(DAY, -1, @Today)
		DECLARE @PreW  DATE = DATEADD(DAY, -7, @Today)
		DECLARE @Start DATE = '{0}'
		DECLARE @End   DATE = '{1}'
		DECLARE @CorpId nvarchar(20) = '" + WrGlobal.CorpID + @"'
 
		;WITH
		-- 수주 집계 (수주일 기준)
		OrderBase AS (
			SELECT
				CAST(OM.ObtainDate AS DATE)      AS ObtainDate,
				SUM(OD.Qty * OD.UnitPrice)       AS OrderAmt,
				COUNT(DISTINCT OM.SdOrderNo)     AS OrderCnt
			FROM BM_SdOrderM OM
			JOIN BM_SdOrderD OD ON OM.CorpId = OD.CorpId AND OM.SdOrderNo = OD.SdOrderNo
			WHERE OM.CorpId = @CorpId
			AND CAST(OM.ObtainDate AS DATE) BETWEEN @Start AND @End
			GROUP BY CAST(OM.ObtainDate AS DATE)
		),
		-- 출하 집계 (출하일 기준)
		SalesBase AS (
			SELECT
				CAST(SM.SalesDate AS DATE)       AS SalesDate,
				SUM(SD.Qty * SD.UnitPrice)       AS SalesAmt,
				COUNT(DISTINCT SM.SalesNo)       AS SalesCnt
			FROM BM_SalesM SM
			JOIN BM_SalesD SD ON SM.CorpId = SD.CorpId AND SM.SalesNo = SD.SalesNo
			WHERE SM.CorpId = @CorpId
			AND CAST(SM.SalesDate AS DATE) BETWEEN @Start AND @End
			GROUP BY CAST(SM.SalesDate AS DATE)
		),
		-- 납기준수율: 출하일 <= 납기일
		DeliveryBase AS (
			SELECT
				CAST(SM.SalesDate AS DATE)  AS SalesDate,
				COUNT(*)                    AS TotalCnt,
				SUM(CASE WHEN CAST(SM.SalesDate AS DATE) <= CAST(OD.DeliveryDate AS DATE)
						 THEN 1 ELSE 0 END) AS OnTimeCnt
			FROM BM_SalesM  SM
			JOIN BM_SalesD  SD ON SM.CorpId   = SD.CorpId AND  SM.SalesNo   = SD.SalesNo
			JOIN BM_SdOrderD OD ON SM.CorpId = OD.CorpId AND  SM.SalesNo   = SD.SalesNo
							   AND SD.SdOrderSeq = OD.SdOrderSeq
			WHERE SM.CorpId = @CorpId
			AND CAST(SM.SalesDate AS DATE) BETWEEN @Start AND @End
			GROUP BY CAST(SM.SalesDate AS DATE)
		),
		-- 리드타임: 수주→출하, 수주→납기
		LeadBase AS (
			SELECT
				CAST(SM.SalesDate AS DATE) AS SalesDate,
				AVG(CAST(DATEDIFF(DAY, OM.ObtainDate, SM.SalesDate)    AS FLOAT)) AS SalesLT,
				AVG(CAST(DATEDIFF(DAY, OM.ObtainDate, OD.DeliveryDate) AS FLOAT)) AS ProdLT
			FROM BM_SalesM   SM
			JOIN BM_SalesD   SD ON SM.CorpId   = SD.CorpId AND SM.SalesNo    = SD.SalesNo
			JOIN BM_SdOrderD OD ON SM.CorpId = OD.CorpId AND SD.SdOrderNo  = OD.SdOrderNo
							   AND SD.SdOrderSeq = OD.SdOrderSeq
			JOIN BM_SdOrderM OM ON OD.CorpId  = OM.CorpId AND  OD.SdOrderNo  = OM.SdOrderNo
			WHERE SM.CorpId = @CorpId
			AND CAST(SM.SalesDate AS DATE) BETWEEN @Start AND @End
			GROUP BY CAST(SM.SalesDate AS DATE)
		)
		SELECT
			-- 수주금액
			ISNULL(SUM(O.OrderAmt), 0)                                                        AS OrderAmt_Total,
			ISNULL((SELECT SUM(OrderAmt) FROM OrderBase WHERE ObtainDate = @PreD), 0)         AS OrderAmt_PreD,
			ISNULL((SELECT SUM(OrderAmt) FROM OrderBase WHERE ObtainDate = @PreW), 0)         AS OrderAmt_PreW,
			-- 수주건수
			ISNULL(SUM(O.OrderCnt), 0)                                                        AS OrderCnt_Total,
			ISNULL((SELECT SUM(OrderCnt) FROM OrderBase WHERE ObtainDate = @PreD), 0)         AS OrderCnt_PreD,
			ISNULL((SELECT SUM(OrderCnt) FROM OrderBase WHERE ObtainDate = @PreW), 0)         AS OrderCnt_PreW,
			-- 출하금액
			ISNULL(SUM(S.SalesAmt), 0)                                                        AS SalesAmt_Total,
			ISNULL((SELECT SUM(SalesAmt) FROM SalesBase WHERE SalesDate = @PreD), 0)          AS SalesAmt_PreD,
			ISNULL((SELECT SUM(SalesAmt) FROM SalesBase WHERE SalesDate = @PreW), 0)          AS SalesAmt_PreW,
			-- 출하건수
			ISNULL(SUM(S.SalesCnt), 0)                                                        AS SalesCnt_Total,
			ISNULL((SELECT SUM(SalesCnt) FROM SalesBase WHERE SalesDate = @PreD), 0)          AS SalesCnt_PreD,
			ISNULL((SELECT SUM(SalesCnt) FROM SalesBase WHERE SalesDate = @PreW), 0)          AS SalesCnt_PreW,
			-- 납기준수율
			ISNULL(CASE WHEN SUM(DL.TotalCnt) > 0
				   THEN ROUND(SUM(DL.OnTimeCnt) * 100.0 / SUM(DL.TotalCnt), 1)
				   ELSE 0 END, 0)                                                              AS DeliveryPer_Total,
			ISNULL((SELECT CASE WHEN SUM(TotalCnt) > 0
						   THEN ROUND(SUM(OnTimeCnt)*100.0/SUM(TotalCnt),1) ELSE 0 END
					FROM DeliveryBase WHERE SalesDate = @PreD), 0)                             AS DeliveryPer_PreD,
			ISNULL((SELECT CASE WHEN SUM(TotalCnt) > 0
						   THEN ROUND(SUM(OnTimeCnt)*100.0/SUM(TotalCnt),1) ELSE 0 END
					FROM DeliveryBase WHERE SalesDate = @PreW), 0)                             AS DeliveryPer_PreW,
			-- 수주출하 리드타임
			ISNULL(ROUND(AVG(L.SalesLT), 1), 0)                                               AS SalesLT_Total,
			ISNULL((SELECT ROUND(AVG(SalesLT),1) FROM LeadBase WHERE SalesDate = @PreD), 0)   AS SalesLT_PreD,
			ISNULL((SELECT ROUND(AVG(SalesLT),1) FROM LeadBase WHERE SalesDate = @PreW), 0)   AS SalesLT_PreW,
			-- 생산 리드타임 (수주→납기)
			ISNULL(ROUND(AVG(L.ProdLT), 1), 0)                                                AS ProdLT_Total,
			ISNULL((SELECT ROUND(AVG(ProdLT),1) FROM LeadBase WHERE SalesDate = @PreD), 0)    AS ProdLT_PreD,
			ISNULL((SELECT ROUND(AVG(ProdLT),1) FROM LeadBase WHERE SalesDate = @PreW), 0)    AS ProdLT_PreW
		FROM OrderBase O
		FULL OUTER JOIN SalesBase    S  ON O.ObtainDate = S.SalesDate
		FULL OUTER JOIN DeliveryBase DL ON ISNULL(O.ObtainDate, S.SalesDate) = DL.SalesDate
		FULL OUTER JOIN LeadBase     L  ON ISNULL(O.ObtainDate, S.SalesDate) = L.SalesDate
		";

		// ▌ 추이 차트: 날짜별 일별 집계
		string SQL_TREND = @"
		DECLARE @CorpId nvarchar(20) = '" + WrGlobal.CorpID + @"'
		DECLARE @Start DATE = '{0}'
		DECLARE @End   DATE = '{1}'
		;WITH DateRange AS (
			SELECT CAST(DATEADD(DAY, n, @Start) AS DATE) AS Dt
			FROM (
				SELECT TOP (DATEDIFF(DAY, @Start, @End) + 1)
					   ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS n
				FROM sys.all_objects
			) x
		),
		OrderBase AS (
			SELECT
				CAST(OM.ObtainDate AS DATE)  AS Dt,
				SUM(OD.Qty * OD.UnitPrice)   AS OrderAmt,
				COUNT(DISTINCT OM.SdOrderNo) AS OrderCnt
			FROM BM_SdOrderM OM
			JOIN BM_SdOrderD OD ON OM.CorpId = OD.CorpId AND OM.SdOrderNo = OD.SdOrderNo
			WHERE OM.CorpId = @CorpId 
			AND CAST(OM.ObtainDate AS DATE) BETWEEN @Start AND @End
			GROUP BY CAST(OM.ObtainDate AS DATE)
		),
		SalesBase AS (
			SELECT
				CAST(SM.SalesDate AS DATE)  AS Dt,
				SUM(SD.Qty * SD.UnitPrice)  AS SalesAmt,
				COUNT(DISTINCT SM.SalesNo)  AS SalesCnt
			FROM BM_SalesM SM
			JOIN BM_SalesD SD ON SM.CorpId = SD.CorpId AND SM.SalesNo = SD.SalesNo 
			WHERE SM.CorpId = @CorpId 
			AND CAST(SM.SalesDate AS DATE) BETWEEN  @Start AND @End
			GROUP BY CAST(SM.SalesDate AS DATE)
		),
		LeadBase AS (
			SELECT
				CAST(SM.SalesDate AS DATE) AS Dt,
				ROUND(AVG(CAST(DATEDIFF(DAY, OM.ObtainDate, SM.SalesDate)    AS FLOAT)), 1) AS SalesLT,
				ROUND(AVG(CAST(DATEDIFF(DAY, OM.ObtainDate, OD.DeliveryDate) AS FLOAT)), 1) AS ProdLT
			FROM BM_SalesM   SM
			JOIN BM_SalesD   SD ON SM.CorpId    = SD.CorpId AND SM.SalesNo    = SD.SalesNo
			JOIN BM_SdOrderD OD ON SD.CorpId  = OD.CorpId AND SD.SdOrderNo  = OD.SdOrderNo
							   AND SD.SdOrderSeq = OD.SdOrderSeq
			JOIN BM_SdOrderM OM ON OD.CorpId  = OM.CorpId AND  OD.SdOrderNo  = OM.SdOrderNo 
			WHERE SM.CorpId = @CorpId 
			AND CAST(SM.SalesDate AS DATE) BETWEEN  @Start AND @End
			GROUP BY CAST(SM.SalesDate AS DATE)
		),
		DeliveryBase AS (
			SELECT
				CAST(SM.SalesDate AS DATE) AS Dt,
				ROUND(
					SUM(CASE WHEN CAST(SM.SalesDate AS DATE) <= CAST(OD.DeliveryDate AS DATE)
							 THEN 1.0 ELSE 0 END)
					/ NULLIF(COUNT(*), 0) * 100, 1)                AS DeliveryPer
			FROM BM_SalesM   SM
			JOIN BM_SalesD   SD ON SM.CorpId    = SD.CorpId AND  SM.SalesNo    = SD.SalesNo
			JOIN BM_SdOrderD OD ON SD.CorpId  = OD.CorpId AND  SD.SdOrderNo  = OD.SdOrderNo 
							   AND SD.SdOrderSeq = OD.SdOrderSeq
			WHERE SM.CorpId = @CorpId 
			AND CAST(SM.SalesDate AS DATE) BETWEEN  @Start AND @End
			GROUP BY CAST(SM.SalesDate AS DATE)
		)
		SELECT
			D.Dt,
			ISNULL(O.OrderAmt,    0) AS OrderAmt,
			ISNULL(S.SalesAmt,    0) AS SalesAmt,
			ISNULL(O.OrderCnt,    0) AS OrderCnt,
			ISNULL(S.SalesCnt,    0) AS SalesCnt,
			ISNULL(L.SalesLT,     0) AS SalesLT,
			ISNULL(L.ProdLT,      0) AS ProdLT,
			ISNULL(DP.DeliveryPer,0) AS DeliveryPer
		FROM DateRange    D
		LEFT JOIN OrderBase    O  ON D.Dt = O.Dt
		LEFT JOIN SalesBase    S  ON D.Dt = S.Dt
		LEFT JOIN LeadBase     L  ON D.Dt = L.Dt
		LEFT JOIN DeliveryBase DP ON D.Dt = DP.Dt
		ORDER BY D.Dt
		";

		// ▌ 상세 그리드: 일자별
		string SQL_DETAIL = @"
		SELECT
			CAST(SM.SalesDate AS DATE)                                                          AS 일자,
			COUNT(DISTINCT OM.SdOrderNo)                                                        AS 수주건수,
			SUM(OD.Qty * OD.UnitPrice)                                                          AS 수주금액,
			COUNT(DISTINCT SM.SalesNo)                                                          AS 출하건수,
			SUM(SD.Qty * SD.UnitPrice)                                                          AS 출하금액,
			ROUND(AVG(CAST(DATEDIFF(DAY, OM.ObtainDate, SM.SalesDate)    AS FLOAT)), 1)        AS 수주출하LT,
			ROUND(AVG(CAST(DATEDIFF(DAY, OM.ObtainDate, OD.DeliveryDate) AS FLOAT)), 1)        AS 생산LT,
			ROUND(
				SUM(CASE WHEN CAST(SM.SalesDate AS DATE) <= CAST(OD.DeliveryDate AS DATE)
						 THEN 1.0 ELSE 0 END)
				/ NULLIF(COUNT(*), 0) * 100, 1)                                                AS 납기준수율
		FROM BM_SalesM   SM
		JOIN BM_SalesD   SD ON SM.CorpId    = SD.CorpId  AND SM.SalesNo    = SD.SalesNo
		JOIN BM_SdOrderD OD ON SD.CorpId  = OD.CorpId	AND SD.SdOrderNo    = OD.SdOrderNo AND SD.SdOrderSeq = OD.SdOrderSeq
		JOIN BM_SdOrderM OM ON OD.SdOrderNo  = OM.SdOrderNo
		WHERE SM.CorpId = '" + WrGlobal.CorpID + @"'
		AND CAST(SM.SalesDate AS DATE) BETWEEN '{0}' AND '{1}'
		GROUP BY CAST(SM.SalesDate AS DATE)
		ORDER BY CAST(SM.SalesDate AS DATE) DESC
		";

		// ▌ 거래처별 수주 현황 (상위 10)
		string SQL_CUSTOMER = @"
		SELECT TOP 10
			CC.CustName                          AS 거래처명,
			SUM(OD.Qty * OD.UnitPrice)           AS 수주금액
		FROM BM_SdOrderM OM
		JOIN BM_SdOrderD OD ON OM.CorpId = OD.CorpId AND OM.SdOrderNo = OD.SdOrderNo
		JOIN CM_Cust  CC ON OM.CorpId   = CC.CorpId  AND OM.SoldToCode   = CC.CustCode
		WHERE OM.CorpId = '" + WrGlobal.CorpID + @"'
		AND CAST(OM.ObtainDate AS DATE) BETWEEN '{0}' AND '{1}'
		GROUP BY CC.CustName
		ORDER BY SUM(OD.Qty * OD.UnitPrice) DESC
		";

		// ▌ 품목별 수주/출하 현황 (상위 10)
		string SQL_MATERIAL = @"
		SELECT TOP 10
			CM.ItemName                           AS 품명,
			SUM(OD.Qty * OD.UnitPrice)           AS 수주금액,
			ISNULL(SUM(SD.Qty * SD.UnitPrice),0) AS 출하금액
		FROM BM_SdOrderM OM
		JOIN BM_SdOrderD OD  ON OM.SdOrderNo  = OD.SdOrderNo
		JOIN CM_Item  CM  ON OD.ItemCode    = CM.ItemCode
		LEFT JOIN BM_SalesD  SD  ON SD.SdOrderNo  = OD.SdOrderNo
								AND SD.SdOrderSeq = OD.SdOrderSeq
		LEFT JOIN BM_SalesM  SM  ON SD.SalesNo    = SM.SalesNo
								AND CAST(SM.SalesDate AS DATE) BETWEEN '{0}' AND '{1}'
		WHERE CAST(OM.ObtainDate AS DATE) BETWEEN '{0}' AND '{1}'
		GROUP BY CM.ItemName
		ORDER BY SUM(OD.Qty * OD.UnitPrice) DESC
		";

		#endregion
		#endregion

		#region ── KPI 카드 ──────────────────────────────────────────────

		private void LoadKpiCards(DateTime dtStart, DateTime dtEnd)
		{
			string sql = string.Format(SQL_KPI,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));

			DataTable dt = db.GetDataTable(sql);
			if (dt == null || dt.Rows.Count == 0) return;

			DataRow r = dt.Rows[0];

			// ── 수주금액
			decimal orderAmt = r["OrderAmt_Total"].ConvertDecimal_Null_To_Zero();
			decimal orderAmt_PreD = r["OrderAmt_PreD"].ConvertDecimal_Null_To_Zero();
			decimal orderAmt_PreW = r["OrderAmt_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl1_OrderAmt.Text = orderAmt == 0 ? "" : orderAmt.ToString("N0");
			Lbl1_OrderAmt_PreD.Text = orderAmt_PreD == 0 ? "" : orderAmt_PreD.ToString("N0");
			Lbl1_OrderAmt_PreW.Text = orderAmt_PreW == 0 ? "" : orderAmt_PreW.ToString("N0");
			SetDiffLabel(Lbl1_OrderAmt_PreD_Per, orderAmt, orderAmt_PreD);
			SetDiffLabel(Lbl1_OrderAmt_PreW_Per, orderAmt, orderAmt_PreW);

			// ── 출하금액
			decimal salesAmt = r["SalesAmt_Total"].ConvertDecimal_Null_To_Zero();
			decimal salesAmt_PreD = r["SalesAmt_PreD"].ConvertDecimal_Null_To_Zero();
			decimal salesAmt_PreW = r["SalesAmt_PreW"].ConvertDecimal_Null_To_Zero();

			Lbl2_SalesAmt.Text = salesAmt == 0 ? "" : salesAmt.ToString("N0");
			Lbl2_SalesAmt_PreD.Text = salesAmt_PreD == 0 ? "" : salesAmt_PreD.ToString("N0");
			Lbl2_SalesAmt_PreW.Text = salesAmt_PreW == 0 ? "" : salesAmt_PreW.ToString("N0");
			SetDiffLabel(Lbl2_SalesAmt_PreD_Per, salesAmt, salesAmt_PreD);
			SetDiffLabel(Lbl2_SalesAmt_PreW_Per, salesAmt, salesAmt_PreW);

			// ── 수주건수
			decimal orderCnt = r["OrderCnt_Total"].ConvertDecimal_Null_To_Zero();
			decimal orderCnt_PreD = r["OrderCnt_PreD"].ConvertDecimal_Null_To_Zero();
			decimal orderCnt_PreW = r["OrderCnt_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl3_OrderCnt.Text = orderCnt == 0 ? "" : orderCnt.ToString("N0");
			Lbl3_OrderCnt_PreD.Text = orderCnt_PreD== 0 ? "" : orderCnt_PreD.ToString("N0");
			Lbl3_OrderCnt_PreW.Text = orderCnt_PreW == 0 ? "" : orderCnt_PreW.ToString("N0");
			SetDiffLabel(Lbl3_OrderCnt_PreD_Per, orderCnt, orderCnt_PreD);
			SetDiffLabel(Lbl3_OrderCnt_PreW_Per, orderCnt, orderCnt_PreW);

			// ── 출하건수
			decimal salesCnt	  = r["SalesCnt_Total"].ConvertDecimal_Null_To_Zero();
			decimal salesCnt_PreD = r["SalesCnt_PreD"].ConvertDecimal_Null_To_Zero();
			decimal salesCnt_PreW = r["SalesCnt_PreW"].ConvertDecimal_Null_To_Zero();

			Lbl4_SalesCnt.Text = salesCnt == 0 ? "" : salesCnt.ToString("N0");
			Lbl4_SalesCnt_PreD.Text = salesCnt_PreD== 0 ? "" : salesCnt_PreD.ToString("N0");
			Lbl4_SalesCnt_PreW.Text = salesCnt_PreW == 0 ? "" : salesCnt_PreW.ToString("N0");
			SetDiffLabel(Lbl4_SalesCnt_PreD_Per, salesCnt, salesCnt_PreD);
			SetDiffLabel(Lbl4_SalesCnt_PreW_Per, salesCnt, salesCnt_PreW);

			// ── 수주출하 리드타임
			decimal salesLT		 = r["SalesLT_Total"].ConvertDecimal_Null_To_Zero();
			decimal salesLT_PreD = r["SalesLT_PreD"].ConvertDecimal_Null_To_Zero();
			decimal salesLT_PreW = r["SalesLT_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl5_LeadTime.Text = salesLT == 0 ? "" : salesLT.ToString("N1");
			Lbl5_LeadTime_PreD.Text = salesLT_PreD == 0 ? "" : salesLT_PreD.ToString("N1");
			Lbl5_LeadTime_PreW.Text = salesLT_PreW == 0 ? "" : salesLT_PreW.ToString("N1");
			// 리드타임은 낮을수록 좋음 → 색 반전
			SetDiffLabel(Lbl5_LeadTime_PreD_Per, salesLT, salesLT_PreD, reverseColor: true);
			SetDiffLabel(Lbl5_LeadTime_PreW_Per, salesLT, salesLT_PreW, reverseColor: true);

			// ── 납기준수율
			decimal delivPer	  = r["DeliveryPer_Total"].ConvertDecimal_Null_To_Zero();
			decimal delivPer_PreD = r["DeliveryPer_PreD"].ConvertDecimal_Null_To_Zero();
			decimal delivPer_PreW = r["DeliveryPer_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl6_DeliveryPer.Text = delivPer == 0 ? "" : delivPer.ToString("N1");
			Lbl6_DeliveryPer_PreD.Text = delivPer_PreD == 0 ? "" : delivPer_PreD.ToString("N1");
			Lbl6_DeliveryPer_PreW.Text = delivPer_PreW == 0 ? "" :delivPer_PreW.ToString("N1");
			SetDiffLabel(Lbl6_DeliveryPer_PreD_Per, delivPer, delivPer_PreD);
			SetDiffLabel(Lbl6_DeliveryPer_PreW_per, delivPer, delivPer_PreW);
		}

		/// <summary>
		/// 전일/전주 대비 증감률 표시. reverseColor=true 이면 증가가 파랑(나쁨), 감소가 빨강(나쁨→아니, 감소가 좋음→파랑)
		/// </summary>
		private void SetDiffLabel(DevExpress.XtraEditors.LabelControl lbl,
								  decimal current, decimal prev, bool reverseColor = false)
		{
			if (prev == 0)
			{
				lbl.Text = "-";
				lbl.Appearance.ForeColor = Color.Gray;
				return;
			}

			decimal diff = (current - prev) / Math.Abs(prev) * 100;
			string arrow = diff >= 0 ? "▲" : "▼";
			lbl.Text = $"{arrow} {Math.Abs(diff):N2}%";

			bool isUp = diff >= 0;
			// 기본: 증가=빨강, 감소=파랑 / 리드타임처럼 반전: 증가=파랑, 감소=빨강
			Color upColor = reverseColor ? Color.Blue : Color.Red;
			Color downColor = reverseColor ? Color.Red : Color.Blue;
			lbl.Appearance.ForeColor = isUp ? upColor : downColor;
		}

		#endregion

		#region ── 추이 차트 초기화 ─────────────────────────────────────

		private void InitializeCharts()
		{
			SetupLineChart(ChartInOutDay, "수주금액", "출하금액", Color.DodgerBlue, Color.LimeGreen, false , "{V:#,##0}");
			SetupLineChart(chartControl3, "수주건수", "출하건수", Color.DodgerBlue, Color.LimeGreen, false , "{V:#,##0}");
			SetupLineChart(chartControl1, "수주출하LT", "생산LT", Color.Orange, Color.MediumPurple , false , "{V:#,##0.0}");
			SetupLineChart(chartControl2, "납기준수율", null, Color.Gold, Color.Transparent, true, "{V:#,##0.0}");

			SetupBarChart(ChartOrder_Cust, "거래처별 수주현황(TOP 10)", Color.DodgerBlue, "거래처명", "수주금액");
			SetupBarChart(ChartOrder_Item, "품목별 수주/출하 현황(TOP 10)", Color.DodgerBlue, "품명", "수주금액");
		}

		private void SetupLineChart(DevExpress.XtraCharts.ChartControl chart,
									 string series1Name, string series2Name,
									 Color color1, Color color2,
									 bool singleSeries
									, string format)
		{
		
			chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
			chart.Series.Clear();

			// Series 1
			var s1 = new Series(series1Name, ViewType.Line);
			((LineSeriesView)s1.View).Color = color1;
			((LineSeriesView)s1.View).MarkerVisibility = DefaultBoolean.True;
			((LineSeriesView)s1.View).LineMarkerOptions.Size = 6;
			s1.ArgumentDataMember = "Dt";
			//s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 라벨 보이도록 설정
			s1.Label.TextPattern = format;
			s1.CrosshairLabelPattern = "{A:MM-dd} : " + format;
			chart.Series.Add(s1);

			if (!singleSeries && !string.IsNullOrEmpty(series2Name))
			{
				var s2 = new Series(series2Name, ViewType.Line);
				((LineSeriesView)s2.View).Color = color2;
				((LineSeriesView)s2.View).LineMarkerOptions.Size = 6;
				//((LineSeriesView)s2.View).MarkerVisibility = DefaultBoolean.True;
				s2.ArgumentDataMember = "Dt";
				s2.Label.TextPattern = format; 
				s2.CrosshairLabelPattern = "{A:MM-dd} : " +format;

				chart.Series.Add(s2);
			}

			var diagram = (XYDiagram)chart.Diagram;
			//diagram.AxisX.Label.Angle = -45;
			diagram.AxisX.Label.Font = new Font("Pretendard", 8f);
			diagram.AxisX.Label.TextPattern = "{0:MM-dd}";
			diagram.AxisX.GridLines.Visible = false;

			diagram.AxisY.Label.Font = new Font("Pretendard", 8f);
			diagram.AxisY.Label.TextPattern = format;
			//diagram.AxisY.GridLines.Visible = false;

			diagram.DefaultPane.BackColor = Color.Transparent;
			//diagram.DefaultPane.BorderVisible = false;

			// 범례 (시리즈 이름 직접 표시)
			Legend legend = chart.Legend;

			if (legend != null)
			{
				legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
				legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
				legend.Direction = LegendDirection.LeftToRight;
				legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
				legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
				legend.BackColor = System.Drawing.Color.Transparent;
			}
		}

		private void SetupBarChart(ChartControl chart, string name, Color color, string argument, string value)
		{
			string format = "{V:#,##0}";

			chart.Series.Clear();
			chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

			Series series = new Series(name, ViewType.Bar);
			series.ArgumentDataMember = argument;
			series.ValueDataMembers.AddRange(value);
			series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
			series.Label.TextPattern = format;
			series.CrosshairLabelPattern = "{A} : {V:#,##0}";

			chart.Series.Add(series);

			BarSeriesView barView = (BarSeriesView)series.View;
			//barView.ColorEach = true;
			barView.BarWidth = 0.5;
			barView.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
			barView.Color = color;

			BarSeriesLabel label = (BarSeriesLabel)series.Label;
			label.TextPattern = format;
			label.Position = BarSeriesLabelPosition.Auto;
			label.TextOrientation = TextOrientation.Horizontal;

			//label.TextColor = Color.Black;
			label.BackColor = Color.Transparent;
			label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

			XYDiagram diagram = (XYDiagram)chart.Diagram;
			diagram.Rotated = true;
			diagram.DefaultPane.BackColor = Color.Transparent;
			diagram.DefaultPane.BorderVisible = false;

			diagram.AxisX.Label.Font = new Font("Pretendard", 8f);
			diagram.AxisX.Tickmarks.Visible = false;
			diagram.AxisX.Tickmarks.MinorVisible = false;
			diagram.AxisX.Reverse = true;

			diagram.AxisX.Visible = true;
			diagram.AxisX.Label.Visible = true; // 거래처명 라벨 강제 표시
			diagram.AxisX.Label.TextAlignment = StringAlignment.Near; // 거래처명 라벨 강제 표시

			diagram.AxisY.GridLines.Visible = false;
			diagram.AxisY.Label.Visible = false;
			diagram.AxisY.Label.TextPattern = format;
			diagram.AxisY.Label.Font = new Font("Pretendard", 8f);
		}

		#endregion

		#region ── 추이 차트 데이터 바인딩 ─────────────────────────────

		private void LoadTrendCharts(DateTime dtStart, DateTime dtEnd)
		{
			string sql = string.Format(SQL_TREND, dtStart.ToString("yyyy-MM-dd"), dtEnd.ToString("yyyy-MM-dd"));

			DataTable dt = db.GetDataTable(sql);
			if (dt == null) return;

			// 수주/출하 금액 추이
			BindLineChart(ChartInOutDay, dt, "Dt", "OrderAmt", "SalesAmt");
			// 수주/출하 건수 추이
			BindLineChart(chartControl3, dt, "Dt", "OrderCnt", "SalesCnt");
			// 리드타임 추이
			BindLineChart(chartControl1, dt, "Dt", "SalesLT", "ProdLT");
			// 납기준수율 추이 (단일 시리즈)
			BindLineChartSingle(chartControl2, dt, "Dt", "DeliveryPer");

			sql = string.Format(SQL_CUSTOMER, dtStart.ToString("d"), dtEnd.ToString("d"));
			dt = db.GetDataTable(sql);

			BindLineChartSingle(ChartOrder_Cust, dt, "거래처명", "수주금액");


			sql = string.Format(SQL_MATERIAL, dtStart.ToString("d"), dtEnd.ToString("d"));
			dt = db.GetDataTable(sql);

			BindLineChartSingle(ChartOrder_Item, dt, "품명", "출하금액");
		}

		private void BindLineChart(DevExpress.XtraCharts.ChartControl chart,
									DataTable dt,
									string argField, string val1Field, string val2Field)
		{
			chart.DataSource = dt;
			chart.Series[0].ArgumentDataMember = argField;
			chart.Series[0].ValueDataMembers.AddRange(new string[] { val1Field });

			if (chart.Series.Count > 1)
			{
				chart.Series[1].ArgumentDataMember = argField;
				chart.Series[1].ValueDataMembers.AddRange(new string[] { val2Field });
			}
		}

		private void BindLineChartSingle(DevExpress.XtraCharts.ChartControl chart,
										  DataTable dt, string argField, string valField)
		{
			chart.DataSource = dt;
			chart.Series[0].ArgumentDataMember = argField;
			chart.Series[0].ValueDataMembers.AddRange(new string[] { valField });
		}

		#endregion

		#region ── 상세 그리드 초기화 / 바인딩 ─────────────────────────

		private void InitializeGrid()
		{
			GridView gv = GvList;

			gv.OptionsBehavior.Editable = false;
			gv.OptionsView.ShowGroupPanel = false;
			gv.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 9f);
			gv.Appearance.Row.Font = new Font("Pretendard", 9f);
			gv.Appearance.FooterPanel.Font = new Font("Pretendard SemiBold", 9f);
			gv.OptionsView.ShowFooter = true;

			//
			grid.Create_GvColumn(gv, "일자", "일자");
			grid.Create_GvColumn(gv, "수주건수", "수주건수");
			grid.Create_GvColumn(gv, "출하건수", "출하건수");
			grid.Create_GvColumn(gv, "수주금액", "수주금액");
			grid.Create_GvColumn(gv, "출하금액", "출하금액");
			grid.Create_GvColumn(gv, "수주출하LT", "수주출하LT");
			grid.Create_GvColumn(gv, "생산LT", "생산LT");
			grid.Create_GvColumn(gv, "납기준수율", "납기준수율");

			//
			grid.SetCol_Type(gv.Columns["수주건수"], clsGrid.TypeList.TInt);
			grid.SetCol_Type(gv.Columns["출하건수"], clsGrid.TypeList.TInt);
			grid.SetCol_Type(gv.Columns["수주금액"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["출하금액"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["수주출하LT"], clsGrid.TypeList.TFloatN1);
			grid.SetCol_Type(gv.Columns["생산LT"], clsGrid.TypeList.TFloatN1);
			grid.SetCol_Type(gv.Columns["납기준수율"], clsGrid.TypeList.TFloatN1);

			//
			grid.SetCol_Summary(gv, "수주건수");
			grid.SetCol_Summary(gv, "출하건수");
			grid.SetCol_Summary(gv, "수주금액");
			grid.SetCol_Summary(gv, "출하금액");
			grid.SetCol_Summary(gv, "수주출하LT", DevExpress.Data.SummaryItemType.Average, "{0:N1}");
			grid.SetCol_Summary(gv, "생산LT", DevExpress.Data.SummaryItemType.Average, "{0:N1}");
			grid.SetCol_Summary(gv, "납기준수율", DevExpress.Data.SummaryItemType.Average, "{0:N1}");

			//
			gv.RowCellStyle += Gv_RowCellStyle;
		}

		private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			if (e.Column.FieldName != "납기준수율") return;
			if (e.CellValue == null || e.CellValue == DBNull.Value) return;
			double val = Convert.ToDouble(e.CellValue);
			e.Appearance.ForeColor = val >= 95 ? Color.LimeGreen
								   : val >= 80 ? Color.Orange
											   : Color.Red;
		}

		private void LoadDetailGrid(DateTime dtStart, DateTime dtEnd)
		{
			string sql = string.Format(SQL_DETAIL,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));

			DataTable dt = db.GetDataTable(sql);
			GcList.DataSource = dt;
		}

		#endregion

		#region ── 거래처별 수주 Bar 차트 ───────────────────────────────

		private void LoadCustomerBarChart(DateTime dtStart, DateTime dtEnd)
		{
			// 디자이너 코드에는 없는 차트이므로, 런타임에 동적으로 생성하거나
			// 별도 컨트롤(chartControl_Customer)을 디자이너에 추가 후 연결 필요.
			// 여기서는 chartControl2가 납기준수율이므로 별도 처리.
			// → 실제 화면처럼 하려면 디자이너에 ChartControl 2개 더 추가 권장.
			// 이 메서드는 해당 차트 컨트롤 연결 후 아래 로직 사용.

			/*
			string sql = string.Format(SQL_CUSTOMER,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));
			DataTable dt = db.GetDataTable(sql);
			if (dt == null) return;

			chartCustomer.Series.Clear();
			var s = new Series("수주금액", ViewType.Bar);
			s.ArgumentDataMember = "거래처명";
			s.ValueDataMembersNames = "수주금액";
			((BarSeriesView)s.View).Color = Color.DodgerBlue;
			chartCustomer.Series.Add(s);
			chartCustomer.DataSource = dt;
			var diag = (XYDiagram)chartCustomer.Diagram;
			diag.Rotated = true; // 수평 막대
			*/
		}

		#endregion

		#region ── 품목별 수주/출하 Bar 차트 ────────────────────────────

		private void LoadMaterialBarChart(DateTime dtStart, DateTime dtEnd)
		{
			/*
			string sql = string.Format(SQL_MATERIAL,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));
			DataTable dt = db.GetDataTable(sql);
			if (dt == null) return;

			chartMaterial.Series.Clear();

			var s1 = new Series("수주금액", ViewType.Bar);
			s1.ArgumentDataMember = "품목명";
			s1.ValueDataMembersNames = "수주금액";
			((BarSeriesView)s1.View).Color = Color.DodgerBlue;

			var s2 = new Series("출하금액", ViewType.Bar);
			s2.ArgumentDataMember = "품목명";
			s2.ValueDataMembersNames = "출하금액";
			((BarSeriesView)s2.View).Color = Color.LimeGreen;

			chartMaterial.Series.Add(s1);
			chartMaterial.Series.Add(s2);
			chartMaterial.DataSource = dt;
			var diag = (XYDiagram)chartMaterial.Diagram;
			diag.Rotated = true;
			*/
		}

		#endregion


		private void ExcelDownload()
		{
			using (var dlg = new SaveFileDialog())
			{
				dlg.Filter = "Excel Files (*.xlsx)|*.xlsx";
				dlg.FileName = $"수주출하현황_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
				if (dlg.ShowDialog() != DialogResult.OK) return;

				try
				{
					GvList.ExportToXlsx(dlg.FileName);
					DevExpress.XtraEditors.XtraMessageBox.Show(
						"엑셀 파일이 저장되었습니다.", "완료",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					DevExpress.XtraEditors.XtraMessageBox.Show(
						$"저장 실패: {ex.Message}", "오류",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}