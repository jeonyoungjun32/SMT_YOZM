using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace SMT_MES.View.Quality.Rpt
{
	public partial class RptIQC_TPG : DevExpress.XtraReports.UI.XtraReport
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";

        public string g_Rst_No = "";
        public string G_Rst_Row_Seq = "";
        public string ExamDivCd = "10"; //016 - 10 : 수입검사, 20 : 출하검사, 30 : 공정검사

        public RptIQC_TPG()
        {
            InitializeComponent();
        }

        public RptIQC_TPG(DataRow dr)
        {
            InitializeComponent();


			DataTable dtExam = SelectExam(dr);

            this.DataSource = dtExam;

            query = $@"SELECT * FROM CM_CateItem WHERE CLASS = 'Q005' AND ClassItemCode = '10'";
            DataRow drCCi = db.GetDataRow(query);

            if(drCCi != null)
			{
                CellDocNo.Text = drCCi["Remark"].ConvertString_Null_To_Empty();//문서번호
            }
            
            if (dtExam.Rows.Count > 0)
			{
				DataRow drExam = dtExam.Rows[0];

                CellItemDiv.Text = drExam["ItemTypeName"].ConvertString_Null_To_Empty() + "(" + drExam["ItemClass1Name"] + ")";
                //CellExamEmp.Text = drExam["EmployeeName"].ConvertString_Null_To_Empty();

                //string result = dtExam.AsEnumerable().All(row => row.Field<string>("PassDivCd") == "10")? "합격": "불합격";
                //CellPassDiv.Text = result;
            }
		}

        private DataTable SelectExam(DataRow dr)
        {
            query = $@"

                        SELECT 
                          qr.ExamNo
                        , mn.ExamNm
                        , mn.ExamMethod
                        , qr.ExamSeq
                        , qr.ExamDate

                        --품목
                        , qr.ItemCode AS ItemCode
                        , cm.ItemName
                        , cm.Alias
                        , cm.Size
                        , cm.Texture
                        , (select dbo.FN_Get_CateItem_Title (qr.CorpId, 'A010', cm.ItemType)  ) ItemTypeName
                        , (select dbo.FN_Get_CateItem_Title (qr.CorpId, 'A01210', cm.ItemClass1)  ) ItemClass1Name

                        , qr.ExamDivCd
                        , qr.ExamCd
                        , qr.RefNo
                        , qr.RefSeq
                        , qr.ExamQty
                        , CONVERT(DATE, qr.PassDate) PassDate
                         , CASE WHEN em.ValueTypeDivCd = '10' AND qr.X1 = 'Y' THEN '양호' 
						       WHEN em.ValueTypeDivCd = '10' AND qr.X1 = 'N' THEN '불량'
						  ELSE qr.X1 END AS X1 
                        , CASE WHEN em.ValueTypeDivCd = '10' AND qr.X2 = 'Y' THEN '양호' 
						       WHEN em.ValueTypeDivCd = '10' AND qr.X2 = 'N' THEN '불량'
						  ELSE qr.X2 END AS X2 
					    , CASE WHEN  em.ValueTypeDivCd = '10' AND qr.X3 = 'Y' THEN '양호' 
						       WHEN  em.ValueTypeDivCd = '10' AND qr.X3 = 'N' THEN '불량'
						  ELSE qr.X3 END AS X3 
                        , qr.PassDivCd
                        , qr.ExamEmpId
                        , cu.EmployeeName ExamEmpName
                        , em.ExamSpec
                        , em.ExamMethod
                        , CASE 
                            WHEN SUM(CASE WHEN qr.PassDivCd <> '10' THEN 1 ELSE 0 END)
                                 OVER (PARTITION BY qr.RefNo, qr.RefSeq) = 0
                            THEN '합격'
                            ELSE '불합격' END AS AllPass

                        --출하
                        , sm.CustCode
                        , cc.CustName
                        , sm.StoDate
                        , sd.Qty
                        , sd.Remark

                        FROM QM_ExamResult qr
                        LEFT JOIN QM_ExamMgnt mn ON qr.CorpId = mn.CorpId AND mn.ExamDivCd = mn.ExamDivCd AND qr.ExamCd = mn.ExamCd
                        LEFT JOIN MM_PurchaseD sd on qr.CorpId = sd.CorpId AND qr.RefNo = sd.StoNo AND qr.RefSeq = sd.StoSeq
                        LEFT JOIN MM_PurchaseM sm ON qr.CorpId = sm.CorpId AND qr.RefNo = sm.StoNo 
                        LEFT JOIN CM_Item cm ON qr.CorpId = cm.CorpId AND qr.ItemCode = cm.ItemCode
                        LEFT JOIN QM_ItemExamMgnt em ON qr.CorpId = em.CorpId AND qr.ExamDivCd = em.ExamDivCd AND qr.ExamCd = em.ExamCd  AND qr.ItemCode = em.ItemCode
                        LEFT JOIN CM_Cust cc ON sm.CorpId = cc.CorpId AND sm.CustCode = cc.CustCode
                        LEFT JOIN SM_User cu ON qr.CorpId = cu.CorpId AND qr.ExamEmpId = cu.EmployeeId 
                        WHERE qr.CorpId = '{WrGlobal.CorpID}'
                        AND qr.ExamDivCd = '{ExamDivCd}'
                        AND qr.RefNo = '{dr["RefNo"]}'
                        AND qr.RefSeq = '{dr["RefSeq"]}'

            ";

            DataTable dt = db.GetDataTable(query);

            return dt;
        }
    }
}
