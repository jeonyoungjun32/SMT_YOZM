
namespace SmartMES.View.QM
{
    partial class Frm_PopupMeasure
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cmd_End = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Test = new System.Windows.Forms.TextBox();
            this.SaveConfig = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btn_discnt = new System.Windows.Forms.Button();
            this.lb_Port = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_cnt = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_Fc = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_SB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_Prt = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_DB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_BPS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Port = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cmd_Save = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmd_Save);
            this.panelControl1.Controls.Add(this.memoEdit1);
            this.panelControl1.Controls.Add(this.cmd_End);
            this.panelControl1.Controls.Add(this.groupBox1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(464, 286);
            this.panelControl1.TabIndex = 0;
            // 
            // cmd_End
            // 
            this.cmd_End.Appearance.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmd_End.Appearance.Options.UseFont = true;
            this.cmd_End.Location = new System.Drawing.Point(874, 417);
            this.cmd_End.Name = "cmd_End";
            this.cmd_End.Size = new System.Drawing.Size(120, 58);
            this.cmd_End.TabIndex = 401;
            this.cmd_End.Text = "종료";
            this.cmd_End.Click += new System.EventHandler(this.cmd_End_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Test);
            this.groupBox1.Controls.Add(this.SaveConfig);
            this.groupBox1.Controls.Add(this.txtStatus);
            this.groupBox1.Controls.Add(this.btn_discnt);
            this.groupBox1.Controls.Add(this.lb_Port);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btn_cnt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cb_Fc);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cb_SB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cb_Prt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cb_DB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cb_BPS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cb_Port);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 266);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Option";
            // 
            // txt_Test
            // 
            this.txt_Test.Font = new System.Drawing.Font("굴림체", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Test.Location = new System.Drawing.Point(313, 92);
            this.txt_Test.Name = "txt_Test";
            this.txt_Test.Size = new System.Drawing.Size(127, 35);
            this.txt_Test.TabIndex = 392;
            // 
            // SaveConfig
            // 
            this.SaveConfig.Location = new System.Drawing.Point(313, 31);
            this.SaveConfig.Name = "SaveConfig";
            this.SaveConfig.Size = new System.Drawing.Size(127, 55);
            this.SaveConfig.TabIndex = 386;
            this.SaveConfig.Text = "설정저장";
            this.SaveConfig.UseVisualStyleBackColor = true;
            this.SaveConfig.Click += new System.EventHandler(this.SaveConfig_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(11, 212);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(227, 48);
            this.txtStatus.TabIndex = 18;
            this.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_discnt
            // 
            this.btn_discnt.Location = new System.Drawing.Point(341, 193);
            this.btn_discnt.Name = "btn_discnt";
            this.btn_discnt.Size = new System.Drawing.Size(99, 67);
            this.btn_discnt.TabIndex = 17;
            this.btn_discnt.Text = "Disconnect";
            this.btn_discnt.UseVisualStyleBackColor = true;
            this.btn_discnt.Click += new System.EventHandler(this.btn_discnt_Click);
            // 
            // lb_Port
            // 
            this.lb_Port.FormattingEnabled = true;
            this.lb_Port.Location = new System.Drawing.Point(201, 31);
            this.lb_Port.Name = "lb_Port";
            this.lb_Port.Size = new System.Drawing.Size(106, 147);
            this.lb_Port.Sorted = true;
            this.lb_Port.TabIndex = 4;
            this.lb_Port.SelectedIndexChanged += new System.EventHandler(this.lb_Port_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(206, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 14);
            this.label7.TabIndex = 16;
            this.label7.Text = "Port List";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(19, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "State";
            // 
            // btn_cnt
            // 
            this.btn_cnt.Location = new System.Drawing.Point(244, 193);
            this.btn_cnt.Name = "btn_cnt";
            this.btn_cnt.Size = new System.Drawing.Size(91, 67);
            this.btn_cnt.TabIndex = 14;
            this.btn_cnt.Text = "Connect";
            this.btn_cnt.UseVisualStyleBackColor = true;
            this.btn_cnt.Click += new System.EventHandler(this.btn_cnt_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(8, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Flow Control -";
            // 
            // cb_Fc
            // 
            this.cb_Fc.Enabled = false;
            this.cb_Fc.FormattingEnabled = true;
            this.cb_Fc.Items.AddRange(new object[] {
            "Xon/Xoff",
            "RTS/CTS",
            "None"});
            this.cb_Fc.Location = new System.Drawing.Point(103, 161);
            this.cb_Fc.Name = "cb_Fc";
            this.cb_Fc.Size = new System.Drawing.Size(91, 21);
            this.cb_Fc.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(29, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Stop Bits -";
            // 
            // cb_SB
            // 
            this.cb_SB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SB.FormattingEnabled = true;
            this.cb_SB.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cb_SB.Location = new System.Drawing.Point(103, 134);
            this.cb_SB.Name = "cb_SB";
            this.cb_SB.Size = new System.Drawing.Size(91, 21);
            this.cb_SB.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(49, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Parity -";
            // 
            // cb_Prt
            // 
            this.cb_Prt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Prt.FormattingEnabled = true;
            this.cb_Prt.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None",
            "Mark",
            "Space"});
            this.cb_Prt.Location = new System.Drawing.Point(103, 107);
            this.cb_Prt.Name = "cb_Prt";
            this.cb_Prt.Size = new System.Drawing.Size(91, 21);
            this.cb_Prt.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(29, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data Bits -";
            // 
            // cb_DB
            // 
            this.cb_DB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DB.FormattingEnabled = true;
            this.cb_DB.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cb_DB.Location = new System.Drawing.Point(103, 80);
            this.cb_DB.Name = "cb_DB";
            this.cb_DB.Size = new System.Drawing.Size(91, 21);
            this.cb_DB.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(19, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Baud Rate -";
            // 
            // cb_BPS
            // 
            this.cb_BPS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_BPS.FormattingEnabled = true;
            this.cb_BPS.Items.AddRange(new object[] {
            "50",
            "75",
            "100",
            "200",
            "250",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.cb_BPS.Location = new System.Drawing.Point(103, 53);
            this.cb_BPS.Name = "cb_BPS";
            this.cb_BPS.Size = new System.Drawing.Size(91, 21);
            this.cb_BPS.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(25, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "COM Port -";
            // 
            // cb_Port
            // 
            this.cb_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Port.FormattingEnabled = true;
            this.cb_Port.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16"});
            this.cb_Port.Location = new System.Drawing.Point(103, 26);
            this.cb_Port.Name = "cb_Port";
            this.cb_Port.Size = new System.Drawing.Size(91, 21);
            this.cb_Port.TabIndex = 1;
            // 
            // cmd_Save
            // 
            this.cmd_Save.Appearance.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmd_Save.Appearance.Options.UseFont = true;
            this.cmd_Save.Location = new System.Drawing.Point(731, 417);
            this.cmd_Save.Name = "cmd_Save";
            this.cmd_Save.Size = new System.Drawing.Size(125, 58);
            this.cmd_Save.TabIndex = 403;
            this.cmd_Save.Text = "저장";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(484, 23);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(337, 286);
            this.memoEdit1.TabIndex = 402;
            // 
            // FrmPopupMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 286);
            this.Controls.Add(this.panelControl1);
            this.Name = "Frm_PopupMeasure";
            this.Text = "Frm_PopupMeasure";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Frm_PopupMeasure_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Test;
        private System.Windows.Forms.Button SaveConfig;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btn_discnt;
        private System.Windows.Forms.ListBox lb_Port;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_cnt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_Fc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_SB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_Prt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_DB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_BPS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_Port;
        private System.IO.Ports.SerialPort serialPort1;
        private DevExpress.XtraEditors.SimpleButton cmd_End;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.SimpleButton cmd_Save;
    }
}