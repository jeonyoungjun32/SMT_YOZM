namespace SmartMES.View.Chatbot
{
    partial class ChatMain_V2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // ====== 추가된 컨트롤 필드 ======
        private DevExpress.XtraEditors.ListBoxControl listBoxControlChat;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.MemoEdit memoEditInput;
        private DevExpress.XtraEditors.SimpleButton btnSend;

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
            this.listBoxControlChat = new DevExpress.XtraEditors.ListBoxControl();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.memoEditInput = new DevExpress.XtraEditors.MemoEdit();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.BtnStock = new DevExpress.XtraEditors.SimpleButton();
            this.BtnProc = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCmd = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new System.Windows.Forms.Label();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxControlChat
            // 
            this.listBoxControlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControlChat.HorizontalScrollbar = true;
            this.listBoxControlChat.Location = new System.Drawing.Point(2, 88);
            this.listBoxControlChat.Name = "listBoxControlChat";
            this.listBoxControlChat.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxControlChat.Size = new System.Drawing.Size(478, 548);
            this.listBoxControlChat.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.memoEditInput);
            this.panelBottom.Controls.Add(this.btnSend);
            this.panelBottom.Controls.Add(this.labelControl2);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(2, 636);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(478, 96);
            this.panelBottom.TabIndex = 1;
            // 
            // memoEditInput
            // 
            this.memoEditInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.memoEditInput.Location = new System.Drawing.Point(2, 2);
            this.memoEditInput.Name = "memoEditInput";
            this.memoEditInput.Properties.NullValuePrompt = "메시지를 입력하세요 (Enter=전송, Ctrl+Enter=줄바꿈)";
            this.memoEditInput.Size = new System.Drawing.Size(387, 75);
            this.memoEditInput.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.btnSend.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.btnSend.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSend.Appearance.Options.UseBackColor = true;
            this.btnSend.Appearance.Options.UseFont = true;
            this.btnSend.Appearance.Options.UseForeColor = true;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(392, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(84, 75);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "전송";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 8F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl2.Location = new System.Drawing.Point(2, 77);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(474, 17);
            this.labelControl2.TabIndex = 49;
            this.labelControl2.Text = "©2025 SMT Vision Co., Ltd. All rights reserved.";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.listBoxControlChat);
            this.groupControl1.Controls.Add(this.panelBottom);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(482, 734);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "groupControl1";
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.BtnStock);
            this.panelControl2.Controls.Add(this.BtnProc);
            this.panelControl2.Controls.Add(this.BtnCmd);
            this.panelControl2.Controls.Add(this.label1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 34);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(478, 54);
            this.panelControl2.TabIndex = 7;
            // 
            // BtnStock
            // 
            this.BtnStock.AllowFocus = false;
            this.BtnStock.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnStock.Appearance.Options.UseBackColor = true;
            this.BtnStock.Location = new System.Drawing.Point(216, 25);
            this.BtnStock.Name = "BtnStock";
            this.BtnStock.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.BtnStock.Size = new System.Drawing.Size(97, 23);
            this.BtnStock.TabIndex = 5;
            this.BtnStock.Text = "재고확인";
            this.BtnStock.Click += new System.EventHandler(this.Btn_Click);
            // 
            // BtnProc
            // 
            this.BtnProc.AllowFocus = false;
            this.BtnProc.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnProc.Appearance.Options.UseBackColor = true;
            this.BtnProc.Location = new System.Drawing.Point(10, 25);
            this.BtnProc.Name = "BtnProc";
            this.BtnProc.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.BtnProc.Size = new System.Drawing.Size(97, 23);
            this.BtnProc.TabIndex = 2;
            this.BtnProc.Text = "생산현황";
            this.BtnProc.Click += new System.EventHandler(this.Btn_Click);
            // 
            // BtnCmd
            // 
            this.BtnCmd.AllowFocus = false;
            this.BtnCmd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnCmd.Appearance.Options.UseBackColor = true;
            this.BtnCmd.Location = new System.Drawing.Point(113, 25);
            this.BtnCmd.Name = "BtnCmd";
            this.BtnCmd.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.BtnCmd.Size = new System.Drawing.Size(97, 23);
            this.BtnCmd.TabIndex = 3;
            this.BtnCmd.Text = "지시현황";
            this.BtnCmd.Click += new System.EventHandler(this.Btn_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Pretendard SemiBold", 8F);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "ⓘ 자주 사용하는 기능 : 대화를 끝내고 싶을 때는 ‘종료’라고 입력하세요.";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(478, 32);
            this.panelControl1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(432, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 7, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(46, 41);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::SmartMES.Properties.Resources.SFLOGO;
            this.pictureEdit1.Location = new System.Drawing.Point(3, 2);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(104, 28);
            this.pictureEdit1.TabIndex = 0;
            // 
            // ChatMain_V2
            // 
            this.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 734);
            this.Controls.Add(this.groupControl1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(482, 734);
            this.Name = "ChatMain_V2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FAQ";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.Label btnClose;
        private DevExpress.XtraEditors.SimpleButton BtnProc;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnStock;
        private DevExpress.XtraEditors.SimpleButton BtnCmd;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
