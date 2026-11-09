using SmartPOP.Common;
using SmartPOP.Models;
using System.Data;

namespace SmartPOP.View.QC
{
	public partial class RptOQC_TPG : DevExpress.XtraReports.UI.XtraReport
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";

        public string g_Rst_No = "";
        public string G_Rst_Row_Seq = "";
        public string Exam_Div_Cd = "20"; //016 - 10 : 수입검사, 20 : 출하검ㄴ사, 30 : 공정검사

        public RptOQC_TPG()
        {
            InitializeComponent();
        }

        public RptOQC_TPG(DataRow dr)
        {
            InitializeComponent();

			DataTable dtExam = SelectExam(dr);
			if (dtExam == null) return;

			this.DataSource = dtExam;

			if (dtExam.Rows.Count > 0)
			{
				DataRow drExam = dtExam.Rows[0];
                CellMaterialName.Text = drExam["ItemName"].ConvertString_Null_To_Empty();
                CellSize.Text = drExam["Size"].ConvertString_Null_To_Empty();
                CellQty.Text = drExam["ExamQty"].ConvertString_Null_To_Empty();
                CellExamDate.Text = drExam["ExamDate"].ConvertDate_To_Char10();
                CellCustName.Text = drExam["CustName"].ConvertString_Null_To_Empty();
                CellExamEmp.Text = drExam["ExamEmpName"].ConvertString_Null_To_Empty();
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
                        , CONVERT(DATE, qr.ExamDate) ExamDate
                        , qr.ExamEmpId
                        , cu.EmployeeName ExamEmpName

                        --품목
                        , qr.ItemCode
                        , cm.ItemName
                        , cm.Alias
                        , cm.Size
                        , cm.Texture

                        , qr.ExamDivCd
                        , qr.ExamCd
                        , qr.RefNo
                        , qr.RefSeq
                        , qr.X1
                        , qr.X2
                        , qr.X3
                        , qr.X4
                        , qr.X5
                        , qr.X6
                        , qr.ExamQty
                        , em.ExamSpec
                        , em.ExamMethod

                        --출하
                        , sm.BillToName
                        , cc.CustName
                        , sm.SalesDate
                        , sd.Qty
                        , sd.Remark

                        FROM QM_ExamResult qr
                        LEFT JOIN CM_Item cm ON qr.CorpId = cm.CorpId AND qr.ItemCode = cm.ItemCode
                        LEFT JOIN QM_ItemExamMgnt em ON qr.CorpId = em.CorpId AND qr.ExamDivCd = em.ExamDivCd AND qr.ExamCd = em.ExamCd AND qr.ItemCode = em.ItemCode
                        LEFT JOIN QM_ExamMgnt mn ON qr.CorpId = em.CorpId AND mn.ExamDivCd = mn.ExamDivCd AND qr.ExamCd = mn.ExamCd
                        LEFT JOIN BM_SalesD sd on qr.CorpId = sd.CorpId AND qr.RefNo = sd.SalesNo AND qr.RefSeq = sd.SalesSeq
                        LEFT JOIN BM_SalesM sm ON qr.CorpId = sm.CorpId AND qr.RefNo = sm.SalesNo 
                        LEFT JOIN CM_Cust cc ON qr.CorpId = cc.CorpId AND sm.BillToCode = cc.CustCode
                        LEFT JOIN SM_User cu ON qr.CorpId = cu.CorpId AND qr.ExamEmpId = cu.EmployeeId
                        WHERE qr.CorpId = '{WrGlobal.CorpID}'
                        AND qr.ExamDivCd = '{Exam_Div_Cd}'
                        AND qr.RefNo = '{dr["RefNo"]}'
                        AND qr.RefSeq = '{dr["RefSeq"]}'

            ";

            DataTable dt = db.GetDataTable(query);

            return dt;
        }
    }
}
