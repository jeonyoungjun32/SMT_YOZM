using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;

namespace SmartMES.Models.SM
{
    public class SM_AuthorGroup
    {
        #region Property
        public string CorpId { get; set; }

        public string GroupCode { get; set; }

        public string GroupName { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_SM_AuthorGroup()
        {
            query = $@"
                    SELECT '' AS Status
                        , CorpId
                        , GroupCode
                        , GroupName
                        , CreId
                        , CreIP
                        , CreDt
                        , ModId
                        , ModIP
                        , ModDt
                    FROM SM_AuthorGroup
                    ";

            return db.GetDataTable(query);
        }

        public string GetGroupCode_Dev()
        {
            query = $@"
            SELECT GroupCode
            FROM SM_AuthorGroup
            WHERE GroupName = '개발자'
            ";

            DataRow dr = db.GetDataRow(query);
            string code = "";

            if (dr != null)
            {
                code = dr["GroupCode"].ConvertString_Null_To_Empty();
            }

            return code;
        }

        public bool Ins_SM_AuthorGroup()
		{
            query = $@"
                INSERT INTO SM_AuthorGroup
                (
                    CorpId
                    , GroupCode
                    , GroupName
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                     '{WrGlobal.PublicCorpID}'
                    , '{GroupCode}'
                    , '{GroupName}'
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientIpAddress}'
                    , GETDATE()
                )
            ";

            return db.ExecuteQuery(query);
		}

        public bool Upd_SM_AuthorGroup()
        {
            query = $@"
                UPDATE SM_AuthorGroup
                SET
                      GroupName = '{GroupName}'
                    , ModId     = '{WrGlobal.LoginID}'
                    , ModIP     = '{WrGlobal.ClientIpAddress}'
                    , ModDt     = GETDATE()
                WHERE CorpId    = '{WrGlobal.PublicCorpID}'
                AND   GroupCode = '{GroupCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_SM_AuthorGroup()
		{
            query = $@"
                DELETE SM_AuthorGroup
                WHERE CorpId    = '{WrGlobal.PublicCorpID}'
                AND GroupCode   = '{GroupCode}'
            ";

            return db.ExecuteQuery(query);
        }
	}
}
