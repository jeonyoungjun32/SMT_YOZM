using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SmartMES.Models.CM
{
	public class CM_Item
    {
        internal static bool UsePurchasingFields => string.Equals(Database.DbName, "YOZM", StringComparison.OrdinalIgnoreCase);
        private SqlParameter[] purchasingParameters;

        /// <summary>품목 화면에서 입력한 구매·통관 항목을 기존 품목 저장 명령에 포함합니다.</summary>
        internal void SetPurchasingFields(DataRow row)
        {
            if (!UsePurchasingFields)
                return;

            purchasingParameters = new[]
            {
                TextParameter("PurchaseUnitCode", 20, row),
                new SqlParameter("@PurchaseConversionQty", SqlDbType.Decimal)
                    { Precision = 19, Scale = 6, Value = row["PurchaseConversionQty"] },
                new SqlParameter("@DeliveryOverTolerancePct", SqlDbType.Decimal)
                    { Precision = 9, Scale = 4, Value = row["DeliveryOverTolerancePct"] },
                TextParameter("HSCode", 30, row),
                TextParameter("HSName", 200, row),
                TextParameter("HSSpec", 200, row),
                TextParameter("HSUnit", 30, row)
            };
        }

        private static SqlParameter TextParameter(string field, int size, DataRow row)
        {
            string value = Convert.ToString(row[field]).Trim();
            return new SqlParameter("@" + field, SqlDbType.NVarChar, size)
            {
                Value = value.Length == 0 ? (object)DBNull.Value : value
            };
        }

        private bool ExecuteItemWrite()
        {
            if (purchasingParameters == null)
                return db.ExecuteQuery(query);
            using (var command = new SqlCommand(query))
            {
                command.Parameters.AddRange(purchasingParameters);
                return db.ExecuteCommand(command);
            }
        }

        #region Property

        public string CorpId { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string CustCode { get; set; }

        public string ItemType { get; set; }

        public string ItemClass1 { get; set; }

        public string ItemClass2 { get; set; }

        public string ItemClass3 { get; set; }

        public string Size { get; set; }

        public string Alias { get; set; }

        public string Standard { get; set; }

        public string UnitCode { get; set; }

        public float UnitWeight { get; set; }

        public string Type { get; set; }

        public string Texture { get; set; }

        public string Revision { get; set; }

        public string RoutingCode { get; set; }

        public string HouseID { get; set; }

        public string PackUnit { get; set; }

        public float PackQty { get; set; }

        public string Abbreviation { get; set; }

        public string EnglishAbbreviation { get; set; }

        public string ItemGroup { get; set; }

        public string SupplyType { get; set; }

        public string MaterialT { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public decimal NetWeight { get; set; }

        public decimal YogurtG { get; set; }

        public decimal YogurtKg { get; set; }

        public int PTLoadQty { get; set; }

        public string BarCode { get; set; }

        public string OldItemCode { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public float SaftyQty { get; set; }

        public float NormalQty { get; set; }

        public float OverQty { get; set; }

        public string DisplayYn { get; set; }

        public string ExamYn { get; set; }

        public string UseYn { get; set; }

        public string Remark { get; set; }
        #endregion

        Database db = new Database();
        string query;

        public DataTable SelectCM_Item(string[] ItemType = null, string[] ItemClass1 = null)
        {
            CustCode = CustCode.ConvertString_Null_To_Empty();

            query = $@" SELECT 
                            cm.CorpId
                            , cm.ItemCode
                            , cm.ItemName
                            , cm.CustCode
                            , cm.ItemType

                            , cm.Size
                            , cm.Alias
                            , cm.Standard
                            , cm.UnitCode
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A011', cm.UnitCode)) AS UnitName

                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Remark

                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.PackQty
                            , cm.Abbreviation
                            , cm.EnglishAbbreviation
                            , cm.ItemGroup
                            , cm.SupplyType
                            , cm.MaterialT
                            , cm.Width
                            , cm.Height
                            , cm.NetWeight
                            , cm.YogurtG
                            , cm.YogurtKg
                            , cm.PTLoadQty
                            , cm.BarCode
                            , cm.OldItemCode

                        FROM CM_Item cm
                        LEFT JOIN CM_Cust cc ON cm.CorpId = cc.CorpId AND cm.CustCode = cc.CustCode
                        LEFT JOIN CM_CateItem cii ON cm.CorpId = cii.CorpId and cii.CLASS='A010' and cm.ItemType = cii.ClassItemCode
                        WHERE cm.CorpId = '{WrGlobal.CorpID}'
                        AND cm.UseYN = 'Y'
                        ";

            //특정 구분만 보고 싶은 경우
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

            //특정 구분만 보고 싶은 경우
            if (ItemClass1 != null && ItemClass1.Length > 0)
            {
                query += " AND (";
                for (int idx = 0; idx < ItemClass1.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" cm.ItemClass1 = '{ItemClass1[idx]}'";
                }

                query += ")";
            }

            // 특정거래처만 보고싶은 경우
            if (CustCode != "") query += $"\n AND cm.CustCode = '{CustCode}'";

            DataTable dtCM_Item = db.GetDataTable(query);

            return dtCM_Item;
        }

        public DataTable Select_Item_Std(string[] ItemType)
        {
            db.Edit_Property(this);

            CustCode = CustCode.ConvertString_Null_To_Empty() == "ALL" ? "" : CustCode.ConvertString_Null_To_Empty();
            DisplayYn = DisplayYn.ConvertString_Null_To_Empty();

            string purchasingColumns = UsePurchasingFields ? @",
                              it.PurchaseUnitCode, it.PurchaseConversionQty, it.DeliveryOverTolerancePct,
                              it.HSCode, it.HSName, it.HSSpec, it.HSUnit" : "";

            //품목관리(Frm_BS_Item) 조회 Query
            query = $@"SELECT '' AS Status,
                              [it].ItemCode,
                              [it].ItemName,
                              [it].ItemType,

                              [it].SaftyQty,

                              [it].ItemClass1,
                              (select dbo.[FN_Get_CateItem_Title] ('{WrGlobal.CorpID}', 'A012', it.ItemClass1)) ItemClass1Name,
                              [it].ItemClass2,
                              [it].ItemClass3,

                              [it].Alias,
                              [it].Size,
                              [it].Standard,
                              [it].StartDate,
                              [it].EndDate,
                              [it].UnitCode,
                              [it].UnitWeight,
                              [it].Type,

                              [it].Texture,

                              [it].DisplayYn,
                              [it].ExamYn,
                              [it].UseYn,
                              [it].PackUnit,
                              [it].PackQty,
                              [it].Abbreviation,
                              [it].EnglishAbbreviation,
                              [it].ItemGroup,
                              [it].SupplyType,
                              [it].MaterialT,
                              [it].Width,
                              [it].Height,
                              [it].NetWeight,
                              [it].YogurtG,
                              [it].YogurtKg,
                              [it].PTLoadQty,
                              [it].BarCode,
                              [it].OldItemCode,

                              [it].Remark,

                              [it].CustCode,
                              [ct].CustName,

                              [it].CreId,
                              [it].CreIP,
                              [it].CreDt,
                              [it].ModId,
                              [it].ModIP,
                              [it].ModDt

                              , NULL btnImg --이미지등록용
                              , [it].HouseID
                              {purchasingColumns}


                         FROM CM_Item [it]
                    LEFT JOIN CM_Cust ct ON ct.CustCode = [it].CustCode AND ct.CorpId = [it].CorpId
                        WHERE [it].CorpId = '{WrGlobal.CorpID}'
                        AND ISNULL(DisplayYn, 'Y') <> 'N'   
                ";

            //조회조건
            if (ItemType != null && ItemType.Length > 0)
            {
                query += $" AND [it].ItemType in (";

                for (int i = 0; i < ItemType.Length; i++)
                {
                    if (i != 0)
                    {
                        query += ", ";
                    }
                    query += $"'{ItemType[i]}'";
                }

                query += ")";
            }
            //else
            //{
            //    query += $"AND [it].ItemType = '{ItemType}'";
            //}

            if (ItemClass1 != "ALL" && ItemClass1 != "" && ItemClass1 != null)
            {
                //품목분류
                query += $" AND [it].ItemClass1 = '{ItemClass1}' ";
            }

            if (CustCode != "")
            {
                query += $" AND [it].CustCode = '{CustCode}' ";
            }
            //정렬
            query += "\nORDER BY [it].ItemCode ASC";

            return db.GetDataTable(query);
        }

        public DataTable SelectList_BOM(string[] ItemType)
        {
            db.Edit_Property(this);

            CustCode = CustCode.ConvertString_Null_To_Empty() == "ALL" ? "" : CustCode.ConvertString_Null_To_Empty();

            //품목관리(Frm_BS_Item) 조회 Query
            query = $@"SELECT '' AS Status,
                              [it].ItemCode,
                              [it].ItemName,
                              [it].ItemType,
                              (select dbo.[FN_Get_CateItem_Title] ('{WrGlobal.CorpID}', 'A010', it.ItemType)) TypeName,

                              [it].SaftyQty,

                              [it].ItemClass1,
                              (select dbo.[FN_Get_CateItem_Title] ('{WrGlobal.CorpID}', 'A012', it.ItemClass1)) ItemClass1Name,
                              [it].ItemClass2,
                              [it].ItemClass3,


                              [it].Alias,
                              [it].Size,
                              [it].Standard,
                              [it].UnitCode,
                              (select dbo.[FN_Get_CateItem_Title] ('{WrGlobal.CorpID}', 'A011', it.UnitCode)) UnitName,

                              [it].Type,
                              [it].UnitWeight,

                              [it].Texture,

                              [it].ColorCode

                              , [it].RoutingCode
                            , CASE WHEN EXISTS (
                                        SELECT 1
                                        FROM CM_BOMRev AS B
                                        WHERE B.CorpId = it.CorpId AND B.ItemCode = it.ItemCode
                                    )
                                    THEN 'Y' -- BOM 마스터에 등록됨
                                    ELSE '' -- BOM 마스터에 없음
                                END AS BomYn

                         FROM CM_Item [it]
                    LEFT JOIN CM_Cust ct ON ct.CustCode = [it].CustCode AND ct.CorpId = [it].CorpId
                        WHERE [it].CorpId = '{WrGlobal.CorpID}'";

            //조회조건
            if (ItemType.Length > 0)
            {
                query += $" AND [it].ItemType in (";

                for (int i = 0; i < ItemType.Length; i++)
                {
                    if (i != 0)
                    {
                        query += ", ";
                    }
                    query += $"'{ItemType[i]}'";
                }

                query += ")";
            }
            //else
            //{
            //    query += $"AND [it].ItemType = '{ItemType}'";
            //}

            if (ItemClass1 != "ALL" && ItemClass1 != "" && ItemClass1 != null)
            {
                //품목분류
                query += $" AND [it].ItemClass1 = '{ItemClass1}' ";
            }

            if (CustCode != "")
            {
                query += $" AND [it].CustCode = '{CustCode}' ";
            }

            //정렬
            query += "\nORDER BY [it].ItemCode ASC";

            return db.GetDataTable(query);
        }

        public DataTable SelectCM_ItemAll_RND(string type, string wareType, string wareHouse)
        {
            query = $@"SELECT 
							'' AS Status, 
							cm.ItemCode, 
							cm.ItemName, 
							cm.ItemType, 
							--CAST(ISNULL(SUM(IIF(st.StockQty > 0, st.StockQty, st.StockQty * -1)), 0) AS INT) AS StockQty, 
                            SUM(ISNULL(st.StockQty, 0)) AS StockQty, 
							'' AS Qty, 
							'' AS Stock_Cd, 
							'' AS FromStock_Cd, 
							'' AS ToStock_Cd, 
							'' AS Remark
						FROM CM_Item cm
						LEFT JOIN StockListView st ON cm.ItemCode = st.ItemCode
                        LEFT JOIN CM_Warehouse CW ON st.WareHouseCode = CW.HouseID
						WHERE 1=1 
                        AND WarehouseCode <> 'S0004'
                        ";

            if (type == "제품") query += $@"AND cm.ItemType IN ('10', '20', '30')";
            else if (type == "자재") query += $@"AND cm.ItemType IN ('40', '50', '60', '70')";

            if (ItemCode != "" && ItemCode != "ALL") query += $@" AND cm.ItemCode = '{ItemCode}'";
            if (wareType != "ALL") query += $"\n AND CW.WareType = '{wareType}'";
            if (wareHouse != "ALL") query += $"\n AND st.WareHouseCode = '{wareHouse}'";

            query += " Group by cm.ItemCode, cm.ItemType,  cm.ItemName";
            query += " ORDER BY cm.ItemType, ItemCode";

            return db.GetDataTable(query);
        }

        public DataTable SelectCM_ItemAll_Lot(string type, bool zeroVisible, string wareType, string wareHouse)
        {
            query = $@"SELECT 
                        	'' AS Status, 
                        	cm.ItemCode, 
                        	cm.ItemName, 
                        	cm.ItemType, 
                        	--CAST(ISNULL(SUM(IIF(st.StockQty > 0, st.StockQty, st.StockQty * -1)), 0) AS INT) AS StockQty, 
                           ISNULL(st.StockQty, 0) AS StockQty, 
                            LotNo,
                            st.WareHouseCode AS Stock_Cd,
                        	NULL AS Qty, 
                        	NULL AS Adj_Qty, 
                        	--'' AS Stock_Cd, 
                        	--'' AS FromStock_Cd, 
                        	--'' AS ToStock_Cd, 
                        	'' AS Remark
                        FROM CM_Item cm
                        LEFT JOIN StockListView_Lot st ON cm.CorpId = st.CorpId AND cm.ItemCode = st.ItemCode AND st.LotNo <> ''
                        LEFT JOIN CM_Warehouse CW ON st.WareHouseCode = CW.HouseID
                        WHERE cm.CorpId = '{WrGlobal.CorpID}'
                        ";

            //if (type == "제품") query += $@"AND cm.ItemType IN ('10', '20', '30')";
            //else if (type == "자재") query += $@"AND cm.ItemType IN ('40', '50', '60', '70')";

            if (!zeroVisible) query += $"\nAND ISNULL(st.StockQty, 0) <> 0 ";

            if (ItemCode != "ALL") query += $@" AND cm.ItemCode = '{ItemCode}'";
            if (wareType != "ALL") query += $"\n AND CW.WareType = '{wareType}'";
            if (wareHouse != "ALL") query += $"\n AND st.WareHouseCode = '{wareHouse}'";


            query += " ORDER BY cm.ItemType, cm.ItemCode";

            return db.GetDataTable(query);
        }

        public DataTable Select_LupProduct()
        {
            query = $@"SELECT'ALL' 코드, '전체' 품명 UNION ALL 
                                SELECT ItemCode 코드, ItemName 품명 
                                 FROM CM_Item
                                WHERE ItemType = '{ItemType}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_Item_HasExamMgnt(string examDivCd)
        {
            ItemType = ItemType.ConvertString_Null_To_Empty();
            ItemClass1 = ItemClass1.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT 
                            it.ItemType
                            , cci.Title AS ItemTypeName
                            , it.ItemCode
                            , it.ItemName
                            , it.Alias
                            , it.Size
                            , it.Standard

                            , it.ItemClass1
                            , it.ItemClass2
                            , it.ItemClass3
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01210', it.ItemClass1)) AS ClassName1	--품목분류1
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01220', it.ItemClass2)) AS ClassName2	--품목분류2

                            , CASE WHEN EXISTS(SELECT TOP 1
                            		                  iem.ItemCode
                                                 FROM QM_ItemExamMgnt iem
                                                WHERE iem.CorpId = it.CorpId AND iem.ExamDivCd = '{examDivCd}' AND iem.ItemCode = it.ItemCode 
                                        ) THEN 'Y'
                                   ELSE 'N'
                              END AS [ExistsExam]
                            , '' AS ChkYN
                        FROM CM_Item it
                        LEFT JOIN CM_CateItem cci ON cci.CorpId = it.CorpId AND cci.CLASS = 'A010' AND it.ItemType = cci.ClassItemCode 
                       WHERE it.CorpId = '{WrGlobal.CorpID}'";


            if (ItemType != "")
            {
                query += $"\nAND it.ItemType = '{ItemType}'";
            }

            if (ItemClass1 != "")
            {
                query += $"\nAND it.ItemClass1 = '{ItemClass1}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Item_List_ByCust(string cust = null, string[] item_Div = null, bool all_Yn = false)
        {
            //거래처별단가에따른 품목정보
            query = "";

            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += "SELECT '전체' AS Cm_Nm\n";

                //단일거래처일경우 거래처도 표기
                if (WrGlobal.ProcOption["BS"]["Item_OneCust"] == "Y")
                {
                    query += ", '전체' AS CustCode\n";
                    query += ", '전체' AS CustName\n";
                }

                query += @"     ,'ALL' AS ItemCode
                                , '전체' AS ItemName
                                , '전체' AS Alias
                                
                             UNION ALL
                            ";

            }

            query += "SELECT cmcd.Cm_Nm AS Cm_Nm\n";

            //단일거래처일경우 거래처도 표기
            if (WrGlobal.ProcOption["BS"]["Item_OneCust"] == "Y")
            {
                query += " , bi.CustCode\n";
                query += " , bc.CustName\n";
            }

            query += $@"     , bi.ItemCode
                             , bi.ItemName
                             , bi.Alias";



            query += $@"  

                          FROM BS_Item bi
                     LEFT JOIN CM_CateItem cmcd ON cmcd.ItemCode = bi.ItemType AND cmcd.Cm_Class = 'A030'
                     LEFT JOIN CM_Cust bc ON bc.CustCode = bi.CustCode AND bi.CorpId = bc.CorpId
                         WHERE bi.CorpId = '{WrGlobal.CorpID}'";

            //특정 거래처항목만 보고싶은 경우
            cust = cust.ConvertStr_Empty_To_Null();


            //특정 구분만 보고 싶은 경우
            if (item_Div != null &&
                item_Div.Length > 0)
            {
                query += "\nAND bi.ItemType IN (";

                for (int idx = 0; idx < item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{item_Div[idx]}'";
                }

                query += ")";
            }

            DataTable dtItem = db.GetDataTable(query);

            //품목 초기화 옵션(Column 사용여부)을 통한 Column 숨김처리
            if (dtItem != null && item_Div != null && item_Div.Length > 0)
            {
                if (item_Div.Contains("10") || item_Div.Contains("20") || item_Div.Contains("30")) { HideCol_ByCdOption(dtItem, "제품코드 초기화"); } //제품초기화 옵션
                else if (item_Div.Contains("40") || item_Div.Contains("50") || item_Div.Contains("60")) { HideCol_ByCdOption(dtItem, "자재코드 초기화"); } //자재초기화 옵션
            }

            return dtItem;
        }

        private void HideCol_ByCdOption(DataTable dtItem, string optionType)
        {
            //초기화에 있는 Column 수정
            foreach (DataColumn col in dtItem.Columns)
            {
                foreach (string fieldNm in WrGlobal.CdOption[optionType].Keys)
                {
                    WrGlobal.CodeOption cdOption = WrGlobal.CdOption[optionType][fieldNm];

                    if (col.ColumnName == fieldNm)
                    {
                        //초기화테이블에 있는 Column
                        col.Caption = fieldNm == "ItemCode" ? "코드" : cdOption.Nm;  //Caption 설정
                        if (cdOption.DispYn == false) { dtItem.Columns[col.ColumnName].ColumnMapping = MappingType.Hidden; }   //Column 숨김
                    }
                }
            }

            //초기화에 없는 Column 삭제
            foreach (DataColumn col in dtItem.Columns)
            {
                bool exists = false;

                foreach (string fieldNm in WrGlobal.CdOption[optionType].Keys)
                {
                    if (col.ColumnName == fieldNm)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    col.ColumnMapping = MappingType.Hidden;
                }
            }
        }

        public DataRow Select_Item_RowInfo()
        {
            if (ItemCode == "" && ItemName == "" && Alias == "") return null;

            query = $@"SELECT TOP 1
	                          bi.ItemCode
	                        , bi.ItemName
	                        , bi.Alias
                            , bi.ItemClass1
                            , bi.ItemClass2
                            , bi.ItemClass3
                            , bi.Size
                            , bi.UnitWeight
                            , bi.Texture
                            , bi.Type
                            , bi.UnitCode
                            , bi.ColorCode
	                        , bi.Standard
	                        , bi.ItemType
	                        , bi.CustCode
	                        , bi.Revision
	                        , bi.PackQty
                            , cmcd.Title AS ItemTypeName

                         FROM CM_Item bi
                    LEFT JOIN CM_CateItem cmcd ON cmcd.ClassItemCode = bi.ItemType
                                          AND cmcd.Class = 'A010' 
                        WHERE bi.CorpID = '{WrGlobal.CorpID}'";

            if (ItemCode != "" && ItemCode != null) { query += $" AND bi.ItemCode = '{ItemCode}'"; }
            if (ItemName != "" && ItemName != null) { query += $" AND bi.ItemName = '{ItemName}'"; }
            if (Alias != "" && Alias != null) { query += $" AND bi.Alias   = '{Alias}'"; }

            return db.GetDataRow(query);
        }

        public void Insert_Update_CM_Item()
        {
            if (ItemName.Contains("'"))
            {
                ItemName = ItemName.Replace("'", "''");
            }
            query = $@"IF EXISTS(
                                SELECT  ItemCode
                                FROM CM_Item
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                      AND ItemCode = '{ItemCode}'
                                )
                                UPDATE CM_Item
                                       SET 
                                           ItemName = '{ItemName}',
                                           ItemType = '{ItemType}',
                                           Standard = '{Standard}',
                                           UnitWeight = '{UnitWeight}',
                                           Size = '{Size}',
                                           UnitCode = '{UnitCode}',
                                           SaftyQty = '{SaftyQty}',
                                           Remark = '{Remark}'
                                       WHERE CorpId = '{WrGlobal.CorpID}'
                                       AND ItemCode = '{ItemCode}' 
                            
                            ELSE
                            INSERT INTO CM_Item(
                                        CorpId, 
                                        ItemType, 
                                        ItemCode, 
                                        ItemName, 
                                        Standard,
                                        UnitWeight, 
                                        Size, 
                                        UnitCode, 
                                        SaftyQty, 
                                        Remark
                                    )
                                    VALUES(
                                        '{WrGlobal.CorpID}',
                                        '{ItemType}',
                                        '{ItemCode}',
                                        '{ItemName}',
                                        '{Standard}',
                                        '{UnitWeight}',
                                        '{Size}',
                                        '{UnitCode}',
                                        '{SaftyQty}',
                                        '{Remark}'
                                     )";

            db.ExecuteQuery(query);
        }

        // 기존 호성 엑셀업로드와 맞지않아서 광주요로 다시 만듬
        public void Insert_Update_CM_Item_Item_Excel()
        {
            if (ItemName.Contains("'"))
            {
                ItemName = ItemName.Replace("'", "''");
            }
            query = $@"IF EXISTS(
                                SELECT  ItemCode
                                FROM CM_Item
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                      AND ItemCode = '{ItemCode}'
                                )
                                UPDATE CM_Item
                                       SET 
                                           ItemType = '{ItemType}',
                                           ItemName = '{ItemName}',
                                           ItemClass1 = '{ItemClass1}',
                                           ItemClass2 = '{ItemClass2}',
                                           ItemClass3 = '{ItemClass3}',
                                           RoutingCode = '{RoutingCode}', 
                                           UnitCode = '{UnitCode}',
                                           Texture = '{Texture}',
                                           SaftyQty = '{SaftyQty}',
                                           Remark = '{Remark}',
                                           ModId = '{WrGlobal.LoginID}',
                                           ModIP = '{WrGlobal.HostName}',
                                           ModDt =  GETDATE()
                                       WHERE CorpId = '{WrGlobal.CorpID}'
                                       AND ItemCode = '{ItemCode}' 
                            
                            ELSE
                            INSERT INTO CM_Item(
                                        CorpId, 
                                        ItemType, 
                                        ItemCode, 
                                        ItemName, 
                                        ItemClass1,
                                        ItemClass2,
                                        ItemClass3,
                                        RoutingCode, 
                                        --Standard,
                                        --UnitWeight, 
                                        --Size, 
                                        UnitCode, 
                                        Texture,
                                        SaftyQty, 
                                        Remark,
                                        CreId, 
                                        CreIP, 
                                        CreDt 
                                    )
                                    VALUES(
                                        '{WrGlobal.CorpID}',
                                        '{ItemType}',
                                        '{ItemCode}',
                                        '{ItemName}',
                                        '{ItemClass1}',
                                        '{ItemClass2}',
                                        '{ItemClass3}',
                                        '{RoutingCode}', 
                                        --'{Standard}',
                                        --'{UnitWeight}',
                                        --'{Size}',
                                        '{UnitCode}',
                                        '{Texture}',
                                        '{SaftyQty}',
                                        '{Remark}',
                                        '{WrGlobal.LoginID}',
                                        '{WrGlobal.HostName}',
                                         GETDATE()
                                     )";

            db.ExecuteQuery(query);
        }

        public DataTable Select_Material_List(string[] GroupType = null, bool all_Yn = false)
        {
            query = "";

            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT '전체' AS 구분,'ALL' AS 코드, '전체' AS 품명, '전체' AS 품번
                        UNION ALL ";
            }

            query += $@"SELECT  cm.ItemType AS 구분, cm.ItemCode AS 코드, cm.ItemName AS 품명, cm.Alias AS 품번
                          FROM CM_Item cm
                         WHERE cm.CorpId = '{WrGlobal.CorpID}'";

            //특정 구분만 보고 싶은 경우
            if (GroupType != null &&
                GroupType.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < GroupType.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" cm.ItemType = '{GroupType[idx]}'";
                }

                query += ")";
            }
            //특정 거래처만 보고싶을 경우
            if (CustCode != null
                && CustCode.ToUpper() != "ALL")
            {
                query += $" AND cm.CustCode = '{CustCode}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_MaterialList_All(bool allYn = false, string[] type = null)
        {
            if (allYn)
            {
                query = $@"SELECT'ALL' 품목코드, '전체' 품명
                            UNION ALL
                            ";
            }

            query += $@"SELECT ItemCode 품목코드, ItemName 품명
                        FROM CM_Item 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                    ";

            if (type != null)
            {
                query += $"\n AND ItemType IN ('{string.Join("','", type)}')";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_PartList_BaseItem(string[] item_Div = null)
        {
            //PartList 기준품목 조회 Query
            query = $@"
                        SELECT  
                                ROW_NUMBER() OVER (ORDER BY IT.ItemCode, IT.ItemName) DispSeq,
                                IT.ItemType,
                                IT.ItemCode,
                                IT.ItemName,
                                IT.Alias,
                                IT.Standard,
                                IT.UnitCode,
                                CASE WHEN EXISTS( SELECT Parent_ItemCode
                                                    FROM BS_PartList
                                                   WHERE Parent_ItemCode = ItemCode)
                                     THEN 'Y'
                                     ELSE 'N'
                                     END PartList_Yn
                                                       
                        FROM  CM_Item IT
                        WHERE  IT.CorpId = '{WrGlobal.CorpID}'
                          AND (   IT.BOM_Div_Cd = '10'
                               OR IT.BOM_Div_Cd = '30')
          
                        ";

            //특정 구분만 보고 싶은 경우
            if (item_Div != null &&
                item_Div.Length > 0)
            {
                query += "\nAND IT.ItemType IN (";

                for (int idx = 0; idx < item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{item_Div[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public bool Insert_CM_Item()
        {
            string purchasingColumns = purchasingParameters == null ? "" :
                ", PurchaseUnitCode, PurchaseConversionQty, DeliveryOverTolerancePct, HSCode, HSName, HSSpec, HSUnit";
            string purchasingValues = purchasingParameters == null ? "" :
                ", @PurchaseUnitCode, @PurchaseConversionQty, @DeliveryOverTolerancePct, @HSCode, @HSName, @HSSpec, @HSUnit";
            // 테이블 정리 필요해서 추후 수정 예정
            query = $@"INSERT INTO CM_Item(
                        CorpId
                        , ItemCode
                        , ItemName
                        , CustCode
                        , ItemType
                        , ItemClass1
                        , ItemClass2
                        , ItemClass3
                        , Size
                        , Alias
                        , Standard
                        , StartDate
                        , EndDate
                        , DisplayYn
                        , ExamYn
                        , UnitCode
                        , SaftyQty
                        , UnitWeight
                        , Remark
                        , Type
                        , UseYn
                        , PackUnit
                        , PackQty
                        , Texture
                        , Abbreviation
                        , EnglishAbbreviation
                        , ItemGroup
                        , SupplyType
                        , MaterialT
                        , Width
                        , Height
                        , NetWeight
                        , YogurtG
                        , YogurtKg
                        , PTLoadQty
                        , BarCode
                        , OldItemCode

                        , RoutingCode
                        , HouseID
                        , CreId
                        , CreIP
                        , CreDt
                        {purchasingColumns}
                        )
                        VALUES
                        (
                          '{WrGlobal.CorpID}'
                        ,IIF('{ItemCode}' = '', (dbo.[FN_Create_Cd_Material]('{CorpId}', '{ItemType}')), '{ItemCode}')
                        , '{ItemName}'
                        , '{CustCode}'
                        , '{ItemType}'

                        , {ItemClass1.ConvertStr_Empty_To_Null()}
                        , {ItemClass2.ConvertStr_Empty_To_Null()}
                        , {ItemClass3.ConvertStr_Empty_To_Null()}

                        , {Size.ConvertStr_Empty_To_Null()}
                        , {Alias.ConvertStr_Empty_To_Null()}
                        , {Standard.ConvertStr_Empty_To_Null()}

                        , {StartDate.ConvertStr_Empty_To_Null()}
                        , {EndDate.ConvertStr_Empty_To_Null()}

                        , '{DisplayYn}'
                        , '{ExamYn}'

                        , {UnitCode.ConvertStr_Empty_To_Null()}
                        , {SaftyQty.ConvertFloat_Empty_To_Null()}
                        , {UnitWeight.ConvertFloat_Empty_To_Null()}
                        , {Remark.ConvertStr_Empty_To_Null()}
                        , {Type.ConvertStr_Empty_To_Null()}

                        , '{UseYn}'

                        , {PackUnit.ConvertStr_Empty_To_Null()}
                        , '{PackQty}'
                        , {Texture.ConvertStr_Empty_To_Null()}
                        , {Abbreviation.ConvertStr_Empty_To_Null()}
                        , {EnglishAbbreviation.ConvertStr_Empty_To_Null()}
                        , {ItemGroup.ConvertStr_Empty_To_Null()}
                        , {SupplyType.ConvertStr_Empty_To_Null()}
                        , {MaterialT.ConvertStr_Empty_To_Null()}
                        , {Width.ConvertStr_Empty_To_Null()}
                        , {Height.ConvertStr_Empty_To_Null()}
                        , {NetWeight.ConvertDecimal_Null_To_Zero()}
                        , {YogurtG.ConvertDecimal_Null_To_Zero()}
                        , {YogurtKg.ConvertDecimal_Null_To_Zero()}
                        , {PTLoadQty.ConvertInt_Null_To_Zero()}
                        , {BarCode.ConvertStr_Empty_To_Null()}
                        , {OldItemCode.ConvertStr_Empty_To_Null()}

                        , {RoutingCode.ConvertStr_Empty_To_Null()}
                        , {HouseID.ConvertStr_Empty_To_Null()}

                        ,'{WrGlobal.LoginID}'
                        ,'{WrGlobal.HostName}'
                        , GETDATE()
                        {purchasingValues}
                   
                    )";

            return ExecuteItemWrite();
        }

        public bool Update_CM_Item()
        {
            string purchasingUpdate = purchasingParameters == null ? "" : @",
                        PurchaseUnitCode = @PurchaseUnitCode,
                        PurchaseConversionQty = @PurchaseConversionQty,
                        DeliveryOverTolerancePct = @DeliveryOverTolerancePct,
                        HSCode = @HSCode, HSName = @HSName, HSSpec = @HSSpec, HSUnit = @HSUnit";
            // 테이블 정리 필요해서 추후 수정 예정
            query = $@"
                        UPDATE CM_Item
                        SET
                         ItemName = '{ItemName}'
                        , CustCode = '{CustCode}'
                        , ItemType = '{ItemType}'
                        , ItemClass1 = {ItemClass1.ConvertStr_Empty_To_Null()}
                        , ItemClass2 = {ItemClass2.ConvertStr_Empty_To_Null()}
                        , ItemClass3 = {ItemClass3.ConvertStr_Empty_To_Null()}
                        , Size = {Size.ConvertStr_Empty_To_Null()}
                        , Alias = {Alias.ConvertStr_Empty_To_Null()}
                        , Standard = {Standard.ConvertStr_Empty_To_Null()}
                        , StartDate = {StartDate.ConvertStr_Empty_To_Null()}
                        , EndDate = {EndDate.ConvertStr_Empty_To_Null()}
                        , DisplayYn = '{DisplayYn}'
                        , ExamYn = '{ExamYn}'
                        , UnitCode = {UnitCode.ConvertStr_Empty_To_Null()}
                        , SaftyQty = {SaftyQty.ConvertFloat_Empty_To_Null()}
                        , UnitWeight = {UnitWeight.ConvertFloat_Empty_To_Null()}
                        , Remark = {Remark.ConvertStr_Empty_To_Null()}
                        , Type = {Type.ConvertStr_Empty_To_Null()}
                        , UseYn = '{UseYn}'
                        , Texture = '{Texture}'
                        , ModId ='{WrGlobal.LoginID}'
                        , ModIP ='{WrGlobal.HostName}'
                        , ModDt = GETDATE()
                        , PackUnit = {PackUnit.ConvertStr_Empty_To_Null()}
                        , PackQty = {PackQty.ConvertFloat_Empty_To_Null()}
                        , Abbreviation = {Abbreviation.ConvertStr_Empty_To_Null()}
                        , EnglishAbbreviation = {EnglishAbbreviation.ConvertStr_Empty_To_Null()}
                        , ItemGroup = {ItemGroup.ConvertStr_Empty_To_Null()}
                        , SupplyType = {SupplyType.ConvertStr_Empty_To_Null()}
                        , MaterialT = {MaterialT.ConvertStr_Empty_To_Null()}
                        , Width = {Width.ConvertStr_Empty_To_Null()}
                        , Height = {Height.ConvertStr_Empty_To_Null()}
                        , NetWeight = {NetWeight.ConvertDecimal_Null_To_Zero()}
                        , YogurtG = {YogurtG.ConvertDecimal_Null_To_Zero()}
                        , YogurtKg = {YogurtKg.ConvertDecimal_Null_To_Zero()}
                        , PTLoadQty = {PTLoadQty.ConvertInt_Null_To_Zero()}
                        , BarCode = {BarCode.ConvertStr_Empty_To_Null()}
                        , OldItemCode = {OldItemCode.ConvertStr_Empty_To_Null()}
                        , RoutingCode = {RoutingCode.ConvertStr_Empty_To_Null()}
                        , HouseID = {HouseID.ConvertStr_Empty_To_Null()}
                        {purchasingUpdate}
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ItemCode = '{ItemCode}'

                        ";

            return ExecuteItemWrite();
        }

        public bool Delete_CM_Item()
        {
            query = $@" DELETE FROM CM_Item
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ItemCode = '{ItemCode}' ";

            return db.ExecuteQuery(query);
        }

        public bool UpdateRevisionRoutingCode()
		{
            //품목 Revision, RoutingCode UPDATE
            query = $@"
                    UPDATE CM_Item
                    SET Revision = '{Revision}'
                    , RoutingCode = '{RoutingCode}'
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND ItemCode = '{ItemCode}'
                    ";

            return db.ExecuteQuery(query);

        }

        // ItemCode로 UnitCode 조회
        public string GetUnitCode(string itemCode)
        {
            itemCode = itemCode.ConvertString_Null_To_Empty();

            query = $@"
                SELECT ISNULL(UnitCode, '') AS UnitCode
                FROM Cm_Item
                WHERE ItemCode = '{itemCode}'
            ";
            return db.GetDataScalar(query).ToString();
        }

    }
}
