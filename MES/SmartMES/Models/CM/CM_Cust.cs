using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.CM
{
	public class CM_Cust
    {

        #region Property

        public string CorpId { get; set; }

        public string CustCode { get; set; }

        public string CustName { get; set; }

        public string CustEngName { get; set; }

        public string CustType { get; set; }

        public string ExchangeType { get; set; }

        public string VatType { get; set; }

        public string CustNo { get; set; }

        public string CustRegNo { get; set; }

        public string PresiName { get; set; }

        public string CitizeNo { get; set; }

        public string PostNo { get; set; }

        public string Address { get; set; }

        public string AddressEng { get; set; }

        public string Tel { get; set; }

        public string CellPhone { get; set; }

        public string Fax { get; set; }

        public string HomePage { get; set; }

        public string Email { get; set; }

        public string BussinessCategory { get; set; }

        public string BussinessField { get; set; }

        public string OpenDate { get; set; }

        public string CloseDate { get; set; }

        public string SalesMan { get; set; }

        public string Remark { get; set; }

        public string UseYn { get; set; }
        #endregion

        Database db = new Database();

        string query;

        public DataTable Select_CM_CustAll()
        {
            query = $@"
                SELECT ''AS Status
                    , CustCode
                    , CustName
                    , CustEngName
                    , CustType
                    , ExchangeType
                    , VatType
                    , CustNo
                    , CustRegNo
                    , PresiName
                    , CitizeNo
                    , PostNo
                    , Address
                    , AddressEng
                    , Tel
                    , CellPhone
                    , Fax
                    , HomePage
                    , Email
                    , BussinessCategory
                    , BussinessField
                    , OpenDate
                    , CloseDate
                    , SalesMan
                    , Remark
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                    , ModId
                    , ModIP
                    , ModDt
                
                FROM CM_Cust
                WHERE CorpId = '{WrGlobal.CorpID}'
                ";

            return db.GetDataTable(query);
        }

        public DataRow Select_CmCustomeDetail()
        {
            query = $@"
                SELECT ''AS Status
                    , CustCode
                    , CustName
                    , CustEngName
                    , CustType
                    , ExchangeType
                    , VatType
                    , CustNo
                    , CustRegNo
                    , PresiName
                    , CitizeNo
                    , PostNo
                    , Address
                    , AddressEng
                    , Tel
                    , CellPhone
                    , Fax
                    , HomePage
                    , Email
                    , BussinessCategory
                    , BussinessField
                    , OpenDate
                    , CloseDate
                    , SalesMan
                    , Remark
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                    , ModId
                    , ModIP
                    , ModDt
          
                FROM CM_Cust
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND CustCode = '{CustCode}'
                ";

            return db.GetDataRow(query);
        }

        public DataTable SelectCustomer(string[] custType = null)
        {
            query = "";

            query += $@"
                        SELECT cc.CustCode , cc.CustName, cc.CustType, cci.Title CustTypeName, cc.PresiName, cc.CustNo
                        FROM CM_Cust cc
                        LEFT JOIN CM_CateItem cci on cc.custType = cci.ClassItemCode and cci.class = 'a030' --and cci.useyn = 'Y'
                        WHERE cc.CorpID = '{WrGlobal.CorpID}'
                        AND cc.UseYn = 'Y'
                        ";

            //거래처구분
            if (custType != null &&
            custType.Length > 0)
            {
                query += " AND cc.CustType IN (";

                for (int idx = 0; idx < custType.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{custType[idx]}'";
                }

                query += ")";
            }


            return db.GetDataTable(query);
        }

        public DataTable SelectCustomer_Price(string[] type = null)
        {
            query = "";

            query += $@"
                        SELECT cc.CustCode , cc.CustName, cc.CustType, cci.Title CustTypeName
                        ,  CASE WHEN EXISTS(
                                        SELECT CustCOde
                                        FROM CM_ItemPrice ip
                                        WHERE ip.CustCOde = cc.CustCOde
                                    )
                            THEN 'Y'ELSE 'N'
                            END AS PriceYn
                        FROM CM_Cust cc
                        LEFT JOIN CM_CateItem cci on cc.custType = cci.ClassItemCode and cci.class = 'a030' --and cci.useyn = 'Y'
                        WHERE cc.CorpID = '{WrGlobal.CorpID}'
                        AND cc.UseYn = 'Y'
                        ";

            if (CustType != "" && CustType != null && CustType.ToUpper() != "ALL")
            {
                query += $"\nAND cc.CustType = '{CustType}'";
            }

            if (type != null)
            {
                if (!(type.Length == 1 && type[0].ToUpper() == "ALL"))
                {
                    query += $"\n AND cc.CustType IN ('{string.Join("','", type)}')";
                }
            }

            return db.GetDataTable(query);
        }

        public DataRow Select_Cust_Info()
        {
            if ((CustCode == "" || CustCode == null) && (CustName == "" || CustName == null)) return null;

            query = $@"
                        SELECT TOP 1 bc.*
                        FROM CM_Cust bc  
                        WHERE bc.CorpID = '{WrGlobal.CorpID}'";

            if (CustCode != "" && CustCode != null) { query += $" AND bc.CustCode = '{CustCode}'"; }
            if (CustName != "" && CustName != null) { query += $" AND bc.CustName = '{CustName}'"; }

            return db.GetDataRow(query);
        }

        public DataTable Select_Cust_List(string[] cust_Div = null, bool all_Yn = false)
        {
            query = "";

            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT'ALL' AS 코드, '전체' AS 명칭
                        UNION ALL ";
            }

            query += $@"SELECT CustCode AS 코드, CustName AS 명칭
                         FROM CM_Cust
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            //특정 구분만 보고 싶은 경우
            if (cust_Div != null &&
                cust_Div.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < cust_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR ";
                    }

                    query += $@"CustType = '{cust_Div[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_LupCust(bool AllYn = false, string[] type = null)
        {
            query = "";

            if (AllYn)
            {
                query = $@" SELECT'ALL' 거래처코드, '전체' 거래처명
                            UNION ALL
                        ";
            }

            query += $@"
                        SELECT CustCode 거래처코드, CustName 거래처명
                        FROM CM_Cust
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        --AND UseYn = 'Y'
                        ";

            if (type != null) query += $"\n AND CustType IN ('{string.Join("','", type)}')";

            return db.GetDataTable(query);
        }

        public DataRow Check_Existence()
        {
            query = $@"SELECT 1 FROM CM_Cust WHERE CustCode = '{CustCode}'";
            return db.GetDataRow(query);
        }

        public void Insert_Update_CM_Cust()
        {
            if (CustName.Contains("'"))
            {
                CustName = CustName.Replace("'", "''");
            }

            query = $@"IF EXISTS(
                                                SELECT  CustCode
                                                  FROM CM_Cust
                                                WHERE CorpId = '{WrGlobal.CorpID}'
                                                    AND CustCode = '{CustCode}'
                                                )
                                UPDATE CM_Cust
                                       SET 
                                       CustName = '{CustName}',
                                       CustNo = '{CustNo}',
                                       Tel = '{Tel}',
                                       SalesMan = '{SalesMan}',
                                       Address = '{Address}',
                                       CustType = '{CustType}',
                                       PresiName = '{PresiName}',
                                       CitizeNo = '{CitizeNo}',
                                       BussinessCategory = '{BussinessCategory}',
                                       BussinessField = '{BussinessField}',
                                       Fax = '{Fax}',
                                       Email = '{Email}',
                                       HomePage = '{HomePage}',
                                       PostNo = '{PostNo}',
                                       UseYn = '{UseYn}',
                                       Remark = '{Remark}'
                          WHERE CorpId = '{WrGlobal.CorpID}'
                            AND CustCode = '{CustCode}' 
                            
                            ELSE
                            INSERT INTO CM_Cust(CorpId, CustCode, CustName, CustNo, Tel, SalesMan, Address, 
                                                                        CustType, PresiName, CitizeNo, BussinessCategory,
                                                                        BussinessField, Fax, Email, HomePage, PostNo, UseYn, Remark)
                                    VALUES(
                                    '{WrGlobal.CorpID}',
                                    '{CustCode}',
                                    '{CustName}',
                                    '{CustNo}',
                                    '{Tel}',
                                    '{SalesMan}',
                                    '{Address}',
                                    '{CustType}',
                                    '{PresiName}',
                                    '{CitizeNo}',
                                    '{BussinessCategory}',
                                    '{BussinessField}',
                                    '{Fax}',
                                    '{Email}',
                                    '{HomePage}',
                                    '{PostNo}',
                                    '{UseYn}',
                                    '{Remark}')";

            db.ExecuteQuery(query);
        }

        public bool Insert_CM_Cust()
        {
            query = $@"

                INSERT INTO CM_Cust
                ( CorpId
                  , CustCode
                  , CustName
                  , CustType
                  , UseYn
                  , SalesMan
                  , PresiName
                  , CustNo
                  , CustRegNo
                  , BussinessCategory
                  , BussinessField
                  , Tel
                  , Fax
                  , Email
                  , HomePage
                  , PostNo
                  , Address
                  , Remark
                  , CreId
                  , CreIP
                  , CreDt
                )
                VALUES
                (
                 '{WrGlobal.CorpID}'
                , '{CustCode}'
                , {CustName.ConvertStr_Empty_To_Null()}
                , {CustType.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , {SalesMan.ConvertStr_Empty_To_Null()}
                , {PresiName.ConvertStr_Empty_To_Null()}
                , {CustNo.ConvertStr_Empty_To_Null()}
                , {CustRegNo.ConvertStr_Empty_To_Null()}
                , {BussinessCategory.ConvertStr_Empty_To_Null()}
                , {BussinessField.ConvertStr_Empty_To_Null()}
                , {Tel.ConvertStr_Empty_To_Null()}
                , {Fax.ConvertStr_Empty_To_Null()}
                , {Email.ConvertStr_Empty_To_Null()}
                , {HomePage.ConvertStr_Empty_To_Null()}
                , {PostNo.ConvertStr_Empty_To_Null()}
                , {Address.ConvertStr_Empty_To_Null()}
                , {Remark.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
                )
                ";

            return db.ExecuteQuery(query);
        }

        public bool Update_CM_Cust()
        {
            query = $@"

                UPDATE CM_Cust
                SET
                  CustName = {CustName.ConvertStr_Empty_To_Null()}
                , CustType = {CustType.ConvertStr_Empty_To_Null()}
                , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                , SalesMan = {SalesMan.ConvertStr_Empty_To_Null()}
                , PresiName = {PresiName.ConvertStr_Empty_To_Null()}
                , CustNo = {CustNo.ConvertStr_Empty_To_Null()}
                , CustRegNo = {CustRegNo.ConvertStr_Empty_To_Null()}
                , BussinessCategory = {BussinessCategory.ConvertStr_Empty_To_Null()}
                , BussinessField = {BussinessField.ConvertStr_Empty_To_Null()}
                , Tel = {Tel.ConvertStr_Empty_To_Null()}
                , Fax = {Fax.ConvertStr_Empty_To_Null()}
                , Email = {Email.ConvertStr_Empty_To_Null()}
                , HomePage = {HomePage.ConvertStr_Empty_To_Null()}
                , PostNo = {PostNo.ConvertStr_Empty_To_Null()}
                , Address = {Address.ConvertStr_Empty_To_Null()}
                , Remark = {Remark.ConvertStr_Empty_To_Null()}
                , ModId = '{WrGlobal.LoginID}'
                , ModIP = '{WrGlobal.ClientHostName}'
                , ModDt = GETDATE()
                WHERE Corpid = '{WrGlobal.CorpID}'
                AND CustCode = '{CustCode}'
                ";

            return db.ExecuteQuery(query);
        }


        public bool Delete_CM_Cust()
        {
            query = $@"
                DELETE CM_Cust
                WHERE Corpid = '{WrGlobal.CorpID}'
                AND CustCode = '{CustCode}'
                ";

            return db.ExecuteQuery(query);
        }
    }
}
