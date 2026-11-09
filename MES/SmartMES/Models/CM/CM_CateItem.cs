using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.Models.CM
{
	class CM_CateItem
    {
        #region property
        public string CorpId { get; set; }

        public string Class { get; set; }

        public string ClassItemCode { get; set; }

        public string Title { get; set; }

        public string ExtraValue { get; set; }

        public string ExtraValue1 { get; set; }

        public string CodeType { get; set; }

        public int? OrderSeq { get; set; }

        public string UseYn { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        #endregion

        Database db = new Database();

        string query;

        public DataTable SelectClassItemLookUp(string corpID, string _class, string jobType = null)
        {
            query = " SELECT [ClassItemCode] 분류코드, [Title] 분류명 " +
                    "   FROM [CM_CateItem] " +
                   $"  WHERE Class = '{_class}' " +
                   $"    AND UseYn = 'Y'" +
                   $"    AND CorpId = '{corpID}' ";

            //JobType 이 null 이 아니면
            //분류명에 따른 JobType 값 매핑
            if (jobType != null)
            {
                query += $" AND ClassItemCode = '" + jobType + "'";
            }
            //order by ExtraValue 추가
            query += " ORDER BY ExtraValue";

            DataTable dtCmCateItem = db.GetDataTable(query);

            return dtCmCateItem;
        }

        public DataTable SelectClassItemLoopUp(string corpID, string _class, string[] ClassItemCode)
        {
            query = " SELECT [ClassItemCode] 분류코드, [Title] 분류명 " +
                    "   FROM [CM_CateItem] " +
                   $"  WHERE Class = '{_class}' " +
                   $"    AND UseYn = 'Y'" +
                   $"    AND CorpId = '{corpID}' ";

            //특정 구분만 보고 싶은 경우
            if (ClassItemCode != null && ClassItemCode.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < ClassItemCode.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" ClassItemCode = '{ClassItemCode[idx]}'";
                }

                query += ")";
            }

            //order by ExtraValue 추가
            query += " ORDER BY ExtraValue";

            DataTable dtCmCateItem = db.GetDataTable(query);

            return dtCmCateItem;
        }

        public DataTable GetCategory(string pclass)
        {
            query = $@" Select '' Status, * 
                        FROM CM_CateItem 
                        Where CorpId = '{ WrGlobal.CorpID}' AND CLASS = '{pclass}' ";

            return db.GetDataTable(query);
        }

        public DataRow Select_CateItem_ByCode()
        {
            Title = Title.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT
                            CLASS
                            , ClassItemCode
                            , Title
                            , ExtraValue
                            , ExtraValue1
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = '{Class}'
                        AND ClassItemCode = '{ClassItemCode}' 
                    ";

            if(Title != "")
			{
                query += $"\nAND Title = '{Title}'";
			}

            DataRow dr = db.GetDataRow(query);

            return dr;
        }

        public DataRow Select_ItemCode_By_CmCateItem()
        {
            query = $@"SELECT ClassItemCode
                                 FROM CM_CateItem 
                                 WHERE Class = '{Class}'
                                     AND Title = '{Title}'";

            return db.GetDataRow(query);
        }

        public DataTable Select_LupItem()
        {
            query = $@"SELECT ClassItemCode as 분류코드, Title as 분류명
                                 FROM CM_CateItem
                                 WHERE Class = '{Class}'";

            return db.GetDataTable(query);
        }

        public DataTable SelectClassItemLoopUp_WithALL(string corpID, string _class, string[] ClassItemCode, bool AllYn = true)
        {
            query = "";
            if (AllYn)
            {
                query += @"SELECT'ALL' AS 분류코드, '전체' AS 분류명
                        UNION ALL ";
            }

            query += $@" SELECT [ClassItemCode] 분류코드, [Title] 분류명
                        FROM [CM_CateItem]
                       WHERE Class = '{_class}'
                        AND CorpId = '{corpID}'
                        ";

            //특정 구분만 보고 싶은 경우
            if (ClassItemCode != null && ClassItemCode.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < ClassItemCode.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" ClassItemCode = '{ClassItemCode[idx]}'";
                }

                query += ")";
            }

            DataTable dtCmCateItem = db.GetDataTable(query);

            return dtCmCateItem;
        }

        public DataTable SelectCateItem(string Class, string[] ClassItemCode = null, bool AllYn = false)
        {
            query = "";
            if (AllYn)
            {
                query = $@"SELECT'ALL' AS 코드, '전체' AS 명칭
                            UNION ALL";
            }

            query += $@" SELECT ClassItemCode AS 코드, Title as 명칭
                           FROM CM_CateItem
                          WHERE Corpid = '{WrGlobal.CorpID}'
                            AND CLASS = '{Class}'
                            AND UseYn = 'Y'
                            ";
            if (ClassItemCode != null &&
                ClassItemCode.Length > 0)
            {
                //일부 데이터만 추출하고싶은 경우
                query += " AND ( ";

                for (int idx = 0; idx < ClassItemCode.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR ";
                    }

                    query += $" ClassItemCode = '{ClassItemCode[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Value_Div_Cd()
        {
            query = $@"SELECT Class 코드, ClassName 명칭 FROM CM_CLASS WHERE CLASSLEVEL = '2' AND HIGHCLASS = '10'";

            return db.GetDataTable(query);
        }

        public DataTable Select_UseYn()
        {
            query = $@"SELECT ClassItemCode 코드, Title 명칭
                                 FROM CM_CateItem 
                                 WHERE Class = 'A050'";

            return db.GetDataTable(query);
        }

        public DataTable SelectCmCateItem()
        {
            query = $@"
                        SELECT '' AS Status
                                ,CorpId
                                ,CLASS
                                ,ClassItemCode
                                ,ClassItemCode as Item
                                ,Title
                                ,ExtraValue
                                ,ExtraValue1
                                ,CodeType
                                ,OrderSeq
                                ,EditYn
                                ,UseYn
                                ,Remark
                                ,CreId
                                ,CreIP
                                ,CreDt
                                ,ModId
                                ,ModIP
                                ,ModDt
                        FROM CM_CateItem
                        WHERE CorpID ='{WrGlobal.CorpID}'
                        AND CLASS =  '{Class}'
                        ORDER BY CASE WHEN OrderSeq IS NULL THEN 1 ELSE 0 END, OrderSeq ASC ";

            return db.GetDataTable(query);
        }

        public bool InsertCateItem()
        {
            query = $@"INSERT INTO CM_CateItem
                            (CorpId
                             ,CLASS
                             ,ClassItemCode
                             ,Title
                             ,ExtraValue
                             ,ExtraValue1
                             ,OrderSeq
                             ,UseYn
                             ,CreId
                             ,CreIP
                             ,CreDt)
                       VALUES(
                             '{WrGlobal.CorpID}'
                             , '{Class}'
                             , '{ClassItemCode}'
                             , {Title.ConvertStr_Empty_To_Null()}
                             , {ExtraValue.ConvertStr_Empty_To_Null()}
                             , {ExtraValue1.ConvertStr_Empty_To_Null()}
                             , {(OrderSeq.HasValue ? OrderSeq.ToString() : "NULL")}
                             , {UseYn.ConvertStr_Empty_To_Null()}
                             , '{WrGlobal.LoginID}'
                             , '{WrGlobal.ClientIpAddress}'
                             , GETDATE())";

            return db.ExecuteQuery(query);
        }

        public bool UpdateCateItem(string item)
        {
            query = $@"UPDATE CM_CateItem
                      SET ClassItemCode    = '{ClassItemCode}'
                        , Title       = {Title.ConvertStr_Empty_To_Null()}
                        , ExtraValue  = {ExtraValue.ConvertStr_Empty_To_Null()}
                        , ExtraValue1 = {ExtraValue1.ConvertStr_Empty_To_Null()}
                        , OrderSeq    = {(OrderSeq.HasValue ? OrderSeq.ToString() : "NULL")}
                        , UseYn       = '{UseYn}'
                        , ModId       = '{WrGlobal.LoginID}'
                        , ModIP       = '{WrGlobal.ClientIpAddress}'
                        , ModDt       = GETDATE()
                        WHERE CorpID  = '{WrGlobal.CorpID}'
                            AND CLASS = '{Class}'
                            AND ClassItemCode = '{item}'";

            return db.ExecuteQuery(query);
        }

        public bool DeleteCateItem()
        {
            query = $@"DELETE CM_CateItem
                    WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = '{Class}'
                        AND ClassItemCode = '{ClassItemCode}'
                      ";

            return db.ExecuteQuery(query);
        }
    }
}
