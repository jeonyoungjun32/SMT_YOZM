
namespace SmartPOP.Views.Work
{
	partial class ucBtnMachineO
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBtnMachineO));
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.SuspendLayout();
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.BackColor = System.Drawing.Color.LightBlue;
			this.simpleButton1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 24F, System.Drawing.FontStyle.Bold);
			this.simpleButton1.Appearance.Options.UseBackColor = true;
			this.simpleButton1.Appearance.Options.UseFont = true;
			this.simpleButton1.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.simpleButton1.AppearanceDisabled.Options.UseForeColor = true;
			this.simpleButton1.AppearanceHovered.ForeColor = System.Drawing.Color.Black;
			this.simpleButton1.AppearanceHovered.Options.UseForeColor = true;
			this.simpleButton1.AppearancePressed.ForeColor = System.Drawing.Color.Black;
			this.simpleButton1.AppearancePressed.Options.UseForeColor = true;
			this.simpleButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("simpleButton1.BackgroundImage")));
			this.simpleButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.simpleButton1.Location = new System.Drawing.Point(0, 0);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Padding = new System.Windows.Forms.Padding(3);
			this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.simpleButton1.Size = new System.Drawing.Size(185, 102);
			this.simpleButton1.TabIndex = 0;
			this.simpleButton1.Text = "버튼";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// ucBtnMachineO
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.simpleButton1);
			this.Name = "ucBtnMachineO";
			this.Size = new System.Drawing.Size(185, 102);
			this.Load += new System.EventHandler(this.ucBtnMachineO_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButton1;
	}
}
