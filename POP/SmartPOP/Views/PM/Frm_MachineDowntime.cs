using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.EQ;
using SmartPOP.Views.POPUP;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SmartPOP.Views.PM
{
    public partial class Frm_MachineDowntime : DevExpress.XtraEditors.XtraForm
    {
        private string orderNo;
        private int orderSeq;
        private int SubSeq;
        private string processCode;
        private string lineCode;
        private string MachineCode;

        private string query = string.Empty;

        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        Database db = new Database();

        clsCode code = new clsCode();

        public Frm_MachineDowntime()
        {
            InitializeComponent();
        }

        public Frm_MachineDowntime(string orderNo, int seq)
        {
            InitializeComponent();

            this.orderNo = orderNo;
            this.orderSeq = seq;

            TxtOrderNo.Text = orderNo;
            TxtOrderSeq.EditValue = orderSeq;
            layout_SubSeq.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            BtnSelect.Visible = false;
        }

        public Frm_MachineDowntime(string orderNo, int seq, int subSeq, string machineCode)
        {
            InitializeComponent();

            this.orderNo = orderNo;
            this.orderSeq = seq;
            this.SubSeq = subSeq;
            this.MachineCode = machineCode;


            TxtOrderNo.Text = orderNo;
            TxtOrderSeq.EditValue = orderSeq;
            TxtSubSeq.EditValue = SubSeq;

            BtnSelect.Visible = false;
        }

        private void frmSelectPM_ProductPlan_Load(object sender, EventArgs e)
        {
            SetGrid();

            Set_Info();

            DisplayOper();
        }

        #region 디자인
        private void SetGrid()
        {
            GridView gv = GvOperation;

            gv.IndicatorWidth = 50;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lupOper = rpo.Create_RepositoryItemSearchLookUp(code.GetOperationCode(), "코드", "코드명");
            lupOper.View.OptionsView.ShowAutoFilterRow = true;  // Lup 검색설정

            rpo.Set_POP_SearchLup(lupOper);

            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            lupMachine.View.OptionsView.ShowAutoFilterRow = true;  // Lup 검색설정
            rpo.Set_POP_SearchLup(lupMachine);
            // 날짜
            RepositoryItemDateEdit dtOper = new RepositoryItemDateEdit();
            rpo.Set_POP_Dtp(dtOper);
            dtOper.Mask.EditMask = "yyyy-MM-dd";
            dtOper.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dtOper.UseMaskAsDisplayFormat = true;

            // 시작, 종료
            RepositoryItemTimeEdit timeEdit = new RepositoryItemTimeEdit();
            timeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            timeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm";
            timeEdit.TimeEditStyle = TimeEditStyle.TouchUI;

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "비가동일자", "WorkDay", dtOper);
            grid.Create_GvColumn(gv, "비가동코드", "ClassItemCode", lupOper);
            grid.Create_GvColumn(gv, "시작시간", "StartTime", timeEdit);
            grid.Create_GvColumn(gv, "종료시간", "EndTime", timeEdit);

            //
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // 수정여부 설정
            grid.Column_AllowEdit(gv.Columns["WorkDay"]);
            grid.Column_AllowEdit(gv.Columns["ClassItemCode"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);


            //이벤트 설정
            grid.SetPOPGrid(gv);
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.RowCellClick += Set_DateTimePad; //시간터치패드
        }

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

            MachineCode = MachineCode == string.Empty ? dr["MachineCode"].ToString() : MachineCode;
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
            string date = string.Empty;
            if (drChanged[fieldName].ToString() == string.Empty)
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
                if (newDate.Trim() == string.Empty) return;

                gv.SetFocusedRowCellValue(fieldName, newDate);
            }
        }

        #endregion


        private void DisplayOper()
        {
            EQ_Operation mo = new EQ_Operation();
            mo.OrderNo = orderNo;
            mo.OrderSeq = orderSeq;

            GcOperation.DataSource = mo.SelectMachineOP();

            GvOperation.BestFitColumns();
        }

        #region 행추가, 삭제, 저장


        private void BtnRowEdit_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("WorkDay", DateTime.Now.ToString("d"));
                dic.Add("MachineCode", this.MachineCode);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(GvOperation.GetFocusedRowCellValue("Status")) == "삭제" ? string.Empty : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(GcOperation, dic, txt_Act);

            GvOperation.BestFitColumns();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            OperSave();
        }

        private void OperSave()
        {
            //작성내역 저장
            DataTable dt = GcOperation.DataSource as DataTable;

            if (!Chk_Oper(dt)) return;

            //1. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                //else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                //{
                //    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                //}
            }

            cls_MessageBox.Show("저장이 완료되었습니다.");

            DisplayOper();
        }

        private bool Chk_Oper(DataTable dt)
        {
            if (dt.Select("WorkDay IS NULL").Length > 0)
            {
                cls_MessageBox.Show("비가동일자를 입력하세요.");
                return false;
            }

            if (dt.Select("StartTime IS NULL").Length > 0)
            {
                cls_MessageBox.Show("비가동 시작시간을 입력하세요.");
                return false;
            }

            //if (dt.Select("ItemCode IS NULL").Length > 0)
            //{
            //    cls_MessageBox.Show("비가동 코드를 입력하세요.");
            //    return false;
            //}

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            if (div == 'I')
            {
                if (!InsOper(dr))
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'U')
            {
                if (!UpdOper(dr))
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            //else if (div == 'D')
            //{ 
            //    if(!DelOper(dr))
            //    {
            //        MessageBox.Show("저장 중 에러가 발생하였습니다.");
            //        return false;
            //    }
            //}

            dr[0] = string.Empty;

            return true;
        }

        //추가
        private bool InsOper(DataRow dr)
        {
            string startTimeStr = Convert.ToDateTime(dr["StartTime"]).ToString("yyyy-MM-dd HH:mm");
            string endTimeStr = dr["EndTime"].ConvertString_Null_To_Empty() == string.Empty ? null : Convert.ToDateTime(dr["EndTime"]).ToString("yyyy-MM-dd HH:mm");

            // 필수 파싱용 DateTime
            DateTime startTime = Convert.ToDateTime(startTimeStr);
            DateTime? endTime = endTimeStr == null ? (DateTime?)null : Convert.ToDateTime(endTimeStr);

            string insertStart = startTimeStr;   // 기본값: 정상 순서
            string insertEnd = endTimeStr;       // 기본값: 정상 순서
            double stopTime = 0;

            // 종료시간이 있는 경우에만 StopTime 계산
            if (endTime != null)
            {
                // 시간 역전 시 Start/End 교체
                if (startTime > endTime.Value)
                {
                    insertStart = endTimeStr;       // 빠른 시간
                    insertEnd = startTimeStr;       // 늦은 시간
                    stopTime = startTime.Subtract(endTime.Value).TotalMinutes;
                }
                else
                {
                    stopTime = endTime.Value.Subtract(startTime).TotalMinutes;
                }
            }

            // 종료시간이 없는 경우 (작업 시작만 등록)
            if (endTime == null)
            {
                insertEnd = string.Empty;
                stopTime = 0;
            }

            string workDay = dr["WorkDay"].ConvertDate_To_Char8();

            query = $@"
            INSERT INTO EQ_Operation (
                CorpId, WorkDay, ProcessCode, LineCode,
                Seq, MachineCode, OperationYn, ClassItemCode,
                StartTime, EndTime, StopTime,
                Remark, OperationMeasure,
                OrderNo, OrderSeq,
                CreId, CreIP, CreDt
            )
            VALUES (
                '{WrGlobal.CorpID}', '{workDay}',
                '{processCode}', '{lineCode}',
                (
                    SELECT ISNULL(MAX(Seq), 0) + 1
                    FROM EQ_Operation
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND WorkDay     = '{workDay}'
                    AND ProcessCode = '{processCode}'
                    AND LineCode    = '{lineCode}'
                ),
                '{MachineCode}',
                'N', '{dr["ClassItemCode"]}',
                '{insertStart}', {insertEnd.ConvertStr_Empty_To_Null()}, {stopTime.ConvertNumeric_Empty_To_Null()},
                '{dr["Remark"]}', '{dr["OperationMeasure"]}',
                '{orderNo}', '{orderSeq}',
                '{WrGlobal.LoginID}', HOST_NAME(), GETDATE()
            );
        ";

            return db.ExecuteQuery(query);
        }

        //수정
        private bool UpdOper(DataRow dr)
        {
            string startStr = Convert.ToDateTime(dr["StartTime"]).ToString("yyyy-MM-dd HH:mm");
            string endStr = dr["EndTime"].ConvertString_Null_To_Empty() == string.Empty ? null : Convert.ToDateTime(dr["EndTime"]).ToString("yyyy-MM-dd HH:mm");

            // DateTime 변환
            DateTime startTime = Convert.ToDateTime(startStr);
            DateTime? endTime = endStr == null ? (DateTime?)null : Convert.ToDateTime(endStr);

            // 기본값 설정
            string insertStart = startStr;
            string insertEnd = endStr;
            double stopTime = 0;

            // 종료시간이 있는 경우만 StopTime 계산
            if (endTime != null)
            {
                if (startTime > endTime.Value)
                {
                    // 역전이면 순서 바꿔 저장
                    insertStart = endStr;
                    insertEnd = startStr;
                    stopTime = startTime.Subtract(endTime.Value).TotalMinutes;
                }
                else
                {
                    stopTime = endTime.Value.Subtract(startTime).TotalMinutes;
                }
            }

            // 종료시간 있으면 포함, 없으면 NULL로 처리
            if (endTime == null)
            {
                insertEnd = string.Empty;
                stopTime = 0;
            }

            string workDay = dr["WorkDay"].ConvertDate_To_Char8();

            query = $@"
                UPDATE EQ_Operation
                SET  WorkDay = '{workDay}',
                     ClassItemCode = '{dr["ClassItemCode"]}',
                     StartTime = '{insertStart}',
                     EndTime = {insertEnd.ConvertStr_Empty_To_Null()},
                     StopTime = {stopTime.ConvertNumeric_Empty_To_Null()},
                     Remark = '{dr["Remark"]}',
                     OperationMeasure = '{dr["OperationMeasure"]}'
                WHERE CorpId = '{WrGlobal.CorpID}'
                    AND WorkDay     = '{workDay}'
                    AND ProcessCode = '{processCode}'
                    AND LineCode    = '{lineCode}'
                    AND Seq = '{dr["Seq"].ConvertInt_Null_To_Zero()}'
            ";

            return db.ExecuteQuery(query);
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            DataRow dr = GvOperation.GetFocusedDataRow();
            if (dr == null) return;

            if (cls_MessageBox.Show("선택한 내역을 삭제하시겠습니까?", "비가동 삭제", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            EQ_Operation op = new EQ_Operation();
            op.CorpId = WrGlobal.CorpID;
            op.WorkDay = dr["WorkDay"].ConvertDate_To_Char8();
            op.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
            op.LineCode = dr["LineCode"].ConvertString_Null_To_Empty();
            op.Seq = dr["Seq"].ConvertInt_Null_To_Zero();

            if (op.Del_MachineOP())
            {
                cls_MessageBox.Show("삭제가 완료되었습니다.");
                DisplayOper();
            }
        }

        #endregion

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            string empNo = WrGlobal.LoginID;

            Frm_PM_OrderProcess_List frm = new Frm_PM_OrderProcess_List(empNo);
            frm.ShowDialog();
        }

        private void BtnOPStart_Click(object sender, EventArgs e)
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
            mo.ItemCode = string.Empty; //일단 비가동코드 빈칸으로 등록
            mo.StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//dr["StartTime"].ConvertString_Null_To_Empty();

            mo.Ins_MachineOP();

            DisplayOper();
        }

        private void BtnOPEnd_Click(object sender, EventArgs e)
        {
            DataRow dr = GvOperation.GetFocusedDataRow();
            if (dr == null) return;

            GvOperation.FocusInvalidRow();

            EQ_Operation mo = new EQ_Operation();
            mo.CorpId = WrGlobal.CorpID;
            mo.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
            mo.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
            mo.WorkDay = dr["WorkDay"].ConvertDate_To_Char8();
            mo.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
            mo.LineCode = dr["LineCode"].ConvertString_Null_To_Empty();
            mo.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
            mo.ItemCode = dr["ClassItemCode"].ConvertString_Null_To_Empty();
            mo.EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//dr["EndTime"].ConvertString_Null_To_Empty();

            DateTime start = Convert.ToDateTime(dr["StartTime"]);
            DateTime end = Convert.ToDateTime(mo.EndTime);

            TimeSpan diff = end - start;
            int stopTime = (int)diff.TotalMinutes;

            mo.StopTime = stopTime;

            mo.Upd_MachineOP_EndTime();

            DisplayOper();

            cls_MessageBox.Show("비가동 종료");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DisplayOper();
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}