using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using SMT_MES.Model;
using SmartPOP.Common;

namespace SmartMES.Report
{
    public partial class Rpt_IW_Injection : DevExpress.XtraReports.UI.XtraReport
    {
        Database db = new Database();

        /*
            IWAUTO 원자재 부자재 구분 레포트 적용
            업체 번호 판단해서 바인딩 값 다르게 해주가
            ex) IWAUTO이면 원자재인식표, 부자재인식표 구분이 필요함 원, 부자재에 따라서도 값 

            IW오토모티브 원자재 부자재 판단 40 : 원자재, 50 : 부자재
        */

        public Rpt_IW_Injection(DataTable labelList)
        {
            InitializeComponent();
            #region 01. Report에 넣을 DataTable 

            //01-1. 필요한 칼럼들 labelList에 빈 칼럼으로 추가
            //labelList.Columns.Add("CustName", typeof(string));
            labelList.Columns.Add("xrLabel1", typeof(string));
            labelList.Columns.Add("labelCustNm", typeof(string));
            labelList.Columns.Add("label2", typeof(string));
            labelList.Columns.Add("label3", typeof(string));
            labelList.Columns.Add("label4", typeof(string));
            labelList.Columns.Add("Page", typeof(int));

            //01-2. 추가한 칼럼에 데이터 넣기
            foreach (DataRow row in labelList.Rows)
            {
                string div = row["MaterialType"].ToString();

                if (div == "40") // 원자재
                {
                    row["xrLabel1"] = "원자재 불출전표";
                    row["labelCustNm"] = "거 래 처";
                    row["label2"] = "원자재코드";
                    row["label3"] = "원자재명";
                    row["label4"] = "납 품 사";
                }
                //else if (div == "50") // 부자재
                //{
                //    row["xrLabel1"] = "부자재 불출전표";
                //    row["labelCustNm"] = "고 객 사";
                //    row["label2"] = "부자재명";
                //    row["label3"] = "남 품 사";
                //    row["label4"] = "차     종";
                //}
                //else if (div == "20") // 공정이동
                //{
                //    row["xrLabel1"] = "공정이동표";
                //    row["labelCustNm"] = "고 객 사";
                //    row["label2"] = "제 품 명";
                //    row["label3"] = "남 품 사";
                //    row["label4"] = "차     종";
                //}
            }

            //01-3. Item_Div_Cd를 DESC 순으로 정렬
            labelList.DefaultView.Sort = "StoNo, StoSeq ASC";
            DataTable labelList_sort = labelList.DefaultView.ToTable();

            //01-4. 재정렬된 labelList를 DataSource로 설정
            this.DataSource = labelList_sort;

            #endregion  DataTable 준비끝

            #region 02. 데이터 바인딩

            //02. 데이터 바인딩
            foreach (DataRow row in labelList_sort.Rows)
            {

                //02-1. 라벨 데이터바인딩
                xrLabel1.DataBindings.Add("Text", labelList_sort, "xrLabel1");
                labelCustNm.DataBindings.Add("Text", labelList_sort, "labelCustNm");
                label2.DataBindings.Add("Text", labelList_sort, "label2");
                label3.DataBindings.Add("Text", labelList_sort, "label3");
                label4.DataBindings.Add("Text", labelList_sort, "label4");          

                //02-2. 셀 데이터바인딩(공통)                
                CellCustNm.DataBindings.Add("Text", labelList_sort, "CustName");                    //고객사   //거래처(계산하는업체)                              
                Cell2.DataBindings.Add("Text", labelList_sort, "MaterialCode");                     //부자재코드 
                Cell3.DataBindings.Add("Text", labelList_sort, "materialName");                     //부자재명 //재질(품명)                      
                CellQty.DataBindings.Add("Text", labelList_sort, "Order_Qty", "{0:#,0}");           //발주수량               
                CellInQty.DataBindings.Add("Text", labelList_sort, "Qty", "{0:#,0}");               //입고수량
                CellLotNo.DataBindings.Add("Text", labelList_sort, "LotNo");                        //LotNo
                xrBarCode1.DataBindings.Add("Text", labelList_sort, "LotNo");                       //LotNo                
                CellInDate.DataBindings.Add("Text", labelList_sort, "StoDate", "{0:yyyy-MM-dd}");   //입고일자                
                CellRmk.DataBindings.Add("Text", labelList_sort, "Remark");                         //특이사항               
                xrLabel24.DataBindings.Add("Text", labelList_sort, "PassResult");                     //수입검사 - 판정
                xrLabel23.DataBindings.Add("Text", labelList_sort, "PassResult");                     //수입검사 - 판정

                //02-3. 셀 데이터바인딩  -  원자재 부자재 구분에 따라 셀의 바인딩 데이터(칼럼)이 달라지는 항목들
                //string materials = Convert.ToString(row["MaterialType"]);

                //if (materials == "40")
                //{
                //    //GRADE
                //    Cell3.DataBindings.Add("Text", labelList_sort, "Grade");

                //    //납품사(고객사 Vendor_Cd)(물건 왔다갔다하는 업체)
                //    DataTable dtOutCust = Search_CustName(labelList_sort.Rows[0]["Direct_Cust_Cd"].ToString());
                //    if (dtOutCust.Rows.Count < 1) { Cell4.Text = "";}
                //    else { Cell4.Text = dtOutCust.Rows[0]["Cust_Nm"].ToString();}
                //}
                //else if (materials == "50")
                //{
                //    //남품사
                //    DataTable dtOutCust = Search_CustName(labelList_sort.Rows[0]["Direct_Cust_Cd"].ToString());
                //    if (dtOutCust.Rows.Count < 1) { Cell4.Text = ""; }
                //    else { Cell3.Text = dtOutCust.Rows[0]["Cust_Nm"].ToString(); }

                //    //차종
                //    Cell4.DataBindings.Add("Text", labelList_sort, "ItemClass_Nm");
                //}
            }

            #endregion #region 02. 데이터 바인딩 end

        }

        #region Query
        private DataTable Search_CustName(string cust_Cd)
        {
            string query1 = "";
            query1 += $@"SELECT Cust_Nm
                         FROM BS_Customer
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
						and Cust_Cd = '{cust_Cd}'";

            return db.GetDataTable(query1);
        }

        private DataTable Search_ItemName(string item_Cd)
        {
            string query2 = "";
            query2 += $@"SELECT Item_Nm
                         FROM BS_Item
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
						and Item_Cd = '{item_Cd}'";

            return db.GetDataTable(query2);
        }
        #endregion #region
    }
}
