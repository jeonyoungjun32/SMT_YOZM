using SmartCommon.Common;
using System;

namespace SmartMES.Models.SM
{
    public class SM_Author
	{
        #region Property
        public string CorpId { get; set; }

        public string MenuID { get; set; }

        public string AuthorCode { get; set; }

        public string AuthorName { get; set; }

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

        public bool Ins_SM_Author()
		{
            query = $@"
                INSERT INTO SM_Author
                (
                    CorpId
                    , MenuID
                    , AuthorCode
                    , AuthorName
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                     '{CorpId}'
                    , '{MenuID}'
                    , '{AuthorCode}'
                    , '{AuthorName}'
                    , '{UseYn}'
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientIpAddress}'
                    , GETDATE()
                )
            ";

            return db.ExecuteQuery(query);
		}

        public bool Del_SM_Author()
		{
            query = $@"
                DELETE SM_Author
                WHERE CorpId    = '{CorpId}'
                AND MenuID      = '{MenuID}'
                AND AuthorCode  = '{AuthorCode}'
            ";

            return db.ExecuteQuery(query);
        }
	}
}
