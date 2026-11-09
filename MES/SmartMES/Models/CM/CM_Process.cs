using SmartMES.Common;
using System.Data;
using SmartCommon.Common;

namespace SmartMES.Models.CM
{
    class CM_Process
    {
        #region Property

        public string CorpId { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string ProcessType { get; set; }
        public string WorkType { get; set; }
        public string WorkDept { get; set; }
        public float? StdPrice { get; set; }
        public int? CycleTime { get; set; }
        public string WorkUnit { get; set; }
        public string QtyUnit { get; set; }
        public string Remark { get; set; }
        public string UseYn { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public string CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public string ModDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public string[] SearchProcess;

        public DataTable Select_CM_Process()
        {
            query = $@" SELECT  '' Status
                                , CorpId
                                , ProcessCode
                                , ProcessName
                                , WorkType
                                , WorkDept
                                , StdPrice
                                , CycleTime
                                , WorkUnit
                                , QtyUnit
                                , Remark
                                , UseYn
                                , CreId
                                , CreIP
                                , CreDt
                                , ModId
                                , ModIP
                                , ModDt
                                , ProcessType
                           FROM CM_Process
                          WHERE CorpId = '{WrGlobal.CorpID}'
                ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Cm_ProcessByWorkType()
        {
            query += $@" SELECT ProcessCode, ProcessName , '' LineCode, '' LineName
                           FROM CM_Process
                          WHERE CorpId = '{WrGlobal.CorpID}'
                        AND UseYn = 'Y'
                        AND WorkType = '{WorkType}'
                ";

            return db.GetDataTable(query);
        }

        public DataTable SelectCM_Process(bool allYn, string[] processList)
        {
            query = "";

            //조회항목에서 전체를 추가하고 싶을경우
            if (allYn)
            {
                query += @"SELECT'ALL' AS 코드, '전체' AS 명칭
                        UNION ALL ";
            }

            query += $@" SELECT ProcessCode AS 코드, ProcessName AS 명칭
                           FROM CM_Process
                          WHERE CorpId = '{WrGlobal.CorpID}'
                            AND UseYn = 'Y'
                            ";

            //특정 Process만 보고 싶은 경우
            if (processList != null &&
                processList.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < processList.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" ProcessCode = '{processList[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable SelectCM_ProcessByName(bool allYn = false)
        {
            query = "";

            //조회항목에서 전체를 추가하고 싶을경우
            if (allYn)
            {
                query += @"SELECT'ALL' AS 코드, '전체' AS 명칭
                        UNION ALL ";
            }

            query += $@" SELECT ProcessCode AS 코드, ProcessName AS 명칭
                         FROM CM_Process
                         WHERE CorpId = '{WrGlobal.CorpID}'
                         AND ProcessName LIKE '%{ProcessName}%' ";


            return db.GetDataTable(query);
        }

        public DataTable SelectCM_ProcessLookup()
        {
            string sql = "SELECT'ALL' 코드, '전체' 공정명 UNION ALL ";
            sql += " SELECT ProcessCode 코드, ProcessName 공정명 " +
                         "   FROM CM_Process " +
                         "  WHERE CorpId = '" + $"{WrGlobal.CorpID}" + "' " +
                         "  AND UseYn = 'Y'";

            DataTable dtCM_Process = db.GetDataTable(sql);

            return dtCM_Process;
        }

        public bool Ins_Cm_Process()
        {
            query = $@"
                        INSERT INTO CM_Process
                        (
                             CorpId
                            , ProcessCode
                            , ProcessName
                            , WorkType
                            , WorkDept
                            , StdPrice
                            , CycleTime
                            , WorkUnit
                            , QtyUnit
                            , Remark
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ProcessType
                        ) VALUES
                        (
                             '{WrGlobal.CorpID}'
                            , CASE WHEN '{WrGlobal.ProcOption["BS"]["Process_AutoNo"]}' = 'Y'
                                    THEN dbo.FN_Create_Cd_Process('{WrGlobal.CorpID}')
                                    ELSE '{ProcessCode.ConvertStr_Empty_To_Null()}'
                                END
                            , {ProcessName.ConvertStr_Empty_To_Null()}
                            , {WorkType.ConvertStr_Empty_To_Null()}
                            , {WorkDept.ConvertStr_Empty_To_Null()}
                            , {StdPrice.ConvertFloat_Null_To_Zero()}
                            , {CycleTime.ConvertInt_Null_To_Zero()}
                            , {WorkUnit.ConvertStr_Empty_To_Null()}
                            , {QtyUnit.ConvertStr_Empty_To_Null()}
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , {UseYn.ConvertStr_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                            , {ProcessType.ConvertStr_Empty_To_Null()}
                        )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Cm_Process()
        {
            query = $@"
                        UPDATE CM_Process SET
                              ProcessName  = {ProcessName.ConvertStr_Empty_To_Null()}
                            , WorkType     = {WorkType.ConvertStr_Empty_To_Null()}
                            , WorkDept     = {WorkDept.ConvertStr_Empty_To_Null()}
                            , StdPrice     = {StdPrice.ConvertFloat_Null_To_Zero()}
                            , CycleTime    = {CycleTime.ConvertInt_Null_To_Zero()}
                            , WorkUnit     = {WorkUnit.ConvertStr_Empty_To_Null()}
                            , QtyUnit      = {QtyUnit.ConvertStr_Empty_To_Null()}
                            , Remark       = {Remark.ConvertStr_Empty_To_Null()}
                            , UseYn        = {UseYn.ConvertStr_Empty_To_Null()}
                            , ModId        = '{WrGlobal.LoginID}'
                            , ModIP        = '{WrGlobal.ClientHostName}'
                            , ModDt        = GETDATE()
                            , ProcessType  = {ProcessType.ConvertStr_Empty_To_Null()}
                        WHERE CorpId      =  '{WrGlobal.CorpID}'
                          AND ProcessCode = '{ProcessCode}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_Cm_Process()
        {
            query = $@"
                DELETE CM_Process
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND ProcessCode = '{ProcessCode}'
                ";

            return db.ExecuteQuery(query);
        }

    }
}
