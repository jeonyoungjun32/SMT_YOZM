using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;

namespace SmartMES.Models.MD
{
    public class MD_MoldChk
    {
        #region Property

        public string MoldCode { get; set; }

        public int Seq { get; set; }

        public string InspectDate { get; set; }

        public string ChkTimeDivCd { get; set; }

        public string InspectDivCd { get; set; }

        public string InspectCheck { get; set; }

        public int InspectShot { get; set; }

        public string ChargeEmpNo { get; set; }

        public string ActionResultDivCd { get; set; }

        public string ActionDivCd { get; set; }

        public string ActionMemo { get; set; }

        public string ActionEmpNo { get; set; }

        public byte[] ChkImage { get; set; }

        public string Remark { get; set; }

        public string MachineCode { get; set; }
        public string ItemCode { get; set; }
        public string WeekDivCd { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Sel_Mold_Chk()
        {
            InspectDivCd = InspectDivCd.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT '' AS Status
                             , mc.MoldCode
							 , mc.Seq
							 , MS.DispSeq
							 , mc.InspectDate
							 , mc.ChkTimeDivCd
							 , mc.InspectDivCd
							 , ManagementItem
							 , CheckMethod
							 , StandardSpec
							 , ValueTypeDivCd
							 , mc.InspectCheck
							 --, CASE WHEN mc.InspectCheck = '10' THEN '○'
							 --       WHEN mc.InspectCheck = '20' THEN '△'
                             --       WHEN mc.InspectCheck = '30' THEN '□'
							 --		  ELSE ''
							 --  END InspectCheck
                             , CycleDivCd
                             , WeekDivCd

							 , mc.ChargeEmpNo
							 , mc.ActionResultDivCd
							 , mc.ActionDivCd
							 , mc.ActionMemo
							 , mc.ActionEmpNo

							 , MS.InspectMin
							 , MS.InspectMax
							 , mc.Remark
                             , mc.InspectShot
							 , md.MoldSN
							 , md.MoldName
							 , md.Chasu
							 , md.ProdtCust
							 , md.SupplyCust
                        FROM MD_MoldChk MC
                        INNER JOIN MD_MoldChk_Std MS ON MC.CorpId = MS.CorpId AND MC.MoldCode = MS.MoldCode AND MC.Seq = MS.Seq
                        INNER JOIN MD_Mold MD ON MC.CorpId = MD.CorpId AND MC.MoldCode = MD.MoldCode
                        WHERE mc.CorpId = '{WrGlobal.CorpID}' 
                         AND mc.InspectDate = '{InspectDate}'
                      ";

            if (MoldCode != "ALL")
            {
                query += $"\n AND mc.MoldCode = '{MoldCode}' ";
            }

            if (InspectDivCd != "")
            {
                query += $"\n AND mc.InspectDivCd = '{InspectDivCd}'";
            }

            if (InspectDivCd == "10")
            {
                query += $"\n AND mc.ChkTimeDivCd = '{ChkTimeDivCd}'";
            }

            if (InspectDivCd == "20")
            {
                query += $"\n AND MS.WeekDivCd = '{WeekDivCd}'";
            }

            query += "\n ORDER BY MS.DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Sel_Mold_ChkView(string FromDate, string ToDate)
        {
            InspectDivCd = InspectDivCd.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT '' AS Status
                             , mc.MoldCode
                             , MD.MoldSN
                             , MD.Chasu
							 , MD.MoldName
							 , MD.MoldMgmtNo
							 , mc.Seq
							 , MS.DispSeq
							 , CONVERT(date, mc.InspectDate) AS InspectDate
							 , mc.ChkTimeDivCd
							 , mc.InspectDivCd
							 , ManagementItem
							 , CheckMethod
							 , StandardSpec
							 , ValueTypeDivCd
							 , mc.InspectCheck
							 --, CASE WHEN mc.InspectCheck = '10' THEN 'Y'
							 --         WHEN mc.InspectCheck = '20' THEN 'N'
							 --		  ELSE mc.InspectCheck
							 --  END InspectCheck
                             , CycleDivCd
                             , WeekDivCd

							 , mc.ChargeEmpNo
							 , mc.ActionResultDivCd
							 , mc.ActionDivCd
							 , mc.ActionMemo
							 , mc.ActionEmpNo

							 , MS.InspectMin
							 , MS.InspectMax
							 , mc.Remark
                             , mc.InspectShot
                        FROM MD_MoldChk MC
                        INNER JOIN MD_MoldChk_Std MS ON MC.CorpId = MS.CorpId AND MC.MoldCode = MS.MoldCode AND MC.Seq = MS.Seq
                        INNER JOIN MD_Mold MD ON MC.CorpId = MD.CorpId AND MC.MoldCode = MD.MoldCode
                        WHERE mc.CorpId = '{WrGlobal.CorpID}' 
                          AND mc.InspectDate BETWEEN '{FromDate}' AND '{ToDate}'
                      ";

            if (MoldCode != "ALL" && MoldCode != "")
            {
                query += $"\n AND mc.MoldCode = '{MoldCode}' ";
            }

            if (InspectDivCd != "")
            {
                query += $"\n AND mc.InspectDivCd = '{InspectDivCd}'";

                //if (InspectDivCd == "10")
                //{
                //    query += $"\n AND mc.ChkTimeDivCd = '{ChkTimeDivCd}'";
                //}
                //else if (InspectDivCd == "20")
                //{
                //    query += $"\n AND MS.WeekDivCd = '{WeekDivCd}'";
                //}
            }

            query += "\n ORDER BY mc.InspectDate, mc.ChkTimeDivCd, ms.DispSeq";

            return db.GetDataTable(query);
        }


        public DataRow Select_Mold_ChkImage()
        {
            //이미지 등록 확인
            query = $@"SELECT CONVERT(varchar(max), ChkImage) AS ChkImage
                        FROM MD_MoldChk
						WHERE CorpId = '{WrGlobal.CorpID}' 
						  AND InspectDivCd = '{InspectDivCd}'
					      AND InspectDate = '{InspectDate}'
						  AND MoldCode = '{MoldCode}'
						  AND Seq = {Seq}
						  AND ChkTimeDivCd = '{ChkTimeDivCd}'
                      ";

            query += $"\nORDER BY MoldCode, Seq DESC";

            return db.GetDataRow(query);
        }

        public DataTable Select_DGPTime_Temp(string fromDate, string toDate)
        {
            ItemCode = ItemCode.ConvertString_Null_To_Empty();
            MachineCode = MachineCode.ConvertString_Null_To_Empty();

            query = "";
            query += $@"
                    SELECT	
	                      mc.DgpName
	                    , CONVERT(date, mt.DgpTime) AS DgpDate
	                    , CONVERT(time, mt.DgpTime) AS DgpTime
	                    , mc.Gauge_Type
	                    , CASE WHEN mt.DgpCode like 'T%' THEN mt.Gathering_Count * 0.1
						       ELSE  mt.Gathering_Count End AS Gathering_Count
                        , bm.Machine_Nm

                    FROM MTDGPTime mt
                    LEFT JOIN MTDGPCode mc ON mt.DgpCode = mc.DgpCode
                    LEFT JOIN EQ_Machine bm ON bm.MachineCode = mc.Equip_Name
                    WHERE mt.DgpTime between '{fromDate} 00:00:00' and '{toDate} 23:59:59'
                          AND mc.Equip_Name != ''  --현재값만 보도록 설정
                   
                ";
            if (ItemCode != "")
            {
                query += $"\n AND mc.Gauge_Type = '{ItemCode}'";
            }
            if (MachineCode != "")
            {
                query += $"\n AND mc.DgpCode = '{MachineCode}'";
            }

            query += $"\n  ORDER by DgpTime DESC, mc.Equip_Name ";

            return db.GetDataTable(query);
        }

        public DataTable Select_DGPTime_Temp_Chart(string fromDate, string toDate)
        {
            ItemCode = ItemCode.ConvertString_Null_To_Empty();
            MachineCode = MachineCode.ConvertString_Null_To_Empty();

            query = "";
            query += $@"
                    SELECT	
	                      mc.DgpName
	                    , CONVERT(date, mt.DgpTime) AS DgpDate
                        --날짜, 시간
                        , SUBSTRING(CONVERT(varchar, CONVERT(datetime, mt.DgpTime), 120),6,11)  AS DgpTime 
                        --시간만
	                    --, CONVERT(varchar(5), CONVERT(datetime, mt.DgpTime), 108)  AS DgpTime  
	                    , mc.Gauge_Type
	                    , CASE WHEN mt.DgpCode like 'T%' THEN mt.Gathering_Count * 0.1
						       ELSE  mt.Gathering_Count End AS Gathering_Count

                    FROM MTDGPTime mt
                    LEFT JOIN MTDGPCode mc ON mt.DgpCode = mc.DgpCode
					LEFT JOIN EQ_Machine bm ON bm.MachineCode = mc.Equip_Name
                    WHERE mt.DgpTime between '{fromDate} 00:00:00' and '{toDate} 23:59:59'
                          AND mc.Equip_Name = '{MachineCode}'                   
                ";
            query += $"\n  ORDER by  mt.DgpTime   ";

            return db.GetDataTable(query);
        }

        public DataTable Select_DGPTime_Temp_ChartSeries(string fromDate, string toDate)
        {
            ItemCode = ItemCode.ConvertString_Null_To_Empty();
            MachineCode = MachineCode.ConvertString_Null_To_Empty();

            query = "";
            query += $@"
                    SELECT	 DISTINCT
	                      mc.DgpName

                    FROM MTDGPTime mt
                    LEFT JOIN MTDGPCode mc ON mt.DgpCode = mc.DgpCode
					LEFT JOIN EQ_Machine bm ON bm.MachineCode = mc.Equip_Name
                    WHERE mt.DgpTime between '{fromDate} 00:00:00' and '{toDate} 23:59:59'
                          AND mc.Equip_Name = '{MachineCode}'
                ";

            return db.GetDataTable(query);
        }

        public DataTable Sel_Bad_Total(string fromDate, string toDate)
        {
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"
                        SELECT 
							  WorkDate
                           -- , SUBSTRING(WorkDate,5,8) AS strDate
                            , SubString(CONVERT(Varchar, Convert(date, WorkDate)), 6,10) AS strDate
                            , SUM(WORK_QTY) AS 생산수량
                            , SUM(WORK_QTY) - SUM(BAD_QTY) AS GOOD_QTY
                            , SUM(BAD_QTY)  AS 불량수량
                        FROM PM_OrderResult A
                        WHERE A.CorpId = '{WrGlobal.CorpID}'
                        AND WorkDate BETWEEN '{fromDate}'  and '{toDate}'
                        AND MachineCode = {MachineCode}
                        AND End_Time IS NOT NULL 
                        GROUP BY WorkDate
            ";


            return db.GetDataTable(query);
        }

        public DataTable Select_Mold_ChkView_upd(DateTime date)
        {
            MoldCode = MoldCode == "ALL" ? "" : MoldCode;
            //설비일상점검 조회
            string day_Column = ""; // 1~31일까지
            //for (int i = 1; i <= 31; i++)
            //{
            //    day_Column += $@", CASE WHEN(SELECT TOP 1 InspectCheck FROM MD_MoldChk mc WHERE CorpId = '{WrGlobal.CorpId}'  AND mc.ChkTimeDivCd = '{ChkTimeDivCd}' 
			//	AND InspectDate = '{date.ToString("yyyyMM") + i.ToString("D2")}' AND MoldCode = mc.MoldCode AND mc.Seq = mcs.Seq) = 'Y' THEN '○'
            //    WHEN(SELECT TOP 1 InspectCheck FROM MD_MoldChk mc WHERE CorpId = '{WrGlobal.CorpId}'  AND mc.ChkTimeDivCd = '{ChkTimeDivCd}'
			//	AND InspectDate = '{date.ToString("yyyyMM") + i.ToString("D2")}' AND MoldCode = mc.MoldCode AND mc.Seq = mcs.Seq) = 'C' THEN '△'
            //    WHEN(SELECT TOP 1 InspectCheck FROM MD_MoldChk mc WHERE CorpId = '{WrGlobal.CorpId}'  AND mc.ChkTimeDivCd = '{ChkTimeDivCd}'
			//	AND InspectDate = '{date.ToString("yyyyMM") + i.ToString("D2")}' AND MoldCode = mc.MoldCode AND mc.Seq = mcs.Seq) = 'R' THEN '□'
            //    ELSE '' END AS '" + i.ToString("D2") + "일'";
            //}

            int lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 1; i <= lastDay; i++)
            {
                string day = date.ToString("yyyy-MM-") + i.ToString("D2");

                day_Column += $@",
                                   MAX(CASE 
                                       WHEN mc.InspectDate = '{day}' AND mc.InspectCheck = '10' THEN '○'
                                       WHEN mc.InspectDate = '{day}' AND mc.InspectCheck = '20' THEN '△'
                                       WHEN mc.InspectDate = '{day}' AND mc.InspectCheck = '30' THEN '□'
                                       ELSE ''
                                   END) AS [{i.ToString("D2")}일]";
            }

            query = $@" SELECT 
							mcs.MoldCode
							, mcs.MoldCode AS MoldName
							, mcs.Seq
							, mcs.InspectDivCd
							, mcs.ManagementItem
							, mcs.CheckMethod
							, mcs.StandardSpec
							--, mcs.ValueTypeDivCd

                            , bm.MoldSN

                            , bm.SupplyCust
                            , bm.ProdtCust
							, bm.Chasu

                            , bcS.CustName AS Supply_CustName
                            , mcs.Remark
							";

            query += day_Column;

            query += $@" FROM MD_MoldChk_Std mcs
						LEFT JOIN MD_MoldChk mc  
						 ON  mc.CorpId = mcs.CorpId AND mc.MoldCode = mcs.MoldCode AND mc.Seq = mcs.Seq AND mc.CorpId = '{WrGlobal.CorpID}' AND mc.ChkTimeDivCd = '{ChkTimeDivCd}'
                        LEFT JOIN MD_Mold bm ON mcs.MoldCode = bm.MoldCode
                        LEFT JOIN CM_Cust bcS ON bm.CorpId = bcS.CorpId AND bcS.CustCode = bm.SupplyCust
                        LEFT JOIN CM_Cust bcP on bm.CorpId = bcP.CorpId AND bcP.CustCode = bm.ProdtCust
						WHERE mcs.CorpId = '{WrGlobal.CorpID}'
						AND mcs.InspectDivCd = '10' 
						--AND mc.InspectDate BETWEEN '{date.ToString("yyyyMM" + "01")}' AND '{date.ToString("yyyyMM" + (DateTime.DaysInMonth(date.Year, date.Month)).ToString("D2"))}'

";

            if (MoldCode != null && MoldCode != "")
            {
                query += $"\nAND mcs.MoldCode = '{MoldCode}'";
            }

            query += $@"GROUP BY 
                                 mcs.MoldCode,
                                 mcs.Seq,
                                 mcs.InspectDivCd,
                                 mcs.ManagementItem,
                                 mcs.CheckMethod,
                                 mcs.StandardSpec,
                                 bm.MoldSN,
                                 bm.SupplyCust,
                                 bm.ProdtCust,
                                 bm.Chasu,
                                 bcS.CustName,
                                 mcs.Remark";

            return db.GetDataTable(query);
        }

        public DataTable Select_Mold_ChkView_Ins(string Month)
        {
            //설비일상점검 조회

            string day_Column = ""; // 1~31일까지
            for (int i = 1; i <= 31; i++)
            {
                day_Column += $", '' AS '" + i.ToString("D2") + "일'";
            }

            query = $@" SELECT 
							'{MoldCode}' AS MoldCode
							, '{MoldCode}' AS MoldName
							, mcs.Seq
							, InspectDivCd
							, mcs.ManagementItem
							, mcs.CheckMethod
							, mcs.StandardSpec							
							--, mcs.ValueTypeDivCd

                            , bm.MoldSN

                            , bm.SupplyCust
                            , bcS.CustName AS Supply_CustName

                            , bm.ProdtCust
                            , bcP.CustName AS Prodt_CustName
							";

            query += day_Column;

            query += $@" FROM MD_MoldChk_Std mcs
                        LEFT JOIN MD_Mold bm ON mcs.MoldCode = bm.MoldCode
                        LEFT JOIN CM_Cust bcS ON bm.CorpId = bcS.CorpId AND bcS.CustCode = bm.SupplyCust
                        LEFT JOIN CM_Cust bcP on bm.CorpId = bcP.CorpId AND bcP.CustCode = bm.ProdtCust
						WHERE mcs.CorpId = '{WrGlobal.CorpID}' AND mcs.MoldCode = '{MoldCode}'  AND InspectDivCd = '{InspectDivCd}'	";

            return db.GetDataTable(query);
        }

        public bool Ins_Mold_Chk()
        {
            query = $@"
                        INSERT INTO MD_MoldChk
                        (
                            CorpId
                            , MoldCode
                            , Seq
                            , InspectDate
                            , ChkTimeDivCd
                            , InspectDivCd
                            , InspectCheck
                            , ChargeEmpNo
                            , ActionResultDivCd
                            , ActionDivCd
                            , ActionMemo
                            , ActionEmpNo
                            , ChkImage
                            , Remark
                            , InspectShot
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{MoldCode}'
                            , {Seq}
                            , '{InspectDate}'
                            , '{ChkTimeDivCd}'
                            , {InspectDivCd.ConvertStr_Empty_To_Null()}
                            , {InspectCheck.ConvertStr_Empty_To_Null()}
                            , {ChargeEmpNo.ConvertStr_Empty_To_Null()}
                            , {ActionResultDivCd.ConvertStr_Empty_To_Null()}
                            , {ActionDivCd.ConvertStr_Empty_To_Null()}
                            , {ActionMemo.ConvertStr_Empty_To_Null()}
                            , {ActionEmpNo.ConvertStr_Empty_To_Null()}
                            , @Pic
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , (select sum(a.Shot)
                                from
	                                (select MoldCode, shot
	                                from PM_OrderResult
	                                where CorpId = '{WrGlobal.CorpID}' AND WorkDate < '{InspectDate}'

                                    union all

	                                select MoldCode, InitShot as Shot
	                                from MD_Mold
                                    WHERE CorpId = '{WrGlobal.CorpID}' 
	                                ) a
                                where a.MoldCode = '{MoldCode}'
                                group by a.MoldCode)
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                        
                       ";

            return db.ExecuteQueryImage(query, ChkImage);
        }

        public bool Upd_Mold_Chk()
        {
            query = $@"
                        UPDATE MD_MoldChk
                        SET  InspectDivCd = {InspectDivCd.ConvertStr_Empty_To_Null()}
                           , InspectCheck = {InspectCheck.ConvertStr_Empty_To_Null()}
                           , ChargeEmpNo = {ChargeEmpNo.ConvertStr_Empty_To_Null()}
                           , ActionResultDivCd = {ActionResultDivCd.ConvertStr_Empty_To_Null()}
                           , ActionDivCd = {ActionDivCd.ConvertStr_Empty_To_Null()}
                           , ActionMemo = {ActionMemo.ConvertStr_Empty_To_Null()}
                           , ActionEmpNo = {ActionEmpNo.ConvertStr_Empty_To_Null()}
                          -- , '{ChkImage}'
                           , Remark = {Remark.ConvertStr_Empty_To_Null()}
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND Seq = '{Seq}'
                          AND InspectDate = '{InspectDate}'
                          AND ChkTimeDivCd = '{ChkTimeDivCd}'
                    
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_Mold_Chk()
        {
            query = $@"
                        DELETE MD_MoldChk
                        WHERE CorpId = '{WrGlobal.CorpID}'
                         AND Seq = '{Seq}'
                         AND InspectDate = '{InspectDate}'
                         AND ChkTimeDivCd = '{ChkTimeDivCd}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_InspectResult()
        {
            query = $@"DELETE MD_MoldChk
                        WHERE CorpId = '{WrGlobal.CorpID}'
						  AND MoldCode = '{MoldCode}'
						  AND Seq = '{Seq}' ";

            return db.ExecuteQuery(query);
        }

        public bool Del_Mold_Chk_ByMoldCode()
        {
            query = $@"
                        DELETE MD_MoldChk
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND InspectDate = '{InspectDate}'
                        AND ChkTimeDivCd = '{ChkTimeDivCd}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
