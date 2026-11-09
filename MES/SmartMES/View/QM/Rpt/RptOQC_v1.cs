using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace SMT_MES.View.Quality.Rpt
{
    public partial class RptOQC_v1 : DevExpress.XtraReports.UI.XtraReport
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";

        public string g_Rst_No = "";
        public string G_Rst_Row_Seq = "";
        public string ExamDivCd = "20"; //016 - 10 : 수입검사, 20 : 출하검사, 30 : 공정검사

        public RptOQC_v1()
        {
            InitializeComponent();
        }

        public RptOQC_v1(DataRow dr)
        {
            InitializeComponent();

            CellCorpName.Text = WrGlobal.CorpName;

			DataTable dtExam = SelectExam(dr);
            if (dtExam == null) return;

			if (dtExam.Rows.Count > 0)
			{
                DataRow drExam = dtExam.Rows[0];

                CellSize.Text = drExam["Size"].ConvertString_Null_To_Empty();
                CellSalesDate.Text = Convert.ToDateTime(DateTime.Today).ToString("yyyy년 MM월 dd일");
                CellItemName.Text = drExam["ItemName"].ConvertString_Null_To_Empty();
                CellTexture.Text = drExam["Texture"].ConvertString_Null_To_Empty();
                CellQty.Text = drExam["Qty"].ConvertString_Null_To_Empty();
                CellCustName.Text = drExam["BillToName"].ConvertString_Null_To_Empty();
				CustName.Text = drExam["BillToName"].ConvertString_Null_To_Empty();
			}

			//출하검사 횟수
			DataRow drOQCCnt = SelectOQC_ExamCnt(dr);

			if (drOQCCnt != null)
			{
				string cnt = drOQCCnt["Value"].ConvertString_Null_To_Zero();
				CellSampleQty.Text = cnt;
			}

			//이미지
			DataRow drImg = Select_LastImg(dr);

			if(drImg != null)
			{
				byte[] bytes = drImg["Pic"] as byte[];

				if (bytes.Length > 0)
				{
					string ext = Convert.ToString(drImg["FileExt"]).ToUpper();

					if (ext == ".BMP" || ext == ".JPG" || ext == ".JPEG" || ext == ".PNG")
					{
						MemoryStream mem = new MemoryStream(bytes);
						Bitmap bmp = new Bitmap(mem);
						Image img = bmp;

						xrPictureBox1.ImageSource = new ImageSource(img);
					}
				}
			}

			//02 출하검사성적서 Detail 정보
			List<XRTableCell[]> tableList = new List<XRTableCell[]>
					{
						new XRTableCell[]{ CellNo1, CellExamNm1, CellExamSpec1, CellMgnt1, CellX1_1, CellX2_1, CellX3_1, CellX4_1, CellX5_1, CellX6_1  },
						new XRTableCell[]{ CellNo2, CellExamNm2, CellExamSpec2, CellMgnt2, CellX1_2, CellX2_2, CellX3_2, CellX4_2, CellX5_2, CellX6_2  },
						new XRTableCell[]{ CellNo3, CellExamNm3, CellExamSpec3, CellMgnt3, CellX1_3, CellX2_3, CellX3_3, CellX4_3, CellX5_3, CellX6_3  },
						new XRTableCell[]{ CellNo4, CellExamNm4, CellExamSpec4, CellMgnt4, CellX1_4, CellX2_4, CellX3_4, CellX4_4, CellX5_4, CellX6_4  },
						new XRTableCell[]{ CellNo5, CellExamNm5, CellExamSpec5, CellMgnt5, CellX1_5, CellX2_5, CellX3_5, CellX4_5, CellX5_5, CellX6_5  },
						new XRTableCell[]{ CellNo6, CellExamNm6, CellExamSpec6, CellMgnt6, CellX1_6, CellX2_6, CellX3_6, CellX4_6, CellX5_6, CellX6_6  },
						new XRTableCell[]{ CellNo7, CellExamNm7, CellExamSpec7, CellMgnt7, CellX1_7, CellX2_7, CellX3_7, CellX4_7, CellX5_7, CellX6_7  },
						new XRTableCell[]{ CellNo8, CellExamNm8, CellExamSpec8, CellMgnt8, CellX1_8, CellX2_8, CellX3_8, CellX4_8, CellX5_8, CellX6_8  },
						new XRTableCell[]{ CellNo9, CellExamNm9, CellExamSpec9, CellMgnt9, CellX1_9, CellX2_9, CellX3_9, CellX4_9, CellX5_9, CellX6_9  },
						new XRTableCell[]{ CellNo10, CellExamNm10, CellExamSpec10, CellMgnt10, CellX1_10, CellX2_10, CellX3_10, CellX4_10, CellX5_10, CellX6_10  },
					};

            int rowIndex = 0;
            int maxCnt = 10;

			foreach (DataRow row in dtExam.Rows)
			{
				if (rowIndex >= maxCnt) return;

				string mgnt = dtExam.Rows[rowIndex]["Cal_Min"].ConvertString_Null_To_Empty();
				mgnt += " /\n";
				mgnt += dtExam.Rows[rowIndex]["Cal_Max"].ConvertString_Null_To_Empty();

				tableList[rowIndex][0].Text = (rowIndex + 1).ConvertString_Null_To_Empty();
				tableList[rowIndex][1].Text = dtExam.Rows[rowIndex]["ExamNm"].ConvertString_Null_To_Empty();	//검사명
				tableList[rowIndex][2].Text = dtExam.Rows[rowIndex]["ExamSpec"].ConvertString_Null_To_Empty(); //규격
				if (mgnt.Length > 3)
				{
					//허용공차 (관리범위 하한값/관리범위 상한값)
					tableList[rowIndex][3].Text = mgnt;
				}

				//검사방법
				//tableList[rowIndex][3].Text = dtExam.Rows[rowIndex]["ExamMethod"].ConvertString_Null_To_Empty();
				//검사횟수    Y: OK, N: NG
				for (int i = 1; i <= 6; i++)
				{
					string xValue = dtExam.Rows[rowIndex]["X" + i].ConvertString_Null_To_Empty();

					if (xValue == "Y")
					{
						tableList[rowIndex][i + 3].Text = "OK";
					}
					else if (xValue == "N")
					{
						tableList[rowIndex][i + 3].Text = "NG";
					}
					else
					{
						tableList[rowIndex][i + 3].Text = xValue;
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
						, em.ExamMethod
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
						LEFT JOIN QM_ItemExamMgnt em ON qr.CorpId = em.CorpId AND qr.ExamDivCd = em.ExamDivCd AND qr.ExamCd = em.ExamCd and qr.ItemCode = em.ItemCode
						INNER JOIN QM_ExamMgnt mn ON qr.CorpId = em.CorpId AND mn.ExamDivCd = mn.ExamDivCd AND qr.ExamCd = mn.ExamCd
						LEFT JOIN BM_SalesD sd on qr.CorpId = sd.CorpId AND qr.RefNo = sd.SalesNo AND qr.RefSeq = sd.SalesSeq
						LEFT JOIN BM_SalesM sm ON qr.CorpId = sm.CorpId AND qr.RefNo = sm.SalesNo
					WHERE qr.CorpId = '{WrGlobal.CorpID}'
						AND qr.ExamDivCd = '{ExamDivCd}'
						AND qr.RefNo = '{dr["RefNo"]}'
						AND qr.RefSeq = '{dr["RefSeq"]}'
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

		public DataRow SelectOQC_ExamCnt(DataRow dr)
		{
			query = $@"
						SELECT
							Task_Div
                            , Option_Nm
                            , Value

                        FROM SC_Switch
                        WHERE CorpID = '{WrGlobal.CorpID}'
						AND Task_Div = 'QT'
						AND Option_Nm = 'OQC_ExamCnt' ";
			return db.GetDataRow(query);
		}
	}
}
