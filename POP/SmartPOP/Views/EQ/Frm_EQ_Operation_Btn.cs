using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
//using SmartMES.View.Lookup;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.EQ;
using SmartPOP.Views.PM;
using SmartPOP.Views.POPUP;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartPOP.Views.Work
{
	public partial class Frm_EQ_Operation_Btn : DevExpress.XtraEditors.XtraForm
    {
        private string orderNo;
        private int orderSeq;
        private int SubSeq;
        private string processCode;
        private string lineCode;
        private string MachineCode;

        private string query = "";
        
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        Database db = new Database();

        clsCode code = new clsCode();

        ucBtnMachineO[] DataChartClass = null;
        ucBtnMachineO[] DataChartItem = null;

        int MaxCnt = 12;    //MaxUserControl 수
        int ColCnt = 3;
        int RowCnt = 4;

        public Frm_EQ_Operation_Btn()
        {
            InitializeComponent();
        }

        public Frm_EQ_Operation_Btn(string orderNo, int seq)
        {
            InitializeComponent();

            this.orderNo = orderNo;
            this.orderSeq = seq;

            TxtOrderNo.Text = orderNo;
            TxtOrderSeq.EditValue = orderSeq;
            layout_SubSeq.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public Frm_EQ_Operation_Btn(string orderNo, int seq, int subSeq, string machineCode)
        {
            InitializeComponent();

            this.orderNo = orderNo;
            this.orderSeq = seq;
            this.SubSeq = subSeq;
            this.MachineCode = machineCode;


            TxtOrderNo.Text = orderNo;
            TxtOrderSeq.EditValue = orderSeq;
            TxtSubSeq.EditValue = SubSeq;
        }

        private void frmSelectPM_ProductPlan_Load(object sender, EventArgs e)
        {
            InitSetting();

            // 이미 비가동 중인지 체크
            SetLblState_IsDown(IsDownRunning());
            Set_Info();
		}

        private void InitSetting()
        {
            //업체별 DGP MaxCount, Column, Row 수  설정
            DataTable dtClass = code.GetClass("2030").ToTable();
            int cntClass = dtClass.Rows.Count;

            //비가동분류 버튼 생성
            TpClass.Rows.Clear();
            TpClass.Columns.Clear();

            //컬럼 생성 1개만
            TpClass.Columns.Add(new TablePanelColumn(TablePanelEntityStyle.Relative, 50));

            //TablePanel Row 생성
            for (int i = 0; i < cntClass; i++)
            {
                TpClass.Rows.Add(new TablePanelRow(TablePanelEntityStyle.Relative, 50));
            }

			DataChartClass = new ucBtnMachineO[MaxCnt];

			for (int i = 0; i < cntClass; i++)
			{
				string itemCode = dtClass.Rows[i]["CLASS"].ConvertString_Null_To_Empty();
				string text = dtClass.Rows[i]["CLASSNAME"].ConvertString_Null_To_Empty();

				DataChartClass[i] = new ucBtnMachineO("분류", itemCode, text);

				DataChartClass[i].Dock = DockStyle.Fill;
				DataChartClass[i].Visible = true;
				DataChartClass[i].ItemClicked += UcBtn_ItemClicked;

                TpClass.Controls.Add(DataChartClass[i]);
                TpClass.SetCell(DataChartClass[i], i, 0);
            }

            if (dtClass.Rows.Count > 0)
			{
                //하위 비가동 항목 버튼으로 생성
                SetTpMain(dtClass.Rows[0]["CLASS"].ConvertString_Null_To_Empty());
			}
        }

        private void SetTpMain(string classCode)
		{
            DataTable dtItemCode = code.GetCateItem(WrGlobal.CorpID, classCode);
            MaxCnt = dtItemCode.Rows.Count;

            int nCount = 0;

            //우측 비가동유형
            //TablePanel 초기화 
            TPMain.Controls.Clear();
            TPMain.Rows.Clear(); 
            TPMain.Columns.Clear();

            //TablePanel 컬럼 생성
            for (int i = 0; i < ColCnt; i++)
            {
                TPMain.Columns.Add(new TablePanelColumn(TablePanelEntityStyle.Relative, 50));
            }

            //TablePanel Row 생성
            for (int i = 0; i < RowCnt; i++)
            {
                TPMain.Rows.Add(new TablePanelRow(TablePanelEntityStyle.Relative, 50));
            }

            DataChartItem = new ucBtnMachineO[MaxCnt];

            for (int i = 0; i < TPMain.Rows.Count; i++)
            {
                for (int j = 0; j < TPMain.Columns.Count; j++)
                {
                    if (nCount < MaxCnt)
                    {
                        string itemCode = dtItemCode.Rows[nCount]["코드"].ConvertString_Null_To_Empty();
                        string text = dtItemCode.Rows[nCount]["명칭"].ConvertString_Null_To_Empty();

                        DataChartItem[nCount] = new ucBtnMachineO("비가동코드", itemCode, text);

                        DataChartItem[nCount].Dock = DockStyle.Fill;
                        DataChartItem[nCount].Visible = true;
                        DataChartItem[nCount].ItemClicked += UcBtn_ItemClicked;

                        TPMain.Controls.Add(DataChartItem[nCount]);
                        TPMain.SetCell(DataChartItem[nCount], i, j);                                     

                        nCount++;
                    }
                }
            }
        }

        private void UcBtn_ItemClicked(string type, string itemCode)
        {
			if (type == "분류")
			{
                //분류 선택시 비가동 유형 버튼 재생성
                SetTpMain(itemCode);
			}
			else if (type == "비가동코드")
			{
                // 이미 비가동 중인지 체크
                if (IsDownRunning())
                {
                    cls_MessageBox.Show("이미 비가동이 진행 중입니다.");
                    return;
                }

                OPStart(itemCode);
            }
        }

        private bool IsDownRunning()
        {
            EQ_Operation mo = new EQ_Operation();
            mo.CorpId = WrGlobal.CorpID;
            mo.OrderNo = orderNo;
            mo.OrderSeq = orderSeq;

            DataTable dt = mo.SelectOP_EndTimeNULL();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void SetLblState_IsDown(bool isDown)
        {
            if (isDown)
            {
                lblState.Text = "● 비가동 진행중";
                lblState.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblState.Text = "● 정상";
                lblState.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        #region 디자인

        private void Set_Info()
        {
            query = $@"SELECT TOP 1 
                              OP.ProcessCode
                            , CP.ProcessName
                            , OP.LineCode
                            , OP.MachineCode
                            , CL.LineName
                        FROM PM_OrderProcess OP
                        INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode
                        INNER JOIN CM_Line CL ON OP.LineCode = CL.LineCode
                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                        AND OP.OrderNo = '{orderNo}'
                        AND OP.OrderSeq = {orderSeq}
                        ";

            DataRow dr = db.GetDataRow(query);

            TxtLine.Text = dr["LineName"].ToString();
            TxtProcess.Text = dr["ProcessName"].ToString();

            MachineCode = MachineCode == "" ? dr["MachineCode"].ToString() : MachineCode;
            processCode = dr["ProcessCode"].ToString();
            lineCode = dr["LineCode"].ToString();
        }

        private void Set_DateTimePad(object sender, RowCellClickEventArgs e)
        {
            LabelControl lb = sender as LabelControl;
            GridView gv = sender as GridView;
            int TargetRow = e.RowHandle;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            if (!fieldName.Contains("Time")) return;

            //팝업 키패드에 띄우는 날짜
            string currentValue = drChanged[fieldName].ToString();
            string date = "";
            if (drChanged[fieldName].ToString() == "")
            {
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                date = Convert.ToDateTime(drChanged[fieldName]).ToString("yyyy-MM-dd HH:mm");
            }
            //날짜+시간 입력패드
            Frm_DateTime_POP frm = new Frm_DateTime_POP(date);
            frm.ShowDialog();

            Tuple<string, string> result = frm.resultDateTime();
            string newDate = result.Item1;  // 팝업에서 선택한 새 값

            //item1: 일자,시간 item2:시간
            // 값이 실제로 변경되었는지 확인
            if (newDate != currentValue)
            {
                // 값이 변경된 경우에만 셀 값을 업데이트
                if (newDate.Trim() == "") return;

                gv.SetFocusedRowCellValue(fieldName, newDate);
            }
        }

        #endregion

        private void OPStart(string itemCode)
		{
            EQ_Operation mo = new EQ_Operation();
            mo.CorpId = WrGlobal.CorpID;
            mo.OrderNo = orderNo;
            mo.OrderSeq = orderSeq;
            mo.WorkDay = DateTime.Now.ToString("yyyyMMdd");
            mo.ProcessCode = processCode;
            mo.LineCode = lineCode;
            mo.Seq = mo.GetNextSeq();  //Seq 신규 채번
            mo.MachineCode = MachineCode;
            mo.OperationYn = "N";
            mo.ItemCode = itemCode; 
            mo.StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//dr["StartTime"].ConvertString_Null_To_Empty();

			if (mo.Ins_MachineOP())
			{
                SetLblState_IsDown(true);
            }
        }

		private void BtnOPEnd_Click_1(object sender, EventArgs e)
		{
            //같은 지시번호, 순번으로 endTime null인 항목 조회, 시작시간 맞는지 확인 후 아닌 경우 비가동 등록 팝업 띄우기
            EQ_Operation mo = new EQ_Operation();
            mo.CorpId = WrGlobal.CorpID;
            mo.OrderNo = orderNo;
            mo.OrderSeq = orderSeq;

            DataTable dt = mo.SelectOP_EndTimeNULL();

            if (dt == null) {  return; }
            if (dt.Rows.Count < 1) { return; }

            DataRow drOP = dt.Rows[0];
            string startTime = drOP["StartTime"].ConvertString_Null_To_Empty();

            if (cls_MessageBox.Show($"{startTime}에 시작된 비가동을\n종료하시겠습니까?", "비가동 종료", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            mo.ItemCode = drOP["ClassItemCode"].ConvertString_Null_To_Empty();
            mo.WorkDay = drOP["WorkDay"].ConvertString_Null_To_Empty();
            mo.ProcessCode = drOP["ProcessCode"].ConvertString_Null_To_Empty();
            mo.LineCode = drOP["LineCode"].ConvertString_Null_To_Empty();
            mo.Seq = drOP["Seq"].ConvertInt_Null_To_Zero();
            mo.EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//dr["EndTime"].ConvertString_Null_To_Empty();

            DateTime start = Convert.ToDateTime(startTime);
            DateTime end = Convert.ToDateTime(mo.EndTime);

            TimeSpan diff = end - start;
            int stopTime = (int)diff.TotalMinutes;

            mo.StopTime = stopTime;

            mo.Upd_MachineOP_EndTime();

            SetLblState_IsDown(IsDownRunning()); //상태값 변경
        }

		private void BtnSearch_Click(object sender, EventArgs e)
		{
            Frm_MachineDowntime frm = new Frm_MachineDowntime(orderNo, orderSeq, SubSeq, MachineCode);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

		private void simpleButton3_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}