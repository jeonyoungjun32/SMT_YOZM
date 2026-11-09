using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Windows.Forms;
using SmartCommon.Common;
using System.Data;
using SmartMES.Models.CM;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace SmartMES.View.Lookup
{
    public partial class Frm_LookUpItem : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetData(DataRowView drv);  //델리게이트 선언
        public event SetData onSetData;                 //계약


        clsCode code = new clsCode();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        string ItemType = ""; //P:제품, M:자재, '' : 기타
        string[] ItemDiv = null;

        public Frm_LookUpItem()
        {
            InitializeComponent();
        }

        public Frm_LookUpItem(string ItemType, string[] itemDiv)
        {
            InitializeComponent();
            this.ItemDiv = itemDiv;
        }

        private void Frm_LookUpItem_Load(object sender, EventArgs e)
        {
            SetGridView(GvItem);

            Display();
        }

        private void SetGridView(GridView gv)
		{
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;

            //
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            if (ItemType == "P")
			{
                grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");
			}
			else
			{
                grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");
			}
            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //이벤트 설정
            grid.RowNumber_Indicator(gv);   //행번호 표기
        }

        private void Display()
		{
            CM_Item item = new CM_Item();
            GcItem.DataSource = item.SelectCM_Item(ItemDiv);
            GvItem.BestFitColumns();
		}

		private void BtnSelect_Click(object sender, EventArgs e)
		{
            if (GvItem.SelectedRowsCount < 1)
            {
                MessageBox.Show("등록할 항목을 선택해 주세요.");
                return;
            }

            int rowIdx = 0;

            for (int rowCnt = 0; rowCnt < GvItem.SelectedRowsCount; rowCnt++)
            {
                rowIdx = GvItem.GetSelectedRows()[rowCnt];

                onSetData((DataRowView)GvItem.GetRow(rowIdx));
            }

            GvItem.ClearSelection();


                this.Close();
        }

		private void smtSearchButton1_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
    
}
