using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebViewer.View.Document
{
    public partial class DownFile : System.Web.UI.Page
    {
        string path = @"\\192.168.1.8\edmsdata$";
        protected void Page_Load(object sender, EventArgs e)
        {
            string fullFileName = path + Request.QueryString["name"];

            //fullFileName = @"C:\Temp\B100107597.tif";
            clsDatabase db = new clsDatabase();

            string query = string.Format("Select * From {0} Where DocumentKey = '{1}'", (Request.QueryString["table"] ?? ""), (Request.QueryString["key"] ?? ""));
            DataRowView drv = db.GetDataRecord(query);

            query = string.Format("exec Code_UseSession '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}'"
                                , Session["CorpId"], Session["EmpNo"], Session["SessionNo"], (Request.QueryString["id"] ?? ""), "Download"
                                , Request.QueryString["pid"]
                                , Request.QueryString["key"]
                                , drv["DocumentNo"].ToString()
                                , drv["DocumentRev"].ToString()
                                , drv["Date_Stored"].ToString()
                                , drv["Date_Last_Chgd"].ToString());
            db.ExecuteQuery(query);

            byte[] imgByte = File.ReadAllBytes(fullFileName);
            string fileName = drv["DocumentNo"].ToString() + "_" + drv["SheetNumber"].ToString() + "_" + drv["DocumentRev"].ToString() + "." + fullFileName.Substring(fullFileName.LastIndexOf(".") + 1);
            string browser = Request.Browser.Type.ToUpper();
            string userAgent = Request.UserAgent.ToUpper();
            bool isIE = browser.StartsWith("IE") || browser.Contains("INTERNETEXPLORER") || userAgent.Contains("TRIDENT") || userAgent.Contains("MSIE");
            bool isEdge = userAgent.Contains("EDGE");
            if (isIE || isEdge)
                fileName = HttpUtility.UrlEncode(fileName, UTF8Encoding.UTF8).Replace("+", " ");
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Transfer-Encoding", "Binary");
            Response.BinaryWrite(imgByte);
            Response.Flush();
            Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}