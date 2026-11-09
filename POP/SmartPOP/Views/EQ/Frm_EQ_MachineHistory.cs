using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SMT_MES.Controls;
using SmartPOP.Common;
using SmartPOP.Models;
using DevExpress.XtraEditors.Repository;
using SmartPOP.Model.EQ;

namespace SmartPOP.Views.EQ
{
    public partial class Frm_EQ_MachineHistory : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string groupCode = "";
        string query = "";
        string procCode = "", machineCode = "";
        cls_Repository rpo = new cls_Repository();
        cls_Grid grid = new cls_Grid();

        public Frm_EQ_MachineHistory()
        {
            InitializeComponent();
        }

        private void frmMachineHistory_Load(object sender, EventArgs e)
        {
            //장비그룹 리스트 조회

            #region 이전 조회 쿼리 
            /*
             * 그룹으로 나눌 필요 X
            query = $@"SELECT * 
                    FROM CLASS
                    WHERE CorpId = '{WrGlobal.CorpID}' AND CLASS Like 'P0%' AND CLASSLEVEL = 1";
            */
            #endregion

            #region 이전 공정 treeList 조회
            /*
            query = $@"SELECT *
                        FROM CM_Process
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND WorkType = '10'     -- 사내만
                    ";
            treeListCategoryGroup.DataSource = db.GetDataTable(query);
            treeListCategoryGroup.ExpandAll();
            */
            #endregion 이전 공정 treeList 조회 END

            LoadProcessLookUp();
            Lup_Process.smtLookUpEditValueChanged += LupProcess_EditValueChanged;
            LoadMachineLookUp(procCode);
            Lup_Machine.smtLookUpEditValueChanged += LupMachine_EditValueChanged;

            grid.SetPOPGrid(gvRepair);
            DisplayData();
        }

        #region LookUpEdit 설정
        private void LoadProcessLookUp()
        {
            cls_Lookup clsLup = new cls_Lookup();

            query = $@" SELECT 'ALL' AS 공정코드, '전체' AS 공정명
                        UNION ALL
                        SELECT ProcessCode 공정코드, 
                               ProcessName 공정명
                        FROM CM_Process 
                        WHERE CorpId = '{WrGlobal.CorpID}' AND UseYn = 'Y' AND WorkType = '10'";
            DataTable dt = db.GetDataTable(query);
            Lup_Process.smtLookUpDataSource = dt;
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpDataValueMember = "공정코드";
            if (dt.Rows.Count > 0)
            {
                Lup_Process.smtLookUpValue = dt.Rows[0]["공정코드"].ConvertString_Null_To_Empty();
                this.procCode = dt.Rows[0]["공정코드"].ToString();
            }
            Lup_Process.smtLookUpValue = "ALL";
        }
        private void LoadMachineLookUp(string processCode)
        {
            cls_Lookup clsLup = new cls_Lookup();

            query = $@"SELECT 'ALL' AS 설비코드, '전체' AS 설비명
                       --UNION ALL
                       SELECT 
	                        --cp.ProcessCode
	                        --, cp.ProcessName
	                        --, cl.LineCode
	                        --, cl.LineName
	                        cm.MachineCode 설비코드,
                            cm.MachineName 설비명
                        FROM CM_Process cp
                        LEFT JOIN CM_Line cl ON cp.ProcessCode = cl.ProcessCode
                        LEFT JOIN EQ_Machine cm ON cl.MachineCode = cm.MachineCode
                        WHERE cp.CorpId = '{WrGlobal.CorpID}'
                        AND cp.WorkType = '10'
                        AND ('{processCode}' = 'ALL' OR cp.ProcessCode = '{processCode}')
                        AND cm.MachineCode IS NOT NULL 
                        AND cm.MachineName IS NOT NULL
                    ";
            DataTable dt = db.GetDataTable(query);
            Lup_Machine.smtLookUpDataSource = dt;
            Lup_Machine.smtLookUpDataDisplayMember = "설비명";
            Lup_Machine.smtLookUpDataValueMember = "설비코드";
            if (dt.Rows.Count > 0)
            {
                Lup_Machine.smtLookUpValue = dt.Rows[0]["설비코드"].ConvertString_Null_To_Empty();
                this.machineCode = dt.Rows[0]["설비코드"].ToString();
            }
            else
            {
                this.machineCode = "";
            }
            Lup_Machine.smtLookUpValue = "ALL";

            query = $@" SELECT 
	                        cm.MachineCode 설비코드,
                            cm.MachineName 설비명
                        FROM CM_Process cp
                        LEFT JOIN CM_Line cl ON cp.ProcessCode = cl.ProcessCode
                        LEFT JOIN EQ_Machine cm ON cl.MachineCode = cm.MachineCode
                        WHERE cp.CorpId = '{WrGlobal.CorpID}'
                        AND cp.WorkType = '10'
                        AND ('{processCode}' = 'ALL' OR cp.ProcessCode = '{processCode}')
                        AND cm.MachineCode IS NOT NULL 
                        AND cm.MachineName IS NOT NULL";

            dt = db.GetDataTable(query);

            slupMachine.DataSource = dt;
            slupMachine.DisplayMember = "설비명";
            slupMachine.ValueMember = "설비코드";
        }
        private void LupProcess_EditValueChanged(object sender, EventArgs e)
        {
            this.procCode = Lup_Process.smtLookUpValue?.ToString();
            LoadMachineLookUp(procCode);
        }
        private void LupMachine_EditValueChanged(object sender, EventArgs e)
        {
            this.machineCode = Lup_Process.smtLookUpValue?.ToString();
        }

        #endregion LookUpEdit 설정 END


        private void treeListCategoryGroup_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DisplayDetail();         
        }

        private void DisplayDetail()
        {
            //gvMachine.FocusInvalidRow();

            #region 이전 조회 쿼리
            /*
            //선택된 노드 가져오기
            TreeListMultiSelection selectedNodes = treeListCategoryGroup.Selection;
            groupCode = selectedNodes[0].GetValue(treeListCategoryGroup.Columns["CLASS"]).ToString();

            //장비 리스트 조회
            query = $@"SELECT cm.*, cc.CLASSNAME FROM dbo.EQ_Machine cm Left Join CLASS cc on cm.CorpId = cc.CorpId 
                        and cc.CLASS = cm.Mgroup where cm.CorpId = '{WrGlobal.CorpID}' and cm.Mgroup like '{groupCode}' +'%'";
            */
            #endregion
            //string procCode = treeListCategoryGroup.GetFocusedRowCellValue("ProcessCode").ToString();

            //query = $@"SELECT CM.*
            //            FROM CM_Process CP
            //            INNER JOIN CM_Line CL ON CP.ProcessCode = CL.ProcessCode
            //            INNER JOIN EQ_Machine CM ON CL.MachineCode = CM.MachineCode
            //            WHERE CP.CorpId = '{WrGlobal.CorpID}'
            //            AND CP.ProcessCode = '{procCode}'
            //        ";

            //gcMachine.DataSource = db.GetDataTable(query);
            DisplayData();
        }

        //조회 
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void gvMachine_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            //머신코드 가져오기
            //string MachineCode = gvMachine.GetFocusedRowCellDisplayText("MachineCode");

            query = $@"SELECT '' Status
                                ,CorpId, MachineCode, Seq, Disp_Seq, Repair_Date, Repair_Cause, Repair_State, Repair_Emp_No, Repair_Start_Time, Repair_End_Time, Repair_Time, Repair_Cust_Cd, Repair_Amt, Remark, Ins_Emp_No, Ins_Date_Time, Upd_Emp_No, Upd_Date_Time 
                                FROM MachineRepairList 
                                Where CorpId = {WrGlobal.CorpID} ";

            if (procCode != "ALL" && procCode != "")
            {
                query += $@" AND ProcessCode = '{procCode}'";
            }

            if (machineCode != "ALL" && machineCode != "")
            {
                query += $@" AND MachineCode = '{machineCode}'";
            }

            gcRepair.DataSource = db.GetDataTable(query);
            gvRepair.BestFitColumns();
        }

        #region 등록 기능
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            //머신코드 가져오기
            //string MachineCode = gvMachine.GetFocusedRowCellDisplayText("MachineCode");

            try
            {
                DataTable dt = gcRepair.DataSource as DataTable;

                if (dt == null) // dt데이터 없을 때 새로 추가해줌
                {
                    DataTable _dt = new DataTable();

                    foreach (GridColumn col in gvRepair.Columns)
                    {
                        _dt.Columns.Add(col.FieldName, col.ColumnType);
                    }

                    dt = _dt;
                    gcRepair.DataSource = dt;
                }

                DataRow dr = dt.NewRow();

                //행추가 기본값 셋팅
                dr["Status"] = "신규";
                dr["CorpId"] = WrGlobal.CorpID;
                dr["MachineCode"] = machineCode;
                dr["Seq"] = GetNextMachineRepairSeq();

                dt.Rows.Add(dr);

                gvRepair.BestFitColumns();
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message);
            }
        }
        #endregion 등록 기능 End

        #region 저장 기능 
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                //작성내역 저장
                DataTable dt = gcRepair.DataSource as DataTable;

                //2. 데이터 저장
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
                }
                //3. 조회
                DisplayData();

                cls_MessageBox.Show("저장이 정상적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message);
            }
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            EQ_Machine machine = new EQ_Machine();
            machine.MachineCode = Convert.ToString(dr["MachineCode"]);     //설비코드
            machine.Seq = Convert.ToString(dr["Seq"]); //Seq
            machine.Repair_Cause = dr["Repair_Cause"].ConvertString_Null_To_Empty();  //고장내용
            machine.Repair_State = dr["Repair_State"].ConvertString_Null_To_Empty();  //수리내용
            machine.Repair_Emp_No = dr["Repair_Emp_No"].ConvertString_Null_To_Empty(); //수리 담당자
            machine.Repair_Cust_Cd = dr["Repair_Cust_Cd"].ConvertString_Null_To_Empty();  //거래처
            machine.Repair_Amt = dr["Repair_Amt"].ConvertInt_Null_To_Zero(); //수리금액
            machine.Repair_Amt = dr["Repair_Amt"].ConvertInt_Null_To_Zero(); //수리금액
            machine.Repair_Time = dr["Repair_Time"].ConvertInt_Null_To_Zero(); //수리시간
            machine.Remark = Convert.ToString(dr["Remark"]);

            //수리날짜
            if (Convert.ToString(dr["Repair_Date"]) != "")
            {
                machine.Repair_Date = Convert.ToDateTime(dr["Repair_Date"]).ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                machine.Repair_Date = "";
            }
            //수리시작시간
            if (Convert.ToString(dr["Repair_Start_Time"]) != "")
            {
                machine.Repair_Start_Time = Convert.ToDateTime(dr["Repair_Start_Time"]).ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                machine.Repair_Start_Time = dr["Repair_Start_Time"].ConvertString_Null_To_Empty();
            }
            //수리종료시간
            if (Convert.ToString(dr["Repair_End_Time"]) != "")
            {
                machine.Repair_End_Time = Convert.ToDateTime(dr["Repair_End_Time"]).ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                machine.Repair_End_Time = dr["Repair_End_Time"].ConvertString_Null_To_Empty();
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!machine.Ins_Repair())
                {
                    //저장 중 에러 발생시
                    cls_MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'U')
            {
                //Update
                if (!machine.Upd_Repair())
                {
                    //저장 중 에러 발생시
                    cls_MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }

            //상태값 초기화
            dr[0] = "";

            return true;
        }
        #endregion 저장 기능 end

        #region 삭제 기능
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (gvRepair.SelectedRowsCount > 0)
            {
                //자료를 삭제하는 부분
                if (cls_MessageBox.Show("선택한 자료를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                try
                {
                    int row = 0;

                    for (int rowCnt = 0; rowCnt < gvRepair.SelectedRowsCount; rowCnt++)
                    {
                        if (gvRepair.GetSelectedRows()[rowCnt] >= 0)
                        {
                            row = gvRepair.GetSelectedRows()[rowCnt];

                            string seq = gvRepair.GetRowCellValue(row, "Seq")?.ToString() ?? "";
                            string machineCode = gvRepair.GetRowCellValue(row, "MachineCode")?.ToString() ?? "";

                            query = $@"DELETE MachineRepairList WHERE MachineCode = '{machineCode}' and Seq='{seq}' and CorpID = '{WrGlobal.CorpID}'";
                            db.ExecuteQuery(query);
                        }
                    }

                    cls_MessageBox.Show($"{gvRepair.SelectedRowsCount}건의 수리내역을 삭제하였습니다.");
                    DisplayData();
                }
                catch (Exception ex)
                {
                    cls_MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion 삭제 기능 End

        //엑셀 버튼
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(gvRepair);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvRepair_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Status") return;  // 상태값이 바뀐거면 return;

            // 상태값 수정으로 변경
            Change_Status(gvRepair , e.RowHandle);

            gvRepair.BestFitColumns();
        }

        private void Change_Status(GridView gv, int rowHandle)
        {
            //셀값 수정 시 상태값 수정
            DataTable dt = gv.GridControl.DataSource as DataTable;
            string str_Status = Convert.ToString(gv.GetFocusedRowCellValue(dt.Columns[0].Caption));    //상태 값

            if (str_Status != "신규" &&
                str_Status != "신규(일괄)" &&
                str_Status != "수정" &&
                str_Status != "삭제")
            {
                gv.SetRowCellValue(rowHandle, gv.Columns[0].FieldName, "수정");
            }
        }

        public string GetNextMachineRepairSeq()
        {
            //string machineCode = gvMachine.GetFocusedRowCellDisplayText("MachineCode");

            string query = $"SELECT ISNULL(MAX(Seq), 0) + 1 AS NextSeq FROM MachineRepairList WHERE CorpId = '{WrGlobal.CorpID}' AND MachineCode = '{machineCode}'";
            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                // drv[0]을 int로 형변환한 후 ToString()을 호출하여 string으로 변환
                int nextSeq = Convert.ToInt32(drv[0]);
                return nextSeq.ToString();
                //return drv[0].ToString();
            }
            else
            {
                return "";
            }
        }
    }
}