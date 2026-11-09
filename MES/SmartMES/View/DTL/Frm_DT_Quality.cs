using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using SmartCommon.Common;
using System.Data;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using SmartMES.Models.PM;

namespace SmartMES.View.DTL
{
    public partial class Frm_DT_Quality : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();


        public Frm_DT_Quality()
        {
            InitializeComponent();
        }

        private void frmPM_OrderProcessView_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            DisplayData();
        }

        #region setDesign
        private void SetLup()
        {
            // 날짜
            DtOrder.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            DtOrder.smtEndDateTimeValue = DateTime.Now;

            // 공정
            Lup_Process.smtLookUpDataSource = code.GetProcess_List(true);;
            Lup_Process.smtLookUpDataValueMember = "공정코드";
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpValue = "ALL";
        }

        private void SetGridView()
        {
            SetGridPM_ProductPlan(Gv_DT);
        }

        private void SetGridPM_ProductPlan(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();

            // Repository: displayMember를 코드로 고정
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(null), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            // 컬럼 생성 (화면 표시명, 바인딩 필드명, 에디터)
            grid.Create_GvColumn(gv, "발생일시", "OrderDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "설비명", "ProcessName");
            grid.Create_GvColumn(gv, "오류유형", "LineName");
            grid.Create_GvColumn(gv, "오류항목", "MachineName");
            grid.Create_GvColumn(gv, "원본값", "PgRate");
            grid.Create_GvColumn(gv, "정제값", "NgRate");
            grid.Create_GvColumn(gv, "처리상태", "OrderQty");

            // 포맷 설정
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            //grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["PgRate"], clsGrid.TypeList.TFloatN1);
            
            // 정렬
            gv.Columns["LineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // Visible = false
            //gv.Columns["OrderSeq"].Visible = false;

            // 네비게이터 설정
            grid.Set_Navigator(Gv_DT);

            // Event
            //gv.CustomDrawCell += gv_CustomDrawCell;
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("UrgentYn") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "긴급")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Red;
            }
        }

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            baseSMT.smtSearchButton Btn = (baseSMT.smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                //조회
                case "cmdDisplay":
                    DisplayData();
                    break;

                //엑셀다운
                case "cmdExcel":
                    cmdExcel_Click();
                    break;
            }
        }

        #region Event
        //엑셀변환 하기
        private void cmdExcel_Click()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_DT.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event

        #region 조회
        private void DisplayData()
        {
			string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char8();
			string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char8();

   //         PM_OrderProcess op = new PM_OrderProcess();
   //         op.ItemCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;

			//GcPM_OrderProcess.DataSource = op.Select_OrderPropcess(startDate, endDate);

			//GvPM_OrderProcess.BestFitColumns();
		}
        #endregion 조회
    }
}