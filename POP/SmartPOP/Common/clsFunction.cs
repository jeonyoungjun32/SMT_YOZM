using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SmartPOP.Common
{
    class clsFunction
    {
        public string GetIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddr = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = host.AddressList[i].ToString();
                }
            }
            return ipAddr;
        }

        public int GetMaxSeq(GridView gv)
        {
            int seq = 0;
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                int lastSeq = (int)gv.GetRowCellValue(i, "Seq");

                if (lastSeq > seq) seq = lastSeq;
            }

            return seq;

        }

        public void ExportExcel(GridView gv)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gv.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }//end fnction
        }

        public static string GetFileSize(long byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }

    }
}
