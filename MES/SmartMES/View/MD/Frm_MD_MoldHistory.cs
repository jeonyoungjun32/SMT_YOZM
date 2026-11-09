using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.MD;
using SmartMES.Models.PM;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_MoldHistory : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();

        public Frm_MD_MoldHistory()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_GvRepair(Gv_Mold);
            Set_SearchLup();
        }

        private void Set_SearchLup()
        {
            //LookUp Setting
            cls_Lookup clsLup = new cls_Lookup();
            MD_Mold Mold = new MD_Mold();

            DataTable dt_Mold = Mold.Select_Mold_Mgmt_No(true);
            clsLup.Create_RepositoryItemSearchLookUp(lupMold, dt_Mold, "코드", "금형명");
            lupMold.smtLookUpValue = "ALL";
        }

        #region GridView Setting

        //설비리스트

        //설비수리이력그리드
        private void Set_GvRepair(GridView gv)
        {
            //GvRepair Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName");
            grid.Create_GvColumn(gv, "실적일자", "WorkDate");
            grid.Create_GvColumn(gv, "실적번호", "OrderNo");
            grid.Create_GvColumn(gv, "실적순번", "OrderSeq");
            //grid.Create_GvColumn(gv, "차종"        , "CarType");
            //grid.Create_GvColumn(gv, "품번"        , "Alias");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            //grid.Create_GvColumn(gv, "수명SHOT"  , "LifeShot");
            //grid.Create_GvColumn(gv, "초기SHOT"  , "InitShot");
            grid.Create_GvColumn(gv, "실적량", "WorkQty");
            grid.Create_GvColumn(gv, "작업 Shot", "Shot");
            //grid.Create_GvColumn(gv, "사용한계"  , "LastShot");

            // 수정여부
            grid.Column_AllowEdit(gv.Columns["Shot"]);

            //셀 속성
            //grid.SetCol_Type(gv.Columns["LifeShot"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["InitShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Shot"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["LastShot"], clsGrid.TypeList.TInt);

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인

            gv.Columns["MoldCode"].Visible = false;
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
            clsGrid grid = new clsGrid();
            grid.Check_OverLap_CellValue(sender as GridView, e, "MoldCode");
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_MoldHistory_Load(object sender, EventArgs e)
        {
            Func_Search();
        }

        #endregion Event End

        #region 조회 기능

        private void BtnSearch_Mold_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            MD_Mold Mold = new MD_Mold();
            Mold.MoldCode = lupMold.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_Mold.DataSource = Mold.Select_MoldHistory();

            int LifeShot = 0;      //수명SHOT
            int InitShot = 0;      //초기SHOT
            int Shot = 0;           //작업량
            int LastShot = 0;      //사용한계

            for (int i = 0; i < Gv_Mold.RowCount; i++)
            {
                LifeShot = int.Parse(Gv_Mold.GetRowCellValue(i, "LifeShot").ToString());
                InitShot = int.Parse(Gv_Mold.GetRowCellValue(i, "InitShot").ToString());
                Shot = int.Parse(Gv_Mold.GetRowCellValue(i, "Shot").ToString());

                if (i == 0)
                {
                    LastShot = LifeShot - InitShot - Shot;
                }
                else
                {
                    LastShot = int.Parse(Gv_Mold.GetRowCellValue(i - 1, "LastShot").ToString());
                    LastShot = LastShot - Shot;
                }

                Gv_Mold.SetRowCellValue(i, "LastShot", LastShot.ToString());
            }

            Gv_Mold.BestFitColumns();
        }

        #endregion 조회 기능 End

        #region 저장 기능

        private void BtnSave_Mold_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            GridView gv = Gv_Mold;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = Gc_Mold.DataSource as DataTable;

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

            //선택한 행 번호 저장
            int SaveFocusedRowInx = Gv_Mold.FocusedRowHandle;

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            //3. 정상절차 진행
            Func_Search();


            //저장 후 해당 행으로 focus이동
            Gv_Mold.FocusedRowHandle = SaveFocusedRowInx;
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            //삭제내역 있으면 물어보기
            if (dt.Select("Status = '삭제'").Length > 0)
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

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            PM_OrderResult pr = new PM_OrderResult();

            try
            {
                pr.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
                pr.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
                pr.Shot = dr["Shot"].ConvertInt_Null_To_Zero();
            }
            catch
            {
                MessageBox.Show("값 형태변환 에러");
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                //if (!Mold.Ins_Repair())
                //{
                //    //저장 중 에러 발생시
                //    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                //    return false;
                //}

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'U')
            {
                //Update
                if (!pr.Upd_Shot())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else
            if (div == 'D')
            {
                //Delete
                //if (!Mold.Del_Repair())
                //{
                //    //저장 중 에러 발생시
                //    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                //    return false;
                //}

                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr[0] = "";

            return true;
        }

        #endregion 저장 기능 End

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_Mold);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}