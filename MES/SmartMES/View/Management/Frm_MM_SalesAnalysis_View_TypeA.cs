
using DevExpress.Data;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Management
{
	public partial class Frm_MM_SalesAnalysis_View_TypeA : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        string FormType = ""; // 폼로드명 매입 purchase 매출 Sales
        Database db = new Database();

        public Frm_MM_SalesAnalysis_View_TypeA()
        {
            InitializeComponent();
        }
        public Frm_MM_SalesAnalysis_View_TypeA(string formType)
        {
            InitializeComponent();
            this.FormType = formType;
        }
        private void Frm_MM_SalesAnalysis_View_TypeA_Load(object sender, EventArgs e)
        {
            barDate.smtStartDateTimeValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            barDate.smtEndDateTimeValue = DateTime.Now;

            Set_SearchLup();
			Set_Grid(Gv_Purchase);

			CboSearchType.smtComboBoxValue = "일";

            Search();
        }

        private void Set_SearchLup()
        {
            // 거래처
            string query = $@"select'ALL' 거래처코드, '전체' 거래처명 Union All select CustCode 거래처코드, CustName 거래처명 
                        From CM_Cust 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND CustType In ('10','40')";
            LupCustomer.smtLookUpDataSource = db.GetDataTable(query);
            LupCustomer.smtLookUpDataDisplayMember = "거래처명";
            LupCustomer.smtLookUpDataValueMember = "거래처코드";
            LupCustomer.smtLookUpValue = "ALL";

            // 품목
            string query2 = $@"select'ALL' 품목코드, '전체' 품명 Union All select ItemCode 품목코드, ItemName 품명
                        From CM_Item
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND ItemType In ('40')";

            LupItem.smtLookUpDataSource = db.GetDataTable(query2);
            LupItem.smtLookUpDataDisplayMember = "품명";
            LupItem.smtLookUpDataValueMember = "품목코드";
            LupItem.smtLookUpValue = "ALL";
        }

        private void Set_Grid(GridView gv)
		{
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.OptionsView.ShowGroupPanel = false;

            //gv.OptionsFind.AlwaysVisible = false;

            //Column 생성
            grid.Create_GvColumn(gv, "거래처명", "CustName");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, " ", "GKey");
            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "금액", "Price");


            //
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);

            //Repository 설정

            grid.RowNumber_Indicator(gv);       //행순번 표기
            gv.RowStyle += gridView_RowStyle;
        }

	
		private void gridView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView gv = sender as GridView;

			if (e.RowHandle < 0) return;

			string mName = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "IsSubTotal"));

			if (mName == "1")//1:거래처소계, 2:합계, 3: 일자별 소계
			{
				e.Appearance.BackColor = Color.LemonChiffon;
				e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
				e.HighPriority = true; // 스타일 강제 적용
			}

			else if (mName == "2")
            {
                e.Appearance.BackColor = Color.LightSalmon;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.HighPriority = true; // 스타일 강제 적용
            }
            else if (mName == "3")
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.HighPriority = true; // 스타일 강제 적용
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            this.Validate();

            string startDt = barDate.smtStartDateTimeValue.ToString("d");
            string endDt = barDate.smtEndDateTimeValue.ToString("d");
            string groupType = CboSearchType.smtComboBoxValue.ConvertString_Null_To_Empty();
            string custCode = LupCustomer.smtLookUpValue.ConvertString_Null_To_Empty();
            string ItemCode = LupItem.smtLookUpValue.ConvertString_Null_To_Empty();

            if (groupType == "년") groupType = "Y";
            else if (groupType == "월") groupType = "M";
            else if (groupType == "일") groupType = "D";

            string query = $@"
DECLARE @CorpId       VARCHAR(4)   = '{WrGlobal.CorpID}'
      , @StartDate    DATE        = '{startDt}'
      , @EndDate      DATE        = '{endDt}'
      , @GroupType    CHAR(1)     = '{groupType}'      -- Y / M / D
      , @CustCode     VARCHAR(20) = '{custCode}'
      , @ItemCode VARCHAR(20) = '{ItemCode}';

;WITH Base AS (
    SELECT
          pm.BillToCode AS CustCode
        , cur.CustName
        , pd.ItemCode AS ItemCode
        , cm.ItemName
        , CASE @GroupType
            WHEN 'Y' THEN CONVERT(VARCHAR(4),  pm.SalesDate, 120)
            WHEN 'M' THEN CONVERT(VARCHAR(7),  pm.SalesDate, 120)
            WHEN 'D' THEN CONVERT(VARCHAR(10), pm.SalesDate, 120)
          END AS GKey
        , pd.Qty
        , pd.TotalPrice
    FROM BM_SalesM pm
    JOIN BM_SalesD pd ON pm.CorpId = pd.CorpId AND pm.SalesNo = pd.SalesNo
    LEFT JOIN CM_Cust cur ON cur.CorpId = pm.CorpId AND cur.CustCode = pm.BillToCode
    LEFT JOIN CM_Item cm ON cm.CorpId = pd.CorpId AND cm.ItemCode = pd.ItemCode
    WHERE pm.CorpId = @CorpId
      AND pm.SalesDate BETWEEN @StartDate AND @EndDate
	  AND pm.OutType = '10' --출하출고
      AND (@CustCode     ='ALL' OR pm.BillToCode     = @CustCode)
      AND (@ItemCode ='ALL' OR pd.ItemCode = @ItemCode)
),
AllRows AS (
    ----------------------------------------------------  
    -- ① 상세   
    ----------------------------------------------------
    SELECT
          CustCode
        , CustName
        , ItemCode
        , ItemName
        , GKey
        , SUM(Qty) AS Qty
        , SUM(TotalPrice) AS Price
        , 0 AS SortType         -- 상세
    FROM Base
    GROUP BY CustCode, CustName, ItemCode, ItemName, GKey

    UNION ALL

    ----------------------------------------------------  
    -- ② 거래처별 소계  
    ----------------------------------------------------
    SELECT
          CustCode
        , CustName
        , NULL
        , '소계'
        , NULL
        , SUM(Qty)
        , SUM(TotalPrice)
        , 1 AS SortType         -- Cust 소계
    FROM Base
    GROUP BY CustCode, CustName

    UNION ALL

    ----------------------------------------------------  
    -- ③ GKey별 소계 (전체 거래처 기준)
    ----------------------------------------------------
    SELECT
          NULL
        , NULL
        , NULL
        , GKey + ' 소계'
        , GKey
        , SUM(Qty)
        , SUM(TotalPrice)
        , 3 AS SortType         -- GKey 소계
    FROM Base
    GROUP BY GKey

    UNION ALL

    ----------------------------------------------------  
    -- ④ 전체 합계  
    ----------------------------------------------------
    SELECT
          NULL
        , NULL
        , NULL
        , '합계'
        , NULL
        , SUM(Qty)
        , SUM(TotalPrice)
        , 2 AS SortType         -- 전체 합계
    FROM Base
)

SELECT
      CustName
    , ItemCode
    , ItemName
    , GKey
    , Qty
    , Price
    , SortType AS IsSubTotal
FROM AllRows
ORDER BY
      CASE WHEN SortType = 2 THEN 9 ELSE SortType END,  
      GKey,
      CustCode,
      ItemCode;





";

            Gc_Purchase.DataSource = db.GetDataView("매출", query);

            Gv_Purchase.BestFitColumns();

            decimal total = 0;

            for (int i = 0; i < Gv_Purchase.RowCount; i++)
            {
                var sortTypeObj = Gv_Purchase.GetRowCellValue(i, "IsSubTotal");
                if (sortTypeObj == null) continue;

                int sortType = Convert.ToInt32(sortTypeObj);

                // 0 = 상세만 포함
                if (sortType == 0)
                {
                    var v = Gv_Purchase.GetRowCellValue(i, "Price");
                    if (v != null && v != DBNull.Value)
                    {
                        total += Convert.ToDecimal(v);
                    }
                }
            }


            lbl_SummarySum.Text = total.ToString("#,#");
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_Purchase.ExportToXls(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}