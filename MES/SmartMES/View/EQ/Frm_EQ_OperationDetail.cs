using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.EQ;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_OperationDetail : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();

        string MachineCode = "";
        int Seq = 0;

        public Frm_EQ_OperationDetail()
        {
            InitializeComponent();
        }

        public Frm_EQ_OperationDetail(string machineCode, int seq = 0, bool saveYn = true)
        {
            InitializeComponent();

            MachineCode = machineCode;
            Seq = seq;
            btn_Save.Visible = saveYn;

            if (machineCode != "")
            {
                Lup_MachineName.smtLookUpMust = false;
                Lup_MachineName.smtLookUpReadOnly = true;
            }
        }

        private void Frm_EQ_OperationDetail_Load(object sender, EventArgs e)
        {
            Set_Design();
            Func_Seach();
        }

        private void Set_Design()
        {
            Dt_WorkDay.smtDateTimeValue = DateTime.Now;
            Dt_StartDate.smtDateTimeValue = DateTime.Today;
            Dt_EndDate.smtDateTimeValue = DateTime.Today;

            Lup_MachineName.smtLookUpDataSource = code.Select_Machine_Lup(true);
            Lup_MachineName.smtLookUpDataDisplayMember = "설비명";
            Lup_MachineName.smtLookUpDataValueMember = "설비코드";

            Lup_ItemCode.smtLookUpDataSource = code.GetClassList("2030M");
            Lup_ItemCode.smtLookUpDataDisplayMember = "분류명";
            Lup_ItemCode.smtLookUpDataValueMember = "분류코드";
        }

        #region 조회 기능

        private void Func_Seach()
        {
            if (MachineCode == "") { return; }

            EQ_Operation mo = new EQ_Operation();
            mo.MachineCode = MachineCode;
            mo.Seq = Seq;

            DataRow dr = mo.Select_EQ_Operation_ByCode();

            Txt_MachineCode.smtTextBoxValue = dr["MachineCode"].ConvertString_Null_To_Empty();
            Seq = dr["Seq"].ConvertInt_Null_To_One();
            Lup_MachineName.smtLookUpValue = dr["MachineCode"].ConvertString_Null_To_Empty();
            Lup_ItemCode.smtLookUpValue = dr["ItemCode"].ConvertString_Null_To_Empty();
            Dt_WorkDay.smtDateTimeValue = Convert.ToDateTime(dr["WorkDay"]);
            Dt_StartDate.smtDateTimeValue = Convert.ToDateTime(dr["StartTime"]);
            Dt_EndDate.smtDateTimeValue = Convert.ToDateTime(dr["EndTime"]);
        }

        #endregion 조회 기능

        #region 저장 기능

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!Chk_Save()) { return; }

            EQ_Operation mo = new EQ_Operation();
            mo.MachineCode = Txt_MachineCode.smtTextBoxValue;
            mo.Seq = Seq == 0 ? mo.Get_LastEQ_OperationSeq()["Seq"].ConvertInt_Null_To_One() : Seq;
            mo.WorkDay = Dt_WorkDay.smtDateTimeValue.ConvertDate_To_Char8();
            mo.StartTime = Dt_StartDate.smtDateTimeValue.ToString("yyyy-MM-dd HH:mm");
            mo.EndTime = Dt_EndDate.smtDateTimeValue.ToString("yyyy-MM-dd HH:mm");
            mo.OperationYn = "N";   // 비가동
            mo.CLASS = "2030M";     // 설비 비가동사유
            mo.ItemCode = Lup_ItemCode.smtLookUpValue;

            if (MachineCode == "")
            {
                if (!mo.Ins_EQ_Operation())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;
                }
            }
            else
            {
                if (!mo.Upd_EQ_Operation())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;

                }
            }

            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private bool Chk_Save()
        {
            if (Lup_MachineName.smtLookUpValue == "")
            {
                MessageBox.Show("설비명을 선택해 주십시오", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Dt_WorkDay.smtDateTimeValue.ConvertString_Null_To_Empty() == "")
            {
                MessageBox.Show("비가동일자 선택해 주십시오", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Dt_StartDate.smtDateTimeValue.ConvertString_Null_To_Empty() == "")
            {
                MessageBox.Show("시작시간을 선택해 주십시오", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Dt_EndDate.smtDateTimeValue.ConvertString_Null_To_Empty() == "")
            {
                MessageBox.Show("종료시간을 선택해 주십시오", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        #endregion 저장 기능

        #region 이벤트

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lup_MachineName_smtLookUpEditValueChanged(object sender, EventArgs e)
        {
            Txt_MachineCode.smtTextBoxValue = Lup_MachineName.smtLookUpValue;
        }

        #endregion 이벤트

    }
}