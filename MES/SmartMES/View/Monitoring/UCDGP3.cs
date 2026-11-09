using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.IF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace SmartMES.View.Monitoring
{
    /*
     DGP GaugeType을 불러와 항목명으로 사용함
     */
    public partial class UCDGP3 : DevExpress.XtraEditors.XtraUserControl
    {
        List<LabelControl> lstDgpNm = null;
        List<LabelControl> lstValue = null;

        public int Cnt = 3; //항목 갯수
        public int MaxCnt = 6; //항목 최대 갯수

        public UCDGP3()
        {
            InitializeComponent();
            Init();
        }

        public UCDGP3(int cnt)
        {
            InitializeComponent();

            Cnt = cnt;  //업채별 항목갯수 다를때

            Init();
        }

        private void Init()
        {
            lstDgpNm = new List<LabelControl>() { LblC1_Nm, LblC2_Nm, LblC3_Nm, LblC4_Nm, LblC5_Nm, LblC6_Nm };
            lstValue = new List<LabelControl>() { LblM1_1, LblM1_2, LblM1_3, LblM1_4, LblM1_5, LblM1_6 };

            //
            if (Cnt < MaxCnt)
            {
                for (int i = MaxCnt; i > MaxCnt - Cnt; i--)
                {
                    TablePanelRow row = TpMachine1.Rows[i];
                    if (row == null) continue;

                    row.Visible = false;
                }
            }

            //항목 많을 경우 폰트 잘림 조정
            if(MaxCnt>5)
            {
                foreach (LabelControl item in lstValue)
                {
                    item.Font = new Font("맑은 고딕", 21, FontStyle.Bold);
                }
            }
        }

        public void Display(string MachineCode)
        {
            IF_DGPCode mTDGPCode = new IF_DGPCode();
            mTDGPCode.Equip_Name = MachineCode;

            DataTable dt = mTDGPCode.Select_ActiveQty();
            if (dt == null) return;

            float minValue = 0;
            float maxValue = 0;
            float curValue = 0;
            double tempValue = 0;
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
                        lstValue[cnt].Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        lstValue[cnt].Appearance.BackColor = Color.Empty;
                        lstValue[cnt].Appearance.ForeColor = Color.Aqua;
                    }
                }
                else
                {
                    //curValue가 0일 경우
                    lstValue[cnt].Appearance.BackColor = Color.Empty;
                    lstValue[cnt].Appearance.ForeColor = Color.Aqua;
                }

                cnt++;
            }

            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    Lbl_M1_Nm.Text = dt.Rows[0]["MachineName"].ConvertString_Null_To_Empty();
                }
            }
        }

        public void Display(DataTable dt)
        {
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
                        lstValue[cnt].Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        lstValue[cnt].Appearance.BackColor = Color.Empty;
                        lstValue[cnt].Appearance.ForeColor = Color.Aqua;
                    }
                }
                else
                {
                    //curValue가 0일 경우
                    lstValue[cnt].Appearance.BackColor = Color.Empty;
                    lstValue[cnt].Appearance.ForeColor = Color.Aqua;
                }

                cnt++;
            }

            if (dt.Rows.Count > 0)
            {
                Lbl_M1_Nm.Text = dt.Rows[0]["MachineName"].ConvertString_Null_To_Empty();
            }
        }

        public void DisplayDGP(DataRow dr)
        {
            if (dr == null) return;

            Lbl_M1_Nm.Text = dr["DgpName"].ConvertString_Null_To_Empty();

            LblC1_Nm.Text = "상한";
            LblC2_Nm.Text = "현재";
            LblC3_Nm.Text = "하한";

            LblM1_1.Text = String.Format("{0:#,###0.##}", dr["Value_Max"].ConvertFloat_Null_To_Zero());
            LblM1_2.Text = String.Format("{0:#,###0.##}", dr["Active_Qty"].ConvertFloat_Null_To_Zero());
            LblM1_3.Text = String.Format("{0:#,###0.##}", dr["Value_Min"].ConvertFloat_Null_To_Zero());
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
                    Lbl_M1_Nm.Text = dt.Rows[0]["MachineName"].ConvertString_Null_To_Empty();
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
    }
}
