using DevExpress.XtraDiagram.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.IF;
using SmartMES.Models.PM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.IF
{
    public partial class Frm_IF_PLCSetting : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력
        clsCode code = new clsCode();

        public Frm_IF_PLCSetting()
        {
            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            Set_DGPControl(gvDGPControl);
            Set_DGPCode(gvDGPCode);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(gvDGPCode, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_DGPControl(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false; //DataTable 입력시 Column 생성 방지
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            //Column 생성
            grid.Create_GvColumn(gv, "컨트롤코드", "CTCode");
            grid.Create_GvColumn(gv, "컨트롤명", "CTName");
            grid.Create_GvColumn(gv, "PLC제조사", "CT_Company");
            grid.Create_GvColumn(gv, "IP", "CT_IP");
            grid.Create_GvColumn(gv, "Port", "CT_Port");

            //grid.RowNumber_Indicator(gv);       //행순번 표기
        }

        private void Set_DGPCode(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "컨트롤코드", "CTCode");
            grid.Create_GvColumn(gv, "항목코드", "DgpCode");
            grid.Create_GvColumn(gv, "항목명", "DgpName");
            grid.Create_GvColumn(gv, "항목스펙", "DgpSpec");
            grid.Create_GvColumn(gv, "설비명", "Equip_Name");
            grid.Create_GvColumn(gv, "읽기주소", "MemoryCode");
            grid.Create_GvColumn(gv, "쓰기주소", "Write_Address");
            grid.Create_GvColumn(gv, "항목타입", "Gauge_Type");
            grid.Create_GvColumn(gv, "사용여부", "Value_Or");
            grid.Create_GvColumn(gv, "하한값", "Value_Min");
            grid.Create_GvColumn(gv, "상한값", "Value_Max");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["DgpName"]);          //조건명
            grid.Column_AllowEdit(gv.Columns["DgpSpec"]);          //조건스펙
            grid.Column_AllowEdit(gv.Columns["Equip_Name"]);          //조건스펙
            grid.Column_AllowEdit(gv.Columns["MemoryCode"]);          //읽기주소
            grid.Column_AllowEdit(gv.Columns["Write_Address"]);          //쓰기주소
            grid.Column_AllowEdit(gv.Columns["Gauge_Type"]);          //조건타입
            grid.Column_AllowEdit(gv.Columns["Value_Or"]);          //사용여부
            grid.Column_AllowEdit(gv.Columns["Value_Min"]);          //하한값
            grid.Column_AllowEdit(gv.Columns["Value_Max"]);          //상한값

            RepositoryItemSearchLookUpEdit lup_MachineCode = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList(false), "설비코드", "설비명");
            gv.Columns["Equip_Name"].ColumnEdit = lup_MachineCode;

            RepositoryItemCheckEdit chk_Use = rpo.Create_rpoChk();
            gv.Columns["Value_Or"].ColumnEdit = chk_Use;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            gv.Columns["Value_Or"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부

            gv.Columns["CTCode"].Visible = false;
            //gv.Columns["MemoryCode"].Visible = false;
            //gv.Columns["Write_Address"].Visible = false;
            gv.Columns["Value_Or"].Visible = false;
            gv.Columns["DgpSpec"].Visible = false;

            grid.SetCol_Type(gv.Columns["Value_Min"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Value_Max"], clsGrid.TypeList.TFloat);

            //이벤트 설정
            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;        //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;                 //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;                  //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;          //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;                  //코드 입력시 중복 확인
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                //case (Keys.Alt | Keys.I): Btn_Row_Ins.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                //case (Keys.Alt | Keys.D): Btn_Row_Del.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                string ctCode = (gvDGPControl.GetFocusedRowCellValue("CTCode") ?? "").ToString();

                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("CTCode", ctCode);
                dic.Add("Value_Or", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(gvDGPCode.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gcDGPCode, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            GridView gv = sender as GridView;
            grid.Check_OverLap_CellValue(gv, e, "DgpCode");
        }

        #region Event

        private void frmPLCSetting_Load(object sender, EventArgs e)
        {
            SearchDGPControl();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnSave": Func_Save(); break;

                case "Btn_Reset": Count_Reset(); break;
            }
        }

        #endregion Event End

        #region 조회 기능

        #region 품목 검사항목 조회

        private void SearchDGPControl()
        {
            if (!Check_Search()) return;

            IF_DGPControl mc = new IF_DGPControl();
            DataTable dt = mc.Select_IF_DGPControl();

            gcDGPControl.DataSource = dt;
            gvDGPControl.BestFitColumns();
        }
        private void Gv_Machine_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchDGP();
        }

        private void SearchDGP()
        {
            string ctCode = (gvDGPControl.GetFocusedRowCellValue("CTCode") ?? "").ToString();

            IF_DGPCode mc = new IF_DGPCode();
            mc.CTCode = ctCode;

            DataTable dt = mc.Select_IF_DGPCode();
            gcDGPCode.DataSource = dt;
            gvDGPCode.BestFitColumns();
        }

        private bool Check_Search()
        {
            if ((gvDGPCode.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
                                                                    OR Status = '수정'
                                                                    OR Status = '삭제'").Length > 0)
            {
                if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //다른항목 진행할 경우
                    return true;
                }
                else
                {
                    //다른항목 진행하지 않을경우
                    return false;
                }
            }

            return true;
        }

        #endregion 품목 검사항목 조회 End

        #endregion 조회버튼 클릭 End

        #region 저장 기능


        private void Func_Save()
        {
            //작성내역 저장
            DataTable dtItem = gcDGPCode.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'D')) return;
                }
            }

            SearchDGP();

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            //미입력된 칸 있는지 확인
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    if (Convert.ToString(dr["DgpName"]) == "")
                    {
                        MessageBox.Show("조건명 항목이 비어있습니다.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정")
                {
                    if (Convert.ToString(dr["DgpName"]) == "")
                    {
                        MessageBox.Show("조건명 항목이 비어있습니다.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {

                    isDelete = true;
                    checkCnt++;
                }
            }
            if (checkCnt == 0) return false;

            //삭제내역 있으면 물어보기
            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {

            //데이터베이스에 반영
            //1. 기본 셋팅
            IF_DGPCode mc = new IF_DGPCode();
            mc.CTCode = dr["CTCode"].ConvertString_Null_To_Empty();   // 컨트롤코드
            mc.DgpCode = dr["DgpCode"].ConvertString_Null_To_Empty();   // 조건코드
            if (div != 'D')
            {
                //Delete제외 공통
                mc.DgpName = dr["DgpName"].ConvertString_Null_To_Empty();   // 조건명
                //mc.DgpSpec = dr["DgpSpec"].ConvertString_Null_To_Empty();   // 조건스펙
                mc.Equip_Name = dr["Equip_Name"].ConvertString_Null_To_Empty();   // 설비코드
                mc.Gauge_Type = dr["Gauge_Type"].ConvertString_Null_To_Empty();   // 조건타입
                mc.Value_Or = dr["Value_Or"].ConvertString_Null_To_Empty();   // 사용여부
                mc.MemoryCode = dr["MemoryCode"].ConvertString_Null_To_Empty();   // 읽기주소
                mc.Write_Address = dr["Write_Address"].ConvertString_Null_To_Empty();   // 쓰기주소
                mc.Value_Max = dr["Value_Max"].ConvertDecimal_Null_To_Zero();   // 상한값
                mc.Value_Min = dr["Value_Min"].ConvertDecimal_Null_To_Zero();   // 하한값

                mc.PlcModel = gvDGPControl.GetFocusedRowCellValue("CT_Company").ConvertString_Null_To_Empty();  // PLC제조사
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!mc.Ins_IF_DGPCode())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'U')
            {
                //Update
                if (!mc.Upd_IF_DGPCode())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'D')
            {
                //Delete
                if (!mc.Del_IF_DGPCode())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr[0] = "";


            return true;

        }

        #endregion 저장 기능 End

        private void Count_Reset()
        {
            GridView gv = gvDGPCode;
            if (gv.SelectedRowsCount < 1) return;

            DataTable dt = gv.GridControl.DataSource as DataTable;
            if (dt.Rows.Count <= 0) return;

            foreach (int seq in gv.GetSelectedRows())
            {
                DataRow row = gv.GetDataRow(seq);

                string dgpCode = row["DgpCode"].ConvertString_Null_To_Empty();

                IF_DGPStatus status = new IF_DGPStatus();
                status.DgpCode = dgpCode;
                status.CounterReset = "ON";

                status.Update_CounterReset();
            }

            // 선택 해제
            gv.ClearSelection();
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}