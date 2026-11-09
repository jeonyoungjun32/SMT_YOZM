using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Model.EQ
{
	class EQ_MachineChkStd
    {
        #region Property

        public string CorpId { get; set; }

        public string MachineCode { get; set; }

        public int Seq { get; set; }

        public decimal? DispSeq { get; set; }

        public string InspectDivCd { get; set; }

        public string ManagementItem { get; set; }

        public string CheckMethod { get; set; }

        public string StandardSpec { get; set; }

        public string CycleDivCd { get; set; }

        public string WeekDivCd { get; set; }

        public string ValueTypeDivCd { get; set; }

        public string StartDate { get; set; }

        public string LastDate { get; set; }

        public string ExpectDate { get; set; }

        public byte[] ChkImage { get; set; }

        public string InspectOk { get; set; }

        public decimal InspectMin { get; set; }

        public decimal InspectMax { get; set; }

        public string Remark { get; set; }


        #endregion Property End

        Database db = new Database();
        string query;

        public DataTable Select_Machine_Lup(bool allYn = false)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT'ALL'  코드 , '전체' 명칭
                                UNION ALL

                            ";
            }

            query += $@"SELECT MachineCode as 코드
                            , MachineName as 명칭 
                                                     
                         FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_Manage(bool all_Yn = false)
        {
            query = "";
            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT'ALL' AS 관리번호, '전체' AS 명칭
                        UNION ALL ";
            }

            query += $@"SELECT CONVERT(VARCHAR, Seq) as 관리번호
                            , ManagementItem as 명칭 
                                                     
                         FROM EQ_MachineChkStd
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }
        public DataTable Select_Machine_Chk_Std(string InspectDivCd, bool UseYn = true)
        {
            query = $@"SELECT '' AS Status
                              ,mcs.MachineCode
                              ,mcs.MachineCode AS MachineName
                              ,Seq
                              ,DispSeq
                              ,InspectDivCd
                              ,ManagementItem
                              ,CheckMethod
                              ,StandardSpec
                              ,CycleDivCd
                              ,WeekDivCd
                              ,ValueTypeDivCd
                              ,CASE WHEN mcs.StartDate = '' THEN NULL ELSE CONVERT(DATE, mcs.StartDate) END AS Start_Date
                              ,CASE WHEN mcs.LastDate = '' THEN NULL ELSE CONVERT(DATE, mcs.LastDate) END AS Last_Date
                              ,CASE WHEN mcs.ExpectDate = '' THEN NULL ELSE CONVERT(DATE, mcs.ExpectDate) END AS Expect_Date
                              ,ChkImage
                              ,InspectOk
                              ,InspectMin
	                          ,InspectMax
                              ,mcs.Remark
                              ,mcs.CreId
                              ,mcs.CreDt
                              ,mcs.ModId
                              ,mcs.ModDt
                         FROM EQ_MachineChkStd mcs
                            LEFT JOIN EQ_Machine m on mcs.CorpId = m.CorpId AND mcs.MachineCode = m.MachineCode
                        WHERE mcs.CorpId = '{WrGlobal.CorpID}'";

            if (InspectDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND InspectDivCd = {InspectDivCd.ConvertStr_Empty_To_Null()}";
            }

            query += $"ORDER BY MachineCode, DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_Machine_Chk_Std_byCd()
        {
            query = $@"
                        SELECT '' AS Status
                              ,mcs.MachineCode
                              ,mcs.MachineCode AS Machine_Nm
                              ,mcs.Seq
                              ,mcs.DispSeq
                              ,mcs.InspectDivCd
                              ,mcs.ManagementItem
                              ,mcs.CheckMethod
                              ,mcs.StandardSpec
                              ,mcs.CycleDivCd
                              ,mcs.WeekDivCd
                              ,mcs.ValueTypeDivCd
                              ,mcs.ChkImage
                              ,mcs.InspectOk
                              ,mcs.InspectMin
	                          ,mcs.InspectMax
                              ,mcs.Remark
                              ,mcs.CreId
                              ,mcs.CreDt
                              ,mcs.ModId
                         FROM EQ_MachineChkStd mcs
                            LEFT JOIN EQ_Machine m on mcs.CorpId = m.CorpId AND mcs.MachineCode = m.MachineCode
                         WHERE mcs.CorpId = '{WrGlobal.CorpID}'
                           AND InspectDivCd = '{InspectDivCd}'
                           AND mcs.MachineCode = '{MachineCode}'
                         ORDER BY MachineCode, DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_Machine_Chk_List()
        {
            query = $@"SELECT  '신규' AS Status
                            , mcs.MachineCode
							 , m.MachineName Machine_Nm
                             , Seq
                             , DispSeq
                             , NULL AS InspectDate
                             , ''AS ChkTimeDivCd
                             , InspectDivCd
                             , ManagementItem
                             , CheckMethod
                             , StandardSpec
                             , ValueTypeDivCd
                             --, InspectOk AS InspectCheck
							 , CASE WHEN InspectOk = 'Y' THEN '10'
							          WHEN InspectOk = 'N' THEN '20'
									  ELSE InspectOk
							   END InspectCheck
                             , CycleDivCd
                             , WeekDivCd

							 , '' AS ChargeEmpNo
							 , '' AS ActionResultDivCd
							 , '' AS ActionDivCd
							 , '' AS ActionMemo
							 , '' AS ActionEmpNo
                             
                             --, ChkImage

                             , InspectMin
	                         , InspectMax
                             , mcs.Remark

                         FROM EQ_MachineChkStd mcs
                    LEFT JOIN EQ_Machine m on mcs.MachineCode = m.MachineCode
                        WHERE mcs.CorpId = '{WrGlobal.CorpID}'
                          AND mcs.InspectDivCd = '{InspectDivCd}'
                          AND mcs.MachineCode = '{MachineCode}'  
                            ";


            if (InspectDivCd == "20" && WeekDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND(mcs.WeekDivCd = '' OR mcs.WeekDivCd = '{WeekDivCd}')";
            }

            query += "\n ORDER BY mcs.DispSeq";

            return db.GetDataTable(query);
        }

        public DataRow Select_Machine_Chk_Std_DispSeq()
        {
            query = $@"
                        SELECT TOP 1 DispSeq
                        FROM EQ_MachineChkStd
                        WHERE MachineCode = '{MachineCode}'
                        AND InspectDivCd = '{InspectDivCd}'
                        ORDER BY DispSeq DESC
                    ";

            return db.GetDataRow(query);
        }

        public bool Ins_Machine()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO EQ_MachineChkStd(CorpId, MachineCode, DispSeq, InspectDivCd, ManagementItem, CheckMethod, StandardSpec,
                                                CycleDivCd, WeekDivCd, ValueTypeDivCd, InspectOk, InspectMin, InspectMax, Remark,
                                                CreId, CreIP, CreDt
                                                )
                                        VALUES ('{WrGlobal.CorpID}'
                                                , '{MachineCode}'
                                                , '{DispSeq}'
                                                , '{InspectDivCd}'
                                                , '{ManagementItem}'
                                                , '{CheckMethod}'
                                                , '{StandardSpec}'
                                                , '{CycleDivCd}'
                                                , '{WeekDivCd}'
                                                , '{ValueTypeDivCd}'
                                                , '{InspectOk}'
                                                , '{InspectMin}'
                                                , '{InspectMax}'
                                                , '{Remark}'
                                                ,'{WrGlobal.LoginID}'
                                                , '{WrGlobal.ClientIpAddress}'
                                                ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Machine()
        {
            // InspectDivCd
            db.Edit_Property(this);

            query = $@"UPDATE EQ_MachineChkStd
                          SET 
                             MachineCode =            '{MachineCode}'
                            , DispSeq  =              '{DispSeq}'
                            , InspectDivCd =         '{InspectDivCd}'
                            , ManagementItem =        '{ManagementItem}'
                            , CheckMethod =           '{CheckMethod}'
                            , StandardSpec =          '{StandardSpec}'
                            , CycleDivCd =           '{CycleDivCd}'
                            , WeekDivCd =            '{WeekDivCd}'
                            , ValueTypeDivCd =       '{ValueTypeDivCd}'
                            , InspectOk =             '{InspectOk}'
                            , InspectMin =            '{InspectMin}'
                            , InspectMax =            '{InspectMax}'
                            , Remark =                    '{Remark}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()

                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND Seq = '{Seq}'
						  --AND DispSeq = '{DispSeq}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_Machine()
        {
            query = $@"DELETE EQ_MachineChkStd
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MachineCode = '{MachineCode}'
                          AND Seq = '{Seq}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_InspectResult()
        {
            query = $@"DELETE EQ_MachineChk
                        WHERE CorpId = '{WrGlobal.CorpID}'
						  AND MachineCode = '{MachineCode}'
						  AND Seq = '{Seq}' ";

            return db.ExecuteQuery(query);
        }

        public bool Ins_MachineRepairStd()
        {
            query = $@"
                        INSERT INTO EQ_MachineChkStd
                        (
                            CorpId
                            , MachineCode
                            , DispSeq
                            , InspectDivCd
                            , CheckMethod
                            , Remark
                            , CreId
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{MachineCode}'
                            , {DispSeq}
                            , '{InspectDivCd}'
                            , '{CheckMethod}'
                            , '{Remark}'
                            , '{WrGlobal.LoginID}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_MachineRepairStd()
        {
            query = $@"
                        UPDATE EQ_MachineChkStd
                        SET 
                            MachineCode =               '{MachineCode}'
                            , DispSeq  =              '{DispSeq}'
                            , InspectDivCd =         '{InspectDivCd}'
                            , CheckMethod =           '{CheckMethod}'
                            , Remark =                    '{Remark}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND Seq = {Seq}
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MachineRepairStd()
        {
            query = $@"
                        DELETE EQ_MachineChkStd
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MachineCode = '{MachineCode}'
                        AND Seq = {Seq}
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
