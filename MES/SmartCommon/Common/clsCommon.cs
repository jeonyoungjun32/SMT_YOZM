using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.Common
{
    public class clsCommon
    {
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

        public static string getString(object obj)
        {
            if (Convert.IsDBNull(obj))
            {
                return "";
            }
            else
            {
                if (obj.GetType().Name == "DateTime")
                {
                    DateTime dtime = (DateTime)obj;
                    return dtime.ToShortDateString();
                }
                else
                {
                    return obj.ToString();
                }

            }
        }

        public static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }

        public static bool IsValidPassword(string password)
        {
            return
               password.Any(c => IsLetter(c)) &&
               password.Any(c => IsDigit(c)) &&
               password.Any(c => IsSymbol(c));
        }

        // SHA256  256bit 암호화
        public static string SHA256Hash(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.Unicode.GetBytes(Data));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString().ToUpper();
        }

        // 정수 자릿수 반올림
        public static double IntRound(double Value, int Digit)
        {
            double Temp = Math.Pow(10.0, Digit);
            return Math.Round(Value * Temp) / Temp;
        }
        // 정수 자릿수 올림
        public static double IntCeiling(double Value, int Digit)
        {
            double Temp = Math.Pow(10.0, Digit);
            return Math.Ceiling(Value * Temp) / Temp;
        }
        // 정수 자릿수 올림
        public static double IntFloor(double Value, int Digit)
        {
            double Temp = Math.Pow(10.0, Digit);
            return Math.Floor(Value * Temp) / Temp;
        }

        public static bool IsAllRowsSelected(GridView view)
        {
            bool isDataBoundSelection = view.OptionsSelection.MultiSelectMode == DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect && !string.IsNullOrEmpty(view.OptionsSelection.CheckBoxSelectorField);
            int rowCount = view.DataController.VisibleListSourceRowCount + (isDataBoundSelection ? 0 : view.DataController.GroupRowCount);
            return view.SelectedRowsCount == rowCount;
        }
    }
}
