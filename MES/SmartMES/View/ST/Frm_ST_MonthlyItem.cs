using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.ST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_MonthlyItem : DevExpress.XtraEditors.XtraForm
    {
        string FormType = "";
        string[] m_MaterialType = new string[] { "40" };   //10 : 상품  20 : 제품  30 : 반제품  40 : 원자재  50 : 부자재  60 : 샘플/개발  70 : 배출자재  80 : 배출자재_황동  90 : 배출자재_알루미늄
        //string stockType =  "10,20,30";  
        string[] stockType = { "10", "20", "30" }; //10 : 원자재 20 : 제품 30 : 공정

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        public Frm_ST_MonthlyItem(string FT)
        {
            InitializeComponent();
            FormType = FT;
            if(FormType == "자재")
            {
                m_MaterialType = new string[] { "30","40","50" };
                stockType = new string[] { "10"};
                GroupControlTitle.Text = "자재리스트";
            }
            else if(FormType == "제품")
            {
                m_MaterialType = new string[] { "10","20","30" };
                stockType = new string[] { "20", "30" };
                GroupControlTitle.Text = "제품리스트";
            }
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //DateEdit 설정
            barEditDate.Properties.Mask.EditMask = "yyyy-MM";
            barEditDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            barEditDate.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
            DateTime date = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy-MM"));
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

            CM_CateItem item = new CM_CateItem();
            item.Class = "A011";
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(item.Select_LupItem(), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "창고", "WareHouseCode");
            grid.Create_GvColumn(gv, "창고명", "Warehouse");
            grid.Create_GvColumn(gv, "품목코드", "ProductCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "단위", "UnitCode", lup_UnitCode);
            grid.Create_GvColumn(gv, "입고수량", "InQty");
            grid.Create_GvColumn(gv, "출고수량", "OutQty");
            grid.Create_GvColumn(gv, "확인수량", "AuditQty");

            // Column 숨김
            gv.Columns["AuditQty"].Visible = false;

            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["AuditQty"], clsGrid.TypeList.TFloat);

            // 그룹 설정
            gv.OptionsView.ShowFooter = true;   //하단 합계창 보이기

            grid.SetCol_Summary(gv, "InQty", DevExpress.Data.SummaryItemType.Sum, "{0:N3}");
            grid.SetCol_Summary(gv, "OutQty", DevExpress.Data.SummaryItemType.Sum, "{0:N3}");
            grid.SetCol_Summary(gv, "AuditQty", DevExpress.Data.SummaryItemType.Sum, "{0:N3}");
        }
        #endregion GridView Setting

        private void Set_SearchLup()
        {
            //Lup Settting
            smtHouseCode.smtLookUpDataSource = code.GetWarehouseList_Dt(true, stockType); ;
            smtHouseCode.smtLookUpDataDisplayMember = "창고명";
            smtHouseCode.smtLookUpDataValueMember = "창고코드";
            smtHouseCode.smtLookUpValue = "ALL";

            ssCmMaterial.smtLookUpDataSource = code.GetMaterialList(true, m_MaterialType);
            ssCmMaterial.smtLookUpDataValueMember = "품목코드";
            ssCmMaterial.smtLookUpDataDisplayMember = "품명";
            ssCmMaterial.smtLookUpValue = "ALL";

            smtHouseCode.smtLookUpEditValueChanged += Lup_EditValueChanged;
            ssCmMaterial.smtLookUpEditValueChanged += Lup_EditValueChanged;
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

            string strDate = barEditDate.EditValue.ConvertDate_To_Char6(); // "yyyyMM" 형태
            int year = int.Parse(strDate.Substring(0, 4));
            int month = int.Parse(strDate.Substring(4, 2));

            // 해당 월의 마지막 날 계산
            int lastDay = DateTime.DaysInMonth(year, month);

            string Date = $"{year}{month:D2}{lastDay:D2}";

            stock.ItemType = m_MaterialType;
            stock.WareHouseCode = smtHouseCode.smtLookUpValue.ConvertString_Null_To_Empty();
            stock.ItemCode =  ssCmMaterial.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_MonthlyMaterial.DataSource = stock.Select_ST_MonthlyMaterial("", Date);
            Gv_MonthlyMaterial.BestFitColumns();
        }

        #endregion 데이터 조회 End

        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            Set_GridView();
            Search();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}