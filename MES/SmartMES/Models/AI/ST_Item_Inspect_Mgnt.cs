using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.AI
{
    class ST_Item_Inspect_Mgnt
    {
        #region Properties

        public string CorpId { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string Alias { get; set; }

        public decimal? Y_Nominal { get; set; }

        public decimal? Y_Min { get; set; }

        public decimal? Y_Max { get; set; }

        public decimal? Y_DangerMin { get; set; }

        public decimal? Y_DangerMax { get; set; }

        public string X_Nm { get; set; }

        public string X_Value { get; set; }

        public decimal? X1_Min { get; set; }

        public decimal? X1_Max { get; set; }

        public decimal? X2_Min { get; set; }

        public decimal? X2_Max { get; set; }

        public decimal? X3_Min { get; set; }

        public decimal? X3_Max { get; set; }

        public decimal? X4_Min { get; set; }

        public decimal? X4_Max { get; set; }

        public decimal? X5_Min { get; set; }

        public decimal? X5_Max { get; set; }

        public decimal? X6_Min { get; set; }

        public decimal? X6_Max { get; set; }

        public decimal? X7_Min { get; set; }

        public decimal? X7_Max { get; set; }

        public decimal? X8_Min { get; set; }

        public decimal? X8_Max { get; set; }

        public decimal? X9_Min { get; set; }

        public decimal? X9_Max { get; set; }

        public decimal? X10_Min { get; set; }

        public decimal? X10_Max { get; set; }

        public decimal? X11_Min { get; set; }

        public decimal? X11_Max { get; set; }

        public decimal? X12_Min { get; set; }

        public decimal? X12_Max { get; set; }

        public decimal? X13_Min { get; set; }

        public decimal? X13_Max { get; set; }

        public decimal? X14_Min { get; set; }

        public decimal? X14_Max { get; set; }

        public decimal? X15_Min { get; set; }

        public decimal? X15_Max { get; set; }

        public string CreId { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public DateTime? ModDt { get; set; }

        #endregion Properties End

        Database db = new Database();
        string query;

        public DataTable Select_Item_Inspect_Mgnt()
        {
            query = "";
            for (int i = 1; i <= 8; i++)
            {
                query += $"UPDATE ST_Item_Inspect_Mgnt" +
                         $" SET X{i}_Min = T.X{i}_Min" +
                         $" FROM (SELECT AVG(X{i}_Min) AS X{i}_Min" +
                                 $" FROM ST_Item_Inspect_Mgnt" +
                                 $" WHERE X{i}_Min IS NOT NULL" +
                                 $" GROUP BY X{i}_Min) T" +
                         $" WHERE ST_Item_Inspect_Mgnt.X{i}_Min IS NULL\n";

                query += $"UPDATE ST_Item_Inspect_Mgnt" +
                         $" SET X{i}_Max = T.X{i}_Max" +
                         $" FROM (SELECT AVG(X{i}_Max) AS X{i}_Max" +
                             $"     FROM ST_Item_Inspect_Mgnt" +
                             $"    WHERE X{i}_Max IS NOT NULL" +
                             $" GROUP BY X{i}_Max) T" +
                         $" WHERE ST_Item_Inspect_Mgnt.X{i}_Max IS NULL\n";
            }

            query += $@"SELECT LEFT(ItemCode,2) AS ItemCode, ItemName, Alias, Y_Nominal, Y_Min, Y_Max, Y_DangerMin, Y_DangerMax, 
                                             X1_Min, X1_Max, X2_Min, X2_Max, X3_Min, X3_Max, X4_Min, X4_Max, X5_Min, X5_Max,
                                             X6_Min, X6_Max, X7_Min, X7_Max, X8_Min, X8_Max, X9_Min, X9_Max, X10_Min, X10_Max,
                                             X11_Min, X11_Max, X12_Min, X12_Max, X13_Min, X13_Max, X14_Min, X14_Max, X15_Min, X15_Max
                                 FROM ST_Item_Inspect_Mgnt  ";


            if(Database.DbName == "AI_JINSUNGTECH")
            {
                query = $@"SELECT im.ItemCode, im.ItemName, im.Alias, im.Y_Nominal, im.Y_Min, im.Y_Max, im.Y_DangerMin, im.Y_DangerMax, 
                                             im.X1_Min, im.X1_Max, im.X2_Min, im.X2_Max, im.X3_Min, im.X3_Max, im.X4_Min, im.X4_Max, im.X5_Min, im.X5_Max,
                                             im.X6_Min, im.X6_Max, im.X7_Min, im.X7_Max, im.X8_Min, im.X8_Max, im.X9_Min, im.X9_Max, im.X10_Min, im.X10_Max,
                                             X11_Min, X11_Max, im.X12_Min, im.X12_Max, im.X13_Min, im.X13_Max, im.X14_Min, im.X14_Max, im.X15_Min, im.X15_Max, st.MachineName
                                 FROM ST_Item_Inspect_Mgnt	im
								 INNER JOIN ST_Item_Machine st ON im.ItemCode = st.ItemCode";
            }

            return db.GetDataSet(query);
        }

        public DataTable Select_Item_Inspect()
        {
            query = $@"SELECT ItemCode, ItemName, Alias, Y_Nominal, Y_Min, Y_Max, Y_DangerMin, Y_DangerMax, 
                                             X1_Min, X1_Max, X2_Min, X2_Max, X3_Min, X3_Max, X4_Min, X4_Max, X5_Min, X5_Max,
                                             X6_Min, X6_Max, X7_Min, X7_Max, X8_Min, X8_Max, X9_Min, X9_Max, X10_Min, X10_Max,
                                             X11_Min, X11_Max, X12_Min, X12_Max, X13_Min, X13_Max, X14_Min, X14_Max, X15_Min, X15_Max
                                 FROM ST_Item_Inspect_Mgnt	
                                WHERE ItemCode = '{ItemCode}' ";

            return db.GetDataSet(query);
        }

        public DataTable Select_Item_Y_Nominal()
        {
            query = $@"SELECT ItemCode, Y_Nominal FROM ST_Item_Inspect_Mgnt";

            return db.GetDataSet(query);
        }

        public DataRowView Select_Item_YDangerMinMax()
        {
            query = $@"SELECT Y_DangerMin, Y_DangerMax
                         FROM ST_Item_Inspect_Mgnt
                        WHERE ItemCode = '{ItemCode}'";

            return db.GetDataRecord(query);
        }

        public void Update_MinMax()
        {
            query = $@"UPDATE ST_Item_Inspect_Mgnt SET {X_Nm} = '{X_Value}' WHERE ItemCode = '{ItemCode}'";

            db.ExecuteQuery(query);
        }
    }
}
