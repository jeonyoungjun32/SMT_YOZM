using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using SmartCommon.Common;
using System;
using DevExpress.XtraGrid.Columns;

namespace SmartMES.View.QM
{
    public partial class Frm_LOTTracking : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsCode code = new clsCode();
        string query = "";
        clsGrid grid = new clsGrid();
        cls_Lookup clsLup = new cls_Lookup();

        public Frm_LOTTracking()
        {
            InitializeComponent();
            SetDesign();
        }

        private void Frm_LOTTracking_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void SetDesign()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Item.smtLookUpDataSource = code.GetMaterialList(new string[] { "10", "20", "30", "40", "50", "60", "70" }, true).ToTable();
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";

            Set_GvIn(Gv_List);
        }

        private void Set_GvIn(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //Column 셋팅
            //grid.Create_GvColumn(gv, "LOT NO", "MatLot_No");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "수주일자", "ObtainDate");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
            grid.Create_GvColumn(gv, "수주수량", "Qty");

            grid.Create_GvColumn(gv, "지시일자", "ProcCmd_Date");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");

            grid.Create_GvColumn(gv, "실적일자", "WorkDate");
            //grid.Create_GvColumn(gv, "실적번호", "Rst_No");
            //grid.Create_GvColumn(gv, "실적순번", "Rst_Row_Seq");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");

            grid.Create_GvColumn(gv, "출하일자", "SalesDate");
            grid.Create_GvColumn(gv, "출하번호", "SalesNo");
            grid.Create_GvColumn(gv, "출하순번", "SalesSeq");
            grid.Create_GvColumn(gv, "출하수량", "OutQty");

            //Repository
            //BS_Customer cust = new BS_Customer();
            //RepositoryItemSearchLookUpEdit lup_Cust_Cd = rpo.Create_RepositoryItemSearchLookUp(cust.Select_Cust_List(CustDiv), "코드", "명칭");

            //SC_Emp emp = new SC_Emp();
            //emp.Prodt_Work_Or = "N";
            //RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(emp.Select_Emp_List(), "코드", "명칭");

            //gv.Columns["Cust_Nm"].ColumnEdit = lup_Cust_Cd;
            //gv.Columns["Item_Nm"].ColumnEdit = lup_ItemCode;

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //컬럼 타입
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);

            grid.RowNumber_Indicator(gv);
        }

        private void Search()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            string ItemCode = Lup_Item.smtLookUpValue.ToString();

            query = $@"
                         	SELECT 
									 A.ItemCode
									, C.ItemName

									-- 수주
									, ObtainDate
									, A.SdOrderNo
									, A.SdOrderSeq
									, A.Qty

									-- 지시
									, F.OrderNo 
									, F.OrderSeq
									, F.OrderQty

									-- 실적
                                    , G.SerialNo
									, G.WorkDate
									, G.GoodQty
									, G.NgQty

									-- 출하
									, I.SalesDate
									, H.SalesNo
									, H.SalesSeq
									, H.Qty OutQty

							FROM BM_SdOrderD A
							LEFT JOIN BM_SdOrderM B ON A.SdOrderNo = B.SdOrderNo 
							LEFT JOIN CM_Item C ON A.ItemCode = C.ItemCode
							LEFT JOIN PM_ProductPlan D ON A.SdOrderNo = D.SdOrderNo AND A.SdOrderSeq = D.SdOrderSeq
							--LEFT JOIN PM_SdRoute E ON A.SdOrderNo = E.SdOrderNo AND A.SdOrderSeq = E.SdOrderSeq
							LEFT JOIN PM_OrderProcess F ON D.OrderNo = F.OrderNo
							LEFT JOIN PM_OrderResult G ON F.OrderNo = G.OrderNo AND F.OrderSeq = G.OrderSeq
							LEFT JOIN BM_SalesD H ON A.SdOrderNo = H.SdOrderNo AND A.SdOrderSeq = H.SdOrderSeq
							LEFT JOIN BM_SalesM I On H.SalesNo = I.SalesNo
                            WHERE ObtainDate BETWEEN '{startDate}' AND '{endDate}'
                            ";

            if (ItemCode != "ALL" && ItemCode != "")
            {
                query += $"\nAND A.ItemCode = '{ItemCode}' ";
            }

            Gc_List.DataSource = db.GetDataTable(query);
            Gv_List.BestFitColumns();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}