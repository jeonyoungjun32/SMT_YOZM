using SmartCommon.Common;
using System;
using System.Data;

namespace SmartMES.Models.SM
{
    class SM_Notice
    {
        #region Properties
        public string CorpId { get; set; }
        public string BoardNo        { get; set; }
        public string Class          { get; set; }
        public string WriteDate      { get; set; }
        public string Subject        { get; set; }
        public string WriteName      { get; set; }
        public string Body           { get; set; }
        public string DisplayType    { get; set; }
        public string StartDate      { get; set; }
        public string EndDate        { get; set; }
        public string CreId          { get; set; }
        public string CreIP          { get; set; }
        public DateTime? CreDt          { get; set; }
        public string ModId          { get; set; }
        public string ModIP          { get; set; }
        public DateTime? ModDt { get; set; }

        public string SearchStart { get; set; }
        public string SearchEnd { get; set; }
        #endregion

        string query = "";
        Database db = new Database();

        public DataTable Select_SM_NoticeList()
        {
            query = $@"
                    SELECT CorpId
                        , BoardNo
                        , Class
                        , WriteDate
                        , Subject
                        , WriteName
                        , Body
                        , DisplayType
                        , StartDate
                        , EndDate
                        , CreId
                        , CreIP
                        , CreDt
                        , ModId
                        , ModIP
                        , ModDt
                    FROM SM_Notice
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND CONVERT(DATE, WriteDate) BETWEEN CONVERT(DATE, '{SearchStart}') AND CONVERT(DATE, '{SearchEnd}')
                    ORDER BY BoardNo DESC
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_SM_Notice_Top5(string date)
        {
            query = $@"
                    SELECT TOP 5 CorpId
                        , BoardNo
                        , Class
                        , WriteDate
                        , Subject
                        , WriteName
                        , Body
                        , DisplayType
                        , StartDate
                        , EndDate
                        , CreId
                        , CreIP
                        , CreDt
                        , ModId
                        , ModIP
                        , ModDt
                    FROM SM_Notice
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND CAST(StartDate AS DATE) <= '{date}'
                    AND CAST(EndDate AS DATE) >= '{date}'
                    ORDER BY BoardNo DESC
                    ";

            return db.GetDataTable(query);
        }


        public DataTable Select_SM_Notice_Popup(string sDate)
        {
            query = $@"
                    SELECT CorpId
                        , BoardNo
                        , Class
                        , WriteDate
                        , Subject
                        , WriteName
                        , Body
                        , DisplayType
                        , StartDate
                        , EndDate
                        , CreId
                        , CreIP
                        , CreDt
                        , ModId
                        , ModIP
                        , ModDt
                    FROM SM_Notice
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND (CONVERT(date, '{sDate}') BETWEEN CONVERT(date, StartDate) AND CONVERT(date, EndDate))
                    ORDER BY BoardNo DESC
                    ";

            return db.GetDataTable(query);
        }


        public bool Ins_SM_Notice()
        {
            query = $@"
                        INSERT INTO SM_Notice
                        (
                            CorpId
                            , BoardNo
                            , Class
                            , WriteDate
                            , Subject
                            , Body
                            , StartDate
                            , EndDate
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{BoardNo}'
                            , '{Class}'
                            , '{WriteDate}'
                            , '{Subject}'
                            , '{Body}'
                            , '{StartDate}'
                            , '{EndDate}'
                            , '{WrGlobal.LoginID}'
                            , HOST_NAME()
                            , GETDATE()
                        )
                        ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_SM_Notice()
        {
            query = $@"
                UPDATE SM_Notice
                SET Subject = '{Subject}'
                    , Body = '{Body}'
                    , StartDate = '{StartDate}'
                    , EndDate = '{EndDate}'
                    , ModId = '{WrGlobal.LoginID}'
                    , ModIP = '{WrGlobal.ClientHostName}'
                    , ModDt = GETDATE()
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND BoardNo = '{BoardNo}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_SM_Notice()
        {
            query = $@"DELETE FROM SM_Notice
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND BoardNo = '{BoardNo}'
                            ";

            return db.ExecuteQuery(query);
        }
    }
}
