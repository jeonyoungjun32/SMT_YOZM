using DevExpress.XtraDiagram.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MM;
using SmartMES.View.BM.Popup;
using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace SmartMES.View.MM
{
	public partial class Frm_MM_PlacingD : DevExpress.XtraEditors.XtraForm
    {
        string crud = "";
        string mmOrderNo = "";
        clsCode code = new clsCode();
        string placingType = "";

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_MM_PlacingD()
        {
            InitializeComponent();
        }

        public Frm_MM_PlacingD(string _crud, string _mmOrderNo, string _placingType = "")
        {
            InitializeComponent();

            this.crud = _crud;
            this.mmOrderNo = _mmOrderNo;
            this.placingType = _placingType;

            Set_Lup();
            SetGridView(gvDetail);

            // 마스터용 DataTable 생성
            DataTable dtMaster = new DataTable();
            dtMaster.Columns.Add("CorpId", typeof(string));
            dtMaster.Columns.Add("MmOrderNo", typeof(string));
            dtMaster.Columns.Add("PlacingDate", typeof(DateTime));
            dtMaster.Columns.Add("DeliveryDate", typeof(DateTime));
            dtMaster.Columns.Add("PlacingType", typeof(string));
            dtMaster.Columns.Add("Price", typeof(decimal));
            dtMaster.Columns.Add("VatPrice", typeof(decimal));
            dtMaster.Columns.Add("TotalPrice", typeof(decimal));
            dtMaster.Columns.Add("WriteID", typeof(string));
            dtMaster.Columns.Add("VatType", typeof(string));
            dtMaster.Columns.Add("VatRate", typeof(int));
            dtMaster.Columns.Add("State", typeof(string));
            dtMaster.Columns.Add("CreIP", typeof(string));
            dtMaster.Columns.Add("CreId", typeof(string));
            dtMaster.Columns.Add("CreDt", typeof(DateTime));

            if (crud == "Update")
            {
                DisplayMaster();
            }
            else
            {
                // 신규 데이터 입력
                DataRow dr = dtMaster.NewRow();
                dr["CorpId"] = WrGlobal.CorpID;
                dr["MmOrderNo"] = "";
                dr["PlacingDate"] = DateTime.Now.Date;
                dr["DeliveryDate"] = DateTime.Now.Date.AddDays(7);
                dr["PlacingType"] = "10"; //통합
                dr["Price"] = 0;
                dr["VatPrice"] = 0;
                dr["TotalPrice"] = 0;
                dr["WriteID"] = WrGlobal.LoginID;
                dr["VatType"] = "10";
                dr["VatRate"] = 10;
                dr["State"] = "진행";
                dr["CreIP"] = WrGlobal.ClientHostName;
                dr["CreId"] = WrGlobal.LoginID;
                dr["CreDt"] = DateTime.Now;

                dtMaster.Rows.Add(dr);
            }

            DisplayDetail();
        }

        #region 디자인

        private void Set_Lup()
        {
            /*
             * 거래처구분 *
             CLASS = 'A030'

                10 : 매입
                20 : 매출
                30 : 외주
                40 : 매입매출
             */
            // 거래처
            searchCustomer.Properties.DataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, false);
            searchCustomer.Properties.DisplayMember = "거래처명";
            searchCustomer.Properties.ValueMember = "거래처코드";

            // 사용자
            searchSalesMan.Properties.DataSource = code.GetUser();
            searchSalesMan.Properties.DisplayMember = "사용자명";
            searchSalesMan.Properties.ValueMember = "ID";

            code.LookupCategory(ref vatTypeLookUpEdit, "C010");         // 과세구분

            placingDateEdit.EditValue = DateTime.Now.Date;
            deliveryDateDateEdit.EditValue = DateTime.Now.Date.AddDays(7);
            vatTypeLookUpEdit.EditValue = "10";
            vatRateSpinEdit.EditValue = 10;
            searchSalesMan.EditValue = WrGlobal.LoginID;
        }

        private void SetGridView(GridView gv)
		{
            gv.Columns.Clear();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            //Repository
            RepositoryItemButtonEdit btnDelete = new RepositoryItemButtonEdit();
            btnDelete.AutoHeight = false;
            btnDelete.Buttons.Clear();

            btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            btnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDelete_ButtonClick);

            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");


            //
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "발주순번", "MmOrderSeq");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "발주량", "Qty");
            grid.Create_GvColumn(gv, "입고량", "InQty");
            grid.Create_GvColumn(gv, "미입고량", "BalanceQty");

            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "삭제", "colDelete", btnDelete);
            AddPlanningColumns(gv, grid, () => placingDateEdit.EditValue,
                () => searchCustomer.EditValue, () => searchCustomer.Text);
            if (MM_PlacingD.UsePlanningFields)
            {
                placingDateEdit.EditValueChanged += (s, e) => gv.RefreshData();
                searchCustomer.EditValueChanged += (s, e) => gv.RefreshData();
            }

            //Allow Edit
			grid.Column_AllowEdit(gv.Columns["DeliveryDate"]);
			grid.Column_AllowEdit(gv.Columns["Qty"]);
			grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
			grid.Column_AllowEdit(gv.Columns["Price"]);
			grid.Column_AllowEdit(gv.Columns["VatPrice"]);
			grid.Column_AllowEdit(gv.Columns["TotalPrice"]);
			grid.Column_AllowEdit(gv.Columns["Remark"]);
			grid.Column_AllowEdit(gv.Columns["colDelete"]);

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("MaterialClass1") != null) gv.Columns["MaterialClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("MaterialClass2") != null) gv.Columns["MaterialClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("MaterialClass3") != null) gv.Columns["MaterialClass3"].ColumnEdit = lup_Class_Cd3;

            //
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            gv.Columns["colDelete"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //이벤트
            grid.RowNumber_Indicator(gv);
			gv.CellValueChanged += Gv_CellValueChanged;
        }

        /// <summary>발주 목록과 편집 화면에 같은 구매 계획 항목과 계산식을 적용합니다.</summary>
        internal static void AddPlanningColumns(GridView view, clsGrid grid, Func<object> orderDate = null,
            Func<object> customerCode = null, Func<object> customerName = null)
        {
            if (!MM_PlacingD.UsePlanningFields)
                return;

            bool editable = orderDate != null;
            if (editable)
            {
                grid.Create_GvUnboundColumn(view, "거래처 코드", "PlanningCustCode", typeof(string), "");
                grid.Create_GvUnboundColumn(view, "구매처", "PlanningCustName", typeof(string), "");
            }
            else
            {
                grid.Create_GvColumn(view, "거래처 코드", "CustCode");
                grid.Create_GvColumn(view, "구매처", "CustName");
            }

            grid.Create_GvColumn(view, "현재고량", "CurrentStockQty");
            grid.Create_GvColumn(view, "월사용량", "MonthlyUsageQty", null, editable);
            grid.Create_GvColumn(view, "용도", "UsagePurpose", CreatePlanningTextEditor(view, 200, editable), editable);
            grid.Create_GvColumn(view, "사용부서", "UsageDepartment", CreatePlanningTextEditor(view, 100, editable), editable);
            grid.Create_GvColumn(view, "MOQ", "MinimumOrderQty", null, editable);
            grid.Create_GvColumn(view, "1P 기준", "PTLoadQty");
            foreach (string field in new[] { "CurrentStockQty", "MonthlyUsageQty", "MinimumOrderQty", "PTLoadQty" })
            {
                view.Columns[field].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns[field].DisplayFormat.FormatString = "#,##0.####";
            }

            grid.Create_GvUnboundColumn(view, "일평균 (월 22일)", "DailyUsageQty", typeof(decimal), "", DevExpress.Utils.FormatType.Numeric, "#,##0.##");
            grid.Create_GvUnboundColumn(view, "재고회전일수", "StockDays", typeof(decimal), "", DevExpress.Utils.FormatType.Numeric, "#,##0.##");
            grid.Create_GvUnboundColumn(view, "리드 타임 (일)", "LeadTimeDays", typeof(int), "");
            grid.Create_GvUnboundColumn(view, "납품일 기준 잔여재고량", "DeliveryRemainingQty", typeof(decimal), "", DevExpress.Utils.FormatType.Numeric, "#,##0.##");
            grid.Create_GvUnboundColumn(view, "납품일 기준 재고회전일수", "DeliveryStockDays", typeof(decimal), "", DevExpress.Utils.FormatType.Numeric, "#,##0.##");

            view.CustomUnboundColumnData += (sender, e) =>
            {
                if (!e.IsGetData)
                    return;
                switch (e.Column.FieldName)
                {
                    case "PlanningCustCode": e.Value = Convert.ToString(customerCode()); return;
                    case "PlanningCustName": e.Value = Convert.ToString(customerName()); return;
                }

                object Value(string field) => view.GetListSourceRowCellValue(e.ListSourceRowIndex, field);
                bool hasDelivery = DateTime.TryParse(Convert.ToString(Value("DeliveryDate")), out DateTime delivery);
                if (e.Column.FieldName == "LeadTimeDays")
                {
                    if (hasDelivery && DateTime.TryParse(Convert.ToString(editable ? orderDate() : Value("PlacingDate")), out DateTime placed)
                        && delivery.Date >= placed.Date)
                        e.Value = (delivery.Date - placed.Date).Days;
                    return;
                }

                object usage = Value("MonthlyUsageQty");
                if (usage == null || usage == DBNull.Value)
                    return;
                decimal daily = Convert.ToDecimal(usage) / 22m;
                object stockValue = Value("CurrentStockQty");
                if (e.Column.FieldName == "DailyUsageQty")
                {
                    e.Value = daily;
                    return;
                }
                if (stockValue == null || stockValue == DBNull.Value)
                    return;
                decimal stock = Convert.ToDecimal(stockValue);
                decimal remaining = stock - daily * Math.Max(0, (delivery.Date - DateTime.Today).Days);
                switch (e.Column.FieldName)
                {
                    case "StockDays": if (daily > 0) e.Value = stock / daily; break;
                    case "DeliveryRemainingQty": if (hasDelivery) e.Value = remaining; break;
                    case "DeliveryStockDays":
                        if (hasDelivery && daily > 0)
                        {
                            decimal pending = Math.Max(0m, Value("Qty").ConvertDecimal_Null_To_Zero() - Value("InQty").ConvertDecimal_Null_To_Zero());
                            e.Value = (remaining + pending) / daily;
                        }
                        break;
                }
            };
        }

        private static RepositoryItemTextEdit CreatePlanningTextEditor(GridView view, int maxLength, bool editable)
        {
            if (!editable)
                return null;
            var editor = new RepositoryItemTextEdit { MaxLength = maxLength };
            view.GridControl.RepositoryItems.Add(editor);
            return editor;
        }

		private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);
            string value = e.Value.ConvertString_Null_To_Empty();

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);


            gv.BestFitColumns();
        }

		#endregion

		#region 조회
		private void DisplayMaster()
        {
            MM_PlacingM pm = new MM_PlacingM();
            pm.MmOrderNo = mmOrderNo;

            DataTable dt = pm.Select_MM_PlacingM_MmOrderNo();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                mmOrderNoTextEdit.EditValue = dr["MmOrderNo"];
                placingDateEdit.EditValue = dr["PlacingDate"];
                deliveryDateDateEdit.EditValue = dr["DeliveryDate"];
                stateTextEdit.EditValue = dr["State"];
                searchCustomer.EditValue = dr["CustCode"];
                vatTypeLookUpEdit.EditValue = dr["VatType"];
                vatRateSpinEdit.EditValue = dr["VatRate"];
                customerUserTextEdit.EditValue = dr["CustomerUser"];
                customerTelTextEdit.EditValue = dr["CustomerTel"];
                searchSalesMan.EditValue = dr["WriteID"];
                remarkTextEdit.EditValue = dr["Remark"];
            }
        }

        private void DisplayDetail()
        {
            MM_PlacingD pd = new MM_PlacingD();
            pd.MmOrderNo = mmOrderNo;

            gcDetail.DataSource = pd.Select_MM_PlacingD_MmOrderNo();
            gvDetail.BestFitColumns();
        }

        #endregion


        #region 저장

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!gvDetail.PostEditor() || !gvDetail.UpdateCurrentRow())
                return;
            if ((searchCustomer.EditValue ?? "").ToString() == "")
            {
                MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((deliveryDateDateEdit.EditValue ?? "").ToString() == "")
            {
                MessageBox.Show("납기일자를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (gvDetail.SelectedRowsCount <= 0)
            {
                MessageBox.Show("품목을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MM_PlacingD.UsePlanningFields && gcDetail.DataSource is DataTable detail)
            {
                foreach (DataRow row in detail.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;
                    foreach (string field in new[] { "MonthlyUsageQty", "MinimumOrderQty" })
                    {
                        decimal quantity = row[field].ConvertDecimal_Null_To_Zero();
                        if (quantity < 0 || quantity > 999999999999999.9999m)
                        {
                            MessageBox.Show("월사용량과 MOQ는 0 이상이며 정수부가 15자리를 넘을 수 없습니다.", "입력 확인");
                            return;
                        }
                    }
                }
            }

            MM_PlacingM pm = new MM_PlacingM();

            try
            {
                // 1. 마스터
                pm.CorpId = WrGlobal.CorpID;
                pm.MmOrderNo = mmOrderNoTextEdit.EditValue.ConvertString_Null_To_Empty();

                pm.CustCode = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                pm.PlacingDate = placingDateEdit.EditValue.ConvertDate_To_Char10();
                pm.DeliveryDate = deliveryDateDateEdit.EditValue.ConvertDate_To_Char10();
                pm.WriteID = searchSalesMan.EditValue.ConvertString_Null_To_Empty();
                pm.VatType = vatTypeLookUpEdit.EditValue.ConvertString_Null_To_Empty();
                pm.VatRate = vatRateSpinEdit.EditValue.ConvertInt_Null_To_Zero();
                pm.CustomerUser = customerUserTextEdit.EditValue.ConvertString_Null_To_Empty();
                pm.CustomerTel = customerTelTextEdit.EditValue.ConvertString_Null_To_Empty();
                pm.State = "진행";
                pm.PlacingType = "10"; //통합
                pm.Remark = remarkTextEdit.EditValue.ConvertString_Null_To_Empty();

            }
            catch
            {
                MessageBox.Show("입력 값 변환 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (crud == "Insert")
            {
                pm.MmOrderNo = "MO" + code.GetAutoNumber("발주번호", Convert.ToDateTime(pm.PlacingDate));

                if (!pm.Insert_MM_PlacingM())
                {
                    MessageBox.Show("저장 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else // Update
            {
                if (!pm.Update_MM_PlacingM())
                {
                    MessageBox.Show("저장 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            // 2. 디테일
            int sumPrice = 0;
            int sumVat = 0;
            int sumTotal = 0;

            try
            {
                for (int i = 0; i < gvDetail.RowCount; i++)
                {
                    DataRow dr = gvDetail.GetDataRow(i);
                    if (dr == null) return;

                    MM_PlacingD pd = new MM_PlacingD();

                    pd.CorpId = pm.CorpId;
                    pd.MmOrderNo = pm.MmOrderNo;
                    pd.MmOrderSeq = dr["MmOrderSeq"].ConvertInt_Null_To_Zero();
                    pd.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
                    pd.UnitCode = dr["UnitCode"].ConvertString_Null_To_Empty();
                    pd.Qty = dr["Qty"].ConvertFloat_Null_To_Zero();
                    pd.UnitPrice = dr["UnitPrice"].ConvertInt_Null_To_Zero();
                    pd.Price = dr["Price"].ConvertInt_Null_To_Zero();
                    pd.VatPrice = dr["VatPrice"].ConvertInt_Null_To_Zero();
                    pd.TotalPrice = dr["TotalPrice"].ConvertInt_Null_To_Zero();
                    pd.VatType = dr["VatType"].ConvertString_Null_To_Empty();
                    pd.VatRate = dr["VatRate"].ConvertInt_Null_To_Zero();
                    pd.DeliveryDate = dr["DeliveryDate"].ConvertDate_To_Char10();
                    pd.State = "진행";
                    pd.Remark = dr["Remark"].ConvertString_Null_To_Empty();
                    pd.SetPlanningFields(dr);

                    //금액 합계 계산
                    sumPrice += pd.Price ?? 0;
                    sumVat += pd.VatPrice ?? 0;
                    sumTotal += pd.TotalPrice ?? 0;

                    string status = dr["Status"].ConvertString_Null_To_Empty();

                    if (status == "신규")
                    {
                        if (!pd.Insert_MM_PlacingD())
                        {
                            MessageBox.Show("디테일 저장 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if(status =="수정") // Update
                    {
                        if (!pd.Update_MM_PlacingD())
                        {
                            MessageBox.Show("디테일 수정 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }else if(status == "삭제")
					{
                        if(!pd.Delete_MM_PlacingD())
						{
                            MessageBox.Show("디테일 수정 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
					}

                    dr["Status"] = "";
                }

                // 마스터 금액 합계 업데이트
                pm.Price = sumPrice;
                pm.VatPrice = sumVat;
                pm.TotalPrice = sumTotal;

                if (!pm.Update_MM_PlacingM_Price())
                {
                    MessageBox.Show("마스터 합계 업데이트 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 이벤트

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                int rowHandle = gvDetail.FocusedRowHandle;
                if (rowHandle < 0) return;

				DataRow dr = gvDetail.GetDataRow(rowHandle);
				if (dr == null) return;

				// 완료 상태 체크
				string status = (dr["Status"] ?? "").ToString();
				if (status == "신규")
				{
					gvDetail.DeleteRow(rowHandle);
				}
				else if (status == "삭제")
				{
					gvDetail.SetRowCellValue(rowHandle, "Status", "");
				}
				else
				{
					gvDetail.SetRowCellValue(rowHandle, "Status", "삭제");
				}
			}
		}

		private void frmMM_PlacingD_Load(object sender, EventArgs e)
        {
        }

        // 금액 재계산
        private void CalculatePrice()
        {
            for (int i = 0; i < gvDetail.RowCount; i++)
            {
                DataRow row = gvDetail.GetDataRow(i);
                if (row == null) continue;

                if (row["Qty"] == DBNull.Value || string.IsNullOrEmpty(row["Qty"].ToString()))
                    row["Qty"] = 0;

                if (row["UnitPrice"] == DBNull.Value || string.IsNullOrEmpty(row["UnitPrice"].ToString()))
                    row["UnitPrice"] = 0;

                decimal qty = Convert.ToDecimal(row["Qty"]);
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                decimal vatRate = Convert.ToDecimal(vatRateSpinEdit.EditValue);

                row["Price"] = qty * unitPrice;
                row["VatPrice"] = Math.Round((qty * unitPrice) * vatRate / 100);
                row["TotalPrice"] = Convert.ToDecimal(row["Price"]) + Convert.ToDecimal(row["VatPrice"]);
            }
        }

        // 부가세율 변경시
        private void vatRateSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void vatTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if ((vatTypeLookUpEdit.EditValue ?? "").ToString() == "21")
            {
                vatRateSpinEdit.EditValue = 10;
            }
            else
            {
                vatRateSpinEdit.EditValue = 0;
            }
        }

        private void gvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "Qty")
            {
                CalculatePrice();
            }
        }

		private void smtSearchButton2_Click(object sender, EventArgs e)
		{
            this.Close();
		}

		#endregion

		#region 품목선택

		private void btn_Check_Click(object sender, EventArgs e)
		{
			if (searchCustomer.EditValue.ConvertString_Null_To_Empty() == "")
			{
				MessageBox.Show("거래처를 먼저 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

            Frm_SelectItem form = new Frm_SelectItem(searchCustomer.EditValue.ConvertString_Null_To_Empty(), "10"); //매입
            form.StartPosition = FormStartPosition.CenterParent;
            form.WindowState = FormWindowState.Maximized;
			form.onSetData += new Frm_SelectItem.SetData(ProductSetData);
			form.ShowDialog();
		}

		private void ProductSetData(DataRow drv)
        {
            DataRow newItem;

            try
            {
                DataTable dt = gvDetail.GridControl.DataSource as DataTable;
                if (dt == null) return;

                newItem = dt.NewRow();

                newItem["CorpId"] = WrGlobal.CorpID;
                //newItem["MmOrderNo"] = TxtSdOrderNo.Text;
                newItem["Status"] = "신규";
                newItem["State"] = "대기";

                int maxSeq = 0;

                foreach (DataRow item in dt.Rows)
                {
                    int seq = item["MmOrderSeq"] == DBNull.Value ? 0 : (int)item["MmOrderSeq"];
                    if (maxSeq < seq) maxSeq = seq;
                }
                newItem["MmOrderSeq"] = ++maxSeq;

                CM_Item cm = new CM_Item();
                cm.ItemCode = clsCommon.getString(drv["ItemCode"]);

                DataRow drItem = cm.Select_Item_RowInfo();

                if (dt.Columns.Contains("ItemCode")) newItem["ItemCode"] = clsCommon.getString(drv["ItemCode"]);
                if (dt.Columns.Contains("ItemName")) newItem["ItemName"] = clsCommon.getString(drItem["ItemName"]);
                if (dt.Columns.Contains("Alias")) newItem["Alias"] = clsCommon.getString(drItem["Alias"]);
                if (dt.Columns.Contains("ItemClass1")) newItem["ItemClass1"] = clsCommon.getString(drItem["ItemClass1"]);
                if (dt.Columns.Contains("ItemClass2")) newItem["ItemClass2"] = clsCommon.getString(drItem["ItemClass2"]);
                if (dt.Columns.Contains("ItemClass3")) newItem["ItemClass3"] = clsCommon.getString(drItem["ItemClass3"]);
                if (dt.Columns.Contains("Size")) newItem["Size"] = clsCommon.getString(drItem["Size"]);
                if (dt.Columns.Contains("UnitWeight")) newItem["UnitWeight"] = drItem["UnitWeight"].ConvertInt_Null_To_Zero();
                if (dt.Columns.Contains("Texture")) newItem["Texture"] = clsCommon.getString(drItem["Texture"]);
                if (dt.Columns.Contains("Type")) newItem["Type"] = clsCommon.getString(drItem["Type"]);
                if (dt.Columns.Contains("UnitCode")) newItem["UnitCode"] = clsCommon.getString(drItem["UnitCode"]);
                if (dt.Columns.Contains("ColorCode")) newItem["ColorCode"] = clsCommon.getString(drItem["ColorCode"]);
                if (dt.Columns.Contains("ShipToCode")) newItem["ShipToCode"] = clsCommon.getString(drv["ShipToCode"]);
                new MM_PlacingD().FillPlanningItem(newItem, drItem);

                if (Convert.ToString(deliveryDateDateEdit.EditValue) != "")
                {
                    newItem["DeliveryDate"] = deliveryDateDateEdit.EditValue;
                }

                //newItem["Qty"] = drv["SdOrderQty"];

                decimal orderQty = drv["OrderQty"].ConvertDecimal_Null_To_Zero();
                newItem["Qty"] = orderQty;

                if (dt.Columns.Contains("StockQty")) newItem["StockQty"] = 0;
                if (dt.Columns.Contains("ReqQty")) newItem["ReqQty"] = 0;
                if (dt.Columns.Contains("ProQty")) newItem["ProQty"] = 0;
                if (dt.Columns.Contains("OutQty")) newItem["OutQty"] = 0;

                /*
                //if (drv["SalesPrice"] == DBNull.Value || drv["SalesPrice"] == null)
                if (drv["UnitPrice"] == DBNull.Value || drv["UnitPrice"] == null)
                {
                    newItem["UnitPrice"] = 0;
                }
                */
                //거래처별 단가에 데이터가 있는지 체크
                /*
                else if ((searchCustomer.EditValue ?? "").ToString() != "")
                {
                */
                string custCode = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                string ItemCode = drv["ItemCode"].ToString();

                CM_ItemPrice cmPrice = new CM_ItemPrice();
                cmPrice.PriceType = "10";
                cmPrice.ApplyDate = DateTime.Now.ToString("d");

                decimal price = cmPrice.Get_MaterialPrice(custCode, ItemCode);
                newItem["UnitPrice"] = price;
                /*
            }
            else
            {
                newItem["UnitPrice"] = drv["UnitPrice"];
            }
                */

                newItem["Price"] = price * orderQty;
                newItem["VatPrice"] = price * orderQty * 0.1.ConvertDecimal_Null_To_Zero();
                newItem["TotalPrice"] = price * orderQty * 1.1.ConvertDecimal_Null_To_Zero();

                newItem["CreId"] = WrGlobal.LoginID;
                newItem["CreIP"] = WrGlobal.ClientHostName;
                newItem["CreDt"] = DateTime.Now;

                dt.Rows.Add(newItem);

                gvDetail.FocusedRowHandle = gvDetail.RowCount - 1;

                //CalculatePriceByUnitPriceOrQty();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            gvDetail.BestFitColumns();
        }

        #endregion
    }
}
