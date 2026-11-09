using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.CM
{
    public class CM_Process
    {
        #region Property

        public string CorpId { get; set; }

        public string ProcessCode { get; set; }

        public string WorkType { get; set; }

        public string WorkDept { get; set; }

        public string ProcessName { get; set; }

        public float? StdPrice { get; set; }

        public int? CycleTime { get; set; }

        public int? LeadTime { get; set; }

        public string DisplayYn { get; set; }

        public string Warehouse { get; set; }

        public string Remark { get; set; }

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

        public DataTable SelectCM_Process(bool allYn = false)
        {
            query = "";

            //조회항목에서 전체를 추가하고 싶을경우
            if (allYn)
            {
                query += @"SELECT 'All' AS 코드, '전체' AS 명칭
                        UNION ALL ";
            }

            query += $@" SELECT ProcessCode AS 코드, ProcessName AS 명칭
                           FROM CM_Process
                          WHERE CorpId = '{WrGlobal.CorpID}' ";

            //특정 Process만 보고 싶은 경우
            if (SearchProcess != null &&
                SearchProcess.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < SearchProcess.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" ProcessCode = '{SearchProcess[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable SelectCM_Process_SINGWANG(bool allYn = true)
        {
            query = "";

            //조회항목에서 전체를 추가하고 싶을경우
            if (allYn)
            {
                query += @"SELECT 'All' AS 코드, '전체' AS 명칭
                        UNION ALL ";
            }

            query += $@" SELECT ProcessCode AS 코드, ProcessName AS 명칭
                           FROM CM_Process
                          WHERE CorpId = '{WrGlobal.CorpID}' ";

            //특정 Process만 보고 싶은 경우
            if (SearchProcess != null &&
                SearchProcess.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < SearchProcess.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" ProcessCode = '{SearchProcess[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_CM_Process_JINSUNG()
        {
            string sql = " SELECT CorpId, ProcessCode, WorkType, WorkDept, ProcessName, " +
                         "        StdPrice, CycleTime, LeadTime, DisplayYn, Remark, " +
                         "        CreId, CreIP, CreDt, ModId, ModIP, " +
                         "        ModDt, Warehouse " +
                         "   FROM CM_Process " +
                         "  WHERE CorpId = '" + CorpId + "' ";

            DataTable dtCM_Process = db.GetDataTable(sql);

            return dtCM_Process;
        }

        public DataTable SelectCM_ProcessByName(bool allYn = false)
        {
            query = "";

            //조회항목에서 전체를 추가하고 싶을경우
            if (allYn)
            {
                query += @"SELECT 'All' AS 코드, '전체' AS 명칭
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
            string sql = "SELECT 'ALL' 코드, '전체' 공정명 UNION ALL ";
            sql += " SELECT ProcessCode 코드, ProcessName 공정명 " +
                         "   FROM CM_Process " +
                         "  WHERE CorpId = '" + $"{WrGlobal.CorpID}" + "' ";

            DataTable dtCM_Process = db.GetDataTable(sql);

            return dtCM_Process;
        }

        public DataTable CheckCM_Process()
        {
            string sql = $@" SELECT ProcessCode 
                                         FROM CM_Process
                                       WHERE ProcessCode = '{ProcessCode}'";

            DataTable dtCM_Process = db.GetDataTable(sql);

            return dtCM_Process;
        }

        public bool CheckProcessCode()
        {
            query = $@"SELECT ProcessCode
                                 FROM CM_Process
                               WHERE ProcessCode = '{ProcessCode}'";

            DataTable dt = db.GetDataTable(query);
            if (dt.Rows.Count > 0) return false;
            else return true;
        }

        public void InsertUpdateCM_Process()
        {
            string sql = " IF EXISTS ( " +
                         "       SELECT CorpId, ProcessCode " +
                         "         FROM CM_Process " +
                         "        WHERE CorpId = '" + CorpId + "' " +
                         "          AND ProcessCode = '" + ProcessCode + "') " +
                         "       UPDATE CM_Process " +
                         "          SET WorkType = '" + WorkType + "', " +
                         "              WorkDept = '" + WorkDept + "', " +
                         "              ProcessName = '" + ProcessName + "', " +
                         "              StdPrice = '" + StdPrice + "', " +
                         "              CycleTime = '" + CycleTime + "', " +

                         "              LeadTime = '" + LeadTime + "', " +
                         "              DisplayYn = '" + DisplayYn + "', " +
                         "              Warehouse = '" + Warehouse + "', " +
                         "              Remark = '" + Remark + "', " +
                         "              ModId = '" + ModId + "', " +

                         "              ModIP = '" + ModIP + "', " +
                         "              ModDt = '" + ModDt + "' " +
                         "        WHERE CorpId = '" + CorpId + "' " +
                         "          AND ProcessCode = '" + ProcessCode + "' " +
                         " ELSE " +
                         "       INSERT INTO CM_Process ( " +
                         "              CorpId, ProcessCode, WorkType, WorkDept, ProcessName, " +
                         "              StdPrice, CycleTime, LeadTime, DisplayYn, Warehouse, " +
                         "              Remark, CreId, CreIP, CreDt " +
                         "       )   VALUES   ( " +
                         "       '" + CorpId + "', " +
                         "       '" + ProcessCode + "', " +
                         "       '" + WorkType + "', " +
                         "       '" + WorkDept + "', " +
                         "       '" + ProcessName + "', " +

                         "       '" + StdPrice + "', " +
                         "       '" + CycleTime + "', " +
                         "       '" + LeadTime + "', " +
                         "       '" + DisplayYn + "', " +
                         "       '" + Warehouse + "', " +

                         "       '" + Remark + "', " +
                         "       '" + CreId + "', " +
                         "       '" + CreIP + "', " +
                         "       '" + CreDt + "') ";

            db.ExecuteQuery(sql);
        }

        public void DeleteCM_Process()
        {
            string sql = " DELETE " +
                         "   FROM CM_Process " +
                         "  WHERE CorpId = '" + CorpId + "' " +
                         "    AND ProcessCode = '" + ProcessCode + "' ";

            db.ExecuteQuery(sql);
        }
    }
}
