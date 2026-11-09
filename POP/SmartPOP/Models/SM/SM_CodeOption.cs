using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.SM
{
    public class SM_CodeOption
    {
        #region Property

        public string Cd_Div_Cd { get; set; }
        public string Field_ID { get; set; }
        public string Caption_Nm { get; set; }
        public string Disp_Seq { get; set; }
        public string Disp_Yn { get; set; }
        public string Lock_Yn { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_CdOption(string div = null)
        {
            //사업부관리(Frm_SC_Corp) 호출 Query
            query = $@"SELECT '' AS Status
                            , DivCd
                            , FieldID
                            , CaptionNm
                            , DispSeq
                            , DispYn
                            , LockYn

                         FROM SM_CodeOption";

            if (div != null)
            {
                query += $"\nWHERE DivCd = '{div}'";
            }

            query += $@" ORDER BY 
                         CASE 
                         	WHEN DispSeq IS NULL OR ISNULL(DispSeq, '0') = '0' THEN 1
                         	ELSE 0 
                         END
                         , DispSeq ASC ";

            return db.GetDataTable(query);
        }

        public DataTable Select_CdOption_DataType(string div = null)
        {
            //사업부관리(Frm_SC_Corp) 호출 Query
            query = $@"SELECT '' AS Status
                            , Cd_Div_Cd
                            , Field_ID
                            , Caption_Nm
                            , Disp_Seq
                            , Disp_Yn
                            , Lock_Yn
                            , tb.DataType

                         FROM SM_CodeOption cd
                        LEFT JOIN (
                        	SELECT
                        		 COLUMN_NAME AS [Column]
                        		, DATA_TYPE AS [DataType]
                        	FROM INFORMATION_SCHEMA.COLUMNS
                        	WHERE TABLE_NAME = 'BS_Item'
                        ) tb ON cd.Field_ID = tb.[Column]
                        ";

            if (div != null)
            {
                query += $"\nWHERE Cd_Div_Cd = '{div}'";
            }

            query += $@" ORDER BY 
                         CASE 
                         	WHEN Disp_Seq IS NULL OR ISNULL(Disp_Seq, '0') = '0' THEN 1
                         	ELSE 0 
                         END
                         , Disp_Seq ASC ";

            return db.GetDataTable(query);
        }

        public DataTable Select_CdOption_CdDiv()
        {
            query = $@"SELECT DISTINCT
                              Cd_Div_Cd

                         FROM SM_CodeOption";

            return db.GetDataTable(query);
        }


        public bool Ins_CdOption()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO SM_CodeOption(Cd_Div_Cd, Field_ID, Caption_Nm, Disp_Yn, Lock_Yn                              
                                           , Ins_Emp_No, Ins_Date_Time)
                            VALUES ('{Cd_Div_Cd}'
                                  , '{Field_ID}'
                                  , '{Caption_Nm}'
                                  , '{Disp_Yn}'
                                  , '{Lock_Yn}'

                                  , '{WrGlobal.LoginID}'
                                  , GETDATE())";

            return db.ExecuteQuery(query);
        }

        public bool Upd_CdOption()
        {
            db.Edit_Property(this);

            query = $@"UPDATE SM_CodeOption
                          SET Field_ID = '{Field_ID}'
                            , Caption_Nm = '{Caption_Nm}'
                            , Disp_Seq = {Disp_Seq.ConvertStr_Empty_To_Null()}
                            , Disp_Yn = '{Disp_Yn}'
                            , Lock_Yn = '{Lock_Yn}'

                            , Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()


                        WHERE Cd_Div_Cd = '{Cd_Div_Cd}'
                          AND Field_ID = '{Field_ID}'";

            return db.ExecuteQuery(query);
        }
    }
}
