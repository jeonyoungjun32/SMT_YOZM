using DevExpress.XtraReports.UI;
using SMT_MES.Model;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using SMT_MES.Controls;
using SmartPOP.Common;

namespace SMT_MES.View.Product.Rpt
{
    public partial class RptWorkOrder_Injection : DevExpress.XtraReports.UI.XtraReport
    {
        int MaxRowCnt = 15;

        public RptWorkOrder_Injection(DataTable dt)
        {
            InitializeComponent();

            CellBaseWeight.TextFormatString = "{0:#,#}";
            CellCT.TextFormatString = "{0:#,#}";

            //23.08.02 임시 데이터 바인딩 처리 - 수정 필요함
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    //LblLine.Text = dt.Rows[0]["Line_Nm"].ConvertString_Null_To_Empty();
                    LblProcess.Text = dt.Rows[0]["ProcessName"].ConvertString_Null_To_Empty();
                    //LblDate.Text = dt.Rows[0]["ProcCmd_Date"].ConvertString_Null_To_Empty();
                    LblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }

            //MaxRowCnt > dt.Row.Count일 경우 빈칸 채우기
            int remainder = dt.Rows.Count % MaxRowCnt;

            if (remainder != 0)
            {
                for (int i = 0; i < MaxRowCnt - remainder; i++)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
            }

            this.DataSource = dt;
        }
    }
}
