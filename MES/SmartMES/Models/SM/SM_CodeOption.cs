using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.SM
{
    public class SM_CodeOption
    {
        #region Property

        public string CorpId { get; set; }
        public string DivCd { get; set; }
        public string FieldID { get; set; }
        public string CaptionNm { get; set; }
        public string DispSeq { get; set; }
        public string DispYn { get; set; }
        public string LockYn { get; set; }

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

        public DataTable Select_CdOption_CdDiv()
        {
            query = $@"SELECT DISTINCT
                              DivCd

                         FROM SM_CodeOption";

            return db.GetDataTable(query);
        }


        public bool Ins_CdOption()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO SM_CodeOption(DivCd, FieldID, CaptionNm, DispYn, LockYn                              
                                           , CreId, CreIP, CreDt)
                            VALUES ('{DivCd}'
                                  , '{FieldID}'
                                  , '{CaptionNm}'
                                  , '{DispYn}'
                                  , '{LockYn}'

                                  , '{WrGlobal.LoginID}'
                                  , '{WrGlobal.ClientHostName}'
                                  , GETDATE())";

            return db.ExecuteQuery(query);
        }

        public bool Upd_CdOption()
        {
            db.Edit_Property(this);

            query = $@"UPDATE SM_CodeOption
                          SET FieldID = '{FieldID}'
                            , CaptionNm = '{CaptionNm}'
                            , DispSeq = {DispSeq.ConvertStr_Empty_To_Null()}
                            , DispYn = '{DispYn}'
                            , LockYn = '{LockYn}'

                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()


            WHERE DivCd = '{DivCd}'
                          AND FieldID = '{FieldID}'";

            return db.ExecuteQuery(query);
        }
    }
}
