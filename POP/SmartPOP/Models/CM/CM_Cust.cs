
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.CM
{
    public class CM_Cust
    { 

        #region Property

        public string CorpId { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string CustEngName { get; set; }
        public string CustCodeCustom { get; set; }
        public string CustType { get; set; }
        public string ExchangeType { get; set; }
        public string VatType { get; set; }
        public string CustNo { get; set; }
        public string CustRegNo { get; set; }
        public string PresiName { get; set; }
        public string CitizeNo { get; set; }
        public string PostNo { get; set; }
        public string UseYn { get; set; }
        public string Remark { get; set; }
        public string Address { get; set; }
        public string AddressEng { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }
        public string Email { get; set; }
        public string BussinessCategory { get; set; }
        public string BussinessField { get; set; }
        public string OpenDate { get; set; }
        public string CloseDate { get; set; }
        public string SalesMan { get; set; }
        public string SoldToCode { get; set; }
        public string RecentlyUsedYN { get; set; }
        public string OrderType { get; set; }
        public string Remarks { get; set; }
        public string HubReleaseType { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public string CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public string ModDt { get; set; }

        #endregion

        Database db = new Database();

        string query;

        public DataTable Select_CM_Cust()
		{
            query =  $@"SELECT 'ALL' 거래처코드, '전체' 거래처명 
                        UNION ALL 
                        SELECT distinct CustCode 거래처코드, CustName 거래처명 
                        FROM CM_Cust 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CustType = '{CustType}' ";

            //조회조건
            if (CustType.ToUpper() != "ALL")
            {
                //거래처구분
                query += $" AND CustType = '{CustType}' ";
            }

            return db.GetDataTable(query);
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

        public DataRow Check_Existence()
        {
            query = $@"SELECT 1 FROM CM_Cust WHERE CustCode = '{CustCode}'";
            return db.GetDataRow(query);
        }
    }
}
