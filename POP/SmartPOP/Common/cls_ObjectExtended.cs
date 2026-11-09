using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Common
{
    public static class cls_ObjectExtended
    { 
        
        // if(string A == null) return ~> if(A.IsNullOrEmpty()) return

        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return true;
            else
                return false;
        }

        public static decimal ConvertDecimal_Null_To_Zero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 0;
            else
                return Convert.ToDecimal(obj);
        }

        public static float ConvertFloat_Null_To_Zero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 0;
            else
                return Convert.ToSingle(obj);
        }

        public static double ConvertDouble_Null_To_Zero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 0;
            else
                return Convert.ToDouble(obj);
        }

        public static int ConvertInt_Null_To_Zero(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        public static decimal ConvertDecimal_Null_To_One(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 1;
            else
                return Convert.ToDecimal(obj);
        }

        public static float ConvertFloat_Null_To_One(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 1;
            else
                return Convert.ToSingle(obj);
        }

        public static double ConvertDouble_Null_To_One(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 1;
            else
                return Convert.ToDouble(obj);
        }

        public static int ConvertInt_Null_To_One(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return 1;
            else
                return Convert.ToInt32(obj);
        }

        /*
         * String A = null 일때 "" 반환
         * 
         * A.GetNullToEmpty();
         */
        public static string ConvertString_Null_To_Empty(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;
            else
                return obj.ToString().Trim();
        }
        public static string ConvertString_Null_To_Zero(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "0";
            else
                return obj.ToString().Trim();
        }


        /*
         * 날짜 포맷 변경
         * 2022-11-01 ~> 20221101
         * 2022-11-01 13:24:12 ~> 20221101
         * 2022-11-01 오후 12:24:12 ~> 20221101
         */
        public static string ConvertDate_To_Char8(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return string.Empty;
            else
                return obj.ToString().Substring(0, 10).Replace("-", "");
        }

        public static string ConvertDate_To_Char10(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == "")
                return string.Empty;
            else
                return obj.ToString().Substring(0, 10);
        }

        //Query에 이용
        public static string ConvertStr_Empty_To_Null(this string obj)
        {
            if (obj == null || obj == "" || obj.ToUpper() == "ALL")
                return "NULL";
            else
                return "'" + obj + "'";
        }

        public static string ConvertInt_Empty_To_Null(this int obj)
        {
            if (obj == null || obj == 0)
                return "NULL";
            else
                return obj.ToString();
        }

        public static string ConvertFloat_Empty_To_Null(this float obj)
        {
            if (obj == null || obj == 0)
                return "NULL";
            else
                return obj.ToString();
        }

        public static string ConvertDecimal_Empty_To_Null(this decimal obj)
        {
            if (obj == null || obj == 0)
                return "NULL";
            else
                return obj.ToString();
        }

        public static string ConvertNumeric_Empty_To_Null(this object obj)
        {
            if (obj == null || obj.ConvertFloat_Null_To_Zero() == 0)
                return "NULL";
            else
                return obj.ToString();
        }
    }
}
