using SmartCommon.Common;
using System;
using System.Data;
using SmartMES.Common;

namespace SmartMES.Models.CM
{
    public class CM_DefectCode
    {
        #region Property

        public string CorpId { get; set; }

        public string FaultCode { get; set; }

        public string ProcessID { get; set; }

        public string Title { get; set; }

        public string ClassID { get; set; }

        public string ExamType { get; set; }

        public string Remark { get; set; }

        public string UseYn { get; set; }

        public string POPDisplayYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        #endregion

        Database db = new Database();
		string query = "";

		public DataTable Select_CM_DefectCode()
		{
            ProcessID = ProcessID.ConvertString_Null_To_Empty() == "ALL" ? "" : ProcessID.ConvertString_Null_To_Empty();

            query = $@"
                SELECT ''AS Status
                    , CorpId
                    , FaultCode
                    , ProcessID
                    , Title
                    , ClassID
                    , ExamType
                    , Remark
                    , UseYn
	                , POPDisplayYn
                    , CreId
                    , CreIP
                    , CreDt
                    , ModId
                    , ModIP
                    , ModDt
                FROM CM_DefectCode
                WHERE CorpId = '{WrGlobal.CorpID}'
                ";

            if (ProcessID.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND ProcessID = '{ProcessID}'";
            }

            return db.GetDataTable(query);
		}

		public bool Ins_CM_DefectCode()
		{
			query = $@"

                INSERT INTO CM_DefectCode
                ( CorpId
                  , FaultCode
                  , ProcessID
                  , Title
                  , ClassID
                  , ExamType
                  , Remark
                  , UseYn
                  , POPDisplayYn
                  , CreId
                  , CreIP
                  , CreDt
                )
                VALUES
                (
                 '{WrGlobal.CorpID}'
                ,'{FaultCode}'
                ,'{ProcessID}'
                ,'{Title}'
                , {ClassID.ConvertStr_Empty_To_Null()}
                , {ExamType.ConvertStr_Empty_To_Null()}
                , {Remark.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , {POPDisplayYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
                )
                ";

			return db.ExecuteQuery(query);
		}

        public bool Upd_CM_DefectCode()
		{
            query = $@"

                UPDATE CM_DefectCode
                SET
                  ProcessID = {ProcessID.ConvertStr_Empty_To_Null()}
                , Title = {Title.ConvertStr_Empty_To_Null()}
                , ClassID = {ClassID.ConvertStr_Empty_To_Null()}
                , ExamType = {ExamType.ConvertStr_Empty_To_Null()}
                , Remark = {Remark.ConvertStr_Empty_To_Null()}
                , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                , POPDisplayYn = {POPDisplayYn.ConvertStr_Empty_To_Null()}
                , ModId = '{WrGlobal.LoginID}'
                , ModIP = '{WrGlobal.ClientHostName}'
                , ModDt = GETDATE()
                WHERE Corpid = '{WrGlobal.CorpID}'
                AND FaultCode = '{FaultCode}'
                ";

            return db.ExecuteQuery(query);
		}

        public bool Del_CM_DefectCode()
        {
            query = $@"
                DELETE CM_DefectCode
                WHERE Corpid = '{WrGlobal.CorpID}'
                AND FaultCode = '{FaultCode}'
                ";

            return db.ExecuteQuery(query);
        }
    }
}
