using SmartMES.Common;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.CM
{
    public class CM_Line
    {
        #region Property

        public string CorpId { get; set; }

        public string ProcessCode { get; set; }

        public string LineCode { get; set; }

        public string Title { get; set; }

        public string MachineCode { get; set; }

        public int? StdCT { get; set; }

        public int? RealCT { get; set; }

        public int? NectTime { get; set; }

        public string DisplayYn { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public string CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public string ModDt { get; set; }

        #endregion

        Database db = new Database();

        public DataTable SelectCM_Line()
        {
            string sql = " SELECT LineCode , Title  FROM CM_Line " +
                         "  WHERE CorpId = '" + CorpId + "' " +
                         "    AND ProcessCode = '" + ProcessCode + "' ";

            DataTable dtCM_Line = db.GetDataTable(sql);

            return dtCM_Line;

            //query = " SELECT CorpId, CreDt, CreIP, CreId, DisplayYn, " +
            //        "        LineCode, MachineCode, ModDt, ModIP, ModId, " +
            //        "        NectTime, ProcessCode, RealCT, Remark, StdCT, " +
            //        "        Title " +
            //        "   FROM CM_Line " +
            //        "  WHERE CorpId = '" + CorpId + "' " +
            //        "    AND ProcessCode = '" + ProcessCode + "' ";

            //DataTable dtCmCarType = db.GetDataSet(query);

            //return dtCmCarType;
        }
        
        public DataTable Select_Lup_Line()
        {
            string query = $@"SELECT LineCode 코드, LineName 라인명 
                                            FROM CM_Line
                                            WHERE CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_MachineByProcessing()
        {
            string query = $@"SELECT A.MachineCode 코드, A.LineName 설비명 
                                            FROM CM_Line A
                                            INNER JOIN CM_Process B ON A.ProcessCode = B.ProcessCode AND B.ProcessName Like '%가공%'
											WHERE A.CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public void InsertUpdateCM_Line()
        {
            string sql = " IF EXISTS ( " +
                         "       SELECT CorpId, ProcessCode, LineCode " +
                         "         FROM CM_Line " +
                         "        WHERE CorpId = '" + WrGlobal.CorpID + "' " +
                         "          AND ProcessCode = '" + ProcessCode + "' " +
                         "          AND LineCode = '" + LineCode + "') " +
                         "       UPDATE CM_Line " +
                         "          SET Title = '" + Title + "', " +
                         "              MachineCode = '" + MachineCode + "', " +
                         "              StdCT = '" + StdCT + "', " +
                         "              RealCT = '" + RealCT + "', " +
                         "              NectTime = '" + NectTime + "', " +

                         "              DisplayYn = '" + DisplayYn + "', " +
                         "              Remark = '" + Remark + "', " +
                         "              ModId = '" + ModId + "', " +
                         "              ModIP = '" + ModIP + "', " +
                         "              ModDt = '" + ModDt + "' " +
                         "        WHERE CorpId = '" + CorpId + "' " +
                         "          AND ProcessCode = '" + ProcessCode + "' " +
                         "          AND LineCode = '" + LineCode + "' " +
                         " ELSE " +
                         "       INSERT INTO CM_Line ( " +
                         "              CorpId, ProcessCode, LineCode, Title, MachineCode, " +
                         "              StdCT, RealCT, NectTime, DisplayYn, Remark, " +
                         "              CreId, CreIP, CreDt " +
                         "       )   VALUES   ( " +
                         "       '" + CorpId + "', " +
                         "       '" + ProcessCode + "', " +
                         "       '" + LineCode + "', " +
                         "       '" + Title + "', " +
                         "       '" + MachineCode + "', " +
                         "       '" + StdCT + "', " +
                         "       '" + RealCT + "', " +
                         "       '" + NectTime + "', " +
                         "       '" + DisplayYn + "', " +
                         "       '" + Remark + "', " +
                         "       '" + CreId + "', " +
                         "       '" + CreIP + "', " +
                         "       '" + CreDt + "') ";

            db.ExecuteQuery(sql);
        }

        public void DeleteCM_Line()
        {
            string sql = " DELETE " +
                         "   FROM CM_Line " +
                         "  WHERE CorpId = '" + WrGlobal.CorpID + "' " +
                         "    AND ProcessCode = '" + ProcessCode + "' " +
                         "    AND LineCode = '" + LineCode + "' ";

            db.ExecuteQuery(sql);
        }

        public void DeleteCM_LineByCM_Process()
        {
            string sql = " DELETE " +
                         "   FROM CM_Line " +
                         "  WHERE CorpId = '" + WrGlobal.CorpID + "' " +
                         "    AND ProcessCode = '" + ProcessCode + "' ";

            db.ExecuteQuery(sql);
        }
    }
}
