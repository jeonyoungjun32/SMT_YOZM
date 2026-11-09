using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MD;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_Mold_Management : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_MD_Mold_Management()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_SearchLookUp();
            Set_GridView();

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Mold, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_SearchLookUp()
        {
            cls_Lookup clsLup = new cls_Lookup();

            MD_Mold mold = new MD_Mold();

            DataTable dt_Mold = mold.Select_Mold_Mgmt_No(true);
            clsLup.Create_RepositoryItemSearchLookUp(lupMold, dt_Mold, "코드", "금형명");
            lupMold.smtLookUpValue = "ALL";
        }

        #region GridView Setting

        private void Set_GridView()
        {
            SetGrid_Mold(Gv_Mold);
            SetGrid_MoldIInOut(Gv_MoldInOut);
            SetGrid_MoldChk(Gv_MoldCheck);
        }

        private void SetGrid_Mold(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            //Repository 설정
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "차수", "Chasu");
            grid.Create_GvColumn(gv, "프로젝트명", "MoldPjtNo");
            grid.Create_GvColumn(gv, "금형관리번호", "MoldMgmtNo");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName");
            grid.Create_GvColumn(gv, "적용TON", "ApplyEquip");
            grid.Create_GvColumn(gv, "금형규격", "MoldStd");

            grid.Create_GvColumn(gv, "CAVITY수", "MoldCavity");
            grid.Create_GvColumn(gv, "초기Shot", "InitShot");
            grid.Create_GvColumn(gv, "수명Shot", "LifeShot");
            grid.Create_GvColumn(gv, "총 누적 Shot", "AccumulateShot");
            grid.Create_GvColumn(gv, "최근세척/점검Shot", "RepairShot");
            grid.Create_GvColumn(gv, "진행타수", "Shot");
            grid.Create_GvColumn(gv, "최근세척/점검일", "RepairDate");
            grid.Create_GvColumn(gv, "관리등급", "ManageRating");
            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            gv.Columns["ManageRating"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //관리등급
            grid.SetCol_Type(gv.Columns["MoldItemCount"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["MoldCavity"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["InitShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["LifeShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Shot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["AccumulateShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["RepairShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["RepairDate"], clsGrid.TypeList.TDate);

            gv.Columns["MoldCode"].Visible = false;
            gv.Columns["ApplyEquip"].Visible = false;

            //이벤트 설정
            grid.RowNumber_Indicator(gv);    //행번호 표기
            //grid.SetInfo_ByObject(gv);
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;   //Row 변경시 하위항목 조회
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);
            string MoldCode = Convert.ToString(dr["MoldCode"]);

            //금형입출고 조회
            Func_Search_Item_InOut(MoldCode);
            //금형점검기록 조회
            Func_Search_Chk(MoldCode);
        }

        private void SetGrid_MoldIInOut(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName");
            grid.Create_GvColumn(gv, "프로젝트명", "MoldPjtNo");
            grid.Create_GvColumn(gv, "금형관리번호", "MoldMgmtNo");
            grid.Create_GvColumn(gv, "금형입고 날짜", "InDate");
            grid.Create_GvColumn(gv, "금형출고 날짜", "OutDate");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            //이벤트 설정
            grid.RowNumber_Indicator(gv);    //행번호 표기

            //Merge
            gv.OptionsView.AllowCellMerge = true;                           // Merge허용
            gv.CellMerge += Gv_CellMerge;   //셀병합 예외처리
        }

        private void SetGrid_MoldChk(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            CM_CateItem cm = new CM_CateItem();
            RepositoryItem lup_InspectDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD40"), "분류코드", "분류명");
            RepositoryItem lup_CycleDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4050"), "분류코드", "분류명");
            RepositoryItem lup_WeekDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4060"), "분류코드", "분류명");
            RepositoryItem lup_ValueTypeDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4070"), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "점검구분", "InspectDivCd", lup_InspectDiv);
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
            grid.Create_GvColumn(gv, "점검주기", "CycleDivCd", lup_CycleDiv);
            grid.Create_GvColumn(gv, "관리요일", "WeekDivCd", lup_WeekDiv);
            grid.Create_GvColumn(gv, "등록형태", "ValueTypeDivCd", lup_ValueTypeDiv);
            grid.Create_GvColumn(gv, "점검결과", "InspectCheck");

            grid.RowNumber_Indicator(gv);    //행번호 표기

            //Merge
            gv.OptionsView.AllowCellMerge = true;                           // Merge허용
            gv.CellMerge += Gv_CellMerge;   //셀병합 예외처리
        }

        private void Gv_CellMerge(object sender, CellMergeEventArgs e)
        {
            //셀 병합시 예외처리
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;

            //머지 활성화
            if (fieldNm == "MoldSN"
                || fieldNm == "MoldCode"
                || fieldNm == "Chasu"
                || fieldNm == "SupplyCust"
                || fieldNm == "ProdtCust"
                || fieldNm == "StandardSpec"
                || fieldNm == "InspectDivCd"
                || fieldNm == "MoldPjtNo"
                || fieldNm == "MoldMgmtNo"
                || fieldNm == "MoldName"
                )
            {
                string v1 = Convert.ToString(e.CellValue1);
                string v2 = Convert.ToString(e.CellValue2);

                e.Merge = (v1 == v2);
                e.Handled = true;
            }
            else
            {
                // 그 외 컬럼은 병합 금지
                e.Merge = false;
                e.Handled = true;
            }
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_Mold_Management_Load(object sender, EventArgs e)
        {
            Func_Search_Mold();
        }

        #endregion Event End

        #region 조회 기능

        private void BtnSearch_Mold_Click(object sender, EventArgs e)
        {
            Func_Search_Mold();
        }

        private void Func_Search_Mold()
        {
            //2. 금형조회
            MD_Mold mold = new MD_Mold();
            mold.MoldCode = lupMold.smtLookUpValue.ConvertString_Null_To_Empty() == "전체" ? "" : lupMold.smtLookUpValue.ConvertString_Null_To_Empty();
            //SC_CmCd sc = new SC_CmCd();
            //sc.Cm_Class = "060";

            Gc_Mold.DataSource = mold.Select_Mold();

            //DataTable dt = sc.Select_CmCd();

            GridView gv = Gv_Mold;
            int AccumulateShot = 0;
            int RepairShot = 0;

            for (int i = 0; i < gv.RowCount; i++)
            {
                DataRow dr = gv.GetDataRow(i);

                AccumulateShot = dr["AccumulateShot"].ConvertInt_Null_To_Zero();
                RepairShot = dr["RepairShot"].ConvertInt_Null_To_Zero();
                dr["Shot"] = AccumulateShot - RepairShot;

                //for (int j = 0; j < dt.Rows.Count; j++)
                //{
                //    if (AccumulateShot < dt.Rows[j]["Attr"].ConvertInt_Null_To_Zero())
                //    {
                //        dr["ManageRating"] = dt.Rows[j]["Cm_Nm"];
                //        break;
                //    }
                //}
            }

            Gv_Mold.BestFitColumns();   //Column 너비 조정
        }

        private void Func_Search_Item_InOut(string MoldCode)
        {
            MD_MoldInOut mi = new MD_MoldInOut();
            mi.MoldCode = MoldCode;

            Gc_MoldInOut.DataSource = mi.Select_MoldInOut();

            Gv_MoldInOut.BestFitColumns();   //Column 너비 조정
        }

        private void Func_Search_Chk(string MoldCode)
        {
            MD_MoldChk_Std chk = new MD_MoldChk_Std();
            chk.MoldCode = MoldCode;

            Gc_MoldCheck.DataSource = chk.Select_MoldChk_Std();

            Gv_MoldCheck.BestFitColumns();   //Column 너비 조정
        }

        #endregion 조회 기능 End

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