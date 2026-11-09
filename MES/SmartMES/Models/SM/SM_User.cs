using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.SM
{
	class SM_User
    {
        #region Property

        public string CorpId { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Password { get; set; }

        public string AuthorGroup { get; set; }

        public string DeptId { get; set; }

        public string JobTitle { get; set; }

        public string Phone { get; set; }

        public string CellPhone { get; set; }

        public byte[] Photo { get; set; }

        public string UseYn { get; set; }

        public string AutoLoginYn { get; set; }

        public string Email { get; set; }

        public string CustYn { get; set; }

        public string CustCode { get; set; }

        public byte[] Pic { get; set; }

        public string WorkerYn { get; set; }


        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_SM_User()
        {
            WorkerYn = WorkerYn.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT '' AS Status
                               , CorpId
                               , EmployeeId
                               , EmployeeName
                               , '' AS Password
                               , Password AS Password2
                               , DeptId
                               , JobTitle
                               , Phone
                               , CellPhone
                               , AuthorGroup
                               , Email
                               , UseYn
                               , AutoLoginYn
                               , WorkerYn
                               , Pic
                               , CreId, CreIP, CreDt
                               , ModId, ModIP, ModDt
                        FROM SM_User
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        ";

            if(WorkerYn != "")
			{
                query += $"\nAND WorkerYn = '{WorkerYn}'";
			}

            query += $"\nORDER BY CASE WHEN WorkerYn = 'Y' THEN 2 ELSE 1 END, EmployeeId";

            return db.GetDataTable(query);
        }

        public DataTable SelectUserList(bool all, string[] deptId = null, string workerYn = "")
        {
            query = "";

            if (all)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT'ALL' AS 코드
                                , '전체' AS 명칭
                            UNION ALL ";
            }

            query += $@"
                Select cu.EmployeeId 코드, cu.EmployeeName 명칭 
                From SM_User cu
                Where cu.CorpId = '{WrGlobal.CorpID}' 
                and cu.UseYn = 'Y'
                and cu.EmployeeId <> 'Dev'
                ";

            //작업자여부
            if (workerYn != "")
            {
                query += $"\nAND cu.WorkerYn = '{workerYn}'";
            }

            //특정 부서만 보고 싶은 경우
            if (deptId != null &&
                deptId.Length > 0)
            {
                query += "\nAND cu.DeptId IN (";

                for (int idx = 0; idx < deptId.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{deptId[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }


        public DataTable SelectWorkerList()
        {
            query = $@"
                        SELECT ''AS Status, EmployeeId, EmployeeName, DeptId, Password, UseYn, WorkerYn
                               , CreId, CreIP, CreDt
                               , ModId, ModIP, ModDt
                        FROM SM_User 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND WorkerYn = 'Y'
                        ";

            return db.GetDataTable(query);
        }


        public byte[] SelectUserPic()
        {
            query = $@"
                        SELECT Pic
                        FROM SM_User 
                        WHERE CorpId = '{CorpId}'
                        AND EmployeeId = '{EmployeeId}'
                        ";

            return db.ExecuteImageToByte(query);
        }

        public string GetNewEmpId(string corpId, string empType)
		{
            //empType 관리자:E, 작업자:W
            query = $@"select dbo.[FN_Create_Cd_Emp]('{corpId}','{empType}')";

            DataRow dr = db.GetDataRow(query);
            string empNo = "";

            if(dr != null)
			{
                empNo = dr[0].ConvertString_Null_To_Empty();
			}

            return empNo;
		}

        public bool Ins_SM_User()
		{
            query = $@"
                INSERT INTO SM_User
                (
                CorpId
                , EmployeeId
                , EmployeeName
                , Password
                , DeptId
                , Phone
                , CellPhone
                , AuthorGroup
                , JobTitle
                , Email
                , UseYn
                , AutoLoginYn
                , WorkerYn
                , CreId
                , CreIP
                , CreDt
                )
                VALUES
                (
                '{CorpId}'
                , '{EmployeeId}'
                , {EmployeeName.ConvertStr_Empty_To_Null()}
                , '{Password}'
                , {DeptId.ConvertStr_Empty_To_Null()}
                , {Phone.ConvertStr_Empty_To_Null()}
                , {CellPhone.ConvertStr_Empty_To_Null()}
                , {AuthorGroup.ConvertStr_Empty_To_Null()}
                , {JobTitle.ConvertStr_Empty_To_Null()}
                , {Email.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , {AutoLoginYn.ConvertStr_Empty_To_Null()}
                , {WorkerYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
                )
                ";

            return db.ExecuteQuery(query);
        }

        public bool InsertWorker()
        {
            query = $@"
                INSERT INTO SM_User
                (
                CorpId
                , EmployeeId
                , EmployeeName
                , Password
                , DeptId
                , UseYn
                , WorkerYn
                , CreId
                , CreIP
                , CreDt
                )
                VALUES
                (
                '{WrGlobal.CorpID}'
                 , CASE WHEN '{WrGlobal.ProcOption["SC"]["Worker_AutoNo"]}' = 'Y' THEN dbo.FN_Create_Cd_Emp('{WrGlobal.CorpID}', 'W')
                                    ELSE '{EmployeeId}' end
                , '{EmployeeName}'
                , '{Password}'
                , {DeptId.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , {WorkerYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
                )
                ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_SM_User()
        {
            query = $@"
                    UPDATE SM_User
                    SET
                       EmployeeName = {EmployeeName.ConvertStr_Empty_To_Null()}
                     , Password = '{Password}'
                     , DeptId = {DeptId.ConvertStr_Empty_To_Null()}
                     , Phone = {Phone.ConvertStr_Empty_To_Null()}
                     , CellPhone = {CellPhone.ConvertStr_Empty_To_Null()}
                     , AuthorGroup = {AuthorGroup.ConvertStr_Empty_To_Null()}
                     , JobTitle = {JobTitle.ConvertStr_Empty_To_Null()}
                     , Email = {Email.ConvertStr_Empty_To_Null()}
                     , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                     , AutoLoginYn = {AutoLoginYn.ConvertStr_Empty_To_Null()}
                     , WorkerYn = {WorkerYn.ConvertStr_Empty_To_Null()}
                     , ModId = '{WrGlobal.LoginID}'
                     , ModIP = '{WrGlobal.ClientHostName}'
                     , ModDt = GETDATE()

                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND EmployeeId = '{EmployeeId}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool UpdateWorker()
        {
            query = $@"
                UPDATE SM_User
                SET EmployeeName = '{EmployeeName}'
                , DeptId = '{DeptId}'
                , UseYn = '{UseYn}'
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND EmployeeId = '{EmployeeId}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_SM_User_Pic(byte[] pic)
        {
            query = $@"
                    UPDATE SM_User
                    SET Pic = @Pic

                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND EmployeeId = '{EmployeeId}'
                    ";

            return db.ExecuteQueryImage(query, pic);
        }

        public bool Del_SM_User()
        {
            query = $@"
                DELETE FROM SM_User
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND EmployeeId = '{EmployeeId}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_SM_User_Pic()
        {
            query = $@"
                    UPDATE SM_User
                    SET Pic = null

                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND EmployeeId = '{EmployeeId}'
                    ";

            return db.ExecuteQuery(query);
        }

    }
}
