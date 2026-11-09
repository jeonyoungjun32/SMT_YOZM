using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using SmartMES.Common;
using SmartMES.View.EQ;
using SmartMES.Models.IF;
using SmartCommon.Common;

namespace SmartMES.UserControls
{
    /*
     DGP GaugeType을 불러와 항목명으로 사용함
     */
    public partial class UCDGP2 : DevExpress.XtraEditors.XtraUserControl
    {
        List<LabelControl> lstDgpNm = null;
        List<LabelControl> lstValue = null;
        List<SimpleButton> lstBtn = null;

        public int Cnt = 3; //항목 갯수
        public int MaxCnt = 20; //항목 최대 갯수

        public UCDGP2()
        {
            InitializeComponent();
            Init();
        }

        public UCDGP2(int cnt)
{
            InitializeComponent();

            Cnt = cnt;  //업채별 항목갯수 다를때

            Init();
        }

        private void Init()
        {
            lstDgpNm = new List<LabelControl>() { LblC1_Nm, LblC2_Nm, LblC3_Nm, LblC4_Nm, LblC5_Nm, LblC6_Nm, LblC7_Nm, LblC8_Nm, LblC9_Nm, LblC10_Nm };
            lstValue = new List<LabelControl>() { LblM1_1, LblM1_2, LblM1_3, LblM1_4, LblM1_5, LblM1_6, LblM1_7, LblM1_8, LblM1_9, LblM1_10, LblM1_11, LblM1_12, LblM1_13, LblM1_14, LblM1_15, LblM1_16, LblM1_17, LblM1_18, LblM1_19, LblM1_20 };
            lstBtn = new List<SimpleButton>() { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10 };

            //textBox1.Focus();
            //
            //if (Cnt < MaxCnt)
            //{
            //    for (int i = MaxCnt; i > MaxCnt - Cnt; i--)
            //    {
            //        TablePanelRow row = TpMachine1.Rows[i];
            //        if (row == null) continue;

            //        row.Visible = false;
            //    }
            //}
        }

        public void Display(string MachineCode)
        {
            IF_DGPCode mTDGPCode = new IF_DGPCode();
            mTDGPCode.Equip_Name = MachineCode;

            //DataTable dt = mTDGPCode.Select_ActiveQty();
            DataTable dt = mTDGPCode.Select_DGPTemp_SM();
            if (dt == null) return;

            float minValue = 0;
            float maxValue = 0;
            float nowValue = 0;
            float setValue = 0;
            int valCnt = 0;
            int cnt = 0;
            string dgpName = "";

            foreach (DataRow item in dt.Rows)
            {
                if (cnt >= MaxCnt) continue;

                //minValue = item["Value_Min"].ConvertFloat_Null_To_Zero();
                //maxValue = item["Value_Max"].ConvertFloat_Null_To_Zero();
                nowValue = item["NowValue"].ConvertFloat_Null_To_Zero();
                setValue = item["SetValue"].ConvertFloat_Null_To_Zero();
                dgpName = item["DgpName"].ConvertString_Null_To_Empty();

                lstDgpNm[cnt].Text = dgpName;
                lstDgpNm[cnt].BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                lstValue[valCnt].Text = string.Format("{0:#,###0.##}", nowValue);
                lstValue[valCnt].BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                valCnt++;
                lstValue[valCnt].Text = string.Format("{0:#,###0.##}", setValue);
                lstValue[valCnt].BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

                lstBtn[cnt].Text = "설정";
                lstBtn[cnt].Tag = item["DgpCode"].ConvertString_Null_To_Empty();
                lstBtn[cnt].Visible = true;

                //if (curValue != 0)
                //{
                //    if (minValue > curValue || maxValue < curValue)
                //    {
                //        lstValue[cnt].Appearance.BackColor = Color.OrangeRed;
                //        lstValue[cnt].Appearance.ForeColor = Color.White;
                //    }
                //    else
                //    {
                //        lstValue[cnt].Appearance.BackColor = Color.Empty;
                //        lstValue[cnt].Appearance.ForeColor = Color.Aqua;
                //    }
                //}
                //else
                //{
                //    //curValue가 0일 경우
                //    lstValue[cnt].Appearance.BackColor = Color.Empty;
                //    lstValue[cnt].Appearance.ForeColor = Color.Aqua;
                //}

                cnt++;
                valCnt++;
            }

            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    Lbl_M1_Nm.Text = dt.Rows[0]["MachineName"].ConvertString_Null_To_Empty();
                }
            }
        }

        public void DisplayByLine(string MachineCode)
        {
            IF_DGPCode mTDGPCode = new IF_DGPCode();
            mTDGPCode.Equip_Name = MachineCode;

            DataTable dt = mTDGPCode.Select_ActiveQty();
            if (dt == null) return;

            float minValue = 0;
            float maxValue = 0;
            float curValue = 0;
            int cnt = 0;

            foreach (DataRow item in dt.Rows)
            {
                if (cnt >= MaxCnt) continue;

                minValue = item["Value_Min"].ConvertFloat_Null_To_Zero();
                maxValue = item["Value_Max"].ConvertFloat_Null_To_Zero();
                curValue = item["Active_Qty"].ConvertFloat_Null_To_Zero();

                lstDgpNm[cnt].Text = item["Gauge_Type"].ConvertString_Null_To_Empty();
                lstValue[cnt].Text = string.Format("{0:#,###0.##}", curValue);

                if (curValue != 0)
                {
                    if (minValue > curValue || maxValue < curValue)
                    {
                        lstValue[cnt].Appearance.BackColor = Color.OrangeRed;
                    }
                }

                cnt++;
            }

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Lbl_M1_Nm.Text = dt.Rows[0]["Machine_Nm"].ConvertString_Null_To_Empty();
                }
            }
        }

        public void DisplayDGP(string dgpCode)
        {
            //DGP 공정조건 모니터링

            IF_DGPCode mTDGPCode = new IF_DGPCode();
            mTDGPCode.DgpCode = dgpCode;

            DataTable dt = mTDGPCode.Select_ActiveQty();
            if (dt == null) return;

            DataRow dr = null;

            if(dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }

            if (dr == null) return;

            Lbl_M1_Nm.Text = dr["DgpName"].ConvertString_Null_To_Empty();

            LblC1_Nm.Text = "상한";
            LblC2_Nm.Text = "현재";
            LblC3_Nm.Text = "하한";

            LblM1_1.Text = String.Format("{0:#,###0.##}", dr["Value_Max"].ConvertFloat_Null_To_Zero());
            LblM1_2.Text = String.Format("{0:#,###0.##}", dr["Active_Qty"].ConvertFloat_Null_To_Zero());
            LblM1_3.Text = String.Format("{0:#,###0.##}", dr["Value_Min"].ConvertFloat_Null_To_Zero());
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            string dgpCode = btn.Tag.ToString();

            Frm_EQ_SetProcessConditionInsert form = new Frm_EQ_SetProcessConditionInsert(dgpCode);
            form.ShowDialog();
        }
    }
}
