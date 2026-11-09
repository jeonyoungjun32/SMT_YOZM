using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.DragEngine;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.MM
{
    public partial class Frm_MM_Placing_NotInList : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsRadioGroup rdo = new clsRadioGroup();
        String query = "";

        string[] ItemType = WrGlobal.Material_ItemType;
        string[] CustType = new string[]{"10","40"};


        public Frm_MM_Placing_NotInList()
        {
            InitializeComponent();
        }

        private void Frm_MM_Placing_NotInList_Load(object sender, EventArgs e)
        {
            Set_Lup();

            Set_Grid(gvNotIn);

            Set_Rdo();

            Search();
        }

        private void Set_Lup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Cust.smtLookUpDataSource = code.Get_CustomerDt(true, CustType);
            Lup_Cust.smtLookUpDataDisplayMember = "내용";
            Lup_Cust.smtLookUpDataValueMember = "코드";
            Lup_Cust.smtLookUpValue = "ALL";

            CM_Item cm = new CM_Item();
            Lup_Item.smtLookUpDataSource = cm.Select_MaterialList_All(true, ItemType);
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";
        }

        private void Set_Grid(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "발주순번", "MmOrderSeq");
            grid.Create_GvColumn(gv, "발주일자", "PlacingDate");
            grid.Create_GvColumn(gv, "납기예정일", "DeliveryDate");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "발주량", "Qty");
            grid.Create_GvColumn(gv, "입고량", "InQty");
            grid.Create_GvColumn(gv, "미입고량", "BalanceQty");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BalanceQty"], clsGrid.TypeList.TFloat);

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			gv.CustomDrawCell += Gv_CustomDrawCell;
            grid.Set_Navigator(gv);
        }

		private void Gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
            if(e.Column.FieldName == "BalanceQty")
			{
                if (gvNotIn.GetRowCellValue(e.RowHandle, "BalanceQty").ConvertFloat_Null_To_Zero() > 0)
				{
                    e.Appearance.ForeColor = Color.Orange;

                    if(DateTime.Now.Date.CompareTo(gvNotIn.GetRowCellValue(e.RowHandle, "DeliveryDate")) == 1)
					{
                        e.Appearance.ForeColor = Color.Red;
					}
                }
            }
		}

		private void Set_Rdo()
        {
            rdo.Create_RadioGroupItem(RdoPurchase, "전체", "ALL");
            rdo.Create_RadioGroupItem(RdoPurchase, "입고진행중", "진행");
            rdo.Create_RadioGroupItem(RdoPurchase, "입고완료", "완료");

            RdoPurchase.SelectedIndexChanged += RdoPurchase_SelectedIndexChanged;

            RdoPurchase.EditValue = "ALL";
        }

        private void Btnearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void RdoPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        // 데이터 조회
        private void Search()
        {
            string startDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            string customer = Lup_Cust.smtLookUpValue.ToString() == "ALL" ? "" : Lup_Cust.smtLookUpValue.ToString();
            string material = Lup_Item.smtLookUpValue.ToString() == "ALL" ? "" : Lup_Item.smtLookUpValue.ToString();
            string state = RdoPurchase.EditValue.ToString() == "ALL" ? "" : RdoPurchase.EditValue.ToString();

            query = $@"SELECT PD.MmOrderNo, PD.MmOrderSeq
                            , PM.PlacingDate, PD.DeliveryDate
                            , PM.CustCode, CC.CustName
                            , PD.ItemCode, CM.ItemName
                            , CM.Alias
                            , CM.ItemClass1
                            , CM.ItemClass2
                            , CM.ItemClass3
                            , CM.Size
                            , CM.UnitWeight
                            , CM.Texture
                            , CM.Type
                            , CM.UnitCode
                            , CM.ColorCode
                            
                            , PD.Qty
                            , ISNULL(pc.InQty, 0) InQty
                            , PD.Qty - ISNULL(pc.InQty, 0) AS BalanceQty

                        FROM MM_PlacingD PD
                        INNER JOIN MM_PlacingM PM ON PD.MmOrderNo = PM.MmOrderNo

                        INNER JOIN CM_Item CM ON PD.ItemCode = cm.ItemCode
                        INNER JOIN CM_Cust CC ON PM.CustCode = CC.CustCode
                        LEFT JOIN 
						(
						    SELECT pc.CorpId, pc.MmOrderNo, pc.MmOrderSeq, SUM(Qty) AS InQty
						    FROM MM_PurchaseD pc
						    GROUP BY pc.CorpId, pc.MmOrderNo, pc.MmOrderSeq
						) AS pc ON pd.CorpId = pc.CorpId AND pd.MmOrderNo = pc.MmOrderNo AND pc.MmOrderSeq = pd.MmOrderSeq

                        WHERE PD.CorpId = '{WrGlobal.CorpID}'
                        AND PM.PlacingDate BETWEEN '{startDt}' AND '{endDt}'
                        AND CM.ItemType IN ('40', '50', '60')       -- 40 : 원자재 , 50 : 부자재, 60 : 소모품
                        ";
            if (customer != "") query += $"\n AND CC.CustCode = '{customer}'";
            if (material != "") query += $"\n AND PD.ItemCode = '{material}'";
            if (state != "") query += $"\n AND PD.State = '{state}'";

            gcNotIn.DataSource = db.GetDataTable(query);
            gvNotIn.BestFitColumns();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvNotIn.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }//end fnction
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}