using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using SmartPOP.Common;

namespace SMT_MES.View.Material.Rpt
{
	public partial class Rpt_Injection : DevExpress.XtraReports.UI.XtraReport
	{
		Database db = new Database();

		public Rpt_Injection(DataTable labelList, string in_Div_Cd = "")
		{
			InitializeComponent();

            this.DataSource = labelList;

			//업체
			string custCd = labelList.Rows[0]["CustCode"].ToString();
			CellCustNm.Text = labelList.Rows[0]["CustName"].ToString();

			//품명
			string itemCd = 
			CellItemNm.Text = labelList.Rows[0]["ItemName"].ToString();

			CellSpec.DataBindings.Add("Text", labelList, "Spec");                        //사양

			//if(in_Div_Cd == "20")
   //         {
			//	CellOrderQty.DataBindings.Add("Text", labelList, "ROrder_Qty", "{0:#,0}");        //발주수량=수주수량(사출품입고)
   //         }
   //         else
            //{
				CellOrderQty.DataBindings.Add("Text", labelList, "PlacingQty", "{0:#,0}");        //발주수량
            //}

			CellInQty.DataBindings.Add("Text", labelList, "Qty", "{0:#,0}");          //입고수량
			CellLotNo.DataBindings.Add("Text", labelList, "LotNo");                  //LotNo
			xrBarCode1.DataBindings.Add("Text", labelList, "LotNo");                 //LotNo
			CellInDate.DataBindings.Add("Text", labelList, "StoDate", "{0:yyyy-MM-dd}");  //입고일자
			CellRmk.DataBindings.Add("Text", labelList, "Remark");                          //특이사항
			CheckResult.DataBindings.Add("Text", labelList, "IQC_YN");                   //수입검사 - 결과
			CheckPass.DataBindings.Add("Text", labelList, "IQC_YN");                     //수입검사 - 판정
		}
	}
}
