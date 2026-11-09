namespace SmartMES.View.Chatbot
{
    partial class ChatMain
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
            this.components = new System.ComponentModel.Container();
            this.listBoxControlChat = new DevExpress.XtraEditors.ListBoxControl();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.memoEditInput = new DevExpress.XtraEditors.MemoEdit();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditInput.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxControlChat
            // 
            this.listBoxControlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControlChat.HorizontalScrollbar = true;
            this.listBoxControlChat.Location = new System.Drawing.Point(0, 0);
            this.listBoxControlChat.Name = "listBoxControlChat";
            this.listBoxControlChat.Size = new System.Drawing.Size(1127, 560);
            this.listBoxControlChat.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.memoEditInput);
            this.panelBottom.Controls.Add(this.btnSend);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 560);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1127, 96);
            this.panelBottom.TabIndex = 1;
            // 
            // memoEditInput
            // 
            this.memoEditInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEditInput.Location = new System.Drawing.Point(2, 2);
            this.memoEditInput.Name = "memoEditInput";
            this.memoEditInput.Properties.AcceptsReturn = true;
            this.memoEditInput.Properties.NullValuePrompt = "메시지를 입력하세요 (Enter=전송, Ctrl+Enter=줄바꿈)";
            this.memoEditInput.Properties.NullValuePromptShowForEmptyValue = true;
            this.memoEditInput.Size = new System.Drawing.Size(1003, 92);
            this.memoEditInput.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(1005, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 92);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            // 
            // ChatMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 656);
            this.Controls.Add(this.listBoxControlChat);
            this.Controls.Add(this.panelBottom);
            this.Name = "ChatMain";
            this.Text = "ChatMain";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditInput.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
