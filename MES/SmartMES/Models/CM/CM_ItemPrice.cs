using SmartMES.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCommon.Common;
using DevExpress.DataProcessing.InMemoryDataProcessor;

namespace SmartMES.Models.CM
{
    public class CM_ItemPrice
    {
        #region Property

        public int Seq { get; set; }
        public string ItemCode { get; set; }
        public string PriceType { get; set; }
        public string CustCode { get; set; }
        public string CustAlias { get; set; }
        public string ApplyDate { get; set; }
        public string EndDate { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable SelectMaterialPrice()
		{
            CustCode = CustCode.ConvertString_Null_To_Empty();
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT '' Status, CorpId, ItemCode, PriceType, CustCode, CustAlias, Price
                              , ApplyDate, EndDate, Seq, Remark
                              , CreId, CreIP, CreDt, ModId, ModIP, ModDt
                        FROM     CM_ItemPrice
                        WHERE  CorpId = '{WrGlobal.CorpID}'
                    ";

			if (CustCode != "ALL" && CustCode != "") query += $@" AND CustCode = '{CustCode}'";
			if (PriceType != "ALL") query += $@" AND PriceType = '{PriceType}'";
			if (ItemCode != "ALL" && ItemCode != "") query += $@" AND ItemCode = '{ItemCode}'";

			query += $"\n ORDER BY CustCode, ItemCode, ApplyDate DESC";

			DataTable dt = db.GetDataTable(query);

			return dt;
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

        public string Select_Cust_Alias()
        {
            query = $@" SELECT CustAlias 
                        FROM CM_ItemPrice
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CustCode = '{CustCode}' 
                        AND ItemCode = '{ItemCode}'";

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


        public DataRow Get_MaterialPrice()
        {
            query = $@"SELECT TOP 1 Price
                        FROM CM_ItemPrice
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND CustCode = '{CustCode}'
                        AND ItemCode = '{ItemCode}'
                        AND PriceType = '{PriceType}'
                        AND ApplyDate <= '{ApplyDate}'
                        ORDER BY SEQ DESC
                    ";

            return db.GetDataRow(query);
        }

        public decimal Get_MaterialPrice(string cust, string material)
        {
            query = $@"SELECT TOP 1 Price
                        FROM CM_ItemPrice
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND CustCode = '{cust}'
                        AND ItemCode = '{material}'
                        AND PriceType = '{PriceType}'
                        AND ApplyDate <= '{ApplyDate}'
                        ORDER BY SEQ DESC
                    ";

            DataRowView drv = db.GetDataRecord(query);

            if (drv != null) return drv[0].ConvertDecimal_Null_To_Zero();
            else return 0;
        }

        public DataRow SelectMaterialPrice_Dr()
        {
            query = $@"
                        SELECT TOP 1
                                Price
                        FROM     CM_ItemPrice
                        WHERE  CorpId = '{WrGlobal.CorpID}'
                        AND ItemCode = '{ItemCode}'
                        AND CustCode = '{CustCode}'
                        AND PriceType = '{PriceType}'
                        AND ApplyDate <= '{ApplyDate}'
                        ORDER BY ApplyDate DESC
                    ";

            return db.GetDataRow(query);
        }

        public decimal Get_MaterialPrice_byItemCode(string ItemCode)
        {
            query = $@"SELECT TOP 1 Price
                        FROM CM_ItemPrice
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND ItemCode = '{ItemCode}'
                        AND PriceType = '{PriceType}'
                        AND ApplyDate <= '{ApplyDate}'
                       
                    ";
            if (CustCode.ConvertString_Null_To_Empty() != "")
            {
                query += $@"AND CustCode = '{CustCode}'";
            }

            query += $"\nORDER BY SEQ DESC";

            DataRowView drv = db.GetDataRecord(query);
            if (drv != null) return drv[0].ConvertDecimal_Null_To_Zero();
            else return 0;
        }

        //등록
        public bool Ins_MaterialPrice()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO CM_ItemPrice (
                              CorpId
                            , ItemCode
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
                                '{ItemCode}',
                                '{PriceType}',
                                '{CustCode}',
                                {CustAlias.ConvertStr_Empty_To_Null()},
                                '{Price}',
                                {ApplyDate.ConvertStr_Empty_To_Null()},
                                {EndDate.ConvertStr_Empty_To_Null()},
                                '{Remark}',
                                '{WrGlobal.LoginID}',
                                '{WrGlobal.ClientHostName}',
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
		                        , ModId = '{WrGlobal.LoginID}'
                                , ModIP = '{WrGlobal.ClientHostName}'
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
                        AND Seq = '{Seq}' 
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MaterialPriceByCode()
        {
            query = $@"
                        DELETE CM_ItemPrice 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND ItemCode = '{ItemCode}' 
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ItemPrice()
        { 
            query = $@"UPDATE CM_ItemPrice
                                SET Price = {Price},
                                    ApplyDate = '{ApplyDate}'
                                , ModId = '{WrGlobal.LoginID}',
                                    ModDt = GETDATE()

                            WHERE CorpId = '{WrGlobal.CorpID}'
                                AND PriceType = {PriceType.ConvertStr_Empty_To_Null()}
                                AND ItemCode = {ItemCode.ConvertStr_Empty_To_Null()}
                                AND CustCode = {CustCode.ConvertStr_Empty_To_Null()}";

                return db.ExecuteQuery(query);
        }

        public bool Upd_ItemPrice_ByHD()
        {
            //Frm_BS_ItemPrice_History에서 History 삭제 시 최근 단가이력으로 Update
            query = $@"UPDATE CM_ItemPrice
                          SET Price = T.Price
		                    , ApplyDate = T.Apply_Date
		                    , ModId = '{WrGlobal.LoginID}'
		                    , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()

	                     FROM CM_ItemPrice pr,
                             (  SELECT TOP 1
                                       CorpId
                                     , Price_Div_Cd
                                     , ItemCode
                                     , Cust_Cd
                                     , Price
                                     , Apply_Date
                                  FROM BS_ItemPrice_History
                                 WHERE CorpId = '{WrGlobal.CorpID}'
                                   AND Price_Div_Cd = {PriceType.ConvertStr_Empty_To_Null()}
                                   AND ItemCode = {ItemCode.ConvertStr_Empty_To_Null()}
                                   AND Cust_Cd = {CustCode.ConvertStr_Empty_To_Null()}
                              ORDER BY Apply_Date DESC
                            ) T
                        WHERE T.CorpId = pr.CorpId
                          AND T.Price_Div_Cd = pr.Price_Div_Cd
                          AND T.ItemCode = pr.ItemCode
                          AND T.Cust_Cd = pr.Cust_Cd";

            return db.ExecuteQuery(query);
        }
    }
}
