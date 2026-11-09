using SmartCommon.Common;
using System;
using System.Data;
using SmartMES.Common;

namespace SmartMES.Models.CM
{
    public class CM_Dept
    {
        #region Property

        public string CorpId { get; set; }

        public string DeptCode { get; set; }

        public string ParentId { get; set; }

        public string DeptName { get; set; }

        public int RowNum { get; set; }

        public int GroupLevel { get; set; }

        public string GroupYn { get; set; }

        public string Bigo { get; set; }

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

		public DataTable Select_CM_Dept()
		{
			query = $@"
                SELECT ''AS Status
                    , CorpId
                    , DeptCode
                    , ParentId
                    , DeptName
                    , RowNum
                    , GroupLevel
                    , GroupYn
                    , Bigo
	                , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                    , ModId
                    , ModIP
                    , ModDt
                FROM CM_Dept
                WHERE CorpId = '{WrGlobal.CorpID}'
                ";

            return db.GetDataTable(query);
		}

		public bool Ins_CM_Dept()
		{
			query = $@"

                INSERT INTO CM_Dept
                ( CorpId
                  , DeptCode
                  , DeptName
                  , Bigo
                  , UseYn
                  , CreId
                  , CreIP
                  , CreDt
                )
                VALUES
                (
                 '{WrGlobal.CorpID}'
                ,'{DeptCode}'
                , {DeptName.ConvertStr_Empty_To_Null()}
                , {Bigo.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
                )
                ";

			return db.ExecuteQuery(query);
		}

        public bool Upd_CM_Dept()
		{
            query = $@"

                UPDATE CM_Dept
                SET
                  DeptName = {DeptName.ConvertStr_Empty_To_Null()}
                , Bigo = {Bigo.ConvertStr_Empty_To_Null()}
                , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                , ModId = '{WrGlobal.LoginID}'
                , ModIP = '{WrGlobal.ClientHostName}'
                , ModDt = GETDATE()
                WHERE Corpid = '{WrGlobal.CorpID}'
                AND DeptCode = '{DeptCode}'
                ";

            return db.ExecuteQuery(query);
		}

        public bool Del_CM_Dept()
        {
            query = $@"
                DELETE CM_Dept
                WHERE Corpid = '{WrGlobal.CorpID}'
                AND DeptCode = '{DeptCode}'
                ";

            return db.ExecuteQuery(query);
        }
    }
}
