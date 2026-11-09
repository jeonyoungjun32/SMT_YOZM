using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartPOP.Common;

namespace SmartPOP.Views.Work
{
	public partial class ucBtnMachineO : DevExpress.XtraEditors.XtraUserControl
	{
		string Type = "";
		string ItemCode = "";

		// ⭐ 클릭 이벤트 (Form에서 받음)
		public event Action<string, string> ItemClicked;

		public ucBtnMachineO()
		{
			InitializeComponent();
		}

		public ucBtnMachineO(string type, string itemCode, string text)
		{
			InitializeComponent();

			this.Type = type;
			this.ItemCode = itemCode;
			simpleButton1.Text = text;
			simpleButton1.Tag = itemCode;

			simpleButton1.Font = new Font(new FontFamily("Pretendard SemiBold"), 18);

			if (type == "분류")
			{
				simpleButton1.BackgroundImage = SmartPOP.Properties.Resources.btn_green02;
				simpleButton1.Font = new Font(new FontFamily("Pretendard SemiBold"), 28);
				simpleButton1.ForeColor = Color.White;
			}
		}

		private void ucBtnMachineO_Load(object sender, EventArgs e)
		{

		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			// ⭐ 이벤트만 발생
			ItemClicked?.Invoke(Type, ItemCode);
		}
	}
}
