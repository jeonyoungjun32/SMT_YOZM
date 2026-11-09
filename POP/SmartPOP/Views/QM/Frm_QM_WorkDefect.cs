using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartPOP.Common;
using SMT_MES.Controls;

namespace SmartPOP.Views.QM
{
    public partial class Frm_QM_WorkDefect : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        string serialNo;


        public Frm_QM_WorkDefect()
        {
            InitializeComponent();

        }

        public Frm_QM_WorkDefect(string serialNo)
        {
            InitializeComponent();

            this.serialNo = serialNo;
        }

        private void frmDefectReg_Load(object sender, EventArgs e)
        {
            Set_Info();
            Set_Defective();
        }

        #region 셋팅
        private void Set_Info()
        {
            query = $@"SELECT	SM.SerialNo
		                        , SM.OrderNo
                                , SM.OrderSeq
		                        , OP.HalfMaterialCode MaterialCode
		                        , CM.MaterialName
		                        , ORL.ProcessCode
		                        , CP.ProcessName
		                        , ORL.StartDate
		                        , ORL.EndDate
                        FROM SerialMaster SM
                        INNER JOIN PM_OrderResult  ORL ON SM.SerialNo = ORL.SerialNo
                        INNER JOIN PM_OrderProcess OP ON ORL.OrderNo = OP.OrderNo AND ORL.OrderSeq = OP.OrderSeq
                        INNER JOIN CM_Item CM ON CM.MaterialCode = OP.HalfMaterialCode
                        INNER JOIN CM_Process CP ON ORL.ProcessCode = CP.ProcessCode
                        WHERE SM.CorpID = '{WrGlobal.CorpID}'
                        AND SM.SerialNo = '{serialNo}'
                        ";

            DataTable dt = db.GetDataTable(query);

            txtSerialNo.Text = dt.Rows[0]["SerialNo"].ToString();
            TxtOrderNo.Text = dt.Rows[0]["OrderNo"].ToString();
            TxtOrderSeq.Text = dt.Rows[0]["OrderSeq"].ToString();
            TxtMaterialCode.Text = dt.Rows[0]["MaterialCode"].ToString();
            TxtMaterialName.Text = dt.Rows[0]["MaterialName"].ToString();
            TxtProcessCode.Text = dt.Rows[0]["ProcessCode"].ToString();
            TxtProcessName.Text = dt.Rows[0]["ProcessName"].ToString();
            TxtWorker.Text = WrGlobal.LoginUserNM;
        }

        private void Set_Defective()
        {
            string processCode = TxtProcessCode.EditValue?.ToString();

            query = $@"SELECT DISTINCT FaultCode 코드, Title 코드명
                        FROM CM_DefectCode CD --CM_LineProcessWorker CA
                        --INNER JOIN CM_DefectCode CD ON CD.ProcessID = CA.ProcessCode
                        WHERE CD.CorpID = '{WrGlobal.CorpID}'
                        --AND CA.EmpNo = '{WrGlobal.LoginID}'
                        AND CD.ProcessID = '{processCode}'
                        ";

            LupDefective.Properties.DataSource = db.GetDataTable(query);
            LupDefective.Properties.ValueMember = "코드";
            LupDefective.Properties.DisplayMember = "코드명";
        }
        #endregion

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cls_MessageBox.Show("부적합수량을 등록하시겠습니까?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string defectiveCode = LupDefective.EditValue?.ToString();
            double ngQty = Convert.ToDouble(TxtNgQty.EditValue?.ToString());
            string remark = memoRemark.EditValue?.ToString();

            if (defectiveCode == null) defectiveCode = "";
            if (remark == null) remark = "";

            if (defectiveCode == "")
            {
                cls_MessageBox.Show("부적합유형을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ngQty == 0)
            {
                cls_MessageBox.Show("부적합수량을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            query = $@"EXEC POP_WriteQM_WorkDefect_NEW '{WrGlobal.CorpID}', '{serialNo}', '{defectiveCode}', {ngQty}, '{remark}', '{WrGlobal.LoginID}'";
            db.ExecuteQuery(query);
            cls_MessageBox.Show("부적합수량을 등록했습니다.");

            this.Close();
            
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}