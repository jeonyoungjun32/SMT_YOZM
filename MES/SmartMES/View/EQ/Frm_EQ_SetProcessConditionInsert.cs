using SmartCommon.Common;
using SmartMES.Models.EQ;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_SetProcessConditionInsert : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        string Code = "";

        public Frm_EQ_SetProcessConditionInsert(string code = null)
        {
            Code = code;

            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            query = $"SELECT MachineCode 설비코드, MachineName 설비명 FROM EQ_Machine WHERE CorpId = '{WrGlobal.CorpID}' AND MachineCode IN ('MC001', 'MC002')";
            machineNameSearchLookupEdit.Properties.DataSource = db.GetDataView("설비", query);
            machineNameSearchLookupEdit.Properties.DisplayMember = "설비명";
            machineNameSearchLookupEdit.Properties.ValueMember = "설비코드";

            if (Code != null)
            {
                query = $@"
                        SELECT
                            mc.DgpCode
                            , mc.DgpName
                            , mc.Equip_Name
                            , cm.MachineName
                        FROM MTDGPCode mc
                        LEFT JOIN EQ_Machine cm ON mc.Equip_Name = cm.MachineCode
                        WHERE mc.CorpId = '{WrGlobal.CorpID}'
                        AND mc.DgpCode = '{Code}'
                    ";

                DataRow dr = db.GetDataRow(query);

                machineNameSearchLookupEdit.Text = dr["Equip_Name"].ToString();
                dgpNameSearchLookUpEdit.Text = dr["DgpCode"].ToString();

                machineNameSearchLookupEdit.Properties.ReadOnly = true;
                dgpNameSearchLookUpEdit.Properties.ReadOnly = true;
                machineNameSearchLookupEdit.BackColor = System.Drawing.Color.White;
                dgpNameSearchLookUpEdit.BackColor = System.Drawing.Color.White;
            }

            workDateTextEdit.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void machineNameSearchLookupEdit_EditValueChanged(object sender, EventArgs e)
        {
            machineCodeTextEdit.Text = null;
            dgpCodeTextEdit.Text = null;
            dgpNameSearchLookUpEdit.Text = null;
            dgpNameSearchLookUpEdit.Properties.DataSource = null;

            if (((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue == null)
            {
                return;
            }

            string machineCode = ((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue.ToString();
            machineCodeTextEdit.Text = machineCode;

            query = $"SELECT DgpCode 계측기코드, DgpName 계측기명 FROM MTDGPCode WHERE CorpId = '{WrGlobal.CorpID}' AND Equip_Name = '{machineCode}' AND Gauge_Type = '설정'";
            dgpNameSearchLookUpEdit.Properties.DataSource = db.GetDataView("계측기", query);
            dgpNameSearchLookUpEdit.Properties.DisplayMember = "계측기명";
            dgpNameSearchLookUpEdit.Properties.ValueMember = "계측기코드";
        }

        private void dgpNameSearchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            dgpCodeTextEdit.Text = null;

            if (((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue == null)
            {
                return;
            }

            string dgpCode = ((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue.ToString();
            dgpCodeTextEdit.Text = dgpCode;
        }

        private void cmdSave_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            if (machineCodeTextEdit.Text == "")
            {
                MessageBox.Show("설비를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgpCodeTextEdit.Text == "")
            {
                MessageBox.Show("계측기를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (setValueTextEdit.Text == "")
            {
                MessageBox.Show("설정값을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float num;
            bool numChk = float.TryParse(setValueTextEdit.Text, out num);

            if (!numChk)
            {
                MessageBox.Show("설정값에 문자는 입력할 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Save()) return;

            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private bool Save()
        {
            //MachineControl mc = new MachineControl();
            //mc.Equip_Name = machineCodeTextEdit.Text;
            //mc.DgpCode = dgpCodeTextEdit.Text;
            //mc.WorkDate = workDateTextEdit.Text;
            //mc.SetValue = Convert.ToInt32(setValueTextEdit.Text);
            //
            //if (!mc.Insert_MachineControl())
            //{
            //    MessageBox.Show("저장 중 에러가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            //
            //if (!mc.Update_MTDGPLink())
            //{
            //    MessageBox.Show("저장 중 에러가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            return true;
        }

        private void cmdClose_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            this.Close();
        }
    }
}