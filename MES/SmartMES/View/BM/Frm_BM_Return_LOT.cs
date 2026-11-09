using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SmartMES.Models.ST;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_Return_LOT : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        int lastRowHandle = -1;

        public Frm_BM_Return_LOT()
        {
            InitializeComponent();
        }

        private void frmSalesView_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            //출고처
            Lup_Cust.smtLookUpDataSource = code.GetCustomer_Lup(WrGlobal.CorpID, true);
            Lup_Cust.smtLookUpDataDisplayMember = "내용";
            Lup_Cust.smtLookUpDataValueMember = "코드";
            Lup_Cust.smtLookUpValue = "ALL";

            //품목
            CM_Item cm = new CM_Item();
            Lup_Item.smtLookUpDataSource = cm.Select_MaterialList_All(true);
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";

            Set_gvSales(gvSales);
            Set_gvSReturn(gvReturn);
            SetGridView_BM_SalesD(gvBM_SalesD);

            //권한설정
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정

            //프로세스 옵션 : 반품 시 자동 입고 처리 여부 Y:반품 시 바로 입고 처리, N: 반품 등록 후 입고 등록 따로 처리
            if (WrGlobal.ProcOption["BN"]["Return_AutoReceive_YN"] == "Y")
			{
                cmdReceiving.Visible = false;
			}

            DisplayData();
        }

        private void Set_gvSales(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //품목 단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");

            //품목분류
            RepositoryItemSearchLookUpEdit lup_Class1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", false), "코드", "명칭");

            //Column 셋팅
            grid.Create_GvColumn(gv, "출고번호", "SalesNo");
            grid.Create_GvColumn(gv, "출고순번", "SalesSeq");
            grid.Create_GvColumn(gv, "출고처", "ShipToName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "출고량", "Qty");
            grid.Create_GvColumn(gv, "총반품량", "TotalQty");
            grid.Create_GvColumn(gv, "주문번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "주문순번", "SdOrderSeq");
            
            grid.Create_GvColumn(gv, "반품일자", "ReturnDate");
            grid.Create_GvColumn(gv, "반품량", "ReturnQty");
            grid.Create_GvColumn(gv, "반품금액", "Price");
            grid.Create_GvColumn(gv, "반품사유", "Reason");
            grid.Create_GvColumn(gv, "재고반영", "StockYn", rpo.Create_rpoChk());

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class3;

            //셀값 수정
            grid.Column_AllowEdit(gv.Columns["ReturnDate"]);    
            //grid.Column_AllowEdit(gv.Columns["ReturnQty"]);    
            grid.Column_AllowEdit(gv.Columns["Price"]);    
            grid.Column_AllowEdit(gv.Columns["Reason"]);
            grid.Column_AllowEdit(gv.Columns["StockYn"]);

            //
            gv.Columns["StockYn"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["Reason"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["Price"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["ReturnQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["ReturnDate"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            // 천 단위
            grid.SetCol_Type(gv.Columns["ReturnDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ReturnQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);

            gv.ValidatingEditor += Gv_ValidatingEditor;
            gv.BestFitColumns();
        }

        private void Set_gvSReturn(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            //Column 셋팅
            grid.Create_GvColumn(gv, "반품번호", "ReturnNo");
            grid.Create_GvColumn(gv, "구분", "Title");
            grid.Create_GvColumn(gv, "반품사유", "Reason");
            grid.Create_GvColumn(gv, "반품일자", "ReturnDate");
            grid.Create_GvColumn(gv, "반품량", "Qty");
            grid.Create_GvColumn(gv, "반품금액", "Price");
            grid.Create_GvColumn(gv, "입고일자", "InDate");
            grid.Create_GvColumn(gv, "입고구분", "IsInStock");
            grid.Create_GvColumn(gv, "입고번호", "InOutNo");
            grid.Create_GvColumn(gv, "입고순번", "Seq");

            //셀값 수정
            grid.Column_AllowEdit(gv.Columns["InDate"]);

            //프로세스 : 반품 시 자동 입고 처리 여부
            if (WrGlobal.ProcOption["BN"]["Return_AutoReceive_YN"] == "Y")
            {
                grid.Column_AllowEdit(gv.Columns["InDate"], false);
                //gv.OptionsSelection.MultiSelect = false;
            }

            gv.Columns["InOutNo"].Visible = false;
            gv.Columns["Seq"].Visible = false;

            // 천 단위
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);

            gv.RowStyle += Gv_RowStyle;         // 입고된 행은 색상 표시
            gv.BestFitColumns();
        }

        private void SetGridView_BM_SalesD(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;
            //gv.OptionsBehavior.ReadOnly = true;

            RepositoryItemSearchLookUpEdit lup_WareHouse = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "출고창고", "HouseCode", lup_WareHouse);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "LotNo", "LotNo");
            grid.Create_GvColumn(gv, "재고수량", "Qty", rpo.Create_rpoTxt_Numeric()); 
            grid.Create_GvColumn(gv, "반품수량", "ReturnQty", rpo.Create_rpoTxt_Numeric());

            grid.Column_AllowEdit(gv.Columns["ReturnQty"]);

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ReturnQty"], clsGrid.TypeList.TFloat);

            //컬럼 위치 설정
            gv.Columns["ReturnQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            grid.SetCol_Summary(gv, "Qty");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += GvStock_CellValueChanged;
            grid.Set_Navigator(gv);
        }

        private void Gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            //e.Appearance.BackColor = Color.Yellow;
            GridView gv = sender as GridView;

            //// 컬럼이 있는지 확인
            //if (gv.Columns["IsInStock"] == null)
            //{
            //    MessageBox.Show("IsInStock 컬럼이 없어요!");
            //    return;
            //}

            if (gv == null || e.RowHandle < 0) return;

            // 상태값 확인
            string state = gv.GetRowCellValue(e.RowHandle, gv.Columns["IsInStock"]).ConvertString_Null_To_Empty();

            // 입고상태면 행 색상 변경
            if (state == "입고")
            {
                e.Appearance.BackColor = Color.PeachPuff;
            }
        }

        private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
        }

        private void GvStock_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            //재고리스트의 반품수량 입력시 출고리스트의 반품수량에 합계 수량 반영
            if (fieldName == "ReturnQty")
            {
                decimal sum = 0;

                for (int i = 0; i < gvBM_SalesD.RowCount; i++)
                {
                    object val = gvBM_SalesD.GetRowCellValue(i, "ReturnQty");

                    if (!val.IsNullOrEmpty()) sum += Convert.ToDecimal(val);
                }

                gvSales.SetRowCellValue(gvSales.FocusedRowHandle, "ReturnQty", sum);
            }

            //상태값 수정
            grid.Change_Status(gv, e.RowHandle);
        }

        // 출고 리스트 
        private void DisplayData()
        {
            string startDate = string.Format("{0:yyyy-MM-dd}", Dt_Date.smtStartDateTimeValue);
            string endDate = string.Format("{0:yyyy-MM-dd}", Dt_Date.smtEndDateTimeValue);
            string shipToCode = Lup_Cust.smtLookUpValue.ToString();
            //string RequestID = Lup_User.smtLookUpValue.ToString();
            string ItemCode = Lup_Item.smtLookUpValue.ToString();

            if (shipToCode == "" || ItemCode == "")
            {
                return;
            }

            BM_SalesD sd = new BM_SalesD();
            sd.ItemCode = ItemCode;

            gcSales.DataSource = sd.SelectSalesList_Return(startDate, endDate, shipToCode);
            gvSales.BestFitColumns();
        }

        private void DisplayReturn()
        {
            //반품등록내역 조회
            string salesNo = gvSales.GetFocusedRowCellValue("SalesNo").ConvertString_Null_To_Empty();
            int salesSeq = gvSales.GetFocusedRowCellValue("SalesSeq").ConvertInt_Null_To_Zero();

            BM_Return rt = new BM_Return();
            rt.SalesNo = salesNo;
            rt.SalesSeq = salesSeq;

            gcReturn.DataSource = rt.SelectReturn_BySalesNoSeq();
            gvReturn.BestFitColumns();
        }

        private void DisplayBM_SalesD()
        {
            GridView gv = gcSales.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            ST_StockD std = new ST_StockD();
            std.JobNo = dr["SalesNo"].ConvertString_Null_To_Empty();
            std.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();

            gcBM_SalesD.DataSource = std.SelectBM_SalesD_Return();

            gvBM_SalesD.BestFitColumns();
        }

        private void gvSales_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayBM_SalesD();
            DisplayReturn();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            DisplayData();
            //DisplayReturn();
        }

        private void btn_Return_Click(object sender, EventArgs e)
        {
            //반품등록
            gvSales.PostEditor();
            gvSales.UpdateCurrentRow();
            gvBM_SalesD.PostEditor();
            gvBM_SalesD.UpdateCurrentRow();

          

            int idx = gvSales.FocusedRowHandle;
            if (idx < 0)
            {
                MessageBox.Show("선택된 데이터가 없습니다.");
                return;
            }

            DataRow row = gvSales.GetDataRow(idx);

            int returnQty = row["ReturnQty"].ConvertInt_Null_To_Zero();

            if (returnQty <= 0)
            {
                MessageBox.Show("반품량을 입력해주세요");
                return;
            }

            BM_Return re = new BM_Return();
            re.ReturnNo = "RE" + code.GetAutoNumber("출하취소", DateTime.Now);
            re.Type = "20";
            re.Reason = row["Reason"].ToString();
            re.ReturnDate = row["ReturnDate"].ConvertDate_To_Char10();
            re.ItemCode = row["ItemCode"].ToString();
            re.Qty = row["ReturnQty"].ConvertInt_Null_To_Zero();
            re.SdOrderNo = row["SdOrderNo"].ConvertString_Null_To_Empty();
            re.SdOrderSeq = row["SdOrderSeq"].ConvertInt_Null_To_One();
            re.SalesNo = row["SalesNo"].ToString();
            re.SalesSeq = Convert.ToInt32(row["SalesSeq"]);
            re.Price = row["Price"].ConvertInt_Null_To_Zero();
            re.StockYn = row["StockYn"].ConvertString_Null_To_Empty();
            re.Insert_BM_Return();

            if (WrGlobal.ProcOption["BN"]["Return_AutoReceive_YN"] == "Y")
            {
                if (re.StockYn == "Y")
                {
                    ST_StockM stockM = new ST_StockM();
                    ST_StockD stockD = new ST_StockD();

                    re.InDate = re.ReturnDate;
                    re.Update_BM_Return();

                    // 재고 마스터
                    stockM.InOutNo = "ST" + code.GetAutoNumber("재고", DateTime.Now);
                    stockM.OutDate = re.InDate;
                    stockM.ToHouseCode = "S0002"; //완제품창고
                    stockM.Ins_ST_StockM();

                    stockD.InOutNo = stockM.InOutNo;

                    // 재고 디테일
                    int seq = 1;

                    for (int i = 0; i < gvBM_SalesD.RowCount; i++)
                    {
                        DataRow lotRow = gvBM_SalesD.GetDataRow(i);
                        if (lotRow == null) return;

                        int qty = lotRow["ReturnQty"].ConvertInt_Null_To_Zero();
                        if (qty <= 0) continue;

                        stockD.Seq = seq++;
                        stockD.LotNo = lotRow["LotNo"].ToString();
                        stockD.InOutDate = stockM.OutDate;
                        stockD.Type = "반품";
                        stockD.ItemCode = lotRow["ItemCode"].ToString();
                        stockD.ToHouseCode = "S0002";
                        stockD.JobNo = re.ReturnNo;
                        stockD.Qty = qty;

                        stockD.Ins_ST_StockD();
                    }
                }
            }

            MessageBox.Show("반품등록이 완료되었습니다.");
            DisplayData();
            DisplayReturn();
            DisplayBM_SalesD();
        }

        private void cmdReceiving_Click(object sender, EventArgs e)
        {
            //입고등록
            clsCode cls = new clsCode();

            if (gvReturn.SelectedRowsCount <= 0)
            {
                MessageBox.Show("선택된 데이터가 없습니다.");
                return;
            }

            bool alreadyReceived = false;
            string alreadyReceivedItems = "";

            foreach (int idx in gvReturn.GetSelectedRows())
            {
                DataRow row = gvReturn.GetDataRow(idx);
                string status = row["IsInStock"].ToString();

                if (status == "입고")
                {
                    alreadyReceived = true;
                    alreadyReceivedItems += row["ReturnNo"].ToString() + ", ";
                }

                string inDate = row["InDate"].ConvertString_Null_To_Empty();

                if (string.IsNullOrWhiteSpace(inDate))
                {
                    MessageBox.Show("입고일자를 입력해주세요.");
                    return;
                }
            }

            // 🚫 이미 입고된 항목
            if (alreadyReceived)
            {
                alreadyReceivedItems = alreadyReceivedItems.TrimEnd(',', ' ');
                MessageBox.Show($"이미 입고된 항목이 포함되어 있습니다!\n{alreadyReceivedItems}\n다시 확인해주세요.");
                return;
            }

            // 반품 입고 마스터
            int seq = 1;
            string inOutNo = "ST" + cls.GetAutoNumber("재고", DateTime.Now);

            ST_StockM stockM = new ST_StockM();
            
            stockM.InOutNo = inOutNo;
            stockM.OutDate = DateTime.Now.ToString("yyyy-MM-dd");
            stockM.ToHouseCode = "S0002"; //완제품창고
            stockM.Ins_ST_StockM();

            ST_StockD stockD = new ST_StockD();

            stockD.InOutNo = inOutNo;
            stockD.InOutDate = stockM.OutDate;
            stockD.Type = "반품";
            stockD.ToHouseCode = "S0002";

            // 반품 입고 디테일
            foreach (int idx in gvReturn.GetSelectedRows())
            {
                BM_Return sd = new BM_Return();

                DataRow row = gvReturn.GetDataRow(idx);

                sd.ReturnNo = row["ReturnNo"].ToString();
                sd.InDate = Convert.ToDateTime(row["InDate"]).ToString("yyyy-MM-dd");
                sd.Update_BM_Return(); // 입고일자 업데이트

                stockD.Seq = seq++;
                stockD.ItemCode = row["ItemCode"].ToString();
                stockD.Qty = row["Qty"].ConvertInt_Null_To_Zero();

                stockD.Ins_ST_StockD();
            }

            DisplayReturn();
            MessageBox.Show("입고등록이 완료되었습니다.");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDel_Click(object sender, EventArgs e)
        {
            if (gvReturn.SelectedRowsCount <= 0)
            {
                MessageBox.Show("선택된 데이터가 없습니다.");
                return;
            }

            ST_StockD stockD = new ST_StockD();


            foreach (int idx in gvReturn.GetSelectedRows())
            {
                DataRow row = gvReturn.GetDataRow(idx);

                BM_Return re = new BM_Return();
                re.ReturnNo = row["ReturnNo"].ToString();

                // 반품
                re.Delete_BM_Return();

                // 재고
                stockD.JobNo = re.ReturnNo;
                stockD.DeleteStockMD_JobNo();
            }

            MessageBox.Show("삭제되었습니다.");
            DisplayData();
            DisplayReturn();
            DisplayBM_SalesD();
        }
    }
}