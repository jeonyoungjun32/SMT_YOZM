
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.CM
{
    public class CM_Item
    {
        private SqlConnection adoCon = new SqlConnection();
        #region Property

        public string Corpid { get; set; }
        public string MaterialCode { get; set; }
        public string ItemCode { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string MaterialName { get; set; }
        public string ItemName { get; set; }
        public string MaterialType { get; set; }
        public string ItemType { get; set; }
        public string Size { get; set; }
        public string SizeType { get; set; }
        public string Alias { get; set; }
        public float UnitStandard { get; set; }
        public float RollWidth { get; set; }
        public float RollHeight { get; set; }
        public float PouchWidth { get; set; }
        public float PouchHeight { get; set; }
        public float PouchBase { get; set; }
        public float FabricWidth1 { get; set; }
        public float FabricWidth2 { get; set; }
        public float FabricWidth3 { get; set; }
        public float FabricWidth4 { get; set; }
        public float FabricWidth5 { get; set; }
        public int UnitHeight { get; set; }
        public int UnitPrice { get; set; }
        public int ColorCnt { get; set; }
        public string Texture1 { get; set; }
        public string Texture2 { get; set; }
        public string Texture3 { get; set; }
        public string Texture4 { get; set; }
        public string Texture5 { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Color4 { get; set; }
        public string Color5 { get; set; }
        public string Color6 { get; set; }
        public string Color7 { get; set; }
        public string Color8 { get; set; }
        public string Color9 { get; set; }
        public string Color10 { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SetYn { get; set; }
        public string DisplayYn { get; set; }
        public string PurUnitPrice { get; set; }
        public string SaleUnitPrice { get; set; }
        public string ExamYn { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string UnitWeight { get; set; }
        public string SaftyQty { get; set; }
        public string NormalQty { get; set; }
        public string OverQty { get; set; }
        public string Remark { get; set; }
        public string CarType { get; set; }
        public string Voltage { get; set; }
        public string HotWaterYN { get; set; }
        public string AirYN { get; set; }
        public string Spec { get; set; }
        public string Texture { get; set; }
        public string CustCode { get; set; }
        public string Thickness { get; set; }
        public string Thick_Min { get; set; }
        public string Thick_Max { get; set; }
        public string CustName { get; set; }
        public string PaintingTime { get; set; }
        public string SecureMaterial { get; set; }
        public string RackQty { get; set; }
        public string CreID { get; set; }
        public string CreIP { get; set; }
        public string CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public string ModDt { get; set; }

        public string StandardDocNum { get; set; }

        public string RingYn { get; set; }

        public string BakingYn { get; set; }

        public string CoatingYn { get; set; }

        public string ProdLine { get; set; }

        public string SurfaceArea { get; set; }

        public string ElecVolt { get; set; }

        public string ChargeMin { get; set; }

        public string ChargeMax { get; set; }

        public string IDNum { get; set; }

        public string Remark1 { get; set; }

        public string Remark2 { get; set; }

        public string Customer { get; set; }

        public string Charge { get; set; }

        public string CarName { get; set; }

        public string Thick { get; set; }
        public string StoNo { get; set; }
        public byte[] Work_Standard_Sheet { get; set; }

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

        public DataRow Select_ItemImage()
        {
            query = $@"SELECT TOP 1 PicPdf AS mDesign, Work_Standard_Sheet, Work_Standard_Sheet ItemImg
                FROM dbo.CM_Item cm
                LEFT JOIN dbo.CM_ItemDesign cmd ON cm.MaterialCode = cmd.MaterialCode
                WHERE cm.CorpId = '{WrGlobal.CorpID}' AND cmd.materialCode = '{MaterialCode}'
                Order by cmd.CreDt DESC";

            return db.GetDataRow(query);
        }

        public DataRow Select_CmdReport()
        {
            query = $@"
                    SELECT Pic FROM CmOrderWorkStandards WHERE CorpId = '{WrGlobal.CorpID}'  AND MaterialCode = '{MaterialCode}'
                ";

            return db.GetDataRow(query);
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
            if (ItemType != "" && ItemType != null) { query += $" AND bi.ItemType   = '{ItemType}'"; }

            return db.GetDataRow(query);
        }

        public DataTable Select_MaterialList()
        {
            query = $@"SELECT ct.Title AS [차종]
                            , cm.MaterialCode AS [품목코드]
                            , cm.Alias AS [품번]
                            , cm.MaterialName AS [품명]
                            , (SELECT TOP 1
									  UnitPrice
								 FROM CM_ItemPrice mp
								WHERE mp.MaterialCode = cm.MaterialCode
						     ORDER BY mp.StartDate DESC) AS [단가]
                         FROM CM_Item cm
                    LEFT JOIN CmCarType ct ON ct.CarID = CM.CarType
                        WHERE cm.CustCode = '{CustCode}'";

            return db.GetDataTable(query);
        }


        public DataTable SelectCM_ItemWithCartype(string corpId, string materialType)
        {
            query = $@" SELECT 
                            Mat.*
                            , ci.ItemClassName   
                            , cc.CustCode
                            , cc.CustName
                            , cii.Title SizeTypeName
 							, CASE WHEN EXISTS(SELECT TOP 1
                        			                  iem.Item_Cd
                        	                     FROM BS_ItemExam_Mgnt iem
                        	                    WHERE iem.Item_Cd = Mat.MaterialCode
                                        ) THEN 'Y'
                        	       ELSE 'N'
                        	  END AS [ExistsExam]                           
                        FROM [CM_Item] Mat  
                        LEFT JOIN CmItemClass ci on (Mat.Alias = ci.ItemClassCode)
                        LEFT JOIN CM_Cust cc ON Mat.CustCode = cc.CustCode
                        LEFT JOIN CmCateItem cii on Mat.CorpId = cii.CorpId and cii.CLASS='T010' and Mat.SizeType = cii.ItemCode
                        WHERE MaterialType = CASE WHEN '{materialType}' = 'ALL' THEN MaterialType
                                             ELSE '{materialType}' END ";

            if(Alias != "" && MaterialName != "")
			{
                query += $@" AND (   MaterialName LIKE CONCAT('%','{MaterialName}', '%')
                          	             OR Alias LIKE CONCAT('%','{Alias}',   '%')
                          	     )";
            }
            DataTable dtCM_Item = db.GetDataTable(query);

            return dtCM_Item;
        }

        public DataTable SelectCM_ItemAll(string type)
        {
            query = $@"SELECT '' AS Status, *, '' AS Qty, '' AS Stock_Cd, '' AS FromStock_Cd, '' AS ToStock_Cd 
                                  FROM CM_Item
                                 WHERE 1=1 ";

            if (type == "제품") query += $@"AND MaterialType IN ('10', '20', '30')"; 
            else if (type == "자재") query += $@"AND MaterialType IN ('40', '50', '60', '70')";

            if (MaterialCode != "ALL") query += $@" AND MaterialCode = '{MaterialCode}'";

            query += " ORDER BY MaterialType, MaterialCode";

            return db.GetDataTable(query);             
        }


        public DataTable Select_LupProduct()
        {
            query = $@"SELECT 'ALL' 코드, '전체' 품명 UNION ALL 
                                SELECT MaterialCode 코드, MaterialName 품명 
                                 FROM CM_Item
                                WHERE MaterialType = '{MaterialType}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_ProductAdj()
        {
            query = $@"SELECT *, '' AS Qty, '' AS Stock_Cd FROM CM_Item 
                                WHERE MaterialType IN ('10', '20', '30')";

            return db.GetDataTable(query);
        }

        public void Insert_Update_CM_Item()
        {
            if (MaterialName.Contains("'"))
            {
                MaterialName = MaterialName.Replace("'", "''");
            }
            query = $@"IF EXISTS(
                                                SELECT  MaterialCode
                                                  FROM CM_Item
                                                WHERE CorpId = '{WrGlobal.CorpID}'
                                                    AND MaterialCode = '{MaterialCode}'
                                                )
                                UPDATE CM_Item
                                       SET 
                                       MaterialName = '{MaterialName}',
                                       CustCode = '{CustCode}',
                                       MaterialType = '{MaterialType}',
                                       SizeType = '{SizeType}',
                                       UnitStandard = '{UnitStandard}',
                                       RollWidth = '{RollWidth.ConvertFloat_Null_To_Zero()}',
                                       RollHeight = '{RollHeight.ConvertFloat_Null_To_Zero()}',
                                       PouchWidth = '{PouchWidth.ConvertFloat_Null_To_Zero()}',
                                       PouchHeight = '{PouchHeight.ConvertFloat_Null_To_Zero()}',
                                       PouchBase = '{PouchBase.ConvertFloat_Null_To_Zero()}',
                                       UnitHeight = '{UnitHeight.ConvertFloat_Null_To_Zero()}',
                                       FabricWidth1 = '{FabricWidth1.ConvertFloat_Null_To_Zero()}',
                                       FabricWidth2 = '{FabricWidth2.ConvertFloat_Null_To_Zero()}',
                                       FabricWidth3 = '{FabricWidth3.ConvertFloat_Null_To_Zero()}',
                                       FabricWidth4 = '{FabricWidth4.ConvertFloat_Null_To_Zero()}',
                                       FabricWidth5 = '{FabricWidth5.ConvertFloat_Null_To_Zero()}',
                                       ColorCnt = '{ColorCnt.ConvertInt_Null_To_Zero()}',
                                       Texture1 = '{Texture1}',
                                       Texture2 = '{Texture2}',
                                       Texture3 = '{Texture3}',
                                       Texture4 = '{Texture4}',
                                       Texture5 = '{Texture5}',
                                       Color1 = '{Color1}',
                                       Color2 = '{Color2}',
                                       Color3 = '{Color3}',
                                       Color4 = '{Color4}',
                                       Color5 = '{Color5}',
                                       Color6 = '{Color6}',
                                       Color7 = '{Color7}',
                                       Color8 = '{Color8}',
                                       Color9 = '{Color9}',
                                       Color10 = '{Color10}',
                                       UnitCode = '{UnitCode}',
                                       UnitPrice = '{UnitPrice}'
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND MaterialCode = '{MaterialCode}' 
                            
                            ELSE
                            INSERT INTO CM_Item(CorpId, MaterialCode, MaterialName, CustCode, MaterialType, SizeType, UnitStandard, RollWidth, RollHeight, 
                                                                          PouchWidth, PouchHeight, PouchBase, UnitHeight ,FabricWidth1,FabricWidth2,FabricWidth3,FabricWidth4,FabricWidth5,ColorCnt,
                                                                          Texture1, Texture2, Texture3, Texture4, Texture5,
                                                                          Color1 ,Color2 ,Color3 ,Color4 ,Color5 ,Color6 ,Color7 ,Color8 ,Color9 ,Color10 ,UnitCode ,UnitPrice)
                                    VALUES(
                                    '{WrGlobal.CorpID}',
                                    '{MaterialCode}',
                                    '{MaterialName}',
                                    '{CustCode}',
                                    '{MaterialType}',
                                    '{SizeType}',
                                    '{UnitStandard}',
                                    '{RollWidth}',
                                    '{RollHeight}',
                                    '{PouchWidth}',
                                    '{PouchHeight}',
                                    '{PouchBase}',
                                    '{UnitHeight}',
                                    '{FabricWidth1}',
                                    '{FabricWidth2}',
                                    '{FabricWidth3}',
                                    '{FabricWidth4}',
                                    '{FabricWidth5}',
                                    '{ColorCnt}',
                                    '{Texture1}',
                                    '{Texture2}',
                                    '{Texture3}',
                                    '{Texture4}',
                                    '{Texture5}',
                                    '{Color1}',
                                    '{Color2}',
                                    '{Color3}',
                                    '{Color4}',
                                    '{Color5}',
                                    '{Color6}',
                                    '{Color7}',
                                    '{Color8}',
                                    '{Color9}',
                                    '{Color10}',
                                    '{UnitCode}',
                                    '{UnitPrice}')";

            db.ExecuteQuery(query);
        }

        public bool Upd_Item_Work_Standard_Sheet()
        {
            query = $@" UPDATE CM_Item 
                           SET Work_Standard_Sheet = @Pic

                         WHERE CorpId = '{WrGlobal.CorpID}' 
                           AND MaterialCode = {MaterialCode.ConvertStr_Empty_To_Null()}";

            return db.ExecuteQueryImage(query, Work_Standard_Sheet);
        }

        public DataTable Select_Material_List(string CustCode = null, string[] GroupType = null, bool all_Yn = false)
        {
            query = "";

            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT '전체' AS 차종, '전체' AS 구분, 'All' AS 코드, '전체' AS 품명, '전체' AS 품번,
                                  '전체' AS 적재단위, 0 AS 적재단위수량
                        UNION ALL ";
            }

            query += $@"SELECT ct.Title AS 차종, cm.GroupID AS 구분, cm.MaterialCode AS 코드, cm.MaterialName AS 품명, cm.Alias AS 품번,
                               cm.PackUnit AS 적재단위, cm.PackQty AS 적재단위수량
                          FROM CM_Item cm
                     LEFT JOIN CmCarType ct ON ct.CarID = cm.CarType
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

                    query += $@" cm.GroupID = '{GroupType[idx]}'";
                }

                query += ")";
            }
            //특정 거래처만 보고싶을 경우
            if(CustCode != null
                && CustCode != "All")
            {
                query += $" AND cm.CustCode = '{CustCode}'";
            }

            return db.GetDataTable(query);
        }


        public DataTable Auto_Num_MaterialCode()
        {
            string sql = $@"SELECT dbo.FN_Create_Cd_Material('{WrGlobal.CorpID}', '{MaterialType}')";

            DataTable dtCM_Item = db.GetDataTable(sql);

            return dtCM_Item;
        }

        public DataTable SelectCM_ItemUnitPrice(string UnitPriceCode, string Date)
        {
            // 10 : 매입, 20 : 매출

            string query = $@"
                    SELECT TOP 1 ISNULL(UnitPrice, 0) AS UnitPrice
                    FROM CM_ItemPrice
                    WHERE MaterialCode = '{MaterialCode}'
                    AND UnitPriceCode = '{UnitPriceCode}' -- 10 : 매입, 20 : 매출
                    AND StartDate <= '{Date}'
                    ORDER BY StartDate DESC
                    ";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public string SelectNewMatCode_J()
        {
            query = $@"
                        DECLARE @Initial VARCHAR(2)
                        , @MaxCode  VARCHAR(20)
                        , @NewCode  VARCHAR(20)
                        
                        
                        --거래처 이니셜 가져옴
                        SELECT @Initial = Initial 
                        FROM CM_Cust 
                        WHERE CustCode = '{CustCode}'
                        
                        --이니셜 없을경우 MC로 품목 생성
                        IF @Initial IS NULL OR @Initial = ''
                        BEGIN
                        	SET @Initial = 'MC'
                        END
                        
                        --최대코드 가져옴
                        SELECT @MaxCode = MAX(MaterialCode)
                        FROM CM_Item
                        WHERE MaterialCode LIKE @INITIAL + '%'
                        
                        IF @MaxCode IS NULL
                        BEGIN
                        	--없으면 생성
                        	SET @NewCode = @Initial + '001'
                        END
                        ELSE 
                        BEGIN
                        	DECLARE @LEN INT = LEN(REPLACE(@MAXCODE, @INITIAL, ''))
                        
                        	--있으면 SEQ++
                        	SET @NewCode = 	@Initial + FORMAT(RIGHT(@MaxCode, @LEN) + 1 , CONCAT('D', @LEN))
                        END
                        
                        SELECT @NewCode
                    ";

            DataRowView drv = db.GetDataRecord(query);

            return drv[0].ConvertString_Null_To_Empty();
        }

        public DataTable Select_MaterialList_All(bool allYn = false, string[] type = null)
        {
            if (allYn)
            {
                query = $@"SELECT 'All' 품목코드, '전체' 품명
                            UNION ALL
                            ";
            }

            query += $@"SELECT MaterialCode 품목코드, MaterialName 품명
                        FROM CM_Item 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                    ";

            if (type != null)
            {
                query += $"\n AND MaterialType IN ('{string.Join("','", type)}')";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Material_ZStart()
        {
            query = $@"SELECT MaterialCode 품목코드, MaterialName 품명
                        FROM CM_Item 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MaterialCode LIKE 'Z%' 
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_WareHouse_Lookup()
        {
            query = $@"SELECT HouseID AS 분류코드, Title as 분류명 FROM CM_Warehouse WHERE CorpId = '{WrGlobal.CorpID}' ";

            return db.GetDataTable(query);
        }
    }
}
