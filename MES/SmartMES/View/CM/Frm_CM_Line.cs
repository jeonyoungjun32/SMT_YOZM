using baseSMT;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_Line : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        public Frm_CM_Line()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 데이터 입력
            Set_GridView(Gv_Line);

            //엑셀다운
            //btn_ExcelDown.DownGridControl = Gc_Line;

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) =>
            {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Line, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        #region GridView Setting

        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItemSearchLookUpEdit lup_Process = rpo.Create_RepositoryItemSearchLookUp(code.Select_Process_Lup(), "공정코드", "공정이름");
            RepositoryItemSearchLookUpEdit lup_Machine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList(false), "설비코드", "설비명");
            RepositoryItemSearchLookUpEdit lup_Stock = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");
            ;
            //Column 생성
            grid.Create_GvColumn(gv, "상태",      "Status");
            grid.Create_GvColumn(gv, "라인코드",  "LineCode");
            grid.Create_GvColumn(gv, "라인명",    "LineName");
            grid.Create_GvColumn(gv, "공정명", "ProcessCode", lup_Process);
            grid.Create_GvColumn(gv, "설비명", "MachineCode", lup_Machine);

            grid.Create_GvColumn(gv, "투입창고", "InHouseID" , lup_Stock);
            grid.Create_GvColumn(gv, "출고창고", "OutHouseID", lup_Stock);
            grid.Create_GvColumn(gv, "불량창고", "BadHouseID", lup_Stock);

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //Repository 설정
            gv.Columns["LineCode"].ColumnEdit = rpo.Create_rpoTxt_Length(5);
            gv.Columns["LineName"].ColumnEdit = rpo.Create_rpoTxt_Length(30);

            //자동채번이 아닐 경우 코드값 편집 허용
            if (WrGlobal.ProcOption["BS"]["Line_AutoNo"] != "Y")
            {
                grid.Column_AllowEdit(gv.Columns["LineCode"]);
            }

            grid.Column_AllowEdit(gv.Columns["LineName"]);
			grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
			grid.Column_AllowEdit(gv.Columns["MachineCode"]);
			grid.Column_AllowEdit(gv.Columns["InHouseID"]);
			grid.Column_AllowEdit(gv.Columns["OutHouseID"]);
			grid.Column_AllowEdit(gv.Columns["BadHouseID"]);

			gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["MachineCode"].Visible = false;
            gv.Columns["InHouseID"].Visible = false;
            gv.Columns["OutHouseID"].Visible = false;
            gv.Columns["BadHouseID"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인

            //코드숨김
            //if (WrGlobal.LoginID != "Dev")
            //{
            //    gv.Columns["LineCode"].Visible = false;
            //}
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmdIns.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): cmdDel.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            //S-MES 에서 btn.Name => btn.Tag로 수정.
            //SimpleButton btn = sender as SimpleButton;
            //string txt_Act = btn.Name.Substring(btn.Name.Length - 3);
            //Button btn = sender as Button;

            // 1. 태그의 마지막 3글자 가져오기(Ins,Del)
            if (sender is smtSearchButton btn)
            {
                string txt_Act = Convert.ToString(btn.Tag).Substring(Convert.ToString(btn.Tag).Length - 3);


                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (txt_Act != "Del")
                {
                    //삽입, 추가 진행인 경우
                    dic.Add("Status", "신규");
                }
                else
                {
                    //삭제 진행인경우
                    dic.Add("Status", Convert.ToString(Gv_Line.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }
                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(Gc_Line, dic, txt_Act);
            }
            else
            {
                // sender가 System.Windows.Forms.Button이 아닌 경우 처리
                throw new ArgumentException("유효하지 않은 버튼입니다.");
            }
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            grid.Check_OverLap_CellValue(sender as GridView, e, "LineCode");
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_CM_Line_Load(object sender, EventArgs e)
        {
            Func_Search();
        }

        #endregion Event End

        #region 조회 기능

        private void Func_Search()
        {
            //1. 조회 검사
            //if (!Check_Search()) return;

            //2. 라인 조회
            CM_Line Line = new CM_Line();
            Gc_Line.DataSource = Line.Select_CM_Line();

            Gv_Line.BestFitColumns();   //Column 너비 조정
        }

        private bool Check_Search()
        {
            if ((Gv_Line.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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

        #endregion 조회 기능 End

        #region 저장 기능

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            Gv_Line.PostEditor();
            Gv_Line.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = Gc_Line.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

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
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            //3. 정상절차 진행
            Func_Search();

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Check_SaveClick(DataTable dt)
        {
            int checkCnt = 0;
            bool isDelete = false;

            //Data를 불러온 상태인지 검사
            if (dt == null)
            {
                return false;
            }

            //변경사항이 없을 경우 RETURN
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    checkCnt++;
                }
                else if (status == "수정")
                {
                    checkCnt++;
                }
                else if (status == "삭제")
                {
                    checkCnt++;
                    isDelete = true;
                }
            }

            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    return true;
                }
            }

            return true;
        }


        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_Line line = new CM_Line();
            line.LineCode = dr["LineCode"].ConvertString_Null_To_Empty();           //라인코드
            line.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();     //공정코드
            line.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();     //설비코드
            line.InHouseID = dr["InHouseID"].ConvertString_Null_To_Empty();         //투입창고
            line.OutHouseID = dr["OutHouseID"].ConvertString_Null_To_Empty();       //출고창고
            line.BadHouseID = dr["BadHouseID"].ConvertString_Null_To_Empty();       //불량창고

            if (div != 'D')
            {
                //Delete제외 공통
                line.LineName = dr["LineName"].ConvertString_Null_To_Empty();          //라인명
            }

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    //Insert
                    if (!line.Ins_CM_Line())
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
                    if (!line.Upd_CM_Line())
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
                    if (!line.Del_CM_Line())
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion 저장 기능 End


        #region 엑셀버튼 + 닫기 버튼
        private void cmdExcel_Click(object sender, EventArgs e)
        {
                clsFunction function = new clsFunction();
                function.ExportExcel(Gv_Line);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 엑셀버튼 + 닫기버튼 END
    }
}