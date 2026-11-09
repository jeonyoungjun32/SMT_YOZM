using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileViewer
{
    public class clsAuthor
    {
        public string MenuID { get; set; }
        public string ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public string AuthorCode { get; set; }
        public string Url { get; set; }
        public string Url2 { get; set; } 
        public string WebImageKey { get; set; }
        public string ParamText { get; set; }   // ✅ 추가
        public string CssClass { get; set; }

        public clsAuthor(string _menuID, string _menuName, string _parentMenuID, string _authorCode, string _url, string _webImageKey, string _paramText, string _cssClass)
        {
            MenuID = _menuID;
            ParentMenuID = _parentMenuID;
            MenuName = _menuName;
            AuthorCode = _authorCode;
            Url = _url;
            Url2 = _url.Replace("Document/", "");
            WebImageKey = _webImageKey;
            ParamText = _paramText;
            CssClass = _cssClass;
        }
    }
}