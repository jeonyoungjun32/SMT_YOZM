using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_SdOrder_MD : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsRepository rpo = new clsRepository();
        clsGrid grid = new clsGrid();

        clsObtainOrder sales = new clsObtainOrder();

        BM_SdOrderM sdorderM = new BM_SdOrderM();
        BM_EstimateM estimateM = new BM_EstimateM();

        public Frm_BM_SdOrder_MD()
        {
            InitializeComponent();
        }

        private void frmObtainView_HS_Load(object sender, EventArgs e)
        {
            InitLup();

            SetGvMaster(GvBM_SdOrderM);
            SetGvDetail(GvBM_SdOrderD);

            DisplayMaster();
        }

        private void InitLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            code.LookupCustomer(WrGlobal.CorpID, "매출", true, "", ref Lup_Cust);
            Lup_Cust.smtLookUpValue = "ALL";
        }

        private void SetGvMaster(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            GvBM_SdOrderM.OptionsView.ShowFooter = true;

            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "거래처", "CustName");
            grid.Create_GvColumn(gv, "창고", "HouseName");
            grid.Create_GvColumn(gv, "수주일자", "ObtainDate");
            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //Format
            grid.SetCol_Type(gv.Columns["Price"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);
            gv.Columns["State"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //합계
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");
        }

        private void gvObtainMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Validate();
            DisplayDetail();
        }

		private void gvObtainMaster_DoubleClick(object sender, EventArgs e)
        {
            if (GvBM_SdOrderM.FocusedRowHandle < 0) return;

            DataRow dr = GvBM_SdOrderM.GetFocusedDataRow();
            if (dr == null) return;

            string state = Convert.ToString(dr["State"]);
            string sdorderno = Convert.ToString(dr["SdOrderNo"]);

            state = sales.GetObtainState(WrGlobal.CorpID, sdorderno);

            if (state == "진행")
            {
                Frm_BM_SdOrderD form = new Frm_BM_SdOrderD("Update", sdorderno);
                form.ShowDialog();
                DisplayMaster();
            }
            else
            {
                MessageBox.Show("진행 상태의 건만 수정이 가능합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvObtainMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("State") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "수주확정")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black ;
                e.Appearance.BackColor = Color.Transparent ;
            }
        }

        private void SetGvDetail(GridView gv)
		{
            GvBM_SdOrderD.Columns.Clear();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            //거래처
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A011"), "코드", "명칭");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");
            //품목
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");

            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수주량", "Qty");

            grid.Create_GvColumn(gv, "단가"   , "UnitPrice");
            grid.Create_GvColumn(gv, "공급가" , "Price");
            grid.Create_GvColumn(gv, "부가세" , "VatPrice");
            grid.Create_GvColumn(gv, "합계"   , "TotalPrice");

            //프로세스 옵션 : 납품처 사용, 미사용 -> 미사용 시 거래처로 자동 입력
            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "Y")
            {
                grid.Create_GvColumn(gv, "납품처", "ShipToCode", lupCust);
            }

            grid.Create_GvColumn(gv, "작업계획", "OrderStatus");
            grid.Create_GvColumn(gv, "발주상태", "PlacingStatus");
            grid.Create_GvColumn(gv, "비고", "Remark");

            RepositoryItemTextEdit txtQty = new RepositoryItemTextEdit();
            txtQty.Mask.EditMask = "g";
            txtQty.Mask.UseMaskAsDisplayFormat = true;

            //Repository
            gv.Columns["ItemName"].ColumnEdit = lup_Item;

            gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //Format
            grid.SetCol_Type(gv.Columns["Qty"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);

            grid.SetCol_Type(gv.Columns["UnitPrice"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], (clsGrid.TypeList)clsGrid.TypeList.TFloat);

            //visible
            gv.Columns["SdOrderNo"].Visible = false;

            grid.Set_Navigator(GvBM_SdOrderD);
			gv.CustomDrawCell += Gv_CustomDrawCell;
        }

		private void Gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName == "OverQty" || fieldName == "OverBoxQty")
            {
                //과부족 - 빨간색 표기
                float value = e.CellValue.ConvertFloat_Null_To_Zero();

                if (value < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            DisplayMaster();
        }

		private void DisplayMaster()
        {
            this.Validate();
            GvBM_SdOrderM.FocusInvalidRow();

            string custCode = Lup_Cust.smtLookUpValue.ToString();
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            BM_SdOrderM sm = new BM_SdOrderM();
            GcBM_SdOrderM.DataSource = sm.Select_BM_SdOrderM( "", startDate, endDate, custCode);
            GvBM_SdOrderM.BestFitColumns();
        }

        private void DisplayDetail()
        {
            DataRow dr = GvBM_SdOrderM.GetFocusedDataRow();

            if (dr == null)
            {
                GcBM_SdOrderD.DataSource = null;
				return;
			}

			//수주
			string orderNo = (GvBM_SdOrderM.GetFocusedRowCellValue("SdOrderNo") ?? "").ToString();

            BM_SdOrderD sd = new BM_SdOrderD();

            GcBM_SdOrderD.DataSource = sd.SelectBM_SdOrderD_ByNo(WrGlobal.CorpID, orderNo);
            GvBM_SdOrderD.BestFitColumns();
		}

  
        private void btn_Ins_Click(object sender, EventArgs e)
        {
            Frm_BM_SdOrderD form = new Frm_BM_SdOrderD("Insert", "");
            form.ShowDialog();
            DisplayMaster();
        }

        private void btn_Upd_Click(object sender, EventArgs e)
        {
            if (GvBM_SdOrderM.FocusedRowHandle < 0) return;

            string sdorderno = GvBM_SdOrderM.GetFocusedRowCellValue("SdOrderNo").ToString();
            string state = sales.GetObtainState(WrGlobal.CorpID, sdorderno);

            if (state == "진행")
            {
                Frm_BM_SdOrderD form = new Frm_BM_SdOrderD("Update", GvBM_SdOrderM.GetFocusedRowCellValue("SdOrderNo").ToString());
                form.ShowDialog();
                DisplayMaster();
            }
            else
            {
                MessageBox.Show("진행 상태의 건만 수정이 가능합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            //자료를 삭제하는 부분
            if (MessageBox.Show("선택한 자료를 삭제 하시겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            try
            {
                string sdOrderNo = Convert.ToString(GvBM_SdOrderM.GetRowCellValue(GvBM_SdOrderM.FocusedRowHandle, "SdOrderNo"));
                string state = sales.GetObtainState(WrGlobal.CorpID, sdOrderNo);

                if (sdOrderNo.Length > 0)
                {
                    if (sales.DeleteCheckBM_SalesD(sdOrderNo))
                    {
                        MessageBox.Show("출고 이력이 있는 주문 건은 삭제할 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (sales.DeleteCheckOrderProcess(sdOrderNo))
                    {
                        if (MessageBox.Show("작업 지시 이력이 있습니다." + Environment.NewLine + "대기 중인 작업은 삭제되고 나머지 작업은 재고 생산으로 변경됩니다." + Environment.NewLine + "진행하겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }//end if
                        else
                        {
                            // 수주 삭제시 작지 적용 쿼리 재작성 필요
                            //sales.UpdateOrderProgressBySdOrderDelete(sdOrderNo);
                        }
                    }

                    if (state == "수주확정")
                    {
                        MessageBox.Show("수주확정된 주문 건은 삭제할 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string estimateNo = "";

					// 삭제전 수주마스터에서 견적번호 조회
					// Estimate 클래스를 사용하여 견적번호 확인
					sdorderM.CorpId = WrGlobal.CorpID;
					sdorderM.SdOrderNo = sdOrderNo;
					estimateNo = sdorderM.Get_EstimateNo();

					if (!string.IsNullOrEmpty(estimateNo))
					{
						if (MessageBox.Show("견적등록된 수주내역입니다." + Environment.NewLine + "해당 견적서의 수주내역도 삭제됩니다." + Environment.NewLine + "진행하시겠습니까?", "견적 연결 확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
						{
							return;
						}
					}

					//삭제
					//수주
					sales.DeleteBM_SdOrderM(GvBM_SdOrderM.GetFocusedRowCellValue("CorpId").ToString(), GvBM_SdOrderM.GetFocusedRowCellValue("SdOrderNo").ToString());

					// 수주 삭제 후 견적 상태 롤백
					if (!string.IsNullOrEmpty(estimateNo))
                    {
                        estimateM.CorpId = WrGlobal.CorpID;
                        estimateM.EstimateNo = estimateNo;
                        estimateM.Upd_BM_EstimateM_State_Rollback();
                    }

                    DisplayMaster();
                }//end if
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            //1. 선택된 자료 확정
            if (GvBM_SdOrderM.GetSelectedRows().Length == 0)
            {
                MessageBox.Show("확정할 수주를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChangeState("수주확정");
            MessageBox.Show("확정되었습니다.");
            DisplayDetail();
        }

        private void btn_Confirm_Cancel_Click(object sender, EventArgs e)
        {
            //1. 선택된 자료 확정 취소
            if (GvBM_SdOrderM.GetSelectedRows().Length == 0)
            {
                MessageBox.Show("확정 취소할 수주를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChangeState("진행");
            MessageBox.Show("확정 취소되었습니다.");
        }

        private void ChangeState(string state)
        {
            //1. 선택된 자료 수주확정/취소
            
            int row;

            for (int rowCnt = 0; rowCnt < GvBM_SdOrderM.SelectedRowsCount; rowCnt++)
            {
                if (GvBM_SdOrderM.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = GvBM_SdOrderM.GetSelectedRows()[rowCnt];
                    string sdOrderNo = GvBM_SdOrderM.GetRowCellValue(row, "SdOrderNo").ToString();
                    
                    string checkYn = "N";

                    BM_SdOrderD sd = new BM_SdOrderD();
                    sd.CorpId = WrGlobal.CorpID;
                    sd.SdOrderNo = sdOrderNo;

                    //2. 확정취소를 하는 경우 BM_SdOrderD에 OrderStatus가 작업확정이 있는지 검사
                    if (state == "수주확정")
                    {
                        sd.OrderStatus = "작업확정";

                        checkYn = sd.GetSdOrderD_OrderStatus()? "Y" : "N";
                    }

					if (checkYn == "N")
					{
                        //마스터 상태 업데이트
                        sdorderM.CorpId = WrGlobal.CorpID;
                        sdorderM.SdOrderNo = sdOrderNo;
                        sdorderM.State = state;
                        sdorderM.Upd_BM_SdOrderM_State();

                        //디테일 상태 업데이트
                        sd.OrderStatus = state;
						sd.Upd_OrderStatus_BySdOrderNo();
					}
					else
					{
						MessageBox.Show("작업확정이 진행되어서 취소할 수 없습니다.");
                    }
                }
            }

            DisplayMaster();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(GvBM_SdOrderM);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}