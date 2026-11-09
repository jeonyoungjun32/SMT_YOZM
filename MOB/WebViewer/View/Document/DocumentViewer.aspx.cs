using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebViewer.View.Document
{
    public partial class DocumentViewer : System.Web.UI.Page
    {
        protected string pstrReportName = "";
        protected string pstrErrMsg = "";
        protected clsDatabase db = new clsDatabase();
        protected string query = "";
        protected DataRowView master = null;

        protected string pstrPrintAuthor = "N";

        protected string pstrMenuId = "";
        protected string pstrPrjId = "";
        protected string pstrTable = "";
        protected string pstrKey = "";

        string path = @"\\192.168.1.8\edmsdata$";
        string destFolder = "~/Temp";

        DataTable dataTable = new DataTable();

        int idx = 0;
        protected string fileInfo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EmpNo"] ?? "").ToString() == "")
            {
                try
                {
                    HttpContext.Current.Response.Redirect("~/View/SessionError.aspx");
                }
                catch (ApplicationException)
                {
                    HttpContext.Current.Response.RedirectLocation =
                                         System.Web.VirtualPathUtility.ToAbsolute("~/View/SessionError.aspx");
                }
            }

            if (!IsCallback && !IsPostBack)
            {
                pstrMenuId = Request.QueryString["id"] ?? "";
                pstrPrjId = Request.QueryString["pid"] ?? "";
                pstrTable = Request.QueryString["table"] ?? "";
                pstrKey = Request.QueryString["key"] ?? "";

                if (pstrMenuId == "") return;

                clsDatabase db = new clsDatabase();
                query = string.Format("Select *, REPLACE(FileName, '1:', '') FileCol From {0} Where DocumentKey in({1}) Order By DocumentKey", pstrTable, pstrKey);
                DataView dv = db.GetDataView(pstrTable, query);

                if (dv.Count == 0)
                {
                    pstrErrMsg = "파일을 찾을 수 없습니다.";
                    return;
                }

                string fullFileName = "";
                string fileGubun = "";
                destFolder += "/" + Session["EmpNo"].ToString();

                if (Directory.Exists(MapPath(destFolder)))
                {
                    string[] files = System.IO.Directory.GetFiles(MapPath(destFolder));
                    foreach (string s in files)
                    {
                        string fileName = System.IO.Path.GetFileName(s);
                        string deletefile = MapPath(destFolder) + @"\" + fileName;
                        System.IO.File.Delete(deletefile);
                    }
                }
                else
                {
                    Directory.CreateDirectory(MapPath(destFolder));
                }

                dataTable.Columns.Add("ImageURL");

                //int i = 0;

                foreach (DataRowView drv in dv)
                {
                    fullFileName = path + drv["FileCol"].ToString();

                    //if (i == 0)
                    //    fullFileName = @"C:\Temp\B100107597.tif";
                    //else
                    //    fullFileName = @"C:\Temp\B100107599.tif";

                    //i++;

                    if (idx == 0)
                    {
                        SetLeftInfo(pstrPrjId, drv);
                    }

                    fileGubun = drv["FileCol"].ToString().Substring(drv["FileCol"].ToString().LastIndexOf(".") + 1).ToLower();

                    if (fileGubun == "tif" || fileGubun == "tiff")
                    {
                        pstrReportName = string.Format("Image_{0}_Landscape", drv["ActualSize"].ToString());
                        SaveImagesFromTiffFile(fullFileName, MapPath(destFolder), drv["DocumentKey"].ToString());
                    }
                    else if (fileGubun == "pdf")
                    {
                        pstrReportName = string.Format("Pdf_{0}_Landscape", drv["ActualSize"].ToString());
                        SaveFileFromDisk(fullFileName, MapPath(destFolder), drv["DocumentKey"].ToString());
                    }
                    else if (fileGubun == "png" || fileGubun == "jpg" || fileGubun == "gif" || fileGubun == "bmp")
                    {
                        pstrReportName = string.Format("Image_{0}_Landscape", drv["ActualSize"].ToString());
                        SaveFileFromDisk(fullFileName, MapPath(destFolder), drv["DocumentKey"].ToString());
                    }
                    else
                    {
                        continue;
                    }

                    query = string.Format("exec Code_UseSession '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}'"
                                    , Session["CorpId"], Session["EmpNo"], Session["SessionNo"], (Request.QueryString["id"] ?? ""), "FileView"
                                    , pstrPrjId
                                    , drv["DocumentKey"].ToString()
                                    , drv["DocumentNo"].ToString()
                                    , drv["DocumentRev"].ToString()
                                    , drv["Date_Stored"].ToString()
                                    , drv["Date_Last_Chgd"].ToString());
                    db.ExecuteQuery(query);

                }

                CheckUserAuthor();
                master = IsExistReportItem(pstrReportName);

                if (master == null)
                {
                    pstrErrMsg = "선택된 메뉴는 레포트 작성 폼이 아닙니다.";
                    return;
                }

                if (master["LayoutData"].ToString() == "")
                {
                    pstrErrMsg = "선택된 메뉴는 레포트가 작성되지 않았습니다.";
                    return;
                }

                master["LayoutData"] = master["LayoutData"].ToString().Replace("?<?", "<?");

                XtraReport rpt = new XtraReport();
                // Obtain the report from the string.
                using (StreamWriter sw = new StreamWriter(new MemoryStream()))
                {
                    sw.Write(master["LayoutData"]);
                    sw.Flush();
                    rpt.LoadLayoutFromXml(sw.BaseStream);
                }

                foreach (DevExpress.XtraReports.Parameters.Parameter param in rpt.Parameters)
                {
                    if (Request.QueryString[param.Name] != null)
                    {
                        param.Value = Request.QueryString[param.Name];
                    }
                }

                rpt.RequestParameters = false;
                rpt.DataSource = dataTable;

                WebDodumentViewer.OpenReport(rpt);
            }

        }

        private void SetLeftInfo(string pid, DataRowView item)
        {
            clsDatabase db = new clsDatabase();
            query = string.Format("select FieldName, FieldCaption from MB_ProjectField Where ProjectId = {0} Order By DisplayOrder", pid);
            DataView dv = db.GetDataView("ProjectField", query);

            ltlLeftInfo.Text = "";

            foreach (DataRowView row in dv)
            {
                ltlLeftInfo.Text += "<div class=\"form-group-row\">";
                ltlLeftInfo.Text += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>", "txt" + row["FieldName"].ToString(), row["FieldCaption"].ToString());
                ltlLeftInfo.Text += "<div class=\"col-sm-8\">";
                ltlLeftInfo.Text += string.Format("<div class=\"dxbs-textbox\" id=\"{0}\" style=\"width:100%;\">", "txt" + row["FieldName"].ToString());
                ltlLeftInfo.Text += "<div>";
                ltlLeftInfo.Text += string.Format("<input id=\"{0}_I\" class=\"form-control\" type=\"text\" name=\"{0}\" readonly=\"readonly\" value=\"{1}\" autocomplete=\"off\">", "txt" + row["FieldName"].ToString(), item[row["FieldName"].ToString()]);
                ltlLeftInfo.Text += "</div>";
                ltlLeftInfo.Text += "</div>";
                ltlLeftInfo.Text += "</div>";
                ltlLeftInfo.Text += "</div>";
            }
        }

        private void SaveImagesFromTiffFile(string sourceTiffFileMappedPath, string destFolderMappedPath, string documentKey)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourceTiffFileMappedPath);
            FrameDimension frameDimension = new FrameDimension(image.FrameDimensionsList[0]);
            int framesCount = image.GetFrameCount(frameDimension);

            for (int index = 0; index < framesCount; index++)
            {
                image.SelectActiveFrame(frameDimension, index);
                string frameFileName = string.Format("{0}.png", documentKey + index.ToString());
                image.Save(Path.Combine(destFolderMappedPath, frameFileName), ImageFormat.Png);

                DataRow dr = dataTable.NewRow();
                dr[0] = string.Format(@"{0}/WebViewer/Temp/{1}/{2}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), Session["EmpNo"].ToString(), frameFileName);
                dataTable.Rows.Add(dr);

                if (fileInfo != "") fileInfo += "|";
                fileInfo += documentKey;
                idx++;
            }

            image.Dispose();
        }

        private void SaveFileFromDisk(string sourceTiffFileMappedPath, string destFolderMappedPath, string documentKey)
        {
            File.Copy(sourceTiffFileMappedPath, destFolderMappedPath, true);
            DataRow dr = dataTable.NewRow();
            dr[0] = string.Format(@"{0}/WebViewer/Temp/{1}/{2}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), Session["EmpNo"].ToString(), Path.GetFileName(destFolderMappedPath));
            dataTable.Rows.Add(dr);

            if (fileInfo == "") fileInfo += "|";
            fileInfo += documentKey;
            idx++;
        }

        private long GetCurrentTimeStamp()
        {
            DateTime dt = DateTime.Now;
            return ((DateTimeOffset)dt).ToUnixTimeSeconds();
        }

        protected void CheckUserAuthor()
        {
            List<clsAuthor> authorList = (List<clsAuthor>)Session["Author"];

            if (authorList == null) { return; }

            if (authorList.Exists(s => s.MenuID == Request.QueryString["id"] && s.AuthorCode == "04"))
            {   // 출력 권한 체크
                pstrPrintAuthor = "Y";
            }
        }

        protected DataRowView IsExistReportItem(string reportName)
        {
            query = string.Format("Select * From ReportItem Where CorpId = '{0}' and ReportName = '{1}'",
                            Session["CorpId"], reportName);

            return db.GetDataRecord(query);
        }


        protected void docCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            if (e.Parameter == "Print")
            {
                query = string.Format("Select * From {0} Where DocumentKey in({1}) Order By DocumentKey", Request.QueryString["table"], Request.QueryString["key"]);
                DataView dv = db.GetDataView(Request.QueryString["table"], query);

                foreach (DataRowView drv in dv)
                {
                    query = string.Format("exec Code_UseSession '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}'"
                                        , Session["CorpId"], Session["EmpNo"], Session["SessionNo"], (Request.QueryString["id"] ?? ""), e.Parameter
                                        , Request.QueryString["pid"]
                                        , drv["DocumentKey"].ToString()
                                        , drv["DocumentNo"].ToString()
                                        , drv["DocumentRev"].ToString()
                                        , drv["Date_Stored"].ToString()
                                        , drv["Date_Last_Chgd"].ToString());
                    db.ExecuteQuery(query);
                }
            }
        }

        protected void calllbackPnl_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            clsDatabase db = new clsDatabase();
            query = string.Format("Select *, REPLACE(FileName, '1:', '') FileCol From {0} Where DocumentKey in({1})", Request.QueryString["table"], e.Parameter);
            DataRowView drv = db.GetDataRecord(query);

            SetLeftInfo(Request.QueryString["pid"], drv);
        }
    }
}