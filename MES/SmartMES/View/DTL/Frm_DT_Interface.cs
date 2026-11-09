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
using SmartMES.Models.EQ;

namespace SmartMES.View.DTL
{
    public partial class Frm_DT_Interface : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();


        public Frm_DT_Interface()
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
            // 공정
            Lup_Machine.smtLookUpDataSource = code.GetMachineList(true);;
            Lup_Machine.smtLookUpDataValueMember = "설비코드";
            Lup_Machine.smtLookUpDataDisplayMember = "설비명";
            Lup_Machine.smtLookUpValue = "ALL";
        }

        private void SetGridView()
        {
            SetGridPM_ProductPlan(GvPM_OrderProcess);
        }

        private void SetGridPM_ProductPlan(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            // 컬럼 생성 (화면 표시명, 바인딩 필드명, 에디터)
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "연결상태", "");

            // 네비게이터 설정
            grid.Set_Navigator(GvPM_OrderProcess);
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
                GvPM_OrderProcess.ExportToXls(sfd.FileName);

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
            EQ_Machine machine = new EQ_Machine();
            machine.MachineCode = Lup_Machine.smtLookUpValue == "ALL" ? "" : Lup_Machine.smtLookUpValue;

            DataTable dt = machine.SelectMachine();

            GcPM_OrderProcess.DataSource = dt;
            GvPM_OrderProcess.BestFitColumns();

            Set_Card(dt);
        }
        #endregion 조회

        #region 카드 셋팅
        private void Set_Card(DataTable dt)
        {
            if (dt != null && dt.Rows.Count != 0)
            {
                int machineCnt = dt.Rows.Count;

                LblMachineCnt.Text = machineCnt.ConvertString_Null_To_Empty();
            }
            else
            {
                LblMachineCnt.Text = "0";
                LblCompleLink.Text = "0";
                LblDisconnect.Text = "0";
                LblChk.Text = "0";
            }
        }
        #endregion
    }
}