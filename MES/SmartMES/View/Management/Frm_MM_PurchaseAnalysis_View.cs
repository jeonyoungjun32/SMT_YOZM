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
//using SmartMES.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
//using SmartMES.Model.Basic;
//using SmartMES.Model.SystemCode;
//using SmartMES.Model.Management;
//using SmartMES.Model;
using DevExpress.XtraGrid;
using SmartMES.Common;
using SmartCommon.Common;
using SmartMES.Models;

namespace SmartMES.View.Management
{
    public partial class Frm_MM_PurchaseAnalysis_View : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        string FormType = ""; // 폼로드명 매입 purchase 매출 Sales
        Database db = new Database();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        string ItemType = "40,50,60,70, 80, 90";

        public Frm_MM_PurchaseAnalysis_View()
        {
            InitializeComponent();
        }
        public Frm_MM_PurchaseAnalysis_View(string formType)
        {
            InitializeComponent();
            this.FormType = formType;
        }
        private void Frm_MM_PurchaseAnalysis_View_Load(object sender, EventArgs e)
        {
            barDate.smtStartDateTimeValue = DateTime.Now;

            Set_SearchLup();
            Set_Grid(Gv_Purchase);

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

            //품목분류 1
            code.Class = "A01210";
            LupItemClass1.smtLookUpDataSource = code.GetCM_CateItemALL(WrGlobal.CorpID, "A01210", new string[] { ItemType }, true);
            LupItemClass1.smtLookUpDataDisplayMember = "명칭";
            LupItemClass1.smtLookUpDataValueMember = "코드";
            LupItemClass1.smtLookUpValue = "ALL";
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            this.Validate();

            string startDt = barDate.smtStartDateTimeValue.ToString("yyyy-MM-01");
            string endDt = Convert.ToDateTime(startDt).AddMonths(1).AddDays(-1).ToString("d");

            string query = $@"
SELECT 

 cur.CustName
, cm.ItemCode
, cm.ItemName
, cm.Alias
, cm.ItemClass1
, cm.ItemClass2
, cm.ItemClass3
, cm.Size
, cm.UnitWeight
, cm.Texture
, cm.Type
, cm.UnitCode
, cm.ColorCode
 
, AA.*
FROM(
		SELECT 
				AL.CustCode,
				AL.ItemCode,

				(
					ISNULL(SUM([01_QTY]),0) +
					ISNULL(SUM([02_QTY]),0) +
					ISNULL(SUM([03_QTY]),0) +
					ISNULL(SUM([04_QTY]),0) +
					ISNULL(SUM([05_QTY]),0) +
					ISNULL(SUM([06_QTY]),0) +
					ISNULL(SUM([07_QTY]),0) +
					ISNULL(SUM([08_QTY]),0) +
					ISNULL(SUM([09_QTY]),0) +
					ISNULL(SUM([10_QTY]),0) +
					ISNULL(SUM([11_QTY]),0) +
					ISNULL(SUM([12_QTY]),0) +
					ISNULL(SUM([13_QTY]),0) +
					ISNULL(SUM([14_QTY]),0) +
					ISNULL(SUM([15_QTY]),0) +
					ISNULL(SUM([16_QTY]),0) +
					ISNULL(SUM([17_QTY]),0) +
					ISNULL(SUM([18_QTY]),0) +
					ISNULL(SUM([19_QTY]),0) +
					ISNULL(SUM([20_QTY]),0) +
					ISNULL(SUM([21_QTY]),0) +
					ISNULL(SUM([22_QTY]),0) +
					ISNULL(SUM([23_QTY]),0) +
					ISNULL(SUM([24_QTY]),0) +
					ISNULL(SUM([25_QTY]),0) +
					ISNULL(SUM([26_QTY]),0) +
					ISNULL(SUM([27_QTY]),0) +
					ISNULL(SUM([28_QTY]),0) +
					ISNULL(SUM([29_QTY]),0) +
					ISNULL(SUM([30_QTY]),0) +
					ISNULL(SUM([31_QTY]),0)
				) AS TOTAL_QTY,

				(
					ISNULL(SUM([Price_01]),0) +
					ISNULL(SUM([Price_02]),0) +
					ISNULL(SUM([Price_03]),0) +
					ISNULL(SUM([Price_04]),0) +
					ISNULL(SUM([Price_05]),0) +
					ISNULL(SUM([Price_06]),0) +
					ISNULL(SUM([Price_07]),0) +
					ISNULL(SUM([Price_08]),0) +
					ISNULL(SUM([Price_09]),0) +
					ISNULL(SUM([Price_10]),0) +
					ISNULL(SUM([Price_11]),0) +
					ISNULL(SUM([Price_12]),0) +
					ISNULL(SUM([Price_13]),0) +
					ISNULL(SUM([Price_14]),0) +
					ISNULL(SUM([Price_15]),0) +
					ISNULL(SUM([Price_16]),0) +
					ISNULL(SUM([Price_17]),0) +
					ISNULL(SUM([Price_18]),0) +
					ISNULL(SUM([Price_19]),0) +
					ISNULL(SUM([Price_20]),0) +
					ISNULL(SUM([Price_21]),0) +
					ISNULL(SUM([Price_22]),0) +
					ISNULL(SUM([Price_23]),0) +
					ISNULL(SUM([Price_24]),0) +
					ISNULL(SUM([Price_25]),0) +
					ISNULL(SUM([Price_26]),0) +
					ISNULL(SUM([Price_27]),0) +
					ISNULL(SUM([Price_28]),0) +
					ISNULL(SUM([Price_29]),0) +
					ISNULL(SUM([Price_30]),0) +
					ISNULL(SUM([Price_31]),0)
				) AS TOTAL_PRICE,

				ISNULL(SUM([01_QTY]	),0) AS [01_QTY],
				ISNULL(SUM([02_QTY]	),0) AS [02_QTY],
				ISNULL(SUM([03_QTY]	),0) AS	[03_QTY],
				ISNULL(SUM([04_QTY]	),0) AS	[04_QTY],
				ISNULL(SUM([05_QTY]	),0) AS	[05_QTY],
				ISNULL(SUM([06_QTY]	),0) AS	[06_QTY],
				ISNULL(SUM([07_QTY]	),0) AS	[07_QTY],
				ISNULL(SUM([08_QTY]	),0) AS	[08_QTY],
				ISNULL(SUM([09_QTY]	),0) AS	[09_QTY],
				ISNULL(SUM([10_QTY]	),0) AS	[10_QTY],
				ISNULL(SUM([11_QTY]	),0) AS	[11_QTY],
				ISNULL(SUM([12_QTY]	),0) AS	[12_QTY],
				ISNULL(SUM([13_QTY]	),0) AS	[13_QTY],
				ISNULL(SUM([14_QTY]	),0) AS	[14_QTY],
				ISNULL(SUM([15_QTY]	),0) AS	[15_QTY],
				ISNULL(SUM([16_QTY]	),0) AS	[16_QTY],
				ISNULL(SUM([17_QTY]	),0) AS	[17_QTY],
				ISNULL(SUM([18_QTY]	),0) AS	[18_QTY],
				ISNULL(SUM([19_QTY]	),0) AS	[19_QTY],
				ISNULL(SUM([20_QTY]	),0) AS	[20_QTY],
				ISNULL(SUM([21_QTY]	),0) AS	[21_QTY],
				ISNULL(SUM([22_QTY]	),0) AS	[22_QTY],
				ISNULL(SUM([23_QTY]	),0) AS	[23_QTY],
				ISNULL(SUM([24_QTY]	),0) AS	[24_QTY],
				ISNULL(SUM([25_QTY]	),0) AS	[25_QTY],
				ISNULL(SUM([26_QTY]	),0) AS	[26_QTY],
				ISNULL(SUM([27_QTY]	),0) AS	[27_QTY],
				ISNULL(SUM([28_QTY]	),0) AS	[28_QTY],
				ISNULL(SUM([29_QTY]	),0) AS	[29_QTY],
				ISNULL(SUM([30_QTY]	),0) AS	[30_QTY],
				ISNULL(SUM([31_QTY]	),0) AS	[31_QTY],

				ISNULL(SUM([Price_01]	),0) AS [Price_01],
				ISNULL(SUM([Price_02]	),0) AS [Price_02],
				ISNULL(SUM([Price_03]	),0) AS	[Price_03],
				ISNULL(SUM([Price_04]	),0) AS	[Price_04],
				ISNULL(SUM([Price_05]	),0) AS	[Price_05],
				ISNULL(SUM([Price_06]	),0) AS	[Price_06],
				ISNULL(SUM([Price_07]	),0) AS	[Price_07],
				ISNULL(SUM([Price_08]	),0) AS	[Price_08],
				ISNULL(SUM([Price_09]	),0) AS	[Price_09],
				ISNULL(SUM([Price_10]	),0) AS	[Price_10],
				ISNULL(SUM([Price_11]	),0) AS	[Price_11],
				ISNULL(SUM([Price_12]	),0) AS	[Price_12],
				ISNULL(SUM([Price_13]	),0) AS	[Price_13],
				ISNULL(SUM([Price_14]	),0) AS	[Price_14],
				ISNULL(SUM([Price_15]	),0) AS	[Price_15],
				ISNULL(SUM([Price_16]	),0) AS	[Price_16],
				ISNULL(SUM([Price_17]	),0) AS	[Price_17],
				ISNULL(SUM([Price_18]	),0) AS	[Price_18],
				ISNULL(SUM([Price_19]	),0) AS	[Price_19],
				ISNULL(SUM([Price_20]	),0) AS	[Price_20],
				ISNULL(SUM([Price_21]	),0) AS	[Price_21],
				ISNULL(SUM([Price_22]	),0) AS	[Price_22],
				ISNULL(SUM([Price_23]	),0) AS	[Price_23],
				ISNULL(SUM([Price_24]	),0) AS	[Price_24],
				ISNULL(SUM([Price_25]	),0) AS	[Price_25],
				ISNULL(SUM([Price_26]	),0) AS	[Price_26],
				ISNULL(SUM([Price_27]	),0) AS	[Price_27],
				ISNULL(SUM([Price_28]	),0) AS	[Price_28],
				ISNULL(SUM([Price_29]	),0) AS	[Price_29],
				ISNULL(SUM([Price_30]	),0) AS	[Price_30],
				ISNULL(SUM([Price_31]	),0) AS	[Price_31]
				FROM 
				(
					SELECT 
					pm.CustCode,
					pd.ItemCode,
					case when DAY(pm.StoDate) = 	1	then pd.Qty else 0 end as   [01_QTY],
					case when DAY(pm.StoDate) = 	2	then pd.Qty else 0 end as   [02_QTY],
					case when DAY(pm.StoDate) = 	3	then pd.Qty else 0 end as	[03_QTY],
					case when DAY(pm.StoDate) = 	4	then pd.Qty else 0 end as	[04_QTY],
					case when DAY(pm.StoDate) = 	5	then pd.Qty else 0 end as	[05_QTY],
					case when DAY(pm.StoDate) = 	6	then pd.Qty else 0 end as	[06_QTY],
					case when DAY(pm.StoDate) = 	7	then pd.Qty else 0 end as	[07_QTY],
					case when DAY(pm.StoDate) = 	8	then pd.Qty else 0 end as	[08_QTY],
					case when DAY(pm.StoDate) = 	9	then pd.Qty else 0 end as	[09_QTY],
					case when DAY(pm.StoDate) = 	10	then pd.Qty else 0 end as	[10_QTY],
					case when DAY(pm.StoDate) = 	11	then pd.Qty else 0 end as	[11_QTY],
					case when DAY(pm.StoDate) = 	12	then pd.Qty else 0 end as	[12_QTY],
					case when DAY(pm.StoDate) = 	13	then pd.Qty else 0 end as	[13_QTY],
					case when DAY(pm.StoDate) = 	14	then pd.Qty else 0 end as	[14_QTY],
					case when DAY(pm.StoDate) = 	15	then pd.Qty else 0 end as	[15_QTY],
					case when DAY(pm.StoDate) = 	16	then pd.Qty else 0 end as	[16_QTY],
					case when DAY(pm.StoDate) = 	17	then pd.Qty else 0 end as	[17_QTY],
					case when DAY(pm.StoDate) = 	18	then pd.Qty else 0 end as	[18_QTY],
					case when DAY(pm.StoDate) = 	19	then pd.Qty else 0 end as	[19_QTY],
					case when DAY(pm.StoDate) = 	20	then pd.Qty else 0 end as	[20_QTY],
					case when DAY(pm.StoDate) = 	21	then pd.Qty else 0 end as	[21_QTY],
					case when DAY(pm.StoDate) = 	22	then pd.Qty else 0 end as	[22_QTY],
					case when DAY(pm.StoDate) = 	23	then pd.Qty else 0 end as	[23_QTY],
					case when DAY(pm.StoDate) = 	24	then pd.Qty else 0 end as	[24_QTY],
					case when DAY(pm.StoDate) = 	25	then pd.Qty else 0 end as	[25_QTY],
					case when DAY(pm.StoDate) = 	26	then pd.Qty else 0 end as	[26_QTY],
					case when DAY(pm.StoDate) = 	27	then pd.Qty else 0 end as	[27_QTY],
					case when DAY(pm.StoDate) = 	28	then pd.Qty else 0 end as	[28_QTY],
					case when DAY(pm.StoDate) = 	29	then pd.Qty else 0 end as	[29_QTY],
					case when DAY(pm.StoDate) = 	30	then pd.Qty else 0 end as	[30_QTY],
					case when DAY(pm.StoDate) = 	31	then pd.Qty else 0 end as	[31_QTY],
					case when DAY(pm.StoDate) = 	1	then pd.TotalPrice else 0 end as 	[Price_01],
					case when DAY(pm.StoDate) = 	2	then pd.TotalPrice else 0 end as 	[Price_02],
					case when DAY(pm.StoDate) = 	3	then pd.TotalPrice else 0 end as	[Price_03],
					case when DAY(pm.StoDate) = 	4	then pd.TotalPrice else 0 end as	[Price_04],
					case when DAY(pm.StoDate) = 	5	then pd.TotalPrice else 0 end as	[Price_05],
					case when DAY(pm.StoDate) = 	6	then pd.TotalPrice else 0 end as	[Price_06],
					case when DAY(pm.StoDate) = 	7	then pd.TotalPrice else 0 end as	[Price_07],
					case when DAY(pm.StoDate) = 	8	then pd.TotalPrice else 0 end as	[Price_08],
					case when DAY(pm.StoDate) = 	9	then pd.TotalPrice else 0 end as	[Price_09],
					case when DAY(pm.StoDate) = 	10	then pd.TotalPrice else 0 end as	[Price_10],
					case when DAY(pm.StoDate) = 	11	then pd.TotalPrice else 0 end as	[Price_11],
					case when DAY(pm.StoDate) = 	12	then pd.TotalPrice else 0 end as	[Price_12],
					case when DAY(pm.StoDate) = 	13	then pd.TotalPrice else 0 end as	[Price_13],
					case when DAY(pm.StoDate) = 	14	then pd.TotalPrice else 0 end as	[Price_14],
					case when DAY(pm.StoDate) = 	15	then pd.TotalPrice else 0 end as	[Price_15],
					case when DAY(pm.StoDate) = 	16	then pd.TotalPrice else 0 end as	[Price_16],
					case when DAY(pm.StoDate) = 	17	then pd.TotalPrice else 0 end as	[Price_17],
					case when DAY(pm.StoDate) = 	18	then pd.TotalPrice else 0 end as	[Price_18],
					case when DAY(pm.StoDate) = 	19	then pd.TotalPrice else 0 end as	[Price_19],
					case when DAY(pm.StoDate) = 	20	then pd.TotalPrice else 0 end as	[Price_20],
					case when DAY(pm.StoDate) = 	21	then pd.TotalPrice else 0 end as	[Price_21],
					case when DAY(pm.StoDate) = 	22	then pd.TotalPrice else 0 end as	[Price_22],
					case when DAY(pm.StoDate) = 	23	then pd.TotalPrice else 0 end as	[Price_23],
					case when DAY(pm.StoDate) = 	24	then pd.TotalPrice else 0 end as	[Price_24],
					case when DAY(pm.StoDate) = 	25	then pd.TotalPrice else 0 end as	[Price_25],
					case when DAY(pm.StoDate) = 	26	then pd.TotalPrice else 0 end as	[Price_26],
					case when DAY(pm.StoDate) = 	27	then pd.TotalPrice else 0 end as	[Price_27],
					case when DAY(pm.StoDate) = 	28	then pd.TotalPrice else 0 end as	[Price_28],
					case when DAY(pm.StoDate) = 	29	then pd.TotalPrice else 0 end as	[Price_29],
					case when DAY(pm.StoDate) = 	30	then pd.TotalPrice else 0 end as	[Price_30],
					case when DAY(pm.StoDate) = 	31	then pd.TotalPrice else 0 end as	[Price_31]
					FROM 
					MM_PurchaseM pm
					left join 
					MM_PurchaseD pd
					on pm.CorpId = pd.CorpId 
					and pm.StoNo = pd.StoNo
							
								WHERE pm.CorpId = '{WrGlobal.CorpID}'
                                and CONVERT(VARCHAR(7),pm.StoDate,120) = '{((DateTime)barDate.smtStartDateTimeValue).ToString("yyyy-MM")}'
                                and pm.StoDate BETWEEN '{startDt}' AND '{endDt}'
								and ('{LupCustomer.smtLookUpValue}' ='ALL' or  pm.CustCode = '{LupCustomer.smtLookUpValue}')
								and ('{LupItem.smtLookUpValue}' ='ALL' or ItemCode = '{LupItem.smtLookUpValue}')
							) AL
				GROUP BY AL.CustCode, al.ItemCode
				) as AA
				left join CM_Cust cur on cur.CorpId = '{WrGlobal.CorpID}' AND AA.CustCode = cur.CustCode
				left join CM_Item cm on cm.CorpId = '{WrGlobal.CorpID}' AND AA.ItemCode = cm.ItemCode
                WHERE 1=1 ";

            if (LupItemClass1.smtLookUpValue.ConvertString_Null_To_Empty() != "ALL")
            {
                query += $"\n AND cm.ItemClass1 = '{LupItemClass1.smtLookUpValue}' ";
            }

            Gc_Purchase.DataSource = db.GetDataView("월별 매입 ", query);

            //매입총액
            lbl_SummarySum.Text = SummarySum();

            //Gv_Purchase.BestFitColumns();
            Gv_Purchase.Columns["ItemName"].BestFit();
        }

        #region Band 생성/ gv추가 시작
        private void Create_Band(BandedGridView gv, string name, string caption, string parentBand = null)
        {
            GridBand gridBand = new GridBand();
            gridBand.Name = name; //밴드명
            gridBand.Caption = caption; //화면에보이는 밴드 텍스트
            gv.Bands.Add(gridBand); //gv에 밴드 넣기

            if (parentBand != null) // 부모밴드 매개변수가 있으면 해당 밴드 자식밴드로 추가
            {
                gv.Bands[parentBand].Children.Add(gridBand);
            }

            gridBand.MinWidth = 100;
            gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        #endregion Band생성/gv추가 끝

        #region Column 생성/ gv추가 시작
        private void Create_BandedColumn(BandedGridView gv, string name, string caption, string fieldName, string parentBandNm = null, string childBandNm = null)
        {
            BandedGridColumn bandCol = new BandedGridColumn();
            bandCol.Name = name;
            bandCol.Caption = caption;
            bandCol.FieldName = fieldName;
            bandCol.Visible = true;
            gv.Columns.Add(bandCol);
            if (parentBandNm != null && childBandNm != null)
            { // 자식밴드 안에 넣을 때 
                gv.Bands[parentBandNm].Children[childBandNm].Columns.Add(bandCol);
            }
            else if (parentBandNm != null)
            { // 단일 밴드에 넣을 때
                gv.Bands[parentBandNm].Columns.Add(bandCol);
            }
            else
            {
                return;
            }
        }
        #endregion Column 생성/gv추가 끝

        private void Set_Grid(BandedGridView gv)
        {
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            //밴드 생성
            Create_Band(gv, "bandCustNm", "거래처명");
            Create_Band(gv, "bandItemClass", "품목분류1");
            Create_Band(gv, "bandItemName", "품명");

			Create_Band(gv, "bandTotal", "합계");
			Create_Band(gv, "TOTAL_QTY", "수량", "bandTotal");
			Create_Band(gv, "TOTAL_PRICE", "금액", "bandTotal");

			for (int i = 1; i <= 31; i++)
            {
                Create_Band(gv, i.ToString("D2") + "Day", i.ToString("D2") + "일");
            }

            // n일 - 수량/금액 자식 밴드 생성 + 추가
            for (int i = 1; i <= 31; i++)
            {
                Create_Band(gv, "QTY_" + i.ToString("D2") , "수량", i.ToString("D2") + "Day");
                Create_Band(gv, "Price" + i.ToString("D2"), "금액", i.ToString("D2") + "Day");
            }

            //컬럼 생성 & 셋팅
            for (int i = 1; i <= 31; i++)
            {
                Create_BandedColumn(gv, "Qty_" + i.ToString("D2"), "수량", i.ToString("D2") + "_QTY"  , i.ToString("D2") + "Day", "QTY_" + i.ToString("D2"));
                Create_BandedColumn(gv, "Amt_" + i.ToString("D2"), "금액", "Price_" + i.ToString("D2"), i.ToString("D2") + "Day", "Price" + i.ToString("D2"));

                //정렬
                grid.SetCol_Type(gv.Columns[i.ToString("D2") + "_QTY"], clsGrid.TypeList.TFloat);
                grid.SetCol_Type(gv.Columns["Price_" + i.ToString("D2")], clsGrid.TypeList.TFloat);
                gv.Columns["Price_" + i.ToString("D2")].AppearanceCell.FontStyleDelta = FontStyle.Bold;
            }

			Create_BandedColumn(gv, "colCustNm", "거래처명", "CustName", "bandCustNm");
            Create_BandedColumn(gv, "colItemClass", "품목분류1", "ItemClass1", "bandItemClass");
            Create_BandedColumn(gv, "colItemName", "품명", "ItemName", "bandItemName");
			Create_BandedColumn(gv, "colQty", "수량", "TOTAL_QTY", "bandTotal", "TOTAL_QTY");
			Create_BandedColumn(gv, "colPrice", "금액", "TOTAL_PRICE", "bandTotal", "TOTAL_PRICE");

			grid.SetCol_Type(gv.Columns["TOTAL_QTY"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["TOTAL_PRICE"], clsGrid.TypeList.TFloat);

			gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;

            //Summary
            for (int i = 1; i <= 31; i++)
            {
                string formattedIndex = i.ToString("D2");
                gv.Columns["Price_"+ formattedIndex].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
                gv.Columns["Price_" + formattedIndex].SummaryItem.DisplayFormat= "{0:N0}";
            }

            gv.OptionsView.ShowFooter = true;            
            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowColumnHeaders = false; // 컬럼 부분 보이지 않게 설정 (이렇게 해야 제품코드 초기화부분 컬럼 하나로 나올 수 있음)
            gv.OptionsBehavior.Editable = false; // 데이터 수정 불가 처리

			//컬럼 하단 합계
			grid.SetCol_Summary(gv, "TOTAL_QTY");
			grid.SetCol_Summary(gv, "TOTAL_PRICE");

			//밴드 고정
			gv.Bands["bandCustNm"].Fixed = FixedStyle.Left;
			if (gv.Bands["bandItemClass"].Visible) { gv.Bands["bandItemClass"].Fixed = FixedStyle.Left; }
			gv.Bands["bandItemName"].Fixed = FixedStyle.Left;
			gv.Bands["bandTotal"].Fixed = FixedStyle.Left;

            #region 거래처로 묶어서 보여달라고 하면 밑에꺼 쓰면 됨
			/*
            //컬럼 그룹
            gv.ClearGrouping();
			gv.ClearSorting();
			gv.Columns["CustName"].GroupIndex = 0;
			gv.OptionsBehavior.AutoExpandAllGroups = true;

			//그룹 명칭 숨겨놓기
			gv.OptionsView.ShowGroupedColumns = false;
			gv.GroupFormat = "";
			gv.GroupRowHeight = 1;

			//그룹 확장하기
			gv.ExpandAllGroups();
			gv.BestFitColumns();

			//그룹 하단 합계
			grid.SetGroup_Summary(gv, "TOTAL_QTY");
			grid.SetGroup_Summary(gv, "TOTAL_PRICE");
			*/
			
            #endregion
        }
        private string SummarySum()
        {
            //총 매입매출 구하기 (하단 summary 합계)
            decimal Sum = 0;
            for (int i = 1; i <= 31; i++)
            {
                string formattedIndex = i.ToString("D2");
                decimal summary = Gv_Purchase.Columns["Price_" + formattedIndex].SummaryItem.SummaryValue.ConvertDecimal_Null_To_Zero();

                Sum += summary;
            }
            return Sum.ToString("#,##0") + " 원";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void LupItemClass1_smtLookUpEditValueChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}