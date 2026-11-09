using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.EQ
{
	class EQ_MachineChk
    {
        #region Property

        public string MachineCode { get; set; }
        public int Seq { get; set; }
        public string InspectDate { get; set; }
        public string ChkTimeDivCd { get; set; }
        public string InspectDivCd { get; set; }
        public string InspectCheck { get; set; }
        public string ChargeEmpNo { get; set; }
        public string ActionResultDivCd { get; set; }
        public string ActionDivCd { get; set; }
        public string ActionMemo { get; set; }
        public string ActionEmpNo { get; set; }
        public string ChkImage { get; set; }
        public string Remark { get; set; }


		//
        public string WeekDivCd { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }



		#endregion Property End

		Database db = new Database();
        string query;

		public DataTable Select_InspectResult()
		{
			//설비점검(Frm_EQ_MachineChk)에서 사용하는 Query
			query = $@" SELECT mc.MachineCode
							 , m.MachineName MachineName
							 , mc.Seq
							 , mcs.DispSeq
							 , mc.InspectDate
							 , mc.ChkTimeDivCd
							 , mc.InspectDivCd
							 , ManagementItem
							 , CheckMethod
							 , StandardSpec
							 , ValueTypeDivCd
							 --, mc.InspectCheck
							 , CASE WHEN mc.InspectCheck = 'Y' THEN '10'
							          WHEN mc.InspectCheck = 'N' THEN '20'
									  ELSE mc.InspectCheck
							   END InspectCheck
                             , CycleDivCd
                             , WeekDivCd

							 , mc.ChargeEmpNo
							 , mc.ActionResultDivCd
							 , mc.ActionDivCd
							 , mc.ActionMemo
							 , mc.ActionEmpNo

							 , mcs.InspectMin
							 , mcs.InspectMax
							 , mc.Remark

						FROM EQ_MachineChk mc
						LEFT JOIN EQ_MachineChkStd mcs ON (mcs.MachineCode = mc.MachineCode OR mcs.MachineCode = '') AND mcs.seq = mc.seq
						LEFT JOIN EQ_Machine m on mcs.MachineCode = m.MachineCode

						WHERE mc.CorpId = '{WrGlobal.CorpID}' 
						  AND mc.InspectDivCd = '{InspectDivCd}'
					      AND mc.InspectDate = '{InspectDate}'
						  AND mc.MachineCode = '{MachineCode}' ";

			if (InspectDivCd == "10")
			{
				query += $"\n AND mc.ChkTimeDivCd = '{ChkTimeDivCd}'";
			}

			if (InspectDivCd == "20" && WeekDivCd.ConvertString_Null_To_Empty() != "")
			{
				query += $"\n AND mcs.WeekDivCd = '{WeekDivCd}'";
			}

			query += "\n ORDER BY mcs.DispSeq";

			return db.GetDataTable(query);
		}

		public DataTable Select_Machine_ChkView_upd(string Month)
		{
			//설비일상점검 조회
			string day_Column = ""; // 1~31일까지
			for (int i = 1; i <= 31; i++)
			{
				 day_Column += $@", CASE WHEN(SELECT InspectCheck FROM EQ_MachineChk mc WHERE CorpId = '{WrGlobal.CorpID}'  AND mc.ChkTimeDivCd = '{ChkTimeDivCd}' AND MONTH(InspectDate) = "
					+ Month +
					" AND DAY(InspectDate) = "+ i + " AND MachineCode = mc.MachineCode AND mc.Seq = mcs.Seq) = 'Y' THEN 'O' ELSE '' END AS '" + i.ToString("D2") +"일'";
			}

			query = $@" SELECT DISTINCT
							mcs.MachineCode
							, mcs.MachineCode AS Machine_Nm
							, mcs.Seq
							, mcs.InspectDivCd
							, mcs.ManagementItem
							, mcs.CheckMethod
							, mcs.StandardSpec
							--, mcs.ValueTypeDivCd
							";

			query += day_Column;

			query += $@" FROM EQ_MachineChkStd mcs
						LEFT JOIN EQ_MachineChk mc  
						 ON  mc.CorpId = mcs.CorpId AND mc.MachineCode = mcs.MachineCode AND mc.Seq = mcs.Seq AND mc.CorpId = '{WrGlobal.CorpID}' AND mc.ChkTimeDivCd = '{ChkTimeDivCd}' AND MONTH(mc.InspectDate) = {Month} AND DAY(mc.InspectDate) BETWEEN 1 AND 31
						WHERE  mcs.InspectDivCd = '10' ";

			if (MachineCode != "NULL")
			{
				query += $"\nAND mcs.MachineCode = '{MachineCode}'";
			}

			return db.GetDataTable(query);
		}
		public DataTable Select_Machine_ChkView_Ins(string Month)
		{
			//설비일상점검 조회

			string day_Column = ""; // 1~31일까지
			for (int i = 1; i <= 31; i++)
			{
				day_Column += $", '' AS '" + i.ToString("D2") + "일'";
			}

			query = $@" SELECT 
							'{MachineCode}' AS MachineCode
							, '{MachineCode}' AS Machine_Nm
							, mcs.Seq
							, '10' AS InspectDivCd
							, mcs.ManagementItem
							, mcs.CheckMethod
							, mcs.StandardSpec							
							--, mcs.ValueTypeDivCd
							";

			query += day_Column;

			query += $@" FROM EQ_MachineChkStd mcs
						WHERE mcs.CorpId = '{WrGlobal.CorpID}' AND mcs.MachineCode = '{MachineCode}' 	";

			return db.GetDataTable(query);
		}

		public DataTable Select_Machine_ChkView(string date)
        {
			//설비일상점검, 정기점검현황 조회
			query = $@" EXEC [EQ_Machine_ChkView] '{WrGlobal.CorpID}', '{date}', '{InspectDivCd}', '{MachineCode}'	";

			return db.GetDataTable(query);
        }

		public DataRow Select_Machine_Chk_Image()
		{
			//이미지 등록 확인
			query = $@"SELECT CONVERT(varchar(max), Chk_Image) AS Chk_Image
                        FROM EQ_MachineChk
						WHERE CorpId = '{WrGlobal.CorpID}' 
						  AND InspectDivCd = '{InspectDivCd}'
					      AND InspectDate = '{InspectDate}'
						  AND MachineCode = '{MachineCode}'
						  AND Seq = {Seq}
						  AND ChkTimeDivCd = '{ChkTimeDivCd}'
                      ";

			query += $"\nORDER BY MachineCode, Seq DESC";

			return db.GetDataRow(query);
		}

		public DataTable EQ_MachineChkView(string date)
		{
			//설비일상점검, 정기점검현황 조회
			query = $@" EXEC [EQ_MachineChkView] '{WrGlobal.CorpID}', '{date}', '{InspectDivCd}', '{MachineCode}'	";

			return db.GetDataTable(query);
		}

		public bool Ins_InspectResult()
		{
			db.Edit_Property(this);

			query = $@"INSERT INTO EQ_MachineChk
						(
							  CorpId, MachineCode, Seq
							, InspectDate, ChkTimeDivCd, InspectDivCd
							, InspectCheck, ChargeEmpNo    
							, ActionResultDivCd, ActionDivCd, ActionMemo, ActionEmpNo
							--, Chk_Image
							, Remark
							, CreId,CreDt
						)
						VALUES
						(
							  '{WrGlobal.CorpID}', '{MachineCode}', {Seq}
							, '{InspectDate}', '{ChkTimeDivCd}', '{InspectDivCd}'
							, '{InspectCheck}', '{ChargeEmpNo}'
							, '{ActionResultDivCd}', '{ActionDivCd}', '{ActionMemo}', '{ActionEmpNo}'
							--, '{ChkImage}'
							, '{Remark}'
							, '{WrGlobal.LoginID}', GETDATE()
						)";

			return db.ExecuteQuery(query);
		}

		public bool Upd_InspectResult()
		{
			// ExamDivCd
			db.Edit_Property(this);

			query = $@"UPDATE EQ_MachineChk
						  SET InspectCheck = '{InspectCheck}'
						    , ChargeEmpNo = '{ChargeEmpNo}'
						    , ActionResultDivCd = '{ActionResultDivCd}'
						    , ActionDivCd = '{ActionDivCd}'
						    , ActionMemo = '{ActionMemo}'
						    , ActionEmpNo = '{ActionEmpNo}'
						    --, Chk_Image = '{ChkImage}'
						    , Remark = '{Remark}'
							, ModId = '{WrGlobal.LoginID}'
                            , ModDt = GETDATE()

						WHERE CorpId = '{WrGlobal.CorpID}' 
						  AND InspectDivCd = '{InspectDivCd}'
						  AND InspectDate = '{InspectDate}'
						  AND MachineCode = '{MachineCode}'
						  AND Seq = {Seq}
						  AND ChkTimeDivCd = '{ChkTimeDivCd}' ";

			return db.ExecuteQuery(query);
		}

		public bool Del_InspectResult()
		{
			query = $@"DELETE EQ_MachineChk
                        WHERE CorpId = '{WrGlobal.CorpID}'
						  AND InspectDivCd = '{InspectDivCd}'
						  AND InspectDate = '{InspectDate}'
						  AND MachineCode = '{MachineCode}'
						  AND ChkTimeDivCd = '{ChkTimeDivCd}' ";

            return db.ExecuteQuery(query);
		}

		public bool Del_InspectResult_Daliy()
		{
			query = $@"DELETE EQ_MachineChk
                        WHERE CorpId = '{WrGlobal.CorpID}'
						  AND InspectDivCd = '{InspectDivCd}'
						  AND Seq = '{Seq}'
						  AND InspectDate = '{InspectDate}'
						  AND MachineCode = '{MachineCode}'
						  AND ChkTimeDivCd = '{ChkTimeDivCd}' ";

			return db.ExecuteQuery(query);
		}

		public bool Del_InspectResult_Routine(string yyyyMM)
		{
			query = $@"DELETE EQ_MachineChk
                        WHERE CorpId = '{WrGlobal.CorpID}'
						  AND InspectDivCd = '{InspectDivCd}'
						  AND InspectDate LIKE '{yyyyMM}%'
						  AND MachineCode = '{MachineCode}'
						  AND ChkTimeDivCd = '{ChkTimeDivCd}' ";

			return db.ExecuteQuery(query);
		}


		public DataTable Select_MachineMaster()
		{
			//설비관리대장: 일상점검 + 정기점검
			query = $@" SELECT mc.MachineCode
							 , m.MachineName Machine_Nm
							 , mc.Seq
							 , mcs.DispSeq
							 , mc.InspectDate
							 , mc.ChkTimeDivCd
							 , mc.InspectDivCd
							 , ManagementItem
							 , CheckMethod
							 , StandardSpec
							 , ValueTypeDivCd
							 --, mc.InspectCheck
							 , CASE WHEN mc.InspectCheck = 'Y' THEN '10'
							          WHEN mc.InspectCheck = 'N' THEN '20'
									  ELSE mc.InspectCheck
							   END InspectCheck
                             , CycleDivCd
                             , WeekDivCd

							 , mc.ChargeEmpNo
							 , mc.ActionResultDivCd
							 , mc.ActionDivCd
							 , mc.ActionMemo
							 , mc.ActionEmpNo
							 , mc.ChkImage

							 , mcs.InspectMin
							 , mcs.InspectMax
							 , mc.Remark

							 , mc.CreId
							 , mc.CreDt
							 , mc.ModId
							 , mc.ModDt

						FROM EQ_MachineChk mc
						LEFT JOIN EQ_MachineChkStd mcs ON (mcs.MachineCode = mc.MachineCode OR mcs.MachineCode = '') AND mcs.seq = mc.seq
						LEFT JOIN EQ_Machine m on mcs.MachineCode = m.MachineCode

						WHERE mc.CorpId = '{WrGlobal.CorpID}' 
						AND mc.InspectDate BETWEEN '{startDate}' AND '{endDate}'
						 ";

			if (MachineCode != "ALL")
			{
				query += $@"AND mc.MachineCode = '{MachineCode}' ";
			}

			query += "\n ORDER BY mcs.DispSeq";

			return db.GetDataTable(query);
		}

		public DataTable Select_MachineWork()
		{
			//설비이력관리
			query = $@" SELECT  '' AS Status
								, op.MachineCode
								, cm.MachineName
								, op.OrderNo 
								, op.OrderSeq
								, op.ItemCode
								, cmt.ItemName
								, ors.GoodQty
								, ors.NgQty
								, ors.WorkerID
								, cu.EmployeeName
								, ors.SerialNo
								, ors.WorkDate
								, ors.StartDate
								, ors.EndDate
								, ors.CreId
								, ors.CreIP
								, ors.CreDt
						FROM PM_OrderProcess op
						LEFT JOIN PM_OrderResult ors ON op.OrderNo = ors.OrderNo AND op.OrderSeq = ors.OrderSeq
						LEFT JOIN EQ_Machine cm ON op.CorpId = cm.CorpId AND op.MachineCode = cm.MachineCode
						LEFT JOIN CM_Item cmt ON op.CorpId = cmt.CorpId AND op.ItemCode = cmt.ItemCode
						LEFT JOIN SM_User cu ON ors.CorpId = cu.CorpId AND ors.WorkerID = cu.EmployeeId
						WHERE op.CorpId = '{WrGlobal.CorpID}' AND op.MachineCode is Not Null
							AND ors.WorkDate BETWEEN '{startDate}' AND '{endDate}'
						 ";

            if (MachineCode != "ALL")
            {
                query += $@"AND op.MachineCode = '{MachineCode}' ";
            }

            query += "\n ORDER BY ors.WorkDate";

            return db.GetDataTable(query);
		}

		public DataTable Select_EQ_Repair()
		{
			query = $@"
                        SELECT
                            '' AS Status
                            , CorpId
                            , RepairNo
                            , CONVERT(DATETIME, RepairDate, 112) RepairDate
                            , MachineCode
                            , MachineCode AS MachineName
                            , RePairCause
                            , RepairState
                            , RepairEmpNo
                            , RepairStartTime
                            , RepairEndTime
                            , RepairTime
                            , RepairCustCd
                            , RepairAmt
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM EQ_Repair

                        WHERE CorpId = '{WrGlobal.CorpID}' AND RepairDate BETWEEN '{startDate}' AND '{endDate}'
                    ";

			if (MachineCode != "ALL")
			{
				query += $"\nAND MachineCode = '{MachineCode}'";
			}

			return db.GetDataTable(query);
		}
	}
}