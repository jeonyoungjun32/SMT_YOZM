using System;
using System.Data;
using System.Drawing;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.IF;

namespace SmartMES.UserControls
{
    /*
     DGP GaugeType을 불러와 항목명으로 사용함
     */
    public partial class UCDGP : DevExpress.XtraEditors.XtraUserControl
    {
        //public int Cnt = 3; //항목 갯수
        //public int MaxCnt = 6; //항목 최대 갯수

        Font FontDGPName = null;
        Font FontQty = null;

        public UCDGP()
        {
            InitializeComponent();

            FontDGPName = Lbl_DgpNm.Appearance.Font;
            FontQty = LblActiveQty.Appearance.Font;

            Init();
        }

        private void Init()
        {
            //lstDgpNm = new List<LabelControl>() { LblC1_Nm, LblC2_Nm, LblC3_Nm, LblC4_Nm, LblC5_Nm, LblC6_Nm };
            //lstValue = new List<LabelControl>() { LblM1_1, LblM1_2, LblM1_3, LblM1_4, LblM1_5, LblM1_6 };

            ////
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


        public void DisplayDGP(string dgpCode)
        {
            //DGP 공정조건 모니터링

            float minValue = 0;
            float maxValue = 0;
            float curValue = 0;
            float count = 0;
            int cnt = 0;

            IF_DGPCode mTDGPCode = new IF_DGPCode();
            mTDGPCode.DgpCode = dgpCode;

            DataTable dt = mTDGPCode.Select_DGPCounter();
            if (dt == null) return;

            DataRow dr = null;

            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }

            if (dr == null) return;

            labelControl3.Text = "▲";
            labelControl2.Text = "▼";
            Lbl_DgpNm.Text = dr["DgpName"].ConvertString_Null_To_Empty();
            Lbl_MaterialNM.Text = dr["ItemName"].ConvertString_Null_To_Empty();

            //if (dr["Gauge_Type"].ToString() != "카운터")
            //{
            //    maxValue = dr["Value_Max"].ConvertFloat_Null_To_Zero();
            //    minValue = dr["Value_Min"].ConvertFloat_Null_To_Zero();
            //    curValue = dr["Active_Qty"].ConvertFloat_Null_To_Zero();
            //}
            //else
            //{
            //    TpMachine1.Rows[2].Visible = false; //상하한 숨김
            //    curValue = dr["Counter"].ConvertFloat_Null_To_Zero();
            //}

            TpMachine1.Rows[3].Visible = false;
            count = dr["Counter"].ConvertFloat_Null_To_Zero();

            LblMax.Text = String.Format("{0:#,###0.##}", maxValue);
            LblMin.Text = String.Format("{0:#,###0.##}", minValue);
            LblActiveQty.Text = String.Format("{0:#,##0}", count);

            //폰트 조정
            if (LblActiveQty.Text.Length >= 5)
            {
                LblActiveQty.Font = new Font(FontQty.FontFamily, FontQty.Size - 10, FontQty.Style);
            }
            else
            {
                LblActiveQty.Font = FontQty;
            }

            if (Lbl_DgpNm.Text.Length >= 9)
            {
                Lbl_DgpNm.Font = new Font(FontDGPName.FontFamily, FontDGPName.Size - 5, FontDGPName.Style);
            }
            else
            {
                Lbl_DgpNm.Font = FontDGPName;
            }

            //if (curValue != 0 && dr["Gauge_Type"].ToString() != "카운터")
            //{
            //    if (minValue > curValue || maxValue < curValue)
            //    {
            //        LblActiveQty.Appearance.ForeColor = Color.OrangeRed;
            //    }
            //    else
            //    {
            //        LblActiveQty.Appearance.ForeColor = Color.Aqua;
            //    }
            //}
        }
    }
}
