using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES.View.Common
{
	public partial class Frm_RptDesingner : DevExpress.XtraEditors.XtraForm
	{
		public Frm_RptDesingner()
		{
			InitializeComponent();
		}

		public Frm_RptDesingner(XtraReport rpt)
		{
			InitializeComponent();

			this.reportDesigner1.OpenReport(rpt);

			//티피지 사용안할것같은 visible false 처리
			panelContainer2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
			panelContainer3.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
			panelContainer4.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
		}
	}
}