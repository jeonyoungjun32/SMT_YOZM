using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using SmartCommon.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_ExcelUpLoad_Material : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        Database db = new Database();
        string query = "";

        CM_CateItem cc = new CM_CateItem();
        CM_Item cm = new CM_Item();
        CM_Cust cust = new CM_Cust();

        public GridControl Gc = new GridControl();
        public string formName = "";


        public Frm_CM_ExcelUpLoad_Material(string formType = "")
        {
            InitializeComponent();
        }

        private void Frm_ExcelUpLoad_Load(object sender, EventArgs e)
        {

        }

        #region 디자인 아이템 생성

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

                string cust_Nm = "";
                string unit_Nm = "";

                foreach (DataRow dataRow in dtExcel.Rows)
                {
                    ++seq;

                    if (dataRow["ItemCode"].ToString() == "") continue;

                    //호성테크닉스 품목구분 코드 20 : 제품, 30: 반제품, 40: 원자재
                    if (dataRow["ItemType"].ToString() == "반제품") dataRow["ItemType"] = "30";
                    if (dataRow["ItemType"].ToString() == "원자재") dataRow["ItemType"] = "40";

                    //거래처
                    if (dtExcel.Columns.Contains("CustCode"))
                    {
                        cust_Nm = Convert.ToString(dataRow["CustCode"]);

                        DataRow drCust = Select_CustRow(cust_Nm);

                        if (drCust != null)
                        {
                            dataRow["CustCode"] = drCust["CustCode"];
                        }
                        else
                        {
                            dataRow["Chk"] = "거래처 이상";
                            continue;
                        }
                    }

                    if (dtExcel.Columns.Contains("UnitCode"))
                    {
                        unit_Nm = Convert.ToString(dataRow["UnitCode"]);

                        DataRow drUnitCode = Select_UnitRow(unit_Nm);

                        if (drUnitCode != null)
                        {
                            dataRow["UnitCode"] = drUnitCode["ItemCode"];
                        }
                        else
                        {
                            dataRow["Chk"] = "단위 이상";
                            continue;
                        }
                    }

                    if (Error == "Y")
                    {
                        XtraMessageBox.Show("잘못 입력된 값이 있거나 필수 값 미입력 행이 있습니다.\n필수 값: 품목구분, BOM구분, 창고명 등", "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        //품명, 품번이 빈칸일 경우 SKIP
                        if (Convert.ToString(dr["ItemName"]) == "")
                        {
                            continue;
                        }

                        if (!SaveData(dr))
                        {
                            XtraMessageBox.Show("저장 중 에러가 발생하였습니다.");
                            break;
                        }
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
                CM_Item.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
                CM_Item.ItemName = dr["ItemName"].ConvertString_Null_To_Empty();
                CM_Item.ItemType = dr["ItemType"].ConvertString_Null_To_Empty();
                CM_Item.UnitCode = dr["UnitCode"].ConvertString_Null_To_Empty();

                CM_Item.Insert_Update_CM_Item();

                return true;
            }

            catch(Exception ex)
			{
                return false;
            }
        }

        private DataRow Select_CustRow(string cust_Nm)
        {
            cust.CustName = cust_Nm;

            DataRow drCust = cust.Select_Cust_Info();

            if (drCust == null)
                return null;

            return drCust;
        }

        private DataRow Select_UnitRow(string unit_Nm)
        {
            cc.Title = unit_Nm;

            DataRow drUnit = cc.Select_CateItem_ByCode();

            if (drUnit == null)
                return null;

            return drUnit;
        }

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