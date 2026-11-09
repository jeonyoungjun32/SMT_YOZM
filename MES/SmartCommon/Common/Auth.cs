using baseSMT;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.Common
{
    public static class Auth
    {
        public static void ApplyButtonAuth(string menuId, Control parentControl)
        {
            bool canEdit = HasAuth(menuId, "02");   // 수정/등록 권한
            bool canDelete = HasAuth(menuId, "03"); // 삭제 권한(분리 예정)

            foreach (var ctrl in GetAllControls(parentControl))
            {
                if (ctrl is SimpleButton btn)
                {
                    switch (btn.Name.ToUpper())
                    {
                        case "BTNINS":
                        case "CMDINS":
                        case "BTNINSERT":
                        case "CMDINSERT":

                        case "BTNSAVE":
                        case "CMDSAVE":

                        case "BTNDEL":
                        case "CMDDEL":
                        case "BTNDELETE":
                        case "CMDDELETE":
                            btn.Visible = canEdit;
                            break;

                            //btn.Visible = canDelete;
                            //break;
                    }
                }
                else if (ctrl is smtSearchButton btn1)
                {
                    switch (btn1.Name.ToUpper())
                    {
                        case "BTNINS":
                        case "CMDINS":
                        case "BTNINSERT":
                        case "CMDINSERT":

                        case "BTNSAVE":
                        case "CMDSAVE":

                        //삭제 권한 분기 안되어있어 일단 같이
                        case "BTNDEL": 
                        case "CMDDEL":
                        case "BTNDELETE": 
                        case "CMDDELETE":
                            btn1.Visible = canEdit;
                            break;

                            //btn1.Visible = canDelete;
                            //break;
                    }
                }
            }
        }

        public static bool HasAuth(string menuId, string authCode)
        {
            return WrGlobal.AuthorList.Any(a => a.MenuID == menuId && a.AuthorCode == authCode);
        }

        private static IEnumerable<Control> GetAllControls(Control container)
        {
            foreach (Control c in container.Controls)
            {
                yield return c;
                foreach (var child in GetAllControls(c))
                    yield return child;
            }
        }
    }
}
