
namespace SmartPOP.View.Lookup
{
    partial class Frm_LookUpStockMove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LookUpStockMove));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Save = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.txt_length = new baseSMT.smtBodyTextBox();
            this.txt_Width = new baseSMT.smtBodyTextBox();
            this.txt_QtyNum = new baseSMT.smtBodyTextBox();
            this.txt_Qty = new baseSMT.smtBodyTextBox();
            this.txt_CutSize = new baseSMT.smtBodyTextBox();
            this.LupWareType = new baseSMT.smtBodyLookUp();
            this.txt_ProductCode = new baseSMT.smtBodyTextBox();
            this.txt_StockQty = new baseSMT.smtBodyTextBox();
            this.memo_Reason = new baseSMT.smtBodyMemoEdit();
            this.LupWareHouse = new baseSMT.smtBodyLookUp();
            this.txt_ProductName = new baseSMT.smtBodyTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Save);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(846, 45);
            this.smtCommonBar1.smtImageTitleAutoText = false;
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "재고이동";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 0;
            // 
            // btn_Save
            // 
            this.btn_Save.AllowFocus = false;
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Save.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseForeColor = true;
            this.btn_Save.AutoSize = true;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Save.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Save.ImageOptions.ImageIndex = 4;
            this.btn_Save.ImageOptions.ImageToTextIndent = 15;
            this.btn_Save.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Save.Location = new System.Drawing.Point(688, 0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Save.Size = new System.Drawing.Size(79, 45);
            this.btn_Save.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "저장";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.AllowFocus = false;
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Close.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseForeColor = true;
            this.btn_Close.AutoSize = true;
            this.btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Close.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Close.ImageOptions.ImageIndex = 5;
            this.btn_Close.ImageOptions.ImageToTextIndent = 15;
            this.btn_Close.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Close.Location = new System.Drawing.Point(767, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.Size = new System.Drawing.Size(79, 45);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_length
            // 
            this.txt_length.Location = new System.Drawing.Point(22, 132);
            this.txt_length.Name = "txt_length";
            this.txt_length.Size = new System.Drawing.Size(300, 32);
            this.txt_length.smtLabelTitleValue = "장";
            this.txt_length.smtLabelTitleVisible = true;
            this.txt_length.smtLabelTitleWidth = 100;
            this.txt_length.smtTextBoxDecimal = 0;
            this.txt_length.smtTextBoxMust = false;
            this.txt_length.smtTextBoxNumber = false;
            this.txt_length.smtTextBoxReadOnly = true;
            this.txt_length.smtTextBoxValue = "";
            this.txt_length.TabIndex = 100;
            // 
            // txt_Width
            // 
            this.txt_Width.Location = new System.Drawing.Point(23, 170);
            this.txt_Width.Name = "txt_Width";
            this.txt_Width.Size = new System.Drawing.Size(299, 32);
            this.txt_Width.smtLabelTitleValue = "폭";
            this.txt_Width.smtLabelTitleVisible = true;
            this.txt_Width.smtLabelTitleWidth = 100;
            this.txt_Width.smtTextBoxDecimal = 0;
            this.txt_Width.smtTextBoxMust = false;
            this.txt_Width.smtTextBoxNumber = false;
            this.txt_Width.smtTextBoxReadOnly = true;
            this.txt_Width.smtTextBoxValue = "";
            this.txt_Width.TabIndex = 101;
            // 
            // txt_QtyNum
            // 
            this.txt_QtyNum.Location = new System.Drawing.Point(23, 208);
            this.txt_QtyNum.Name = "txt_QtyNum";
            this.txt_QtyNum.Size = new System.Drawing.Size(299, 32);
            this.txt_QtyNum.smtLabelTitleValue = "절수";
            this.txt_QtyNum.smtLabelTitleVisible = true;
            this.txt_QtyNum.smtLabelTitleWidth = 100;
            this.txt_QtyNum.smtTextBoxDecimal = 0;
            this.txt_QtyNum.smtTextBoxMust = false;
            this.txt_QtyNum.smtTextBoxNumber = false;
            this.txt_QtyNum.smtTextBoxReadOnly = true;
            this.txt_QtyNum.smtTextBoxValue = "";
            this.txt_QtyNum.TabIndex = 102;
            // 
            // txt_Qty
            // 
            this.txt_Qty.Location = new System.Drawing.Point(23, 286);
            this.txt_Qty.Name = "txt_Qty";
            this.txt_Qty.Size = new System.Drawing.Size(299, 32);
            this.txt_Qty.smtLabelTitleValue = "현재고량";
            this.txt_Qty.smtLabelTitleVisible = true;
            this.txt_Qty.smtLabelTitleWidth = 100;
            this.txt_Qty.smtTextBoxDecimal = 0;
            this.txt_Qty.smtTextBoxMust = false;
            this.txt_Qty.smtTextBoxNumber = true;
            this.txt_Qty.smtTextBoxReadOnly = true;
            this.txt_Qty.smtTextBoxValue = "";
            this.txt_Qty.TabIndex = 104;
            // 
            // txt_CutSize
            // 
            this.txt_CutSize.Location = new System.Drawing.Point(23, 248);
            this.txt_CutSize.Name = "txt_CutSize";
            this.txt_CutSize.Size = new System.Drawing.Size(299, 32);
            this.txt_CutSize.smtLabelTitleValue = "재단규격";
            this.txt_CutSize.smtLabelTitleVisible = true;
            this.txt_CutSize.smtLabelTitleWidth = 100;
            this.txt_CutSize.smtTextBoxDecimal = 0;
            this.txt_CutSize.smtTextBoxMust = false;
            this.txt_CutSize.smtTextBoxNumber = false;
            this.txt_CutSize.smtTextBoxReadOnly = true;
            this.txt_CutSize.smtTextBoxValue = "";
            this.txt_CutSize.TabIndex = 103;
            // 
            // LupWareType
            // 
            this.LupWareType.Location = new System.Drawing.Point(21, 56);
            this.LupWareType.LookAndFeel.SkinName = "WXI";
            this.LupWareType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupWareType.Name = "LupWareType";
            this.LupWareType.Size = new System.Drawing.Size(302, 32);
            this.LupWareType.smtLabelTitleValue = "창고";
            this.LupWareType.smtLabelTitleVisible = true;
            this.LupWareType.smtLabelTitleWidth = 100;
            this.LupWareType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupWareType.smtLookUpDataDisplayMember = "코드명";
            this.LupWareType.smtLookUpDataSource = null;
            this.LupWareType.smtLookUpDataValueMember = "코드";
            this.LupWareType.smtLookUpMust = false;
            this.LupWareType.smtLookUpReadOnly = true;
            this.LupWareType.smtLookUpSelectdIndex = -1;
            this.LupWareType.smtLookUpText = "";
            this.LupWareType.smtLookUpValue = "";
            this.LupWareType.TabIndex = 105;
            // 
            // txt_ProductCode
            // 
            this.txt_ProductCode.Location = new System.Drawing.Point(21, 94);
            this.txt_ProductCode.Name = "txt_ProductCode";
            this.txt_ProductCode.Size = new System.Drawing.Size(302, 32);
            this.txt_ProductCode.smtLabelTitleValue = "품목코드";
            this.txt_ProductCode.smtLabelTitleVisible = true;
            this.txt_ProductCode.smtLabelTitleWidth = 100;
            this.txt_ProductCode.smtTextBoxDecimal = 0;
            this.txt_ProductCode.smtTextBoxMust = false;
            this.txt_ProductCode.smtTextBoxNumber = false;
            this.txt_ProductCode.smtTextBoxReadOnly = true;
            this.txt_ProductCode.smtTextBoxValue = "";
            this.txt_ProductCode.TabIndex = 107;
            // 
            // txt_StockQty
            // 
            this.txt_StockQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_StockQty.Location = new System.Drawing.Point(329, 286);
            this.txt_StockQty.Name = "txt_StockQty";
            this.txt_StockQty.Size = new System.Drawing.Size(302, 32);
            this.txt_StockQty.smtLabelTitleValue = "이동 재고량";
            this.txt_StockQty.smtLabelTitleVisible = true;
            this.txt_StockQty.smtLabelTitleWidth = 100;
            this.txt_StockQty.smtTextBoxDecimal = 0;
            this.txt_StockQty.smtTextBoxMust = true;
            this.txt_StockQty.smtTextBoxNumber = true;
            this.txt_StockQty.smtTextBoxReadOnly = false;
            this.txt_StockQty.smtTextBoxValue = "";
            this.txt_StockQty.TabIndex = 110;
            // 
            // memo_Reason
            // 
            this.memo_Reason.Location = new System.Drawing.Point(329, 132);
            this.memo_Reason.LookAndFeel.SkinName = "WXI";
            this.memo_Reason.LookAndFeel.UseDefaultLookAndFeel = false;
            this.memo_Reason.Name = "memo_Reason";
            this.memo_Reason.Size = new System.Drawing.Size(492, 108);
            this.memo_Reason.smtLabelTitleValue = "이동사유";
            this.memo_Reason.smtLabelTitleVisible = true;
            this.memo_Reason.smtLabelTitleWidth = 100;
            this.memo_Reason.smtMemoMust = false;
            this.memo_Reason.smtMemoReadOnly = false;
            this.memo_Reason.smtMemoValue = "";
            this.memo_Reason.TabIndex = 112;
            // 
            // LupWareHouse
            // 
            this.LupWareHouse.Location = new System.Drawing.Point(329, 248);
            this.LupWareHouse.LookAndFeel.SkinName = "WXI";
            this.LupWareHouse.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupWareHouse.Name = "LupWareHouse";
            this.LupWareHouse.Size = new System.Drawing.Size(302, 32);
            this.LupWareHouse.smtLabelTitleValue = "이동 창고";
            this.LupWareHouse.smtLabelTitleVisible = true;
            this.LupWareHouse.smtLabelTitleWidth = 100;
            this.LupWareHouse.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupWareHouse.smtLookUpDataDisplayMember = "코드명";
            this.LupWareHouse.smtLookUpDataSource = null;
            this.LupWareHouse.smtLookUpDataValueMember = "코드";
            this.LupWareHouse.smtLookUpMust = true;
            this.LupWareHouse.smtLookUpReadOnly = false;
            this.LupWareHouse.smtLookUpSelectdIndex = -1;
            this.LupWareHouse.smtLookUpText = "";
            this.LupWareHouse.smtLookUpValue = "";
            this.LupWareHouse.TabIndex = 113;
            // 
            // txt_ProductName
            // 
            this.txt_ProductName.Location = new System.Drawing.Point(329, 94);
            this.txt_ProductName.Name = "txt_ProductName";
            this.txt_ProductName.Size = new System.Drawing.Size(494, 32);
            this.txt_ProductName.smtLabelTitleValue = "품명";
            this.txt_ProductName.smtLabelTitleVisible = true;
            this.txt_ProductName.smtLabelTitleWidth = 100;
            this.txt_ProductName.smtTextBoxDecimal = 0;
            this.txt_ProductName.smtTextBoxMust = false;
            this.txt_ProductName.smtTextBoxNumber = false;
            this.txt_ProductName.smtTextBoxReadOnly = true;
            this.txt_ProductName.smtTextBoxValue = "";
            this.txt_ProductName.TabIndex = 114;
            // 
            // frmLookUpStockMove
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(846, 328);
            this.Controls.Add(this.txt_ProductName);
            this.Controls.Add(this.LupWareHouse);
            this.Controls.Add(this.memo_Reason);
            this.Controls.Add(this.txt_StockQty);
            this.Controls.Add(this.txt_ProductCode);
            this.Controls.Add(this.LupWareType);
            this.Controls.Add(this.txt_Qty);
            this.Controls.Add(this.txt_CutSize);
            this.Controls.Add(this.txt_QtyNum);
            this.Controls.Add(this.txt_Width);
            this.Controls.Add(this.txt_length);
            this.Controls.Add(this.smtCommonBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmLookUpStockMove";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmLookUpStockBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Save;
        private baseSMT.smtSearchButton btn_Close;
        private baseSMT.smtBodyTextBox txt_length;
        private baseSMT.smtBodyTextBox txt_Width;
        private baseSMT.smtBodyTextBox txt_QtyNum;
        private baseSMT.smtBodyTextBox txt_Qty;
        private baseSMT.smtBodyTextBox txt_CutSize;
        private baseSMT.smtBodyLookUp LupWareType;
        private baseSMT.smtBodyTextBox txt_ProductCode;
        private baseSMT.smtBodyTextBox txt_StockQty;
        private baseSMT.smtBodyMemoEdit memo_Reason;
        private baseSMT.smtBodyLookUp LupWareHouse;
        private baseSMT.smtBodyTextBox txt_ProductName;
    }
}