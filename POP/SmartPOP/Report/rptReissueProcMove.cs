using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SmartPOP.Common;
using System.Data;
using SmartPOP.Models.PM;

namespace SmartPOP.Report
{
    public partial class rptReissueProcMove : DevExpress.XtraReports.UI.XtraReport
    {
        string SerialNo = "";
        string OrderNo = "";

        public rptReissueProcMove()
        {
            InitializeComponent();
        }

        public rptReissueProcMove(string _SerialNo,string _OrderNo)
        {
            InitializeComponent();

            this.SerialNo = _SerialNo;
            this.OrderNo = _OrderNo;

            Display_Data();
        }

        private void Display_Data()
        {
            PM_OrderResult sm = new PM_OrderResult();
            sm.SerialNo = SerialNo;
            sm.OrderNo = OrderNo;

            this.DataSource = sm.Select_RptSerial();
        }

    }
}
