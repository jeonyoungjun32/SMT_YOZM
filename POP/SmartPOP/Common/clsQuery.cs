using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Common
{
    class clsQuery
    {

        Database db = new Database();
        string query = "";
        public DataRowView GetCustomer(string custCode)
        {
            query = string.Format("Select Top 1 * From CM_Cust Where CustCode = '{0}' or CustName = '{0}'", custCode);
            return db.GetDataRecord(query);
        }
        public DataView GetDeliveryList(string custCode)
        {
            query = string.Format("Select * From CmShipTo Where CustCode = '{0}'", custCode);
            return db.GetDataView("ShipTo", query);
        }
        public DataRowView GetDelivery(string custCode, string shipToCode)
        {
            query = string.Format("Select * From CmShipTo Where CustCode = '{0}' and ShipToCode = '{1}'", custCode, shipToCode);
            return db.GetDataRecord(query);
        }

        public void GetCategory(ref RepositoryItemSearchLookUpEdit searchLookup, string pClass)
        {

            query = string.Format("select ItemCode 코드, Title 내용 from CmCateItem Where Class like '{0}%'", pClass);
            searchLookup.DataSource = db.GetDataView("Type", query);
            searchLookup.DisplayMember = "내용";
            searchLookup.ValueMember = "코드";
        }


        public void GetSearchLookup(ref RepositoryItemSearchLookUpEdit searchLookup, string query)
        {
            searchLookup.DataSource = db.GetDataView("Type", query);
            searchLookup.DisplayMember = "Title";
            searchLookup.ValueMember = "Code";
        }

        public void GetSearchLookup(ref SearchLookUpEdit searchLookup, string query)
        {
            searchLookup.Properties.DataSource = db.GetDataView("Type", query);
            searchLookup.Properties.DisplayMember = "Title";
            searchLookup.Properties.ValueMember = "Code";
        }

        public DataView GetCmCate()
        {
            query = "select ItemCode, Title from CmCateItem where cateid1 = 'Z0' and cateid2 = '10'";
            return db.GetDataView("스펙내역", query);
        }


    }
}
