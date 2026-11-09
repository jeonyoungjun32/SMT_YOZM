using SmartMES.Common;
using System;
using System.Data;
using System.Diagnostics;
using SmartCommon.Common;
using System.Windows.Forms;
using SmartMES.Models;
using DevExpress.XtraEditors;

namespace SmartMES.View.MM
{
    public partial class Frm_MM_Placing_SdOrder : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();

        string query = "";

        clsCode code = new clsCode();
        public Frm_MM_Placing_SdOrder()
        {
            InitializeComponent();
        }

        private void frmOrderPlacing_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Set_Lup();

            DisplayOption();
            DisplayOrder();
        }

        private void Set_Lup()
        {
            //거래처 매입업체만 조회
            query = $"select CustCode 거래처코드, CustName 거래처명 from CM_Cust Where CorpId = {WrGlobal.CorpID} AND CustType = '10' order by CustCode";
            searchCust.DataSource = db.GetDataView("거래처", query);
            searchCust.DisplayMember = "거래처명";
            searchCust.ValueMember = "거래처코드";

            searchCust2.DataSource = db.GetDataView("거래처", query);
            searchCust2.DisplayMember = "거래처명";
            searchCust2.ValueMember = "거래처코드";

            customerBox.smtLookUpDataSource = code.GetCustomer(WrGlobal.CorpID, "10", true).ToTable();
            customerBox.smtLookUpDataDisplayMember = "내용";
            customerBox.smtLookUpDataValueMember = "코드";
            customerBox.smtLookUpValue = "ALL";
            //customerBox.Properties.ShowClearButton = false;

            itemBox.smtLookUpDataSource = code.GetMaterialList(new string[] {"10", "20", "30", "40", "50", "60", "70" },true).ToTable();
            itemBox.smtLookUpDataDisplayMember = "품명";
            itemBox.smtLookUpDataValueMember = "품목코드";
            itemBox.smtLookUpValue = "ALL";
            //itemBox.Properties.ShowClearButton = false;

            //담당자
            searchUser.DataSource = code.GetUser();
            searchUser.DisplayMember = "사용자명";
            searchUser.ValueMember = "ID";

            code.LookupCategory(ref searchVatType, "C010");         // 과세구분
            code.LookupCategory(ref searchVatType2, "C010");         // 과세구분
            code.LookupCategory(ref searchVatType3, "C010");         // 과세구분
            code.LookupCategory(ref searchPlacingType, "I010", null);       // 발주구분
            code.LookupCategory(ref searchPlacingType2, "I010", null);       // 발주구분
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvMaterial.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }//end fnction
        }

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayOption();
            DisplayOrder();
            DisplayData();
        }

        private void gvOrder_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DisplayData();
        }

        private void tabbedData_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            DisplayOption();
            DisplayOrder();
        }

        private void DisplayOption()
        {
            try
            {
                if (tabbedData.SelectedTabPageIndex == 0)   //발주대기
                {
                    if (tabbedOption.SelectedTabPageIndex == 0) //Option 거래처
                    {
                        query = $"Exec Placing_GetObtainOption '{WrGlobal.CorpID}', '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '거래처', '계획확정'";
                        gcCustomer.DataSource = db.GetDataView("거래처목록", query);
                        gvCustomer.BestFitColumns();
                    }
                    else //Option 품목
                    {
                        query = $"Exec Placing_GetObtainOption '{WrGlobal.CorpID}', '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '품목', '계획확정'";
                        gcMaterialOption.DataSource = db.GetDataView("품목목록", query);
                        gvMaterialOption.BestFitColumns();
                    }
                }
                else //발주완료
                {
                    if (tabbedOption.SelectedTabPageIndex == 0) //Option 거래처
                    {
                        query = $"Exec Placing_GetObtainOption '{WrGlobal.CorpID}', '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '거래처', '발주완료'";
                        gcCustomer.DataSource = db.GetDataView("거래처목록", query);
                        gvCustomer.BestFitColumns();
                    }
                    else //Option 품목
                    {
                        query = $"Exec Placing_GetObtainOption '{WrGlobal.CorpID}', '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '품목', '발주완료'";
                        gcMaterialOption.DataSource = db.GetDataView("품목목록", query);
                        gvMaterialOption.BestFitColumns();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayOrder()
        {
            try
            {
                if (tabbedData.SelectedTabPageIndex == 0)   //발주대기
                {
                    gvOrder.FocusInvalidRow();

                    //string custCode = gvCustomer.GetFocusedRowCellValue("CustCode").ToString();
                    //string ItemCode = gvMaterialOption.GetFocusedRowCellValue("ItemCode").ToString();
                    string custCode = customerBox.smtLookUpValue.ToString();
                    string ItemCode = itemBox.smtLookUpValue.ToString();
                    query = $@"Exec Placing_OrderList '{WrGlobal.CorpID}', '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '{custCode}', '{ItemCode}', '발주대기'";

                    gcOrder.DataSource = db.GetDataView("수주목록", query);
                    gvOrder.BestFitColumns();
                }
                else        //발주완료
                {
                    gvOrderPlacing.FocusInvalidRow();

                    //string custCode = gvCustomer.GetFocusedRowCellValue("CustCode").ToString();
                    //string ItemCode = gvMaterialOption.GetFocusedRowCellValue("ItemCode").ToString();
                    string custCode = customerBox.smtLookUpValue.ToString();
                    string ItemCode = itemBox.smtLookUpValue.ToString();
                    //query = $@"Exec Placing_OrderList '{WrGlobal.CorpID}', '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '{custCode}', '{ItemCode}'";
					query = $"Exec Placing_GetOrderPlacingList '{WrGlobal.CorpID}','{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '{custCode}','{ItemCode}'";

					gcOrderPlacing.DataSource = db.GetDataView("발주완료", query);
                    gvOrderPlacing.BestFitColumns();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayData()
        {
            try
            {
                if (tabbedData.SelectedTabPageIndex == 0)
                {
                    //BOM 자재 리스트
                    string sdOrderNo = gvOrder.FocusedRowHandle < 0 ? "" : gvOrder.GetFocusedRowCellValue("SdOrderNo").ToString();
                    string sdOrderSeq = gvOrder.FocusedRowHandle < 0 ? "" : gvOrder.GetFocusedRowCellValue("SdOrderSeq").ToString();

                    if (sdOrderNo == "" || sdOrderSeq == "") return;

                    query = $@"Exec Placing_OrderBOMMaterial2 '{WrGlobal.CorpID}', '{sdOrderNo}', {sdOrderSeq}";

                    gcMaterial.DataSource = db.GetDataView("필요자재", query);

                    String orderDeliveryDate = gvOrder.GetFocusedRowCellValue("DeliveryDate").ToString();
                    DataRow dr;

                    for (int rowCnt = 0; rowCnt < gvMaterial.RowCount; rowCnt++)
                    {
                        //담당자 기본 설정
                        dr = gvMaterial.GetDataRow(rowCnt);
                        dr["WriteID"] = WrGlobal.LoginID;
                    }
                }
                //else //발주완료 리스트
                //{
                //    string custCode = customerBox.smtLookUpValue.ToString();
                //    string ItemCode = itemBox.smtLookUpValue.ToString();

                //    query = $"Exec Placing_GetOrderPlacingList '{WrGlobal.CorpID}','{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', '{custCode}','{ItemCode}'";

                //    gcOrderPlacing.DataSource = db.GetDataView("발주완료", query);
                //    gvOrderPlacing.BestFitColumns();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvMaterial_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            DataRow drM = gvMaterial.GetFocusedDataRow();
            gvMaterial.FocusInvalidRow();

            if (e.Column.FieldName == "VatType")
            {
                if (drM["VatType"].ToString() != "10")  //과세 아닐 경우 과세율 0
                {
                    drM["VatRate"] = 0;
                }
                else
                {
                    drM["VatRate"] = 10;
                }

                calculate(drM);
                return;
            }

            if (e.Column.FieldName != "UnitPrice") return;

            calculate(drM);
        }

        private void calculate(DataRow drM)
        {//금액계산

            if ((drM["Qty"] ?? "").ToString() == "") drM["Qty"] = 0;
            if ((drM["UnitPrice"] ?? "").ToString() == "") drM["UnitPrice"] = 0;

            drM["Price"] = Convert.ToDecimal(drM["Qty"]) * Convert.ToDecimal(drM["UnitPrice"]);
            drM["VatPrice"] = Math.Round(Convert.ToDecimal(drM["Price"]) * Convert.ToDecimal(drM["VatRate"]) / 100);

            drM["TotalPrice"] = Convert.ToDecimal(drM["Price"]) + Convert.ToDecimal(drM["VatPrice"]);
        }

        private void InsPlacingBtn_Click(object sender, EventArgs e)
        {
            //발주하기
            this.Validate();

            if (gvMaterial.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("자재리스트에서 선택한 내역이 없습니다.");
                return;
            }

            if (MessageBox.Show("필요 자재를 발주 하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string custCode, sdOrderNo, sdOrderSeq, ItemCode, placingDate, deliveryDate, unitCode, qty, unitPrice, price, vatPrice, totalPrice, vatType, vatRate, writeID, customerUser, customerTel, placingType, remark;

            sdOrderNo = gvOrder.GetFocusedDataRow()["SdOrderNo"].ToString();
            sdOrderSeq = gvOrder.GetFocusedDataRow()["SdOrderSeq"].ToString();

            try
            {
                foreach (int idx in gvMaterial.GetSelectedRows())
                {
                    DataRow dr = gvMaterial.GetDataRow(idx);
                    custCode = dr["CustCode"].ToString();
                    ItemCode = dr["ItemCode"].ToString();
                    placingDate = ((DateTime)dr["PlacingDate"]).ToString("yyyyMMdd");
                    deliveryDate = ((DateTime)dr["DeliveryDate"]).ToString("yyyyMMdd");
                    unitCode = dr["UnitCode"].ToString();
                    qty = dr["Qty"].ToString();
                    unitPrice = dr["UnitPrice"].ConvertString_Null_To_Zero();
                    price = dr["Price"].ToString();
                    vatPrice = dr["VatPrice"].ToString();
                    vatType = dr["VatType"].ToString();
                    vatRate = dr["VatRate"].ToString();
                    totalPrice = dr["TotalPrice"].ToString();
                    placingType = dr["PlacingType"].ToString();
                    customerUser = dr["CustomerUser"].ToString();
                    customerTel = dr["CustomerTel"].ToString();
                    writeID = dr["WriteID"].ToString();
                    remark = dr["pRemark"].ToString();

                    if (custCode == "")
                    {
                        MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (placingType == "")
                    {
                        MessageBox.Show("발주타입을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (deliveryDate == "")
                    {
                        MessageBox.Show("납기일자를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (placingDate == "")
                    {
                        MessageBox.Show("발주일자를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (unitPrice == "0")
                    {
                        MessageBox.Show("단가를 입력해주세요");
                        return;
                    }

                    query = $@"exec Placing_MakeOrderPlacing '{WrGlobal.CorpID}','{custCode}', '{sdOrderNo}', '{sdOrderSeq}', 
                                '{placingType}', '{ItemCode}','{placingDate}','{deliveryDate}',
                                '{unitCode}', {qty}, {unitPrice}, {price}, {vatPrice}, {totalPrice},
                                '{vatType}', {vatRate}, '{writeID}'
                                ,'{customerUser}','{customerTel}','{remark}','{WrGlobal.LoginID}'";

                    db.ExecuteQuery(query);
                }

                DisplayOption();
                DisplayOrder();
                DisplayData();

                MessageBox.Show("발주 처리를 완료하였습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //발주취소
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (gvOrderPlacing.SelectedRowsCount <= 0) return;

            string mmOrderNo, mmOrderSeq, sdOrderNo, sdOrderSeq;

            try
            {
                foreach (int idx in gvOrderPlacing.GetSelectedRows())
                {
                    DataRow dr = gvOrderPlacing.GetDataRow(idx);
                    if (dr["State"].ToString() == "완료")
                    {
                        MessageBox.Show("완료된 건은 취소가 불가능합니다.");
                        return;
                    }

                    mmOrderNo = dr["MmOrderNo"].ToString();
                    mmOrderSeq = dr["MmOrderSeq"].ToString();
                    sdOrderNo = dr["SdOrderNo"].ToString();
                    sdOrderSeq = dr["SdOrderSeq"].ToString();

                    query = $"Exec Placing_CancelOrderPlacing '{WrGlobal.CorpID}','{mmOrderNo}','{mmOrderSeq}','{sdOrderNo}','{sdOrderSeq}','{WrGlobal.LoginID}'";

                    db.ExecuteQuery(query);
                }
                DisplayOption();
                DisplayOrder();
                DisplayData();
                MessageBox.Show("발주를 취소하였습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //발주제외
        private void button3_Click(object sender, EventArgs e)
        {
            if (gvOrder.SelectedRowsCount <= 0) return;

            if (MessageBox.Show("발주 대상에서 제외하시겠습니까?", "확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string sdOrderNo, sdOrderSeq;

            try
            {
                foreach (int idx in gvOrder.GetSelectedRows())
                {
                    DataRow dr = gvOrder.GetDataRow(idx);
                    sdOrderNo = dr["SdOrderNo"].ToString();
                    sdOrderSeq = dr["SdOrderSeq"].ToString();

                    query = $"update BM_SdOrderD set PlacingStatus = '발주제외' where SdOrderNo = '{sdOrderNo}' AND SdOrderSeq = '{sdOrderSeq}'";

                    db.ExecuteQuery(query);
                }
                MessageBox.Show("발주 대상에서 제외했습니다.");
                DisplayOption();
                DisplayOrder();
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
	}
}