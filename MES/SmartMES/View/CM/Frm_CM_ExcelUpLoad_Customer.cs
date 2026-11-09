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
    public partial class Frm_CM_ExcelUpLoad_Customer : DevExpress.XtraEditors.XtraForm
    {
        public string fieldName { get; set; }
        public string caption { get; set; }
        clsGrid grid = new clsGrid();
        Database db = new Database();
        string query = "";
        clsRepository rpo = new clsRepository();
        CM_CateItem cc = new CM_CateItem();
        CM_Item item = new CM_Item();

        public GridControl Gc = new GridControl();
        public string formName = "";


        public Frm_CM_ExcelUpLoad_Customer(string formType = "")
        {
            InitializeComponent();
        }

        private void Frm_ExcelUpLoad_Load(object sender, EventArgs e)
        {

        }

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
            GridColumn[] columns = new GridColumn[]
            {
                //new GridColumn { FieldName = "PresiName", Caption = "대표자명"},
                new GridColumn { FieldName = "CitizeNo", Caption = "법인번호"},
                new GridColumn { FieldName = "BussinessCategory", Caption = "업태"},
                new GridColumn { FieldName = "BussinessField", Caption = "업종"},
                new GridColumn { FieldName = "Fax", Caption = "팩스번호"},
                new GridColumn { FieldName = "Email", Caption = "이메일"},
                new GridColumn { FieldName = "HomePage", Caption = "홈페이지"},
                new GridColumn { FieldName = "PostNo", Caption = "우편번호"},
                new GridColumn { FieldName = "UseYn", Caption = "사용여부", ColumnEdit = rpo.Create_RepositoryItemSearchLookUp(cc.Select_UseYn(), "코드", "명칭") },
                //new GridColumn { FieldName = "Remark", Caption = "참고사항"}
             };

            foreach (var col in columns)
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
                string Error = "N";
                bool Success = false;

                ////필요 컬럼 추가
                dtExcel.Columns.Add("Chk");

                string custType_Nm = "";

                foreach (DataRow dataRow in dtExcel.Rows)
                {
                    ++seq;

                    int excelRow = seq + 1;

                    if (dataRow["CustCode"].ToString() == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 거래처코드가 없습니다.");
                        return;
                    }

                    if (dataRow["CustName"].ToString() == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 거래처명이 없습니다.");
                        return;
                    }

                    if (dataRow["CustType"].ToString() == "")
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 거래처구분이 없습니다.");
                        return;
                    }

                    // 거래처구분 코드 변환
                    string custTypeNm = Convert.ToString(dataRow["CustType"]);
                    DataRow drCustType = Select_CustRow(custTypeNm);

                    if (drCustType == null)
                    {
                        XtraMessageBox.Show($"{excelRow}행 : 거래처구분 '{custTypeNm}'이(가) 올바르지 않습니다.");
                        return;
                    }

                    dataRow["CustType"] = drCustType["ClassItemCode"];

                    // 저장
                    if (!SaveData(dataRow))
                    {
                        XtraMessageBox.Show($"{seq}행 저장 중 오류가 발생했습니다.");
                        return;
                    }
                }
                XtraMessageBox.Show("저장이 완료되었습니다.");
                this.Close();
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
            GridColumn[] columns = new GridColumn[]
            {
                        //new GridColumn { FieldName = "PresiName", Caption = "대표자명"},
                        new GridColumn { FieldName = "CitizeNo", Caption = "법인번호"},
                        new GridColumn { FieldName = "BussinessCategory", Caption = "업태"},
                        new GridColumn { FieldName = "BussinessField", Caption = "업종"},
                        new GridColumn { FieldName = "Fax", Caption = "팩스번호"},
                        new GridColumn { FieldName = "Email", Caption = "이메일"},
                        new GridColumn { FieldName = "HomePage", Caption = "홈페이지"},
                        new GridColumn { FieldName = "PostNo", Caption = "우편번호"},
                        new GridColumn { FieldName = "UseYn", Caption = "사용여부", ColumnEdit = rpo.Create_RepositoryItemSearchLookUp(cc.Select_UseYn(), "코드", "명칭") },
                        //new GridColumn { FieldName = "Remark", Caption = "참고사항"}
            };

            GridView gridView = Gc.MainView as GridView;
            foreach (var col in columns)
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

                CM_Cust cust = new CM_Cust();
                cust.CustCode = dr["CustCode"].ConvertString_Null_To_Empty();
                cust.CustName = dr["CustName"].ConvertString_Null_To_Empty();
                cust.CustType = dr["CustType"].ConvertString_Null_To_Empty();
                cust.Address = dr["Address"].ConvertString_Null_To_Empty();
                cust.Tel = dr["Tel"].ConvertString_Null_To_Empty();
                cust.CustNo = dr["CustNo"].ConvertString_Null_To_Empty();
                cust.SalesMan = dr["SalesMan"].ConvertString_Null_To_Empty();
                cust.PresiName = dr["PresiName"].ConvertString_Null_To_Empty();
                //cust.CitizeNo = dr["CitizeNo"].ConvertString_Null_To_Empty();
                cust.BussinessCategory = dr["BussinessCategory"].ConvertString_Null_To_Empty();
                cust.BussinessField = dr["BussinessField"].ConvertString_Null_To_Empty();
                cust.Fax = dr["Fax"].ConvertString_Null_To_Empty();
                cust.Email = dr["Email"].ConvertString_Null_To_Empty();
                cust.HomePage = dr["HomePage"].ConvertString_Null_To_Empty();
                cust.PostNo = dr["PostNo"].ConvertString_Null_To_Empty();
                cust.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
                cust.Remark = dr["Remark"].ConvertString_Null_To_Empty();

                cust.Insert_Update_CM_Cust();
                return true;
            }

            catch(Exception ex)
			{
                return false;
            }
        }

        private DataRow Select_CustRow(string custType_Nm)
        {
            cc.Class = "A030";
            cc.Title = custType_Nm;

            DataRow drCust = cc.Select_ItemCode_By_CmCateItem();

            if (drCust == null)
                return null;

            return drCust;
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