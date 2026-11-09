
namespace SmartMES.View.IF
{
    partial class Frm_IF_PLCManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_IF_PLCManager));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Add = new baseSMT.smtSearchButton();
            this.BtnSave = new baseSMT.smtSearchButton();
            this.btn_Del = new baseSMT.smtSearchButton();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.colCorpId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCTCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCTName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCT_IP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCT_Port = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCT_UserYn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcDGPControl = new DevExpress.XtraGrid.GridControl();
            this.gvDGPControl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDGPControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDGPControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Add);
            this.smtCommonBar1.Controls.Add(this.BtnSave);
            this.smtCommonBar1.Controls.Add(this.btn_Del);
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1195, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "frmPLCManager";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 2;
            // 
            // btn_Add
            // 
            this.btn_Add.AllowFocus = false;
            this.btn_Add.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Add.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Add.Appearance.Options.UseBackColor = true;
            this.btn_Add.Appearance.Options.UseForeColor = true;
            this.btn_Add.AutoSize = true;
            this.btn_Add.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Add.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Add.ImageOptions.ImageIndex = 1;
            this.btn_Add.ImageOptions.ImageToTextIndent = 15;
            this.btn_Add.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Add.Location = new System.Drawing.Point(863, 0);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Add.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Add.Size = new System.Drawing.Size(101, 40);
            this.btn_Add.smtButtonAutoText = false;
            this.btn_Add.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.btn_Add.TabIndex = 5;
            this.btn_Add.Text = "항목추가";
            this.btn_Add.Click += new System.EventHandler(this.Btn_RowEdit);
            // 
            // BtnSave
            // 
            this.BtnSave.AllowFocus = false;
            this.BtnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Appearance.Options.UseBackColor = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.AutoSize = true;
            this.BtnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnSave.ImageOptions.ImageIndex = 4;
            this.BtnSave.ImageOptions.ImageToTextIndent = 15;
            this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.BtnSave.Location = new System.Drawing.Point(964, 0);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnSave.Size = new System.Drawing.Size(77, 40);
            this.BtnSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "저장";
            this.BtnSave.Click += new System.EventHandler(this.Btn_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.AllowFocus = false;
            this.btn_Del.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Del.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Del.Appearance.Options.UseBackColor = true;
            this.btn_Del.Appearance.Options.UseForeColor = true;
            this.btn_Del.AutoSize = true;
            this.btn_Del.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Del.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Del.ImageOptions.ImageIndex = 3;
            this.btn_Del.ImageOptions.ImageToTextIndent = 15;
            this.btn_Del.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Del.Location = new System.Drawing.Point(1041, 0);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Del.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Del.Size = new System.Drawing.Size(77, 40);
            this.btn_Del.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.btn_Del.TabIndex = 6;
            this.btn_Del.Text = "삭제";
            this.btn_Del.Click += new System.EventHandler(this.Btn_RowEdit);
            // 
            // cmdDisplay
            // 
            this.cmdDisplay.AllowFocus = false;
            this.cmdDisplay.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdDisplay.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdDisplay.Appearance.Options.UseBackColor = true;
            this.cmdDisplay.Appearance.Options.UseForeColor = true;
            this.cmdDisplay.AutoSize = true;
            this.cmdDisplay.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdDisplay.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdDisplay.ImageOptions.ImageIndex = 0;
            this.cmdDisplay.ImageOptions.ImageToTextIndent = 15;
            this.cmdDisplay.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdDisplay.Location = new System.Drawing.Point(150, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 4;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Visible = false;
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
            this.cmdClose.Location = new System.Drawing.Point(1118, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // colCorpId1
            // 
            this.colCorpId1.Caption = "사업장번호";
            this.colCorpId1.FieldName = "CorpId";
            this.colCorpId1.Name = "colCorpId1";
            // 
            // colCTCode
            // 
            this.colCTCode.Caption = "컨트롤코드";
            this.colCTCode.FieldName = "CTCode";
            this.colCTCode.Name = "colCTCode";
            this.colCTCode.Visible = true;
            this.colCTCode.VisibleIndex = 0;
            this.colCTCode.Width = 98;
            // 
            // colCTName
            // 
            this.colCTName.Caption = "컨트롤명";
            this.colCTName.FieldName = "CTName";
            this.colCTName.Name = "colCTName";
            this.colCTName.Visible = true;
            this.colCTName.VisibleIndex = 1;
            this.colCTName.Width = 148;
            // 
            // colCT_IP
            // 
            this.colCT_IP.Caption = "IP";
            this.colCT_IP.FieldName = "CT_IP";
            this.colCT_IP.Name = "colCT_IP";
            this.colCT_IP.Visible = true;
            this.colCT_IP.VisibleIndex = 2;
            this.colCT_IP.Width = 144;
            // 
            // colCT_Port
            // 
            this.colCT_Port.Caption = "Port";
            this.colCT_Port.FieldName = "CT_Port";
            this.colCT_Port.Name = "colCT_Port";
            this.colCT_Port.Visible = true;
            this.colCT_Port.VisibleIndex = 3;
            // 
            // colCT_UserYn
            // 
            this.colCT_UserYn.Caption = "사용유무";
            this.colCT_UserYn.FieldName = "CT_UserYn";
            this.colCT_UserYn.Name = "colCT_UserYn";
            // 
            // colCreId
            // 
            this.colCreId.FieldName = "CreId";
            this.colCreId.Name = "colCreId";
            // 
            // colCreIP
            // 
            this.colCreIP.FieldName = "CreIP";
            this.colCreIP.Name = "colCreIP";
            // 
            // colCreDt
            // 
            this.colCreDt.FieldName = "CreDt";
            this.colCreDt.Name = "colCreDt";
            // 
            // colModId
            // 
            this.colModId.FieldName = "ModId";
            this.colModId.Name = "colModId";
            // 
            // colModIP
            // 
            this.colModIP.FieldName = "ModIP";
            this.colModIP.Name = "colModIP";
            // 
            // colModDt
            // 
            this.colModDt.FieldName = "ModDt";
            this.colModDt.Name = "colModDt";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1195, 543);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1195, 543);
            this.layoutControlGroup1.Text = "통신설정";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcDGPControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1163, 484);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // gcDGPControl
            // 
            this.gcDGPControl.Location = new System.Drawing.Point(19, 46);
            this.gcDGPControl.MainView = this.gvDGPControl;
            this.gcDGPControl.Name = "gcDGPControl";
            this.gcDGPControl.Size = new System.Drawing.Size(1157, 478);
            this.gcDGPControl.TabIndex = 4;
            this.gcDGPControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDGPControl});
            // 
            // gvDGPControl
            // 
            this.gvDGPControl.GridControl = this.gcDGPControl;
            this.gvDGPControl.Name = "gvDGPControl";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcDGPControl);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1243, 348, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1195, 543);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // frmPLCManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 583);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "frmPLCManager";
            this.Text = "frmPLCManager";
            this.Load += new System.EventHandler(this.frmPLCSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDGPControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDGPControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Add;
        private baseSMT.smtSearchButton BtnSave;
        private baseSMT.smtSearchButton btn_Del;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId1;
        private DevExpress.XtraGrid.Columns.GridColumn colCTCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCTName;
        private DevExpress.XtraGrid.Columns.GridColumn colCT_IP;
        private DevExpress.XtraGrid.Columns.GridColumn colCT_Port;
        private DevExpress.XtraGrid.Columns.GridColumn colCT_UserYn;
        private DevExpress.XtraGrid.Columns.GridColumn colCreId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreIP;
        private DevExpress.XtraGrid.Columns.GridColumn colCreDt;
        private DevExpress.XtraGrid.Columns.GridColumn colModId;
        private DevExpress.XtraGrid.Columns.GridColumn colModIP;
        private DevExpress.XtraGrid.Columns.GridColumn colModDt;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcDGPControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDGPControl;
    }
}