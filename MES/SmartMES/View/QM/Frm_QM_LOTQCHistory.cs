using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.PM;
using SmartMES.Models.QM;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_LOTQCHistory : DevExpress.XtraEditors.XtraForm
    {

        Database db = new Database();
        string query = "";

        WrComFunc func = new WrComFunc();
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        cls_Lookup clsLup = new cls_Lookup();

        // 품목구분 리스트
        // 10 : 상품, 20 : 제품, 30 : 반제품, 40 : 구매자재, 50 : 부자재, 60 : 샘플/개발
        private string[] ItemDiv = { "20", "30" };

        private readonly Color dangerColor = Color.FromArgb(201, 82, 82);

        decimal GoalNgPer = 0.80M; /* 목표 불량률 : CM_Class 'A005010' ExtraValue 기준 */

        public Frm_QM_LOTQCHistory()
        {
            InitializeComponent();
        }

        private void frmQM_WorkDefectView_Load(object sender, EventArgs e)
        {
            Init();

            SetLup();
            Set_GvOrderResult(GvOrderResult);
            Set_GvRework(GvQM_WorkDefect);
            Set_GvExam(GvExam);
            Set_GvLotList(GvList);

            Search_Lot();
        }

        private void Init()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            DataRow drClass = code.GetClassInfo("A005010");

            if (drClass != null)
            {
                decimal goalNgPer = drClass["ExtraValue"].ConvertDecimal_Null_To_Zero();
                if (goalNgPer != 0) GoalNgPer = goalNgPer;
            }
        }

        private void SetLup()
        {
            // 공정
            DataTable dtProcess = code.GetProcess_List_ProcessType(true, null);

            clsLup.Create_RepositoryItemSearchLookUp(Lup_Process, dtProcess, "공정코드", "공정명");
            Lup_Process.smtLookUpValue = "ALL";

            // 설비
            DataTable dtMachine = code.GetMachineList(true);
            clsLup.Create_RepositoryItemSearchLookUp(LupMachine, dtMachine, "설비코드", "설비명");

            LupMachine.smtLookUpValue = "ALL";

            // 품목
            DataTable dtItem = code.GetMaterialList(ItemDiv, true).Table;

            clsLup.Create_RepositoryItemSearchLookUp(Lup_Item, dtItem, "품목코드", "품명");
            Lup_Item.smtLookUpValue = "ALL";
        }

        private void Set_GvOrderResult(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowAutoFilterRow = true;

            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.ReadOnly = true;

            gv.OptionsFind.AlwaysVisible = true;

            //RepositoryItemLookUpEdit lupPass = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q020"), "코드", "명칭");
            //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", new string[] { "10,20,30" }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", new string[] { "10,20,30" }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", new string[] { "10,20,30" }), "코드", "명칭");

            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "LotNo", "SerialNo");
            grid.Create_GvColumn(gv, "생산일자", "WorkDate");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "불량률", "NgPer");
            grid.Create_GvColumn(gv, "작업자", "WorkerID");
            grid.Create_GvColumn(gv, "공정검사유무", "ChkProc");

            if (gv.Columns["ItemClass1"] != null) { gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1; }
            if (gv.Columns["ItemClass2"] != null) { gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd1; }
            if (gv.Columns["ItemClass3"] != null) { gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd1; }

            //
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgPer"], clsGrid.TypeList.TFloatN2);
            gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["SerialNo"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["WorkerID"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["ChkProc"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //
            grid.RowNumber_Indicator(gv);
            gv.RowCellStyle += Gv_RowCellStyle;
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged1;
        }

        private void Gv_FocusedRowObjectChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            Search_Detail();
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Column.FieldName == "NgPer")
            {
                decimal defectRate = GvQM_WorkDefect.GetRowCellValue(e.RowHandle, e.Column.FieldName).ConvertDecimal_Null_To_Zero();

                if (defectRate >= GoalNgPer)
                {
                    e.Appearance.ForeColor = dangerColor;
                }
            }
        }

        private void Set_GvRework(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;

            grid.Create_GvColumn(gv, "LotNo", "SerialNo");
            grid.Create_GvColumn(gv, "불량유형", "DefectName");
            grid.Create_GvColumn(gv, "불량수량", "Qty");

            //
            gv.Columns["SerialNo"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["DefectName"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Qty");

            grid.RowNumber_Indicator(gv);
        }

        private void Set_GvExam(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;

            //
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            //
            grid.Create_GvColumn(gv, "검사구분", "ExamDivCd");
            grid.Create_GvColumn(gv, "검사일자", "ExamDate");

            grid.Create_GvColumn(gv, "검사자", "ExamEmpId");

            grid.Create_GvColumn(gv, "측정구분", "MeasureDivCd");
            grid.Create_GvColumn(gv, "측정시간", "MeasureTime");

            grid.Create_GvColumn(gv, "검사코드", "ExamCd");
            grid.Create_GvColumn(gv, "검사명", "ExamNm");
            grid.Create_GvColumn(gv, "검사규격", "ExamSpec");

            grid.Create_GvColumn(gv, "측정값", "X1");

            grid.Create_GvColumn(gv, "판정", "PassDivCd");

            //
            gv.Columns["MeasureDivCd"].Visible = false;
            gv.Columns["MeasureTime"].Visible = false;
            gv.Columns["ExamCd"].Visible = false;

            gv.Columns["ExamDivCd"].ColumnEdit = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q005"), "코드", "명칭");
            gv.Columns["PassDivCd"].ColumnEdit = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q020"), "코드", "명칭");

            gv.Columns["ExamEmpId"].ColumnEdit = lup_Emp_No;


            //
            gv.Columns["ExamDivCd"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["ExamEmpId"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["PassDivCd"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //
            gv.RowCellStyle += Gv_RowCellStyle1;
            grid.RowNumber_Indicator(gv);
        }

        private void Gv_RowCellStyle1(object sender, RowCellStyleEventArgs e)
        {
            string cellValue = GvQM_WorkDefect.GetRowCellValue(e.RowHandle, e.Column.FieldName).ConvertString_Null_To_Empty();

            if (e.Column.FieldName == "PassDivCd")
            {
                if (cellValue == "10")
                {
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
                }
                else if (cellValue == "20")
                {
                    e.Appearance.BackColor = Color.FromArgb(253, 232, 232); // 연한 분홍/파스텔 레드
                    e.Appearance.ForeColor = Color.FromArgb(200, 50, 50);    // 진한 빨간색
                }
            }
        }

        private void Set_GvLotList(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment
                = HorzAlignment.Center;

            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;

            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.ReadOnly = true;

            gv.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            gv.RowHeight = 28;
            gv.ColumnPanelRowHeight = 30;

            // 컬럼 생성
            grid.Create_GvColumn(gv, "정렬순서", "SortNo");
            grid.Create_GvColumn(gv, "구분", "TraceType");
            grid.Create_GvColumn(gv, "일자", "TraceDate");

            grid.Create_GvColumn(gv, "문서번호", "DocumentNo");
            grid.Create_GvColumn(gv, "순번", "DocumentSeq");

            grid.Create_GvColumn(gv, "생산 LOT", "ProductionLotNo");
            grid.Create_GvColumn(gv, "연결 LOT", "RelatedLotNo");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");

            grid.Create_GvColumn(gv, "공정", "ProcessName");

            // 숨김
            gv.Columns["SortNo"].Visible = false;


            gv.Columns["TraceType"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["DocumentNo"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["DocumentSeq"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["ProductionLotNo"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["RelatedLotNo"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            grid.SetCol_Type(gv.Columns["TraceDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);

            // 수주 → 작업지시 → 생산실적 → 자재투입 → 출하 순서
            gv.Columns["SortNo"].SortIndex = 0;
            gv.Columns["SortNo"].SortOrder
                = DevExpress.Data.ColumnSortOrder.Ascending;

            gv.Columns["TraceDate"].SortIndex = 1;
            gv.Columns["TraceDate"].SortOrder
                = DevExpress.Data.ColumnSortOrder.Ascending;

            // 행번호
            grid.RowNumber_Indicator(gv);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search_Lot();
        }

        #region 조회

        private void Search_Lot()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");
            string serialNo = TxtLotNo.smtTextBoxValue.Trim();

            if(serialNo != "")
			{
                //LotNo 비어있지않으면 날짜 무시
                startDate = "";
                endDate = "";
			}

            PM_OrderResult odr = new PM_OrderResult();
            odr.ProcessCode = Lup_Process.smtLookUpValue.ConvertString_Null_To_Empty();
            odr.ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            odr.SerialNo = serialNo;

            GcOrderResult.DataSource = odr.Select_PM_OrderResult_Indiv(startDate, endDate);
            GvOrderResult.BestFitColumns();
        }

        private void Search_Detail()
        {
            DataRow dr = GvOrderResult.GetFocusedDataRow();

            if(dr == null)
			{
                GcQM_WorkDefect.DataSource = null;
                GcExam.DataSource = null;
                GcList.DataSource = null;
                return;
			}

            string serialNO = dr["SerialNo"].ConvertString_Null_To_Empty();

            Search_WorkDefect(serialNO);
            Search_ExamResult(serialNO);
            Search_LotList(serialNO);
        }

        private void Search_WorkDefect(string serialNo)
		{
            QM_WorkDefect wd = new QM_WorkDefect();
            wd.SerialNo = serialNo;

            GcQM_WorkDefect.DataSource = wd.SelectQM_WorkDefect_BySerialNo();

            GvQM_WorkDefect.BestFitColumns();
        }

        private void Search_ExamResult(string serialNo)
		{
            QM_ExamResult er = new QM_ExamResult();
            er.RefNo = serialNo;

            GcExam.DataSource = er.Select_ExamResult("");
            GvExam.BestFitColumns();
		}

        private void Search_LotList(string serialNo)
        {
            serialNo = serialNo.ConvertString_Null_To_Empty();

            query = $@"
        WITH BaseLot AS
        (
            SELECT DISTINCT
                  R.CorpId
                , R.SerialNo
                , R.WorkDate
                , R.OrderNo
                , R.OrderSeq
                , R.SubSeq
                , R.ProcessCode
                , CP.ProcessName
                , R.ItemCode
                , CI.ItemName
                , R.WorkQty
                , R.GoodQty
                , R.NgQty

                -- 작업지시
                , OP.OrderQty AS InstructionQty
                , OP.OrderDate

                -- 수주
                , PP.SdOrderNo
                , PP.SdOrderSeq
                , SO.Qty AS OrderQty
                , SM.ObtainDate

            FROM PM_OrderResult R

            LEFT JOIN PM_OrderProcess OP
                ON  R.CorpId = OP.CorpId
                AND R.OrderNo = OP.OrderNo
                AND R.OrderSeq = OP.OrderSeq

            LEFT JOIN PM_ProductPlan PP
                ON  R.CorpId = PP.CorpId
                AND R.OrderNo = PP.OrderNo

            LEFT JOIN BM_SdOrderD SO
                ON  PP.CorpId = SO.CorpId
                AND PP.SdOrderNo = SO.SdOrderNo
                AND PP.SdOrderSeq = SO.SdOrderSeq

            LEFT JOIN BM_SdOrderM SM
                ON  SO.CorpId = SM.CorpId
                AND SO.SdOrderNo = SM.SdOrderNo

            LEFT JOIN CM_Item CI
                ON  R.CorpId = CI.CorpId
                AND R.ItemCode = CI.ItemCode

            LEFT JOIN CM_Process CP
                ON  R.CorpId = CP.CorpId
                AND R.ProcessCode = CP.ProcessCode

            WHERE R.CorpId = '{WrGlobal.CorpID}'
              AND R.SerialNo = '{serialNo}'
        ),
        MaterialIssue AS
        (
            SELECT
                  SD.CorpId
                , SD.BarCodeNo AS ProductionLotNo
                , SD.LotNo AS MaterialLotNo
                , SD.ItemCode
                , CI.ItemName
                , SD.Qty
                , SD.SalesNo
                , SD.SalesSeq
                , SM.SalesDate
            FROM BM_SalesD SD

            LEFT JOIN BM_SalesM SM
                ON  SD.CorpId = SM.CorpId
                AND SD.SalesNo = SM.SalesNo

            LEFT JOIN CM_Item CI
                ON  SD.CorpId = CI.CorpId
                AND SD.ItemCode = CI.ItemCode

            WHERE SD.CorpId = '{WrGlobal.CorpID}'
              AND SD.ReqType = '자재출고'
              AND SD.BarCodeNo = '{serialNo}'
              AND SD.LotNo IS NOT NULL
              AND ISNULL(CI.ItemType, '') <> '20'
        ),
        ProductShipment AS
        (
            SELECT
                  SD.CorpId
                , SD.LotNo AS ProductionLotNo
                , NULL AS LotNo --SD.LotNo
                , SD.ItemCode
                , CI.ItemName
                , SD.Qty
                , SD.InOutNo
                , SD.Seq
                , SD.InOutDate
            FROM ST_StockD SD

            LEFT JOIN ST_StockM SM
                ON  SD.CorpId = SM.CorpId
                AND SD.InOutNo = SM.InOutNo

            LEFT JOIN CM_Item CI
                ON  SD.CorpId = CI.CorpId
                AND SD.ItemCode = CI.ItemCode

            WHERE SD.CorpId = '{WrGlobal.CorpID}'
              AND SD.LotNo = '{serialNo}'
              AND ISNULL(SD.Type, '') = 'O'
        )

        -- 수주
        SELECT
              10 AS SortNo
            , N'수주' AS TraceType
            , B.ObtainDate AS TraceDate
            , B.SdOrderNo AS DocumentNo
            , B.SdOrderSeq AS DocumentSeq
            , B.SerialNo AS ProductionLotNo
            , CAST(NULL AS nvarchar(100)) AS RelatedLotNo
            , B.ItemCode
            , B.ItemName
            , B.OrderQty AS Qty
            , CAST(NULL AS decimal(18, 3)) AS GoodQty
            , CAST(NULL AS decimal(18, 3)) AS NgQty
            , CAST(NULL AS nvarchar(100)) AS ProcessName
        FROM BaseLot B

        UNION ALL

        -- 작업지시
        SELECT
              20
            , N'작업지시'
            , CAST(OrderDate AS datetime)
            , B.OrderNo
            , B.OrderSeq
            , B.SerialNo
            , CAST(NULL AS nvarchar(100))
            , B.ItemCode
            , B.ItemName
            , B.InstructionQty
            , CAST(NULL AS decimal(18, 3))
            , CAST(NULL AS decimal(18, 3))
            , B.ProcessName
        FROM BaseLot B

        UNION ALL

        -- 생산실적
        SELECT
              30
            , N'생산실적'
            , B.WorkDate
            , B.OrderNo
            , B.OrderSeq
            , B.SerialNo
            , CAST(NULL AS nvarchar(100))
            , B.ItemCode
            , B.ItemName
            , B.WorkQty
            , B.GoodQty
            , B.NgQty
            , B.ProcessName
        FROM BaseLot B

        UNION ALL

        -- 해당 생산 LOT에 투입된 자재 LOT
        SELECT
              40
            , N'자재투입'
            , M.SalesDate
            , M.SalesNo
            , M.SalesSeq
            , M.ProductionLotNo
            , M.MaterialLotNo
            , M.ItemCode
            , M.ItemName
            , M.Qty
            , CAST(NULL AS decimal(18, 3))
            , CAST(NULL AS decimal(18, 3))
            , CAST(NULL AS nvarchar(100))
        FROM MaterialIssue M

        UNION ALL

        -- 해당 생산 LOT가 출하된 내역
        SELECT
              50
            , N'출하'
            , S.InOutDate
            , S.InOutNo
            , S.Seq
            , S.ProductionLotNo
            , S.LotNo
            , S.ItemCode
            , S.ItemName
            , S.Qty
            , CAST(NULL AS decimal(18, 3))
            , CAST(NULL AS decimal(18, 3))
            , CAST(NULL AS nvarchar(100))
        FROM ProductShipment S

        ORDER BY
              SortNo
            , TraceDate
            , DocumentNo
            , DocumentSeq;
    ";

            GcList.DataSource = db.GetDataTable(query);
            GvList.BestFitColumns();
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
