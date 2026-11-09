using DevExpress.XtraEditors;

namespace SmartMES.View.Ai
{
    partial class Frm_Ai_Template
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Ai_Template));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.BtnSearch = new baseSMT.smtSearchButton();
            this.SearchLupUseYn = new baseSMT.smtSearchLookUp();
            this.SearchTxtTitle = new baseSMT.smtSearchTextBox();
            this.SearchLupType = new baseSMT.smtSearchLookUp();
            this.cmdAdd = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chkUseYn = new baseSMT.smtBodyCheckBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.ButtonEdit();
            this.MemoContent = new baseSMT.smtBodyMemoEdit();
            this.TxtTitle = new baseSMT.smtBodyTextBox();
            this.LupType = new baseSMT.smtBodyLookUp();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_Know = new DevExpress.XtraGrid.GridControl();
            this.Gv_Know = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Know)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Know)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.BtnSearch);
            this.smtCommonBar1.Controls.Add(this.SearchLupUseYn);
            this.smtCommonBar1.Controls.Add(this.SearchTxtTitle);
            this.smtCommonBar1.Controls.Add(this.SearchLupType);
            this.smtCommonBar1.Controls.Add(this.cmdAdd);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1272, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "문서템플릿관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 284;
            // 
            // BtnSearch
            // 
            this.BtnSearch.AllowFocus = false;
            this.BtnSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Appearance.Options.UseBackColor = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.AutoSize = true;
            this.BtnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnSearch.ImageOptions.ImageIndex = 0;
            this.BtnSearch.ImageOptions.ImageToTextIndent = 15;
            this.BtnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.BtnSearch.Location = new System.Drawing.Point(776, 0);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnSearch.Size = new System.Drawing.Size(77, 40);
            this.BtnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.BtnSearch.TabIndex = 5;
            this.BtnSearch.Text = "조회";
            // 
            // SearchLupUseYn
            // 
            this.SearchLupUseYn.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchLupUseYn.Location = new System.Drawing.Point(582, 0);
            this.SearchLupUseYn.LookAndFeel.SkinName = "WXI";
            this.SearchLupUseYn.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SearchLupUseYn.Name = "SearchLupUseYn";
            this.SearchLupUseYn.Size = new System.Drawing.Size(194, 40);
            this.SearchLupUseYn.smtLabelForeColor = System.Drawing.Color.White;
            this.SearchLupUseYn.smtLabelTitleValue = "사용유무";
            this.SearchLupUseYn.smtLabelTitleVisible = true;
            this.SearchLupUseYn.smtLabelTitleWidth = 70;
            this.SearchLupUseYn.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.SearchLupUseYn.smtLookUpDataDisplayMember = "코드명";
            this.SearchLupUseYn.smtLookUpDataSource = null;
            this.SearchLupUseYn.smtLookUpDataValueMember = "코드";
            this.SearchLupUseYn.smtLookUpSelectdIndex = -1;
            this.SearchLupUseYn.smtLookUpText = "";
            this.SearchLupUseYn.smtLookUpValue = "";
            this.SearchLupUseYn.TabIndex = 45;
            // 
            // SearchTxtTitle
            // 
            this.SearchTxtTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchTxtTitle.Location = new System.Drawing.Point(341, 0);
            this.SearchTxtTitle.LookAndFeel.SkinName = "WXI";
            this.SearchTxtTitle.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SearchTxtTitle.Name = "SearchTxtTitle";
            this.SearchTxtTitle.Size = new System.Drawing.Size(241, 40);
            this.SearchTxtTitle.smtLabelTitleValue = "제목";
            this.SearchTxtTitle.smtLabelTitleVisible = true;
            this.SearchTxtTitle.smtLabelTitleWidth = 70;
            this.SearchTxtTitle.smtTextBoxDecimal = 0;
            this.SearchTxtTitle.smtTextBoxMust = false;
            this.SearchTxtTitle.smtTextBoxNumber = false;
            this.SearchTxtTitle.smtTextBoxReadOnly = false;
            this.SearchTxtTitle.smtTextBoxValue = "";
            this.SearchTxtTitle.TabIndex = 44;
            // 
            // SearchLupType
            // 
            this.SearchLupType.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchLupType.Location = new System.Drawing.Point(128, 0);
            this.SearchLupType.LookAndFeel.SkinName = "WXI";
            this.SearchLupType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SearchLupType.Name = "SearchLupType";
            this.SearchLupType.Size = new System.Drawing.Size(213, 40);
            this.SearchLupType.smtLabelForeColor = System.Drawing.Color.White;
            this.SearchLupType.smtLabelTitleValue = "분류";
            this.SearchLupType.smtLabelTitleVisible = true;
            this.SearchLupType.smtLabelTitleWidth = 70;
            this.SearchLupType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.SearchLupType.smtLookUpDataDisplayMember = "코드명";
            this.SearchLupType.smtLookUpDataSource = null;
            this.SearchLupType.smtLookUpDataValueMember = "코드";
            this.SearchLupType.smtLookUpSelectdIndex = -1;
            this.SearchLupType.smtLookUpText = "";
            this.SearchLupType.smtLookUpValue = "";
            this.SearchLupType.TabIndex = 43;
            // 
            // cmdAdd
            // 
            this.cmdAdd.AllowFocus = false;
            this.cmdAdd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdAdd.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdAdd.Appearance.Options.UseBackColor = true;
            this.cmdAdd.Appearance.Options.UseForeColor = true;
            this.cmdAdd.AutoSize = true;
            this.cmdAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdAdd.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdAdd.ImageOptions.ImageIndex = 1;
            this.cmdAdd.ImageOptions.ImageToTextIndent = 15;
            this.cmdAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdAdd.Location = new System.Drawing.Point(1118, 0);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdAdd.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdAdd.Size = new System.Drawing.Size(77, 40);
            this.cmdAdd.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdAdd.TabIndex = 42;
            this.cmdAdd.Text = "등록";
            // 
            // cmdClose
            // 
            this.cmdClose.AllowFocus = false;
            this.cmdClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdClose.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdClose.Appearance.Options.UseBackColor = true;
            this.cmdClose.Appearance.Options.UseForeColor = true;
            this.cmdClose.AutoSize = true;
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdClose.ImageOptions.ImageIndex = 5;
            this.cmdClose.ImageOptions.ImageToTextIndent = 15;
            this.cmdClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdClose.Location = new System.Drawing.Point(1195, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.groupControl2);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1272, 660);
            this.layoutControl1.TabIndex = 285;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chkUseYn);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtFileName);
            this.groupControl2.Controls.Add(this.MemoContent);
            this.groupControl2.Controls.Add(this.TxtTitle);
            this.groupControl2.Controls.Add(this.LupType);
            this.groupControl2.Location = new System.Drawing.Point(745, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(524, 654);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "상세정보";
            // 
            // chkUseYn
            // 
            this.chkUseYn.Location = new System.Drawing.Point(5, 599);
            this.chkUseYn.LookAndFeel.SkinName = "WXI";
            this.chkUseYn.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chkUseYn.Name = "chkUseYn";
            this.chkUseYn.Size = new System.Drawing.Size(125, 32);
            this.chkUseYn.smtCheckEditChecked = false;
            this.chkUseYn.smtCheckEditDisplayValueChecked = "Y";
            this.chkUseYn.smtCheckEditDisplayValueUnchecked = "N";
            this.chkUseYn.smtLabelTitleValue = "사용여부";
            this.chkUseYn.smtLabelTitleVisible = true;
            this.chkUseYn.smtLabelTitleWidth = 100;
            this.chkUseYn.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 115);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 17);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "파일경로";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(104, 108);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFileName.Properties.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(415, 32);
            this.txtFileName.TabIndex = 5;
            this.txtFileName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // MemoContent
            // 
            this.MemoContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoContent.Location = new System.Drawing.Point(5, 146);
            this.MemoContent.LookAndFeel.SkinName = "WXI";
            this.MemoContent.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MemoContent.Name = "MemoContent";
            this.MemoContent.Size = new System.Drawing.Size(514, 447);
            this.MemoContent.smtLabelTitleValue = "설명";
            this.MemoContent.smtLabelTitleVisible = true;
            this.MemoContent.smtLabelTitleWidth = 100;
            this.MemoContent.smtMemoMust = false;
            this.MemoContent.smtMemoReadOnly = false;
            this.MemoContent.smtMemoValue = "";
            this.MemoContent.TabIndex = 3;
            // 
            // TxtTitle
            // 
            this.TxtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTitle.Location = new System.Drawing.Point(5, 70);
            this.TxtTitle.LookAndFeel.SkinName = "WXI";
            this.TxtTitle.LookAndFeel.UseDefaultLookAndFeel = false;
            this.TxtTitle.Name = "TxtTitle";
            this.TxtTitle.Size = new System.Drawing.Size(514, 32);
            this.TxtTitle.smtLabelTitleValue = "템플릿명";
            this.TxtTitle.smtLabelTitleVisible = true;
            this.TxtTitle.smtLabelTitleWidth = 100;
            this.TxtTitle.smtTextBoxDecimal = 0;
            this.TxtTitle.smtTextBoxMust = false;
            this.TxtTitle.smtTextBoxNumber = false;
            this.TxtTitle.smtTextBoxReadOnly = false;
            this.TxtTitle.smtTextBoxValue = "";
            this.TxtTitle.TabIndex = 2;
            // 
            // LupType
            // 
            this.LupType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LupType.Location = new System.Drawing.Point(5, 32);
            this.LupType.LookAndFeel.SkinName = "WXI";
            this.LupType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupType.Name = "LupType";
            this.LupType.Size = new System.Drawing.Size(514, 32);
            this.LupType.smtLabelTitleValue = "문서분류";
            this.LupType.smtLabelTitleVisible = true;
            this.LupType.smtLabelTitleWidth = 100;
            this.LupType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupType.smtLookUpDataDisplayMember = "코드명";
            this.LupType.smtLookUpDataSource = null;
            this.LupType.smtLookUpDataValueMember = "코드";
            this.LupType.smtLookUpMust = false;
            this.LupType.smtLookUpReadOnly = false;
            this.LupType.smtLookUpSelectdIndex = -1;
            this.LupType.smtLookUpText = "";
            this.LupType.smtLookUpValue = "";
            this.LupType.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.Gc_Know);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(720, 654);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "템플릿 리스트";
            // 
            // Gc_Know
            // 
            this.Gc_Know.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_Know.Location = new System.Drawing.Point(2, 29);
            this.Gc_Know.MainView = this.Gv_Know;
            this.Gc_Know.Name = "Gc_Know";
            this.Gc_Know.Size = new System.Drawing.Size(716, 623);
            this.Gc_Know.TabIndex = 0;
            this.Gc_Know.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Know});
            // 
            // Gv_Know
            // 
            this.Gv_Know.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.Gv_Know.GridControl = this.Gc_Know;
            this.Gv_Know.Name = "Gv_Know";
            this.Gv_Know.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "No";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "템플릿명";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "문서분류";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "사용여부";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.splitterItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1272, 660);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(726, 660);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(742, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(530, 660);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(726, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(16, 660);
            // 
            // Frm_Ai_Template
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 700);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Frm_Ai_Template.IconOptions.SvgImage")));
            this.MinimumSize = new System.Drawing.Size(482, 734);
            this.Name = "Frm_Ai_Template";
            this.Text = "문서템플릿관리";
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Know)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Know)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdAdd;
        private baseSMT.smtSearchButton BtnSearch;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private baseSMT.smtSearchTextBox SearchTxtTitle;
        private baseSMT.smtSearchLookUp SearchLupType;
        private GroupControl groupControl2;
        private GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraGrid.GridControl Gc_Know;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Know;
        private baseSMT.smtBodyLookUp LupType;
        private baseSMT.smtBodyTextBox TxtTitle;
        private baseSMT.smtBodyMemoEdit MemoContent;
        private baseSMT.smtSearchLookUp SearchLupUseYn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private LabelControl labelControl1;
        private ButtonEdit txtFileName;
        private baseSMT.smtBodyCheckBox chkUseYn;
    }
}
