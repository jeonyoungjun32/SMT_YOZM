using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SmartMES.Models;
using SmartMES.Common;
using SmartMES.Models.CM;
using SmartCommon.Common;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_Class : DevExpress.XtraEditors.XtraForm
    {
        bool _isWorking = false;
        string inputYn = "N";
        string activeGrid = "Group1";
        clsGrid grid = new clsGrid();

        clsCode codes = new clsCode();

        public Frm_CM_Class()
        {
            InitializeComponent();
        }

        private void Frm_CM_Class_Load(object sender, EventArgs e)
        {
            if (WrGlobal.AuthorList.Exists(s => s.MenuID == this.Tag.ToString() && s.AuthorCode == "02"))
            {
                cmdSave.Visible = true;
                cmdDelete.Visible = true;
            }

            _isWorking = true;
            DisplayGroup1();
        }

        private void DisplayGroup1()
        {
            CM_CLASS cl = new CM_CLASS();
            cl.CLASSLEVEL = 0;

            gcClass1.DataSource = cl.Select_Class();
            gvClass1.BestFitColumns();
        }

        private void DisplayGroup2()
        {
            DataRow dr = gvClass1.GetFocusedDataRow();
            CM_CLASS cl = new CM_CLASS();
            if (dr == null)
            {
                gcClass2.DataSource = null;
                return;
            }

            string code = gvClass1.GetFocusedRowCellValue("CLASS").ToString();

            cl.CLASSLEVEL = 1;
            cl.HIGHCLASS = code;

            gcClass2.DataSource = cl.Select_Class();
            gvClass2.BestFitColumns();
        }

        private void DisplayGroup3()
        {
            DataRow dr = gvClass2.GetFocusedDataRow();
            CM_CLASS cl = new CM_CLASS();
            if (dr == null)
            {
                gcClass3.DataSource = null;
                return;
            }

            string code = gvClass2.GetFocusedRowCellValue("CLASS").ToString();

            cl.CLASSLEVEL = 2;
            cl.HIGHCLASS = code;

            gcClass3.DataSource = cl.Select_Class();
            gvClass3.BestFitColumns();
        }

        private void DisplayGroup4()
        {
            DataRow dr = gvClass3.GetFocusedDataRow();
            CM_CLASS cl = new CM_CLASS();
            if (dr == null)
            {
                gcClass4.DataSource = null;
                return;
            }

            string code = gvClass3.GetFocusedRowCellValue("CLASS").ToString();

            cl.CLASSLEVEL = 3;
            cl.HIGHCLASS = code;

            gcClass4.DataSource = cl.Select_Class();
            gvClass4.BestFitColumns();
        }

        private void gvClass1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayGroup2();
            DisplayGroup3();
            DisplayGroup4();
        }

        private void gvClass2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayGroup3();
            DisplayGroup4();
        }

        private void gvClass3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayGroup4();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 각 GridView별 편집 완료 처리
                gvClass1.PostEditor();
                gvClass1.UpdateCurrentRow();

                gvClass2.PostEditor();
                gvClass2.UpdateCurrentRow();

                gvClass3.PostEditor();
                gvClass3.UpdateCurrentRow();

                gvClass4.PostEditor();
                gvClass4.UpdateCurrentRow();


                DataTable dt1 = gcClass1.DataSource as DataTable;
                DataTable dt2 = gcClass2.DataSource as DataTable;
                DataTable dt3 = gcClass3.DataSource as DataTable;
                DataTable dt4 = gcClass4.DataSource as DataTable;

                if (!Check_SaveClick(dt1)) return;
                if (!Check_SaveClick(dt2)) return;
                if (!Check_SaveClick(dt3)) return;
                if (!Check_SaveClick(dt4)) return;

                for (int curHandle = 0; curHandle < dt1.Rows.Count; curHandle++)
                {
                    if (Convert.ToString(dt1.Rows[curHandle]["Status"]) == "신규")
                    {
                        if (!Save_ToDatabase(dt1.Rows[curHandle], 'I')) return;
                    }
                    else if (Convert.ToString(dt1.Rows[curHandle]["Status"]) == "수정")
                    {
                        if (!Save_ToDatabase(dt1.Rows[curHandle], 'U')) return;
                    }
                }

                if (dt2 != null)
                {
                    for (int curHandle = 0; curHandle < dt2.Rows.Count; curHandle++)
                    {
                        if (Convert.ToString(dt2.Rows[curHandle]["Status"]) == "신규")
                        {
                            if (!Save_ToDatabase(dt2.Rows[curHandle], 'I')) return;
                        }
                        else if (Convert.ToString(dt2.Rows[curHandle]["Status"]) == "수정")
                        {
                            if (!Save_ToDatabase(dt2.Rows[curHandle], 'U')) return;
                        }
                    }
                }

                if (dt3 != null)
                {
                    for (int curHandle = 0; curHandle < dt3.Rows.Count; curHandle++)
                    {
                        if (Convert.ToString(dt3.Rows[curHandle]["Status"]) == "신규")
                        {
                            if (!Save_ToDatabase(dt3.Rows[curHandle], 'I')) return;
                        }
                        else if (Convert.ToString(dt3.Rows[curHandle]["Status"]) == "수정")
                        {
                            if (!Save_ToDatabase(dt3.Rows[curHandle], 'U')) return;
                        }
                    }
                }

                if (dt4 != null)
                {
                    for (int curHandle = 0; curHandle < dt4.Rows.Count; curHandle++)
                    {
                        if (Convert.ToString(dt4.Rows[curHandle]["Status"]) == "신규")
                        {
                            if (!Save_ToDatabase(dt4.Rows[curHandle], 'I')) return;
                        }
                        else if (Convert.ToString(dt4.Rows[curHandle]["Status"]) == "수정")
                        {
                            if (!Save_ToDatabase(dt4.Rows[curHandle], 'U')) return;
                        }
                    }
                }
                MessageBox.Show("저장이 정상적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_CLASS cl = new CM_CLASS();
            cl.CLASS = Convert.ToString(dr["CLASS"]);
            cl.CLASSLEVEL = Convert.ToInt32(dr["CLASSLEVEL"]);
            cl.CLASSNAME = Convert.ToString(dr["CLASSNAME"]);
            cl.HIGHCLASS = Convert.ToString(dr["HIGHCLASS"]);
            cl.ROOTCLASS = Convert.ToString(dr["ROOTCLASS"]);
            cl.DisplayYn = Convert.ToString(dr["DisplayYn"]);

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!cl.Ins_Class())
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
                if (!cl.Upd_Class())
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

        private bool Check_SaveClick(DataTable dt)
        {
            // 불러온 상태인지 검사
            if (dt != null)
            {
                // 미입력된 칸 있는지 확인
                foreach (DataRow dr in dt.Rows)
                {
                    string status = dr["Status"].ConvertString_Null_To_Empty();

                    if (status == "신규" && (string.IsNullOrEmpty(dr["CLASS"].ToString()) || string.IsNullOrEmpty(dr["CLASSNAME"].ToString())))
                    {
                        MessageBox.Show("코드와 명칭을 모두 입력해주세요.");
                        return false;
                    }
                }
            }

            return true;
        }

        private void gvClass1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            dr["Status"] = "신규";
            dr["CLASSLEVEL"] = 0;
            dr["DisplayYn"] = "Y";
        }

        private void gvClass2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            dr["Status"] = "신규";
            dr["CLASSLEVEL"] = 1;
            dr["DisplayYn"] = "Y";
        }

        private void gvClass3_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            dr["Status"] = "신규";
            dr["CLASSLEVEL"] = 2;
            dr["DisplayYn"] = "Y";
        }

        private void gvClass4_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            dr["Status"] = "신규";
            dr["CLASSLEVEL"] = 3;
            dr["DisplayYn"] = "Y";
        }

        private void gvClass1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.FieldName == "CLASS")
            {
                DataRow dr = gv.GetDataRow(e.RowHandle);

                dr["HIGHCLASS"] = e.Value;
                dr["ROOTCLASS"] = e.Value;

                DataTable dt2 = gcClass2.DataSource as DataTable;

                if (dt2 != null)
                {
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        dr2["HIGHCLASS"] = e.Value;
                        dr2["ROOTCLASS"] = e.Value;
                    }

                    DataTable dt3 = gcClass3.DataSource as DataTable;

                    if (dt3 != null)
                    {
                        foreach (DataRow dr3 in dt3.Rows)
                        {
                            dr3["ROOTCLASS"] = e.Value;
                        }

                        DataTable dt4 = gcClass4.DataSource as DataTable;

                        if (dt4 != null)
                        {
                            foreach (DataRow dr4 in dt4.Rows)
                            {
                                dr4["ROOTCLASS"] = e.Value;
                            }
                        }
                    }
                }
            }

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void gvClass2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.FieldName == "CLASS")
            {
                DataRow drHigh = gvClass1.GetFocusedDataRow();
                DataRow dr = gv.GetDataRow(e.RowHandle);

                dr["HIGHCLASS"] = drHigh["CLASS"];
                dr["ROOTCLASS"] = drHigh["CLASS"];

                DataTable dt3 = gcClass3.DataSource as DataTable;

                if (dt3 != null)
                {
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        dr3["HIGHCLASS"] = e.Value;
                    }
                }
            }

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void gvClass3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.FieldName == "CLASS")
            {
                DataRow drRoot = gvClass1.GetFocusedDataRow();
                DataRow drHigh = gvClass2.GetFocusedDataRow();
                DataRow dr = gv.GetDataRow(e.RowHandle);

                dr["HIGHCLASS"] = drHigh["CLASS"];
                dr["ROOTCLASS"] = drRoot["CLASS"];

                DataTable dt4 = gcClass4.DataSource as DataTable;

                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        dr4["HIGHCLASS"] = e.Value;
                    }
                }
            }

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void gvClass4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.FieldName == "CLASS")
            {
                DataRow drRoot = gvClass1.GetFocusedDataRow();
                DataRow drHigh = gvClass3.GetFocusedDataRow();
                DataRow dr = gv.GetDataRow(e.RowHandle);

                dr["HIGHCLASS"] = drHigh["CLASS"];
                dr["ROOTCLASS"] = drRoot["CLASS"];
            }

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void gvClass1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;

            if (gv.FocusedColumn.FieldName == "CLASS")
            {
                string status = gv.GetRowCellValue(gv.FocusedRowHandle, "Status")?.ToString();
                string cl = gv.GetRowCellValue(gv.FocusedRowHandle, "CLASS")?.ToString();

                if (status == "" && cl != "")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvClass2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvClass1.GetFocusedDataRow() == null)
            {
                e.Cancel = true;
                return;
            }

            if (gvClass1.GetFocusedRowCellValue("CLASS").ToString() == "")
            {
                e.Cancel = true;
                return;
            }

            GridView gv = sender as GridView;

            if (gv.FocusedColumn.FieldName == "CLASS")
            {
                string status = gv.GetRowCellValue(gv.FocusedRowHandle, "Status")?.ToString();
                string cl = gv.GetRowCellValue(gv.FocusedRowHandle, "CLASS")?.ToString();

                if (status == "" && cl != "")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvClass3_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvClass2.GetFocusedDataRow() == null)
            {
                e.Cancel = true;
                return;
            }

            if (gvClass2.GetFocusedRowCellValue("CLASS").ToString() == "")
            {
                e.Cancel = true;
                return;
            }

            GridView gv = sender as GridView;

            if (gv.FocusedColumn.FieldName == "CLASS")
            {
                string status = gv.GetRowCellValue(gv.FocusedRowHandle, "Status")?.ToString();
                string cl = gv.GetRowCellValue(gv.FocusedRowHandle, "CLASS")?.ToString();

                if (status == "" && cl != "")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvClass4_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvClass3.GetFocusedDataRow() == null)
            {
                e.Cancel = true;
                return;
            }

            if (gvClass3.GetFocusedRowCellValue("CLASS").ToString() == "")
            {
                e.Cancel = true;
                return;
            }

            GridView gv = sender as GridView;

            if (gv.FocusedColumn.FieldName == "CLASS")
            {
                string status = gv.GetRowCellValue(gv.FocusedRowHandle, "Status")?.ToString();
                string cl = gv.GetRowCellValue(gv.FocusedRowHandle, "CLASS")?.ToString();

                if (status == "" && cl != "")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvClass_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = gv.FocusedColumn.FieldName;

            if (fieldName == "CLASS")
            {
                CM_CLASS cla = new CM_CLASS();
                cla.CLASS = Convert.ToString(e.Value);

                if (cla.Select_Class_by_Key() != null)
                {
                    e.Valid = false;
                    e.ErrorText = "중복된 코드가 있습니다.";
                }
            }
        }

        private void class2DelBtn_Click(object sender, EventArgs e)
        {
            string code = (gvClass2.GetFocusedRowCellValue("CLASS") ?? "").ToString();
            CM_CLASS cla = new CM_CLASS();
            DataTable dt3 = gcClass3.DataSource as DataTable;

            if (MessageBox.Show("선택한 분류를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            if (dt3 != null && dt3.Rows.Count > 0)
            {
                if (MessageBox.Show("선택한 분류를 삭제하면 하위 분류가 삭제됩니다. 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                foreach (DataRow dr in dt3.Rows)
                {
                    cla.CLASS = Convert.ToString(dr["CLASS"]);
                    if (!cla.Del_Class())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
                    }
                }
            }

            cla.CLASS = code;

            if (cla.Del_Class())
            {
                DisplayGroup2();
                DisplayGroup3();
                DisplayGroup4();
                MessageBox.Show("선택한 분류를 삭제 하였습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

        private void class3DelBtn_Click(object sender, EventArgs e)
        {
            string code = (gvClass3.GetFocusedRowCellValue("CLASS") ?? "").ToString();
            DataTable dt4 = gcClass4.DataSource as DataTable;

            if (MessageBox.Show("선택한 분류를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            if (dt4 != null && dt4.Rows.Count > 0)
            {
                if (MessageBox.Show("선택한 분류를 삭제하면 하위 분류가 삭제됩니다. 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }

            CM_CLASS cla = new CM_CLASS();
            cla.CLASS = code;

            if (cla.Del_Class())
            {
                DisplayGroup3();
                DisplayGroup4();
                MessageBox.Show("선택한 분류를 삭제 하였습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

        private void class4DelBtn_Click(object sender, EventArgs e)
        {
            string code = (gvClass4.GetFocusedRowCellValue("CLASS") ?? "").ToString();

            if (MessageBox.Show("선택한 분류를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            CM_CLASS cla = new CM_CLASS();
            cla.CLASS = code;

            if (cla.Del_Class())
            {
                DisplayGroup4();
                MessageBox.Show("선택한 분류를 삭제 하였습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

		private void cmdDelete_Click(object sender, EventArgs e)
		{
            string code = (gvClass1.GetFocusedRowCellValue("CLASS") ?? "").ToString();
            DataTable dt2 = gcClass2.DataSource as DataTable;

            if (MessageBox.Show("선택한 분류를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            if (dt2.Rows.Count > 0)
            {
                if (MessageBox.Show("선택한 분류를 삭제하면 하위 분류가 삭제됩니다. 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }

            CM_CLASS cla = new CM_CLASS();
            cla.ROOTCLASS = code;

            if (cla.Del_Class_Root())
            {
                DisplayGroup1();
                MessageBox.Show("선택한 분류를 삭제 하였습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
        }
	}
}