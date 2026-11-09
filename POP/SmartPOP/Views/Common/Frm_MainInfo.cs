using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Win32;
using SmartERP.Models;
using SmartERP.Models.SM;
using SmartPOP.Common;
using SmartPOP.Model.EQ;
using SmartPOP.Models;
using SmartPOP.Models.PM;
using SmartPOP.Views.Lookup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.Common
{
    public partial class Frm_MainInfo : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsCode code = new clsCode();
        cls_Grid grid = new cls_Grid();
        string query = "";
        string[] MachineDiv = null;
        string mc = string.Empty;

        public Frm_MainInfo()
        {
            InitializeComponent();

            Set_Lup();

            //
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in GvNotice.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gvPM_OrderProcess.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            grid.SetPOPGrid(GvNotice);
            grid.SetPOPGrid(gvPM_OrderProcess);

            MachineByID();
            DisplayData();
            DisplayPM_OrderProcess();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
            DisplayPM_OrderProcess();
        }

        private void DisplayData()
        {
            SM_Notice notice = new SM_Notice();
            DataTable dt = notice.Select_Notice_View();

            GcNotice.DataSource = dt;
        }

        private void DisplayPM_OrderProcess()
        {
            // 작업지시 조회
            PM_OrderProcess op = new PM_OrderProcess();
            op.StartDate = DateTime.Now.AddDays(-7).Date;
            op.EndDate = DateTime.Now.Date;
            op.machineCode = mc;

            gcPM_OrderProcess.DataSource = op.Select_OrderList_Main();
            gvPM_OrderProcess.BestFitColumns();
        }

        private void Set_Lup()
        {
            repositoryItemLookUpEdit1.DataSource = code.smtLookUp("사용자", "", "");
            repositoryItemLookUpEdit1.ValueMember = "사원ID";
            repositoryItemLookUpEdit1.DisplayMember = "사원명";
            repositoryItemLookUpEdit1.NullText = "";

            repositoryItemSearchLookUpEdit2.DataSource = code.GetProcessListDt();
            repositoryItemSearchLookUpEdit2.DisplayMember = "명칭";
            repositoryItemSearchLookUpEdit2.ValueMember = "코드";
            repositoryItemSearchLookUpEdit2.NullText = "";

            repositoryItemSearchLookUpEdit1.DataSource = code.GetLineLookUpList();
            repositoryItemSearchLookUpEdit1.DisplayMember = "명칭";
            repositoryItemSearchLookUpEdit1.ValueMember = "코드";
            repositoryItemSearchLookUpEdit1.NullText = "";
        }

        private void MachineByID()
        {
            EQ_Machine machine = new EQ_Machine();
            machine.ModelNo = WrGlobal.LoginID;

            DataTable dtMachine = machine.Select_Machine_ByID();
            MachineDiv = dtMachine.AsEnumerable().Select(row => row["MachineCode"].ToString()).ToArray();

            if (MachineDiv.Length > 0)
            {
                mc = string.Join("','", MachineDiv);
            }
        }

        private void RowCellDisplayValue(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            //if (fieldName.Contains("Status") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (fieldName.Contains("Status"))
            {
                if (cellValue == "작업중")
                {
                    //진행
                    e.Appearance.ForeColor = Color.DarkGreen;
                    e.DisplayText = "● " + cellValue;
                }
                else if (cellValue == "대기")
                {
                    //대기
                    e.Appearance.ForeColor = Color.Black;
                    e.DisplayText = "● " + cellValue;
                }
                else if (cellValue == "작업완료")
                {
                    //종료
                    e.Appearance.ForeColor = Color.Red;
                    e.DisplayText = "● " + cellValue;
                }
            }
        }

        private void GvNotice_DoubleClick(object sender, EventArgs e)
        {
            OpenNotice();
        }

        #region 공지사항
        string lastDate = "";
        private void OpenNotice()
        {
            //현재시간
            DateTime dDatetime = DateTime.Now;
            string checkKey = "CheckDate_" + WrGlobal.DBName;

            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISPOP", true);
            if (reg == null) return;
            string CheckDate = reg.GetValue(checkKey) == null ? "" : reg.GetValue(checkKey).ToString();
            if (CheckDate == dDatetime.ToString("yyyyMMdd")) return;

            query = $@"
                        select count(*) from SM_Notice
                        where CorpId = {WrGlobal.CorpID} and '{dDatetime.ToString("yyyy-MM-dd")}' between convert(date,StartDate) and convert(date,EndDate)
                    ";
            int count = db.ExecuteScalarQuery(query);
            if (count <= 0) return;

            Frm_LookUpNotice frmNotice = new Frm_LookUpNotice(dDatetime);
            if (frmNotice.ShowDialog() == DialogResult.OK)
            {
                reg.SetValue(checkKey, dDatetime.ToString("yyyyMMdd"));
            }

            lastDate = dDatetime.ToString("yyyyMMdd");
            //if (!tmr_Notice.Enabled) tmr_Notice.Enabled = true;
        }


        #endregion 공지사항

	}
}