using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.CM
{
    public class CM_ItemPrice
    {
        #region Property

        public int Seq { get; set; }
        public string MaterialCode { get; set; }
        public string PriceType { get; set; }
        public string CustCode { get; set; }
        public string CustAlias { get; set; }
        public string ApplyDate { get; set; }
        public string EndDate { get; set; }
        public float Price { get; set; }
        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable SelectMaterialPrice()
        {
            query = $@"
                        SELECT '' Status, CorpId, MaterialCode, PriceType, CustCode, CustAlias, Price
                              , ApplyDate, EndDate, Seq, Remark
                              , CreId, CreIP, CreDt, ModId, ModIP, ModDt
                        FROM     CM_ItemPrice
                        WHERE  CorpId = '{WrGlobal.CorpID}'
                    ";

            if (CustCode != "All") query += $@" AND CustCode = '{CustCode}'";
            if (PriceType != "All") query += $@" AND PriceType = '{PriceType}'";

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public string Select_Cust_Alias()
        {
            query = $@" SELECT CustAlias 
                        FROM CM_ItemPrice
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CustCode = '{CustCode}' 
                        AND MaterialCode = '{MaterialCode}'";

            DataRow resultRow = db.GetDataRow(query);

            if (resultRow != null && resultRow["CustAlias"] != DBNull.Value)
            {
                return resultRow["CustAlias"].ToString();
            }
            else
            {
                return "";
            }
        }

        public decimal Get_MaterialPrice(string cust, string material)
        {
            query = $@"SELECT TOP 1 Price
                        FROM CM_ItemPrice
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND CustCode = '{cust}'
                        AND MaterialCode = '{material}'
                        AND PriceType = '{PriceType}'
                        AND ApplyDate <= '{ApplyDate}'
                        ORDER BY SEQ DESC
                    ";

            DataRowView drv = db.GetDataRecord(query);

            if (drv != null) return drv[0].ConvertDecimal_Null_To_Zero();
            else return 0;
        }

        public DataTable Select_MaterialPrice_Cust(string[] ItemType = null)
        {
            CustCode = CustCode.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT '' Status
								, cmp.ItemCode
								, cmp.PriceType
								, cmp.CustCode
								, cmp.CustAlias
								, cmp.Price
								, cmp.ApplyDate
								, cmp.EndDate
								, cmp.Seq

								, cm.ItemName
								, cm.Alias
								, cm.Size
								, cm.UnitCode
								, cm.ItemClass1
								, cm.ItemClass2
								, cm.ItemClass3
								, cm.PackQty

                        FROM CM_ItemPrice cmp
                        INNER JOIN CM_Item cm ON cmp.CorpId = cm.CorpId AND cmp.ItemCode = cm.ItemCode
                        WHERE  cmp.CorpId = '{WrGlobal.CorpID}' AND cmp.ApplyDate <= '{ApplyDate}'
                    ";

            if (CustCode != "ALL" && CustCode != "") query += $@" AND cmp.CustCode = '{CustCode}'";
            if (PriceType != "ALL") query += $@" AND cmp.PriceType = '{PriceType}'";
            if (ItemType != null && ItemType.Length > 0)
            {
                query += " AND (";
                for (int idx = 0; idx < ItemType.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" cm.ItemType = '{ItemType[idx]}'";
                }

                query += ")";
            }

            query += $"\n ORDER BY cmp.CustCode, cmp.ItemCode, ApplyDate DESC";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        //등록
        public bool Ins_MaterialPrice()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO CM_ItemPrice (
                              CorpId
                            , MaterialCode
                            , PriceType
                            , CustCode
                            , CustAlias
                            , Price
                            , ApplyDate
                            , EndDate
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt)
                            VALUES( 
                                '{WrGlobal.CorpID}',
                                '{MaterialCode}',
                                '{PriceType}',
                                '{CustCode}',
                                {CustAlias.ConvertStr_Empty_To_Null()},
                                '{Price}',
                                {ApplyDate.ConvertStr_Empty_To_Null()},
                                {EndDate.ConvertStr_Empty_To_Null()},
                                '{Remark}',
                                '{WrGlobal.LoginID}',
                                '{WrGlobal.IpAddress}',
                                GETDATE())";

            return db.ExecuteQuery(query);
        }

        //수정
        public bool Upd_MaterialPrice()
        {
            db.Edit_Property(this);

            query = $@"UPDATE CM_ItemPrice
                            SET Price = {Price}
                                , CustAlias = {CustAlias.ConvertStr_Empty_To_Null()}
                                , remark = {Remark.ConvertStr_Empty_To_Null()}
                                , ApplyDate = {ApplyDate.ConvertStr_Empty_To_Null()}
                                , EndDate = {EndDate.ConvertStr_Empty_To_Null()}
		                        , ModId = '{WrGlobal.CorpID}'
                                , ModIP = '{WrGlobal.IpAddress}'
                                , ModDt = GETDATE()
                            WHERE CorpId = '{WrGlobal.CorpID}' AND Seq = '{Seq}' 
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MaterialPrice()
        {
            query = $@"
                        DELETE CM_ItemPrice 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        Seq = '{Seq}' 
                    ";

            return db.ExecuteQuery(query);
        }

    }
}
