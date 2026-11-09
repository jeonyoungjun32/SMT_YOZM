using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;

using System;
using System.Collections.Generic;
using System.Data;

using SmartPOP.Common;


namespace SmartPOP.Models
{
    public class clsSystem
    {
        Database db = new Database();
        string query = "";

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

        public DataView GetEmployeeMenuList()
        {
            // POP 메뉴 생성
            query = string.Format("exec Code_GetPopMenuList '{0}', '{1}', '{2}', '{3}'", WrGlobal.PublicCorpID, WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.ProgramType);
            return db.GetDataView("MenuGroup", query);
        }

        public DataView GetEmployeeQuickMenuList(string type)
        {
            // 즐겨찾기 메뉴 생성
            query = string.Format("exec Code_GetEmployeeQuickMenuList '{0}', '{1}', '{2}', '{3}', '{4}'", WrGlobal.PublicCorpID, WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.ProgramType, type);
            return db.GetDataView("MenuGroup", query);
        }

        //세션 생성
        public void CreateSession()
        {
            WrGlobal.SessionNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            query = string.Format("exec Code_CreateSession '{0}', N'{1}', '{2}', '{3}'", WrGlobal.CorpID, WrGlobal.LoginID, WrGlobal.SessionNo, "CS");
            db.ExecuteQuery(query);
        }
    }
}
