using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using SmartPOP.Model.EQ;
using SmartPOP.Models;
using SmartPOP.Models.EQ;
using SMT_MES.Controls;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SmartPOP.Views.EQ
{
	public partial class Frm_EQ_MachineChk_DailyView : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //점검항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //점검항목 Repository 숫자입력

        //10 : 설비일상점검
        //20 : 설비정기점검
        private string Inspect_Div_Cd = string.Empty; //검사구분
        string SaveDiv = "";
        bool Success = true;
        string Week_Div_Cd = "";

        string G_Rst_No = "";
        int G_Rst_Row_Seq = 0;

        public Frm_EQ_MachineChk_DailyView()
        {
            InitializeComponent();
            Inspect_Div_Cd = "10";

            SetDesign();
        } 
        public Frm_EQ_MachineChk_DailyView(string Div_Cd, string rRst_No, int rRst_Row_Seq)
        {
            InitializeComponent();

            switch (Div_Cd)
            {
                case "Daily": Inspect_Div_Cd = "10"; break;   //설비일상점검
                case "Routine": Inspect_Div_Cd = "20"; break; //설비정기점검
            }

            G_Rst_No = rRst_No;
            G_Rst_Row_Seq = rRst_Row_Seq;

            SetDesign();
        }

        public void GetDay()
        {
            DateTime nowDt = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue.ToString());

            switch (nowDt.DayOfWeek)
            {
                case DayOfWeek.Monday: Week_Div_Cd = "01"; break;
                case DayOfWeek.Tuesday: Week_Div_Cd = "02"; break;
                case DayOfWeek.Wednesday: Week_Div_Cd = "03"; break;
                case DayOfWeek.Thursday: Week_Div_Cd = "04"; break;
                case DayOfWeek.Friday: Week_Div_Cd = "05"; break;
                case DayOfWeek.Saturday: Week_Div_Cd = "06"; break;
                case DayOfWeek.Sunday: Week_Div_Cd = "07"; break;
            }
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now;

            //rpo.Set_POP_Dtp(DtpInspDate);


            if (Inspect_Div_Cd == "10")
            {
                //DtpInspDate.Properties.DisplayFormat.FormatString = "yyyy-MM";
                //DtpInspDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                //DtpInspDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //DtpInspDate.Properties.Mask.EditMask = "yyyy-MM";
                Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            }
            else
            {
                //DtpInspDate.Properties.DisplayFormat.FormatString = "yyyy";
                //DtpInspDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                //DtpInspDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //DtpInspDate.Properties.Mask.EditMask = "yyyy";
                Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Year;
            }

            //검사구분에따른 설정
            //Set_TypeOption();

            //GridView 설정
            SetGrid_Insp(Gv_Insp);

            Set_Lup();
        }

        //private void Set_TypeOption()
        //{
        //    if (Inspect_Div_Cd == "10")
        //    {
        //        this.Name = "Frm_EQ_Machine_Chk_Daily";
        //        this.Text = "설비일상점검";
        //    }
        //    else if (Inspect_Div_Cd == "20")
        //    {
        //        this.Name = "Frm_EQ_Machine_Chk_Routine";
        //        this.Text = "설비정기점검";
        //    }
        //}

        #region GridView 셋팅

        private void SetGrid_Insp(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.IndicatorWidth = 50;                     //순번 너비

            grid.Create_GvColumn(gv, "설비명칭", "Machine_Nm");
            grid.Create_GvColumn(gv, "관리항목", "Management_Item");
            grid.Create_GvColumn(gv, "확인방법", "Check_Method");
            grid.Create_GvColumn(gv, "점검기준", "Standard_Spec");

            int colCnt = 0;
            string fieldNm = "";
            string caption = "";

            switch (Inspect_Div_Cd)
            {
                case "10": colCnt = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1).AddDays(-1).Day; break;
                case "20": colCnt = 12; break;
            }

            for (int i = 1; i <= colCnt; i++)
            {
                fieldNm = i.ToString("D2");
                caption = fieldNm + (Inspect_Div_Cd == "20" ? "월" : "일");

                grid.Create_GvColumn(gv, caption, fieldNm);
                grid.SetCol_Type(gv.Columns[fieldNm], cls_Grid.TypeList.TFloat);
                gv.Columns[fieldNm].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //이벤트
            grid.SetPOPGrid(gv);
            grid.RowNumber_Indicator(gv);     //행번호 표기
        }

        private void Set_Lup()
        {
            cls_Lookup clsLup = new cls_Lookup();

            //1. 조회조건

            //설비
            EQ_MachineChk machine = new EQ_MachineChk();

            DataTable dt_machine = machine.Select_Machine_Lup();
            Lup_Machine.smtLookUpDataSource = dt_machine;
            Lup_Machine.smtLookUpDataDisplayMember = "명칭";
            Lup_Machine.smtLookUpDataValueMember = "코드";
            Lup_Machine.smtLookUpValue = dt_machine.Rows[0][0].ConvertString_Null_To_Empty();

            Lup_Machine.smtLookUpEditValueChanged += Lup_EditValueChanged;

            //LupMachine.Properties.ShowClearButton = false;
            //LupMachine.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            //LupMachine.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정
        }

        #endregion GridView 셋팅 End


        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit Lup = sender as SearchLookUpEdit;

            string LupName = Lup.Name.ToString();
            string LupValue = Lup.EditValue.ConvertString_Null_To_Empty();

            if (LupName == "LupInsp_Div") Inspect_Div_Cd = LupValue;

            Search_InspectItem();
        }

		#endregion

		private void Frm_Load(object sender, EventArgs e)
        {
            Search_InspectItem();
        }

        private void Search_InspectItem()
		{
            //검사목록 불러오기

            string Machine_Cd = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            string date = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM");

            if (Machine_Cd == "")
            {
                cls_MessageBox.Show("설비 미입력");
                return;
            }


            EQ_Machine_Chk mc = new EQ_Machine_Chk();
            mc.Inspect_Div_Cd = Inspect_Div_Cd; //10 : 일상점검, 20 : 정기점검
            mc.Machine_Cd = Machine_Cd;
            //mc.Chk_Time_Div_Cd = Chk_Time_Div_Cd;
            //mc.Week_Div_Cd = Week_Div_Cd;

            Gc_Insp.DataSource = mc.EQ_Machine_ChkView(date);
            Gv_Insp.BestFitColumns();
        }

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search_InspectItem();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void cmdExcel_Click(object sender, EventArgs e)
		{
            GridView gv = Gv_Insp;

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel File|*.xlsx"; //파일형식
            saveFile.Title = "Save an Excel File"; //파일 저장 시 좌측상단에 나타나는 제목
            if (saveFile.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }


            // 파일명을 설정하지 않으면(빈 값) 저장 취소
            if (string.IsNullOrEmpty(saveFile.FileName))
            {
                cls_MessageBox.Show("파일명을 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                gv.ExportToXlsx(saveFile.FileName);
                cls_MessageBox.Show("엑셀 파일 저장 완료", "파일 저장", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 저장된 파일 열기
                Process.Start(saveFile.FileName);
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show($"파일 저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(Gv_Insp);
        }
    }
}
