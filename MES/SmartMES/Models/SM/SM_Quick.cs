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
	class SM_Quick
	{
        #region Property
        public string CorpId { get; set; }

        public string EmployeeId { get; set; }

        public string Id { get; set; }

        public string Title_ko { get; set; }

        public int SortNum { get; set; }

        #endregion

        Database db = new Database();
		string query = "";

        public DataTable Select_SM_Quick()
        {
            query = $@"
                SELECT '' AS Status
                     , CorpId
                     , EmployeeId
                     , Id
                     , Title_ko
                     , SortNum
                     , CreId
                     , CreIP
                     , CreDt
                     , ModId
                     , ModIP
                     , ModDt
                  FROM dbo.SM_Quick
                 WHERE CorpId = '{CorpId}"
            ;

            return db.GetDataTable(query);
        }

        public bool Ins_SM_Quick()
        {
            query = $@"
            INSERT INTO dbo.SM_Quick
            (
                  CorpId
                , EmployeeId
                , Id
                , Title_ko
                , SortNum
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{CorpId}'
                , '{EmployeeId}'
                , '{Id}'
                , {Title_ko.ConvertStr_Empty_To_Null()}
                , {SortNum.ConvertNumeric_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_SM_Quick()
        {
            query = $@"
                UPDATE dbo.SM_Quick
                   SET Title_ko = {Title_ko.ConvertStr_Empty_To_Null()}
                     , SortNum = {SortNum.ConvertNumeric_Empty_To_Null()}
                     , ModId = '{WrGlobal.LoginID}'
                     , ModIP = '{WrGlobal.ClientHostName}'
                     , ModDt = GETDATE()
                 WHERE CorpId = '{CorpId}'
                   AND EmployeeId = '{EmployeeId}'
                   AND Id = '{Id}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_SM_Quick()
        {
            query = $@"
                DELETE dbo.SM_Quick
                 WHERE CorpId = '{CorpId}'
                   AND EmployeeId = '{EmployeeId}'
                   AND Id = '{Id}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_SM_Quick_EmployeeId()
        {
            query = $@"
                DELETE dbo.SM_Quick
                 WHERE CorpId = '{CorpId}'
                   AND EmployeeId = '{EmployeeId}'
                ";

            return db.ExecuteQuery(query);
        }

    }
}
