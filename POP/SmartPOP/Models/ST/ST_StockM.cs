using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.ST
{
    class StockMaster
    {
        public string CorpId { get; set; }

        public string InOutNo { get; set; }

        public string OutDate { get; set; }

        public string CustCode { get; set; }

        public string FromHouseCode { get; set; }

        public string ToHouseCode { get; set; }

        public string WriteID { get; set; }

        public DateTime? WriteDate { get; set; }

        public string DeptCode { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        public string Status { get; set; }

        Database db = new Database();
        string query = "";

        public bool InsStockMaster_ProcMaterialIn()
        {
            query = $@"INSERT INTO StockMaster (
                            CorpId, InOutNo, OutDate
                            ,FromHouseCode, ToHouseCode
                            , WriteID, WriteDate
                            , CreId, CreIP, CreDt
                        ) VALUES (
                                  '{WrGlobal.CorpID}', '{InOutNo}', '{DateTime.Now.Date.ToString("yyyy-MM-dd")}'
                                , '{FromHouseCode}', '{ToHouseCode}'
                                , '{WrGlobal.LoginID}', GETDATE()
                                , '{WrGlobal.LoginID}', HOST_NAME(), GETDATE()
                        )
                        ";

            return db.ExecuteQuery(query);
        }

        public bool InsStockMaster_ByChemicalSubstancIn()
        {
            query = $@"
                        INSERT INTO StockMaster
                        (
                            CorpID
                            , InOutNo
                            , OutDate
                            , FromHouseCode
                            , ToHouseCode
                            , WriteID
                            , WriteDate
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES 
                        (
                           '{WrGlobal.CorpID}'
                            , '{InOutNo}'
                            , GETDATE()
                            , '{FromHouseCode}'
                            , '{ToHouseCode}'
                            , '{WrGlobal.LoginID}'
                            , GETDATE()
                            , '{WrGlobal.LoginID}'
                            , HOST_NAME()
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Ins_StockMaster()
        {
            query = $@"INSERT INTO ST_StockM
                                    VALUES(
                                   '{WrGlobal.CorpID}',
                                   '{InOutNo}',
                                   '{OutDate}',
                                   '{CustCode}',
                                   '{FromHouseCode}',
                                   '{ToHouseCode}',
                                   '{WriteID}',
                                    GETDATE(),
                                   null,
                                   '{Remark}',
                                   '{WrGlobal.LoginID}',
                                   '{WrGlobal.ClientHostName}',
                                    GETDATE(),
                                   null,
                                   null,
                                   null,
                                   '{Status}')";

            return db.ExecuteQuery(query);
        }
    }
}
