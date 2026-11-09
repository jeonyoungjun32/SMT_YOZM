using DevExpress.Web.Internal.XmlProcessor;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Web.ClientControls;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MobileViewer
{
    public class CustomOperationLogger : WebDocumentViewerOperationLogger
    {
        public override ExportedDocument ExportDocumentStarting(string documentId, string asyncExportOperationId, string format, ExportOptions options, PrintingSystemBase printingSystem, Func<ExportedDocument> doExportSynchronously)
        {
            clsDatabase db = new clsDatabase();

            string query = string.Format("Select * From {0} Where DocumentKey in({1}) Order By DocumentKey", HttpContext.Current.Session["DocTable"], HttpContext.Current.Session["DocKey"]);
            DataView dv = db.GetDataView(HttpContext.Current.Session["DocTable"].ToString(), query);

            foreach (DataRowView drv in dv)
            {
                query = string.Format("exec Code_UseSession '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}'"
                                    , HttpContext.Current.Session["CorpId"]
                                    , HttpContext.Current.Session["EmpNo"]
                                    , HttpContext.Current.Session["SessionNo"]
                                    , HttpContext.Current.Session["DocMenuId"]
                                    , "Print"
                                    , HttpContext.Current.Session["DocProjectId"]
                                    , drv["DocumentKey"].ToString()
                                    , drv["DocumentNo"].ToString()
                                    , drv["DocumentRev"].ToString()
                                    , drv["Date_Stored"].ToString()
                                    , drv["Date_Last_Chgd"].ToString());
                db.ExecuteQuery(query);
            }

            return base.ExportDocumentStarting(documentId, asyncExportOperationId, format, options, printingSystem, doExportSynchronously);
        }
    }
}