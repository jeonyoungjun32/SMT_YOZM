using System.Collections.Generic;
using SmartCommon.Common;
using System.Data;
using SmartMES.Common;

namespace SmartMES.Models.MD
{
    public class MD_Mold
    {
        #region Property

        public string MoldCode { get; set; }
        public string RepairDiv { get; set; }
        public string RepairNo { get; set; }

        public string MoldMgmtNo { get; set; }
        public string MoldSN { get; set; }
        public string Chasu { get; set; }
        public string MoldName { get; set; }
        public string ApplyEquip { get; set; }
        public string MoldStd { get; set; }
        public string MoldForm { get; set; }
        public int MoldItemCount { get; set; }
        public int MoldCavity { get; set; }
        public string MoldCavityText { get; set; }
        public string FigurationGrade { get; set; }
        public string SupplyCust { get; set; }
        public string HoldCust { get; set; }
        public string ProdtCust { get; set; }
        public string ProdtDate { get; set; }
        public decimal ProdtAmount { get; set; }
        public string GateMethod { get; set; }
        public decimal ContractRate { get; set; }
        public decimal Weight { get; set; }
        public decimal MoldWeight { get; set; }
        public decimal Sprue { get; set; }
        public int InitShot { get; set; }
        public int LifeShot { get; set; }
        public byte[] Mold_Image1 { get; set; }
        public byte[] Mold_Image2 { get; set; }
        public string Item_Cd { get; set; }
        public string FocusMoldCd { get; set; }

        public string Seq { get; set; }
        public string Disp_Seq { get; set; }
        public string RePairCause { get; set; }
        public string Repair_State { get; set; }
        public string Repair_Start_Time { get; set; }
        public string Repair_End_Time { get; set; }
        public int Repair_Time { get; set; }
        public string Repair_Date { get; set; }
        public string Repair_Emp_No { get; set; }
        public string Repair_Cust_Cd { get; set; }
        public int Repair_Amt { get; set; }
        public string DisposalDate { get; set; }

        public string rstFrom { get; set; }
        public string rstTo { get; set; }

        public string UseYn { get; set; }

        public string Zone { get; set; }
        public string LocNo { get; set; }
        public string MoldPjtNo { get; set; }
        public string SpecialParts { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MD_Mold()
        {
            query = $@"
                        SELECT
                            CorpId
                            , MoldCode
                            , MoldName
                            , MarkingNo
                            , MoldType
                            , ForType
                            , ProdCust
                            , ProdDate
                            , TongNo
                            , Shape
                            , ObtainAmount
                            , Cavity
                            , PressAram
                            , PressMax
                            , PressCnt
                            , Weight
                            , Size
                            , SubSize
                            , Mising
                            , ShrinkRate
                            , SerialNo
                            , DisposeDate
                            , Status
                            , Remark
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM MD_Mold
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        ORDER BY TongNo ASC
                    ";

            return db.GetDataTable(query);
        }

        public DataRow Select_MD_Mold_byCode()
        {
            query = $@"
                        SELECT
                            CorpId
                            , MoldCode
                            , MoldName
                            , MarkingNo
                            , MoldType
                            , ForType
                            , ProdCust
                            , ProdDate
                            , ObtainAmount
                            , Cavity
                            , PressAram
                            , PressMax
                            , PressCnt
                            , Weight
                            , Size
                            , TongNo
                            , Shape
                            , SubSize
                            , Mising
                            , ShrinkRate
                            , SerialNo
                            , DisposeDate
                            , Status
                            , Remark
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM MD_Mold
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                    ";

            return db.GetDataRow(query);
        }

        public DataTable Select_Mold()
        {
            //금형정보(Frm_MD_Mold)에서 금형리스트 호출하는 Query
            query = $@"SELECT '' AS Status
                            , mold.UseYn
                            , mold.MoldCode
                            , mold.Chasu
                            , mold.MoldPjtNo
                            --, mold.MoldMgmtNo 
                            , CAST(mold.MoldMgmtNo  AS INT) AS MoldMgmtNo 
                            , mold.MoldSN
                            , mold.MoldName
                            , mold.ApplyEquip
                            , mold.MoldStd
                            , mold.MoldForm
                            , mold.MoldItemCount
                            , mold.MoldCavity
                            , mold.FigurationGrade
                            , mold.SupplyCust
                            , mold.ProdtCust
                            , CONVERT(DATE, mold.ProdtDate, 112) AS ProdtDate
                            , CONVERT(DATE, mold.DisposalDate, 112) AS DisposalDate
                            , mold.ProdtAmount
                            , mold.GateMethod
                            , mold.ContractRate
                            , mold.Weight
                            , mold.MoldWeight
                            , mold.Sprue
                            , ISNULL(mold.InitShot, 0) InitShot
                            , ISNULL(mold.LifeShot, 0) LifeShot
                            , 0 AS Shot
                            --, CASE
							--WHEN mmr.RepairShot is null AND mmc.InspectShot is null THEN 0
							--WHEN mmr.RepairShot is null AND mmc.InspectShot is not null THEN mmc.InspectShot
							--WHEN mmr.RepairShot is not null AND mmc.InspectShot is null THEN mmr.RepairShot
							--WHEN mmr.RepairShot >= mmc.InspectShot THEN mmr.RepairShot
							--WHEN mmr.RepairShot < mmc.InspectShot THEN mmc.InspectShot
							--ELSE 0
							--END AS RepairShot

							, ISNULL(mmr.RepairShot, 0) AS RepairShot

                            , CASE
								WHEN mmr.RepairDate is null AND mmc.InspectDate is null THEN null
								WHEN mmr.RepairDate is null AND mmc.InspectDate is not null THEN CONVERT(DATETIME, mmc.InspectDate)
								WHEN mmr.RepairDate is not null AND mmc.InspectDate is null THEN CONVERT(DATETIME, mmr.RepairDate)
								WHEN mmr.RepairDate >= mmc.InspectDate THEN CONVERT(DATETIME, mmr.RepairDate)
								WHEN mmr.RepairDate < mmc.InspectDate THEN CONVERT(DATETIME, mmc.InspectDate)
								END AS RepairDate
                            --, ISNULL(pd.Shot, 0) / mold.MoldCavity  AS AccumulateShot
                            , ISNULL(pd.Shot, 0) AS AccumulateShot
                            , mold.CreId
                            , mold.CreDt
                            , mold.ModId
                            , mold.ModDt
							, '' AS ManageRating

                            , mold.Zone
                            , mold.LocNo
                            , mold.MoldPjtNo
                            , mold.MoldCavityText
                            , mold.HoldCust
                            , mold.SpecialParts

                            , CASE
								WHEN ISNULL(sc2.Remark, 0) = 0 THEN 0
								ELSE CAST((ISNULL(pd.Shot, 0) - ISNULL(mmr.RepairShot , 0)) / ISNULL(sc2.Remark, 0) * 100 AS Decimal(18,2))
								END AS ChkRate

                            , ISNULL(sc2.Remark, 0) AS Std_Shot
                            , pd.Last_Rst_Date
                         FROM MD_Mold mold
                    LEFT JOIN MD_MoldRepair mmr ON mold.CorpId = mmr.CorpId AND mold.MoldCode = mmr.MoldCode 
                                                 AND mmr.Seq = (SELECT top 1 seq FROM MD_MoldRepair WHERE CorpId = mold.CorpId AND MoldCode = mold.MoldCode order by RepairDate desc, Seq desc)
				    LEFT JOIN MD_MoldChk    mmc ON mold.CorpId = mmc.CorpId AND mold.MoldCode = mmc.MoldCode 
                                                 AND mmc.InspectDate = (SELECT top 1 InspectDate FROM MD_MoldChk WHERE CorpId = mold.CorpId AND MoldCode = mold.MoldCode order by InspectDate desc)
                                                 AND mmc.Seq = (SELECT top 1 Seq FROM MD_MoldChk WHERE  CorpId = mold.CorpId AND MoldCode = mold.MoldCode order by InspectDate desc, Seq desc)
                    LEFT JOIN MD_MoldOut mo ON mold.MoldCode = mo.MoldCode
			        --LEFT JOIN SC_CmCd sc ON mo.Out_Type = sc.Cm_Cd and sc.Cm_Class = '072'
                    LEFT JOIN
								(
								SELECT A.MoldCode, SUM(CAST(A.Shot AS BIGINT)) AS Shot
                                        , MAX(CONVERT(DATE, WorkDate))  AS Last_Rst_Date
								FROM
									(SELECT MoldCode, shot, WorkDate
									FROM PM_OrderResult
                                    WHERE CorpId = '{WrGlobal.CorpID}'
                    
                                    UNION ALL
                    
	                                SELECT MoldCode
                                           -- MoldSN
                                            , InitShot AS Shot, null AS WorkDate
	                                FROM MD_Mold
                                    WHERE CorpId = '{WrGlobal.CorpID}'
                    
									) A
								GROUP BY A.MoldCode
								) pd ON pd.MoldCode = mold.MoldCode
                     LEFT JOIN CM_CateItem sc2 ON sc2.CorpId = mold.CorpId AND sc2.CLASS = '060' -- AND sc2.Cm_Cd = mold.Mold_Grade
                        WHERE mold.CorpId = '{WrGlobal.CorpID}'";

            if (MoldCode != "ALL" && MoldCode != "")
            {
                query += $@" and mold.MoldCode = '{MoldCode}' ";
            }
            //if (Supply_CustCode != "ALL")
            //{
            //    query += $"\n and mold.Supply_CustCode != '{Supply_CustCode}' ";
            //}

            //query += $"\n  Order By ChkRate DESC";
            query += $"\n  Order By CAST(mold.MoldMgmtNo AS INT)";

             return db.GetDataTable(query);
        }

        public DataTable Select_MoldList(string date)
        {
            //금형정보(Frm_MD_Mold)에서 금형리스트 호출하는 Query
            query = $@"SELECT A.MoldSN
                            , A.MoldCode
                            , A.chasu
                            , ISNULL(pd.Shot, 0) AS AccumulateShot
                            , A.SpecialParts
                         FROM MD_Mold A
                         --JOIN (
                         --       SELECT DISTINCT MoldCode
                         --         FROM PM_OrderResult
                         --        WHERE WorkDate = '{date}'
                         --	       AND MoldCode IS NOT NULL
                         --          AND MoldCode <> ''
                         --      ) B ON A.MoldCode = B.MoldCode
                         LEFT JOIN 
								(
								   SELECT A.MoldCode, SUM(CAST(A.Shot AS BIGINT)) AS Shot
								     FROM
									      (SELECT MoldCode, shot
									         FROM PM_OrderResult
                                            WHERE CorpID = '{WrGlobal.CorpID}'
                                          
                                            UNION ALL
                                          
	                                       SELECT MoldCode, InitShot AS Shot
	                                         FROM MD_Mold
                                            WHERE CorpID = '{WrGlobal.CorpID}'
									      ) A  GROUP BY A.MoldCode
								) pd ON pd.MoldCode = A.MoldCode";
            return db.GetDataTable(query);
        }

        public DataTable Select_EquipmentManagement(string FocusMoldCd)
        {
            //금형관리대장 Query
            FocusMoldCd = FocusMoldCd.ConvertStr_Empty_To_Null();

            query = $@"SELECT  
                      bm.*
		            , Convert(Date,mr.RepairDate) AS RepairDate
		            , mr.RepairCause
		            , mr.RepairState
		            , mr.RepairEmp
		            , mr.RepairStartTime
		            , mr.RepairEndTime
                    , mr.RepairTime
		            , mr.RepairCust
		            , mr.RepairAmt
					, mr.Remark

		            , c1.CustName Supply_CustName 
		            , c2.CustName Prodt_CustName 
		            , c3.CustName Repair_CustName

		            , bi.ItemName   
		            , bi.Alias 
    	           , bi.ItemClass1 CarType

                    FROM MD_Mold bm 

                    INNER JOIN  MD_MoldRepair mr ON bm.CorpID = mr.CorpID And bm.MoldCode = mr.MoldCode
                    LEFT JOIN  CM_Cust    c1 ON bm.CorpID = c1.CorpID And bm.SupplyCust = c1.CustCode
                    LEFT JOIN  CM_Cust    c2 ON bm.CorpID = c2.CorpID And bm.ProdtCust  = c2.CustCode
                    LEFT JOIN  CM_Cust    c3 ON mr.CorpID = c3.CorpID And mr.RepairCust = c3.CustCode
                    LEFT JOIN  ( 
					              SELECT TOP 1 * 
					              FROM MD_MoldItem    
					              WHERE CorpID    = '{WrGlobal.CorpID}'
                                  AND MoldCode    = {FocusMoldCd}
					              AND UseYn = 'Y'
					) mi ON mr.CorpID = mi.CorpID And mr.MoldCode = mi.MoldCode 
                    LEFT JOIN  CM_Item        bi ON mi.CorpID = bi.CorpID And mi.ItemCode        = bi.ItemCode 
                    --LEFT JOIN  CM_ItemClass  bic ON bic.CorpID = bm.CorpID AND bic.ItemClass_Cd = bi.ItemClass_Cd

                    WHERE bm.CorpID    = '{WrGlobal.CorpID}'";

            if (FocusMoldCd != "NULL")
            {
                query += $@" and bm.MoldCode    = {FocusMoldCd} ";
            }

            query += $@" ORDER BY mr.RepairDate asc ";

            return db.GetDataTable(query);
        }

        public DataRow Select_MoldCode_Last()
        {
            query = $@"SELECT TOP 1
                             MoldCode
                        FROM MD_Mold
                        WHERE CorpID = '{WrGlobal.CorpID}'
                    ORDER BY MoldCode DESC";

            return db.GetDataRow(query);

        }

        public DataTable Select_MoldHistory(bool monitoringYN = false)
        {
            MoldCode = MoldCode.ConvertString_Null_To_Empty() == "ALL" ? "" : MoldCode.ConvertString_Null_To_Empty();

            //설비관리 설비수리이력 호출에 사용되는 Query
            query = $@"

                     SELECT
                    '' AS Status
                    , bm.MoldCode
					, bm.MoldName
					, bm.MoldMgmtNo
					, bm.MoldSN
					, bm.ApplyEquip
					, bm.MoldStd
                    , bm.MoldItemCount
					, bm.MoldCavity
					, bm.FigurationGrade
					, bm.MoldForm
                    , bm.SupplyCust
					, bm.ProdtCust
					, bm.ProdtDate
					, bm.ProdtAmount
					, bm.DisposalDate
                    , bm.GateMethod
					, bm.ContractRate
					, bm.Weight
					, bm.Sprue
                    , ISNULL(bm.InitShot,0) InitShot
                    , ISNULL(bm.LifeShot,0) LifeShot
                    , 0 LastShot
                    , 0 LastShot
                    --, 0 Shot

                    , bi.ItemCode
                    , bi.ItemName
                    , bi.Alias
                    --, bic.ItemClass_Nm CarType

                    , pr.OrderNo
                    , pr.OrderSeq
                    , CONVERT(DATE, pr.WorkDate ) WorkDate
                    , pr.WorkQty
                    , ISNULL(pr.Shot,0) Shot

                    FROM MD_Mold bm
                    INNER JOIN  PM_OrderResult pr ON bm.CorpId = pr.CorpId And bm.MoldCode = pr.MoldCode
                    INNER JOIN  CM_Item	bi ON pr.CorpId = bi.CorpId And pr.ItemCode = bi.ItemCode
                    --LEFT JOIN   BS_ItemClass	bic ON bm.CorpId = bic.CorpId AND bic.ItemClass_Cd = bi.ItemClass_Cd

                    WHERE bm.CorpID = '{WrGlobal.CorpID}'";

            if (MoldCode.ConvertString_Null_To_Empty() != "")
            {
                query += $@" and bm.MoldCode = '{MoldCode}'";
            }
            if (monitoringYN)
            {
                query += $@"AND pr.WorkDate  BETWEEN '{rstFrom}' AND '{rstTo}'";
            }
            //query += $@" ORDER BY pr.Rst_No asc  ";


            return db.GetDataTable(query);
        }

        public DataTable Select_Mold_Mgmt_No(bool all_Yn = false)
        {
            query = "";

            if (all_Yn)
            {
                query += $@" SELECT 'ALL' AS 사용유무, 'ALL' AS 코드, '전체' AS 금형코드, '전체' AS 차수, '전체' AS 관리번호 , '전체' AS 금형명
                             UNION ALL 
                            ";
            }

            query += $@"SELECT UseYN AS 사용유무, MoldCode AS 코드, MoldSN AS 금형코드, Chasu AS 차수, MoldMgmtNo AS 관리번호 , MoldName AS 금형명
                         FROM MD_Mold
                        WHERE CorpID = '{WrGlobal.CorpID}' 
                        --AND UseYn = 'Y'
                ";

            DataTable DT = db.GetDataTable(query);

            return DT;
        }

        /*
        public bool Ins_MD_Mold()
        {
            query = $@"

                        DECLARE @MoldCode NVARCHAR(10);

                        SELECT TOP 1 @MoldCode = 'M' + RIGHT('0000' + CAST(CAST(SUBSTRING(MoldCode, 2, 5) AS INT) + 1 AS NVARCHAR), 5)
                        FROM MD_Mold
                        WHERE MoldType = '{MoldType}'
                        ORDER BY MoldCode DESC;
                        
                        IF @MoldCode IS NULL SET @MoldCode = 'M00001';

                        INSERT INTO MD_Mold
                        (
                            CorpId
                            , MoldType
                            , MoldCode
                            , MoldName
                            , MarkingNo
                            , ProdCust
                            , ProdDate
                            , Cavity
                            , PressMax
                            , Size
                            , TongNo
                            , Shape
                            , SubSize
                            , Mising
                            , Weight
                            , Remark
                            , Status
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{MoldType}'
                            , @MoldCode
                            , '{MoldName}'
                            , '{MarkingNo}'
                            , '{ProdCust}'
                            , '{ProdDate}'
                            , {Cavity}
                            , {PressMax}
                            , '{Size}'
                            , '{TongNo}'
                            , '{Shape}'
                            , '{SubSize}'
                            , '{Mising}'
                            , {Weight}
                            , '{Remark}'
                            , '{Status}'
                            , '{UseYn}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }
        */

        public bool Ins_MD_Mold()
        {
            query = $@"INSERT INTO MD_Mold(CorpID
                                         , MoldCode, MoldMgmtNo, MoldPjtNo, MoldSN, Chasu, MoldName, Zone, LocNo, ApplyEquip, MoldStd, MoldForm
                                         , MoldItemCount, MoldCavity, FigurationGrade
                                         , SupplyCust, ProdtCust, ProdtDate, ProdtAmount
                                         , GateMethod, ContractRate, Weight, MoldWeight, Sprue
                                         , InitShot, LifeShot, DisposalDate
                                         , HoldCust, MoldCavityText, SpecialParts
                                         , CreId, CreIP, CreDt, UseYn)
                            VALUES('{WrGlobal.CorpID}'

                            , {MoldCode.ConvertStr_Empty_To_Null()}
                            , {MoldMgmtNo.ConvertStr_Empty_To_Null()}
                            , {MoldPjtNo.ConvertStr_Empty_To_Null()}
                            , {MoldSN.ConvertStr_Empty_To_Null()}
                            , {Chasu.ConvertStr_Empty_To_Null()}
                            , {MoldName.ConvertStr_Empty_To_Null()}
                            , {Zone.ConvertStr_Empty_To_Null()}
                            , {LocNo.ConvertStr_Empty_To_Null()}
                            , {ApplyEquip.ConvertStr_Empty_To_Null()}
                            , {MoldStd.ConvertStr_Empty_To_Null()}
                            , {MoldForm.ConvertStr_Empty_To_Null()}

                            , {MoldItemCount.ConvertInt_Empty_To_Null()}
                            , {MoldCavity.ConvertInt_Empty_To_Null()}
                            , {FigurationGrade.ConvertStr_Empty_To_Null()}

                            , {SupplyCust.ConvertStr_Empty_To_Null()}
                            , {ProdtCust.ConvertStr_Empty_To_Null()}
                            , {ProdtDate.ConvertStr_Empty_To_Null()}
                            , {ProdtAmount.ConvertDecimal_Empty_To_Null()}

                            , {GateMethod.ConvertStr_Empty_To_Null()}
                            , {ContractRate.ConvertDecimal_Empty_To_Null()}
                            , {Weight.ConvertDecimal_Empty_To_Null()}
                            , {MoldWeight.ConvertDecimal_Empty_To_Null()}
                            , {Sprue.ConvertDecimal_Empty_To_Null()}

                            , {InitShot.ConvertInt_Empty_To_Null()}
                            , {LifeShot.ConvertInt_Empty_To_Null()}
                            , {DisposalDate.ConvertStr_Empty_To_Null()}

                            , {HoldCust.ConvertStr_Empty_To_Null()}
                            , {MoldCavityText.ConvertStr_Empty_To_Null()}                        

                            , {SpecialParts.ConvertStr_Empty_To_Null()}

                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                            , '{UseYn}')";

            return db.ExecuteQuery(query);
        }

        public bool Upd_MD_Mold()
        {
            query = $@"UPDATE MD_Mold
                          SET MoldMgmtNo = { MoldMgmtNo.ConvertStr_Empty_To_Null() }
                            , MoldPjtNo = {MoldPjtNo.ConvertStr_Empty_To_Null()}
                            , MoldSN = { MoldSN.ConvertStr_Empty_To_Null() }
                            , Chasu = {Chasu.ConvertStr_Empty_To_Null()}
                            , MoldName = { MoldName.ConvertStr_Empty_To_Null() }
                            , Zone = { Zone.ConvertStr_Empty_To_Null() }
                            , LocNo = { LocNo.ConvertStr_Empty_To_Null() }
                            , ApplyEquip = { ApplyEquip.ConvertStr_Empty_To_Null() }
                            , MoldStd = { MoldStd.ConvertStr_Empty_To_Null() }
                            , MoldForm = { MoldForm.ConvertStr_Empty_To_Null() }

                            , MoldItemCount = { MoldItemCount.ConvertInt_Empty_To_Null() }
                            , MoldCavity = { MoldCavity.ConvertInt_Empty_To_Null() }
                            , FigurationGrade = { FigurationGrade.ConvertStr_Empty_To_Null() }

                            , SupplyCust = { SupplyCust.ConvertStr_Empty_To_Null() }
                            , ProdtCust = { ProdtCust.ConvertStr_Empty_To_Null() }
                            , ProdtDate = { ProdtDate.ConvertStr_Empty_To_Null() }
                            , DisposalDate = { DisposalDate.ConvertStr_Empty_To_Null() }
                            , ProdtAmount = { ProdtAmount.ConvertDecimal_Empty_To_Null() }

                            , GateMethod = { GateMethod.ConvertStr_Empty_To_Null() }
                            , ContractRate = { ContractRate.ConvertDecimal_Empty_To_Null() }
                            , Weight = { Weight.ConvertDecimal_Empty_To_Null() }
                            , MoldWeight = {MoldWeight.ConvertDecimal_Empty_To_Null()}
                            , Sprue = { Sprue.ConvertDecimal_Empty_To_Null() }

                            , InitShot = { InitShot.ConvertInt_Empty_To_Null() }
                            , LifeShot = { LifeShot.ConvertInt_Empty_To_Null() }

                            , HoldCust = {HoldCust.ConvertStr_Empty_To_Null()}
                            , MoldCavityText = {MoldCavityText.ConvertStr_Empty_To_Null()}

                            , SpecialParts = {SpecialParts.ConvertStr_Empty_To_Null()}
                            , UseYn    = {UseYn.ConvertStr_Empty_To_Null()}
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND MoldCode = { MoldCode.ConvertStr_Empty_To_Null() }";

            return db.ExecuteQuery(query);
        }

        public bool Update_InitShot()
        {
            query = $@"
                        UPDATE MD_Mold
                        SET
                            InitShot = {InitShot}
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                    ";

            return db.ExecuteQuery(query);
        }

        /*
        public bool Upd_MD_Mold()
        {
            query = $@"
                        UPDATE MD_Mold
                        SET
                            MoldName = '{MoldName}'
                            , MarkingNo = '{MarkingNo}'
                            , ProdCust = '{ProdCust}'
                            , ProdDate = '{ProdDate}'
                            , Cavity = {Cavity}
                            , PressMax = {PressMax}
                            , Size = '{Size}'
                            , TongNo = '{TongNo}'
                            , Shape = '{Shape}'
                            , SubSize = '{SubSize}'
                            , Mising = '{Mising}'
                            , Weight = {Weight}
                            , Remark = '{Remark}'
                            , UseYn = '{UseYn}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldType = '{MoldType}'
                        AND MoldCode = '{MoldCode}'
                    ";

            return db.ExecuteQuery(query);
        }


        public DataTable SelectPressCntView()
        {
            query = $@"
                        SELECT
	                        CorpId
	                        , MoldCode
	                        , MoldName
                            , PressCnt
                            , PressMax
                        FROM MD_Mold
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldType = '{MoldType}'
                    ";

            return db.GetDataTable(query);
        }

        public bool UpdatePressCnt()
        {
            query = $@"
                        UPDATE MD_Mold
                        SET
                            PressCnt = {PressCnt}
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                    ";

            return db.ExecuteQuery(query);
        }
         */

        public bool Del_MD_Mold()
        {
            query = $@"
                        DELETE MD_Mold WHERE CorpId = '{WrGlobal.CorpID}'  AND MoldCode = '{MoldCode}'
                    ";

            return db.ExecuteQuery(query);
        }

        public (string Code, string Name, string formName) GetMoldTexts(string moldType)
        {
            var moldMap = new Dictionary<string, (string Code, string Name, string formName)>
            {
                { "10", ("목형코드", "목형명", "목형현황") },
                { "20", ("나이프코드", "나이프명", "나이프현황") },
                { "30", ("금형코드", "금형명", "금형현황")}
            };

            return moldMap.TryGetValue(moldType, out var result)? result: ("코드", "명", "현황");
        }

        public static readonly Dictionary<string, string> _moldTypeMap = new Dictionary<string, string>
        {
            { "목형", "10" },
            { "나이프", "20" },
            { "금형", "30" }
        };


        public string GetMoldTypeCode(string moldTypeName)
        {
            return _moldTypeMap.TryGetValue(moldTypeName, out var code) ? code : null;
        }
    }
}
