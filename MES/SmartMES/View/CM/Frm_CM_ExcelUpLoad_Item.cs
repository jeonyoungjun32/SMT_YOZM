using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using SmartCommon.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;


namespace SmartMES.View.CM
{
    public partial class Frm_CM_ExcelUpLoad_Item : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        Database db = new Database();
        string query = "";

        // ToolTip 컨트롤러 추가
        ToolTipController toolTipController = new ToolTipController();

        CM_CateItem cc = new CM_CateItem();
        CM_Item cm = new CM_Item();
        clsCode code = new clsCode();

        public GridControl Gc = new GridControl();
        public string formName = "";


        public Frm_CM_ExcelUpLoad_Item(string formType = "")
        {
            InitializeComponent();

            SetupToolTips();
        }

        private void Frm_ExcelUpLoad_Load(object sender, EventArgs e)
        {

        }

        #region 디자인 아이템 생성
        private void SetupToolTips()
        {
            // 각 버튼에 상세 설명 추가
            toolTipController.SetToolTip(Btn_ExcelDown, "1. 양식 다운로드: 품목 등록을 위한 엑셀 양식을 다운로드합니다. " +
                                                        "\n2. 다운로드된 엑셀 파일에 품목 정보를 입력합니다.\r\n  " +
                                                        "* 필수 입력: 제품코드(ItemCode), 품명(ItemName)\r\n   " +
                                                        "* 제품구분, 거래처, 공정코드, 단위는 콤보박스로 선택 가능합니다. ");
            toolTipController.SetToolTip(Btn_Open, "파일 열기: 작성한 품목 데이터 엑셀 파일을 불러옵니다.");
            toolTipController.SetToolTip(Btn_Save, "저장하기: 불러온 엑셀 데이터를 시스템에 등록합니다. 품목코드와 품명은 필수입니다.");


            // 스프레드시트 컨트롤에도 힌트 추가
            toolTipController.SetToolTip(spreadsheetControl1, "엑셀 데이터 미리보기: 불러온 엑셀 파일의 내용이 표시됩니다.");

            // 폼 전체에 툴팁 컨트롤러 적용
            Btn_ExcelDown.ToolTipController = toolTipController;
            Btn_Open.ToolTipController = toolTipController;
            Btn_Save.ToolTipController = toolTipController;
            Btn_Close.ToolTipController = toolTipController;
            spreadsheetControl1.ToolTipController = toolTipController;

            // 툴팁 표시 시간 설정 (선택사항)
            toolTipController.AutoPopDelay = 5000;  // 5초 동안 표시
            toolTipController.ShowShadow = true;    // 그림자 효과 추가
        }

        #endregion 디자인 아이템 생성 End

        #region 데이터 조회

        private void Excel_Down()
        {
            //양식 다운로드

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "엑셀 파일 (*.xlsx, *.xls, *.csv) | *.xlsx; *.xls; *.csv;";
                saveFileDialog.FileName = formName + DateTime.Now.ToString("yyMMdd");

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XlsxExportOptionsEx ex = new XlsxExportOptionsEx();
                    ex.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                    ex.SheetName = formName;

                    GridControl gc = CloneGrid(Gc);
                    GridView gv = (gc.MainView as GridView);

                    //Column Visible 처리 ex) 상태, 이미지, 등록자, 등록일시 등..

                    if (gv.Columns.ColumnByFieldName("Status") != null) gv.Columns["Status"].Visible = false;

                    //Export
                    gc.ExportToXlsx(saveFileDialog.FileName, ex);

                    // 엑셀 Cell 안에 Validation 추가
                    //ExcelAddValidation(saveFileDialog);

                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private GridControl CloneGrid(GridControl gcOriginal)
        {
            GridControl gc = new GridControl();
            gc.MainView = new GridView(gc);
            gc.MainView.Assign(gcOriginal.MainView, false);
            Controls.Add(gc);

            gc.Visible = false;

            return gc;
        }

        private void ExcelAddValidation(SaveFileDialog saveFileDialog)
        {
            Workbook workbook = new Workbook();
            workbook.LoadDocument(saveFileDialog.FileName);

            Worksheet worksheet = workbook.Worksheets[0];
            List<DataValidation> dataValidations = worksheet.DataValidations.ToList();

            foreach (DataValidation dataValidation in dataValidations)
            {
                int colindex = dataValidation.Range.LeftColumnIndex;
                string colHeading = worksheet.Columns[colindex].Heading;
                dataValidation.Range = worksheet[$"{colHeading}:{colHeading}"];

                worksheet.DataValidations.Add(dataValidation.Range, dataValidation.ValidationType, dataValidation.Criteria);
            }

            workbook.SaveDocument(saveFileDialog.FileName);
        }

        private void Excel_Open()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

                if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;

                spreadsheetControl1.LoadDocument(openFileDialog.FileName);
            }
            catch(Exception ex)
			{
                XtraMessageBox.Show(ex.Message);
			}
        }

        #endregion 데이터 조회 End

        #region 데이터 저장

        private void Data_Save()
		{
            try
			{
                Worksheet worksheet = spreadsheetControl1.Document.Worksheets.ActiveWorksheet;
                CellRange cells = worksheet.GetDataRange();

                DataTable dtExcel = worksheet.CreateDataTable(cells, true);
                ChangeColName(dtExcel);

                DataTableExporter exporter = worksheet.CreateDataTableExporter(cells, dtExcel, true);
                exporter.CellValueConversionError += Exporter_CellValueConversionError;
                exporter.Export();

                if (dtExcel == null) return;
                if (dtExcel.Rows.Count < 1) return;

                int seq = 0;
                string Error = "N";
                bool Success = false;

                ////필요 컬럼 추가
                dtExcel.Columns.Add("Chk");

                string ItemType = "";

                string ItemClass1, ItemClass2, material_Nm = "";
                string unit_Nm = "";

                foreach (DataRow dataRow in dtExcel.Rows)
                {
                    ++seq;

                    int excelRow = seq + 1;

                    if (dataRow["ItemCode"].ToString() == "")
                        continue;

                    // 품목구분
                    ItemType = dataRow["ItemType"].ConvertString_Null_To_Empty();

                    if (ItemType == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 품목구분이 없습니다.");
                        return;
                    }

                    string itemTypeCode = Select_CM_CateItem("A010", ItemType);

                    if (itemTypeCode == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 품목구분 '{ItemType}'이(가) 올바르지 않습니다.");
                        return;
                    }

                    dataRow["ItemType"] = itemTypeCode;

                    // 품목분류1
                    ItemClass1 = dataRow["ItemClass1"].ConvertString_Null_To_Empty();

                    string ItemClass1Cd = Select_CM_CateItem("A01210", ItemClass1);

                    if (ItemClass1 == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 품목분류1 '{ItemClass1}'이(가) 올바르지 않습니다.");
                        return;
                    }

                    dataRow["ItemClass1"] = ItemClass1Cd;

                    // 품목분류2
                    ItemClass2 = dataRow["ItemClass2"].ConvertString_Null_To_Empty();

                    string ItemClass2Cd = Select_CM_CateItem("A01220", ItemClass2);

                    if (ItemClass1 == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 품목분류2 '{ItemClass2}'이(가) 올바르지 않습니다.");
                        return;
                    }

                    dataRow["ItemClass2"] = ItemClass2Cd;


                    // 단위
                    unit_Nm = dataRow["UnitCode"].ConvertString_Null_To_Empty();

                    string unitCode = Select_CM_CateItem("A011", unit_Nm);

                    if (unitCode == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 단위 '{unit_Nm}'이(가) 올바르지 않습니다.");
                        return;
                    }

                    dataRow["UnitCode"] = unitCode;
                }


                // 전체 행 검증이 끝난 후 저장
                foreach (DataRow dr in dtExcel.Rows)
                {
                    // 품번이 빈칸이면 SKIP
                    if (Convert.ToString(dr["ItemCode"]) == "")
                        continue;

                    if (Convert.ToString(dr["ItemName"]) == "")
                        continue;

                    if (!SaveData(dr))
                    {
                        XtraMessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
                    }
                }
                XtraMessageBox.Show("저장이 완료되었습니다.");
            }
            catch (Exception ex)
			{
                XtraMessageBox.Show(ex.Message.ToString());
			}
		}

        private void Exporter_CellValueConversionError(object sender, CellValueConversionErrorEventArgs e)
        {
            MessageBox.Show($"Error in cell : {e.Cell.GetReferenceA1()}\nError Column : {e.DataColumn}\nError Value : {e.CellValue}");
            e.DataTableValue = null;
            e.Action = DataTableExporterAction.Continue;
        }

        private void ChangeColName(DataTable dtExcel)
        {
            GridView gridView = Gc.MainView as GridView;

            foreach (GridColumn gridColumn in gridView.Columns)
            {
                foreach (DataColumn dataColumn in dtExcel.Columns)
                {
                    if (dataColumn.ColumnName == gridColumn.Caption)
                    {
                        dataColumn.ColumnName = gridColumn.FieldName;
                    }
                }
            }
        }

        private bool SaveData(DataRow dr)
        {
            try
			{
                DataTable dt = dr.Table;
                if (dt == null) return false;

                CM_Item CM_Item = new CM_Item();
                CM_Item.ItemType = dr["ItemType"].ConvertString_Null_To_Empty();  //제품구분
                CM_Item.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();  //제품코드
                CM_Item.ItemName = dr["ItemName"].ConvertString_Null_To_Empty();  //품명
                CM_Item.UnitCode = dr["UnitCode"].ConvertString_Null_To_Empty();        //단위
                CM_Item.ItemClass1 = dr["ItemClass1"].ConvertString_Null_To_Empty();    // 품목분류1
                CM_Item.ItemClass2 = dr["ItemClass2"].ConvertString_Null_To_Empty();    // 품목분류2
                //CM_Item.ItemClass3 = dr["ItemClass3"].ConvertString_Null_To_Empty();  // 품목분류3
                //CM_Item.Texture = dr["Texture"].ConvertString_Null_To_Empty();            //재질
                //CM_Item.Standard = dr["Standard"].ConvertString_Null_To_Empty();          //규격
                //CM_Item.UnitWeight = dr["UnitWeight"].ConvertFloat_Null_To_Zero();        //중량
                //CM_Item.Size = dr["Size"].ConvertString_Null_To_Empty();                  //전장
                CM_Item.SaftyQty = dr["SaftyQty"].ConvertInt_Null_To_Zero();            //안전재고
                CM_Item.Remark = dr["Remark"].ConvertString_Null_To_Empty();            //비고
                //CM_Item.RoutingCode = dr["RoutingCode"].ConvertString_Null_To_Empty();    //공정코드

                CM_Item.Insert_Update_CM_Item_Item_Excel();
                return true;
            }
            catch(Exception ex)
			{
                return false;
            }
        }

        #region 공통코드 변환
        private string Select_CM_CateItem(string cm_Class, string cm_Nm)
        {
            // 숫자로 입력된 경우
            if (IsNumeric(cm_Nm))
            {
                query = $@"
                            SELECT ClassItemCode
                            FROM CM_CateItem 
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND CLASS= '{cm_Class}'
                            AND ClassItemCode = '{cm_Nm}'
                        ";

                DataRow drCheck = db.GetDataRow(query);
                if (drCheck != null)
                {
                    return cm_Nm;
                }
            }

            // 문자로 입력된 경우 (기존 로직)
            query = $@"
                        SELECT ClassItemCode
                        FROM CM_CateItem 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS= '{cm_Class}'
                        AND Title = '{cm_Nm}'
                    ";

            DataRow dr = db.GetDataRow(query);
            if (dr == null) return "";

            return Convert.ToString(dr["ClassItemCode"]);
        }

        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }

        // 거래처 변환 (거래처명↔거래처코드)
        private string ConvertCustCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            // 1. 거래처코드로 입력된 경우 → 유효성 검사 후 반환
            query = $@"
                        SELECT CustCode
                        FROM CM_Cust 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CustCode = '{input}'
                        AND UseYn = 'Y'
                    ";

            DataRow drCheck = db.GetDataRow(query);
            if (drCheck != null)
                return input;

            // 2. 거래처명으로 입력된 경우 → CustName으로 검색해서 CustCode 반환
            query = $@"
                        SELECT CustCode
                        FROM CM_Cust 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CustName = '{input}'
                        AND UseYn = 'Y'
                    ";

            DataRow dr = db.GetDataRow(query);
            if (dr != null)
                return Convert.ToString(dr["CustCode"]);

            return "";
        }


        // 공정코드(라우팅) 변환 (공정명↔공정코드)
        private string ConvertRoutingCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            // 1. 공정코드로 입력된 경우 → 유효성 검사 후 반환
            query = $@"
                        SELECT RoutingCode
                        FROM CM_RoutingRev 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND RoutingCode = '{input}'
                        AND RoutingType = '10'
                        AND UseYn = 'Y'
                    ";

            DataRow drCheck = db.GetDataRow(query);
            if (drCheck != null) return input;  // 코드 그대로 반환

            // 2. 공정명으로 입력된 경우 → RoutingName으로 검색해서 RoutingCode 반환
            query = $@"
                        SELECT RoutingCode
                        FROM CM_RoutingRev 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND RoutingName = '{input}'
                        AND RoutingType = '10'
                        AND UseYn = 'Y'
                    ";

            DataRow dr = db.GetDataRow(query);
            if (dr != null)
                return Convert.ToString(dr["RoutingCode"]);

            return "";
        }

        #endregion 공통코드 변환 END

        #endregion 데이터 저장 End

        #region Event

        private void Btn_Click(object sender, EventArgs e)
		{
            SimpleButton btn = sender as SimpleButton;

            string btnName = btn.Name;

            switch (btn.Name)
            {
                case "Btn_ExcelDown":
                    Excel_Down();
                    break;

                case "Btn_Open":
                    Excel_Open();
                    break;

                case "Btn_Save":
                    Data_Save();
                    break;

                case "Btn_Close":
                    this.Close();
                    break;
            }
        }


        #endregion Event End

    }
}