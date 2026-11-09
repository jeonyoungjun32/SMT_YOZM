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
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_Warehouse : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        Database db = new Database();
        string query = "";

        public Frm_CM_Warehouse()
        {
            InitializeComponent();

            wareTypeColLookUpEdit1.DataSource = code.smtLookUp("창고종류", "", "");
            wareTypeColLookUpEdit1.ValueMember = "코드";
            wareTypeColLookUpEdit1.DisplayMember = "코드명";

            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 생성
            Set_GridView(gvCM_Warehouse);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(gvCM_Warehouse, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItemSearchLookUpEdit lup_Stock = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A020", false), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "창고코드", "HouseID");
            grid.Create_GvColumn(gv, "창고명", "Title");
            grid.Create_GvColumn(gv, "창고분류", "WareType", lup_Stock);
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "창고존여부", "ZoneYn");
            grid.Create_GvColumn(gv, "사용여부", "UseYn");

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            gv.Columns["ZoneYn"].ColumnEdit = rpo.Create_rpoChk();
            gv.Columns["UseYn"].ColumnEdit = rpo.Create_rpoChk();

            //자동채번이 아닐 경우 코드값 편집 허용
            //if (WrGlobal.ProcOption["BS"]["Line_AutoNo"] != "Y")
            //{
                grid.Column_AllowEdit(gv.Columns["HouseID"]);
            //}

            grid.Column_AllowEdit(gv.Columns["Title"]);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["WareType"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["ZoneYn"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            gv.Columns["ZoneYn"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["UseYn"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            // 기존 코드는 다른 테이블과 연결되므로 신규 등록 행에서만 입력합니다.
            gv.ShowingEditor += (sender, args) =>
            {
                var row = gv.GetFocusedDataRow();
                if (gv.FocusedColumn.FieldName == "HouseID" && row != null)
                    args.Cancel = row["Status"].ConvertString_Null_To_Empty() != "신규";
            };
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

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            grid.Check_OverLap_CellValue(sender as GridView, e, "HouseID");
        }

        #endregion

        #region 조회 기능

        private void Frm_CM_Warehouse_Load(object sender, EventArgs e)
        {
            if (WrGlobal.AuthorList.Exists(s => s.MenuID == this.Tag.ToString() && s.AuthorCode == "02"))
            {
                cmdInsert.Visible = true;
                cmdSave.Visible = true;
                cmdDelete.Visible = true;
            }

            gvCM_Warehouse.Columns["ZoneYn"].Visible = false; //창고존유무 컬럼 숨김 처리
            //gridColumn1.Visible = false;

            DisplayData();
        }

        private void DisplayData()
        {
            // 1. 조회 검사
            if (!Check_Search()) return;

            CM_Warehouse cm = new CM_Warehouse();
            gcCM_Warehouse.DataSource = cm.Select_CM_Warehouse();
            gvCM_Warehouse.BestFitColumns();
        }

        private bool Check_Search()
        {
            if ((gvCM_Warehouse.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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
        #endregion

        // 창고 그리드 포커스 변경시
        private void gvCM_Warehouse_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view != null && view.IsValidRowHandle(e.FocusedRowHandle))
            //{
            //    DataRowView drv = (DataRowView)CM_WarehouseBindingSource.Current;
            //    if (drv.Row.RowState == DataRowState.Added)
            //    {
            //        view.Columns["CorpId"].OptionsColumn.AllowEdit = true;
            //        view.Columns["HouseID"].OptionsColumn.AllowEdit = true;
            //    }
            //    else
            //    {
            //        view.Columns["CorpId"].OptionsColumn.AllowEdit = false;
            //        view.Columns["HouseID"].OptionsColumn.AllowEdit = false;
            //    }
            //}
            //else
            //{
            //    view.Columns["CorpId"].OptionsColumn.AllowEdit = false;
            //    view.Columns["HouseID"].OptionsColumn.AllowEdit = false;
            //}
        }

        #region 행 삽입, 추가, 삭제
        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기

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
                    dic.Add("UseYn", "Y");
                    dic.Add("ZoneYn", "N");
                }
                else
                {
                    //삭제 진행인경우
                    dic.Add("Status", Convert.ToString(gvCM_Warehouse.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }
                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(gcCM_Warehouse, dic, txt_Act);
            }
            else
            {
                throw new ArgumentException("유효하지 않은 버튼입니다.");
            }
        }

        #endregion


        #region 저장 기능

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            gvCM_Warehouse.PostEditor();
            gvCM_Warehouse.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = gcCM_Warehouse.DataSource as DataTable;

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
            DisplayData();
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

            foreach (DataRow row in dt.Rows)
            {
                string state = row["Status"].ConvertString_Null_To_Empty();
                if (state != "신규" && state != "수정") continue;
                if (string.IsNullOrWhiteSpace(Convert.ToString(row["HouseID"]))
                    || string.IsNullOrWhiteSpace(Convert.ToString(row["Title"]))
                    || string.IsNullOrWhiteSpace(Convert.ToString(row["WareType"])))
                {
                    MessageBox.Show("창고코드·창고명·창고분류를 입력하세요.");
                    return false;
                }
                if (Convert.ToString(row["HouseID"]).Length > 10)
                {
                    MessageBox.Show("입출고 데이터와 호환되도록 창고코드는 10자 이내로 입력하세요.");
                    return false;
                }
            }

            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                return dialogResult == DialogResult.OK;
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_Warehouse wh = new CM_Warehouse();
            wh.CorpId = WrGlobal.CorpID;
            wh.HouseID = dr["HouseID"].ConvertString_Null_To_Empty();
            wh.Title = dr["Title"].ConvertString_Null_To_Empty();
            wh.WareType = dr["WareType"].ConvertString_Null_To_Empty();
            wh.WareKind = dr["WareKind"].ConvertString_Null_To_Empty();
            wh.Remark = dr["Remark"].ConvertString_Null_To_Empty();
            wh.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
            wh.ZoneYn = dr["ZoneYn"].ConvertString_Null_To_Empty();
            // 화면에서 숨긴 정보도 조회 원본 그대로 전달하여 저장 시 NULL로 지워지지 않게 합니다.
            wh.WareState = dr["WareState"].ConvertString_Null_To_Empty();
            wh.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
            wh.DeptCode = dr["DeptCode"].ConvertString_Null_To_Empty();
            wh.Tel = dr["Tel"].ConvertString_Null_To_Empty();
            wh.Fax = dr["Fax"].ConvertString_Null_To_Empty();
            wh.Address = dr["Address"].ConvertString_Null_To_Empty();
            wh.CloseDate = dr.IsNull("CloseDate") ? null
                : Convert.ToDateTime(dr["CloseDate"]).ToString("yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

            if (div != 'D')
            {
                //Delete제외 공통
                wh.Title = Convert.ToString(dr["Title"]);          //창고명
            }

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    //Insert
                    if (!wh.Ins_CM_Warehouse())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!wh.Upd_CM_Warehouse())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!wh.Del_CM_Warehouse())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
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

        #endregion


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvCM_Warehouse.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }
    }
}