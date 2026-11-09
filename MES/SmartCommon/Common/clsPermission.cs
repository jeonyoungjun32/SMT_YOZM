using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.Common
{
    public class clsPermission
    {

        private void PermissionButton(ref Button btnSearch, ref Button btnExcel, ref Button btnPrint)
        {
            btnSearch.Visible = true;
            btnExcel.Visible = true;
            btnPrint.Visible = true;
        }
    }
}
