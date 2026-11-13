using SmartMES.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public string ChannelName { get; set; }
        public string SourceShipToName { get; set; }
        public string SourceSoldToName { get; set; }

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

        /// <summary>기존 단가와 판매품목의 미등록 단가 조회</summary>
        public DataTable SelectCustomerPrice()
        {
            // 모든 사용자가 같은 포케스팅 표시 설정 사용
            string cust = CustCode.Replace("'", "''");
            string type = PriceType.Replace("'", "''");
            query = $@"
                SELECT '' Status, p.*, i.ItemName, i.Size, i.UnitCode
                FROM CM_ItemPrice p
                LEFT JOIN CM_Item i ON i.CorpId=p.CorpId AND i.ItemCode=p.ItemCode
                WHERE p.CorpId='{WrGlobal.CorpID}' AND p.CustCode='{cust}'
                  AND ('{type}'='ALL' OR p.PriceType='{type}')
                ORDER BY p.ChannelName, p.SourceOrder, p.ItemCode, p.ApplyDate DESC";
            return db.GetDataTable(query);
        }

        /// <summary>거래처·품목별 공통 포케스팅 표시 설정 저장</summary>
        // ItemCode와 PriceType이 빈 값이면 거래처 전체 설정, 값이 있으면 해당 거래처의 품목 설정
        internal static void SaveForecastUse(string custCode, string itemCode, string priceType, string useYn)
        {
            using (var connection = new SqlConnection(Database.m_ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    IF @ItemCode='' AND @PriceType=''
                        UPDATE CM_Cust SET ForecastUseYn=@UseYn, ModId=@User, ModDt=GETDATE()
                        WHERE CorpId=@CorpId AND CustCode=@CustCode;
                    ELSE
                        UPDATE CM_ItemPrice SET ForecastUseYn=@UseYn, ModId=@User, ModDt=GETDATE()
                        WHERE CorpId=@CorpId AND CustCode=@CustCode
                          AND ItemCode=@ItemCode AND PriceType=@PriceType;";
                command.Parameters.AddWithValue("@CorpId", WrGlobal.CorpID);
                command.Parameters.AddWithValue("@CustCode", custCode);
                command.Parameters.AddWithValue("@ItemCode", itemCode ?? "");
                command.Parameters.AddWithValue("@PriceType", priceType ?? "");
                command.Parameters.AddWithValue("@UseYn", useYn);
                command.Parameters.AddWithValue("@User", WrGlobal.LoginID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>거래처별 품목의 단가·기간·비고 수정, 미입력 값 보존</summary>
        internal static void SaveCustomerPrice(DataRow row)
        {
            using (var connection = new SqlConnection(Database.m_ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE CM_ItemPrice
                    SET CustCode=@CustCode, ItemCode=@ItemCode, PriceType=@PriceType, ChannelName=@ChannelName,
                        SourceShipToName=@SourceShipToName, SourceSoldToName=@SourceSoldToName,
                        Price=@Price, ApplyDate=@ApplyDate, EndDate=@EndDate,
                        CustAlias=@CustAlias, Remark=@Remark,
                        ModId=@User, ModIP=@IP, ModDt=GETDATE()
                    WHERE CorpId=@CorpId AND Seq=@Seq AND CustCode=@OriginalCustCode
                      AND EXISTS (SELECT 1 FROM CM_Item WHERE CorpId=@CorpId AND ItemCode=@ItemCode)
                      AND EXISTS (SELECT 1 FROM CM_Cust WHERE CorpId=@CorpId AND CustCode=@CustCode AND UseYn='Y')";
                command.Parameters.AddWithValue("@CorpId", WrGlobal.CorpID);
                // 변경 전 거래처로 원본 행 확인 후 선택한 거래처에 연결
                command.Parameters.AddWithValue("@OriginalCustCode", row["CustCode", DataRowVersion.Original]);
                command.Parameters.AddWithValue("@Seq", row["Seq"]);
                command.Parameters.AddWithValue("@ItemCode", row["ItemCode"]);
                command.Parameters.AddWithValue("@PriceType", row["PriceType"]);
                command.Parameters.Add("@ChannelName", SqlDbType.NVarChar, 100).Value = row["ChannelName"];
                command.Parameters.Add("@SourceShipToName", SqlDbType.NVarChar, 200).Value = row["SourceShipToName"];
                command.Parameters.Add("@SourceSoldToName", SqlDbType.NVarChar, 200).Value = row["SourceSoldToName"];
                command.Parameters.AddWithValue("@CustCode", row["CustCode"]);
                command.Parameters.Add("@Price", SqlDbType.Decimal).Value = row["Price"];
                command.Parameters["@Price"].Precision = 18;
                command.Parameters["@Price"].Scale = 4;
                command.Parameters.Add("@ApplyDate", SqlDbType.Date).Value = row["ApplyDate"];
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = row["EndDate"];
                command.Parameters.Add("@CustAlias", SqlDbType.NVarChar, 30).Value = row["CustAlias"];
                command.Parameters.Add("@Remark", SqlDbType.NVarChar, 500).Value = row["Remark"];
                command.Parameters.AddWithValue("@User", WrGlobal.LoginID);
                command.Parameters.AddWithValue("@IP", WrGlobal.ClientHostName);
                connection.Open();
                if (command.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("수정할 품목이 없습니다. 다시 조회해 주세요.");
            }
        }

        internal static DataTable SelectItems()
        {
            using (var connection = new SqlConnection(Database.m_ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT ItemCode AS 품목코드, ItemName AS 품명, Size AS 규격, UnitCode AS 단위 FROM CM_Item WHERE CorpId=@CorpId AND UseYn='Y' ORDER BY ItemName";
                command.Parameters.AddWithValue("@CorpId", WrGlobal.CorpID);
                var table = new DataTable();
                using (var adapter = new SqlDataAdapter(command))
                    adapter.Fill(table);
                return table;
            }
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
                            , ChannelName
                            , SourceShipToName
                            , SourceSoldToName
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
                                {ChannelName.ConvertStr_Empty_To_Null()},
                                {SourceShipToName.ConvertStr_Empty_To_Null()},
                                {SourceSoldToName.ConvertStr_Empty_To_Null()},
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
