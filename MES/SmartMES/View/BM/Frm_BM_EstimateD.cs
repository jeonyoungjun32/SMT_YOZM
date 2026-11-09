using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_EstimateD : DevExpress.XtraEditors.XtraForm
    {
        public DataRowView dataRow = null;
        clsCode code = new clsCode();
        //clsSales Sales = new clsSales();
        Database db = new Database();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        private string EstimateNo = "";
        string query = "";
        string cmdType = "";//I:신규, U:수정
        CM_CateItem cm = new CM_CateItem();

        public Frm_BM_EstimateD(string estimateNo)
        {
            InitializeComponent();

            this.EstimateNo = estimateNo;

            if (EstimateNo == "")
            {
                cmdType = "I";
            }
            else
            {
                cmdType = "U";
            }

            Set_Detail(gvDetail);
            SetData();
        }
        private void frmRegisterObtainOrderDetail_Load(object sender, EventArgs e)
        {
            //버튼 설명
            toolTipController1.SetTitle(cmdApplyPrice, "단가 적용 안내");
            toolTipController1.SetToolTip(cmdApplyPrice, "해당 품목의 거래처별 품목단가에 등록되어있는 매출 단가를 불러옵니다.");

            toolTipController1.SetTitle(cmdAdjustPrice, "단가 조정 안내");
            toolTipController1.SetToolTip(cmdAdjustPrice, @"단가 조정 시 숫자를 입력하면 입력값을 기준으로 % 증감이 적용됩니다.
                                                        예) 10 입력 → 기존 단가에서 +10 %
                                                           -5 입력 → 기존 단가에서 -5 %");

            DisplayData();
        }

        #region 디자인
        private void Set_Detail(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = true; // 전체 너비에 맞게 자동 조정
            gv.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom; //행 추가

            //checkbox 추가
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            string[] productType = new string[] { "20" }; //제품유형 : 완제품  
            //RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] { "20" }), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_Material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(productType, false).ToTable(), "품목코드", "품명");

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");


            //Column 셋팅
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "순번", "Seq");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemCode", lup_Material);
            grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "영업비고", "SalesRemark");

            gv.Columns["Seq"].Visible = false;

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);


            //셀값 수정 가능
            grid.Column_AllowEdit(gv.Columns["ItemCode"]);    
            grid.Column_AllowEdit(gv.Columns["Qty"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["SalesRemark"]);

            //하단 셀 합계
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");


            //해당 그리드 이벤트 선언
            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += Event_CellValueChagned;            //셀값 수정 Event
            grid.Set_Navigator(gv);
            gv.InitNewRow += Event_InitNewRow;           

        }

        private void SetData()
        {
            //거래처
            LupCustomer.smtLookUpDataSource = code.GetCustomer_Lup(WrGlobal.CorpID, false);
            LupCustomer.smtLookUpDataDisplayMember = "내용";
            LupCustomer.smtLookUpDataValueMember = "코드";

            //담당자
            LupSalesMan.smtLookUpDataSource = code.GetDataTableUser(false, null, "N");
            LupSalesMan.smtLookUpDataDisplayMember = "명칭";
            LupSalesMan.smtLookUpDataValueMember = "코드";

            //부가세 구분
            DataTable dtExamType = cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, "C010", new string[] { }, false);
            searchVAT.smtLookUpDataSource = dtExamType;
            searchVAT.smtLookUpDataDisplayMember = "분류명";
            searchVAT.smtLookUpDataValueMember = "분류코드";
            searchVAT.smtLookUpValue = "10";    //과세 기본

            Lup_UnitPrice.smtLookUpDataSource = code.GetCM_CateItem(WrGlobal.CorpID, "OOPU");
            Lup_UnitPrice.smtLookUpDataDisplayMember = "명칭";
            Lup_UnitPrice.smtLookUpDataValueMember = "코드";
            Lup_UnitPrice.smtLookUpValue = "10";    // %, 액중 % 기본


            switch (cmdType)
            {
                case "I":
                    Dt_Date.smtDateTimeValue = DateTime.Now;
                    break;

                case "U":
                    BM_EstimateM est = new BM_EstimateM();
                    est.EstimateNo = this.EstimateNo;

                    DataRow dr = est.Select_BM_EstimateM_Row();
                    if (dr != null)
                    {
                        obtainOrderNoTextEdit.smtTextBoxValue = dr["EstimateNo"].ConvertString_Null_To_Empty();
                        Dt_Date.smtDateTimeValue = Convert.ToDateTime(dr["Date"]);
                        EstimateName.smtTextBoxValue = dr["Name"].ConvertString_Null_To_Empty();
                        LupCustomer.smtLookUpValue = dr["CustCode"].ConvertString_Null_To_Empty();
                        smtTotalPrice.smtTextBoxValue = dr["Price"].ConvertString_Null_To_Empty();
                        LupSalesMan.smtLookUpValue = dr["Manager"].ConvertString_Null_To_Empty();
                        remarkTextEdit.smtMemoValue = dr["Remark"].ConvertString_Null_To_Empty();
                    }
                    break;
            }
            DisplayData();
        }

        private void searchVAT_smtLookUpEditValueChanged(object sender, EventArgs e)
        {
            if ((searchVAT.smtLookUpValue ?? "").ToString() == "10")
            {
                VatRate.smtTextBoxValue = "10";
            }
            else
            {
                VatRate.smtTextBoxValue = "0";
            }
        }
        #endregion 디자인 END

        #region 조회
        private void DisplayData()
        {
            BM_EstimateD est = new BM_EstimateD();
            est.EstimateNo = this.EstimateNo;

            gcDetail.DataSource = est.Select_BM_EstimateD();
            gvDetail.BestFitColumns();
        }
        #endregion 조회 END


        #region 버튼 이벤트
        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;
            string btnname = Btn.Name.ToString();

            // 단가 관련 버튼들은 선택된 항목 체크 필수
            if (btnname == "cmdApplyPrice" || btnname == "cmdAdjustPrice")
            {
                // 공통 검증: 선택된 항목이 있는지 확인
                if (!HasSelectedItems())
                {
                    MessageBox.Show("단가를 처리할 품목을 선택해주세요.\n(체크박스를 이용해서 품목을 선택하세요)",
                                  "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            switch (btnname)
            {
                case "cmdSave":
                    cmdSave_Click(sender, e);
                    break;

                case "cmdDelete":
                    cmdDelete_Click(sender, e);
                    break;

                case "cmdClose": // 닫기 버튼
                    cmdClose_Click(sender, e);
                    break;

                // *** 새로 추가된 단가 관련 버튼들 ***
                case "cmdApplyPrice":
                    cmdApplyPrice_Click(sender, e);
                    break;

                case "cmdAdjustPrice":
                    cmdAdjustPrice_Click(sender, e);
                    break;

                // *** 웹양식 업로드 버튼 ***
                case "cmdUpload":
                    cmdUpload_Click(sender, e);
                    break;
            }
        }
        private bool HasSelectedItems()
        {
            try
            {
                // gvDetail 그리드뷰에서 선택된 행 확인
                int[] selectedRows = gvDetail.GetSelectedRows();

                // 선택된 행이 있고, 실제 데이터 행인지 확인 (새 행 제외)
                if (selectedRows.Length > 0)
                {
                    foreach (int rowHandle in selectedRows)
                    {
                        if (rowHandle >= 0) // 0 이상이면 실제 데이터 행
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HasSelectedItems Error: {ex.Message}");
                return false;
            }
        }
        #endregion 버튼 이벤트 END

        #region 엑셀 양식 업로드
        private void cmdUpload_Click(object sender, EventArgs e)
		{
			//    try
			//    {
			//        // 견적서 엑셀 업로드 폼 열기
			//        Frm_ExcelUpLoad_GJY frm = new Frm_ExcelUpLoad_GJY("Estimate");

			//        if (frm.ShowDialog() == DialogResult.OK)
			//        {
			//            UpdateTotalPrice();

			//            MessageBox.Show("엑셀 업로드가 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//        }
			//    }
			//    catch (Exception ex)
			//    {
			//        MessageBox.Show($"엑셀 업로드 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//    }
		}

        #endregion 엑셀 양식 업로드 END 

        #region 단가 관련 버튼 이벤트
        // 단가적용 버튼 클릭 이벤트 
        private void cmdApplyPrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LupCustomer.smtLookUpValue))
                {
                    MessageBox.Show("거래처를 먼저 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 선택된 행들 가져오기
                int[] selectedRows = gvDetail.GetSelectedRows();

                //DialogResult result = MessageBox.Show($"선택된 {selectedRows.Length}개 품목의 단가를 표준단가로 적용하시겠습니까?",
                //                                    "단가적용 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (result != DialogResult.Yes) return;

                // string custCode = LupCustomer.smtLookUpValue; //거래처 조건 제외

                int appliedCount = 0;
                int noPriceCount = 0; // 단가가 없는 품목 개수

                foreach (int rowHandle in selectedRows)
                {
                    if (rowHandle < 0) continue; // 새 행은 제외

                    DataRow dr = gvDetail.GetDataRow(rowHandle);
                    if (dr == null) continue;

                    string ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
                    if (string.IsNullOrEmpty(ItemCode)) continue;

                    // 표준단가 조회
                    CM_ItemPrice price = new CM_ItemPrice();
                    price.ApplyDate = DateTime.Now.ToString("d");
                    price.PriceType = "20"; // 매출단가

                    decimal standardPrice = price.Get_MaterialPrice_byItemCode(ItemCode).ConvertDecimal_Null_To_Zero();


                    // 단가데이터가 없으면 적용할 단가 데이터가 없다는 알림처리 
                    if (standardPrice <= 0)
                    {
                        noPriceCount++;
                        continue; // 단가가 없는 품목은 건너뛰기
                    }

                    dr["UnitPrice"] = standardPrice;

                    // 금액 재계산
                    decimal qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
                    dr["Price"] = qty * standardPrice;

                    // 상태 업데이트 (신규가 아닌 경우)
                    if (dr["Status"].ConvertString_Null_To_Empty() !=  "신규")
                    {
                        dr["Status"] = "수정";
                    }

                    appliedCount++;
                }

                // 그리드 업데이트
                gvDetail.UpdateCurrentRow();
                gvDetail.UpdateSummary();
                gvDetail.RefreshData();

                // 총금액 업데이트
                UpdateTotalPrice();

                // 결과 메시지
                string resultMessage = $"{appliedCount}개 품목의 단가가 적용되었습니다.";
                if (noPriceCount > 0)
                {
                    resultMessage += $"\n{noPriceCount}개 품목은 표준단가 데이터가 없어 적용되지 않았습니다.";
                }
                MessageBox.Show(resultMessage, "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"단가적용 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 단가조정 버튼 클릭 이벤트 
        private void cmdAdjustPrice_Click(object sender, EventArgs e)
        {
            try
            {
                // 선택된 행들 가져오기 (이미 HasSelectedItems()에서 검증됨)
                int[] selectedRows = gvDetail.GetSelectedRows();

                // 조정값 검증
                string adjustValueText = txtAdjustPrice.smtTextBoxValue.Trim(); 

                if (string.IsNullOrEmpty(adjustValueText))
                {
                    MessageBox.Show("조정값을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAdjustPrice.Focus();
                    return;
                }

                // 단가단위 선택값 확인
                string priceUnit = Lup_UnitPrice.smtLookUpValue;
                if (string.IsNullOrEmpty(priceUnit))
                {
                    MessageBox.Show("단가단위를 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 부호 처리 (+/-) 
                bool isIncrease = true; // 기본값: 증가
                if (adjustValueText.StartsWith("+"))
                {
                    adjustValueText = adjustValueText.Substring(1);
                    isIncrease = true;
                }
                else if (adjustValueText.StartsWith("-"))
                {
                    adjustValueText = adjustValueText.Substring(1);
                    isIncrease = false;
                }

                if (!decimal.TryParse(adjustValueText, out decimal adjustValue))
                {
                    MessageBox.Show("올바른 숫자를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAdjustPrice.Focus();
                    return;
                }

                // 단위 텍스트 가져오기
                string unitText = Lup_UnitPrice.smtLookUpValue; // % 또는 액
                string adjustType = priceUnit == "10" ? "퍼센트" : "금액";
                string adjustDirection = isIncrease ? "증가" : "감소";

                DialogResult result = MessageBox.Show(
                    $"선택된 {selectedRows.Length}개 품목의 단가를 {adjustValueText}{adjustType}로 {adjustDirection}시키겠습니까?",
                    "단가조정 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                int adjustedCount = 0;

                foreach (int rowHandle in selectedRows)
                {
                    if (rowHandle < 0) continue;

                    DataRow dr = gvDetail.GetDataRow(rowHandle);
                    if (dr == null) continue;

                    decimal currentPrice = dr["UnitPrice"].ConvertDecimal_Null_To_Zero();
                    if (currentPrice < 0) continue;

                    decimal newPrice = 0;

                    if (priceUnit == "10") // 퍼센트 조정
                    {
                        if (isIncrease)
                        {
                            newPrice = currentPrice * (1 + adjustValue / 100);
                        }
                        else
                        {
                            newPrice = currentPrice * (1 - adjustValue / 100);
                        }
                    }
                    else // 금액 조정 (priceUnit == "20")
                    {
                        if (isIncrease)
                        {
                            newPrice = currentPrice + adjustValue;
                        }
                        else
                        {
                            newPrice = currentPrice - adjustValue;
                        }
                    }

                    // 음수 방지
                    if (newPrice < 0) newPrice = 0;

					// 소수점 반올림 ~> 소수점 한자리
					newPrice = Math.Round(newPrice, 1);

					dr["UnitPrice"] = newPrice;

                    // 금액 재계산
                    decimal qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
                    dr["Price"] = Math.Round(qty * newPrice, 0);

                    // 상태 업데이트 (신규가 아닌 경우)
                    if (dr["Status"].ConvertString_Null_To_Empty() != "신규")
                    {
                        dr["Status"] = "수정";
                    }

                    adjustedCount++;
                }

                // 그리드 업데이트
                gvDetail.UpdateCurrentRow();
                gvDetail.UpdateSummary();
                gvDetail.RefreshData();

                // 총금액 업데이트
                UpdateTotalPrice();

                MessageBox.Show($"{adjustedCount}개 품목의 단가가 조정되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 입력값 초기화
                txtAdjustPrice.smtTextBoxValue = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"단가조정 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 총금액 업데이트 공통 메서드
        /// </summary>
        private void UpdateTotalPrice()
        {
            try
            {
                // 그리드의 Price 컬럼 합계를 가져와서 총금액에 반영
                if (gvDetail.Columns["Price"] != null && gvDetail.Columns["Price"].SummaryItem != null)
                {
                    smtTotalPrice.smtTextBoxValue = gvDetail.Columns["Price"].SummaryItem.SummaryValue.ConvertString_Null_To_Zero();
                }
            }
            catch (Exception ex)
            {
                // 에러 발생 시 무시 (로그만 기록)
                System.Diagnostics.Debug.WriteLine($"UpdateTotalPrice Error: {ex.Message}");
            }
        }

        #endregion 단가 관련 버튼 이벤트

        private void cmdSave_Click(object sender, EventArgs e)
        {
            gvDetail.PostEditor();
            gvDetail.UpdateCurrentRow();

            if (!Check_SaveClick()) return;

            decimal vatPrice = 0;
            decimal totalPrice = 0;

            //부가세 금액 계산
            decimal itemPrice = smtTotalPrice.smtTextBoxValue.ConvertDecimal_Null_To_Zero();
            decimal vatRate = Convert.ToDecimal(VatRate.smtTextBoxValue ?? "0");

            if (searchVAT.smtLookUpValue == "10")  // 과세인 경우만
            {
                vatPrice = Math.Round(itemPrice * vatRate / 100, 0);  // 부가세 계산
            }
            else  // 비과세
            {
                vatPrice = 0;
                totalPrice = itemPrice;
            }

            totalPrice = itemPrice + vatPrice;

            //마스터
            BM_EstimateM estM = new BM_EstimateM();
            estM.CorpId = WrGlobal.CorpID;
            estM.EstimateNo = "ES" + code.GetAutoNumber("견적서", DateTime.Today);
            estM.Date = Dt_Date.smtDateTimeValue.ToString("yyyy-MM-dd");
            estM.Name = EstimateName.smtTextBoxValue;
            estM.CustCode = LupCustomer.smtLookUpValue;
            estM.Price = smtTotalPrice.smtTextBoxValue.ConvertDecimal_Null_To_Zero();
            estM.VatPrice = vatPrice;
            estM.TotalPrice = totalPrice;
            estM.VatType = searchVAT.smtLookUpValue;
            estM.VatRate = VatRate.smtTextBoxValue.ConvertInt_Null_To_Zero();
            estM.Manager = LupSalesMan.smtLookUpValue;
            estM.Remark = remarkTextEdit.smtMemoValue;

            if (cmdType == "I")
            {
                this.EstimateNo = "ES" + code.GetAutoNumber("견적서", (DateTime)Dt_Date.smtDateTimeValue);
                if (!estM.Ins_BM_EstimateM())
                {
                    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return;
                }
            }
            else if (cmdType == "U")
            {
                estM.EstimateNo = this.EstimateNo;

                if (!estM.Upd_BM_EstimateM())
                {
                    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return;
                }
            }

            //디테일 
            BM_EstimateD estD = new BM_EstimateD();
            estD.CorpId = WrGlobal.CorpID;
            estD.EstimateNo = estM.EstimateNo;

            DataTable dt = gcDetail.DataSource as DataTable;
            int maxSeq = 0;

            foreach (DataRow item in dt.Rows)
            {
                int seq = item["Seq"] == DBNull.Value ? 0 : Convert.ToInt32(item["Seq"]);
                if (maxSeq < seq) maxSeq = seq;
            }

            foreach (DataRow item in dt.Rows)
            {
                estD.ItemCode = item["ItemCode"].ConvertString_Null_To_Empty();
                estD.Qty = item["Qty"].ConvertDecimal_Null_To_Zero();
                estD.UnitPrice = item["UnitPrice"].ConvertDecimal_Null_To_Zero();
                estD.Price = item["Price"].ConvertDecimal_Null_To_Zero();
                estD.Remark = item["Remark"].ConvertString_Null_To_Empty();
                estD.SalesRemark = item["SalesRemark"].ConvertString_Null_To_Empty();

                string status = item["Status"].ConvertString_Null_To_Empty();

                if (cmdType == "I")
                {
                    estD.Seq = ++maxSeq;
                    estD.Ins_BM_EstimateD();
                }
                else if (cmdType == "U")
                {
                    estD.EstimateNo = this.EstimateNo;

                    if (status == "신규")
                    {
                        estD.Seq = ++maxSeq;
                        estD.Ins_BM_EstimateD();
                    }
                    else
                    {
                        estD.Seq = item["Seq"].ConvertInt_Null_To_Zero();
                        estD.Upd_BM_EstimateD();
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool Check_SaveClick()
        {
            if ((LupCustomer.smtLookUpValue ?? "").ToString() == "") 
            {
                MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (EstimateName.smtTextBoxValue.ConvertString_Null_To_Empty() == "")
            {
                MessageBox.Show("견적명을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((LupSalesMan.smtLookUpValue ?? "").ToString() == "")
            {
                MessageBox.Show("담당자를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DataTable dt = gcDetail.DataSource as DataTable;

            if (dt == null)
            {
                MessageBox.Show("상세내역을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("상세내역을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ItemCode"].ConvertString_Null_To_Empty() == "")
                {
                    MessageBox.Show("품목을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (dr["Qty"].ConvertInt_Null_To_Zero() == 0)
                {
                    MessageBox.Show("수량을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }


        private void cmdDelete_Click(object sender, EventArgs e)
        {
            GridView gv = gvDetail; 

            if (gv.FocusedRowHandle < 0) return;

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "항목 삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //
                DataRow dr = gvDetail.GetFocusedDataRow();

                BM_EstimateD em = new BM_EstimateD();
                em.EstimateNo = dr["EstimateNo"].ConvertString_Null_To_Empty();

                if (em.EstimateNo != "")
				{
                    em.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
                    em.Del_BM_EstimateDBySeq();
				}

                //
                gv.DeleteRow(gv.FocusedRowHandle);

                DataTable dt = gcDetail.DataSource as DataTable;
                dt.Rows.Remove(dr);

                dt.AcceptChanges();  //DataTable에 삭제 row 반영

                XtraMessageBox.Show("삭제가 완료되었습니다.");
            }
        }

        #region 행 변경 이벤트
        private void Event_CellValueChagned(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            string fieldName = e.Column.FieldName;

            //1. 품목 수정시
            if (fieldName == "ItemCode")
            {
                string custCode = LupCustomer.smtLookUpValue;
                string ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();

                //단가 가져오기
                if (custCode != "")
                {

                    if (custCode != "" && ItemCode != "")
                    {
                        CM_ItemPrice price = new CM_ItemPrice();
                        price.ApplyDate = DateTime.Now.ToString("d"); 
                        price.PriceType = "20";

                        dr["UnitPrice"] = price.Get_MaterialPrice(custCode, ItemCode).ConvertString_Null_To_Zero();
                    }
                }

                ////티피지는 원가 가져옴 : 원가 이력 테이블 단가구분 :'30'
                //if (Database.DbName.ToUpper() == "TPG")
                //{
                //    CM_ItemPriceHistory cph = new CM_ItemPriceHistory();
                //    cph.ApplyDate = DateTime.Now.ToString("d");
                //    cph.PriceType = "30";

                //    dr["UnitPrice"] = cph.Get_MaterialPriceHistory("", ItemCode).ConvertString_Null_To_Zero(); //원가는 거래처 안들어가서 빈칸 처리
                //}
            }
            //2. 수량 및 단가 수정시
            else if (fieldName == "Qty" || fieldName == "UnitPrice")
            {
                //금액 입력
                float Qty = dr["Qty"].ConvertFloat_Null_To_Zero();
                float unitPrice = dr["UnitPrice"].ConvertFloat_Null_To_Zero();

                float price = Qty * unitPrice;

                dr["Price"] = Math.Round(price, 0);

                gv.UpdateCurrentRow();
                gv.UpdateSummary();

                smtTotalPrice.smtTextBoxValue = gv.Columns["Price"].SummaryItem.SummaryValue.ConvertString_Null_To_Zero();
            }

            // 수정 가능한 필드들이 변경되었을 때 Status를 "수정"으로 설정
            string[] editableFields = { "ItemCode", "Qty", "UnitPrice", "Remark" };
            if (editableFields.Contains(fieldName) && dr["Status"].ConvertString_Null_To_Empty() != "신규")
            {
                dr["Status"] = "수정";
            }

            gv.BestFitColumns();
        }

        private void Event_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            gv.SetRowCellValue(e.RowHandle, "Status", "신규");
        }

        #endregion 행 변경 이벤트 END


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}