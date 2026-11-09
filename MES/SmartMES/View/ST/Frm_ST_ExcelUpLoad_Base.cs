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
using SmartMES.Models.ST;
using SmartMES.Models;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_ExcelUpLoad_Base : DevExpress.XtraEditors.XtraForm
    {
        public string fieldName { get; set; }
        public string caption { get; set; }

        Database db = new Database();
        string query = "";

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();

        CM_CateItem cc = new CM_CateItem();

        CM_Item item = new CM_Item();
        CM_Warehouse house = new CM_Warehouse();

        public GridControl Gc = new GridControl();
        public string formName = "";

        GridColumn[] ExCelColumns = null;


        public Frm_ST_ExcelUpLoad_Base(string formType = "")
        {
            InitializeComponent();
        }

        private void Frm_ExcelUpLoad_Load(object sender, EventArgs e)
        {
            Gc = Gc_Excel;
            ExCelColumns = new GridColumn[]
          {
                new GridColumn { FieldName = "ItemCode", Caption = "품목코드"},
                new GridColumn { FieldName = "ItemName", Caption = "품명(미입력 가능)"},
                new GridColumn { FieldName = "HouseID", Caption = "창고코드"},
                new GridColumn { FieldName = "InDate", Caption = "입고일자"},
                new GridColumn { FieldName = "StockQty", Caption = "재고량"},
              //new GridColumn { FieldName = "UseYn", Caption = "사용여부", ColumnEdit = rpo.Create_RepositoryItemSearchLookUp(cc.Select_UseYn(), "코드", "명칭") },
              //new GridColumn { FieldName = "Remark", Caption = "참고사항"}
          };

        }

        #region 데이터 조회

        private void Excel_Down()
        {
            //양식 다운로드

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "엑셀 파일 (*.xlsx, *.xls, *.csv) | *.xlsx; *.xls; *.csv;";
                saveFileDialog.FileName = formName + DateTime.Now.ToString("yyMMdd") + "_기초재고_양식";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XlsxExportOptionsEx ex = new XlsxExportOptionsEx();
                    ex.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                    ex.SheetName = formName;

                    GridControl gc = CloneGrid(Gc);
                    GridView gv = (gc.MainView as GridView);

                    //Column Visible 처리 ex) 상태, 이미지, 등록자, 등록일시 등..

                    if (gv.Columns.ColumnByFieldName("Status") != null) gv.Columns["Status"].Visible = false;

                    AddAnotherCol(gv);

                    //Export
                    gc.ExportToXlsx(saveFileDialog.FileName, ex);

                    // 엑셀 Cell 안에 Validation 추가
                    ExcelAddValidation(saveFileDialog);

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

        private void AddAnotherCol(GridView gv)
        {
            foreach (var col in ExCelColumns)
            {
                col.Visible = true;
                col.VisibleIndex = gv.Columns.Count;
                gv.Columns.Add(col);
            }
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

            string ColumnLetter = "B";
            CellRange custNameRange = worksheet[$"{ColumnLetter}:{ColumnLetter}"];
            string customFormula = $"=ISTEXT({ColumnLetter}1)"; // 셀 값이 문자열인지 확인하는 수식

            // CustName 열에 데이터 유효성 검사 추가
            DataValidation custNameValidation = worksheet.DataValidations.Add(custNameRange, DataValidationType.Custom, DataValidationOperator.Equal, customFormula);
            custNameValidation.ErrorMessage = "숫자만 들어갈 수 없습니다";

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
                string isError = "N";
                string message = "";

                bool Success = false;

                ////필요 컬럼 추가
                dtExcel.Columns.Add("Chk");


                //전체 품목 Dic
                DataTable dtAllItems = item.Select_Item_Std(null);

                Dictionary<string, DataRow> dicItem = new Dictionary<string, DataRow>(StringComparer.OrdinalIgnoreCase);

                if (dtAllItems != null)
                {
                    foreach (DataRow row in dtAllItems.Rows)
                    {
                        string code = Convert.ToString(row["MaterialCode"]);
                        if (!string.IsNullOrEmpty(code) && !dicItem.ContainsKey(code))
                        {
                            dicItem.Add(code, row);
                        }
                    }
                }

                //창고 Dic
                DataTable dtHouse = house.Select_CM_Warehouse();

                Dictionary<string, DataRow> dicHouse = new Dictionary<string, DataRow>(StringComparer.OrdinalIgnoreCase);

                if (dtHouse != null)
                {
                    foreach (DataRow row in dtHouse.Rows)
                    {
                        string code = Convert.ToString(row["HouseID"]);
                        if (!string.IsNullOrEmpty(code) && !dicHouse.ContainsKey(code))
                        {
                            dicHouse.Add(code, row);
                        }
                    }
                }

                foreach (DataRow dataRow in dtExcel.Rows)
                {
                    ++seq;

                    int excelRow = seq + 1;

                    // 품목 코드 확인
                    string MaterialCode = Convert.ToString(dataRow["MaterialCode"]);

                    if (MaterialCode != "")
                    {
                        if (!dicItem.TryGetValue(MaterialCode, out DataRow drItem))
                        {
                            isError = "Y";
                            dataRow["Chk"] = "품목코드 확인";
                            message += $"{excelRow} 행 품목코드 오류\n";
                        }
                    }

                    //창고 코드 확인
                    string houseID = Convert.ToString(dataRow["HouseID"]);

                    if (houseID != "")
                    {
                        if (!dicHouse.TryGetValue(houseID, out DataRow drHouse))
                        {
                            isError = "Y";
                            dataRow["Chk"] = "창고코드 확인";
                            message += $"{excelRow} 행 창고코드 '{houseID}' 오류\n";
                        }
                    }
                }

                if (isError == "Y")
                {
                    XtraMessageBox.Show("잘못 입력된 값이 있거나 필수 값 미입력 행이 있습니다.\n" + message, "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataRow dr in dtExcel.Rows)
                {
                    //스킵 처리 할 항목
                    if (dr["MaterialCode"].ConvertString_Null_To_Empty() == "" || dr["HouseID"].ConvertString_Null_To_Empty() == "")
                    {
                        continue;
                    }

                    if (!SaveData(dr))
                    {
                        XtraMessageBox.Show("저장 중 에러가 발생하였습니다.");
                        Success = false;
                        break;
                    }
                }

                Success = true;

				if (Success)
				{
                    XtraMessageBox.Show("저장이 완료되었습니다.");
				}
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
            foreach (var col in ExCelColumns)
            {
                gridView.Columns.Add(col);
            }

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

                ST_StockM ST_StockM = Create_ST_StockM(dr);
                ST_StockD ST_StockD = Create_ST_StockD(dr, ST_StockM);

                if (!ST_StockM.Ins_ST_StockM())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                if (!ST_StockD.Ins_ST_StockD())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                return true;
            }

            catch(Exception ex)
			{
                return false;
            }
        }


        private ST_StockM Create_ST_StockM(DataRow dr)
        {
            ST_StockM ST_StockM = new ST_StockM();

            string strInDate = dr["InDate"].ConvertString_Null_To_Empty();
            DateTime inDate = new DateTime();
            DateTime.TryParse(strInDate, out inDate);
            if (inDate == DateTime.MinValue) inDate = Convert.ToDateTime("1900-01-01");

            ST_StockM.InOutNo = "ST" + code.GetAutoNumber("재고", inDate);
            ST_StockM.OutDate = inDate.ConvertDate_To_Char10();
            ST_StockM.FromHouseCode = "";
            ST_StockM.ToHouseCode = dr["HouseID"].ConvertString_Null_To_Empty();
            ST_StockM.WriteID = WrGlobal.LoginID;
            ST_StockM.Remark = "기초";

            return ST_StockM;
        }

        private ST_StockD Create_ST_StockD(DataRow dr, ST_StockM ST_StockM)
        {
            ST_StockD ST_StockD = new ST_StockD();

            ST_StockD.InOutNo = ST_StockM.InOutNo;
            ST_StockD.Seq = 1;
            //ST_StockD.CustCode = "";
            ST_StockD.InOutDate = ST_StockM.OutDate;
            ST_StockD.Type = "조정";
            ST_StockD.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();

            //임의의 LOT 부여 ~> LOT별 출고할때 LOTNO 필요하여 
            string lotNo = "";

            if (dr["ItemType"].ConvertString_Null_To_Empty() == "20")
            {
                lotNo = "SN" + code.GetAutoNumber("바코드", Convert.ToDateTime(ST_StockD.InOutDate));
            }
            else
            {
                lotNo = "M" + code.GetAutoNumber("자재LOT", Convert.ToDateTime(ST_StockD.InOutDate));
            }

            ST_StockD.FromHouseCode = ST_StockM.FromHouseCode;
            ST_StockD.ToHouseCode = ST_StockM.ToHouseCode;
            //ST_StockD.JobNo = "";
            //ST_StockD.JobSeq = "";
            //ST_StockD.JobSeq2 = "";
            //ST_StockD.FaultCode = "";
            ST_StockD.Qty = dr["StockQty"].ConvertDecimal_Null_To_Zero();
            //ST_StockD.OutQty = "";
            ST_StockD.CloseYn = "N";
            ST_StockD.Remark = "기초";

            return ST_StockD;
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