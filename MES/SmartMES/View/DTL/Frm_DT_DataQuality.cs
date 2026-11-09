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

namespace SmartMES.View.DTL
{
    public partial class Frm_DT_DataQuality : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력
        clsCode code = new clsCode();

        public Frm_DT_DataQuality()
        {
            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            Set_DGPCode(gvDataQuality);
        }

        private void Set_DGPCode(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false;
            gv.OptionsView.ShowGroupPanel = false;

            //Column 생성
            grid.Create_GvColumn(gv, "공정명", "CTCode");
            grid.Create_GvColumn(gv, "설비명", "Equip_Name");
            grid.Create_GvColumn(gv, "품질구분", "DgpName");
            grid.Create_GvColumn(gv, "기준범위", "DgpSpec");
            grid.Create_GvColumn(gv, "측정값", "Equip_Name");
            grid.Create_GvColumn(gv, "발생일시", "MemoryCode");


            RepositoryItemSearchLookUpEdit lup_MachineCode = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList(false), "설비코드", "설비명");
            gv.Columns["Equip_Name"].ColumnEdit = lup_MachineCode;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;        //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;               //삭제시 배경색 지정
        }

        #region Event

        private void frmPLCSetting_Load(object sender, EventArgs e)
        {
            SearchDataQuality();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdDisplay": SearchDataQuality(); break;

                case "Btn_Reset": Count_Reset(); break;
            }
        }

        #endregion Event End

        #region 조회 기능

        #region 품목 검사항목 조회

        private void SearchDataQuality()
        {
            IF_DGPCode mc = new IF_DGPCode();

            //DataTable dt = mc.Select_IF_DGPCode();
            //gcDataQuality.DataSource = dt;
            //gvDataQuality.BestFitColumns();
        }


        #endregion 품목 검사항목 조회 End

        #endregion 조회버튼 클릭 End

        private void Count_Reset()
        {
            GridView gv = gvDataQuality;
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