using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SMT_MES.View.Business.Rpt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_Estimate : DevExpress.XtraEditors.XtraForm
    {
        public DataRowView dataRow = null;
        clsCode code = new clsCode();
        Database db = new Database();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        CM_CateItem cm = new CM_CateItem();
        string query = "";
        string[] ItemType = WrGlobal.Business_ItemType;
        string[] CustDiv = { "20", "40" }; //매출, 매입매출

        RepositoryItemSearchLookUpEdit lup_Cust;
        RepositoryItemSearchLookUpEdit lup_Emp;
        RepositoryItemSearchLookUpEdit lup_Material;
        RepositoryItemSearchLookUpEdit lup_VAT;

        BM_EstimateM estM = new BM_EstimateM();
        BM_EstimateD estD = new BM_EstimateD();

        public Frm_BM_Estimate()
        {
            InitializeComponent();
        }

        private void frmComplain_HS_Load(object sender, EventArgs e)
        {
            clsDateFactory.SetDateRange(Dt_Date, DateRangeType.CurrentDatePlusDays, -7); // 현재 날짜 + 7일
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);

            // 버튼 보여지기 유무
            Set_BtnVisible();

            Set_SearchLup();

            Set_Mater(Gv_EstimateMater);
            Set_Detail(Gv_BM_EstimateD);
            
            Func_Search();
        }

        private void Set_BtnVisible()
        {
        
        }

        private void Set_SearchLup()
		{
            //1. 조회조건 서치룩업 데이터바인딩
            LupCustomer.smtLookUpDataSource = code.GetCustomer_Lup(WrGlobal.CorpID, true);
            LupCustomer.smtLookUpDataDisplayMember = "내용";
            LupCustomer.smtLookUpDataValueMember = "코드";
            LupCustomer.smtLookUpValue = "ALL";

            //2. Gird 서치룩업
            lup_Cust = rpo.Create_RepositoryItemSearchLookUp(code.Select_Customer_ByKey(), "거래처코드", "거래처명");
            lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.Get_UserList(), "코드", "코드명");
            lup_Material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(ItemType), "품목코드", "품명");
            CM_CateItem item = new CM_CateItem();
            item.Class = "C010";
            lup_VAT = rpo.Create_RepositoryItemSearchLookUp(item.Select_LupItem(), "분류코드", "분류명");
        }

        private void Set_Mater(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            //gv.OptionsView.ColumnAutoWidth = true; //그리드 전체 너비에 맞게 자동 조정, 컬럼 내용 짤림
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //컬럼 헤더 멀티라인 쓸때 사용
            //gv.OptionsView.AllowHtmlDrawHeaders = true;
            //gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //grid.Create_GvColumn(gv, "견적<Br>번호", "EstimateNo"); //<- 예시

            gv.IndicatorWidth = 50;
            gv.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            //Column 셋팅
            grid.Create_GvColumn(gv, "견적번호", "EstimateNo");
            grid.Create_GvColumn(gv, "견적일자", "Date");
            grid.Create_GvColumn(gv, "진행상황", "State");
            grid.Create_GvColumn(gv, "견적명", "Name");
            grid.Create_GvColumn(gv, "업체명", "CustCode", lup_Cust);
            grid.Create_GvColumn(gv, "견적금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "견적등록자", "Manager", lup_Emp);
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "부가세유형", "VatType", lup_VAT);
            grid.Create_GvColumn(gv, "부가세율", "VatRate");
            grid.Create_GvColumn(gv, "생성자ID", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "생성자IP ", "CreIP");
            grid.Create_GvColumn(gv, "생성일자 ", "CreDt");
            grid.Create_GvColumn(gv, "수정자ID", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정자IP ", "ModIP");
            grid.Create_GvColumn(gv, "수정일자 ", "ModDt");

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Date"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //셀값 수정 가능
            //grid.Column_AllowEdit(gv.Columns["CustCode"]);    

            //gv.Columns["VatPrice"].Visible = false;
            //gv.Columns["TotalPrice"].Visible = false;

            //하단 셀 합계
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            gv.BestFitColumns();

            //해당 그리드 이벤트 선언
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

        private void Set_Detail(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            //gv.OptionsView.ColumnAutoWidth = true; //그리드 전체 너비에 맞게 자동 조정, 컬럼 내용 짤림
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.IndicatorWidth = 50;

            //컬럼 헤더 멀티라인 쓸때 사용
            //gv.OptionsView.AllowHtmlDrawHeaders = true;
            //gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //grid.Create_GvColumn(gv, "견적<Br>번호", "EstimateNo"); //<- 예시

            //Column 셋팅
            grid.Create_GvColumn(gv, "견적번호", "EstimateNo");
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemCode", lup_Material);
            grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "영업비고", "SalesRemark");
            grid.Create_GvColumn(gv, "생성자ID", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "생성자IP ", "CreIP");
            grid.Create_GvColumn(gv, "생성일자 ", "CreDt");
            grid.Create_GvColumn(gv, "수정자ID", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정자IP ", "ModIP");
            grid.Create_GvColumn(gv, "수정일자 ", "ModDt");

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);

            //셀값 수정 가능
            //grid.Column_AllowEdit(gv.Columns["CustCode"]);    

            //하단 셀 합계
            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");

            // 최소 넓이 사이즈 
            gv.Columns["Size"].MinWidth = 80;

            gv.BestFitColumns();

            //해당 그리드 이벤트 선언
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            estD.EstimateNo = Convert.ToString(Gv_EstimateMater.GetFocusedRowCellValue("EstimateNo"));
            Gc_BM_EstimateD.DataSource = estD.Select_BM_EstimateD();

            Gv_BM_EstimateD.BestFitColumns();
        }

        private void Func_Search()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            //est.State = "대기"; //구분자 대기/수주 공통코드 작업 후 라디오 버튼으로 새로 만들 예정 
            estM.CustCode = LupCustomer.smtLookUpValue;

            Gc_EstimateMater.DataSource = estM.Select_BM_EstimateM(startDate, endDate);
            Gv_EstimateMater.BestFitColumns();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdInsert":
                    Frm_BM_EstimateD frm = new Frm_BM_EstimateD("");
                    frm.ShowDialog();
                    Func_Search();
                    break;

                case "cmdUpdate":
                    DataRow dr = Gv_EstimateMater.GetFocusedDataRow();
                    if (dr == null) return;

                    string estimateNo = dr["EstimateNo"].ConvertString_Null_To_Empty();

                    Frm_BM_EstimateD form = new Frm_BM_EstimateD(estimateNo);
                    form.ShowDialog();
                    Func_Search();
                    break;

                case "cmdOrder": //수주전환
                    Estimate_ChangeOrder();
                    break;

                case "cmdDelete":
                    Estimate_Delete();
                    break;

                case "cmdExcel":
                    clsFunction func = new clsFunction();
                    func.ExportExcel(Gv_EstimateMater);
                    break;

                case "cmdClose":
                    this.Close();
                    break;

                case "cmdInspectionRpt":
                    Print_Rpt();
                    break;
            }
        }

        private void Print_Rpt()
        {
            string strEstimateNo = string.Empty;

            int[] selectedRowHandles = Gv_EstimateMater.GetSelectedRows();

            if (selectedRowHandles.Length == 0)
            {
                MessageBox.Show("선택된 항목이 없습니다.");
                return;
            }

            foreach (int i in selectedRowHandles)
            {
                strEstimateNo = strEstimateNo + "'" + Gv_EstimateMater.GetRowCellValue(i, "EstimateNo") + "',";
            }

            strEstimateNo = strEstimateNo.Substring(0, strEstimateNo.Length - 1);

            IReport rpt = null;

            //if (Database.DbName.Contains("TPG"))
			{
                rpt = new Rpt_Estimate_TPG(strEstimateNo);
			}

            if (rpt != null)
            { 
                ReportPrintTool reportTool = new ReportPrintTool(rpt);
                reportTool.ShowPreviewDialog();
            }
        }

        private void Estimate_Delete()
        {
            DataRow dr = Gv_EstimateMater.GetFocusedDataRow();
            if (dr == null) return;

            string estimateNo = dr["EstimateNo"].ToString();

            DialogResult result = MessageBox.Show("선택한 견적을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;


            estD.EstimateNo = estimateNo;
            estD.Del_BM_EstimateD();

            estM.EstimateNo = estimateNo;
            estM.Del_BM_EstimateM();

            Func_Search();
            Gv_BM_EstimateD.RefreshData();
            MessageBox.Show(cls_MessageBox.Del_Comp);
        }

        private void Estimate_ChangeOrder()
        {
            //string sdOrderNo = "SO" + code.GetAutoNumber("수주번호", DateTime.Today);
            int[] selectedRows = Gv_EstimateMater.GetSelectedRows();
            if (selectedRows == null && selectedRows.Length == 0)
            {
                MessageBox.Show("수주전환할 견적을 선택해주세요.\n(체크박스를 이용해서 견적을 선택하세요)",
                                "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<string> alreadyOrderedList = new List<string>();   
            List<DataRow> validEstimates = new List<DataRow>();

            foreach (int rowHandle in selectedRows)
            {
                DataRow row = Gv_EstimateMater.GetDataRow(rowHandle);
                if (row == null) continue;

                string state = row["State"].ConvertString_Null_To_Empty();
                string estimateNo = row["EstimateNo"].ConvertString_Null_To_Empty();

                if (row != null)
                {
                    if (state == "수주")
                    {
                        alreadyOrderedList.Add(row["EstimateNo"].ToString());
                    }
                    else if (state == "대기") // 수주전환 가능한 상태
                    {
                        validEstimates.Add(row);
                    }
                }
            }

            if (alreadyOrderedList.Count > 0)
            {
                string message = $"다음 견적은 이미 수주전환되어 제외됩니다:\n{string.Join(", ", alreadyOrderedList)}";
                MessageBox.Show(message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult result = MessageBox.Show($"선택된 {validEstimates.Count}개 견적을 수주전환하시겠습니까?",
                                                    "수주전환 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
            }

            // <수주전환 처리>
            int successCount = 0;
            List<string> errorList = new List<string>();

            foreach (DataRow item in validEstimates)
            {
                try
                {
                    //수주마스터 저장
                    //GridView view = Gv_EstimateMater as GridView;
                    //if (view == null || view.FocusedRowHandle < 0) return;

                    //string state = (Gv_EstimateMater.GetFocusedRowCellDisplayText("State") ?? "").ToString();
                    //if (state == "수주")
                    //{
                    //    MessageBox.Show("이미 수주전환된 견적입니다.");
                    //    return;
                    //}

                    //DataRow item = view.GetDataRow(view.FocusedRowHandle);

                    string estimateNo = item["EstimateNo"].ConvertString_Null_To_Empty();

                    //Price랑 VatType, VatRate 가 null이면 안되게 조건 추가
                    if (item["Price"] == DBNull.Value || item["VatType"] == DBNull.Value || item["VatRate"] == DBNull.Value)
                    {
                        errorList.Add($"{estimateNo}: 견적금액, 부가세유형, 부가세율이 입력되지 않았습니다.");
                        continue;
                    }
                    string sdOrderNo = "SO" + code.GetAutoNumber("수주번호", DateTime.Today);

                    BM_SdOrderM sm = new BM_SdOrderM();
                    sm.CorpId = WrGlobal.CorpID;
                    sm.SdOrderNo = sdOrderNo;
                    sm.SoldToCode = item["CustCode"].ConvertString_Null_To_Empty();
                    sm.ObtainDate = DateTime.Today.ToString("yyyy-MM-dd");
                    //sm.DeliveryDate = Convert.ToString(deliveryDateDateEdit.DateTime.ToString("d"));
                    sm.Price = Convert.ToString(item["Price"]).ConvertDecimal_Null_To_Zero();
                    sm.VatPrice = Convert.ToString(item["VatPrice"]).ConvertDecimal_Null_To_Zero();
                    sm.TotalPrice = Convert.ToString(item["TotalPrice"]).ConvertDecimal_Null_To_Zero();

                    sm.OrderType = "10";    // CM_CateItem : S020	
                    sm.VatType = item["VatType"].ConvertString_Null_To_Empty();
                    sm.VatRate = item["VatRate"].ConvertInt_Null_To_Zero();
                    sm.SalesMan = item["Manager"].ConvertString_Null_To_Empty();
                    sm.OutState = "대기";
                    sm.Remark = item["Remark"].ConvertString_Null_To_Empty();
                    //sm.HouseID = Convert.ToString(houseIDLookUpEdit.EditValue);

                    // 광주요에서는 자동 수주확정처리 
                    if (Database.DbName.Contains("GJY")) sm.State = "수주확정";
                    else sm.State = "진행";
                    
                    sm.EstimateNo = item["EstimateNo"].ConvertString_Null_To_Empty();
                    sm.Ins_BM_SdOrderM();

                    //DataTable Detail = Gv_BM_EstimateD.GridControl.DataSource as DataTable;

                    //견적 디테일 조회
                    estD.EstimateNo = estimateNo;
                    DataTable Detail = estD.Select_BM_EstimateD();


                    // 유효하지 않은 디테일이 있는지 먼저 체크
                    bool hasInvalidDetail = false;
                    string invalidDetailInfo = "";

                    foreach (DataRow dr in Detail.Rows)
                    {
                        string ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
                        decimal qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
                        decimal unitPrice = dr["UnitPrice"].ConvertDecimal_Null_To_Zero();
                        decimal price = dr["Price"].ConvertDecimal_Null_To_Zero();

                        // 품목코드가 있는데 단가와 금액이 모두 0인 경우
                        if (!string.IsNullOrEmpty(ItemCode) && qty > 0 && unitPrice <= 0 && price <= 0)
                        {
                            hasInvalidDetail = true;
                            invalidDetailInfo = $"품목: {ItemCode}, 순번: {dr["Seq"]}";
                            break;
                        }
                    }

                    if (hasInvalidDetail)
                    {
                        errorList.Add($"{estimateNo}: 단가와 금액이 입력되지 않은 품목이 있습니다. ({invalidDetailInfo})");
                        continue;
                    }

                    //수주디테일 저장
                    foreach (DataRow dr in Detail.Rows)
                    {
                        BM_SdOrderD sd = new BM_SdOrderD();

                        sd.CorpId = WrGlobal.CorpID;
                        sd.SdOrderNo = sdOrderNo;
                        sd.SdOrderSeq = Convert.ToInt32(dr["Seq"]);

                        sd.ItemCode = Convert.ToString(dr["ItemCode"]);
                        //sd.Size = Convert.ToString(item["Size"]);
                        //sd.UnitCode = Convert.ToString(item["UnitCode"]);
                        sd.Qty = Convert.ToDecimal(dr["Qty"]);
                        //sd.ShipToCode = Convert.ToString(item["ShipToCode"]);
                        sd.UnitPrice = Convert.ToDecimal(dr["UnitPrice"]);
                        sd.Price = Convert.ToDecimal(dr["Price"]);
                        //sd.VatPrice = Convert.ToDecimal(item["VatPrice"]);        //견적 디테일 부가세 없어서 보류
                        //sd.TotalPrice = Convert.ToDecimal(item["TotalPrice"]);
                        //sd.DeliveryDate = Convert.ToDateTime(item["DeliveryDate"]);
                        sd.OrderStatus = "대기";
                        sd.PlacingStatus = "발주대기";
                        sd.Remark = Convert.ToString(dr["Remark"]);
                        //sd.VatType = Convert.ToString(item["VatType"]);
                        //sd.VatRate = Convert.ToInt32(item["VatRate"]);
                        sd.State = "대기";
                        sd.Ins_BM_SdOrderD();
                    }

                    // 견적상태 업데이트 (대기 -> 수주)
                    //DataRow es = Gv_EstimateMater.GetFocusedDataRow();
                    estM.CorpId = WrGlobal.CorpID;
                    estM.EstimateNo = estimateNo;
                    estM.Upd_BM_EstimateM_State();

                    successCount++;
                }
                catch (Exception ex)
                {
                    string estimateNo = item["EstimateNo"].ConvertString_Null_To_Empty();
                    errorList.Add($"{estimateNo}: {ex.Message}");
                }
            }
            // 결과 메시지 개선
            string resultMessage = "";
            if (successCount > 0)
            {
                resultMessage = $"{successCount}건의 견적이 성공적으로 수주전환되었습니다.";
            }

            if (errorList.Count > 0)
            {
                if (successCount > 0)
                {
                    resultMessage += $"\n\n실패: {errorList.Count}건\n{string.Join("\n", errorList)}";
                }
                else
                {
                    resultMessage = $"수주전환 실패: {errorList.Count}건\n{string.Join("\n", errorList)}";
                }
            }

            if (successCount == 0 && errorList.Count == 0)
            {
                resultMessage = "처리할 견적이 없습니다.";
            }

            MessageBox.Show(resultMessage, "수주전환 결과", MessageBoxButtons.OK,
                           errorList.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            Func_Search();
        }
    }
}