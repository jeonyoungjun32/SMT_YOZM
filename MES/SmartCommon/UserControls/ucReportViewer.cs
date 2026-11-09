using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraReports.UI;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCommon.UserControls
{
	public partial class ucReportViewer : DevExpress.XtraEditors.XtraUserControl
    {
        clsCode code = new clsCode();
        XtraReport report = null;

        int paramWidth = 0;

        public ucReportViewer()
        {
            InitializeComponent();
        }

        public void ShowReport(string _reportName, Dictionary<string, string> _dictParam, int _paramWidth = 0)
        {
            paramWidth = _paramWidth;

            report = code.GetSelectedReport(_reportName);

            foreach (DevExpress.XtraReports.Parameters.Parameter param in report.Parameters)
            {
                if (_dictParam.ContainsKey(param.Name))
                {
                    if (param.Type.Name == "String")
                    {
                        param.Value = _dictParam[param.Name];
                    }
                    else if (param.Type.Name == "DateTime")
                    {
                        param.Value = Convert.ToDateTime(_dictParam[param.Name]);
                    }
                }
            }

            report.CreateDocument();
            reportViewer.DocumentSource = report;

            if (paramWidth > 0)
            {
                DevExpress.XtraBars.Docking.DockPanel parametersPnl = reportViewer.DockManager.Panels.FirstOrDefault(x => x.Text == DevExpress.XtraPrinting.Localization.PreviewLocalizer.GetString(PreviewStringId.TB_TTip_Parameters));
                parametersPnl.Width = paramWidth;
            }
        }

        public void ShowReport(int _reportId, Dictionary<string, string> _dictParam, int _paramWidth = 0)
        {
            paramWidth = _paramWidth;

            report = code.GetSelectedReport(_reportId);

            foreach (DevExpress.XtraReports.Parameters.Parameter param in report.Parameters)
            {
                if (_dictParam.ContainsKey(param.Name))
                {
                    param.Value = _dictParam[param.Name];
                }
            }
            report.CreateDocument();
            reportViewer.DocumentSource = report;
        }

        public void ShowBlank()
        {
            reportViewer.DocumentSource = null;
           
        }

        private void ucReportViewer_SizeChanged(object sender, EventArgs e)
        {
            if (paramWidth > 0)
            {
                DevExpress.XtraBars.Docking.DockPanel parametersPnl = reportViewer.DockManager.Panels.FirstOrDefault(x => x.Text == DevExpress.XtraPrinting.Localization.PreviewLocalizer.GetString(PreviewStringId.TB_TTip_Parameters));
                parametersPnl.Width = paramWidth;
            }
        }
    }
}
