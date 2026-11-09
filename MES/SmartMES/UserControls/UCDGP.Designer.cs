
namespace SmartMES.UserControls
{
    partial class UCDGP
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
            this.TpMachine1 = new DevExpress.Utils.Layout.TablePanel();
            this.LblActiveQty = new DevExpress.XtraEditors.LabelControl();
            this.LblMax = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.LblMin = new DevExpress.XtraEditors.LabelControl();
            this.Lbl_DgpNm = new DevExpress.XtraEditors.LabelControl();
            this.Lbl_MaterialNM = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.TpMachine1)).BeginInit();
            this.TpMachine1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TpMachine1
            // 
            this.TpMachine1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.TpMachine1.Appearance.Options.UseBackColor = true;
            this.TpMachine1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TpMachine1.Controls.Add(this.Lbl_MaterialNM);
            this.TpMachine1.Controls.Add(this.LblActiveQty);
            this.TpMachine1.Controls.Add(this.LblMax);
            this.TpMachine1.Controls.Add(this.labelControl3);
            this.TpMachine1.Controls.Add(this.labelControl2);
            this.TpMachine1.Controls.Add(this.LblMin);
            this.TpMachine1.Controls.Add(this.Lbl_DgpNm);
            this.TpMachine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TpMachine1.Location = new System.Drawing.Point(0, 0);
            this.TpMachine1.Margin = new System.Windows.Forms.Padding(0);
            this.TpMachine1.Name = "TpMachine1";
            this.TpMachine1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 45F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F)});
            this.TpMachine1.Size = new System.Drawing.Size(402, 223);
            this.TpMachine1.TabIndex = 1;
            // 
            // LblActiveQty
            // 
            this.LblActiveQty.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.LblActiveQty.Appearance.Font = new System.Drawing.Font("맑은 고딕", 55F, System.Drawing.FontStyle.Bold);
            this.LblActiveQty.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.LblActiveQty.Appearance.Options.UseBackColor = true;
            this.LblActiveQty.Appearance.Options.UseFont = true;
            this.LblActiveQty.Appearance.Options.UseForeColor = true;
            this.LblActiveQty.Appearance.Options.UseTextOptions = true;
            this.LblActiveQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblActiveQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblActiveQty.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TpMachine1.SetColumn(this.LblActiveQty, 0);
            this.TpMachine1.SetColumnSpan(this.LblActiveQty, 4);
            this.LblActiveQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblActiveQty.Location = new System.Drawing.Point(0, 75);
            this.LblActiveQty.Margin = new System.Windows.Forms.Padding(0);
            this.LblActiveQty.Name = "LblActiveQty";
            this.TpMachine1.SetRow(this.LblActiveQty, 2);
            this.LblActiveQty.Size = new System.Drawing.Size(403, 108);
            this.LblActiveQty.TabIndex = 13;
            // 
            // LblMax
            // 
            this.LblMax.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.LblMax.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.LblMax.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblMax.Appearance.Options.UseBackColor = true;
            this.LblMax.Appearance.Options.UseFont = true;
            this.LblMax.Appearance.Options.UseForeColor = true;
            this.LblMax.Appearance.Options.UseTextOptions = true;
            this.LblMax.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblMax.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LblMax.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblMax.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TpMachine1.SetColumn(this.LblMax, 3);
            this.LblMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblMax.Location = new System.Drawing.Point(302, 183);
            this.LblMax.Margin = new System.Windows.Forms.Padding(0);
            this.LblMax.Name = "LblMax";
            this.TpMachine1.SetRow(this.LblMax, 3);
            this.LblMax.Size = new System.Drawing.Size(101, 40);
            this.LblMax.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TpMachine1.SetColumn(this.labelControl3, 2);
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl3.Location = new System.Drawing.Point(201, 183);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl3.Name = "labelControl3";
            this.TpMachine1.SetRow(this.labelControl3, 3);
            this.labelControl3.Size = new System.Drawing.Size(101, 40);
            this.labelControl3.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TpMachine1.SetColumn(this.labelControl2, 0);
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(0, 183);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl2.Name = "labelControl2";
            this.TpMachine1.SetRow(this.labelControl2, 3);
            this.labelControl2.Size = new System.Drawing.Size(101, 40);
            this.labelControl2.TabIndex = 10;
            // 
            // LblMin
            // 
            this.LblMin.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.LblMin.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.LblMin.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblMin.Appearance.Options.UseBackColor = true;
            this.LblMin.Appearance.Options.UseFont = true;
            this.LblMin.Appearance.Options.UseForeColor = true;
            this.LblMin.Appearance.Options.UseTextOptions = true;
            this.LblMin.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblMin.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LblMin.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblMin.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TpMachine1.SetColumn(this.LblMin, 1);
            this.LblMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblMin.Location = new System.Drawing.Point(101, 183);
            this.LblMin.Margin = new System.Windows.Forms.Padding(0);
            this.LblMin.Name = "LblMin";
            this.TpMachine1.SetRow(this.LblMin, 3);
            this.LblMin.Size = new System.Drawing.Size(101, 40);
            this.LblMin.TabIndex = 9;
            // 
            // Lbl_DgpNm
            // 
            this.Lbl_DgpNm.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.Lbl_DgpNm.Appearance.Font = new System.Drawing.Font("맑은 고딕", 23F);
            this.Lbl_DgpNm.Appearance.ForeColor = System.Drawing.Color.White;
            this.Lbl_DgpNm.Appearance.Options.UseBackColor = true;
            this.Lbl_DgpNm.Appearance.Options.UseFont = true;
            this.Lbl_DgpNm.Appearance.Options.UseForeColor = true;
            this.Lbl_DgpNm.Appearance.Options.UseTextOptions = true;
            this.Lbl_DgpNm.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Lbl_DgpNm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Lbl_DgpNm.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TpMachine1.SetColumn(this.Lbl_DgpNm, 0);
            this.TpMachine1.SetColumnSpan(this.Lbl_DgpNm, 4);
            this.Lbl_DgpNm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lbl_DgpNm.Location = new System.Drawing.Point(0, 0);
            this.Lbl_DgpNm.Margin = new System.Windows.Forms.Padding(0);
            this.Lbl_DgpNm.Name = "Lbl_DgpNm";
            this.TpMachine1.SetRow(this.Lbl_DgpNm, 0);
            this.Lbl_DgpNm.Size = new System.Drawing.Size(403, 45);
            this.Lbl_DgpNm.TabIndex = 0;
            // 
            // Lbl_MaterialNM
            // 
            this.Lbl_MaterialNM.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.Lbl_MaterialNM.Appearance.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.Lbl_MaterialNM.Appearance.ForeColor = System.Drawing.Color.White;
            this.Lbl_MaterialNM.Appearance.Options.UseBackColor = true;
            this.Lbl_MaterialNM.Appearance.Options.UseFont = true;
            this.Lbl_MaterialNM.Appearance.Options.UseForeColor = true;
            this.Lbl_MaterialNM.Appearance.Options.UseTextOptions = true;
            this.Lbl_MaterialNM.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Lbl_MaterialNM.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Lbl_MaterialNM.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Lbl_MaterialNM.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TpMachine1.SetColumn(this.Lbl_MaterialNM, 0);
            this.TpMachine1.SetColumnSpan(this.Lbl_MaterialNM, 4);
            this.Lbl_MaterialNM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lbl_MaterialNM.Location = new System.Drawing.Point(0, 45);
            this.Lbl_MaterialNM.Margin = new System.Windows.Forms.Padding(0);
            this.Lbl_MaterialNM.Name = "Lbl_MaterialNM";
            this.TpMachine1.SetRow(this.Lbl_MaterialNM, 1);
            this.Lbl_MaterialNM.Size = new System.Drawing.Size(403, 30);
            this.Lbl_MaterialNM.TabIndex = 14;
            // 
            // UCDGP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.TpMachine1);
            this.Name = "UCDGP";
            this.Size = new System.Drawing.Size(402, 223);
            ((System.ComponentModel.ISupportInitialize)(this.TpMachine1)).EndInit();
            this.TpMachine1.ResumeLayout(false);
            this.TpMachine1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel TpMachine1;
        private DevExpress.XtraEditors.LabelControl LblMax;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl LblMin;
        private DevExpress.XtraEditors.LabelControl LblActiveQty;
        private DevExpress.XtraEditors.LabelControl Lbl_DgpNm;
        private DevExpress.XtraEditors.LabelControl Lbl_MaterialNM;
    }
}
