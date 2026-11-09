using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.MD
{
    public class MD_MoldItem
    {
        #region Property

        public string MoldCode { get; set; }
        public string ItemCode { get; set; }
        public string UseYn { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MoldItem()
        {
            query = $@"
                        SELECT
                        '' Status
                        , MML.CorpId
                        , MML.MoldCode
                        , MML.ItemCode
                        , MML.ItemCode ItemName
                        , MML.UseYn
                        FROM MD_MoldItem MML
                        INNER JOIN CM_Item CM ON MML.CorpId = CM.CorpId AND MML.ItemCode = cm.ItemCode
                        WHERE MML.CorpId = '{WrGlobal.CorpID}'
                        AND MML.MoldCode = '{MoldCode}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_MD_MoldItem()
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"SELECT '' AS Status
                            , mi.MoldCode
                            , md.MoldMgmtNo
                            , mi.ItemCode
                            , mi.ItemCode AS ItemName
                            , ci.Alias
                            , ci.Size
                            , ci.UnitCode
                            , mi.UseYn
                            , mi.CreId
                            , mi.CreDt
                            , mi.ModId
                            , mi.ModDt
                         FROM MD_MoldItem mi
                    LEFT JOIN MD_Mold md ON mi.CorpId = md.CorpId AND md.MoldCode = mi.MoldCode
                    LEFT JOIN CM_Item ci ON mi.CorpId = ci.CorpId AND ci.ItemCode = mi.ItemCode
                        WHERE mi.CorpId = '{WrGlobal.CorpID}'
                          AND mi.MoldCode = {MoldCode.ConvertStr_Empty_To_Null()}";

            if (ItemCode != "NULL")
            {
                query += $"\nAND mi.ItemCode = {ItemCode}";
            }

            return db.GetDataTable(query);
        }

        public bool Ins_MD_MoldItem()
        {
            query = $@"
                        INSERT INTO MD_MoldItem
                        (
                            CorpId
                            , MoldCode
                            , ItemCode
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{MoldCode}'
                            , '{ItemCode}'
                            , '{UseYn}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_MD_MoldItem()
        {
            query = $@"
                        UPDATE MD_MoldItem
                        SET
                            UseYn = '{UseYn}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND ItemCode = '{ItemCode}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MD_MoldItem()
        {
            query = $@"
                        DELETE MD_MoldItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND ItemCode = '{ItemCode}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
