using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.SM;
using SmartMES.Models;
using SmartMES.Models.SM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_SystemOption : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        RepositoryItemToggleSwitch toggle = new RepositoryItemToggleSwitch();

        public Frm_SM_SystemOption()
        {
            //if (WrGlobal.LoginID != "Dev") return;

            InitializeComponent();
            Set_Grid(Gv_Switch);
            Init_LupData();

            toggle = rpo.Create_rpoToggle();
            cmdSave.Click += Btn_Click;

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
            SM_SystemOption procOption = new SM_SystemOption();
            DataTable dt = procOption.Select_TaskDivList();
            lup.SetLup_Combo(Lup_TaskDiv, dt, "TaskDiv", "TaskDiv");
            Lup_TaskDiv.Properties.Columns["TaskDiv"].Visible = true;

            Lup_TaskDiv.EditValueChanged += Lup_TaskDiv_EditValueChanged;

            //초기값 지정
            if (dt.Rows.Count > 0)
            {
                Lup_TaskDiv.EditValue = dt.Rows[0]["TaskDiv"];
            }
        }

        private void Set_Grid(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;


            grid.Create_GvColumn(gv, "상태"    , "Status");
            grid.Create_GvColumn(gv, "옵션명"  , "OptionNm");
            grid.Create_GvColumn(gv, "값"      , "Value");
            grid.Create_GvColumn(gv, "비고"    , "Remark");
            grid.Create_GvColumn(gv, "등록자"  , "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자"  , "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Column_AllowEdit(gv.Columns["Value"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);


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
            grid.Check_OverLap_CellValue(sender as GridView, e, "OptionNm");
        }

        private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //검사항목 일정Row의 Repository다르게하기
            GridView gv = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string fieldName = e.Column.FieldName;
                if (fieldName == "Value")
                {
                    string value = Convert.ToString(gv.GetRowCellValue(e.RowHandle, fieldName));

                    if(value == "Y" || value == "N")
                    {
                        e.RepositoryItem = toggle;
                    }
                }
            }
        }

        private void Lup_TaskDiv_EditValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void Display()
        {
            SM_SystemOption procOption = new SM_SystemOption();
            Gc_Switch.DataSource = procOption.Select_SystemOption(Convert.ToString(Lup_TaskDiv.EditValue));

            Gv_Switch.BestFitColumns();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;
            if (btn == null) return;

            switch (btn.Name)
            {
                case "cmdSave":
                    Save();
                    break;

                case "cmdApply":
                    ApplyOption();
                    break;

                case "cmdClose":
                    this.Close();
                    break;
            }
        }

        private void Save()
        {
            DataTable dt = Gc_Switch.DataSource as DataTable;
            bool Success = false;

            if (!Check_SaveClick(dt) || dt == null) return;

            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);

                switch (status)
                {
                    case "수정": Success = Save_ToDatabase(dr, 'U'); break;
                    //case "삭제": Save_ToDatabase(dr, 'D'); break;
                    default: Success = true; break;
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
            SM_SystemOption procOption = new SM_SystemOption();
            procOption.TaskDiv  = Convert.ToString(dr["TaskDiv"]);
            procOption.OptionNm = Convert.ToString(dr["OptionNm"]);
            procOption.Value    = Convert.ToString(dr["Value"]);
            procOption.Remark   = Convert.ToString(dr["Remark"]);

            //2. DB 적용
            if (div == 'U')
            {
                //Update
                if (!procOption.Upd_SystemOption())
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

        private void ApplyOption()
        {
            WrGlobal.ProcOption.Clear();
            WrGlobal.CdOption.Clear();

            //Process Option  호출
            try
            {
                SM_SystemOption procOption = new SM_SystemOption();
                DataTable dtProcOption = procOption.Select_SystemOption();

                foreach (DataRow dr in dtProcOption.Rows)
                {
                    string div = Convert.ToString(dr["TaskDiv"]);
                    string option = Convert.ToString(dr["OptionNm"]);
                    string value = Convert.ToString(dr["Value"]);

                    //업무구분
                    if (WrGlobal.ProcOption.ContainsKey(div))
                    {
                        WrGlobal.ProcOption[div].Add(option, value);
                    }
                    else
                    {
                        WrGlobal.ProcOption.Add(div, new Dictionary<string, string>() { { option, value } });
                    }
                }

                //CodeOption 호출
                SM_CodeOption cdOption = new SM_CodeOption();
                DataTable dtCdOption = cdOption.Select_CdOption();
                foreach (DataRow dr in dtCdOption.Rows)
                {
                    string div = Convert.ToString(dr["DivCd"]);
                    string fieldNm = Convert.ToString(dr["FieldID"]);
                    string caption = Convert.ToString(dr["CaptionNm"]);
                    int DispSeq = dr["DispSeq"].ConvertString_Null_To_Empty() == "" ? 99 : dr["DispSeq"].ConvertInt_Null_To_Zero();
                    bool disp = Convert.ToString(dr["DispYn"]) == "Y" ? true : false;
                    bool lockYn = Convert.ToString(dr["LockYn"]) == "Y" ? true : false;

                    //Option 목록
                    WrGlobal.CodeOption optionList = new WrGlobal.CodeOption { Nm = caption, DispSeq = DispSeq, DispYn = disp, LockYn = lockYn };

                    //옵션구분 존재 확인
                    if (WrGlobal.CdOption.ContainsKey(div))
                    {
                        //옵션구분이 존재시
                        WrGlobal.CdOption[div].Add(fieldNm, optionList);
                    }
                    else
                    {
                        //옵션구분이 없을 시
                        WrGlobal.CdOption.Add(div, new Dictionary<string, WrGlobal.CodeOption>() { { fieldNm, optionList } });
                    }
                }

                MessageBox.Show("적용이 완료되었습니다.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
	}
}