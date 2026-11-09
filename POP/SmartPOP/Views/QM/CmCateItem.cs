using SmartPOP.Common;
using System;
using System.Data;

namespace SmartMES.View.QC
{
    class CmCateItem
    {
        #region property
        public string CorpId { get; set; }

        public string Class { get; set; }

        public string ItemCode { get; set; }

        public string Title { get; set; }

        public string ExtraValue { get; set; }

        public string ExtraValue1 { get; set; }

        public string CodeType { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        #endregion

        Database db = new Database();

        string query;

        public DataTable SelectClassItemLoopUp(string corpID, string _class, string jobType = null)
        {
            query = " SELECT [ItemCode] 분류코드, [Title] 분류명 " +
                    "   FROM [CmCateItem] " +
                   $"  WHERE Class = '{_class}' " +
                   $"    AND UseYn = 'Y'" +
                   $"    AND CorpId = '{corpID}' order by ExtraValue";

            DataTable dtCmCateItem = db.GetDataTable(query);

            //// 분류명에 따른 JobType 값 매핑
            //Dictionary<string, string> examTypeMap = new Dictionary<string, string>
            //{
            //    {  "수입검사", "I" },
            //    {  "출하검사", "O" },
            //    {  "공정검사", "P" }
            //};

            //if (jobType != null)
            //{
            //    foreach (DataRow row in dtCmCateItem.Rows)
            //    {
            //        string divName = row["분류명"].ToString();
            //        if (examTypeMap.ContainsKey(divName))
            //        {
            //            row["분류코드"] = examTypeMap[divName];
            //        }
            //    }
            //}
            return dtCmCateItem;
        }

        public DataTable SelectInspStanLoopUp(string corpID, string _class, string jobType = null)
        {
            query = $@"SELECT 'I' AS 분류코드, '수입검사' AS 분류명
                UNION
                SELECT 'O' AS 분류코드, '출하검사' AS 분류명
                UNION
                SELECT 'P' AS 분류코드, '공정검사' AS 분류명";

            DataTable dtCmCateItem = db.GetDataTable(query);
            return dtCmCateItem;
        }

        public DataTable Select_LupItem()
        {
            query = $@"SELECT ItemCode as 분류코드, Title as 분류명
                                 FROM CmCateItem
                                 WHERE Class = '{Class}'";

            return db.GetDataTable(query);
        }
        public DataTable SelectClassItemLoopUp_WithALL(string corpID, string _class, string[] ItemCode, bool AllYn = true)
        {
            query = "";
            if (AllYn)
            {
                query += @"SELECT 'ALL' AS 분류코드, '전체' AS 분류명
                        UNION ALL ";
            }

            query += $@" SELECT [ItemCode] 분류코드, [Title] 분류명
                        FROM [CmCateItem]
                       WHERE Class = '{_class}'
                         AND CorpId = '{corpID}' ";

            //특정 구분만 보고 싶은 경우
            if (ItemCode != null && ItemCode.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < ItemCode.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" ItemCode = '{ItemCode[idx]}'";
                }

                query += ")";
            }

            DataTable dtCmCateItem = db.GetDataTable(query);

            return dtCmCateItem;
        }

        public DataTable SelectFaultCode()
        {
            query = " SELECT ItemCode, Title as '불량종류' " +
                    "   FROM CmCateItem " +
                   $"  WHERE class = 'G010' "; //불량종류

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable SelectClassItem(string corpId, string Class)
        {
            query = " SELECT [CorpId] ,[Class] ,[ItemCode] ,[Title] ,[ExtraValue], " +
                    "        [ExtraValue1] ,[CodeType] ,[CreId] ,[CreIP] ,[CreDt], " +
                    "        [ModId] ,[ModIP] ,[ModDt] " +
                    "   FROM [CmCateItem]" +
                    "  WHERE Class = '" + Class + "' ";

            DataTable dtCmCateItem = db.GetDataTable(query);

            return dtCmCateItem;
        }

        public DataTable Select_CmCateItem_ByClass()
        {
            query = $@"SELECT '' as Status, ItemCode, Title 
                                   FROM CmCateItem
                                 WHERE CorpId = '{WrGlobal.CorpID}' AND Class = '{Class}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_CmCateItem_ByClass_ForLookup()
        {
            query = $@"SELECT ItemCode 코드, Title 분류명
                                   FROM CmCateItem
                                 WHERE CorpId = '{WrGlobal.CorpID}' AND Class = '{Class}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_MaterialType_ForSystem()
        {
            query = $@"SELECT ItemCode 코드, Title 분류명
                                 FROM CmCateItem
                               WHERE CorpId = 'Z000' AND CLASS = 'A010' AND ItemCode IN ('20', '30')";

            return db.GetDataTable(query);
        }

        public DataTable SelectCateItem(string Class, string[] itemCode = null, bool AllYn = false)
        {
            query = "";
            if (AllYn)
            {
                query = $@"SELECT 'All' AS 코드, '전체' AS 명칭
                            UNION ALL";
            }

            query += $@" SELECT ItemCode AS 코드, Title as 명칭
                           FROM CmCateItem
                          WHERE Corpid = '{WrGlobal.CorpID}'
                            AND CLASS = '{Class}'";
            if (itemCode != null &&
                itemCode.Length > 0)
            {
                //일부 데이터만 추출하고싶은 경우
                query += " AND ( ";

                for (int idx = 0; idx < itemCode.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR ";
                    }

                    query += $" ItemCode = '{itemCode[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public void InsertUpdateCmCateItem()
        {
            query = $@"IF EXISTS (
                       SELECT Class
                         FROM CmCateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND Class = '{Class}'
                          AND ItemCode = '{ItemCode}' )

                       UPDATE CmCateItem
                              SET Title = '{Title}',
                                  ExtraValue = '{ExtraValue}',
                                  ExtraValue1 = '{ExtraValue1}',
                                  CodeType = '{CodeType}',

                                  ModID = '{WrGlobal.LoginID}',
                                  ModIP = '{WrGlobal.ClientIpAddress}',
                                  ModDt = GETDATE()
                            WHERE CorpId = '{CorpId}'
                              AND Class = '{Class}'
                              AND ItemCode = '{ItemCode}'

                        ELSE
                      INSERT INTO CmCateItem (
                                  CorpId, Class, ItemCode, Title, ExtraValue,
                                  ExtraValue1,CodeType, EditYn, UseYn, 
                                  CreId, CreIP, CreDt)
                           VALUES (
                             '{WrGlobal.CorpID}',
                             '{Class}',
                             '{ItemCode}',
                             '{Title}',
                             '{ExtraValue}',
                             '{ExtraValue1}',
                             '{CodeType}',
                             'Y',
                             'Y',
                             '{WrGlobal.LoginID}',
                             '{WrGlobal.ClientIpAddress}',
                             GETDATE() ) ";

            db.ExecuteQuery(query);
        }

        public void UpdateCateItem_AutoInfo()
        {
            query = $@"UPDATE CmCateItem
                          SET Title = '{Title}'
                        WHERE CorpId = '{CorpId}'
                          AND Class = '{Class}'
                          AND ItemCode = '{ItemCode}'";

            db.ExecuteQuery(query);
        }

        public void DeleteCateItem()
        {
            query = $@" DELETE CmCateItem WHERE CLASS = '{Class}' AND ItemCode = '{ItemCode}'";

            db.ExecuteQuery(query);
        }

        public DataTable Select_Value_Div_Cd()
        {
            query = $@"SELECT Class 코드, ClassName 명칭 FROM CLASS WHERE CLASSLEVEL = '2' AND HIGHCLASS = '10'";

            return db.GetDataTable(query);
        }

        public DataRow Select_ItemCode()
        {
            query = $@"SELECT ItemCode 
                                FROM CMCATEITEM 
                                WHERE CLASS = 'A011' AND Title = '{Title}'";

            return db.GetDataRow(query);
        }

        public DataRow Select_ItemCode_By_CmCateItem()
        {
            query = $@"SELECT ItemCode
                                 FROM CmCateItem 
                                 WHERE Class = '{Class}'
                                     AND Title = '{Title}'";

            return db.GetDataRow(query);
        }

        public DataTable Select_Use_Yn()
        {
            query = $@"SELECT ItemCode 코드, Title 명칭
                                 FROM CmCateItem 
                                 WHERE Class = 'A050'";

            return db.GetDataTable(query);
        }

        public DataTable Select_Week_Div()
        {
            query = $@"SELECT ItemCode 코드, Title 명칭
                                 FROM CmCateItem 
                                 WHERE Class = '2060'";

            return db.GetDataTable(query);
        }
    }
}