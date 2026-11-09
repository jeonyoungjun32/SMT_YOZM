using DevExpress.Utils;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    /// <summary>
    /// BOM정보
    /// </summary>
	public partial class Frm_CM_BOMList : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsBom bom = new clsBom();
        int maxChildID = 0;
        string query = "";
        string[] ItemType_P = new string[] { "20", "30" }; //제품, 반제품
        string ItemType = "";
        private Dictionary<string, decimal> bomStockQuantities = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        private bool bomStockInitialized;

        public Frm_CM_BOMList()
        {
            InitializeComponent();

            DragDropManager.Default.DragOver += OnDragOver;
            DragDropManager.Default.DragDrop += OnDragDrop;
        }

        private void frmBOMView_Load(object sender, EventArgs e)
        {
            Init();
            Set_Item_Div_Rdo(Rdo_Item_Div);

            Set_Grid_Product(GvProduct);
            Set_Grid_BOMRev(gvCM_BOMRev);
            Set_Grid_Material(GvMaterial);
            InitializeBomStockColumns();
            InitializeBomQuantityColumns();


            DisplayBomProduct();
            DisplayMaterial();
        }

        private void Init()
		{

        }

        /// <summary>
        /// 전개 소요량·실소요량을 재고와 구분하고 EPL의 공급·출고 조건을 표시합니다.
        /// </summary>
        private void InitializeBomQuantityColumns()
        {
            // 원본 수량은 환산 기준으로 보관하고, 화면에서는 현재 단위 투입량을 전개합니다.
            BomTreeList.Columns.AddField("RequiredQty");
            BomTreeList.Columns.AddField("ActualQty");
            var required = AddBomDetailColumn("ViewRequiredQty", "소요량", 3, "0.0000000");
            var actual = AddBomDetailColumn("ViewActualQty", "실소요량", 4, "0.0000000");
            required.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Decimal;
            actual.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Decimal;

            colUnitCode.Visible = true;
            colUnitCode.VisibleIndex = 5;
            colUnitCode.ColumnEdit = rpo.Create_RepositoryItem(
                code.GetCM_CateItem(WrGlobal.CorpID, "A011"), "코드", "명칭");
            colCurrentStockQty.VisibleIndex = 6;
            colQty.Caption = "단위실투입량";
            colQty.Visible = true;
            colQty.VisibleIndex = 7;
            colQty.Format.FormatType = FormatType.Numeric;
            colQty.Format.FormatString = "0.##########";
            treeListColumn2.VisibleIndex = 8;
            AddBomDetailColumn("SupplyType", "공급구분", 9);
            AddBomDetailColumn("IssueType", "출고구분", 10);
            AddBomDetailColumn("InputProcessName", "투입공정", 11);
            AddBomDetailColumn("BomStartDate", "적용시작일", 12, "yyyy-MM-dd");
            AddBomDetailColumn("BomEndDate", "적용종료일", 13, "yyyy-MM-dd");
            colNodeDel.VisibleIndex = 14;

            BomTreeList.CustomUnboundColumnData += (sender, args) =>
            {
                if (args.IsGetData && (args.Column == required || args.Column == actual))
                    args.Value = (object)GetBomQuantity(args.Node, args.Column == required) ?? DBNull.Value;
            };
            BomTreeList.CellValueChanged += (sender, args) =>
            {
                if (args.Column.FieldName == "Qty" || args.Column.FieldName == "CalcQty")
                    BomTreeList.RefreshDataSource();
            };
        }

        private DevExpress.XtraTreeList.Columns.TreeListColumn AddBomDetailColumn(
            string field, string caption, int index, string format = null)
        {
            var column = BomTreeList.Columns.AddVisible(field);
            column.Caption = caption;
            column.VisibleIndex = index;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.ReadOnly = true;
            column.Width = 120;
            if (format != null)
            {
                column.Format.FormatType = field.EndsWith("Date") ? FormatType.DateTime : FormatType.Numeric;
                column.Format.FormatString = format;
            }
            return column;
        }

        /// <summary>
        /// 부모별 단위실투입량/기준수를 곱해 최상위 제품 기준의 수량을 계산합니다.
        /// </summary>
        private static decimal? GetBomQuantity(TreeListNode node, bool required)
        {
            if (node == null)
                return null;
            decimal quantity = 1m;
            for (var current = node; current != null; current = current.ParentNode)
            {
                object value = current.GetValue("Qty");
                decimal basis = current.GetValue("CalcQty").ConvertDecimal_Null_To_One();
                if (value == null || value == DBNull.Value || basis <= 0)
                    return null;
                quantity *= value.ConvertDecimal_Null_To_Zero() / basis;
            }

            // 살균유처럼 이론 소요량과 실제 투입량이 다른 품목의 원본 비율을 유지합니다.
            decimal actual = node.GetValue("ActualQty").ConvertDecimal_Null_To_Zero();
            object theoretical = node.GetValue("RequiredQty");
            if (required && actual > 0 && theoretical != null && theoretical != DBNull.Value)
                quantity *= theoretical.ConvertDecimal_Null_To_Zero() / actual;
            return quantity;
        }

        /// <summary>
        /// BOM 및 좌우 품목 목록에 현재고를 표시하고 화면 재진입 시 재고를 갱신합니다.
        /// </summary>
        private void InitializeBomStockColumns()
        {
            if (bomStockInitialized)
                return;

            // 디자이너의 재고수량 컬럼에 실제 재고만 연결하며 구성수량은 변경하지 않습니다.
            BomTreeList.CustomUnboundColumnData += (sender, args) =>
            {
                if (args.IsGetData && args.Column.FieldName == "CurrentStockQty")
                    args.Value = GetBomStockQuantity(args.Node.GetValue("MItemCode"));
            };

            foreach (GridView view in new[] { GvProduct, GvMaterial })
            {
                var column = view.Columns.AddVisible("CurrentStockQty", "재고수량");
                column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                column.DisplayFormat.FormatType = FormatType.Numeric;
                column.DisplayFormat.FormatString = "n4";
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
                view.CustomUnboundColumnData += (sender, args) =>
                {
                    if (args.IsGetData && args.Column.FieldName == "CurrentStockQty")
                        args.Value = GetBomStockQuantity(
                            ((GridView)sender).GetListSourceRowCellValue(args.ListSourceRowIndex, "ItemCode"));
                };
            }

            bomStockInitialized = true;
            RefreshBomStockQuantities();
            Activated += (sender, args) => RefreshBomStockQuantities();
        }

        /// <summary>
        /// 자재현재고현황과 같은 재고 뷰에서 POP 차감 대상 창고의 남은 수량을 조회합니다.
        /// </summary>
        private void RefreshBomStockQuantities()
        {
            if (!bomStockInitialized)
                return;

            string corp = WrGlobal.CorpID.Replace("'", "''");
            DataTable stock = db.GetDataTable($@"
                SELECT i.ItemCode,ISNULL(SUM(s.StockQty),0) StockQty
                FROM CM_Item i LEFT JOIN StockListView s ON s.CorpId=i.CorpId AND s.ItemCode=i.ItemCode
                AND s.WareHouseCode=CASE WHEN i.ItemType='30' THEN 'S0010' ELSE 'S0001' END
                WHERE i.CorpId='{corp}' GROUP BY i.ItemCode");

            var quantities = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

            foreach (DataRow row in stock.Rows)
                quantities[row["ItemCode"].ConvertString_Null_To_Empty()] = row["StockQty"].ConvertDecimal_Null_To_Zero();

            bomStockQuantities = quantities;

            // 재조회는 표시값만 갱신하므로 편집 중인 BOM 구성 유지
            BomTreeList.RefreshDataSource();
            GvProduct.RefreshData();
            GvMaterial.RefreshData();
        }

        /// <summary>
        /// 품목별 남은 재고를 반환하며, 수불 내역이 없는 품목은 0으로 표시합니다.
        /// </summary>
        private decimal GetBomStockQuantity(object itemCode)
        {
            decimal quantity;
            bomStockQuantities.TryGetValue(itemCode.ConvertString_Null_To_Empty(), out quantity);
            return quantity;
        }

        private void Set_Item_Div_Rdo(RadioGroup rdo)
        {
            string all = "전체";
            ItemType = "30,40,50";

            DataTable dt = code.smtLookUp("품목구분", new string[] { ItemType }, all);

            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(rdo, dt, "코드명", "코드");

            rdo.SelectedIndex = 0; //초기값

            rdo.EditValueChanged += Rdo_ItemDIv_EditValueChanged;
        }

        private void Set_Grid_Product(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //필터
            gv.OptionsView.ShowAutoFilterRow = true;

            //Column 생성
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "등록여부", "BomYn");

            //grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");


            //이벤트 설정
            grid.RowNumber_Indicator(gv);   //행번호 표기
        }

        private void Set_Grid_BOMRev(GridView gv)
		{
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //필터
            //gv.OptionsView.ShowAutoFilterRow = true;

            CM_RoutingRev routingRev = new CM_RoutingRev();

            RepositoryItemSearchLookUpEdit lupRouting = rpo.Create_RepositoryItemSearchLookUp(routingRev.Select_CM_RoutingRev_Lup(), "코드", "코드");
            RepositoryItemButtonEdit btnRevDelete = new RepositoryItemButtonEdit();

            btnRevDelete.Name = "btnRevDelete";
            btnRevDelete.AutoHeight = false;
            btnRevDelete.Buttons.Clear();
            btnRevDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            btnRevDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnRevDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnRevDelete_ButtonClick);

            //Column 생성
            grid.Create_GvColumn(gv, "라우팅코드", "RoutingCode", lupRouting);
            grid.Create_GvColumn(gv, "리비전", "Revision");
            grid.Create_GvColumn(gv, "리비전명", "RevisionName");
            grid.Create_GvColumn(gv, "기본사용", "UseYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "삭제", "btnDelete", btnRevDelete);

            //
            grid.Column_AllowEdit(gv.Columns["RoutingCode"]);
            grid.Column_AllowEdit(gv.Columns["Revision"]);
            grid.Column_AllowEdit(gv.Columns["RevisionName"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);
            grid.Column_AllowEdit(gv.Columns["btnDelete"]);

            gv.Columns["UseYn"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");


            //이벤트 설정
            grid.RowNumber_Indicator(gv);   //행번호 표기
        }

        private void Set_Grid_Material(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //필터
            gv.OptionsView.ShowAutoFilterRow = true;

            //Column 생성
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "규격", "Size");


            //이벤트 설정
            grid.RowNumber_Indicator(gv);   //행번호 표기
        }

        private void DisplayBomProduct()
        {
            CM_Item item = new CM_Item();
            DataTable dt = item.SelectList_BOM(ItemType_P);
            RemoveAutoProcessItems(dt);

            GcProduct.DataSource = dt;
            GvProduct.BestFitColumns();
        }

        private void RemoveAutoProcessItems(DataTable dt)
        {
            if (dt == null || !dt.Columns.Contains("ItemCode")) return;

            query = $@"SELECT ProcessCode
                         FROM CM_Process
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            DataTable dtProcess = db.GetDataTable(query);

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                string itemCode = dt.Rows[i]["ItemCode"].ConvertString_Null_To_Empty();

                foreach (DataRow drProcess in dtProcess.Rows)
                {
                    string processCode = drProcess["ProcessCode"].ConvertString_Null_To_Empty();
                    if (processCode == "") continue;

                    if (itemCode.EndsWith("-" + processCode, StringComparison.OrdinalIgnoreCase) ||
                        itemCode.EndsWith("_" + processCode, StringComparison.OrdinalIgnoreCase))
                    {
                        dt.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void DisplayBomRev()
        {
            DataRow drProduct = GvProduct.GetFocusedDataRow();
            if (drProduct == null) return;

            CM_BOMRev rev = new CM_BOMRev();
            rev.CorpId = WrGlobal.CorpID;
            rev.ItemCode = drProduct["ItemCode"].ConvertString_Null_To_Empty();

            gcCM_BOMRev.DataSource = rev.SelectBomMaster();
            gcCM_BOMRev.RefreshDataSource();
            gvCM_BOMRev.BestFitColumns();


            // 제품이 바뀌면 최신 리비전부터 표시합니다.
            if (productChanged)
            {
                gvCM_BOMRev.FocusInvalidRow(); //포커스 잃었다가 Select해야 BOM리스트 조회


                // 품목 선택 시 최신 BOM을 즉시 표시하여 리비전을 따로 고를 필요가 없게 합니다.
                if (gvCM_BOMRev.RowCount > 0)
                {
                    gvCM_BOMRev.FocusedRowHandle = 0;
                    gvCM_BOMRev.SelectRow(0);
                }

                productChanged = false;
            }
            DisplayRevisionBom();
        }

        private void DisplayRevisionBom()
        {
            DataRow drProduct = GvProduct.GetFocusedDataRow();
            if (drProduct == null) return;

            DataRow drBOMRev = gvCM_BOMRev.GetFocusedDataRow();
            if (drBOMRev == null) return;

            CM_BOM bom = new CM_BOM();
            bom.PItemCode = drProduct["ItemCode"].ConvertString_Null_To_Empty();
            bom.Revision = drBOMRev["Revision"].ConvertString_Null_To_Empty();

            DataTable dtBom = bom.Code_GetBomRevision();

            maxChildID = 0;

            foreach (DataRow dr in dtBom.Rows)
            {
                if ((int)dr["ChildID"] > maxChildID) maxChildID = (int)dr["ChildID"];
            }

            BomTreeList.DataSource = dtBom;

            BomTreeList.ExpandAll();
            BomTreeList.BestFitColumns();
        }

        private void DisplayMaterial()
        {
            bom.itemDiv = Convert.ToString(Rdo_Item_Div.EditValue);
            DataView dvMaterial = bom.Select_Materialt_List();   //반제품, 자재리스트 검색 (반제품 30, 원자재 40, 부자재 50)
            DataTable dtMaterial = dvMaterial.ToTable();
            RemoveAutoProcessItems(dtMaterial);
            GcMaterial.DataSource = dtMaterial;
            GvMaterial.BestFitColumns();
        }

        #region 포커스 변경

        private bool productChanged = false;

        // 제품 리스트 포커스 변경
        private void gvCmProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GvProduct.FocusedRowHandle < 0) return;
            productChanged = true;
            DisplayBomRev();
        }

        // 리비전 포커스 변경
        private void gvCM_BOMRev_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvCM_BOMRev.FocusedRowHandle < 0)
            {
                // BOMTreeList 초기화
                BomTreeList.ClearNodes();
                return;
            }

            if(gvCM_BOMRev.FocusedRowHandle < 0)
            {
                gvCM_BOMRev.FocusInvalidRow();
            }

            DisplayRevisionBom();
        }
        #endregion 포커스 변경 END


        #region 리비전
        private void gvCM_BOMRev_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (gvCM_BOMRev.GetFocusedRowCellValue("CorpId") != DBNull.Value) return;

            string corpId = WrGlobal.CorpID;
            string ItemCode = (GvProduct.GetFocusedRowCellValue("ItemCode") ?? "").ToString();

            gvCM_BOMRev.SetFocusedRowCellValue("CorpId", corpId);
            gvCM_BOMRev.SetFocusedRowCellValue("ItemCode", ItemCode);

            // 최대 Revision 값 계산
            CM_BOMRev rev = new CM_BOMRev();
            rev.CorpId = corpId;
            rev.ItemCode = ItemCode;
            string revision = rev.GetNextRevision();

            gvCM_BOMRev.SetFocusedRowCellValue("Revision", revision);
            gvCM_BOMRev.SetFocusedRowCellValue("RoutingCode", GvProduct.GetFocusedRowCellValue("RoutingCode"));
            gvCM_BOMRev.SetFocusedRowCellValue("StartDate", DateTime.Today);
            gvCM_BOMRev.SetFocusedRowCellValue("EndDate", Convert.ToDateTime("2999-12-31"));
            gvCM_BOMRev.SetFocusedRowCellValue("UseYn", "N");
            gvCM_BOMRev.SetFocusedRowCellValue("CreDt", DateTime.Today);
            gvCM_BOMRev.SetFocusedRowCellValue("CreId", WrGlobal.LoginID);
            gvCM_BOMRev.SetFocusedRowCellValue("CreIP", WrGlobal.ClientHostName);

            gvCM_BOMRev.PostEditor();
            gvCM_BOMRev.UpdateCurrentRow();
        }

        private void gvCM_BOMRev_ShowingPopupEditForm(object sender, DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventArgs e)
        {
            //// 컨트롤 UI 변경 (버튼 레이블)
            ////Get the list of the form's buttons ('Cancel' and 'Update' buttons)
            List<SimpleButton> buttons = new List<SimpleButton>();
            FindChildrenByType(e.EditForm, buttons);
            //Access the 'Cancel' and 'Update' buttons and change their text
            foreach (SimpleButton btn in buttons)
            {
                if (btn.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormCancelButton))
                {
                    btn.Text = "취소";
                    btn.Click += CancelButtonClick;
                }
                else if (btn.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton))
                {
                    btn.Text = "저장";
                }
            }
            //ShowingPopupEditForm
            //Paint the Addresss text box in yellow.
            TextEdit textEditAddress = e.BindableControls["RoutingCode"] as TextEdit;
            if (textEditAddress != null) textEditAddress.Properties.Appearance.BackColor = Color.LightYellow;
        }
        private void CancelButtonClick(object sender, EventArgs e)
        {
            (sender as SimpleButton).Click -= CancelButtonClick;
        }

        void FindChildrenByType<T>(Control parent, List<T> list) where T : class
        {
            foreach (Control child in parent.Controls)
            {
                if (child is T)
                    list.Add(child as T);
                if (child.HasChildren)
                    FindChildrenByType<T>(child, list);
            }
        }
        private void gvCM_BOMRev_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                DataRow drProduct = GvProduct.GetFocusedDataRow();
                if (drProduct == null) return;

                //CM_RoutingRev RoutingCode 체크
                DataTable dt = gcCM_BOMRev.DataSource as DataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    string routingCode = dr["RoutingCode"].ToString();

                    if (!CheckRoutingCodeExists(routingCode))
                    {
                        MessageBox.Show("RoutingCode가 존재하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // UseYn 처리
                DataRowView row = (DataRowView)e.Row;
                string newYn = row.Row.RowState == DataRowState.Added ? "Y" : "N";

                CM_BOMRev rev = new CM_BOMRev();
                rev.ItemCode = drProduct["ItemCode"].ConvertString_Null_To_Empty();
                rev.Revision = row["Revision"].ConvertString_Null_To_Empty();
                rev.RevisionName = row["RevisionName"].ConvertString_Null_To_Empty();
                rev.RoutingCode = row["RoutingCode"].ConvertString_Null_To_Empty();
                rev.StartDate = row["StartDate"].ConvertDate_To_Char10();
                rev.EndDate = row["EndDate"].ConvertDate_To_Char10();
                rev.Remark = row["Remark"].ConvertString_Null_To_Empty();
                rev.UseYn = row["UseYn"].ConvertString_Null_To_Empty();

                if(rev.UseYn == "Y")
				{
                    //기존 리비전 사용여부 N 처리
                    rev.UpdateBomRev_UseYn_ByItemCode();
				}

                if (row.Row.RowState == DataRowState.Added)
				{
                    rev.InsertBomMaster();
				}
				else if(row.Row.RowState == DataRowState.Modified)
				{
                    rev.Upd_CM_BOMRev();
				}


                // BOM 기본 사용리비전 품목 테이블 적용
                string rowRoutingCode = row["RoutingCode"].ToString();
                code.SetProductBomRevision(WrGlobal.CorpID
                                        , drProduct["ItemCode"].ConvertString_Null_To_Empty()
                                        , row["Revision"].ToString(), newYn
                                        , rowRoutingCode);
                DisplayBomRev();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"에러: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckRoutingCodeExists(string routingCode)
        {
            //bool exists = false;
            query = $"SELECT COUNT(1) FROM CM_RoutingRev WHERE CorpId = '{WrGlobal.CorpID}' AND RoutingCode = '{routingCode}'";
            int result = Convert.ToInt32(code.ExecuteQry(query));
            return result > 0;
        }

        // 리비전 삭제
        private void btnRevDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                if (MessageBox.Show("선택한 BOM리비전을 삭제하겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }//end if

                try
                {
                    DataRow dr = gvCM_BOMRev.GetFocusedDataRow();
                    if (dr == null) return;

                    // 기본 리비전 삭제 방지
                    if (dr["UseYn"].ConvertString_Null_To_Empty() == "Y")
                    {
                        MessageBox.Show("리비전을 최소 1개 기본으로 선택해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    CM_BOMRev rev = new CM_BOMRev();
                    rev.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
                    rev.Revision = dr["Revision"].ConvertString_Null_To_Empty();


                    if (rev.DeleteBomRev())
                    {
                        DisplayBomRev();
                        MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion 리비전 END

        private void OnDragOver(object sender, DragOverEventArgs e)
        {
            try
            {
                if (object.ReferenceEquals(e.Source, e.Target))
                    return;
                e.Default();
                if (e.InsertType == InsertType.None)
                    return;
                e.Action = IsCopy(e.KeyState) ? DragDropActions.Copy : DragDropActions.Move;
                Cursor current = Cursors.No;
                if (e.Action != DragDropActions.None)
                    current = Cursors.Default;
                e.Cursor = current;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool IsCopy(DragDropKeyState key)
        {
            return (key & DragDropKeyState.Control) != 0;
        }

        void OnDragDrop(object sender, DragDropEventArgs e)
        {
            try
            {
                TreeListNode destNode = GetDestNode(e.Location);

                if (destNode != null)
                {
                    string a = destNode["ProcessType"].ToString();
                }
                else
                {
                    e.Handled = true;
                    return;
                }
                if (e.InsertType == InsertType.AsChild && destNode["ProcessType"].ToString() == "M")
                {   // 공정에만 하위 자재로 등록가능
                    e.Handled = true;
                    return;
                }

                // 같은 컨트롤 간 드래그 방지
                if (object.ReferenceEquals(e.Source, e.Target))
                    return;

                if (e.Action == DragDropActions.None || e.InsertType == InsertType.None)
                    return;

                if (e.Target == BomTreeList)
                    OnTreeListDrop(e, destNode);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnTreeListDrop(DragDropEventArgs e, TreeListNode destNode)
        {
            try
            {
                if (((DevExpress.XtraGrid.Views.Grid.GridView)e.Source).Name == "GvMaterial")
                {
                    int gIndex = ((int[])e.Data)[0];
                    if (gIndex < 0) return;

                    int index = CalcDestNodeIndex(e, destNode);
                    TreeListNode addNode = null;

                    index = CalcDestNodeIndex(e, destNode);
                    BomTreeList.BeginUpdate();
                    BomTreeList.Selection.UnselectAll();

                    DataRow sourceRow = ((GridView)e.Source).GetDataRow(gIndex);


                    if (((sourceRow["ItemTypeName"] ?? "").ToString() == "반제품") ||
                            ((sourceRow["ItemTypeName"] ?? "").ToString() == "원자재") ||
                            ((sourceRow["ItemTypeName"] ?? "").ToString() == "부자재"))
                    {
                        // 자재 등록
                        object[] nodeData = new object[24];
                        nodeData[0] = WrGlobal.CorpID;
                        nodeData[1] = gvCM_BOMRev.GetFocusedRowCellValue("ItemCode");
                        nodeData[2] = gvCM_BOMRev.GetFocusedRowCellValue("Revision");
                        nodeData[3] = 0;    // ChildID
                        nodeData[4] = 0;    // ParentID
                        nodeData[5] = 1;    // Qty
                        nodeData[6] = sourceRow["ItemCode"];        // MItemCode
                        nodeData[7] = sourceRow["ItemName"];        // MItemName
                        nodeData[8] = DBNull.Value;     // ProcessCode
                        nodeData[9] = DBNull.Value;     //ProcessName
                        nodeData[10] = DBNull.Value;   // WorkType
                        nodeData[11] = DBNull.Value;   // RoutingSeq
                        nodeData[12] = sourceRow["UnitCode"];   // UnitCode
                        nodeData[13] = DBNull.Value;   // RowNum
                        nodeData[14] = DBNull.Value;   // TreeLevel
                        nodeData[15] = "M";   // ProcessType
                        nodeData[16] = DBNull.Value;   // CreId
                        nodeData[17] = DBNull.Value;   // CreIP
                        nodeData[18] = DBNull.Value;   // CreDt
                        nodeData[19] = DBNull.Value;   // ModId
                        nodeData[20] = DBNull.Value;   // ModIP
                        nodeData[21] = DBNull.Value;   // ModDt
                        nodeData[22] = sourceRow["ItemName"];   // DisplayName
                        nodeData[23] = sourceRow["ItemTypeName"];

                        addNode = BomTreeList.AppendNode(nodeData, destNode);
                        addNode.SetValue("ChildID", ++maxChildID);
                        addNode.SetValue("MItemCode", sourceRow["ItemCode"]);
                        addNode.SetValue("MItemName", sourceRow["ItemName"]);
                        addNode.SetValue("ProcessType", "M");
                        addNode.SetValue("DisplayName", sourceRow["ItemName"]);
                        addNode.SetValue("ItemTypeName", sourceRow["ItemTypeName"]);
                        addNode.SetValue("UnitCode", sourceRow["UnitCode"]);
                        addNode.SetValue("CalcQty", 1);

                        BomTreeList.SelectNode(addNode);

                        if (addNode.ParentNode != null)
                        {
                            addNode.SetValue("ParentID", addNode.ParentNode["ChildID"]);
                            addNode.ParentNode.Expand();
                        }
                    }

                    BomTreeList.EndUpdate();
                    BomTreeList.Columns["DisplayName"].BestFit();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private TreeListNode GetDestNode(Point hitPoint)
        {
            try
            {
                Point pt = new Point();
                try
                {
                    pt = BomTreeList.PointToClient(hitPoint);
                }
                catch { return null; }
                DevExpress.XtraTreeList.TreeListHitInfo ht = BomTreeList.CalcHitInfo(pt);
                TreeListNode destNode = ht.Node;
                if (destNode is TreeListAutoFilterNode)
                    return null;
                return destNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private int CalcDestNodeIndex(DragDropEventArgs e, TreeListNode destNode)
        {
            try
            {
                if (destNode == null)
                    return -1;
                if (e.InsertType == InsertType.AsChild)
                    return -1000;
                var nodes = destNode.ParentNode == null ? BomTreeList.Nodes : destNode.ParentNode.Nodes;
                int index = nodes.IndexOf(destNode);
                if (e.InsertType == InsertType.After)
                    return ++index;
                return index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        private void BomTreeList_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colNodeDel")
                {
                    if (e.Node["ProcessType"].ToString() == "P")
                    {
                        e.RepositoryItem = btnNoDelete;
                    }
                    else
                    {
                        e.RepositoryItem = btnNodeDelete;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNodeDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 삭제 하시겠습니까?", "항목삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                BomTreeList.Nodes.Remove(BomTreeList.FocusedNode);
            }
        }

        private void SetNodeLevel(TreeListNodes nodes)
        {
            if (nodes.Count == 0) return;

            foreach (TreeListNode node in nodes)
            {
                node.SetValue("TreeLevel", node.Level);
                if (node.Nodes.Count > 0)
                {
                    SetNodeLevel(node.Nodes);
                }
            }
        }

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            RefreshBomStockQuantities();
            DisplayBomProduct();
        }

        //저장
        private void cmdSave_Click(object sender, EventArgs e)
        {
            // BOM Revision 저장
            if (BomTreeList.Nodes.Count > 1)
            {
                MessageBox.Show("최상위 계층에는 최종 제품 공정만 존재해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Validate();

                BomTreeList.BeginUpdate();

                SetNodeLevel(BomTreeList.Nodes);

                int cnt = 0;
                //모품번+공정코드: 반제품 새로 넣기
                DataTable dtOri = BomTreeList.DataSource as DataTable;
                DataTable TreeDt = dtOri.Copy();

                for (int i = 0; i < dtOri.Rows.Count;i++)
                {
                    DataRow row = dtOri.Rows[i];
                    if (row.RowState == DataRowState.Deleted) continue;

                    TreeDt.Rows[i].ItemArray = row.ItemArray;
                }

                foreach (DataRow row in TreeDt.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    cnt++;
                    row["RowNum"] = cnt;

                    //공정구분이 조립인 놈들만 반제품 코드 새로 따 줌
                    clsBom bom = new clsBom();
                    bom.ProcessCd = row["ProcessCode"].ToString();   //공정코드
                    string processtitle = bom.Select_ProcessTitle(); //조립공정인지 확인

                    //빈칸에만 자동으로 bom에 넣어줌(모품목코드+공정코드)
                    //if (processtitle != "가공" && row["ItemCode"].ConvertString_Null_To_Empty() == "")
                    if (row["MItemCode"].ConvertString_Null_To_Empty() == "")
                    {
                        string newItemCode = "";
                        string newItemName = "";

                        //01. CM_Item 에 품명+공정코드의 ItemCode가 있는지 확인
                        if (row["MItemCode"].ToString().Contains("-PR"))
                        {
                            newItemCode = row["MItemCode"].ToString();
                            newItemName = row["newItemName"].ToString();
                        }
                        else
                        {
                            newItemCode = row["PItemCode"].ToString() + "-" + row["ProcessCode"].ToString();
                            newItemName = TreeDt.Rows[0]["MItemName"].ToString() + "-" + row["ProcessName"].ToString();
                        }
                        bom.MtCode = newItemCode;
                        DataTable Mdt = bom.Select_Material();

                        //02. 있으면 그대로 BOM에 넣기
                        if (Mdt.Rows.Count != 0)
                        {
                            row["MItemCode"] = newItemCode;
                            row["MItemName"] = newItemName;
                        }
                        else
                        {
                            //03. 없으면 CM_Item테이블에 반제품 코드 새로 만들기 & 그대로 BOM에 넣기

                            //CM_Item
                            CM_Item item = new CM_Item();

                            item.ItemCode = newItemCode;
                            item.ItemName = newItemName;
                            item.ItemType = "30"; //반제품: 30
                            item.StartDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"); ;
                            item.EndDate = "2999-12-31 23:59:59";
                            item.DisplayYn = "N";
                            item.ExamYn = "N";
                            item.UnitCode = TreeDt.Rows[0]["UnitCode"].ConvertString_Null_To_Empty();

                            item.SaftyQty = 0;

                            item.UseYn = "Y";

                            item.Insert_CM_Item();

                            //CM_BOM
                            row["MItemCode"] = newItemCode;
                            row["UnitCode"] = TreeDt.Rows[0]["UnitCode"].ConvertString_Null_To_Empty();
                            row["MItemName"] = newItemName;
                        }
                   }
                }

                foreach (DataRow dr in TreeDt.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted) continue;

                    if (dr.RowState == DataRowState.Added)
                    {
                        dr["CreId"] = WrGlobal.LoginID;
                        dr["CreIP"] = WrGlobal.ClientHostName;
                        dr["CreDt"] = DateTime.Now;
                    }
                    else
                    {
                        dr["ModId"] = WrGlobal.LoginID;
                        dr["ModIP"] = WrGlobal.ClientHostName;
                        dr["ModDt"] = DateTime.Now;
                    }
                    //drv["RowNum"] = ++cnt;
                }

                BomTreeList.EndUpdate();
                TreeDt.AcceptChanges(); //DataRowState 클리어


                if (BomTreeList.Nodes.Count > 0)
                {
                    //저장
                    TreeDt.TableName = "CM_BOM";
                    if(TreeDt.Columns.Contains("DisplayName")) TreeDt.Columns.Remove("DisplayName");
                    if(TreeDt.Columns.Contains("ItemTypeName")) TreeDt.Columns.Remove("ItemTypeName");

                    string ItemCode = (gvCM_BOMRev.GetFocusedRowCellValue("ItemCode") ?? "").ToString();
                    string Revision = (gvCM_BOMRev.GetFocusedRowCellValue("Revision") ?? "").ToString();

                    code.SaveBomTable(ItemCode, Revision, TreeDt);
                }
                else
                {
                    MessageBox.Show("저장할 행이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //저장한거 다시 조회하기
                if (GvProduct.FocusedRowHandle < 0) return;
                productChanged = true;
                DisplayBomRev();
                DisplayRevisionBom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Rdo_ItemDIv_EditValueChanged(object sender, EventArgs e)
        {
            DisplayMaterial();
        }

        //닫기
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
