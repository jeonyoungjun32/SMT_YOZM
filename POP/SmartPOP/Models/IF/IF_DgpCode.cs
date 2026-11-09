using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    public class IF_DgpCode
    {
        #region Property

        public string CTCode { get; set; }
        public string DgpCode { get; set; }
        public string DgpName { get; set; }
        public string DgpSpec { get; set; }
        public string StatCD { get; set; }
        public string Equip_Name { get; set; }
        public string MemoryCode { get; set; }
        public string Write_Address { get; set; }
        public string Gauge_Type { get; set; }
        public string Error_Or { get; set; }
        public decimal Error_Max { get; set; }
        public decimal Error_Min { get; set; }
        public string Value_Or { get; set; }
        public decimal Value_Max { get; set; }
        public decimal Value_Min { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_IF_DgpCodeList(string dgpCode = "", string gauge_Type = "")
        {
            query = $@"
                        SELECT cd.DgpCode 코드, cd.DgpName 명칭
                        FROM IF_DgpCode cd
                        WHERE 1=1
                    ";

            if (dgpCode != "")
            {
                query += $"\nAND cd.DgpCode = '{dgpCode}'";
            }

            if (gauge_Type != "")
            {
                query += $"\nAND cd.Gauge_Type like '%{gauge_Type}%'";
            }

            return db.GetDataTable(query);

        }

        public DataRow Select_DGPCounter(string MachineCode)
        {
            query = $@"SELECT
	                    mc.DgpCode
	                    , mc.DgpName
	                    , mc.Equip_Name
	                    , mc.Gauge_Type
	                    , mct.Counter
                    FROM DOOIL.dbo.IF_DgpCode mc
                    LEFT JOIN DOOIL.dbo.MTDGPCounter mct ON mc.DgpCode = mct.DgpCode
                    WHERE mc.Equip_Name = '{MachineCode}' AND Gauge_Type = '카운터' AND Value_Or = 'Y'";

            return db.GetDataRow(query);
        }

        public DataTable Select_DGPCode()
        {
            query = $@"
                        SELECT
                            Corpid
                            , CTCode
                            , DgpCode
                            , DgpName
                            , DgpSpec
                            , StatCD
                            , Equip_Name
                            , MemoryCode
                            , Write_Address
                            , Gauge_Type
                            , Value_Or
                            , Value_Max
                            , Value_Min
                        FROM IF_DGPCode
                        WHERE Corpid = '{WrGlobal.CorpID}'
                    ";

            if (CTCode != "")
            {
                query += $"\nAND CTCode = '{CTCode}'";
            }

            return db.GetDataTable(query);
        }
    }
}
