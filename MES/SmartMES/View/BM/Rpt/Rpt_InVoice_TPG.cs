using DevExpress.XtraReports.UI;
using SmartMES.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using SmartMES.Common;
using SmartCommon.Common;
using System.Security.Cryptography;

namespace SMT_MES.View.Business.Rpt
{
    public partial class Rpt_InVoice_TPG : DevExpress.XtraReports.UI.XtraReport
    {
        Database db = new Database();
        string query = "";

        int MaxCnt = 7;
        int Seq = 1;

        string SalesDate;

        public Rpt_InVoice_TPG()
        {
            InitializeComponent();
        }

        public Rpt_InVoice_TPG(DataTable dt, string salesDate, string custColName = "BillToCode")
        {
            InitializeComponent();

            Seq = 1;
            this.SalesDate = salesDate;

            dt.TableName = "Sales";
            if (!dt.Columns.Contains("PageInfo"))
                dt.Columns.Add("PageInfo");

            string cust_Code = "";
            string sales_Date = "";

            if(dt.Rows.Count > 0)
            {
                cust_Code = dt.Rows[0][custColName].ConvertString_Null_To_Empty();
                sales_Date = dt.Rows[0]["SalesDate"].ConvertString_Null_To_Empty();
            }

            DataRow drCust = Search_Cust(cust_Code);
            drCust.Table.TableName = "Cust";

            DataSet ds = new DataSet("C");
            ds.Tables.AddRange(new DataTable[] { drCust.Table.Copy(), dt.Clone() });
            ds.Tables["Cust"].Rows.Clear();

            ds.Relations.Add(new DataRelation("Page_Info", ds.Tables["Cust"].Columns["PageInfo"], ds.Tables["Sales"].Columns["PageInfo"]));

            int pageInfo = 1;
            CreateMasterRow(ds, pageInfo, cust_Code);

            DataRow drSales = null;

            foreach (DataRow itemRow in dt.Rows)
            {
                string curCustCode = itemRow[custColName].ConvertString_Null_To_Empty();
                string curSalesDate = itemRow["SalesDate"].ConvertString_Null_To_Empty();

                //거래처가 다를 경우 다른 페이지로 넘김
                if (cust_Code != curCustCode || sales_Date != curSalesDate)
                {
                    if (Seq <= MaxCnt)
                    {
                        CreateEmptyRow(ds.Tables["Sales"], pageInfo);
                        Seq = 1;
                    }

                    //다음 페이지 Master Row 생성
                    pageInfo++;

                    CreateMasterRow(ds, pageInfo, curCustCode);
                }

                //DetailRow가 MaxCnt보다 큰 경우 다음페이지로 넘김
                if (Seq > MaxCnt)
                {
                    //다음 페이지 Master Row 생성
                    Seq = 1;
                    //pageInfo += 1;
                    pageInfo++;

                    CreateMasterRow(ds, pageInfo, curCustCode);
                }

                drSales = ds.Tables["Sales"].NewRow();
                drSales.ItemArray = itemRow.ItemArray;
                drSales["PageInfo"] = pageInfo;
                //drSales["SalesDate"] = drSales["SalesDate"].ConvertDate_To_Char10();
                ds.Tables["Sales"].Rows.Add(drSales.ItemArray);

                cust_Code = curCustCode;
                sales_Date = curSalesDate;

                Seq++;
            }

            //MaxCnt 모자를 경우 EmptyRow 추가
            if (Seq <= MaxCnt)
            {
                CreateEmptyRow(ds.Tables["Sales"], pageInfo);
            }

            this.DataSource = ds;
            this.DataMember = "Cust";

            this.DetailReport.DataSource = ds;
            this.DetailReport.DataMember = "Cust.Page_Info";
        }

        private DataRow Search_Cust(string cust_Code)
        {
            query = $@"
                         SELECT 
                                CONVERT(VARCHAR, ROW_NUMBER() OVER (ORDER BY CorpNo)) AS PageInfo
                                , A.CorpNo 
                                , A.CorpName 
                                , A.PresiName CorpPresiName
                                , A.Address CorpAddr
                                , A.Tel CorpTel
                                , A.Fax CorpFax
                                , A.Fax CorpFax
                                , A.BussinessCategory CorpBussinessCategory
                                , A.BussinessField CorpBussinessField

            
                                , B.CustNo
                                , B.CustName
                                , B.PresiName CustPresiName
                                , B.Address AS CustAddr
                                , B.Tel AS CustTel
                                , B.Fax AS CustFax
                                , B.BussinessCategory CustBussinessCategory
                                , B.BussinessField CustBussinessField
                                , '{SalesDate}' AS SalesDate
               
                        from SM_Corp A
                        LEFT OUTER JOIN CM_Cust B on A.CorpId = B.CorpId AND B.CustCode = '{cust_Code}'          --납품처코드?
                        where A.CorpId = '{WrGlobal.CorpID}'
                      ";

            return db.GetDataRow(query);
        }

        private void CreateEmptyRow(DataTable dt, int pageInfo)
        {
            for (int i = 0; i <= MaxCnt - Seq; i++)
            {
                DataRow dr = dt.NewRow();

                dr["PageInfo"] = pageInfo;
                dt.Rows.Add(dr.ItemArray);
            }
        }

        private void CreateMasterRow(DataSet ds, int pageInfo, string cust_Cd)
        {
            DataTable dtCust = ds.Tables["Cust"];
            if (dtCust == null) return;

            DataRow drCust = Search_Cust(cust_Cd);
            if (drCust == null) return;

            //다음 페이지 Master Row 생성
            DataRow dr = dtCust.NewRow();
            dr.ItemArray = drCust.ItemArray;
            dr["PageInfo"] = pageInfo;

            dtCust.Rows.Add(dr);
        }
	}
}
