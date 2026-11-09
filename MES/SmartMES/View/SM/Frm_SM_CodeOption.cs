using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.SM;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_CodeOption : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        RepositoryItemToggleSwitch toggle = new RepositoryItemToggleSwitch();

        public Frm_SM_CodeOption()
        {
            //if (WrGlobal.LoginID != "Dev") return;

            InitializeComponent();
            Set_Grid(Gv_Switch);
            Init_LupData();

            toggle = rpo.Create_rpoToggle();
            cmdSave.Click += Btn_Save_Click;

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Switch, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Init_LupData()
        {
            cls_Lookup lup = new cls_Lookup();
            SM_CodeOption cdOption = new SM_CodeOption();
            DataTable dt = cdOption.Select_CdOption_CdDiv();
            lup.SetLup_Combo(Lup_TaskDiv, dt, "DivCd", "DivCd");
            Lup_TaskDiv.Properties.Columns["DivCd"].Visible = true;

            Lup_TaskDiv.EditValueChanged += Lup_EditValueChanged;

            //초기값 지정
            if (dt.Rows.Count > 0)
            {
                Lup_TaskDiv.EditValue = dt.Rows[0]["DivCd"];
            }
        }

        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void Set_Grid(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;


            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "FieldName", "FieldID");
            grid.Create_GvColumn(gv, "표기명", "CaptionNm");
            grid.Create_GvColumn(gv, "표시순서", "DispSeq");
            grid.Create_GvColumn(gv, "표시 Yn", "DispYn");
            grid.Create_GvColumn(gv, "수정 Yn", "LockYn");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Column_AllowEdit(gv.Columns["CaptionNm"]);
            grid.Column_AllowEdit(gv.Columns["DispYn"]);
            grid.Column_AllowEdit(gv.Columns["LockYn"]);


            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void Event_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            grid.Check_OverLap_CellValue(sender as GridView, e, "FieldID");
        }

        private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //검사항목 일정Row의 Repository다르게하기
            GridView gv = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string fieldName = e.Column.FieldName;
                if (fieldName == "DispYn" || fieldName == "LockYn")
                {
                    string value = Convert.ToString(gv.GetRowCellValue(e.RowHandle, fieldName));

                    if (value == "Y" || value == "N")
                    {
                        e.RepositoryItem = toggle;
                    }
                }
            }
        }

        private void Display()
        {
            SM_CodeOption cdOption = new SM_CodeOption();
            Gc_Switch.DataSource = cdOption.Select_CdOption(Convert.ToString(Lup_TaskDiv.EditValue));

            Gv_Switch.BestFitColumns();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            DataTable dt = Gc_Switch.DataSource as DataTable;
            bool Success = true;

            if (!Check_SaveClick(dt) || dt == null) return;

            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);

                switch (status)
                {
                    case "수정":
                        Success = Save_ToDatabase(dr, 'U');
                        break;
                        //case "삭제": Save_ToDatabase(dr, 'D'); break;
                }

                if (!Success) break;
            }

            if (Success)
            {
                MessageBox.Show("저장이 완료되었습니다.");
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "삭제")
                {
                    DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            SM_CodeOption cdOption = new SM_CodeOption();
            cdOption.DivCd = Convert.ToString(dr["DivCd"]);
            cdOption.FieldID = Convert.ToString(dr["FieldID"]);
            cdOption.CaptionNm = Convert.ToString(dr["CaptionNm"]);
            cdOption.DispSeq = dr["DispSeq"].ConvertString_Null_To_Empty();
            cdOption.DispYn = Convert.ToString(dr["DispYn"]);
            cdOption.LockYn = Convert.ToString(dr["LockYn"]);

            //2. DB 적용
            if (div == 'U')
            {
                //Update
                if (!cdOption.Upd_CdOption())
                {
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            //else if (div == 'D')
            //{
            //    //Delete
            //    if (!procOption.Del_Item())
            //    {
            //        //저장 중 에러 발생시
            //        MessageBox.Show("저장 중 에러가 발생하였습니다.");
            //        return false;
            //    }

            //    //추가 변경사항 private 따로 만들기
            //}

            //상태값 초기화
            dr[0] = "";

            return true;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;
            if (btn == null) return;

            GridControl gc = Gc_Switch;
            if (gc == null) return;

            GridView gv = gc.MainView as GridView;
            if (gv == null) return;

            DataTable dt = gc.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count <= 1) return;

            int idx = gv.FocusedRowHandle;

            DataRow curRow = null;
            DataRow newRow = null;

            switch (btn.Name)
            {
                case "Btn_Up":

                    if (idx == 0) return;   // Top일 경우 종료

                    curRow = dt.Rows[idx];
                    newRow = dt.NewRow();

                    newRow.ItemArray = curRow.ItemArray; // 내용 복사.
                    newRow["Status"] = "수정";
                    dt.Rows.RemoveAt(idx);

                    gv.SetRowCellValue(idx - 1, "Status", "수정");
                    dt.Rows.InsertAt(newRow, idx - 1);

                    if (idx == dt.Rows.Count - 1)
                    {
                        gv.FocusedRowHandle = gv.FocusedRowHandle - 1;
                    }
                    else
                    {
                        gv.FocusedRowHandle = gv.FocusedRowHandle - 2;
                    }

                    break;

                case "Btn_Down":

                    if (idx == dt.Rows.Count - 1) return; // Bottom일 경우 종료

                    curRow = dt.Rows[idx];
                    newRow = dt.NewRow();

                    newRow.ItemArray = curRow.ItemArray; // 내용 복사.
                    newRow["Status"] = "수정";

                    dt.Rows.RemoveAt(idx);

                    gv.SetRowCellValue(idx, "Status", "수정");
                    dt.Rows.InsertAt(newRow, idx + 1);

                    gv.FocusedRowHandle++;

                    break;
            }

            gv.SelectRow(gv.FocusedRowHandle);

            SetDispSeq();
        }

        /*   기존 순서 변경
        private void Btn_Up_Click(object sender, EventArgs e)
        {
            GridControl gc = Gc_Switch;
            GridView gv = gc.MainView as GridView;

            DataTable dt = gc.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count <= 1) return;

            int idx = gv.FocusedRowHandle;
            if (idx == 0) return; // Top일 경우 종료

            DataRow curRow = dt.Rows[idx];
            DataRow newRow = dt.NewRow();

            newRow.ItemArray = curRow.ItemArray; // 내용 복사.
            newRow["Status"] = "수정";
            newRow["DispSeq"] = idx;
            dt.Rows.RemoveAt(idx);

            gv.SetRowCellValue(idx - 1, "Status", "수정");
            gv.SetRowCellValue(idx - 1, "DispSeq", idx + 1);

            dt.Rows.InsertAt(newRow, idx - 1);

            if (idx == dt.Rows.Count - 1)
            {
                gv.FocusedRowHandle = gv.FocusedRowHandle - 1;
            }
            else
            {
                gv.FocusedRowHandle = gv.FocusedRowHandle - 2;
            }

            gv.SelectRow(gv.FocusedRowHandle);
        }

        private void Btn_Down_Click(object sender, EventArgs e)
        {
            GridControl gc = Gc_Switch;
            GridView gv = gc.MainView as GridView;

            DataTable dt = gc.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count <= 1) return;

            int idx = gv.FocusedRowHandle;
            if (idx == dt.Rows.Count - 1) return; // Bottom일 경우 종료

            DataRow curRow = dt.Rows[idx];
            DataRow newRow = dt.NewRow();

            newRow.ItemArray = curRow.ItemArray; // 내용 복사.
            newRow["Status"] = "수정";
            newRow["DispSeq"] = idx + 2;

            dt.Rows.RemoveAt(idx);

            gv.SetRowCellValue(idx, "Status", "수정");
            gv.SetRowCellValue(idx, "DispSeq", idx + 1);


            dt.Rows.InsertAt(newRow, idx + 1);

            gv.FocusedRowHandle++;

            gv.SelectRow(gv.FocusedRowHandle);
        }*/

        private void Btn_Set_DispSeq_Click(object sender, EventArgs e)
        {
            SetDispSeq();
        }

        private void SetDispSeq()
        {
            Gv_Switch.FocusInvalidRow();

            DataTable dt = Gc_Switch.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count < 1) return;

            int seq = 0;

            for (int i = 0; i < Gv_Switch.RowCount; i++)
            {
                string DispYn = Gv_Switch.GetRowCellValue(i, "DispYn").ConvertString_Null_To_Empty();
                string DispSeq = Gv_Switch.GetRowCellValue(i, "DispSeq").ConvertString_Null_To_Empty();

                if (DispYn == "Y")
                {
                    seq += 1;

                    if (DispSeq == seq.ConvertString_Null_To_Empty()) continue;

                    Gv_Switch.SetRowCellValue(i, "DispSeq", seq);
                    Gv_Switch.SetRowCellValue(i, "Status", "수정");
                }
                else
                {
                    if (DispSeq == "") continue;

                    Gv_Switch.SetRowCellValue(i, "DispSeq", "");
                    Gv_Switch.SetRowCellValue(i, "Status", "수정");
                }
            }
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}