using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SmartMES.Models.PM;
using SmartMES.Models.QM;
using SmartMES.Models.ST;
using SmartMES.View.PM.Popup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.DTL
{
    public partial class Frm_DT_OrderProcessbyResult : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        CM_Process process = new CM_Process();

        string ItemType = WrGlobal.ProcOption["SC"]["Product_ItemType"];
        string DefaultProcessCode = "";

        Database db = new Database();
        string query = "";

        public Frm_DT_OrderProcessbyResult()
        {
            InitializeComponent();
            Set_GridView_Rst(Gv_ProcessResult);    // 생산실적
        }

        private void frmPM_OrderProcessResult_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);

            SetLup();

            Search_Rst();
        }

        #region setDesign
        private void SetLup()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;
           
            DataTable dt = code.GetProcess_List_ProcessType(true, null);//
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    DefaultProcessCode = dt.Rows[0]["공정코드"].ConvertString_Null_To_Empty();
                }
            }

            Lup_Process.smtLookUpDataSource = dt;
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpDataValueMember = "공정코드";
            Lup_Process.smtLookUpValue = "ALL";
        }

        private void Set_GridView_Rst(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            // 그리드 헤더 줄 바꿈 <Br>
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            clsRepository rpo = new clsRepository();
            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");

            grid.Create_GvColumn(gv, "작업일자", "WorkDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "달성률", "AchievementRate");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "양품률", "GoodRate");
            grid.Create_GvColumn(gv, "수정여부", "Modify");
            // 포맷
            grid.SetCol_Type(gv.Columns["WorkDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TInt);

            gv.Columns["ProcessCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["Modify"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //이벤트
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.ValidatingEditor += Gv_ValidatingEditor;
            gv.CustomDrawCell += gv_CustomDrawCell;
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("Modify") == false) 
                return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "수정됨")
            {
                e.Appearance.ForeColor = Color.Red;
                //e.Appearance.BackColor = Color.Red;
            }
        }

        private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            DataRow dr = gv.GetFocusedDataRow();
            string ColName = gv.FocusedColumn.FieldName;

            switch (ColName)
            {
                case "ItemName":

                    if (dr["SerialNo"].ConvertString_Null_To_Empty() != "")
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = "등록된 실적 건의 품목을 변경할 수 없습니다.";
                    }
                    break;
            }
        }

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            smtSearchButton Btn = sender as smtSearchButton;
            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnSearch":
                    //조회
                    Search_Rst();
                    break;

                case "cmdExcel":
                    //엑셀다운
                    cmdExcel_Click();
                    break;
            }
        }

        #region 조회

        private void Search_Rst()
        {
            PM_OrderResult rst = new PM_OrderResult();

            rst.StartDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            rst.EndDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            rst.ProcessCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;

            Gc_ProcessResult.DataSource = rst.Select_PM_OrderResultEditView();
            Gv_ProcessResult.BestFitColumns();
        }

        #endregion 조회

        #region Event

        private void cmdExcel_Click()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_ProcessResult.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event


    }
}