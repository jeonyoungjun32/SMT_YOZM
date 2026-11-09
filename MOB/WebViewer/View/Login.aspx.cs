using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebViewer.View
{
    public partial class Login : System.Web.UI.Page
    {
        clsDatabase db = new clsDatabase();
        string _query;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if ((Session["SessionNo"] ?? "").ToString() != "")
                {
                    _query = string.Format("exec Code_CloseSession '{0}', N'{1}', '{2}'", Session["CorpId"], Session["EmpNo"], Session["SessionNo"]);
                    db.ExecuteQuery(_query);
                }

                Session.Abandon();

                Session["CorpId"] = "B1";
                if (MachineCheck("MOBILE"))
                {
                    Session["ProgramType"] = "3";
                }
                else
                {
                    Session["ProgramType"] = "1";
                }
            }
        }


        public static bool MachineCheck(string MobileGnb)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            bool isMobile = false;
            //모바일인지 PC인지 CHECK    
            if (MobileGnb == "MOBILE")
            {
                string[] browser = { "iphone", "ipod", "ipad", "android", "blackberry", "windows ce", "nokia", "webos", "opera mini", "sonyericsson", "opera mobi", "iemobile", "windows phone" };
                for (int i = 0; i < browser.Length; i++)
                {
                    if (userAgent.ToLower().Contains(browser[i]) == true)
                    {
                        isMobile = true;
                        break;
                    }
                }
            }
            else if (MobileGnb == "IPHONE")
            {
                //아이폰, 안드로이드 CHECK(아이폰일경우 true)        
                if (userAgent.IndexOf("iPhone") > 0 || userAgent.IndexOf("iPad") > 0 || userAgent.IndexOf("iPod") > 0)
                {
                    isMobile = true;
                }
            }
            return isMobile;
        }

        protected void CbPnlLogin_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (cboUserId.Value == null)
            {
                CbPnlLogin.JSProperties["cpLogin"] = "false";
                return;
            }

            _query = string.Format("SELECT cu.* FROM CodeUser cu "
                    + "WHERE cu.CorpId = '{0}' and cu.EmployeeId = '{1}'", Session["CorpId"], cboUserId.Value.ToString());

            DataRowView drv = db.GetDataRecord(_query);

            if (drv == null)
            {
                CbPnlLogin.JSProperties["cpLogin"] = "false";
                CbPnlLogin.JSProperties["cpMsg"] = "등록된 사용자가 아닙니다.";
                return;
            }

            if (clsCommon.SHA256Hash(txtPassword.Text) == clsCommon.getString(drv["Password"]))
            {
                Session["EmpNo"] = (drv["EmployeeId"] ?? "").ToString();
                Session["EmpName"] = (drv["EmployeeName"] ?? "").ToString();
                Session["EmpIp"] = GetUserIP();

                Session["Author"] = GetUserAuthor();

                CreateSession();

                CbPnlLogin.JSProperties["cpLogin"] = "true";
            }
            else
            {
                CbPnlLogin.JSProperties["cpLogin"] = "false";
                CbPnlLogin.JSProperties["cpMsg"] = "비밀번호가 일치 하지 않습니다. 다시 입력하세요.";
            }
        }

        private void CreateSession()
        {
            Session["SessionNo"] = DateTime.Now.ToString("yyyyMMddHHmmss");

            _query = string.Format("exec Code_CreateSession '{0}', N'{1}', '{2}', '{3}'", Session["CorpId"], Session["EmpNo"], Session["SessionNo"], Session["ProgramType"]);
            db.ExecuteQuery(_query);
        }

        private List<clsAuthor> GetUserAuthor()
        {
            _query = string.Format("SELECT AuthorGroup FROM CodeUser WHERE CorpId = '{0}' and EmployeeId = '{1}'", Session["CorpId"].ToString(), Session["EmpNo"].ToString());
            DataRowView drv = db.GetDataRecord(_query);

            if (drv[0] == DBNull.Value) return null;

            drv[0] = drv[0].ToString().Replace(" ", "");

            string[] arrGroup = drv[0].ToString().Split(new char[] { ',' });
            string inGroupStr = "";

            foreach (string item in arrGroup)
            {
                if (item == "") continue;

                if (inGroupStr != "") inGroupStr = inGroupStr + ",";
                inGroupStr = inGroupStr + "''" + item + "''";
            }

            _query = string.Format("exec Code_GetAuthorByAuthorGroups '{0}', '{1}', '{2}'", Session["CorpId"].ToString(), Session["ProgramType"].ToString(), inGroupStr);
            DataView dv = db.GetDataView("권한", _query);

            List<clsAuthor> AuthorList = new List<clsAuthor>();

            foreach (DataRowView view in dv)
            {
                clsAuthor author = new clsAuthor(view["MenuID"].ToString(), view["MenuName"].ToString(), view["ParentID"].ToString(), view["AuthorCode"].ToString());
                AuthorList.Add(author);
            }

            return AuthorList;
        }

        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}