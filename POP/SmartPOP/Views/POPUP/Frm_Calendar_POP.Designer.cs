
namespace SmartPOP.Views.POPUP
{
    partial class Frm_Calendar_POP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Calendar_POP));
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.btn_CurrentDate = new DevExpress.XtraEditors.SimpleButton();
            this.calendarControl1 = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.btn_AM08 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_PM20 = new DevExpress.XtraEditors.SimpleButton();
            this.Txt_Date = new DevExpress.XtraEditors.TextEdit();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseFont = true;
            this.btn_Close.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.ImageOptions.Image")));
            this.btn_Close.Location = new System.Drawing.Point(359, -1);
            this.btn_Close.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(36, 36);
            this.btn_Close.TabIndex = 98;
            this.btn_Close.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_CurrentDate
            // 
            this.btn_CurrentDate.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_CurrentDate.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btn_CurrentDate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.btn_CurrentDate.Appearance.Options.UseBackColor = true;
            this.btn_CurrentDate.Appearance.Options.UseFont = true;
            this.btn_CurrentDate.AppearancePressed.BackColor = System.Drawing.Color.Silver;
            this.btn_CurrentDate.AppearancePressed.Options.UseBackColor = true;
            this.tablePanel1.SetColumn(this.btn_CurrentDate, 0);
            this.btn_CurrentDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_CurrentDate.Location = new System.Drawing.Point(3, 466);
            this.btn_CurrentDate.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.btn_CurrentDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_CurrentDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CurrentDate.Name = "btn_CurrentDate";
            this.tablePanel1.SetRow(this.btn_CurrentDate, 2);
            this.btn_CurrentDate.Size = new System.Drawing.Size(191, 79);
            this.btn_CurrentDate.TabIndex = 100;
            this.btn_CurrentDate.Text = "현재일자";
            this.btn_CurrentDate.Click += new System.EventHandler(this.btn_Click);
            // 
            // calendarControl1
            // 
            this.calendarControl1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.Appearance.Options.UseFont = true;
            this.calendarControl1.AutoSize = false;
            this.calendarControl1.CalendarAppearance.Button.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.Button.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.ButtonHighlighted.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.ButtonHighlighted.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.ButtonPressed.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.ButtonPressed.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.CalendarHeader.Font = new System.Drawing.Font("Pretendard SemiBold", 55F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.CalendarHeader.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCell.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCell.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCell.Options.UseTextOptions = true;
            this.calendarControl1.CalendarAppearance.DayCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellDisabled.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellDisabled.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellHighlighted.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellHighlighted.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellHoliday.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellHoliday.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellHoliday.Options.UseTextOptions = true;
            this.calendarControl1.CalendarAppearance.DayCellHoliday.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellHoliday.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellInactive.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellInactive.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellInactive.Options.UseTextOptions = true;
            this.calendarControl1.CalendarAppearance.DayCellInactive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellInactive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellPressed.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellPressed.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellPressed.Options.UseTextOptions = true;
            this.calendarControl1.CalendarAppearance.DayCellPressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellPressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellSelected.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellSelected.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellSelected.Options.UseTextOptions = true;
            this.calendarControl1.CalendarAppearance.DayCellSelected.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellSelected.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellSpecial.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellSpecialHighlighted.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellSpecialHighlighted.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellSpecialPressed.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellSpecialPressed.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.BackColor = System.Drawing.Color.Yellow;
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.Options.UseBackColor = true;
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.Options.UseTextOptions = true;
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellToday.BackColor = System.Drawing.Color.White;
            this.calendarControl1.CalendarAppearance.DayCellToday.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.DayCellToday.ForeColor = System.Drawing.Color.Black;
            this.calendarControl1.CalendarAppearance.DayCellToday.Options.UseBackColor = true;
            this.calendarControl1.CalendarAppearance.DayCellToday.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellToday.Options.UseForeColor = true;
            this.calendarControl1.CalendarAppearance.DayCellToday.Options.UseTextOptions = true;
            this.calendarControl1.CalendarAppearance.DayCellToday.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calendarControl1.CalendarAppearance.DayCellToday.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.calendarControl1.CalendarAppearance.Header.Font = new System.Drawing.Font("Pretendard SemiBold", 13F, System.Drawing.FontStyle.Bold);
            this.calendarControl1.CalendarAppearance.Header.ForeColor = System.Drawing.Color.Black;
            this.calendarControl1.CalendarAppearance.Header.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.Header.Options.UseForeColor = true;
            this.calendarControl1.CalendarAppearance.WeekDay.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.calendarControl1.CalendarAppearance.WeekDay.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.WeekNumber.FontSizeDelta = 50;
            this.calendarControl1.CalendarAppearance.WeekNumber.Options.UseFont = true;
            this.calendarControl1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetColumn(this.calendarControl1, 0);
            this.tablePanel1.SetColumnSpan(this.calendarControl1, 2);
            this.calendarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarControl1.DrawCellLines = true;
            this.calendarControl1.HeaderPadding = new System.Windows.Forms.Padding(-5);
            this.calendarControl1.HighlightTodayCell = DevExpress.Utils.DefaultBoolean.True;
            this.calendarControl1.InactiveDaysVisibility = DevExpress.XtraEditors.Controls.CalendarInactiveDaysVisibility.Hidden;
            this.calendarControl1.Location = new System.Drawing.Point(1, 68);
            this.calendarControl1.Margin = new System.Windows.Forms.Padding(1);
            this.calendarControl1.Name = "calendarControl1";
            this.tablePanel1.SetRow(this.calendarControl1, 1);
            this.calendarControl1.ShowFooter = false;
            this.calendarControl1.ShowTodayButton = false;
            this.calendarControl1.Size = new System.Drawing.Size(390, 395);
            this.calendarControl1.TabIndex = 0;
            this.calendarControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.calendarControl1_Click);
            // 
            // btn_AM08
            // 
            this.btn_AM08.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_AM08.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btn_AM08.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_AM08.Appearance.Options.UseBackColor = true;
            this.btn_AM08.Appearance.Options.UseFont = true;
            this.btn_AM08.AppearancePressed.BackColor = System.Drawing.Color.Silver;
            this.btn_AM08.AppearancePressed.Options.UseBackColor = true;
            this.tablePanel1.SetColumn(this.btn_AM08, 0);
            this.btn_AM08.Location = new System.Drawing.Point(3, 549);
            this.btn_AM08.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.btn_AM08.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_AM08.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AM08.Name = "btn_AM08";
            this.tablePanel1.SetRow(this.btn_AM08, 3);
            this.btn_AM08.Size = new System.Drawing.Size(191, 4);
            this.btn_AM08.TabIndex = 103;
            this.btn_AM08.Text = "08:00";
            this.btn_AM08.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_PM20
            // 
            this.btn_PM20.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_PM20.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btn_PM20.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_PM20.Appearance.Options.UseBackColor = true;
            this.btn_PM20.Appearance.Options.UseFont = true;
            this.btn_PM20.AppearancePressed.BackColor = System.Drawing.Color.Silver;
            this.btn_PM20.AppearancePressed.Options.UseBackColor = true;
            this.tablePanel1.SetColumn(this.btn_PM20, 1);
            this.btn_PM20.Location = new System.Drawing.Point(200, 549);
            this.btn_PM20.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.btn_PM20.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_PM20.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PM20.Name = "btn_PM20";
            this.tablePanel1.SetRow(this.btn_PM20, 3);
            this.btn_PM20.Size = new System.Drawing.Size(189, 4);
            this.btn_PM20.TabIndex = 104;
            this.btn_PM20.Text = "20:00";
            this.btn_PM20.Click += new System.EventHandler(this.btn_Click);
            // 
            // Txt_Date
            // 
            this.tablePanel1.SetColumn(this.Txt_Date, 0);
            this.tablePanel1.SetColumnSpan(this.Txt_Date, 2);
            this.Txt_Date.EditValue = "0";
            this.Txt_Date.Location = new System.Drawing.Point(3, 2);
            this.Txt_Date.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_Date.Name = "Txt_Date";
            this.Txt_Date.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Txt_Date.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 36F, System.Drawing.FontStyle.Bold);
            this.Txt_Date.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.Txt_Date.Properties.Appearance.Options.UseBackColor = true;
            this.Txt_Date.Properties.Appearance.Options.UseFont = true;
            this.Txt_Date.Properties.Appearance.Options.UseForeColor = true;
            this.Txt_Date.Properties.Appearance.Options.UseTextOptions = true;
            this.Txt_Date.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Txt_Date.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Txt_Date.Properties.ReadOnly = true;
            this.tablePanel1.SetRow(this.Txt_Date, 0);
            this.Txt_Date.Size = new System.Drawing.Size(386, 72);
            this.Txt_Date.TabIndex = 99;
            this.Txt_Date.TabStop = false;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 51.96F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 51.51F)});
            this.tablePanel1.Controls.Add(this.btn_OK);
            this.tablePanel1.Controls.Add(this.Txt_Date);
            this.tablePanel1.Controls.Add(this.btn_PM20);
            this.tablePanel1.Controls.Add(this.btn_AM08);
            this.tablePanel1.Controls.Add(this.calendarControl1);
            this.tablePanel1.Controls.Add(this.btn_CurrentDate);
            this.tablePanel1.Location = new System.Drawing.Point(2, 36);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 67F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 397F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 83F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(392, 555);
            this.tablePanel1.TabIndex = 105;
            // 
            // btn_OK
            // 
            this.btn_OK.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_OK.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btn_OK.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.btn_OK.Appearance.Options.UseBackColor = true;
            this.btn_OK.Appearance.Options.UseFont = true;
            this.btn_OK.AppearancePressed.BackColor = System.Drawing.Color.Silver;
            this.btn_OK.AppearancePressed.Options.UseBackColor = true;
            this.tablePanel1.SetColumn(this.btn_OK, 1);
            this.tablePanel1.SetColumnSpan(this.btn_OK, 2);
            this.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_OK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_OK.ImageOptions.Image")));
            this.btn_OK.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_OK.Location = new System.Drawing.Point(200, 466);
            this.btn_OK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.btn_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_OK.Name = "btn_OK";
            this.tablePanel1.SetRow(this.btn_OK, 2);
            this.btn_OK.Size = new System.Drawing.Size(189, 79);
            this.btn_OK.TabIndex = 105;
            this.btn_OK.Text = "ENTER";
            this.btn_OK.Click += new System.EventHandler(this.btn_Click);
            // 
            // Frm_Calendar_POP
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 570);
            this.ControlBox = false;
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btn_Close);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(500, 620);
            this.MinimumSize = new System.Drawing.Size(400, 600);
            this.Name = "Frm_Calendar_POP";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Frm_DateTime_POP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn_Close;
        private DevExpress.XtraEditors.SimpleButton btn_CurrentDate;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SimpleButton btn_OK;
        private DevExpress.XtraEditors.TextEdit Txt_Date;
        private DevExpress.XtraEditors.SimpleButton btn_PM20;
        private DevExpress.XtraEditors.SimpleButton btn_AM08;
        private DevExpress.XtraEditors.Controls.CalendarControl calendarControl1;
    }
}