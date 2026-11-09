using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SMT_MES.View.Quality.Rpt
{
    public partial class RptOQC_v3 : DevExpress.XtraReports.UI.XtraReport
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";

        public string g_Rst_No = "";
        public string G_Rst_Row_Seq = "";
        public string ExamDivCd = "20"; //016 - 10 : 수입검사, 20 : 출하검사, 30 : 공정검사

        public RptOQC_v3()
        {
            InitializeComponent();
        }

        public RptOQC_v3(DataRow dr)
        {
            InitializeComponent();

			CellCorpName.Text = WrGlobal.CorpName;

			DataTable dtExam = SelectExam(dr);
            if (dtExam == null) return;

			if (dtExam.Rows.Count > 0)
			{
                DataRow drExam = dtExam.Rows[0];

				//CellItemClass.Text = drExam["Title"].ConvertString_Null_To_Empty();
				CellItemName.Text = drExam["ItemName"].ConvertString_Null_To_Empty();
				CellItemCode.Text = drExam["ItemCode"].ConvertString_Null_To_Empty();
				CellCorpName.Text = WrGlobal.CorpName;
				CellDate.Text = Convert.ToDateTime(drExam["SalesDate"]).ToString("yyyy년 MM월 dd일");
				CellQty.Text = drExam["Qty"].ConvertString_Null_To_Empty();
				CellCustName.Text = drExam["BillToName"].ConvertString_Null_To_Empty();
				CustName.Text = drExam["BillToName"].ConvertString_Null_To_Empty();
			}

			//02 출하검사성적서 Detail 정보
			List<XRTableCell[]> tableList = new List<XRTableCell[]>
			{
				new XRTableCell[]{ CellNo1, CellExamNm1, CellExamSpec1, CellCalMin1, CellCalMax1, CellExamMethod1, CellX1_1, CellX2_1, CellX3_1, CellX4_1, CellX5_1, CellX6_1  },
				new XRTableCell[]{ CellNo2, CellExamNm2, CellExamSpec2, CellCalMin2, CellCalMax2, CellExamMethod2, CellX1_2, CellX2_2, CellX3_2, CellX4_2, CellX5_2, CellX6_2  },
				new XRTableCell[]{ CellNo3, CellExamNm3, CellExamSpec3, CellCalMin3, CellCalMax3, CellExamMethod3, CellX1_3, CellX2_3, CellX3_3, CellX4_3, CellX5_3, CellX6_3  },
				new XRTableCell[]{ CellNo4, CellExamNm4, CellExamSpec4, CellCalMin4, CellCalMax4, CellExamMethod4, CellX1_4, CellX2_4, CellX3_4, CellX4_4, CellX5_4, CellX6_4  },
				new XRTableCell[]{ CellNo5, CellExamNm5, CellExamSpec5, CellCalMin5, CellCalMax5, CellExamMethod5, CellX1_5, CellX2_5, CellX3_5, CellX4_5, CellX5_5, CellX6_5  },
				new XRTableCell[]{ CellNo6, CellExamNm6, CellExamSpec6, CellCalMin6, CellCalMax6, CellExamMethod6, CellX1_6, CellX2_6, CellX3_6, CellX4_6, CellX5_6, CellX6_6  },
				new XRTableCell[]{ CellNo7, CellExamNm7, CellExamSpec7, CellCalMin7, CellCalMax7, CellExamMethod7, CellX1_7, CellX2_7, CellX3_7, CellX4_7, CellX5_7, CellX6_7  },
				new XRTableCell[]{ CellNo8, CellExamNm8, CellExamSpec8, CellCalMin8, CellCalMax8, CellExamMethod8, CellX1_8, CellX2_8, CellX3_8, CellX4_8, CellX5_8, CellX6_8  },
				new XRTableCell[]{ CellNo9, CellExamNm9, CellExamSpec9, CellCalMin9, CellCalMax9, CellExamMethod9, CellX1_9, CellX2_9, CellX3_9, CellX4_9, CellX5_9, CellX6_9  },
				new XRTableCell[]{ CellNo10, CellExamNm10, CellExamSpec10, CellCalMin10, CellCalMax10, CellExamMethod10, CellX1_10, CellX2_10, CellX3_10, CellX4_10, CellX5_10, CellX6_10  },
				new XRTableCell[]{ CellNo11, CellExamNm11, CellExamSpec11, CellCalMin11, CellCalMax11, CellExamMethod11, CellX1_11, CellX2_11, CellX3_11, CellX4_11, CellX5_11, CellX6_11  },
				new XRTableCell[]{ CellNo12, CellExamNm12, CellExamSpec12, CellCalMin12, CellCalMax12, CellExamMethod12, CellX1_12, CellX2_12, CellX3_12, CellX4_12, CellX5_12, CellX6_12  },
				new XRTableCell[]{ CellNo13, CellExamNm13, CellExamSpec13, CellCalMin13, CellCalMax13, CellExamMethod13, CellX1_13, CellX2_13, CellX3_13, CellX4_13, CellX5_13, CellX6_13  },
			};

            int rowIndex = 0;
            int maxCnt = tableList.Count;

			foreach (DataRow row in dtExam.Rows)
			{
				if (rowIndex >= maxCnt) return;

				tableList[rowIndex][0].Text = (rowIndex + 1).ConvertString_Null_To_Empty();
				tableList[rowIndex][1].Text = dtExam.Rows[rowIndex]["ExamNm"].ConvertString_Null_To_Empty();
				tableList[rowIndex][2].Text = dtExam.Rows[rowIndex]["ExamSpec"].ConvertString_Null_To_Empty();
				tableList[rowIndex][3].Text = dtExam.Rows[rowIndex]["Cal_Min"].ConvertString_Null_To_Empty();
				tableList[rowIndex][4].Text = dtExam.Rows[rowIndex]["Cal_Max"].ConvertString_Null_To_Empty();
				tableList[rowIndex][5].Text = dtExam.Rows[rowIndex]["Method"].ConvertString_Null_To_Empty();

				//검사횟수    Y: OK, N: NG
				for (int i = 1; i <= 6; i++)
				{
					string xValue = dtExam.Rows[rowIndex]["X" + i].ConvertString_Null_To_Empty();

					if (xValue == "Y")
					{
						tableList[rowIndex][i + 5].Text = "OK";
					}
					else if (xValue == "N")
					{
						tableList[rowIndex][i + 5].Text = "NG";
					}
					else
					{
						tableList[rowIndex][i + 5].Text = xValue;
					}
				}
                //판정  - 10:합 20:불합
                //code.GetCM_CateItem(WrGlobal.CorpID, "Q020");
				//DataTable drExam_div = code.Select_CmCd(false, new string[] { dtExam.Rows[rowIndex]["PassDivCd"].ConvertString_Null_To_Empty() });
				//tableList[rowIndex][10].Text = drExam_div.Rows[0]["Cm_Nm"].ConvertString_Null_To_Empty();

				rowIndex++;
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
						, qr.ExamEmpId

						--품목
						, qr.ItemCode AS ItemCode
						--, ccm.Title
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
						, em.ExamSpec
						, em.ExamMethod as Method
						, em.DefaultValue
						, em.MgntMin
						, em.MgntMax
						, ROUND(em.MgntMin - TRY_CAST(em.DefaultValue AS NUMERIC(13, 4)), 3) AS Cal_Min
						, ROUND(em.MgntMax - TRY_CAST(em.DefaultValue AS NUMERIC(13, 4)), 3) AS Cal_Max

						--출하
						, sm.BillToName
						, sm.SalesDate
						, sd.Qty
						, sd.Remark

					FROM QM_ExamResult qr
						LEFT JOIN CM_Item cm ON qr.CorpId = cm.CorpId AND qr.ItemCode = cm.ItemCode
						--INNER JOIN CM_CateItem ccm ON cm.CorpId = ccm.CorpId AND cm.ItemClass1 = ccm.ItemCode 
						LEFT JOIN QM_ItemExamMgnt em ON qr.CorpId = em.CorpId AND qr.ExamDivCd = em.ExamDivCd AND qr.ExamCd = em.ExamCd and qr.ItemCode = em.ItemCode
						INNER JOIN QM_ExamMgnt mn ON qr.CorpId = em.CorpId AND mn.ExamDivCd = mn.ExamDivCd AND qr.ExamCd = mn.ExamCd
						LEFT JOIN BM_SalesD sd on qr.CorpId = sd.CorpId AND qr.RefNo = sd.SalesNo AND qr.RefSeq = sd.SalesSeq
						LEFT JOIN BM_SalesM sm ON qr.CorpId = sm.CorpId AND qr.RefNo = sm.SalesNo
					WHERE qr.CorpId = '{WrGlobal.CorpID}'
						AND qr.ExamDivCd = '{ExamDivCd}'
						AND qr.RefNo = '{dr["RefNo"]}'
						AND qr.RefSeq = '{dr["RefSeq"]}'
						--AND ccm.CLASS = 'A01210'
            ";

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

		public DataRow Select_LastImg(DataRow dr)
		{
			string ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
			string itemCode = "30"; //30:검사기준서

			query = $@"
                       SELECT TOP 1 CorpId, ItemCode, Seq, Pic, Path, FileExt, Title, Date
                       FROM CM_ItemImage img
                       WHERE img.CorpId = '{WrGlobal.CorpID}'
                       AND img.ItemCode = '{ItemCode}'
                       AND img.ItemCode = '{itemCode}'
						ORDER BY Seq DESC";

			return db.GetDataRow(query);
		}
	}
}
