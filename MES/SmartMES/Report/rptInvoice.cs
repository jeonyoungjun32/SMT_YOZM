using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Report
{
    public partial class rptInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        Database db = new Database();
        string query;

        public rptInvoice()
        {
            InitializeComponent();

            query = string.Format("Select * from TempInvoice Where CorpId = '{0}' and HostName = '{1}'", WrGlobal.CorpID, System.Net.Dns.GetHostName().ToString());

            bindingSource2.DataSource = db.GetDataView("TempInvoice", query);
        }//end function
    }//end class
}//end namespace
