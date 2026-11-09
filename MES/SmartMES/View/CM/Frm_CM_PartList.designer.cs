
namespace SmartMES.View.CM
{
    partial class Frm_CM_PartList 
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_PartList));
			this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.PnlMainTitle = new DevExpress.XtraEditors.PanelControl();
			this.Btn_Row_Ins = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Row_Del = new DevExpress.XtraEditors.SimpleButton();
			this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
			this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
			this.groupControlMain = new DevExpress.XtraEditors.GroupControl();
			this.Gc_BaseItem = new DevExpress.XtraGrid.GridControl();
			this.Gv_BaseItem = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.radioGroup_BaseItem = new DevExpress.XtraEditors.RadioGroup();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.Gc_Cust = new DevExpress.XtraGrid.GridControl();
			this.Gv_Cust = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.radioGroup_Cust = new DevExpress.XtraEditors.RadioGroup();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.Gc_PartList = new DevExpress.XtraGrid.GridControl();
			this.Gv_PartList = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
			this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			((System.ComponentModel.ISupportInitialize)(this.PnlMainTitle)).BeginInit();
			this.PnlMainTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControlMain)).BeginInit();
			this.groupControlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_BaseItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_BaseItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup_BaseItem.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Cust)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Cust)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup_Cust.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_PartList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_PartList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
			this.tablePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
			this.splitContainerControl1.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
			this.splitContainerControl1.Panel2.SuspendLayout();
			this.splitContainerControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnSearch
			// 
			this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearch.ImageOptions.Image")));
			this.BtnSearch.Location = new System.Drawing.Point(877, 8);
			this.BtnSearch.Name = "BtnSearch";
			this.BtnSearch.Size = new System.Drawing.Size(70, 23);
			this.BtnSearch.TabIndex = 265;
			this.BtnSearch.Text = "조회";
			this.BtnSearch.Visible = false;
			this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
			// 
			// PnlMainTitle
			// 
			this.PnlMainTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnlMainTitle.Controls.Add(this.Btn_Row_Ins);
			this.PnlMainTitle.Controls.Add(this.Btn_Row_Del);
			this.PnlMainTitle.Controls.Add(this.BtnSave);
			this.PnlMainTitle.Controls.Add(this.LblMainTitle);
			this.PnlMainTitle.Controls.Add(this.BtnSearch);
			this.PnlMainTitle.Location = new System.Drawing.Point(12, 12);
			this.PnlMainTitle.Name = "PnlMainTitle";
			this.PnlMainTitle.Padding = new System.Windows.Forms.Padding(3);
			this.PnlMainTitle.Size = new System.Drawing.Size(1181, 39);
			this.PnlMainTitle.TabIndex = 268;
			// 
			// Btn_Row_Ins
			// 
			this.Btn_Row_Ins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Row_Ins.Location = new System.Drawing.Point(1027, 8);
			this.Btn_Row_Ins.Name = "Btn_Row_Ins";
			this.Btn_Row_Ins.Size = new System.Drawing.Size(70, 23);
			this.Btn_Row_Ins.TabIndex = 279;
			this.Btn_Row_Ins.Text = " 신규 ";
			this.Btn_Row_Ins.Click += new System.EventHandler(this.Btn_RowEdit);
			// 
			// Btn_Row_Del
			// 
			this.Btn_Row_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Row_Del.Location = new System.Drawing.Point(1103, 8);
			this.Btn_Row_Del.Name = "Btn_Row_Del";
			this.Btn_Row_Del.Size = new System.Drawing.Size(70, 23);
			this.Btn_Row_Del.TabIndex = 267;
			this.Btn_Row_Del.Text = "삭제";
			this.Btn_Row_Del.Click += new System.EventHandler(this.Btn_RowEdit);
			// 
			// BtnSave
			// 
			this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.ImageOptions.Image")));
			this.BtnSave.Location = new System.Drawing.Point(951, 8);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(70, 23);
			this.BtnSave.TabIndex = 266;
			this.BtnSave.Text = "저장";
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// LblMainTitle
			// 
			this.LblMainTitle.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
			this.LblMainTitle.Appearance.Options.UseFont = true;
			this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblMainTitle.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.LblMainTitle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("LblMainTitle.ImageOptions.Image")));
			this.LblMainTitle.Location = new System.Drawing.Point(8, 8);
			this.LblMainTitle.Name = "LblMainTitle";
			this.LblMainTitle.Size = new System.Drawing.Size(300, 23);
			this.LblMainTitle.TabIndex = 59;
			this.LblMainTitle.Text = "파트리스트";
			// 
			// groupControlMain
			// 
			this.groupControlMain.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.groupControlMain.AppearanceCaption.Options.UseFont = true;
			this.groupControlMain.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControlMain.CaptionImageOptions.Image")));
			this.tablePanel1.SetColumn(this.groupControlMain, 0);
			this.groupControlMain.Controls.Add(this.Gc_BaseItem);
			this.groupControlMain.Controls.Add(this.radioGroup_BaseItem);
			this.groupControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControlMain.Location = new System.Drawing.Point(0, 0);
			this.groupControlMain.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
			this.groupControlMain.Name = "groupControlMain";
			this.tablePanel1.SetRow(this.groupControlMain, 0);
			this.groupControlMain.Size = new System.Drawing.Size(597, 246);
			this.groupControlMain.TabIndex = 269;
			this.groupControlMain.Text = "기준품목";
			// 
			// Gc_BaseItem
			// 
			this.Gc_BaseItem.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_BaseItem.Location = new System.Drawing.Point(2, 50);
			this.Gc_BaseItem.MainView = this.Gv_BaseItem;
			this.Gc_BaseItem.Name = "Gc_BaseItem";
			this.Gc_BaseItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
			this.Gc_BaseItem.Size = new System.Drawing.Size(593, 194);
			this.Gc_BaseItem.TabIndex = 280;
			this.Gc_BaseItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_BaseItem});
			// 
			// Gv_BaseItem
			// 
			this.Gv_BaseItem.GridControl = this.Gc_BaseItem;
			this.Gv_BaseItem.Name = "Gv_BaseItem";
			this.Gv_BaseItem.OptionsView.ShowGroupPanel = false;
			// 
			// repositoryItemComboBox1
			// 
			this.repositoryItemComboBox1.AutoHeight = false;
			this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			// 
			// radioGroup_BaseItem
			// 
			this.radioGroup_BaseItem.Dock = System.Windows.Forms.DockStyle.Top;
			this.radioGroup_BaseItem.Location = new System.Drawing.Point(2, 23);
			this.radioGroup_BaseItem.Name = "radioGroup_BaseItem";
			this.radioGroup_BaseItem.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.radioGroup_BaseItem.Size = new System.Drawing.Size(593, 27);
			this.radioGroup_BaseItem.TabIndex = 279;
			this.radioGroup_BaseItem.Visible = false;
			// 
			// groupControl1
			// 
			this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.groupControl1.AppearanceCaption.Options.UseFont = true;
			this.groupControl1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl1.CaptionImageOptions.Image")));
			this.tablePanel1.SetColumn(this.groupControl1, 0);
			this.groupControl1.Controls.Add(this.Gc_Cust);
			this.groupControl1.Controls.Add(this.radioGroup_Cust);
			this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl1.Location = new System.Drawing.Point(0, 252);
			this.groupControl1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
			this.groupControl1.Name = "groupControl1";
			this.tablePanel1.SetRow(this.groupControl1, 1);
			this.groupControl1.Size = new System.Drawing.Size(597, 245);
			this.groupControl1.TabIndex = 270;
			this.groupControl1.Text = "거래처";
			// 
			// Gc_Cust
			// 
			this.Gc_Cust.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_Cust.Location = new System.Drawing.Point(2, 50);
			this.Gc_Cust.MainView = this.Gv_Cust;
			this.Gc_Cust.Name = "Gc_Cust";
			this.Gc_Cust.Size = new System.Drawing.Size(593, 193);
			this.Gc_Cust.TabIndex = 281;
			this.Gc_Cust.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Cust});
			// 
			// Gv_Cust
			// 
			this.Gv_Cust.GridControl = this.Gc_Cust;
			this.Gv_Cust.Name = "Gv_Cust";
			this.Gv_Cust.OptionsFind.AllowFindPanel = false;
			this.Gv_Cust.OptionsView.ShowGroupPanel = false;
			// 
			// radioGroup_Cust
			// 
			this.radioGroup_Cust.Dock = System.Windows.Forms.DockStyle.Top;
			this.radioGroup_Cust.Location = new System.Drawing.Point(2, 23);
			this.radioGroup_Cust.Name = "radioGroup_Cust";
			this.radioGroup_Cust.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.radioGroup_Cust.Size = new System.Drawing.Size(593, 27);
			this.radioGroup_Cust.TabIndex = 280;
			// 
			// groupControl2
			// 
			this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.groupControl2.AppearanceCaption.Options.UseFont = true;
			this.groupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl2.CaptionImageOptions.Image")));
			this.groupControl2.Controls.Add(this.Gc_PartList);
			this.groupControl2.Controls.Add(this.panelControl3);
			this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl2.Location = new System.Drawing.Point(0, 0);
			this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(571, 497);
			this.groupControl2.TabIndex = 271;
			this.groupControl2.Text = "포장BOM";
			// 
			// Gc_PartList
			// 
			this.Gc_PartList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_PartList.Location = new System.Drawing.Point(2, 56);
			this.Gc_PartList.MainView = this.Gv_PartList;
			this.Gc_PartList.Name = "Gc_PartList";
			this.Gc_PartList.Size = new System.Drawing.Size(567, 439);
			this.Gc_PartList.TabIndex = 282;
			this.Gc_PartList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_PartList});
			// 
			// Gv_PartList
			// 
			this.Gv_PartList.GridControl = this.Gc_PartList;
			this.Gv_PartList.Name = "Gv_PartList";
			this.Gv_PartList.OptionsFind.AllowFindPanel = false;
			this.Gv_PartList.OptionsView.ShowGroupPanel = false;
			this.Gv_PartList.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.Gv_PartList_ValidatingEditor);
			// 
			// panelControl3
			// 
			this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl3.Location = new System.Drawing.Point(2, 23);
			this.panelControl3.Name = "panelControl3";
			this.panelControl3.Padding = new System.Windows.Forms.Padding(3);
			this.panelControl3.Size = new System.Drawing.Size(567, 33);
			this.panelControl3.TabIndex = 282;
			this.panelControl3.Visible = false;
			// 
			// tablePanel1
			// 
			this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
			this.tablePanel1.Controls.Add(this.groupControl1);
			this.tablePanel1.Controls.Add(this.groupControlMain);
			this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablePanel1.Location = new System.Drawing.Point(0, 0);
			this.tablePanel1.Name = "tablePanel1";
			this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
			this.tablePanel1.Size = new System.Drawing.Size(600, 497);
			this.tablePanel1.TabIndex = 280;
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainerControl1.Location = new System.Drawing.Point(12, 57);
			this.splitContainerControl1.Name = "splitContainerControl1";
			// 
			// splitContainerControl1.Panel1
			// 
			this.splitContainerControl1.Panel1.Controls.Add(this.tablePanel1);
			this.splitContainerControl1.Panel1.Text = "Panel1";
			// 
			// splitContainerControl1.Panel2
			// 
			this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
			this.splitContainerControl1.Panel2.Text = "Panel2";
			this.splitContainerControl1.Size = new System.Drawing.Size(1181, 497);
			this.splitContainerControl1.SplitterPosition = 600;
			this.splitContainerControl1.TabIndex = 281;
			// 
			// Frm_BS_PartList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1205, 566);
			this.Controls.Add(this.splitContainerControl1);
			this.Controls.Add(this.PnlMainTitle);
			this.Name = "Frm_CM_PartList";
			this.Text = "파트리스트";
			this.Load += new System.EventHandler(this.Frm_CM_PartList_Load);
			((System.ComponentModel.ISupportInitialize)(this.PnlMainTitle)).EndInit();
			this.PnlMainTitle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControlMain)).EndInit();
			this.groupControlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_BaseItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_BaseItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup_BaseItem.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Cust)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Cust)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup_Cust.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_PartList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_PartList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
			this.tablePanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
			this.splitContainerControl1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
			this.splitContainerControl1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private DevExpress.XtraEditors.PanelControl PnlMainTitle;
        private DevExpress.XtraEditors.LabelControl LblMainTitle;
        private DevExpress.XtraEditors.GroupControl groupControlMain;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.RadioGroup radioGroup_BaseItem;
        private DevExpress.XtraEditors.RadioGroup radioGroup_Cust;
        private DevExpress.XtraGrid.GridControl Gc_BaseItem;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_BaseItem;
        private DevExpress.XtraGrid.GridControl Gc_Cust;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Cust;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton Btn_Row_Del;
		private DevExpress.XtraGrid.GridControl Gc_PartList;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_PartList;
		private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton Btn_Row_Ins;
    }
}