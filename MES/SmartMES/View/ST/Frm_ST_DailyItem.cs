using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using SmartCommon.Common;
using SmartMES.Models.ST;

using System;
using DevExpress.XtraEditors;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_DailyItem : DevExpress.XtraEditors.XtraForm
    {
        string FormType = "";
        string[] m_MaterialType = new string[] { "40" };   //10 : 상품  20 : 제품  30 : 반제품  40 : 원자재  50 : 부자재  60 : 샘플/개발  70 : 배출자재  80 : 배출자재_황동  90 : 배출자재_알루미늄
        string stockType =  "10,20,30";  //10 : 원자재 20 : 제품 30 : 공정

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        public Frm_ST_DailyItem(string FT)
        {
            InitializeComponent();
            FormType = FT;
            if(FormType == "자재")
            {
                m_MaterialType = new string[] { "30","40","50" };
                stockType = "10,30";
                GroupControlTitle.Text = "자재리스트";
            }
            else if(FormType == "제품")
            {
                m_MaterialType = new string[] { "10","20","30" };
                stockType = "20,30";
                GroupControlTitle.Text = "제품리스트";
            }
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //DateEdit 설정
            barEditDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            barEditDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            barEditDate.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.MonthView;
            DateTime date = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy-MM-dd"));
            barEditDate.EditValue = date;

            //GridView 설정
            Set_GridView();

            //SerchLookUp 설정
            Set_SearchLup();

            //Excel Down
            GridView gv = Gc_MonthlyMaterial.MainView as GridView;
            //this.btn_Excel. DownGridControl = Gc_MonthlyMaterial;
            //this.btn_ExcelDown.DownGridView = gv;
        }

        #region GridView Setting

        private void Set_GridView()
        {
            Set_GvIn(Gv_MonthlyMaterial);
        }

        private void Set_GvIn(GridView gv)
        {
            gv.Columns.Clear();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "창고", "WareHouseCode");
            grid.Create_GvColumn(gv, "창고명", "Warehouse");
            grid.Create_GvColumn(gv, "품목코드", "ProductCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "단위", "UnitCode");
            grid.Create_GvColumn(gv, "입고수량", "InQty");
            grid.Create_GvColumn(gv, "출고수량", "OutQty");
            grid.Create_GvColumn(gv, "확인수량", "AuditQty");

            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["AuditQty"], clsGrid.TypeList.TInt);

            // Column 숨김
            gv.Columns["AuditQty"].Visible = false;

            // 그룹 설정
            gv.OptionsView.ShowFooter = true;   //하단 합계창 보이기

            grid.SetCol_Summary(gv, "InQty");
            grid.SetCol_Summary(gv, "OutQty");
            grid.SetCol_Summary(gv, "AuditQty");
        }
        #endregion GridView Setting

        private void Set_SearchLup()
        {
            //Lup Settting
            smtHouseCode.smtLookUpDataSource = code.smtLookUp("창고정보",stockType, "전체");
            Lup_Item.smtLookUpDataSource = code.smtLookUp("자재품목", m_MaterialType, "전체");

            //품목
            Lup_Item.smtLookUpDataSource = code.GetMaterialList(true, m_MaterialType);
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";
        }
        #endregion 디자인 아이템 생성 End

        private void Frm_MT_MatOut_Load(object sender, EventArgs e)
        {
            Search();
        }

        #region 데이터 조회

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Set_GridView();
            Search();
        }

        private void Search()
        {
            //데이터 조회
            ST_StockD stock = new ST_StockD();

            string Date = barEditDate.EditValue.ConvertDate_To_Char8(); 

            stock.ItemType = m_MaterialType;
            stock.WareHouseCode = smtHouseCode.smtLookUpValue.ConvertString_Null_To_Empty();
            stock.ItemCode =  Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_MonthlyMaterial.DataSource = stock.Select_ST_MonthlyMaterial("", Date);
            Gv_MonthlyMaterial.BestFitColumns();
        }

        #endregion 데이터 조회 End

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}