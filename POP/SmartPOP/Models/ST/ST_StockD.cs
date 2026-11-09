using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB.Helpers;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.ST
{
    class ST_StockD
    {
        public string CorpId { get; set; }

        public string InOutNo { get; set; }

        public int Seq { get; set; }

        public string CustCode { get; set; }

        public string InOutDate { get; set; }

        public string Type { get; set; }

        public string MaterialCode { get; set; }

        public string LotNo { get; set; }

        public string FromHouseCode { get; set; }

        public string ToHouseCode { get; set; }

        public string JobNo { get; set; }

        public int? JobSeq { get; set; }

        public int? JobSeq2 { get; set; }

        public string FaultCode { get; set; }

        public decimal? Qty { get; set; }

        public decimal? OutQty { get; set; }

        public string WriteID { get; set; }

        public string CloseYn { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        public string Status { get; set; }

        public string MaterialType { get; set; }

        public string HouseCode { get; set; }

        public string MmOrderNo { get; set; }
        public int MmOrderSeq { get; set; }

        Database db = new Database();
        string query = "";

        public bool Ins_ST_StockD()
        {
            query = $@"INSERT INTO ST_StockD
                            (
                              CorpId
                            , InOutNo
                            , Seq
                            , CustCode
                            , InOutDate
                            , Type
                            , ItemCode
                            , LotNo

                            , FromHouseCode
                            , ToHouseCode
                            , JobNo
                            , JobSeq
                            , JobSeq2

                            , FaultCode
                            , Qty
                            , OutQty
                            , WriteID
                            , CloseYn
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , Status
                            )
                                    VALUES(
                                   '{WrGlobal.CorpID}',
                                   '{InOutNo}',
                                   '{Seq}',
                                   '{CustCode}',
                                   '{InOutDate}',

                                   '{Type}',
                                   '{MaterialCode}',
                                   '{LotNo}',
                                   '{FromHouseCode}',
                                   '{ToHouseCode}',
                                   '{JobNo}',
                                   '{JobSeq}',
                                   '{JobSeq2}',

                                   '{FaultCode}',
                                   '{Qty}',
                                   '{OutQty.ConvertInt_Null_To_Zero()}',
                                   '{WriteID}',
                                   '{CloseYn}',
                                   '{Remark}',
                                   '{WrGlobal.LoginID}',
                                   '{WrGlobal.ClientHostName}',
                                    GETDATE(),
                                   '{Status}')";

            return db.ExecuteQuery(query);
        }

        public bool Ins_ST_StockD_ByStoNo()
        {
            query = $@"INSERT INTO ST_StockD 
                        (
                            CorpId
                            , InOutNo
                            , Seq
                            , CustCode
                            , InOutDate
                            , Type
                            , MaterialCode
                            , LotNo
                            , FromHouseCode
                            , ToHouseCode
                            , JobNo
                            , JobSeq
                            , JobSeq2
                            , FaultCode
                            , Qty
                            , OutQty
                            , WriteID
                            , CloseYn
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                            , Status
                        )
                        SELECT PD.CorpId
                                , '{InOutNo}'
                                , PD.StoSeq
                                , PM.CustCode
                                , GETDATE()
                                , 'I'
                                , PD.MaterialCode
                                , null
                                , null
                                , '{ToHouseCode}'
                                , PD.StoNo
                                , PD.StoSeq
                                , null
                                , null
                                , PD.Qty
                                , null
                                , '{WriteID}'
                                , 'N'
                                , null
                                , '{WrGlobal.LoginID}'
                                , HOST_NAME()
                                , GETDATE()
                                , null
                                , null
                                , null
                                , null
                        FROM MM_PurchaseD PD
                        INNER JOIN PurchaseMaster PM ON PD.StoNo = PM.StoNo
                        WHERE PD.CorpId = '{WrGlobal.CorpID}'
                        AND PD.StoNo = '{JobNo}'
                        ";

            return db.ExecuteQuery(query);
        }

        public bool Ins_ST_StockD_ByOSNo()
        {
            query = $@"INSERT INTO ST_StockD
                        (
                            CorpId
                            , InOutNo
                            , Seq
                            , CustCode
                            , InOutDate
                            , Type
                            , MaterialCode
                            , LotNo
                            , FromHouseCode
                            , ToHouseCode
                            , JobNo
                            , JobSeq
                            , JobSeq2
                            , FaultCode
                            , Qty
                            , OutQty
                            , WriteID
                            , CloseYn
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , Status
                        )
                        SELECT OD.CorpId
                                , '{InOutNo}'
                                , 1
                                , OM.CustCode
                                , GETDATE()
                                , 'I'
                                , OD.MaterialCode
                                , null
                                , {FromHouseCode.ConvertStr_Empty_To_Null()}
                                , {ToHouseCode.ConvertStr_Empty_To_Null()}
                                , OD.OSNo
                                , OD.OSSeq
                                , null
                                , null
                                , {Qty.ConvertNumeric_Empty_To_Null()}
                                , {OutQty.ConvertNumeric_Empty_To_Null()}
                                , '{WriteID}'
                                , 'N'
                                , null
                                , '{WrGlobal.LoginID}'
                                , HOST_NAME()
                                , GETDATE()
                                , null
                        FROM OutsourcingDetail OD
                        INNER JOIN OutsourcingMaster OM ON OD.OSNo = OM.OSNo
                        WHERE OD.CorpId = '{WrGlobal.CorpID}'
                        AND OD.OSNo = '{JobNo}'
                        AND OD.OSSeq = '{JobSeq}'

                        ";

            return db.ExecuteQuery(query);
        }

        public bool Ins_ST_StockD_ByReqNo()
        {
            query = $@"INSERT INTO ST_StockD 
                        (
                            CorpId
                            , InOutNo
                            , Seq
                            , CustCode
                            , InOutDate
                            , Type
                            , ItemCode
                            , LotNo
                            , FromHouseCode
                            , ToHouseCode
                            , JobNo
                            , JobSeq
                            , JobSeq2
                            , FaultCode
                            , Qty
                            , OutQty
                            , WriteID
                            , CloseYn
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                            , Status
                        )
                        SELECT     
                            '{WrGlobal.CorpID}'       -- CorpId
                            , '{InOutNo}'             -- InOutNo
                            , {Seq}                     -- Seq
                            , null                    -- CustCode
                            , GETDATE()               -- InOutDate
                            , '{Type}'                -- Type
                            , '{MaterialCode}'        -- MaterialCode
                            , null                    -- LotNo
                            , '{FromHouseCode}'       -- FromHouseCode
                            , '{ToHouseCode}'         -- ToHouseCode
                            , RD.ReqNo               -- JobNo (ReqNo)
                            , RD.ReqSeq                -- JobSeq (ReqSeq)
                            , null                    -- JobSeq2
                            , null                    -- FaultCode
                            , RD.ReqQty                   -- Qty (RequestQty)
                            , null                    -- OutQty
                            , '{WriteID}'             -- WriteID
                            , 'N'                     -- CloseYn
                            , null                    -- Remark
                            , '{WrGlobal.LoginID}'    -- CreId
                            , HOST_NAME()             -- CreIP
                            , GETDATE()               -- CreDt
                            , null                    -- ModId
                            , null                    -- ModIP
                            , null                    -- ModDt
                            , null                    -- Status
                FROM RequestDetail RD
                WHERE RD.CorpId = '{WrGlobal.CorpID}'
                AND RD.ReqNo = '{JobNo}'
                AND RD.MaterialCode = '{MaterialCode}'  -- 특정 자재에 대해서만 처리    
                AND RD.MmOrderNo = '{MmOrderNo}'    -- 작업지시번호 추가
                AND RD.MmOrderSeq = {MmOrderSeq}
                ";

            return db.ExecuteQuery(query);
        }

        public bool Ins_ST_StockD_StockOut()
        {
            query = $@"
                        INSERT INTO ST_StockD (
                            CorpId
                            , InOutNo
                            , Seq
                            , InOutDate
                            , Type
                            , MaterialCode
                            , FromHouseCode
                            , Qty
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{InOutNo}'
                            , 1
                            , '{InOutDate}'
                            , '{Type}'
                            , '{MaterialCode}'
                            , '{FromHouseCode}'
                            , {Qty}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }
        public bool InsST_StockD_ByChemicalSubstancIn()
        {
            query = $@"
                        INSERT INTO ST_StockD 
                        (
                            CorpId
                            , InOutNo
                            , Seq
                            , InOutDate
                            , Type
                            , ItemCode
                            , FromHouseCode
                            , ToHouseCode
                            , JobNo
                            , Qty
                            , WriteID
                            , CloseYn
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES 
                        (
                            '{WrGlobal.CorpID}'
                            , '{InOutNo}'
                            , 1
                            , GETDATE()
                            , '투입'
                            , '{MaterialCode}'
                            , '{FromHouseCode}'
                            , '{ToHouseCode}'
                            , '{JobNo}'
                            , '{Qty}'
                            , '{WrGlobal.LoginID}'
                            , 'N'
                            , '{WrGlobal.LoginID}'
                            , HOST_NAME()
                            , GETDATE()
                        )
                ";

            return db.ExecuteQuery(query);
        }

        #region Upd문
        public void Upd_ST_StockD_WasteQty(string orderNo, string productCode)
        {
            query = $@"
                        -- bom에 한 가공공정에 배출자재 2개가 등록되어있어서 이렇게 함
                        DECLARE @WasteMaterialTable TABLE
                        (
	                        MaterialCode varchar(20)	-- 폐기물코드
	                        , Qty int					-- 수량
                        )

                        INSERT INTO @WasteMaterialTable
                        SELECT
	                        BOM.BomMaterialCode
	                        , BOM.WorkQty
                        FROM PM_SdRoute SR
                        LEFT JOIN PM_SdRoute BOM ON SR.CorpId = BOM.CorpId AND SR.OrderNo = BOM.OrderNo AND SR.ChildID = BOM.ParentID
                        LEFT JOIN CM_Item CM ON BOM.BomMaterialCode = CM.MaterialCode
                        WHERE SR.CorpId = '{WrGlobal.CorpID}'
                        AND SR.OrderNo = '{orderNo}'
                        AND SR.BomMaterialCode = '{productCode}'
                        AND CM.MaterialType = '70'

                        DECLARE cur CURSOR FOR SELECT * FROM @WasteMaterialTable;
                        DECLARE @MaterialCode varchar(20)
		                        , @Qty int

                        OPEN cur;
                        FETCH NEXT FROM cur INTO @MaterialCode, @Qty;

                        WHILE @@FETCH_STATUS = 0
                        BEGIN
	                        UPDATE ST_StockD
		                        SET Qty = (ISNULL(ORT.GoodQty, 0) + ISNULL(ORT.NgQty, 0)) * @Qty
	                        FROM ST_StockD SD
	                        LEFT JOIN PM_OrderResult ORT ON SD.CorpId = ORT.CorpId AND SD.JobNo = ORT.SerialNo
	                        WHERE SD.JobNo = '{JobNo}'
	                        AND SD.Remark = '{Remark}'
	                        AND SD.MaterialCode = @MaterialCode

	                        FETCH NEXT FROM cur INTO @MaterialCode, @Qty;
	                    END
	                    CLOSE cur;
                    ";

            db.ExecuteQuery(query);
        }

        public bool Upd_Qty_ByDefect()
		{
            query = $@"
                UPDATE ST_StockD
                SET Qty = '{Qty}'
                WHERE CorpId = '{CorpId}'
                AND JobNo = '{JobNo}'
                AND JobSeq = '{JobSeq}'
                AND JobSeq2 = '{JobSeq2}'
                AND Type = '{Type}'
                ";

            return db.ExecuteQuery(query);

		}
        #endregion

        #region Select문
        public DataTable SelectStockList()
        {
            if (MaterialCode.ToUpper() == "ALL") MaterialCode = "";

            query = $@"SELECT '' Status
                                , CM.MaterialCode
		                        , CM.MaterialName
		                        , ISNULL(A.StockQty, 0) StockQty
                                , 0 UpdQty
		                        , CM.StockChkDate ChkDate
                        FROM CM_Item CM
                        LEFT JOIN (
	                        SELECT ProductCode MaterialCode
			                        , SUM(StockQty) StockQty
	                        FROM StockListView
                    ";

            if (HouseCode.ConvertString_Null_To_Empty() != "") query += $"\n WHERE WareHouseCode = '{HouseCode}'";

            query += $@"GROUP BY ProductCode
	                        ) AS A ON CM.MaterialCode = A.MaterialCode
                        WHERE CM.CorpId = '{WrGlobal.CorpID}'
                        ";

            if (MaterialCode != "") query += $"\n AND CM.MaterialCode = '{MaterialCode}'";
            if (MaterialType != "") query += $"\n AND CM.MaterialType = '{MaterialType}'";

            return db.GetDataTable(query);
        }

        public DataTable SelectStockList_ByInOutNo()
        {
            query = $@"
                        SELECT *
                        FROM (
                            SELECT CASE
		                            WHEN SD.Type = 'I' THEN '입고'
		                            WHEN SD.Type = 'O' THEN '출고'
		                            ELSE SD.Type
		                            END AS [Type]
		                            , SM.WriteDate AS StockDate
		                            , ISNULL(OutQty, 0) + ISNULL(Qty, 0) AS Qty
		                            , SD.Remark 
                            FROM ST_StockD SD
                            INNER JOIN StockMaster SM ON SD.InOutNo = SM.InOutNo
                            WHERE SD.CorpID = '{WrGlobal.CorpID}'
                            AND SD.MaterialCode = '{MaterialCode}'
                    ";

            if (HouseCode.ConvertString_Null_To_Empty() != "")
            {
                query += $@"
                            AND (SD.ToHouseCode = '{HouseCode}'
                                OR SD.FromHouseCode = '{HouseCode}'
                                )
                             ";
            }

            query += $@"
                            UNION ALL 
                        
                            SELECT '재고조정' AS [Type]
                                    , AM.StartDate AS StockDate
                                    , AD.Qty
                                    , AD.Remark
                            FROM AuditDetail AD
                            INNER JOIN AuditMaster AM ON AD.AuditNo = AM.AuditNo
                            WHERE AD.CorpID = '{WrGlobal.CorpID}'
                            AND AD.MaterialCode = '{MaterialCode}'
                        ";

            if (HouseCode.ConvertString_Null_To_Empty() != "")
            {
                query += $@"
                            AND AM.HouseCode = '{HouseCode}'
                             ";
            }

            query += $@"
                        ) AS A 
                        ORDER BY StockDate DESC
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_StockWasteIn_ByJobNo()
        {
            query = $@"SELECT SD.InOutNo
		                        , SD.Seq
		                        , SD.InOutDate
		                        , SD.MaterialCode
		                        , CM.MaterialName
		                        , SD.Qty / 1000 Qty
		                        , SD.Remark
                        FROM ST_StockD SD
                        LEFT JOIN CM_Item CM ON SD.MaterialCode = CM.MaterialCode
                        WHERE SD.CorpID = '{WrGlobal.CorpID}'
                        AND SD.JobNo = '{JobNo}'
                        AND CM.MaterialType = '70'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_ST_StockD_ByJobNo()
        {
            query = $@"SELECT SD.InOutNo
		                        , SD.Seq
		                        , SD.InOutDate
		                        , SD.MaterialCode
		                        , CM.MaterialName
		                        , SD.Qty
		                        , SD.Remark
                        FROM ST_StockD SD
                        LEFT JOIN CM_Item CM ON SD.MaterialCode = CM.MaterialCode
                        WHERE SD.CorpID = '{WrGlobal.CorpID}'
                        AND SD.JobNo = '{JobNo}'
                        AND SD.JobSeq = '{JobSeq}'
                        AND SD.[Type] = '투입'
                    ";

            return db.GetDataTable(query);
        }
        public DataTable Set_ST_StockD()
        {
            query = $@"SELECT   TOP 0
                                '' Status
                                , SD.InOutNo
		                        , SD.Seq
                                , SD.JobNo
                                , SD.JobSeq
		                        , SD.InOutDate
		                        , SD.MaterialCode
		                        , CM.MaterialName
		                        , SD.Qty
		                        , SD.Remark

                                , SD.FromHouseCode
                                , SD.ToHouseCode
                        FROM ST_StockD SD
                        LEFT JOIN CM_Item CM ON SD.MaterialCode = CM.MaterialCode
                        WHERE SD.CorpID = '{WrGlobal.CorpID}'
                        AND SD.JobNo = '{JobNo}'
                    ";

            return db.GetDataTable(query);
        }

        #endregion

        public bool DeleteStockMD()
        {
            query = $@"
                        DECLARE @InOutNo nvarchar(20) = ''
                        
                        SELECT @InOutNo = InOutNo
                        FROM ST_StockD 
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND JobNo = '{JobNo}'

                        -- stock 삭제
                        DELETE FROM ST_StockD 
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND InOutNo = @InOutNo;        
                    ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteStockMD_Work()
        {
            query = $@"

                    DECLARE @TB TABLE
                    (
                    	InOutNo nvarchar(20)
                    )
                    
                    INSERT @TB
                    SELECT InOutNo
                    FROM ST_StockD 
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND JobNo = '{JobNo}'
                    AND JobSeq = {JobSeq}
                    AND JobSeq2 = {JobSeq2}
                    
                    DELETE ST_StockD WHERE InOutNo IN (SELECT InOutNo FROM @TB)
                    DELETE ST_StockM WHERE InOutNo IN (SELECT InOutNo FROM @TB)
                    ";

            return db.ExecuteQuery(query);
        }
    }

}