using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using SmartCommon.Common;

namespace SmartMES.Models
{
    public class clsSystem
    {
        Database db = new Database();
        string query = "";

        public DataView GetCorp()
        {
            query = string.Format("select CorpId 코드, CorpName 내용 from SM_Corp Order by CorpId");
            return db.GetDataView("사업장", query);
        }

        public bool ExecuteQry(string _query)
        {
            return db.ExecuteQuery(_query);
        }

        public DataView GetCorp(bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select '전체' 코드, '전체' 내용 Union All select CorpId 코드, CorpName 내용 from SM_Corp Order by 코드");
            }
            else
            {
                query = string.Format("select CorpId 코드, CorpName 내용 from SM_Corp Order by 코드");
            }
            return db.GetDataView("사업장", query);
        }

        public void LookupCorp(ref LookUpEdit search)
        {
            search.Properties.DataSource = GetCorp();
            search.Properties.DisplayMember = "내용";
            search.Properties.ValueMember = "코드";
        }

        public void LookupCorp(ref RepositoryItemLookUpEdit search)
        {
            search.DataSource = GetCorp();
            search.DisplayMember = "내용";
            search.ValueMember = "코드";
        }

        public DataView GetAuthorViewByGroupCode(string groupcode)
        {
            query = string.Format("exec Code_GetCodeAuthorByGroupCode '{0}', '{1}'", WrGlobal.PublicCorpID, groupcode);

            return db.GetDataView("사용자권한", query);
        }

        public string GetNewAuthorGroupCode()
        {
            query = string.Format("SELECT CONVERT(NVARCHAR(4), ISNULL(MAX(GroupCode), 1000) + 1) GroupCode FROM SM_AuthorGroup Where CorpId = '{0}'", WrGlobal.PublicCorpID);
            DataRowView drv = db.GetDataRecord(query);

            return drv["GroupCode"].ToString();
        }

        public void ExecuteAuthorGroupDetailDeleteByGroupCode(string groupcode)
        {
            db.ExecuteQuery(string.Format("DELETE FROM SM_UserAuthor WHERE CorpId = '{0}' and GroupCode = '{1}'", WrGlobal.PublicCorpID, groupcode));
            db.ExecuteQuery(string.Format("UPDATE SM_User SET AuthorGroup = REVERSE(STUFF(REVERSE(Replace(Replace(AuthorGroup, ' ', '') + ',', '{0},', '')), 1, 1, ''))", groupcode));
        }

        public DataView GetEmployeeMenuList()
        {
            query = string.Format("exec Code_GetEmployeeMenuList '{0}', '{1}', '{2}', '{3}'", WrGlobal.PublicCorpID, WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.ProgramType);
            return db.GetDataView("MenuGroup", query);
        }
        public DataView GetEmployeeQuickMenuList(string type)
        {
            query = string.Format("exec Code_GetEmployeeQuickMenuList '{0}', '{1}', '{2}', '{3}', '{4}'", WrGlobal.PublicCorpID, WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.ProgramType, type);
            return db.GetDataView("MenuGroup", query);
        }

        public DataRowView GetUserInfo(string uid)
        {
            query = string.Format("SELECT * "
                            + "FROM SM_User "
                            + "WHERE CorpId = '{0}' and EmployeeId = '{1}'", WrGlobal.CorpID, uid);
            return db.GetDataRecord(query);
        }

        public DataView GetUser()
        {
            query = string.Format("SELECT EmployeeID 작업자ID, EmployeeName 작업자명 "
                            + "FROM SM_User "
                            + "WHERE CorpId = '{0}' Order By EmployeeId", WrGlobal.CorpID);
            return db.GetDataView("사용자", query);
        }

        public DataView GetGroup(bool all_Yn = false)
        {
            query = "";

            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT'ALL' AS 그룹코드, '전체' AS 그룹명
                            UNION ALL ";
            }

            query += string.Format("SELECT AMS_GROUP_CODE 그룹코드, AMS_GROUP_NAME 그룹명 "
                            + "FROM AMS_GROUP_LIST");
            return db.GetDataView("그룹", query);
        }

        public DataView GetEmployee()
        {
            query = string.Format("SELECT EmpNo 작업자코드, Name 작업자명 "
                            + "FROM Employee "
                            + "WHERE CorpId = '{0}' Order By EmpNo", WrGlobal.CorpID);
            return db.GetDataView("사용자", query);
        }

        //test
        public void SetUserAuthor()
        {
            query = string.Format("SELECT AuthorGroup FROM SM_User WHERE CorpId = '{0}' and EmployeeId = '{1}'", WrGlobal.CorpID, WrGlobal.LoginID);
            DataRowView drv = db.GetDataRecord(query);

            if (drv[0] == DBNull.Value) return;

            drv[0] = drv[0].ToString().Replace(" ", "");

            string[] arrGroup = drv[0].ToString().Split(new char[] { ',' });
            string inGroupStr = "";

            foreach (string item in arrGroup)
            {
                if (item == "") continue;

                if (inGroupStr != "") inGroupStr = inGroupStr + ",";
                inGroupStr = inGroupStr + "''" + item + "''";
            }

            query = string.Format("exec Code_GetAuthorByAuthorGroups '{0}', '{1}', '{2}'", WrGlobal.PublicCorpID, WrGlobal.ProgramType, inGroupStr);
            DataView dv = db.GetDataView("권한", query);

            foreach (DataRowView view in dv)
            {
                clsAuthor author = new clsAuthor(view["MenuID"].ToString(), view["ParentID"].ToString(), view["AuthorCode"].ToString());
                WrGlobal.AuthorList.Add(author);
            }
        }

        public DataView GetGroupAuthorList()
        {
            query = string.Format("SELECT GroupCode, GroupName FROM SM_AuthorGroup Where CorpId = '{0}' AND GroupName <> '개발자' ORDER BY GroupName", WrGlobal.PublicCorpID);
            return db.GetDataView("SM_AuthorGroup", query);
        }

        public DataView GetGroupAuthorListALL()
        {
            query = string.Format("SELECT GroupCode, GroupName FROM SM_AuthorGroup Where CorpId = '{0}' ORDER BY GroupName", WrGlobal.PublicCorpID);
            return db.GetDataView("SM_AuthorGroup", query);
        }

        public DataView GetDeptList()
        {
            query = string.Format("select DeptCode 부서코드, ParentId, DeptName 부서명 From CM_Dept Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);
            return db.GetDataView("CM_Dept", query);
        }

        public bool ChangePassword(string frpwd, string topwd)
        {
            bool bRet = false;

            query = string.Format("SELECT * FROM SM_User WHERE CorpId = '{0}' and EmployeeId = '{1}'", WrGlobal.CorpID, WrGlobal.LoginID);

            DataRowView drv = db.GetDataRecord(query);

            if (clsCommon.SHA256Hash(frpwd) == clsCommon.getString(drv["Password"]))
            {
                query = string.Format("UPDATE SM_User SET Password = '{2}' WHERE CorpId = '{0}' and EmployeeId = '{1}'", WrGlobal.CorpID, WrGlobal.LoginID, clsCommon.SHA256Hash(topwd));
                bRet = db.ExecuteQuery(query);
            }
            else
            {
                bRet = false;
            }

            return bRet;
        }


        public bool ExecuteAuthorGroupDetailQueryList(List<string> list)
        {
            return db.ExecuteQueryList(list);
        }

        public int GetNextSequence(string seqName)
        {
            int nextSeq = 1;
            query = string.Format("SELECT NEXT VALUE FOR {0}", seqName);
            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                nextSeq = Convert.ToInt32(drv[0]);
            }

            return nextSeq;
        }

        public void SetDashboadMenuItem()
        {
            query = string.Format("Insert Into SM_DashboardItem(CorpId, MenuId, CreId, CreIP, CreDt) "
                            + "Select mg.CorpId, mg.Id MenuId, '{1}', HOST_NAME(), getdate() "
                                + "From SM_MenuGroup mg "
                                    + "Left Join SM_DashboardItem bi on mg.CorpId = bi.CorpId and mg.Id = bi.MenuId "
                                + "Where mg.CorpId = '{0}' and bi.MenuId is null",
                                WrGlobal.PublicCorpID, WrGlobal.LoginID);

            db.ExecuteQuery(query);
        }

        public DataRowView IsExistDashboardItem(string menuId)
        {
            query = string.Format("Select bi.*, mg.Title_ko MenuName, pmg.Title_ko ParentMenuName "
                                + "From SM_DashboardItem bi "
                                    + "Inner Join SM_MenuGroup mg on bi.CorpId = mg.CorpId and bi.MenuId = mg.Id "
                                    + "Left Join SM_MenuGroup pmg on mg.CorpId = pmg.CorpId and mg.ParentId = pmg.Id "
                                + "Where bi.CorpId = '{0}' and bi.MenuId = '{1}'",
                            WrGlobal.PublicCorpID, menuId);

            return db.GetDataRecord(query);
        }

        public void SetCodeAuthorByMenu()
        {
            query = string.Format("exec Code_SetCodeAuthorByMenu '{0}'",
                                WrGlobal.PublicCorpID);

            db.ExecuteQuery(query);
        }
        //세션 생성
        public void CreateSession()
        {
            WrGlobal.SessionNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            query = string.Format("exec Code_CreateSession '{0}', N'{1}', '{2}', '{3}'", WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.SessionNo, "CS");
            db.ExecuteQuery(query);
        }
        //세션 종료
        public void CloseSession()
        {
            query = string.Format("exec Code_CloseSession '{0}', N'{1}', '{2}'", WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.SessionNo);
            db.ExecuteQuery(query);
        }
        //메뉴 사용
        public void UseSession(string menuId, string useType)
        {
            query = string.Format("exec Code_UseSession '{0}', N'{1}', '{2}', '{3}', N'{4}'", WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.SessionNo, menuId, useType);
            db.ExecuteQuery(query);
        }
    }
}
