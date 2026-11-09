using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet;
using SmartCommon.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_ExcelUpLoad_ItemlPrice : DevExpress.XtraEditors.XtraForm
    {
        public string fieldName { get; set; }
        public string caption { get; set; }

        clsGrid grid = new clsGrid();
        Database db = new Database();
        string query = "";
        clsRepository rpo = new clsRepository();
        CM_CateItem cc = new CM_CateItem();
        CM_Cust cust = new CM_Cust();
        clsCode code = new clsCode();

        CM_Item mat = new CM_Item();

        GridColumn[] columns = new GridColumn[] { };

        public GridControl Gc = new GridControl();
        public string formName = "";


        public Frm_CM_ExcelUpLoad_ItemlPrice(string formType = "")
        {
            InitializeComponent();
        }

        private void Frm_ExcelUpLoad_Load(object sender, EventArgs e)
        {
            GridView gv = new GridView();
            Gc.MainView = gv;

            AddAnotherCol(gv);
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
                saveFileDialog.FileName = formName + DateTime.Now.ToString("yyMMdd") + "거래처별품목단가";

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
            columns = new GridColumn[]
            {
                new GridColumn { FieldName = "PriceType", Caption = "단가구분", ColumnEdit = rpo.Create_RepositoryItemSearchLookUp(cc.SelectCateItem("S210"), "코드", "명칭")},
                new GridColumn { FieldName = "CustCode"     , Caption = "거래처코드", UnboundType = DevExpress.Data.UnboundColumnType.String },
                new GridColumn { FieldName = "CustName"     , Caption = "거래처명" },
                new GridColumn { FieldName = "ItemCode" , Caption = "품목코드" },
                new GridColumn { FieldName = "ItemName" , Caption = "품명" },
                new GridColumn { FieldName = "Price", Caption = "단가"},
                new GridColumn { FieldName = "ApplyDate", Caption = "적용일자"},
                new GridColumn { FieldName = "EndDate", Caption = "종료일자"},
                new GridColumn { FieldName = "CustAlias", Caption = "업체품번"},
                new GridColumn { FieldName = "Remark", Caption = "비고"},
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
                //셀 활성화된 상태에서 저장버튼 클릭 시 해당 셀 값 적용이 안되서 Editor Close 해줌
                spreadsheetControl1.CloseCellEditor(CellEditorEnterValueMode.ActiveCell);

                spreadsheetControl1.EndUpdate();

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
                if(!dtExcel.Columns.Contains("Chk")) dtExcel.Columns.Add("Chk");

                string priceType = "";
                string materialCd = "";
                string materialNm = "";
                string custCd = "";
                string custNm = "";

                string message = "";

                foreach (DataRow dataRow in dtExcel.Rows)
                {
                    ++seq;

                    //필수값
                    //단가 구분
                    priceType = Convert.ToString(dataRow["PriceType"]);
                    materialCd = Convert.ToString(dataRow["ItemCode"]);
                    materialNm = Convert.ToString(dataRow["ItemName"]);
                    custCd = Convert.ToString(dataRow["CustCode"]);
                    custNm = Convert.ToString(dataRow["CustName"]);

                    if (custCd == "" && custNm == "") continue;
                    if (materialCd == "" && materialNm == "") continue;

                    if (priceType == "")
                    {
                        worksheet.Rows[seq].Font.Color = Color.Red;
                        Error = "Y";
                        dataRow["Chk"] = "단가구분 미입력";
                        message += $"{seq} 행 단가구분 미 입력\n";
                        continue;
                    }

                    //if (materialCd == "" && materialNm == "")
                    //{
                    //    worksheet.Rows[seq].Font.Color = Color.Red;
                    //    Error = "Y";
                    //    dataRow["Chk"] = "품목코드, 품명 미입력";
                    //    message += $"{seq} 행 품목코드, 품명 미 입력\n";

                    //    continue;
                    //}

                    //if (custCd == "" && custNm == "")
                    //{
                    //    worksheet.Rows[seq].Font.Color = Color.Red;
                    //    Error = "Y";
                    //    dataRow["Chk"] = "거래처코드, 거래처명 미입력";
                    //    message += $"{seq} 행 거래처코드, 거래처명 미 입력\n";
                    //    continue;
                    //}

                    //단가구분 변환
                    DataRow drPType = code.GetCM_CateItemInfo_ByTitle(WrGlobal.CorpID, "S210", priceType);

                    if (drPType != null)
                    {
                        dataRow["PriceType"] = drPType["ItemCode"];
                    }

                    //거래처 확인
                    DataRow drCust = null;

                    if (custCd != "")
                    {
                        drCust = Select_CustRow("Code", custCd, custNm);

					}
					else
					{
                        //거래처명으로 거래처코드 찾기

                        drCust = Select_CustRow("", custCd, custNm);

                        if (drCust != null)
                        {
                            dataRow["CustCode"] = drCust["CustCode"];
                        }
                    }

                    if(drCust == null)
					{
                        worksheet.Rows[seq].Font.Color = Color.Red;
                        Error = "Y";
                        dataRow["Chk"] = "거래처 이상";
                        message += $"{seq} 행 거래처 이상\n";
                        continue;
                    }

                    //품목 확인
                    DataRow drMaterial = null;

					if (materialCd != "")
					{
						drMaterial = Select_ItemRow("Code", materialCd, materialNm);

					}
					else
					{
                        //품명으로 품목코드 찾기
                        drMaterial = Select_ItemRow("", materialCd, materialNm);

                        if (drMaterial != null)
                        {
                            dataRow["ItemCode"] = drMaterial["ItemCode"];
                        }
                    }

					if (drMaterial == null)
                    {
                        worksheet.Rows[seq].Font.Color = Color.Red;
                        Error = "Y";
                        dataRow["Chk"] = "품목 이상";
                        message += $"{seq} 행 품목 이상\n";

                        continue;
                    }
                }

                if (Error == "Y")
				{
					XtraMessageBox.Show("잘못 입력된 값이 있거나 필수 값 미입력 행이 있습니다.\n"+ message, "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				foreach (DataRow dr in dtExcel.Rows)
				{
					//거래처, 품목
					if (Convert.ToString(dr["CustCode"]) == "" || Convert.ToString(dr["ItemCode"]) == "")
					{
						continue;
					}

					if (!SaveData(dr))
					{
						XtraMessageBox.Show("저장 중 에러가 발생하였습니다.");
						break;
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
            //GridView gridView = Gc.MainView as GridView;
            //foreach (var col in columns)
            //{
            //    gridView.Columns.Add(col);
            //}

            foreach (GridColumn gridColumn in columns)
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

                CM_ItemPrice mp = new CM_ItemPrice();
                mp.ItemCode = Convert.ToString(dr["ItemCode"]);
                mp.PriceType = Convert.ToString(dr["PriceType"]);
                mp.CustCode = Convert.ToString(dr["CustCode"]);
                mp.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
                mp.ApplyDate = dr["ApplyDate"].ConvertString_Null_To_Empty() == "" ? DateTime.Now.ToString("d") : Convert.ToDateTime(dr["ApplyDate"]).ToString("yyyy-MM-dd");
                mp.EndDate = dr["EndDate"].ConvertString_Null_To_Empty() == "" ? "" : dr["EndDate"].ConvertDate_To_Char10();
                mp.CustAlias = dr["CustAlias"].ConvertString_Null_To_Empty();
                mp.Remark = dr["Remark"].ConvertString_Null_To_Empty();
                mp.Ins_MaterialPrice();

                //CM_ItemPriceHistory history = new CM_ItemPriceHistory();
                //history.PriceType = Convert.ToString(dr["PriceType"]);
                //history.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
                //history.ApplyDate = mp.ApplyDate;
                //history.Remark = dr["Remark"].ConvertString_Null_To_Empty();
                //history.ItemCode = Convert.ToString(dr["ItemCode"]);
                //history.CustCode = Convert.ToString(dr["CustCode"]);
                //history.Ins_MaterialPriceHistory();

                return true;
            }

            catch(Exception ex)
			{
                return false;
            }
        }


        private DataRow Select_ItemRow(string type, string materialCd, string materialNm)
        {
            mat = new CM_Item();

            if (type == "Code")
            {
                mat.ItemCode = materialCd;
            }
            else
            {
                mat.ItemName = materialNm;
            }

            DataRow drItem = mat.Select_Item_RowInfo();

            if (drItem == null)
                return null;

            return drItem;
        }

        private DataRow Select_CustRow(string type ,string custCode, string custName)
        {
            cust = new CM_Cust();

            if(type == "Code")
			{
                cust.CustCode = custCode;
			}
			else
			{
                cust.CustName = custName;
			}

            DataRow drCust = cust.Select_Cust_Info();

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