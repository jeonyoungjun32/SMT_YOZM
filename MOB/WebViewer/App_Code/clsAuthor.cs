using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebViewer
{
    public class clsAuthor
    {
        public string MenuID = "";
        public string MenuName = "";
        public string ParentMenuID = "";
        public string AuthorCode = "";

        public clsAuthor(string _menuID, string _menuName, string _parentMenuID, string _authorCode)
        {
            MenuID = _menuID;
            ParentMenuID = _parentMenuID;
            MenuName = _menuName;
            AuthorCode = _authorCode;
        }
    }
}