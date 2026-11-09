using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using SmartPOP.Model.EQ;
using SmartPOP.Models;
using SmartPOP.View.Lookup;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.EQ
{
    public partial class Frm_EQ_Operation_View : DevExpress.XtraEditors.XtraForm
    {
        private string orderNo;
        private int orderSeq;
        private string processCode;
        private string processName;
        private string lineCode;
        private string lineName;
        private string machineCode;
        private string machineName;

        private string query = "";
        
        cls_Grid grid = new cls_Grid();
        Database db = new Database();
        cls_Repository rpo = new cls_Repository();

        clsCode code = new clsCode();

        public Frm_EQ_Operation_View()
        {
            InitializeComponent();
            Dt_Date.smtStartDateTimeValue = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            Dt_Date.smtEndDateTimeValue = DateTime.Now.Date;

            //rpo.Set_POP_Dtp(dateWorkStart);
            //rpo.Set_POP_Dtp(dateWorkEnd);
            SetDesign();
        }

        private void frmSelectPM_ProductPlan_Load(object sender, EventArgs e)
        {
            DisplayOper();
        }

        #region 디자인 아이템 생성 
        private void SetDesign()
        {
            //DtpInspDate.EditValue = DateTime.Now;

            //GridView 설정
            SetGrid(GvOperation);
            Set_Lup();
            Set_Info();
        }

        #region GridView 세팅
        private void SetGrid(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.RowAutoHeight = true; 

            cls_Repository rpo = new cls_Repository();
            RepositoryItemSearchLookUpEdit lupOper = rpo.Create_RepositoryItemSearchLookUp(code.GetOperationCode(), "코드", "코드명");
            
            // 날짜
            RepositoryItemDateEdit dtOper = new RepositoryItemDateEdit();
            dtOper.Mask.EditMask = "yyyy-MM-dd";
            dtOper.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dtOper.UseMaskAsDisplayFormat = true;

            // 시작, 종료
            RepositoryItemDateEdit dtStart = new RepositoryItemDateEdit();
            dtStart.Mask.EditMask = "HH:mm";
            dtStart.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dtStart.UseMaskAsDisplayFormat = true;
            //dtStart.ShowToday = false;
            //dtStart.ShowClear = false;
            dtStart.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True; // 시간선택가능

            RepositoryItemDateEdit dtEnd = new RepositoryItemDateEdit();
            dtEnd.Mask.EditMask = "HH:mm";
            dtEnd.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dtEnd.UseMaskAsDisplayFormat = true;
            //dtEnd.ShowToday = false;
            //dtEnd.ShowClear = false;
            dtEnd.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;   // 시간선택가능
            

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "공정명","ProcessName");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");
            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, " ", "Seq");
            grid.Create_GvColumn(gv, "비가동일자", "WorkDay", dtOper);
            grid.Create_GvColumn(gv, "비가동코드", "ItemCode", lupOper);
            grid.Create_GvColumn(gv, "시작시간", "StartTime", dtStart);
            grid.Create_GvColumn(gv, "종료시간", "EndTime", dtEnd);
            grid.Create_GvColumn(gv, "비가동사유", "Remark", rpo.Create_rpoMemo());
            grid.Create_GvColumn(gv, "조치내용", "OperationMeasure", rpo.Create_rpoMemo());
            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업지시순번", "OrderSeq");

            //// 모든 컬럼 수정 불가로 설정
            //foreach(GridColumn col in gv.Columns)
            //{
            //    col.OptionsColumn.AllowEdit = false;
            //}

            gv.Columns["Remark"].MinWidth = 200;
            gv.Columns["OperationMeasure"].MinWidth = 200;

            // 수정여부 설정
            gv.Columns["WorkDay"].OptionsColumn.AllowEdit = true;
            gv.Columns["ItemCode"].OptionsColumn.AllowEdit = true;
            gv.Columns["StartTime"].OptionsColumn.AllowEdit = true;
            gv.Columns["EndTime"].OptionsColumn.AllowEdit = true;
            gv.Columns["Remark"].OptionsColumn.AllowEdit = true;
            gv.Columns["OperationMeasure"].OptionsColumn.AllowEdit = true;

            // Lup 검색설정
            lupOper.View.OptionsView.ShowAutoFilterRow = true;

            // Visible 설정
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
            gv.Columns["MachineCode"].Visible = false;

            //이벤트 설정
            grid.SetPOPGrid(gv);
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            gv.CustomRowCellEdit += GvOperation_CustomRowCellEdit; 
        }
        #endregion GridView 세팅 END

        private void Set_Info()
        {
            string MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            query = $@"SELECT 
				            CL.ProcessCode,
				            CP.ProcessName,
				            CL.LineCode,
				            CL.LineName,
				            CL.MachineCode,
				            CM.MachineName
                    FROM CM_Line CL
                    INNER JOIN CM_Process CP ON CL.ProcessCode = CP.ProcessCode AND CL.CorpId = CP.CorpId
                     LEFT JOIN EQ_Machine CM ON CL.MachineCode = CM.MachineCode AND CL.CorpId = CM.CorpId
                    WHERE CP.CorpId = '{WrGlobal.CorpID}'
                      AND CL.MachineCode = '{MachineCode}'";

            DataRow dr = db.GetDataRow(query);
            if (dr != null)
            {
                processCode = dr["ProcessCode"].ToString();
                processName = dr["ProcessName"].ToString();
                lineCode = dr["LineCode"].ToString();
                lineName = dr["LineName"].ToString();
                machineCode = dr["MachineCode"].ToString();
                machineName = dr["MachineName"].ToString();
            }
        }

        private void Set_Lup()
        {
            cls_Lookup clsLup = new cls_Lookup();

            // 1. 조회조건 : 설비
            EQ_MachineChk machine = new EQ_MachineChk();
            DataTable dt_Machine = machine.Select_Machine_Lup();
            Lup_Machine.smtLookUpDataSource = dt_Machine;
            Lup_Machine.smtLookUpDataDisplayMember = "명칭";
            Lup_Machine.smtLookUpDataValueMember = "코드";
            Lup_Machine.smtLookUpValue = dt_Machine.Rows[0]["코드"].ConvertString_Null_To_Empty();
            Lup_Machine.smtLookUpEditValueChanged += Lup_EditValueChanged;

            //LupMachine.Properties.ShowClearButton = false;
            //LupMachine.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            //LupMachine.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정
        }

        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit Lup = sender as SearchLookUpEdit;
            DisplayOper();
        }
        #endregion 


        #region 조회
        private void DisplayOper()
        {
            string MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            var OperationStartDate = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue);
            var OperationEndDate = Convert.ToDateTime(Dt_Date.smtEndDateTimeValue);

            query = $@"SELECT '' Status
                            , MO.ProcessCode
							, CP.ProcessName
							, MO.LineCode
							, CL.LineName
                            , MO.MachineCode
							, CM.MachineName
                            , MO.Seq
		                    , CONVERT(DATETIME, MO.WorkDay, 112) WorkDay
		                    , MO.ItemCode
		                    , MO.StartTime
		                    , MO.EndTime
		                    , MO.Remark
		                    , MO.OperationMeasure
							, CASE 
								 WHEN ISNULL(MO.OrderNo, '') = '' THEN '설비 직접 등록 비가동'
								 ELSE MO.OrderNo 
							  END AS OrderNo
							, MO.OrderSeq 
                        FROM EQ_Operation MO
			      INNER JOIN CM_Process CP ON MO.ProcessCode = CP.ProcessCode -- AND CP.WorkType = '10' -- 10:사내, 20:외주
                  INNER JOIN CM_Line CL ON MO.LineCode = CL.LineCode 
			      INNER JOIN EQ_Machine CM ON MO.MachineCode = CM.MachineCode 
                        WHERE MO.CorpId = '{WrGlobal.CorpID}'
                          AND MO.MachineCode = '{MachineCode}'
                          AND MO.WorkDay BETWEEN '{OperationStartDate:yyyyMMdd}' AND '{OperationEndDate:yyyyMMdd}'    
                        ORDER BY WorkDay DESC
                    ";

            GcOperation.DataSource = db.GetDataTable(query);
            GvOperation.BestFitColumns();
        }
        #endregion

        #region 행추가, 삭제, 저장
        private void OperSave()
        {
            if (cls_MessageBox.Show("비가동 내역을 저장하시겠습니까?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

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
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            cls_MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            DisplayOper();
        }

        private bool Chk_Oper(DataTable dt)
        {
            if (dt.Select("WorkDay IS NULL").Length > 0)
            {
                cls_MessageBox.Show("등록일자를 선택해주세요.");
                return false;
            }

            if (dt.Select("StartTime IS NULL").Length > 0 || dt.Select("EndTime IS NULL").Length > 0)
            {
                cls_MessageBox.Show("비가동 시간을 선택해주세요.");
                return false;
            }

            if (dt.Select("ItemCode IS NULL").Length > 0)
            {
                cls_MessageBox.Show("비가동 코드를 선택해주세요.");
                return false;
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            if (div == 'I')
            {
                if (!InsOper(dr))
                {
                    cls_MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'U')
            {
                if (!UpdOper(dr))
                {
                    cls_MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'D')
            { 
                if(!DelOper(dr))
                {
                    cls_MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }

            dr[0] = "";

            return true;
        }

        //추가
        private bool InsOper(DataRow dr)
        {
            DateTime startTime = Convert.ToDateTime(dr["StartTime"]);
            DateTime endTime = Convert.ToDateTime(dr["EndTime"]);

            // 종료시간이 시작시간보다 빠르면 빠른순으로 저장 
            if (startTime > endTime)
            {
                query = $@"INSERT INTO EQ_Operation (
                            CorpId, WorkDay, ProcessCode, LineCode
                            , Seq
                            , MachineCode
                            , OperationYn, ItemCode
                            , StartTime, EndTime
                            --, OrderNo, OrderSeq
                            , Remark, OperationMeasure
                            , CreId, CreIP, CreDt
                        ) Values (
                            '{WrGlobal.CorpID}', '{Convert.ToDateTime(dr["WorkDay"]):yyyyMMdd}', '{processCode}', '{lineCode}'
                            , (SELECT ISNULL(MAX(Seq), 0) + 1
                                FROM EQ_Operation 
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                  AND MachineCode = '{machineCode}'
                                  AND WorkDay = '{Convert.ToDateTime(dr["WorkDay"]):yyyyMMdd}'
                                )
                            , '{machineCode}'
                            , 'N', '{dr["ItemCode"].ToString()}'
                            , '{endTime :yyyy-MM-dd HH:mm}', '{startTime:yyyy-MM-dd HH:mm}'
                            , '{dr["Remark"]}', '{dr["OperationMeasure"]}'
                            , '{WrGlobal.LoginID}', HOST_NAME(), GETDATE()
                        )
                    ";
            }
            else
            {
                query = $@"INSERT INTO EQ_Operation (
                            CorpId, WorkDay, ProcessCode, LineCode
                            , Seq
                            , MachineCode
                            , OperationYn, ItemCode
                            , StartTime, EndTime
                            --, OrderNo, OrderSeq
                            , Remark, OperationMeasure  
                            , CreId, CreIP, CreDt
                        ) Values (
                            '{WrGlobal.CorpID}', '{Convert.ToDateTime(dr["WorkDay"]):yyyyMMdd}', '{processCode}', '{lineCode}'
                            , (SELECT ISNULL(MAX(Seq), 0) + 1
                                FROM EQ_Operation 
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                  AND MachineCode = '{machineCode}'
                                  AND WorkDay = '{Convert.ToDateTime(dr["WorkDay"]):yyyyMMdd}'    
                                )
                            , '{machineCode}'
                            , 'N', '{dr["ItemCode"].ToString()}'
                            , '{startTime:yyyy-MM-dd HH:mm}', '{endTime:yyyy-MM-dd HH:mm}'
                            , '{dr["Remark"]}', '{dr["OperationMeasure"]}'
                            , '{WrGlobal.LoginID}', HOST_NAME(), GETDATE()
                        )
                    ";
            }
                return db.ExecuteQuery(query);
        }

        //수정
        private bool UpdOper(DataRow dr)
        {
            DateTime startTime = Convert.ToDateTime(dr["StartTime"]);
            DateTime endTime = Convert.ToDateTime(dr["EndTime"]);
            string orderNoCondition = !string.IsNullOrEmpty(dr["OrderNo"].ToString()) ?
                                        $"AND OrderNo = '{dr["OrderNo"]}' AND OrderSeq = {dr["OrderSeq"]}" :
                                         "AND (OrderNo IS NULL OR OrderNo = '')";
            // 종료시간이 시작시간보다 빠르면 빠른순으로 저장 
            string timeCondition = startTime > endTime ?
                $"StartTime = '{endTime:yyyy-MM-dd HH:mm}', EndTime = '{startTime:yyyy-MM-dd HH:mm}'" :
                $"StartTime = '{startTime:yyyy-MM-dd HH:mm}', EndTime = '{endTime:yyyy-MM-dd HH:mm}'";

            query = $@"UPDATE EQ_Operation
                        SET  WorkDay = '{Convert.ToDateTime(dr["WorkDay"]):yyyyMMdd}'
                            , ItemCode = '{dr["ItemCode"].ToString()}'
                            , {timeCondition}
                            , Remark = '{dr["Remark"]}'
                            , OperationMeasure = '{dr["OperationMeasure"]}'
                        WHERE CorpId = '{WrGlobal.CorpID}'
                            AND MachineCode = '{machineCode}'
                            AND WorkDay = '{Convert.ToDateTime(dr["WorkDay"]):yyyyMMdd}'
                            AND Seq = '{Convert.ToInt32(dr["Seq"])}'
                            {orderNoCondition}
                    ";
            return db.ExecuteQuery(query);
        }

        //삭제
        private bool DelOper(DataRow dr)
        {
            string orderNoCondition = !string.IsNullOrEmpty(dr["OrderNo"].ToString()) ?
                                        $"AND OrderNo = '{dr["OrderNo"]}' AND OrderSeq = {dr["OrderSeq"]}" :
                                         "AND (OrderNo IS NULL OR OrderNo = '')";
            query = $@"DELETE EQ_Operation
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MachineCode = '{machineCode}'
                          AND WorkDay = '{Convert.ToDateTime(dr["WorkDay"]):yyyyMMdd}'
                        AND Seq = '{Convert.ToInt32(dr["Seq"])}'
                        {orderNoCondition}
                    ";

            return db.ExecuteQuery(query);
        }

        #endregion

        #region 이벤트

        private void InitRow_SetDate()
        {
            DataRow dr = GvOperation?.GetFocusedDataRow();

            if (dr == null) return;

            dr["WorkDay"] = DateTime.Now.Date;
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void GvOperation_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GvOperation.BestFitColumns();
        }
        private void GvOperation_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            //GridView gv = sender as GridView;

            //if (gv == null)
            //    return;

            //DataRow row = gv.GetDataRow(e.RowHandle);

            //if (row == null)
            //    return;

            //// 현재 행의 Status가 "신규"이고, 컬럼이 "OrderNo" 또는 "OrderSeq"인 경우
            //if (row["Status"].ToString() == "신규" && (e.Column.FieldName == "OrderNo" || e.Column.FieldName == "OrderSeq"))
            //{
            //    // 편집 불가능한 RepositoryItem을 생성
            //    RepositoryItemTextEdit readOnlyEdit = new RepositoryItemTextEdit();
            //    readOnlyEdit.ReadOnly = true;
            //    e.RepositoryItem = readOnlyEdit;
            //}
        }

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            //grid.Check_OverLap_CellValue(sender as GridView, e, "Cust_Cd");
        }

        private void BtnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");

                //신규일때, 공정명, 라인명, 설비명을 가져옴. 
                dic.Add("ProcessCode", processCode);
                dic.Add("ProcessName", processName);
                dic.Add("LineCode", lineCode);
                dic.Add("LineName", lineName);
                dic.Add("MachineCode", machineCode);
                dic.Add("MachineName", machineName);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(GvOperation.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(GcOperation, dic, txt_Act);

            if (txt_Act != "Del")
            {
                InitRow_SetDate();
            }
        }
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            //string empNo = WrGlobal.LoginID;

            //frmSelectPM_ProductPlan frm = new frmSelectPM_ProductPlan(empNo);
            //frm.ShowDialog();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayOper();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            OperSave();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}