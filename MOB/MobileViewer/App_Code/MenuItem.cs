using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileViewer.App_Code
{
	public class MenuItem
	{
        public string MenuID { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public string MenuType { get; set; }
    }
}