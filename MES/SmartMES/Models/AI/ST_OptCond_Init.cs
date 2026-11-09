using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.AI
{
    class ST_OptCond_Init
    {
        #region Property
        public string CorpId { get; set; }
        public string ItemCode_Yn { get; set; }
        public string Alias_Yn { get; set; }
        public string ItemName_Yn { get; set; }
        public string Y_Nm { get; set; }
        public string Y_Method { get; set; }
        public string Y_Cycle { get; set; }
        public string X_Count { get; set; }
        public string E_Count { get; set; }
        public string InspectType { get; set; }
        public string X1_Nm { get; set; }
        public string X1_Min { get; set; }
        public string X1_Max { get; set; }
        public string X2_Nm { get; set; }
        public string X2_Min { get; set; }
        public string X2_Max { get; set; }
        public string X3_Nm { get; set; }
        public string X3_Min { get; set; }
        public string X3_Max { get; set; }
        public string X4_Nm { get; set; }
        public string X4_Min { get; set; }
        public string X4_Max { get; set; }
        public string X5_Nm { get; set; }
        public string X5_Min { get; set; }
        public string X5_Max { get; set; }
        public string X6_Nm { get; set; }
        public string X6_Min { get; set; }
        public string X6_Max { get; set; }
        public string X7_Nm { get; set; }
        public string X7_Min { get; set; }
        public string X7_Max { get; set; }
        public string X8_Nm { get; set; }
        public string X8_Min { get; set; }
        public string X8_Max { get; set; }
        public string X9_Nm { get; set; }
        public string X9_Min { get; set; }
        public string X9_Max { get; set; }
        public string X10_Nm { get; set; }
        public string X10_Min { get; set; }
        public string X10_Max { get; set; }
        public string X11_Nm { get; set; }
        public string X11_Min { get; set; }
        public string X11_Max { get; set; }
        public string X12_Nm { get; set; }
        public string X12_Min { get; set; }
        public string X12_Max { get; set; }
        public string X13_Nm { get; set; }
        public string X13_Min { get; set; }
        public string X13_Max { get; set; }
        public string X14_Nm { get; set; }
        public string X14_Min { get; set; }
        public string X14_Max { get; set; }
        public string X15_Nm { get; set; }
        public string X15_Min { get; set; }
        public string X15_Max { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable SelectST_OptCond_Init()
        {
            query = $@" SELECT CorpId
                             , ItemCode_Yn
                             , Alias_Yn
                             , ItemName_Yn
                             , Y_Nm
                             , Y_Method
                             , Y_Cycle
                             , X_Count
                             , E_Count
                             , InspectType

                             , X1_Nm,  X1_Min,  X1_Max
                             , X2_Nm,  X2_Min,  X2_Max
                             , X3_Nm,  X3_Min,  X3_Max
                             , X4_Nm,  X4_Min,  X4_Max
                             , X5_Nm,  X5_Min,  X5_Max
                             , X6_Nm,  X6_Min,  X6_Max
                             , X7_Nm,  X7_Min,  X7_Max
                             , X8_Nm,  X8_Min,  X8_Max
                             , X9_Nm,  X9_Min,  X9_Max
                             , X10_Nm, X10_Min, X10_Max
                             , X11_Nm, X11_Min, X11_Max
                             , X12_Nm, X12_Min, X12_Max
                             , X13_Nm, X13_Min, X13_Max
                             , X14_Nm, X14_Min, X14_Max
                             , X15_Nm, X15_Min, X15_Max
                             , X16_Nm, X16_Min, X16_Max
                             , X17_Nm, X17_Min, X17_Max
                             , CreId
                             , CreDt
                             , ModId
                             , ModDt

                          FROM ST_OptCond_Init";

            return db.GetDataSet(query);
        }

        public DataTable SelectST_OptCond_Init_All()
        {
            query = $@" SELECT CorpId
                             , ItemCode_Yn
                             , Alias_Yn
                             , ItemName_Yn
                             , Y_Nm
                             , Y_Method
                             , Y_Cycle
                             , X_Count
                             , E_Count
                             , InspectType

                             , X1_Nm,  X1_Min,  X1_Max
                             , X2_Nm,  X2_Min,  X2_Max
                             , X3_Nm,  X3_Min,  X3_Max
                             , X4_Nm,  X4_Min,  X4_Max
                             , X5_Nm,  X5_Min,  X5_Max
                             , X6_Nm,  X6_Min,  X6_Max
                             , X7_Nm,  X7_Min,  X7_Max
                             , X8_Nm,  X8_Min,  X8_Max
                             , X9_Nm,  X9_Min,  X9_Max
                             , X10_Nm, X10_Min, X10_Max
                             , X11_Nm, X11_Min, X11_Max
                             , X12_Nm, X12_Min, X12_Max
                             , X13_Nm, X13_Min, X13_Max
                             , X14_Nm, X14_Min, X14_Max
                             , X15_Nm, X15_Min, X15_Max
                             , CreId
                             , CreDt
                             , ModId
                             , ModDt

                          FROM ST_OptCond_Init ";

            return db.GetDataSet(query);
        }

        public DataTable Select_X_List()
        {
            query = $@"
                         SELECT 
                           REPLACE(ColValue, 'Nm', 'Value') as ColValue
                         , ColName
                         FROM
                         (
	                         SELECT 
	                            X1_Nm , X2_Nm , X3_Nm , X4_Nm , X5_Nm , X6_Nm , X7_Nm , X8_Nm , X9_Nm , X10_Nm, X11_Nm, X12_Nm, X13_Nm, X14_Nm, X15_Nm, X16_Nm, X17_Nm
	                         FROM ST_OptCond_Init
                         ) AS Result
                         UNPIVOT
                         (ColName FOR ColValue in ([X1_Nm], [X2_Nm], [X3_Nm], [X4_Nm], [X5_Nm], [X6_Nm], [X7_Nm], [X8_Nm], [X9_Nm], [X10_Nm], [X11_Nm], [X12_Nm], [X13_Nm], [X14_Nm], [X15_Nm], [X16_Nm], [X17_Nm])) AS unpv
                       ";

            return db.GetDataSet(query);
        }

        public DataTable Select_Y_Nm()
        {
            query = $@"SELECT Y_Nm
                         FROM ST_OptCond_Init";

            return db.GetDataSet(query);
        }

        public void Insert_ST_OptCond_Init()
        {
            query = $@" INSERT INTO ST_OptCond_Init
                        (
                             CorpId
                            , ItemCode_Yn
                            , Alias_Yn
                            , ItemName_Yn
                            , Y_Nm
                            , Y_Method
                            , Y_Cycle
                            , X_Count
                            , E_Count
                            , InspectType
                            , X1_Nm, X1_Min, X1_Max
                            , X2_Nm, X2_Min, X2_Max
                            , X3_Nm, X3_Min, X3_Max
                            , X4_Nm, X4_Min, X4_Max
                            , X5_Nm, X5_Min, X5_Max
                            , X6_Nm, X6_Min, X6_Max
                            , X7_Nm, X7_Min, X7_Max
                            , X8_Nm, X8_Min, X8_Max
                            , X9_Nm, X9_Min, X9_Max
                            , X10_Nm, X10_Min, X10_Max
                            , X11_Nm, X11_Min, X11_Max
                            , X12_Nm, X12_Min, X12_Max
                            , X13_Nm, X13_Min, X13_Max
                            , X14_Nm, X14_Min, X14_Max
                            , X15_Nm, X15_Min, X15_Max
                            , CreId
                            , CreDt
                        )

                        VALUES
                        (
                            '{CorpId}'
                            , '{ItemCode_Yn}'
                            , '{Alias_Yn}'
                            , '{ItemName_Yn}'
                            , '{Y_Nm}'
                            , '{Y_Method}'
                            , '{Y_Cycle}'
                            , '{X_Count}'
                            , '{E_Count}'
                            , '{InspectType}'
                            , '{X1_Nm}', '{X1_Min}', '{X1_Max}'
                            , '{X2_Nm}', '{X2_Min}', '{X2_Max}'
                            , '{X3_Nm}', '{X3_Min}', '{X3_Max}'
                            , '{X4_Nm}', '{X4_Min}', '{X4_Max}'
                            , '{X5_Nm}', '{X5_Min}', '{X5_Max}'
                            , '{X6_Nm}', '{X6_Min}', '{X6_Max}'
                            , '{X7_Nm}', '{X7_Min}', '{X7_Max}'
                            , '{X8_Nm}', '{X8_Min}', '{X8_Max}'
                            , '{X9_Nm}', '{X9_Min}', '{X9_Max}'
                            , '{X10_Nm}', '{X10_Min}', '{X10_Max}'
                            , '{X11_Nm}', '{X11_Min}', '{X11_Max}'
                            , '{X12_Nm}', '{X12_Min}', '{X12_Max}'
                            , '{X13_Nm}', '{X13_Min}', '{X13_Max}'
                            , '{X14_Nm}', '{X14_Min}', '{X14_Max}'
                            , '{X15_Nm}', '{X15_Min}', '{X15_Max}'
                            , '{WrGlobal.LoginID}'
                            , GETDATE()
                        )
                        ";

            db.ExecuteQuery(query);
        }

        public void Update_ST_OptCond_Init()
        {
            query = $@" UPDATE ST_OptCond_Init
                        SET 
                           
                           X1_Min = '{X1_Min}', X1_Max = '{X1_Max}'
                           , X2_Min = '{X2_Min}', X2_Max = '{X2_Max}'
                           , X3_Min = '{X3_Min}', X3_Max = '{X3_Max}'
                           , X4_Min = '{X4_Min}', X4_Max = '{X4_Max}'
                           , X5_Min = '{X5_Min}', X5_Max = '{X5_Max}'
                           , X6_Min = '{X6_Min}', X6_Max = '{X6_Max}'
                           , X7_Min = '{X7_Min}', X7_Max = '{X7_Max}'
                           , X8_Min = '{X8_Min}', X8_Max = '{X8_Max}'
                           , X9_Min = '{X9_Min}', X9_Max = '{X9_Max}'

                           , ModId = '{WrGlobal.LoginID}'
                           , ModDt = GETDATE()
                        WHERE CorpId = '{CorpId}'";
            // ItemCode_Yn = '{ItemCode_Yn}'
            //, Alias_Yn = '{Alias_Yn}'
            //, ItemName_Yn = '{ItemName_Yn}'
            //, Y_Nm = '{Y_Nm}'
            //, Y_Method = '{Y_Method}'
            //, Y_Cycle = '{Y_Cycle}'
            //, X_Count = '{X_Count}'
            //, E_Count = '{E_Count}'
            //, InspectType = '{InspectType}'
            //X1_Nm = '{X1_Nm}',
            //X2_Nm = '{X2_Nm}',
            //X3_Nm = '{X3_Nm}',
            //X4_Nm = '{X4_Nm}',
            //X5_Nm = '{X5_Nm}',
            //X6_Nm = '{X6_Nm}',
            //X7_Nm = '{X7_Nm}',
            //X8_Nm = '{X8_Nm}',
            //X9_Nm = '{X9_Nm}',
                           //            , X10_Nm = '{X10_Nm}', X10_Min = '{X10_Min}', X10_Max = '{X10_Max}'
                           //, X11_Nm = '{X11_Nm}', X11_Min = '{X11_Min}', X11_Max = '{X11_Max}'
                           //, X12_Nm = '{X12_Nm}', X12_Min = '{X12_Min}', X12_Max = '{X12_Max}'
                           //, X13_Nm = '{X13_Nm}', X13_Min = '{X13_Min}', X13_Max = '{X13_Max}'
                           //, X14_Nm = '{X14_Nm}', X14_Min = '{X14_Min}', X14_Max = '{X14_Max}'
                           //, X15_Nm = '{X15_Nm}', X15_Min = '{X15_Min}', X15_Max = '{X15_Max}'

            db.ExecuteQuery(query);
        }

        public void Delete_ST_OptCond_Init()
        {
            query = $@" DELETE ST_OptCond_Init
                        WHERE CorpId = '{CorpId}'";

            db.ExecuteQuery(query);
        }
    }
}
