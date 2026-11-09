using SmartCommon.Common;
using SmartMES.Common;
using System.Data;
using System.Linq;

namespace SmartMES.Models.MD
{
    public class MD_MoldRepair
    {
        #region Property

        public string MoldCode { get; set; }
        public string RepairNo { get; set; }
        public string Seq { get; set; }
        public string DispSeq { get; set; }
        public string RepairDate { get; set; }
        public string RepairCause { get; set; }
        public string RepairState { get; set; }
        public string RepairEmp { get; set; }
        public string RepairStartTime { get; set; }
        public string RepairEndTime { get; set; }
        public int RepairTime { get; set; }
        public string RepairCust { get; set; }
        public decimal RepairAmt { get; set; }
        public string RepairDiv { get; set; }
        public string Remark { get; set; }

        public string FocusMoldCd { get; set; }
        public string rstFrom { get; set; }
        public string rstTo { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MD_MoldRepair()
        {
            query = $@"
                        SELECT
                            MRL.CorpId
                            , MRL.MoldCode
                            , CM.MoldName
                            , MRL.Seq
                            , MRL.DispSeq
                            , CONVERT(CHAR(10), CONVERT(DATE, MRL.RepairDate, 112), 23) RepairDate
                            , MRL.RepairCause
                            , MRL.RepairState
                            , MRL.RepairEmp
                            , CONVERT(CHAR(16), MRL.RepairStartTime, 120) RepairStartTime
                            , CONVERT(CHAR(16), MRL.RepairEndTime, 120) RepairEndTime
                            , MRL.RepairTime
                            , MRL.RepairCust
                            , MRL.RepairAmt
                            , MRL.Remark
                            , MRL.CreId
                            , MRL.CreIP
                            , MRL.CreDt
                            , MRL.ModId
                            , MRL.ModIP
                            , MRL.ModDt
                        FROM MD_MoldRepair MRL
                        INNER JOIN MD_Mold CM ON MRL.CorpId = CM.CorpId AND MRL.MoldCode = CM.MoldCode
                        WHERE MRL.CorpId = '{WrGlobal.CorpID}'
                        AND MRL.RepairDate = '{RepairDate}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_MD_MoldRepairView(string startDate, string endDate)
        {
            query = $@"
                        SELECT
                            MRL.CorpId
                            , MRL.MoldCode
                            , CM.MoldName
                            , MRL.Seq
                            , MRL.DispSeq
                            , CONVERT(CHAR(10), CONVERT(DATE, MRL.RepairDate, 112), 23) RepairDate
                            , MRL.RepairCause
                            , MRL.RepairState
                            , MRL.RepairEmp
                            , CONVERT(CHAR(16), MRL.RepairStartTime, 120) RepairStartTime
                            , CONVERT(CHAR(16), MRL.RepairEndTime, 120) RepairEndTime
                            , MRL.RepairTime
                            , MRL.RepairCust
                            , MRL.RepairAmt
                            , MRL.Remark
                            , MRL.CreId
                            , MRL.CreIP
                            , MRL.CreDt
                            , MRL.ModId
                            , MRL.ModIP
                            , MRL.ModDt
                        FROM MD_MoldRepair MRL
                        INNER JOIN MD_Mold CM ON MRL.CorpId = CM.CorpId AND MRL.MoldCode = CM.MoldCode
                        WHERE MRL.CorpId = '{WrGlobal.CorpID}'
                        AND RepairDate BETWEEN '{startDate}' AND '{endDate}'
                    ";

            return db.GetDataTable(query);
        }

        public DataRow Select_MD_MoldRepair_ByCode()
        {
            query = $@"
                        SELECT
                            CorpId
                            , MoldCode
                            , Seq
                            , DispSeq
                            , CONVERT(CHAR(10), CONVERT(DATE, RepairDate, 112), 23) RepairDate
                            , RepairCause
                            , RepairState
                            , RepairEmp
                            , RepairStartTime
                            , RepairEndTime
                            , RepairTime
                            , RepairCust
                            , RepairAmt
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM MD_MoldRepair
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND Seq = {Seq}
                    ";

            return db.GetDataRow(query);
        }

        public DataTable Search_Repair(string[] repairdiv)
        {
            //설비관리 설비수리이력 호출에 사용되는 Query
            FocusMoldCd = FocusMoldCd.ConvertStr_Empty_To_Null();
            MoldCode = MoldCode.ConvertStr_Empty_To_Null();

            query = $@"SELECT '' AS Status 
                    , mr.DispSeq
                    , mr.RepairDiv
                    , mr.RepairNo
                    , mr.Seq
                    , mc.MoldCode
		            , mc.MoldMgmtNo
                    , mc.MoldSN
                    , mc.Chasu
                    , mc.MoldPjtNo
		            , mc.MoldCode MoldName
		            , Convert(Date,RepairDate) AS RepairDate
		            , RePairCause
		            , RepairState
		            , RepairEmp
		            , RepairStartTime
		            , RepairEndTime
                    , RepairTime
		            , RepairCust
		            , RepairAmt

                    , mc.MoldImage1
                    , mc.MoldImage2
                    , mr.RepairShot
		            , mr.CreId
		            , mr.CreDt
		            , mr.ModId
		            , mr.ModDt
                    , mc.SupplyCust
                    , mc.ProdtCust
                    , ISNULL(pd.Shot, 0) AS AccumulateShot
                    , mc.SpecialParts

                    FROM MD_MoldRepair mr
                    LEFT JOIN MD_Mold mc ON mr.CorpID = mc.CorpID AND mc.MoldCode = mr.MoldCode
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

									) A
								GROUP BY A.MoldCode
								) pd ON pd.MoldCode = mc.MoldCode
                    WHERE mr.CorpID    = '{WrGlobal.CorpID}'
                      AND mr.RepairDate BETWEEN '{rstFrom}' AND '{rstTo}'";
            if (FocusMoldCd != "NULL")
            {
                //query += $@" and mc.MoldCode    = {FocusMoldCd}";
                query += $@" and mc.MoldSN    = {FocusMoldCd}";
            }
            if (MoldCode != "NULL")
            {
                //query += $@" and mc.MoldCode    = {MoldCode}";
                query += $@" and mc.MoldCode    = {MoldCode}";
            }
            if (repairdiv != null && repairdiv.Length > 0)
            {
                var inClause = string.Join(",", repairdiv.Select(x => $"'{x}'"));
                query += $@" and mr.RepairDiv in ({inClause})";
            }
            query += $@"    ORDER BY RepairDate";

            return db.GetDataTable(query);
        }

        public bool Ins_MD_MoldRepair()
        {
            query = $@"
                        INSERT INTO MD_MoldRepair
                        (
                            CorpId
                            , MoldCode
                            , Seq
                            , DispSeq
                            , RepairDate
                            , RepairCause
                            , RepairState
                            , RepairEmp
                            , RepairStartTime
                            , RepairEndTime
                            , RepairTime
                            , RepairCust
                            , RepairAmt
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{MoldCode}'
                            , {Seq}
                            , '{DispSeq}'
                            , '{RepairDate}'
                            , '{RepairCause}'
                            , '{RepairState}'
                            , '{RepairEmp}'
                            , '{RepairStartTime}'
                            , '{RepairEndTime}'
                            , '{RepairTime}'
                            , '{RepairCust}'
                            , '{RepairAmt}'
                            , '{Remark}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Ins_Repair()
        {
            //공정관리(Frm_EQ_Machine) 화면 등록 Query
            db.Edit_Property(this);

            query = $@"INSERT INTO MD_MoldRepair (
                                CorpID
                                , RepairNo
                                , RepairDiv
                                , DispSeq
                                , MoldCode
                                , RepairDate
                                , RepairCause
                                , RepairState
                                , RepairEmp
                                , RepairStartTime
                                , RepairEndTime
                                , RepairTime
                                , RepairCust
                                , RepairAmt
                                , RepairShot
                                --, Rmk
                                , CreId
                                , CreIP
                                , CreDt)
                            VALUES( 
                                '{WrGlobal.CorpID}',
                                '{RepairNo}',
                                '{RepairDiv}',
                                '{DispSeq}',
                                '{MoldCode}',
                                '{RepairDate}',
                                '{RepairCause}',
                                '{RepairState}',
                                '{RepairEmp}',
                                {RepairStartTime.ConvertStr_Empty_To_Null()},
                                {RepairEndTime.ConvertStr_Empty_To_Null()},
                                 {RepairTime.ConvertInt_Empty_To_Null()},
                                '{RepairCust}',
                                '{RepairAmt}',
                                (select sum(a.Shot)
                                from
	                                (select MoldCode, shot
	                                from PM_OrderResult
	                                where WorkDate < '{RepairDate}'

                                    union all

	                                select MoldCode, ISNULL(InitShot, 0) as Shot
	                                from MD_Mold
	                                ) a
                                where a.MoldCode = '{MoldCode}'
                                group by a.MoldCode), 
                                '{WrGlobal.LoginID}',
                                '{WrGlobal.ClientHostName}',
                                GETDATE())";

            return db.ExecuteQuery(query);
        }


        public bool Upd_MD_MoldRepair()
        {
            query = $@"
                        UPDATE MD_MoldRepair
                        SET
                            RepairDate = '{RepairDate}'
                            , RepairCause = '{RepairCause}'
                            , RepairState = '{RepairState}'
                            , RepairEmp = '{RepairEmp}'
                            , RepairStartTime = '{RepairStartTime}'
                            , RepairEndTime = '{RepairEndTime}'
                            , RepairTime = '{RepairTime}'
                            , RepairCust = '{RepairCust}'
                            , RepairAmt = '{RepairAmt}'
                            , Remark = '{Remark}'
                            , ModId = '{WrGlobal.LoginID}' 
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND Seq = {Seq}
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Repair()
        {
            //공정관리(Frm_EQ_Machine) 화면 수정 Query
            db.Edit_Property(this);

            query = $@"UPDATE MD_MoldRepair 
                          SET 
                              MoldCode = '{MoldCode}',
                              DispSeq = '{DispSeq}',
                              RepairDiv = '{RepairDiv}',
                              RepairDate = '{RepairDate}',
                              RepairCause = '{RepairCause}',
                              RepairState = '{RepairState}',
                              RepairEmp = '{RepairEmp}',
                              RepairStartTime = '{RepairStartTime}',
                              RepairEndTime = '{RepairEndTime}',
                              RepairTime = {RepairTime.ConvertInt_Empty_To_Null()},
                              RepairCust = '{RepairCust}',
                              RepairAmt = '{RepairAmt}',
                              RepairShot = 
                                            (select sum(a.Shot)
                                            from
	                                            (select MoldCode, shot
	                                            from PM_OrderResult
	                                            where WorkDate < '{RepairDate}'

                                                union all

	                                            select MoldCode, InitShot as Shot
	                                            from MD_Mold
	                                            ) a
                                            where a.MoldCode = '{MoldCode}'
                                            group by a.MoldCode), 
                              ModId = '{WrGlobal.LoginID}', 
                              ModIP = '{WrGlobal.ClientHostName}',
                              ModDt = GETDATE()
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND RepairNo = '{RepairNo}'
                          --AND MoldCode = '{MoldCode}'  
                          --AND     Seq = '{Seq}'
                            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MD_MoldRepair()
        {
            query = $@"
                        DELETE MD_MoldRepair WHERE CorpId = '{WrGlobal.CorpID}' AND MoldCode = '{MoldCode}' AND Seq = {Seq}
                    ";

            return db.ExecuteQuery(query);
        }

        public DataRow Get_LastMoldRepairSeq()
        {
            query = $@"
                        SELECT ISNULL(MAX(Seq), 0) + 1 Seq FROM MD_MoldRepair WHERE CorpId = '{WrGlobal.CorpID}' AND MoldCode = '{MoldCode}'
                    ";

            return db.GetDataRow(query);
        }
    }
}
