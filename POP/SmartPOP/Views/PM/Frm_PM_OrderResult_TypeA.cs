using DevExpress.Office.Drawing;
using DevExpress.PivotGrid.OLAP;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Models.PM;
using SmartPOP.Common;
using SmartPOP.Model.EQ;
using SmartPOP.Models;
using SmartPOP.Models.BM;
using SmartPOP.Models.CM;
using SmartPOP.Models.PM;
using SmartPOP.Models.QM;
using SmartPOP.Models.ST;
using SmartPOP.Views.Common;
using SmartPOP.Views.PM.POPUP;
using SmartPOP.Views.POPUP;
using SmartPOP.Views.Work;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SmartPOP.Views.PM
{
	public partial class Frm_PM_OrderResult_TypeA : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";

        cls_Grid grid = new cls_Grid();
        cls_Lookup rpo = new cls_Lookup();
        clsCode code = new clsCode();

        string Target = ""; //터치패드 입력 대상
        //저장, 수정 구분
        bool EditProcessResult = false;

        string _OrderNo = "";
        int _OrderSeq = 0;
        float _OrderQty = 0;
        int _SubSeq = 0;       //실적순번
        string _SerialNo = "";

        string _MaterialCode = "";
        string _ProcessCode = "PR002";

        public Frm_PM_OrderResult_TypeA()
        {
            InitializeComponent();
        }

        private void Frm_POP_Product_Load(object sender, EventArgs e)
        {
            InitGridView();

            TextEditMaskN(Lbl_OrderQty);
            TextEditMaskN(Lbl_TotalWorkQty);
            TextEditMaskN(Lbl_WorkQty);
            TextEditMaskN(Lbl_GoodQty);
            TextEditMaskN(Lbl_NgQty);

            SetLup();

            //터치패드에서 입력 숫자 제한을 위한 옵션
            Lbl_NgQty.Tag = "Qty";

            if (Lbl_MaterialName.Text == "")
            {
                CmdSearch();
            }
        }

        private void TextEditMaskN(TextEdit te)
        {
            te.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            te.Properties.Mask.EditMask = "#,###,##0";
            te.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        public void SetLup()
        {
            rpo.Set_POPSize_Lup(LupProcess);
			rpo.Create_RepositoryItemSearchLookUpDefault(LupProcess, code.GetProcess_List_ProcessType(false, null, new string[] { "10"}), "공정코드", "공정명");
            LupProcess.EditValue = _ProcessCode;

            rpo.Set_POPSize_Lup(LupMachine);
			rpo.Create_RepositoryItemSearchLookUpDefault(LupMachine, code.GetMachineList(false), "설비코드", "설비명");

            rpo.Set_POP_Dtp(DtpWorkDate);
            rpo.Set_POP_Dtp(DtLotNo);
        }

        private void InitGridView()
        {
            GridView gv = gv_Bad;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.Appearance.HeaderPanel.Font = new Font("맑은 고딕", 15, FontStyle.Bold);

            //gv.Appearance.HeaderPanel.ForeColor = Color.White;
            gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Row 정렬
            gv.OptionsBehavior.Editable = false; //모든 컬럼 수정 불가

            gv.OptionsView.ColumnAutoWidth = true;     //true 보다는 베스트핏이 좋음, 컬럼이 하나일때도 화면을 가득채움, 비추천

            gv_Bad.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler(this.gv_Bad_CustomRowFilter); //로우 숨김 처리
            gv.OptionsCustomization.AllowSort = false;  //정렬변경 불가
        }

        private void SearchBad(string processcd, string serialNo)
        {
			GridView gv = gv_Bad;
			gc_Bad.DataSource = null;
			gv.Columns.Clear(); //클리어 해야 동적 컬럼 헤더 생성함


            CM_DefectCode cd = new CM_DefectCode();
            cd.ProcessID = LupProcess.EditValue.ConvertString_Null_To_Empty();

            DataTable dt = cd.Select_BadByProcess_OrderResult(serialNo);
            if (dt == null || dt.Rows.Count == 0) return;

			gc_Bad.DataSource = dt;
			gv_Bad.BestFitColumns();

			int colCnt = dt.Columns.Count;
			int colWid = (gc_Bad.Width - 10) / colCnt;

			for (int i = 0; i < colCnt; i++)
			{
				gv.Columns[i].Width = colWid;
			}
		}

		private void gv_Bad_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            //Bad_Cd 로우 숨김 처리

            int TargetRow = e.ListSourceRow;
            if (TargetRow == 3)
            {
                e.Visible = false;
                e.Handled = true;
            }
        }

        private void gv_Bad_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //불량수량 증가/감소

            ////셀 벨류 체인치 무한 로프 확인
            //if (!LoopFlag) return;
            GridView gv = sender as GridView;
            int TargetRow = e.RowHandle;

            if (Lbl_WorkQty.EditValue.ConvertInt_Null_To_Zero() == 0)
            {
                SetMessage_Qty("생산수량이 없습니다.");
            }

            string ColName = e.Column.ToString();

            var TargetCd = gv.GetListSourceRowCellValue(3, ColName).ToString();
            float TargetQty = gv.GetRowCellValue(0, ColName).ConvertFloat_Null_To_Zero();

            float workQty = Lbl_WorkQty.EditValue.ConvertFloat_Null_To_Zero();
            float ngQty = Lbl_NgQty.EditValue.ConvertFloat_Null_To_Zero();

            if (TargetRow == 0)
            {
                // 첫 번째 행의 열 개수를 구한뒤
                int columnCount = gv.GetDataRow(0).ItemArray.Length;

                int sum = 0;

                // 첫 번째 행의 모든 합을 구함
                for (int i = 0; i < columnCount; i++)
                {
                    // 해당 열의 데이터를 가져옵니다.
                    object data = gv.GetDataRow(0)[i];
                    int intdata = Convert.ToInt32(data);
                    sum += intdata;
                }

                //입련된 불량수량이 생산수량을 넘지않거나
                //현재까지 입력된 불량수량이 생산수량과 동일한경우 0이 아닌 경우에만 수정이 가능함
                if (sum < workQty || sum == workQty && TargetQty != 0)
                {
                    this.Tag = "Qty";
                    SetBadQty(ColName, sender, e);
                    float NewTargetQty = gv.GetRowCellValue(0, ColName).ConvertFloat_Null_To_Zero();

                    if (NewTargetQty <= workQty)
                    {
                        if ((sum - TargetQty) + NewTargetQty > workQty)
                        {
                            SetMessage_Qty("불량수량은 생산수량을 초과할 수 없습니다.");
                            gv.SetRowCellValue(0, ColName, TargetQty);
                        }
                        else
                        {
                            Lbl_NgQty.Text = Convert.ToString(Convert.ToInt16(Lbl_NgQty.EditValue) - TargetQty + NewTargetQty);
                        }
                    }
                    else if (NewTargetQty > workQty)
                    {
                        SetMessage_Qty("불량수량은 생산수량을 초과할 수 없습니다.");
                        gv.SetRowCellValue(0, ColName, TargetQty);
                    }
                    else
                    {
                        Lbl_NgQty.Text = Convert.ToString(workQty - TargetQty + NewTargetQty);
                    }



                    Lbl_GoodQty.Text = (workQty - ((sum - TargetQty) + NewTargetQty)).ConvertString_Null_To_Zero();
                }
                //0에서 추가적으로 숫자를 입력하려는 경우 막음
                else
                {
                    SetMessage_Qty("불량수량은 생산수량을 초과할 수 없습니다.");
                }

                if (Lbl_NgQty.Text.Trim().Equals(""))
                {
                    Lbl_NgQty.EditValue = 0;
                }
            }

			else if (TargetRow == 1) //증가
            {
				gv.SetRowCellValue(0, ColName, TargetQty + 1);
				Lbl_NgQty.EditValue = Lbl_NgQty.EditValue.ConvertFloat_Null_To_Zero() + 1;
				Lbl_GoodQty.EditValue = workQty - Lbl_NgQty.EditValue.ConvertFloat_Null_To_Zero();
			}
			else if (TargetRow == 2) //감소
            {
				if (TargetQty >= 1)
				{
					gv.SetRowCellValue(0, ColName, TargetQty - 1);
					Lbl_NgQty.EditValue = Lbl_NgQty.EditValue.ConvertFloat_Null_To_Zero() - 1;
					Lbl_GoodQty.EditValue = workQty - Lbl_NgQty.EditValue.ConvertFloat_Null_To_Zero();
				}
			}
		}

        private void SetBadQty(string filedName, object sender, EventArgs e)
        {
            //터치패드 입력

            string BtnTag = Tag.ToString();

            try
            {
                Frm_NumPad NumPad = new Frm_NumPad(BtnTag, 0);

                //터치패드로 입력한 값을 넣을 컨트롤명
                Target = filedName;

                //화면의 오른쪽 중앙 고정
                int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
                int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
                int x = (screenWidth - NumPad.Width) / 4;
                int y = (screenHeight - NumPad.Height) / 2;
                NumPad.StartPosition = FormStartPosition.Manual;
                NumPad.Location = new Point(x, y);

                //숫자입력
                NumPad.FormSendEvent += new Frm_NumPad.FormSendDataHandler(AddNum_cell);
                NumPad.ShowDialog();
            }
            catch { }
        }

        private void AddNum_cell(string _num)
        {
            //수량/값 터치패드 입력
            try
            {
                DataRow dr = gv_Bad.GetDataRow(0);
                int SaveFocusedRowInx = gv_Bad.FocusedRowHandle;

                gv_Bad.SetRowCellValue(SaveFocusedRowInx, Target, _num);
            }
            catch { }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button Btn = (Button)sender;

            string btnname = Btn.Name.ToString();

            Form frm = null;

            switch (btnname)
            {
                //지시조회
                case "Btn_CmdSearch":
                    CmdSearch();
                    break;

                //실적조회
                case "Btn_Search":
                    Search();
                    break;

                case "Btn_Save":
                    if (!ChkSave()) return;  //필수등록 확인

                    Save();
                    break;

                case "Btn_LabelPrint":
                    Print();
                    break;

                case "Btn_Start":
                    //지시
                    Start();
                    break;

                case "Btn_Back":
                    this.Close();
                    break;

                case "Btn_Work_Standard_Sheet":
                    Opensheet(Lbl_MaterialName.Tag.ConvertString_Null_To_Empty());
                    break;

            }
        }

        private void Start()
        {
            //작업시작 버튼 클릭
            if(lbl_EndDate.Text.Trim() == "" && TxtSubSeq.Text.ConvertString_Null_To_Empty() != "")
			{
                cls_MessageBox.Show("이미 시작된 작업입니다.\n생산내역 입력 후 저장하세요.");
                return;
			}

            //불량 그리드 클리어
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gv_Bad.Columns)
            {
                gv_Bad.SetRowCellValue(0, col, 0);
            }

            //등록 내역 클리어
            DtpWorkDate.EditValue = DateTime.Now.ToString("d");
            TxtWorkerID.EditValue = null;
            Lbl_WorkQty.EditValue = null;
            Lbl_GoodQty.EditValue = null;
            Lbl_NgQty.EditValue = null;
            lbl_EndDate.Text = "";
            lbl_EndDate.Tag = "";
            Lbl_workstatus.Text = "● 작업중";
            Lbl_workstatus.ForeColor = Color.SeaGreen;

            //시리얼번호 채번, max subseq 구하기, 실적PM_OrderResult 데이터 생성
            //OrderProgress 등록, PM_OrderProcess 생산,양,불,시작,종료일자 업데이트, PM_ProductPlan 업데이터(사내공정 마지막 공정일 경우 생산,양불 수량 업데이트)
            DtpWorkDate.EditValue = DateTime.Now.Date;
            DtLotNo.EditValue = DateTime.Now.Date;
            lbl_StartDate.Tag = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            lbl_StartDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");


            string serialNo = "SN" + code.GetAutoNumber("바코드", DateTime.Now);
            _SerialNo = serialNo;

            PM_OrderResult odr = new PM_OrderResult();
            odr.CorpId = WrGlobal.CorpID;
            odr.OrderNo = _OrderNo;
            odr.OrderSeq = _OrderSeq;

            _SubSeq = odr.GetMaxSubSeq() + 1;
            TxtSubSeq.Text = _SubSeq.ConvertString_Null_To_Zero();

            odr.SubSeq = _SubSeq;
            odr.ProcessCode = LupProcess.EditValue.ConvertString_Null_To_Empty();
            odr.MachineCode = LupMachine.EditValue.ConvertString_Null_To_Empty();
            odr.MaterialCode = _MaterialCode;
            odr.WorkerID = TxtWorkerID.EditValue.ConvertString_Null_To_Empty();
            odr.SerialNo = serialNo;
            odr.WorkDate = DtpWorkDate.DateTime.ToString("d");
            odr.StartDate = lbl_StartDate.Tag.ConvertString_Null_To_Empty();
            odr.EndDate = "";
            odr.WorkQty = Lbl_WorkQty.EditValue.ConvertInt_Null_To_Zero();
            odr.GoodQty = Lbl_GoodQty.EditValue.ConvertInt_Null_To_Zero();
            odr.NgQty = Lbl_NgQty.EditValue.ConvertInt_Null_To_Zero();
            odr.Result = "";
            odr.Ins_PM_OrderResult();

            //
            PM_OrderProcess op = new PM_OrderProcess();
            op.CorpId = WrGlobal.CorpID;
            op.OrderNo = _OrderNo;
            op.OrderSeq = _OrderSeq;
            op.Status = "작업중";
            op.Upd_Status();
        }

        private void CmdSearch()
        {
            //공정지시 조회 팝업창
            Frm_PM_OrderProcess_View ProcessCmdSerch = new Frm_PM_OrderProcess_View();

            ProcessCmdSerch.FormSendEvent += new Frm_PM_OrderProcess_View.FormSendDataHandler(SetResultData);

            ProcessCmdSerch.Size = new Size(this.Width - 20, this.Height - 20);
            ProcessCmdSerch.ShowDialog();
        }

        private void Search()
        {
            //실적 조회 팝업창
            Frm_PM_OrderResult_List ResultSerch = new Frm_PM_OrderResult_List(_OrderNo, _OrderSeq);

            ResultSerch.FormSendEvent += new Frm_PM_OrderResult_List.FormSendDataHandler(SetResultData);
            ResultSerch.Size = new Size(this.Width - 20, this.Height - 20);
            ResultSerch.FormClosed += ResultSerch_FormClosed;
            ResultSerch.ShowDialog();
        }

        private void ResultSerch_FormClosed(object sender, FormClosedEventArgs e)
        {
            PM_OrderResult odr = new PM_OrderResult();
            odr.OrderNo = _OrderNo;
            odr.OrderSeq = _OrderSeq;
            odr.SubSeq = _SubSeq;

            if (!odr.ExistsPM_OrderResult())
            //해당 실적 건이 삭제 된 경우 데이터 Clear
            {

                //수정 ~> 등록으로 변경
                EditProcessResult = false;
                //실적번호 초기화
                _SubSeq = 0;
                TxtSubSeq.Text = "";

                _SerialNo = "";

                DtpWorkDate.EditValue = "";
                DtLotNo.EditValue = "";

                TxtWorkerID.EditValue = "";
                Lbl_WorkQty.Text = "";
                Lbl_GoodQty.Text = "";
                Lbl_NgQty.Text = "";

                lbl_StartDate.Text = "";
                lbl_EndDate.Text = "";

                lbl_StartDate.Enabled = true;
                lbl_EndDate.Enabled = true;

                //Clear();
            }
        }

        private void SetResultData(DataRow dr)
        {
            Clear();
            try
            {
                _OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
                _OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();

                TxtOrderNo.Text = _OrderNo;

                //SubSeq가 있으면
                if(dr["SubSeq"].ConvertInt_Null_To_Zero() != 0)
				{
                    //수정대상 공정실적번호
                    _SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero();
                    _SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty();
                    TxtSubSeq.EditValue = _SubSeq;

                    EditProcessResult = true;
				}
                else
                    TxtSubSeq.EditValue = string.Empty;

                Lbl_MaterialCode.Text   = dr["ItemCode"].ConvertString_Null_To_Empty();
                Lbl_MaterialName.Text   = dr["ItemName"].ConvertString_Null_To_Empty();
                Lbl_Class1.Text         = dr["ClassName1"].ConvertString_Null_To_Empty();
                Lbl_Class2.Text         = dr["ClassName2"].ConvertString_Null_To_Empty();
                Lbl_Alias.Text          = dr["Alias"].ConvertString_Null_To_Empty();
                Lbl_Size.Text           = dr["Size"].ConvertString_Null_To_Empty();
                Lbl_ColorCode.Text      = dr["ColorCode"].ConvertString_Null_To_Empty();
                Lbl_UnitName.Text       = dr["UnitName"].ConvertString_Null_To_Empty();

                TxtWorkerID.EditValue       = dr["WorkerID"];
                DtLotNo.EditValue           = dr["LotNo"];
                Lbl_OrderQty.EditValue      = dr["OrderQty"].ConvertFloat_Null_To_Zero();
                Lbl_TotalWorkQty.EditValue  = dr["TotalWorkQty"].ConvertFloat_Null_To_Zero();
                Lbl_WorkQty.EditValue       = dr["WorkQty"].ConvertFloat_Null_To_Zero();
                Lbl_GoodQty.EditValue       = dr["GoodQty"].ConvertFloat_Null_To_Zero();
                Lbl_NgQty.EditValue         = dr["NgQty"].ConvertFloat_Null_To_Zero();

                DtpWorkDate.EditValue       = dr["WorkDate"];
                //시작시간
                if (dr["StartDate"].ToString() == null || dr["StartDate"].ToString() == "") { lbl_StartDate.Text = Convert.ToString(dr["StartDate"]); }
                else { lbl_StartDate.Text = (Convert.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd HH:mm")); lbl_StartDate.Tag = (Convert.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd HH:mm")); }
                //종료시간
                if (dr["EndDate"].ToString() == null || dr["EndDate"].ToString() == "") { lbl_EndDate.Text = Convert.ToString(dr["EndDate"]); }
                else { lbl_EndDate.Text = (Convert.ToDateTime(dr["EndDate"]).ToString("yyyy-MM-dd HH:mm")); lbl_EndDate.Tag = (Convert.ToDateTime(dr["EndDate"]).ToString("yyyy-MM-dd HH:mm")); }

                Lbl_OpRemark.Text = dr["OPRemark"].ConvertString_Null_To_Empty();

                //_OrderSeq = Convert.ToInt32(dr["ProcCmd_Row_Seq"]);
                //LupProcess.EditValue = dr["ProcessCode"].ToString();
                LupMachine.EditValue = dr["MachineCode"].ToString();
                _MaterialCode = dr["ItemCode"].ToString();
                _OrderQty = (dr["OrderQty"] != DBNull.Value) ? (float)Math.Truncate(Convert.ToDouble(dr["OrderQty"])) : 0;

                SearchBad(LupProcess.EditValue.ConvertString_Null_To_Empty(), _SerialNo);

                //상태 조회
                SetWorkStatus(_OrderNo, _OrderSeq, _SubSeq);

                //비가동 총 누적시간
                SetStopTime(_OrderNo, _OrderSeq);
                //CAPA대비 달성률 조회, 비가동 set 이후에
                SetLblCAPA();

            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message);
            }
        }


        private void SetWorkStatus(string orderNo, int orderSeq, int subSeq)
        {
            //상태: 작업중, 종료, 비가동
            PM_OrderResult odr = new PM_OrderResult();
            odr.CorpId = WrGlobal.CorpID;
            odr.OrderNo = orderNo;
            odr.OrderSeq = orderSeq;
            odr.SubSeq = subSeq;

            DataTable dt = odr.Select_PM_OrderResult_Status();
            if (dt.Rows.Count == 0)
            {
                Lbl_workstatus.Text = "● 대기";
                Lbl_workstatus.ForeColor = Color.Gray;
                return;
            }

            DataRow row = dt.Rows[0];
            string status = row["WorkStatus"].ConvertString_Null_To_Empty();

            switch (status)
            {
                case "작업종료":
                    Lbl_workstatus.Text = "● 작업종료";
                    Lbl_workstatus.ForeColor = Color.Red;
                    break;

                case "비가동":
                    Lbl_workstatus.Text = "● 비가동";
                    Lbl_workstatus.ForeColor = Color.DarkGray;
                    break;

                case "작업중":
                    Lbl_workstatus.Text = "● 작업중";
                    Lbl_workstatus.ForeColor = Color.SeaGreen;
                    break;

                case "대기":
                default:
                    Lbl_workstatus.Text = "● 대기";
                    Lbl_workstatus.ForeColor = Color.Gray;
                    break;
            }

            Lbl_rto.Text = ((Lbl_TotalWorkQty.Text.ConvertFloat_Null_To_Zero() / Lbl_OrderQty.Text.ConvertFloat_Null_To_Zero()) * 100).ToString("F2");
        }

        private void SetStopTime(string orderNo, int orderSeq)
        {
            //비가동 총 누적시간 조회
            string startTime = lbl_StartDate.Tag.ConvertString_Null_To_Empty();
            string endTime = lbl_EndDate.Tag.ConvertString_Null_To_Empty();

            PM_OrderResult odr = new PM_OrderResult();
            odr.CorpId = WrGlobal.CorpID;
            odr.OrderNo = orderNo;
            odr.OrderSeq = orderSeq;

            DataTable dt = odr.Select_PM_OrderResult_StopTime(startTime, endTime);
            DataRow row = dt.Rows[0];

            string stopTime = row["StopTime"].ConvertString_Null_To_Empty();
            Lbl_StopTime.Text = stopTime;
            Lbl_StopTime.Tag = row["StopTimeMM"].ConvertString_Null_To_Zero();
        }

        private void SetLblCAPA()
        {
            //CAPA 대비 달성률 계산 추후 카운터 들어오면 타이머 내부에 넣어야함
            if (_SubSeq == 0) return;
            EQ_Machine cm = new EQ_Machine();
            cm.MachineCode = LupMachine.EditValue.ConvertString_Null_To_Empty();

            DataRow drMachine = cm.Select_Machine_Info();

            if (drMachine != null)
            {
                DateTime curTime = DateTime.Now;
                DateTime startTime = new DateTime();
                DateTime endTime = new DateTime();

                if (!DateTime.TryParse(lbl_StartDate.Tag.ConvertString_Null_To_Empty(), out startTime))
                {
                    cls_MessageBox.Show("시작 시간 입력 이상");
                    return;
                }

                DateTime.TryParse(lbl_EndDate.Tag.ConvertString_Null_To_Empty(), out endTime);

                // 2. 총 경과시간 (분)
                double totalMinute = 0;

                if (lbl_EndDate.Text.Trim() == "")
                {
                    totalMinute = (curTime - startTime).TotalMinutes;
                }
                else
                {
                    totalMinute = (endTime - startTime).TotalMinutes;
                }

                if (totalMinute <= 0)
                {
                    lblCapaRate.Text = "0";
                    return;
                }

                // 3. 비가동시간 (분)
                double downMinute = Lbl_StopTime.Tag.ConvertDouble_Null_To_Zero();

                // 4. 실가동시간 (분)
                double runMinute = totalMinute - downMinute;
                if (runMinute <= 0)
                {
                    lblCapaRate.Text = "0";
                    return;
                }

                // 5. CAPA 요소
                double array = drMachine["ArrayValue"].ConvertDouble_Null_To_Zero();        // 배열
                double rpm = drMachine["RPM"].ConvertDouble_Null_To_Zero();               // 분당 생산량
                double yield = drMachine["ProductionYield"].ConvertDouble_Null_To_Zero();   // 수율 (0~1)

                if (array <= 0 || rpm <= 0 || yield <= 0)
                {
                    lblCapaRate.Text = "0";
                    return;
                }

                // 6. CAPA 계산 (양품 기준)
                double capaMinute = array * rpm * 1 * (yield * 0.01); //분당 capa
                double capa = runMinute *capaMinute;
                double totalWorkQty = Lbl_WorkQty.EditValue.ConvertDouble_Null_To_Zero();// + Lbl_WorkQty.EditValue.ConvertDouble_Null_To_Zero();

                // 7. 화면 표시
                LblStdCAPA.Text = capa.ToString("N0");
                lblCapaRate.Text = Math.Truncate(totalWorkQty / capa * 100).ToString("N0") + "%"; // 소수점 버림 (현업 선호)
            }
        }

        private void Save()
        {
            if(_SubSeq == 0)
			{
                cls_MessageBox.Show("저장 실패");
                return;
			}

            DateTime workDate = DtpWorkDate.DateTime;

            //실적 업데이트
            PM_OrderResult odr = new PM_OrderResult();
            odr.CorpId = WrGlobal.CorpID;
            odr.OrderNo = _OrderNo;
            odr.OrderSeq = _OrderSeq;
            odr.SubSeq = _SubSeq;
            odr.ProcessCode = LupProcess.EditValue.ConvertString_Null_To_Empty();
            odr.MachineCode = LupMachine.EditValue.ConvertString_Null_To_Empty();
            odr.MaterialCode = _MaterialCode;
            odr.WorkerID = TxtWorkerID.EditValue.ConvertString_Null_To_Empty();
            if (DtLotNo.IsNullOrEmpty())
                odr.LotNo = "";
            else
                odr.LotNo = DtLotNo.DateTime.ToString("d");
            odr.WorkDate = DtpWorkDate.DateTime.ToString("d");
            odr.StartDate = lbl_StartDate.Tag.ConvertString_Null_To_Empty();
            odr.EndDate = lbl_EndDate.Tag.ConvertString_Null_To_Empty();
            odr.WorkQty = Lbl_WorkQty.EditValue.ConvertInt_Null_To_Zero();
            odr.GoodQty = Lbl_GoodQty.EditValue.ConvertInt_Null_To_Zero();
            odr.NgQty = Lbl_NgQty.EditValue.ConvertInt_Null_To_Zero();
            odr.Result = "C";

            //하위 자재 재고 조회 후 실적 진행 유무 선택
            // DataTable dtStock = CheckMaterialStock(odr.OrderNo, odr.MaterialCode, odr.WorkQty);
            //
            // if (dtStock != null && dtStock.Rows.Count > 0)
            // {
            //     string msg = $"재고가 부족한 자재가 {dtStock.Rows.Count}종 있습니다.\n\n";
            //
            //     foreach (DataRow drStock in dtStock.Rows)
            //     {
            //         msg += $"{drStock["BomMaterialCode"]}  부족수량 : {drStock["LackQty"]}\n";
            //     }
            //
            //     msg += "\n생산을 진행하시겠습니까?";
            //
            //     DialogResult result = MessageBox.Show(msg, "재고부족", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //     if (result == DialogResult.No)
            //     {
            //         return;
            //     }
            // }

            odr.Upd_PM_OrderResult_POP();

            float orderQty = Lbl_OrderQty.EditValue.ConvertFloat_Null_To_Zero();
            float workQty = Lbl_WorkQty.EditValue.ConvertFloat_Null_To_Zero();
            float totalWorkQty = Lbl_TotalWorkQty.EditValue.ConvertFloat_Null_To_Zero() + workQty;

            //불량등록
            QM_WorkDefect wd = new QM_WorkDefect();
            wd.SerialNo = _SerialNo;
            wd.ExamType = "10"; //10:생산불량, 20ㅣ검사불량
            wd.Del_QM_WorkDefect_BySerialNo();

            DataTable dtDefect = gc_Bad.DataSource as DataTable;

            for (int col = 0; col < gv_Bad.Columns.Count; col++)
            {
                string defectCode = dtDefect.Rows[3][gv_Bad.Columns[col].FieldName].ConvertString_Null_To_Empty();
                string qty = dtDefect.Rows[0][gv_Bad.Columns[col].FieldName].ConvertString_Null_To_Zero();

                query = $@" EXEC [POP_WriteWorkDefect_NEW] '{WrGlobal.CorpID}', '{_SerialNo}', '{defectCode}', {qty}, '', '{WrGlobal.LoginID}' ";
                db.ExecuteQuery(query);
            }

            //PM_OrderProcess 업데이트
            PM_OrderProcess op = new PM_OrderProcess();
            op.CorpId = WrGlobal.CorpID;
            op.OrderNo = _OrderNo;
            op.OrderSeq = _OrderSeq;
            op.Upd_OrderProcess_Result();

            //PM_ProductPlan 업데이트
            PM_ProductPlan wo = new PM_ProductPlan();
            wo.CorpId = WrGlobal.CorpID;
            wo.OrderNo = _OrderNo;
            wo.Upd_PM_ProductPlanQty_POP();

            // 수불등록
            //기존 재고 등록 내역 삭제
            StockMaster stm = new StockMaster();
            ST_StockD std = new ST_StockD();
            std.JobNo = _OrderNo;
            std.JobSeq = _OrderSeq;
            std.JobSeq2 = _SubSeq;

            std.DeleteStockMD_Work();

            //생산재고 입고창고, 자재채고 출고창고 구하기
            query = $@"
                    SELECT TOP 1 

                        A.InHouseID
					    , (  
					    	SELECT HouseID 
					    	FROM CM_Item
					    	WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ItemCode = '{odr.MaterialCode}'
					    ) AS OutHouseID

                    FROM CM_Line A
                    LEFT JOIN CM_Process B ON a.CorpId = B.CorpId AND a.ProcessCode = B.ProcessCode
                    WHERE A.CorpId = '{WrGlobal.CorpID}'
                    AND A.ProcessCode = '{odr.ProcessCode}'
                    
            ";

            DataRow drLine = db.GetDataRow(query);

            //string fromHouse = drLine["InHouseID"].ConvertString_Null_To_Empty();   //하위자재 출고 창고
            //string toHouse = drLine["OutHouseID"].ConvertString_Null_To_Empty();    //생산 입고 창고


            string fromHouse  = "S0001";
            string toHouse = "S0002";

            if (fromHouse.IsNullOrEmpty()) fromHouse = "S0001";
            if (toHouse.IsNullOrEmpty()) toHouse = "S0002";

            //자재수불 + 자재출고 처리
            if (WrGlobal.ProcOption["PD"]["ProcResult_MatOut_AutoYN"] == "Y")
            {
                //기존 자재출고 데이터 삭제
                BM_SalesD sd = new BM_SalesD();
                sd.DeleteSalesMD_Work(_SerialNo);

                if (_OrderNo != "")   //지시있을 경우 SdRoute 기준으로 BOM 조회
                {
                    query = $@"
			                    select 
			                    bom.MItemCode,
			                    bom.ProcessCode,
			                    (bom.WorkQty/bom.CalcQty) * {odr.WorkQty} as NeedQty -- (BOM수량/기준수)* 총생산량
			                    from PM_SdRoute sd
			                    Left join PM_SdRoute bom on sd.CorpId = bom.CorpId and sd.OrderNo = bom.OrderNo and sd.ChildID = bom.ParentID 
			                    INNER join CM_Item cmM on cmM.ItemCode = bom.MItemCode
			                    where sd.CorpId = '{WrGlobal.CorpID}' and sd.OrderNo = '{_OrderNo}' and  sd.MItemCode = '{_MaterialCode}'
		                        and bom.MItemCode is not null;
                    ";
                }
                else //지시없을 경우 Bom 테이블 기준으로 조회
                {

                    query = $@"
                            WITH BOM_CTE AS (
                                SELECT 
                                      BOM.MItemCode
                                    , BOM.ProcessCode
                                    , BOM.ChildID
                                    , BOM.ParentID
                                    , (BOM.Qty / ISNULL(BOM.CalcQty, 1)) * {odr.WorkQty} AS NeedQty
                                FROM [FN_Bom] ('{WrGlobal.CorpID}','{_MaterialCode}') AS BOM
                            )
                            SELECT *
                            FROM BOM_CTE AA
                            INNER JOIN (
                                SELECT ChildID 
                                FROM BOM_CTE
                                WHERE BomMaterialcode = '{_MaterialCode}'
                            ) ROOT
                                ON AA.ParentID = ROOT.ChildID;
                            
                    ";
                }
                DataTable dtBom = db.GetDataTable(query);

                if (dtBom != null)
                {
                    if (dtBom.Rows.Count > 0)
                    {

                        BM_SalesM sm = new BM_SalesM();
                        sm.CorpId = WrGlobal.CorpID;
                        sm.SalesNo = "OG" + code.GetAutoNumber("출고", workDate);
                        sm.SalesDate = odr.WorkDate;
                        sm.OutType = "30"; //30:자재출고
                        sm.WriteDate = odr.WorkDate;
                        sm.InsertBM_SalesM();

                        foreach (DataRow item in dtBom.Rows)
                        {
                            string InOutNo = "ST" + code.GetAutoNumber("재고", workDate);
                            string MaterialCode = item["MItemCode"].ConvertString_Null_To_Empty();
                            decimal NeedQty = item["NeedQty"].ConvertDecimal_Null_To_Zero();
                            string WorkDate = odr.WorkDate;
                            string OrderNo = _OrderNo;
                            int OrderSeq = _OrderSeq;
                            int SubSeq = _SubSeq;
                            string SerialNo = _SerialNo;

                            MaterialOutByLotNo(InOutNo, sm.SalesNo, MaterialCode, NeedQty, WorkDate, fromHouse, OrderNo, OrderSeq, SubSeq, SerialNo);
                        }
                    }
                }
            }

            //생산재고 입고처리
            stm = new StockMaster();
            stm.InOutNo = "ST" + code.GetAutoNumber("재고", workDate);
            stm.OutDate = odr.WorkDate;
            stm.ToHouseCode = toHouse;
            stm.WriteID = WrGlobal.LoginID;
            stm.Ins_StockMaster();

            std = new ST_StockD();
            std.InOutNo = stm.InOutNo;
            std.Seq = 1;
            std.InOutDate = odr.WorkDate;
            std.ToHouseCode = toHouse;
            std.Type = "생산";
            std.MaterialCode = _MaterialCode;
            std.JobNo = _OrderNo;
            std.JobSeq = _OrderSeq;
            std.JobSeq2 = _SubSeq;
            std.Qty = odr.GoodQty.ConvertDecimal_Null_To_Zero();
            std.LotNo = _SerialNo;
            std.Status = "완료";
            std.Ins_ST_StockD();

            //상태, 비가동시간 업데이트
            SetWorkStatus(_OrderNo, _OrderSeq, _SubSeq);
            SetStopTime(_OrderNo, _OrderSeq);
            SetLblCAPA();

            cls_MessageBox.Show(cls_MessageBox.Save_Comp);
        }

        //public DataTable CheckMaterialStock(string orderNo, string materialCode, float workQty)
        //{
        //    query = $@"    SELECT
        //                        B.BomMaterialCode,
        //                        M.MaterialName,
        //                        B.NeedQty,
        //                        ISNULL(S.StockQty,0) StockQty,
        //                        B.NeedQty - ISNULL(S.StockQty,0) AS LackQty
        //                    FROM
        //                    (
        //                        SELECT
        //                            bom.BomMaterialcode,
        //                            (bom.WorkQty * 1.0 / bom.CalcQty) * {workQty} AS NeedQty
        //                        FROM SdRoute sd
        //                        LEFT JOIN SdRoute bom
        //                            ON sd.CorpId = bom.CorpId
        //                            AND sd.OrderNo = bom.OrderNo
        //                            AND sd.ChildID = bom.ParentID
        //                        WHERE sd.CorpId = '{WrGlobal.CorpID}'
        //                        AND sd.OrderNo = '{orderNo}'
        //                        AND sd.BomMaterialCode = '{materialCode}'
        //                        AND bom.BomMaterialcode IS NOT NULL
        //                    ) B
        //                    LEFT JOIN (
        //                        SELECT MaterialCode, SUM(CurrentQty) StockQty
        //                        FROM StockByLotNoView
        //                        GROUP BY MaterialCode
        //                    ) S
        //                    ON B.BomMaterialCode = S.MaterialCode
        //                    LEFT JOIN CM_Item M
        //                    ON B.BomMaterialCode = M.MaterialCode
        //                    WHERE ISNULL(S.StockQty,0) < B.NeedQty
        //                    ";

        //    return db.GetDataTable(query);
        //}

        public bool MaterialOutByLotNo(string InOutNo, string SalesNo, string MaterialCode, decimal NeedQty, string WorkDate, string fromHouse, string OrderNo, int OrderSeq, int SubSeq, string SerialNo)
        {
            // LotNo별 자재 자동출고 프로시저
            query = $@" EXEC MaterialOutByLotNo
                                                '{WrGlobal.CorpID}'
                                              , '{InOutNo}'
                                              , '{SalesNo}'
                                              , '{MaterialCode}'
                                              , '{NeedQty}'
                                              , '{WorkDate}'
                                              , '{fromHouse}'
                                              , '{OrderNo}'
                                              , '{OrderSeq}'
                                              , '{SubSeq}'
                                              , '{WrGlobal.LoginID}'
                                              , '{WrGlobal.ClientHostName}'
                                              , '{SerialNo}'
                                    ";

            return db.ExecuteQuery(query);
        }

        private bool ChkSave(string endCmd = "")
        {
            //필수등록 확인
            //무조건 작업시작후 작업순번 부여받은 후 종료/저장 해야함
            if(TxtSubSeq.EditValue.ConvertString_Null_To_Zero() == "0")
			{
                cls_MessageBox.Show("작업을 시작하지 않았습니다.\n[작업시작] 버튼을 먼저 눌러주세요.");
                return false;
			}

            if (Lbl_WorkQty.EditValue.ConvertString_Null_To_Zero() == "0")
            {
                cls_MessageBox.Show("생산수량 미입력");
                return false;
            }
            else if (TxtWorkerID.EditValue.ConvertString_Null_To_Empty() == "")
            {
				cls_MessageBox.Show("작업자 미입력");
				return false;
			}
            else if (lbl_EndDate.Text.Trim() == "")
            {
                cls_MessageBox.Show("종료시간 미입력");
                return false;
            }


            return true;
        }

        private void SetMessage_Qty(string message)
        {
            if (DialogResult.OK != cls_MessageBox.Show(message))
            {
                return;
            }
        }

        private void Clear()
        {
            try
            {
                //수정 ~> 등록으로 변경
                EditProcessResult = false;

                //실적번호 초기화
                _SubSeq = 0;
                _SerialNo = "";
                _OrderNo = "";
                _OrderSeq = 0;


                //상단
                Lbl_MaterialCode.Text = "";
                Lbl_MaterialName.Text = "";
                Lbl_Alias.Text = "";
                Lbl_Size.Text = "";
                Lbl_ColorCode.Text = "";
                TxtOrderNo.Text = "";
                Lbl_OpRemark.Text = "";

                Lbl_TotalWorkQty.Text = "";
                Lbl_WorkQty.Text = "0";
                Lbl_GoodQty.Text = "0";
                Lbl_NgQty.Text = "0";

                TxtWorkerID.EditValue = "";
                DtpWorkDate.EditValue = DateTime.Now.Date;

                lbl_StartDate.Text = "";
                lbl_EndDate.Text = "";

                //LupProcess.EditValue = "";
                LupMachine.EditValue = "";

                lbl_StartDate.Enabled = true;
                lbl_EndDate.Enabled = true;

            }
            catch { }
        }

        private void Print()
        {
            //PD_ProcessResult Result = new PD_ProcessResult();
            //DataTable dt = Result.Select_ProcessResult(WrGlobal.CorpID, _OrderNo, _Rst_Row_Seq);

            //if (Database.DbName.ToUpper().Contains("KUMHO_ANSAN"))
            //{
            //    Rpt_Process_Result_KHANSAN report = new Rpt_Process_Result_KHANSAN(dt);
            //    ReportPrintTool reportTool = new ReportPrintTool(report);

            //    reportTool.PrinterSettings.Collate = false;
            //    reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            //    reportTool.PreviewForm.PrintControl.ResumeLayout();

            //    reportTool.ShowPreviewDialog();
            //}
            //else
            //{
            //    Rpt_Process_Result report = new Rpt_Process_Result(dt);
            //    ReportPrintTool reportTool = new ReportPrintTool(report);

            //    reportTool.PrinterSettings.Collate = false;
            //    reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            //    reportTool.PreviewForm.PrintControl.ResumeLayout();

            //    reportTool.ShowPreviewDialog();
            //}
        }

        private void Opensheet(string item_Cd)
        {
            if (item_Cd == null || item_Cd == "") return;
            /*
			 * 프로세스 옵션
			 * 제품 사양서 조회(I: 이미지, P: PDF)
			 */

            if (WrGlobal.ProcOption["BS"]["Work_Standard_Sheed_IP"] == "P")
            {
                Frm_PDFViewer_New frm = new Frm_PDFViewer_New(item_Cd, false);
                frm.ShowDialog();
            }
            else
            {
                //이미지 조회
                Frm_PictureEdit frmPictureEdit = new Frm_PictureEdit(item_Cd, "ItemImg", false);
                frmPictureEdit.ShowDialog();
            }
        }

        private void Set_DateTimePad(object sender, EventArgs e)
        {
            LabelControl lb = sender as LabelControl;
            string filedName = lb.Name;
            string date = lb.Tag.ConvertString_Null_To_Empty(); //팝업 시 키패드에 띄우는 날짜

            //날짜+시간 입력패드
            string title = filedName == "lbl_StartDate" ? "시작 시간" : "종료 시간";
            Frm_DateTime_POP frm = new Frm_DateTime_POP(date, title);

            // 화면의 왼쪽 중앙 고정
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int x = (screenWidth - frm.Width) / 4;
            int y = (screenHeight - frm.Height) / 2;

            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(x, y);
            DialogResult dialogResult = frm.ShowDialog();

            if (dialogResult != DialogResult.OK)
                return;

            Tuple<string, string> result = frm.resultDateTime();
            if (filedName == "lbl_StartDate")
            {
                //item1: 일자,시간 item2:시간
                lbl_StartDate.Tag = result.Item1;   //실제 가져갈 값
                lbl_StartDate.Text = result.Item1;  //보여지는 값
            }
            else if (filedName == "lbl_EndDate")
            {
                //item1: 일자,시간 item2:시간
                lbl_EndDate.Tag = result.Item1;      //실제 가져갈 값
                lbl_EndDate.Text = result.Item1;     //보여지는 값
            }
        }


        private void Lbl_Qty_Click(object sender, EventArgs e)
        {
            try
            {
                //시, 분 터치패드 입력
                TextEdit Btn = (TextEdit)sender;

                string BtnTag = Btn.Tag.ToString();

                try
                {
                    Frm_NumPad NumPad = new Frm_NumPad(BtnTag, 0);

                    TextEdit NumEdit = (TextEdit)sender;

                    //터치패드로 입력한 값을 넣을 컨트롤명
                    Target = NumEdit.Name;

                    // 방법1: 화면의 오른쪽 중앙 고정
                    int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
                    int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
                    int x = (screenWidth - NumPad.Width) / 1;
                    int y = (screenHeight - NumPad.Height) / 2;

                    NumPad.StartPosition = FormStartPosition.Manual;
                    NumPad.Location = new Point(x, y);

                    NumPad.FormSendEvent += new Frm_NumPad.FormSendDataHandler(AddNum_label);

                    NumPad.ShowDialog();
                }
                catch { }

            }
            catch (Exception ex)
            {
            }
        }

        private void AddNum_label(string num)
        {
            //시, 분 터치패드 입력
            try
            {

                var target = this.Controls.Find(Target, true).FirstOrDefault();
                string tagetTag = (target.Tag).ConvertString_Null_To_Zero();

                if (tagetTag == "Qty")
                {
                    int wQty = Convert.ToInt32(Lbl_WorkQty.EditValue);
                    target.Text = num;
                    int bQty = Convert.ToInt32(Lbl_NgQty.EditValue.ConvertString_Null_To_Zero());

                    if (target.Name == "Lbl_WorkQty")
                    {
                        Lbl_GoodQty.Text = (Convert.ToInt32(num) - bQty).ToString();
                        Lbl_WorkQty.Text = Lbl_WorkQty.EditValue.ToString();

                        SetLblCAPA();
                    }

                    //불량유형이 아닌 불량수량 직접 입력 시 실행(이벤트 설정)
                    if (target.Name == "Lbl_NgQty")
                    {
                        int Int_qty = Convert.ToInt32(num.ConvertString_Null_To_Zero());
                        Lbl_GoodQty.EditValue = (wQty - Int_qty).ConvertString_Null_To_Zero();
                    }
                }
                else
                {
                    //입력 숫자가 3자리일 경우 시간단위의 앞자리에 0을 추가
                    string numTime = num.PadLeft(4, '0');
                    target.Text = numTime;
                }
            }
            catch { }
        }


		private void btnDownTime_Click(object sender, EventArgs e)
		{
            Frm_EQ_Operation_Btn frm = new Frm_EQ_Operation_Btn(_OrderNo, _OrderSeq, TxtSubSeq.EditValue.ConvertInt_Null_To_Zero(), LupMachine.EditValue.ConvertString_Null_To_Empty());
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            SetWorkStatus(_OrderNo, _OrderSeq, TxtSubSeq.EditValue.ConvertInt_Null_To_Zero());
            SetStopTime(_OrderNo, _OrderSeq);
            SetLblCAPA();
        }

		private void LupWorkerID_Click(object sender, EventArgs e)
		{
            if(TxtSubSeq.EditValue.ConvertString_Null_To_Zero() == "0")
			{
                cls_MessageBox.Show("현재 작업이 시작되지 않았습니다.\n[작업 시작] 버튼을 클릭한 후 작업자를 등록해주세요.", "작업자 선택");
                return;
			}

            DataTable dt = new DataTable();
            dt.Columns.Add("OrderNo", typeof(string));
            dt.Columns.Add("OrderSeq", typeof(string));
            dt.Columns.Add("SubSeq", typeof(string));
			dt.Columns.Add("LineCode", typeof(string));
			dt.Columns.Add("ProcessCode", typeof(string));
			//dt.Columns.Add("Machine_Cd", typeof(string));

			dt.Rows.Add(this._OrderNo, this._OrderSeq, this._SubSeq, "", _ProcessCode);

            if (this.ParentForm is Frm_Main)
            {
                Frm_Main main = this.ParentForm as Frm_Main;

                //폼 열려 있는지 확인
                foreach (var m_form in main.MdiChildren)
                {
                    //Form명 비교
                    if (m_form.Name.Equals("Frm_CM_LineProcessWorker"))
                    {
                        m_form.Focus();
                        return;
                    }
                }

                DataRow dr = dt.Rows[0];

                //안열려있으면 열기
                Frm_CM_LineProcessWorker manage = new Frm_CM_LineProcessWorker(dr);
                manage.Text = "실적 작업자등록";

                //Form 대제목 설정
                Control[] controls = manage.Controls.Find("LblMainTitle", true);

                if (controls.Length > 0)
                {
                    controls[0].Text = manage.Text;
                }

                //열기
                manage.FormSendEvent += new Frm_CM_LineProcessWorker.FormSendDataHandler(SetWorker);
                manage.FormClosed += Manage_FormClosed;

                manage.Show();

                int wer = 0;
            }
        }

        private void Manage_FormClosed(object sender, FormClosedEventArgs e)
        {
            PM_OrderResultWorker wm = new PM_OrderResultWorker();

            wm.OrderNo = _OrderNo;
            wm.OrderSeq = _OrderSeq;
            wm.SubSeq = _SubSeq;

            DataRow dr = wm.Select_Workers_Nm();

            if (dr != null)
            {
                TxtWorkerID.EditValue = dr["WorkerName"];
            }
        }

        private void SetWorker(Dictionary<string, PM_OrderResultWorker> workers)
        {
            //DicWorkers.Clear();

            string txt_emp = "";

            foreach (KeyValuePair<string, PM_OrderResultWorker> item in workers)
            {
                PM_OrderResultWorker pD_Worker = item.Value;
                txt_emp += pD_Worker.WorkerName + ", ";
            }

            if (txt_emp != "")
            {
                txt_emp = txt_emp.Substring(0, txt_emp.Length - 2);
            }

            TxtWorkerID.Text = txt_emp;

        }

        private void Lbl_NgQty_EditValueChanged(object sender, EventArgs e)
        {
            if (Lbl_NgQty.EditValue.ConvertInt_Null_To_Zero() == 0)
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gv_Bad.Columns)
                {
                    gv_Bad.SetRowCellValue(0, col, 0);
                }
            }
        }
           
		private void BtnMatInput_Click(object sender, EventArgs e)
		{
            //Frm_POP_PM_OrderResult_InputLOT f = new Frm_POP_PM_OrderResult_InputLOT(this._SerialNo);
            //f.Show();
		}
    }
}
