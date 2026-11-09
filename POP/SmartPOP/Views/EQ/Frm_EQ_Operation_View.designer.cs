
namespace SmartPOP.Views.EQ
{
    partial class Frm_EQ_Operation_View
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_Operation_View));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnIns = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.GcOperation = new DevExpress.XtraGrid.GridControl();
            this.GvOperation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.Lup_Machine = new baseSMT.smtSearchLookUp();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tablePanel2);
            this.layoutControl1.Controls.Add(this.GcOperation);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1350, 624);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tablePanel2
            // 
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F)});
            this.tablePanel2.Controls.Add(this.BtnSave);
            this.tablePanel2.Controls.Add(this.BtnIns);
            this.tablePanel2.Controls.Add(this.BtnDel);
            this.tablePanel2.Controls.Add(this.cmdSearch);
            this.tablePanel2.Location = new System.Drawing.Point(1241, 34);
            this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel2.MaximumSize = new System.Drawing.Size(130, 0);
            this.tablePanel2.MinimumSize = new System.Drawing.Size(100, 0);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
            this.tablePanel2.Size = new System.Drawing.Size(102, 583);
            this.tablePanel2.TabIndex = 185;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.NavajoWhite;
            this.tablePanel2.SetColumn(this.BtnSave, 0);
            this.BtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.BtnSave.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Location = new System.Drawing.Point(3, 76);
            this.BtnSave.Name = "BtnSave";
            this.tablePanel2.SetRow(this.BtnSave, 1);
            this.BtnSave.Size = new System.Drawing.Size(96, 67);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "저장";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnIns
            // 
            this.BtnIns.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tablePanel2.SetColumn(this.BtnIns, 0);
            this.BtnIns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnIns.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnIns.FlatAppearance.BorderSize = 0;
            this.BtnIns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnIns.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.BtnIns.ForeColor = System.Drawing.Color.Black;
            this.BtnIns.Location = new System.Drawing.Point(3, 149);
            this.BtnIns.Name = "BtnIns";
            this.tablePanel2.SetRow(this.BtnIns, 2);
            this.BtnIns.Size = new System.Drawing.Size(96, 67);
            this.BtnIns.TabIndex = 3;
            this.BtnIns.Text = "추가";
            this.BtnIns.UseVisualStyleBackColor = false;
            this.BtnIns.Click += new System.EventHandler(this.BtnClick);
            // 
            // BtnDel
            // 
            this.BtnDel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tablePanel2.SetColumn(this.BtnDel, 0);
            this.BtnDel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnDel.FlatAppearance.BorderSize = 0;
            this.BtnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDel.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.BtnDel.ForeColor = System.Drawing.Color.Black;
            this.BtnDel.Location = new System.Drawing.Point(3, 222);
            this.BtnDel.Name = "BtnDel";
            this.tablePanel2.SetRow(this.BtnDel, 3);
            this.BtnDel.Size = new System.Drawing.Size(96, 67);
            this.BtnDel.TabIndex = 2;
            this.BtnDel.Text = "삭제";
            this.BtnDel.UseVisualStyleBackColor = false;
            this.BtnDel.Click += new System.EventHandler(this.BtnClick);
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackColor = System.Drawing.Color.NavajoWhite;
            this.tablePanel2.SetColumn(this.cmdSearch, 0);
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.cmdSearch.FlatAppearance.BorderSize = 0;
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.cmdSearch.ForeColor = System.Drawing.Color.Black;
            this.cmdSearch.Location = new System.Drawing.Point(3, 3);
            this.cmdSearch.Name = "cmdSearch";
            this.tablePanel2.SetRow(this.cmdSearch, 0);
            this.cmdSearch.Size = new System.Drawing.Size(96, 67);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "조회";
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // GcOperation
            // 
            this.GcOperation.Location = new System.Drawing.Point(7, 34);
            this.GcOperation.MainView = this.GvOperation;
            this.GcOperation.Name = "GcOperation";
            this.GcOperation.Size = new System.Drawing.Size(1228, 583);
            this.GcOperation.TabIndex = 6;
            this.GcOperation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvOperation});
            // 
            // GvOperation
            // 
            this.GvOperation.Appearance.HeaderPanel.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
            this.GvOperation.Appearance.HeaderPanel.Options.UseFont = true;
            this.GvOperation.Appearance.Row.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
            this.GvOperation.Appearance.Row.Options.UseFont = true;
            this.GvOperation.GridControl = this.GcOperation;
            this.GvOperation.Name = "GvOperation";
            this.GvOperation.OptionsView.ShowGroupPanel = false;
            this.GvOperation.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GvOperation_CellValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1350, 624);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1350, 624);
            this.layoutControlGroup1.Text = "비가동 이력";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GcOperation;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1234, 589);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tablePanel2;
            this.layoutControlItem3.Location = new System.Drawing.Point(1234, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(108, 589);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.Lup_Machine);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.LookAndFeel.SkinName = "WXI";
            this.smtCommonBar1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1350, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "비가동등록";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 1;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(114, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Name = "Dt_Date";
            this.Dt_Date.Size = new System.Drawing.Size(345, 45);
            this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Dt_Date.smtComboTitleItems = new string[0];
            this.Dt_Date.smtComboTitleValue = "";
            this.Dt_Date.smtComboTitleVisible = false;
            this.Dt_Date.smtComboTitleWidth = 100;
            this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 4, 1, 14, 6, 39, 455);
            this.Dt_Date.smtEndDateTimeVisible = true;
            this.Dt_Date.smtLabelTitleValue = "조회기간";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 4, 1, 14, 6, 39, 455);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 1;
            // 
            // Lup_Machine
            // 
            this.Lup_Machine.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Machine.Location = new System.Drawing.Point(459, 0);
            this.Lup_Machine.LookAndFeel.SkinName = "WXI";
            this.Lup_Machine.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Machine.Name = "Lup_Machine";
            this.Lup_Machine.Size = new System.Drawing.Size(288, 45);
            this.Lup_Machine.smtLabelTitleValue = "설비";
            this.Lup_Machine.smtLabelTitleVisible = true;
            this.Lup_Machine.smtLabelTitleWidth = 100;
            this.Lup_Machine.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Machine.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Machine.smtLookUpDataSource = null;
            this.Lup_Machine.smtLookUpDataValueMember = "코드";
            this.Lup_Machine.smtLookUpSelectdIndex = -1;
            this.Lup_Machine.smtLookUpText = "";
            this.Lup_Machine.smtLookUpValue = "";
            this.Lup_Machine.TabIndex = 2;
            // 
            // smtSearchButton1
            // 
            this.smtSearchButton1.AllowFocus = false;
            this.smtSearchButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton1.Appearance.Options.UseBackColor = true;
            this.smtSearchButton1.Appearance.Options.UseForeColor = true;
            this.smtSearchButton1.AutoSize = true;
            this.smtSearchButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton1.ImageOptions.ImageIndex = 5;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(1271, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.Size = new System.Drawing.Size(79, 45);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton1.TabIndex = 3;
            this.smtSearchButton1.Text = "닫기";
            this.smtSearchButton1.Click += new System.EventHandler(this.smtSearchButton1_Click);
            // 
            // frmMachineDowntime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 669);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "frmMachineDowntime";
            this.Text = "비가동등록";
            this.Load += new System.EventHandler(this.frmSelectPM_ProductPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl GcOperation;
        private DevExpress.XtraGrid.Views.Grid.GridView GvOperation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private System.Windows.Forms.Button BtnIns;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button cmdSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.Button BtnSave;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchLookUp Lup_Machine;
        private baseSMT.smtSearchDatetime Dt_Date;
    }
}