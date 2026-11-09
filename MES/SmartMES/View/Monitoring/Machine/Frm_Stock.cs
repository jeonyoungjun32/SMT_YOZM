using DevExpress.XtraBars.Docking2010.Views.Widget;
using SmartCommon.Common;
using SmartMES.UserControls;
using System;
using System.Data;

namespace SmartMES.View.Monitoring.Machine
{
    public partial class Frm_Stock : DevExpress.XtraEditors.XtraForm
    {
        StackGroup group1, group2, group3, group4, group5, group6;
        Database db = new Database();
        string query = "";

        public Frm_Stock()
        {
            InitializeComponent();
        }

        public Frm_Stock(string code1 ,string code2)
        {
            InitializeComponent();
        }


        private void Frm_Stock_Load(object sender, EventArgs e)
        {

            AddDocumentManager();
            widgetView1.StackGroups.AddRange(new StackGroup[] { group1, group2, group3, group4 });

            int idx = 0;
            int setGroup;

            query = $@"SELECT TOP 100
                                cm.ItemCode
		                        , CM.ItemName
		                        , ISNULL(A.StockQty, 0) StockQty
                                , ISNULL(CM.SaftyQty, 0) SaftyQty
                        FROM CM_Item CM
                        LEFT JOIN (
		                        SELECT ItemCode
				                        , SUM(StockQty) StockQty
		                        FROM StockListView
		                        GROUP BY ItemCode 
	                        ) A ON A.ItemCode = cm.ItemCode
                        WHERE CM.CorpId = '{WrGlobal.CorpID}'
                        ORDER BY A.StockQty DESC
                        ";

            DataView dv = db.GetDataView("재고현황", query);

            foreach (DataRowView item in dv)
            {
                string ItemCode = item["ItemCode"].ToString();
                string ItemName = item["ItemName"].ToString();
                int stockQty = Convert.ToInt32(item["StockQty"]);
                int saftyQty = Convert.ToInt32(item["SaftyQty"]);

                AddDocuments(ItemCode, ItemName, stockQty, saftyQty);

                StackGroup group;

                setGroup = idx % 6;

                switch (setGroup)
                {
                    case 0: group = group1; break;
                    case 1: group = group2; break;
                    case 2: group = group3; break;
                    case 3: group = group4; break;
                    case 4: group = group5; break;
                    case 5: group = group6; break;
                    default: group = group1; break;
                }

                widgetView1.Controller.Dock(widgetView1.Documents[idx] as DevExpress.XtraBars.Docking2010.Views.Widget.Document, group);

                idx++;
            }
        }


        void AddDocumentManager()
        {
            group1 = new StackGroup();
            group2 = new StackGroup();
            group3 = new StackGroup();
            group4 = new StackGroup();
            group5 = new StackGroup();
            group6 = new StackGroup();
            group1.Length.UnitType = LengthUnitType.Star;

            widgetView1.StackGroups.AddRange(new StackGroup[] { group1, group2, group3, group4, group5, group6 });
        }

        void AddDocuments(string ItemCode, string ItemName, int stockQty, int saftyQty)
        {
            DevExpress.XtraBars.Docking2010.Views.Widget.Document document;
            document = widgetView1.AddDocument(ItemName, "ucStock") as DevExpress.XtraBars.Docking2010.Views.Widget.Document;
            document.Tag = ItemName;
            document.Height = 120;
            widgetView1.AddDocument(new ucStock(stockQty, saftyQty));
        }

        private void WidgetView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            if (e.Document.ControlName == "ucStock")
                e.Control = new ucStock();
            if (e.Document.ControlName == "ucStock")
                e.Control = new ucStock();
        }
    }
}