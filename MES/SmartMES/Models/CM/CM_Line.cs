using System;
using System.Data;
using SmartCommon.Common;

namespace SmartMES.Models.CM
{
    public class CM_Line
    {
        #region Property

        public string CorpId { get; set; }

        public string ProcessCode { get; set; }

        public string LineCode { get; set; }

        public string LineName { get; set; }

        public string Title { get; set; }

        public string MachineCode { get; set; }

        public int? StdCT { get; set; }

        public int? RealCT { get; set; }

        public int? NectTime { get; set; }

        public string DisplayYn { get; set; }

        public string Remark { get; set; }

        public string InHouseID { get; set; }

        public string OutHouseID { get; set; }

        public string BadHouseID { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public string CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public string ModDt { get; set; }


        private void ValidateRequired(string fieldName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{fieldName}은(는) 필수 항목입니다.");
            }
        }

        #endregion

        Database db = new Database();
        string query = "";

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

        public DataTable Select_CM_Line()
        {
            query = $@"SELECT '' AS Status,
                              LineCode,
                              LineName,
                              '' MachineCode,
                              '' ProcessCode,
                              '' ProcessName,
                              InHouseID,
                              OutHouseID,
                              BadHouseID,
                              CreId,
                              CreIP,
                              CreDt,
                              ModId,
                              ModIP,
                              ModDt
                         FROM CM_Line
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            //정렬
            if (WrGlobal.ProcOption["BS"]["Line_AutoNo"] == "Y")
            {
                query += " ORDER BY LineName ASC";
            }
            else
            {
                query += " ORDER BY LineCode ASC";
            }

            DataTable dtCM_Line = db.GetDataTable(query);

            return dtCM_Line;
        }

        public DataTable Select_Lup_Line(bool allYn)
        {
            query = "";

			if (allYn)
			{
                query = "SELECT'ALL' 코드, '전체' 명칭 UNION ALL ";
			}

           query += $@"SELECT LineCode 코드, LineName 명칭 
                                            FROM CM_Line
                                            WHERE CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public bool Ins_CM_Line()
        {
            ValidateRequired("라인명", LineName);
            //ValidateRequired("공정코드", ProcessCode);
            //ValidateRequired("설비코드", MachineCode);

            db.Edit_Property(this);

            query = $@"INSERT INTO CM_Line(CorpId, 
                                            LineCode, 
                                            ProcessCode, 
                                            LineName, 
                                            MachineCode, 
                                            InHouseID,
                                            OutHouseID,
                                            BadHouseID,
                                            UseYn,
                            	            CreId, CreIP, CreDt)
                            VALUES('{WrGlobal.CorpID}',
                                   CASE WHEN '{WrGlobal.ProcOption["BS"]["Line_AutoNo"]}' = 'Y'
                                        THEN dbo.FN_Create_Cd_Line('{WrGlobal.CorpID}')
                                        ELSE '{LineCode}'
                                   END,
                                    '{ProcessCode}',
                                   '{LineName}',
                                    '{MachineCode}',
                                    '{InHouseID}',
                                    '{OutHouseID}',
                                    '{BadHouseID}',
                                    'Y',
                                   '{WrGlobal.LoginID}',
                                   '{WrGlobal.ClientIpAddress}',
                                   GETDATE())";

            return db.ExecuteQuery(query);
        }

        public bool Upd_CM_Line()
        {
            db.Edit_Property(this);

            query = $@"UPDATE CM_Line
                      SET LineName = '{LineName}',
                          ProcessCode= '{ProcessCode}',
                          MachineCode='{MachineCode}',
                          InHouseID='{InHouseID}',
                          OutHouseID='{OutHouseID}',
                          BadHouseID='{BadHouseID}',
                          ModId = '{WrGlobal.LoginID}',
                          ModIP = '{WrGlobal.ClientIpAddress}',
                          ModDt = GETDATE()
                    WHERE CorpId = '{WrGlobal.CorpID}'
                      AND LineCode = '{LineCode}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_CM_Line()
        {
            query = $@"DELETE CM_Line
                    WHERE CorpId = '{WrGlobal.CorpID}'
                      AND LineCode = '{LineCode}'
                      ";
            return db.ExecuteQuery(query);
        }
    }
}
