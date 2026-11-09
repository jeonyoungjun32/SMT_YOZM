using SmartCommon.Common;
using System;
using System.Data;
using SmartMES.Common;

namespace SmartMES.Models.SM
{
    public class SM_UserAuthor
	{
        #region Property
        public string CorpId { get; set; }

        public string GroupCode { get; set; }

        public string MenuID { get; set; }

        public string AuthorCode { get; set; }

        public string UseYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

  
        public bool Ins_SM_UserAuthor()
		{
            query = $@"
                INSERT INTO SM_UserAuthor
                (
                    CorpId
                    , GroupCode
                    , MenuID
                    , AuthorCode
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                      '{CorpId}'
                    ,'{GroupCode}'
                    , '{MenuID}'
                    , '{AuthorCode}'
                    , '{UseYn}'
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientIpAddress}'
                    , GETDATE()
                )
            ";

            return db.ExecuteQuery(query);
		}

        public bool Del_SM_UserAuthor()
        {
            query = $@"
                DELETE SM_UserAuthor
                WHERE CorpId    = '{CorpId}'
                AND GroupCode   = '{GroupCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_AuthorByMenuID()
		{
            query = $@"
                DELETE SM_UserAuthor
                WHERE CorpId    = '{CorpId}'
                AND GroupCode   = '{GroupCode}'
                AND MenuID      = '{MenuID}'
            ";

            return db.ExecuteQuery(query);
        }
	}
}
