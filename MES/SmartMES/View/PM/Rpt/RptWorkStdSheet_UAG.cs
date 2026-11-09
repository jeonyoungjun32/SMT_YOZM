using SmartCommon.Common;
using System.ComponentModel;
using System.Data;

namespace SmartMES.View.PM.Rpt
{
    public partial class RptWorkStdSheet_UAG : DevExpress.XtraReports.UI.XtraReport
	{
		Database db = new Database();
		string query = "";

		public RptWorkStdSheet_UAG()
		{
			InitializeComponent();
		}

		public RptWorkStdSheet_UAG(DataTable dt)
		{
			InitializeComponent();

			DataTable dtR = new DataTable();

/*
			foreach (DataRow item in dt.Rows)
			{
				string rst_no = item["Rst_No"].ConvertString_Null_To_Empty();
				string rst_row_Seq = item["Rst_Row_Seq"].ConvertString_Null_To_Zero();
				string lR_Div_Cd = item["LR_Div_Cd"].ConvertString_Null_To_Zero();

				DataRow drRst = SelectRst(rst_no, rst_row_Seq);

				if (dtR.Columns.Count == 0)
				{
					dtR = drRst.Table.Clone();
					dtR.Columns.Add("Image", typeof(Image));
				}

				DataRow newRow = dtR.NewRow();

				for (int i = 0; i < drRst.ItemArray.Length; i++)
				{
					newRow[i] = drRst[i];
				}

				//// L/R 구분 이미지
				//if (lR_Div_Cd == "10") // L
				//{
				//	newRow["Image"] = Properties.Resources.Red_L;
				//}
				//else if (lR_Div_Cd == "20") // R
				//{
				//	newRow["Image"] = Properties.Resources.Purple_R;
				//}
				//else
				//{
				//	newRow["Image"] = null;
				//}

				dtR.Rows.Add(newRow);
			}

			dtR.TableName = "Rst";

			//검사값
			DataTable dtExam = SelectExam("", "0", "").Copy().Clone();
			dtExam.TableName = "Exam";

			//바코드
			DataTable dtMat = SelectMatNo("", "0").Copy().Clone();
			dtMat.TableName = "Mat";

			DataSet ds = new DataSet("C");
			//ds.Tables.AddRange(new DataTable[] { dtExam, dtR });
			ds.Tables.AddRange(new DataTable[] { dtExam, dtR, dtMat });

			foreach (DataRow dr in dtR.Rows)
			{
				string rst_No = Convert.ToString(dr["Rst_No"]);
				string rst_Row_Seq = Convert.ToString(dr["Rst_Row_Seq"]);
				string ItemCode = Convert.ToString(dr["ItemCode"]);

				dtExam.Merge(SelectExam(rst_No, rst_Row_Seq, ItemCode));

				//바코드
				dtMat.Merge(SelectMatNo(rst_No, rst_Row_Seq));
			}


			ds.Relations.Add(new DataRelation("Rst_Exam"
				, new DataColumn[] { ds.Tables["Rst"].Columns["Rst_No"], ds.Tables["Rst"].Columns["Rst_Row_Seq"] }
				, new DataColumn[] { ds.Tables["Exam"].Columns["RefNo"], ds.Tables["Exam"].Columns["RefSeq"] }));

			ds.Relations.Add(new DataRelation("Rst_BarCode"
		  , new DataColumn[] { ds.Tables["Rst"].Columns["Rst_No"], ds.Tables["Rst"].Columns["Rst_Row_Seq"] }
		  , new DataColumn[] { ds.Tables["Mat"].Columns["Rst_No"], ds.Tables["Mat"].Columns["Rst_Row_Seq"] }));


			this.DataSource = ds;
			this.DataMember = "Rst";

			xrBarCode1.ExpressionBindings.Clear();
			xrBarCode2.ExpressionBindings.Clear();
			xrBarCode3.ExpressionBindings.Clear();

			*//*
			if (dtMat.Rows[0]["MatLotNo1"].ConvertString_Null_To_Empty() != "")
			{
				xrBarCode1.Visible = true;

				xrBarCode1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
					new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo1]")});
			}

			if (dtMat.Rows[0]["MatLotNo2"].ConvertString_Null_To_Empty() != "")
			{
				xrBarCode2.Visible = true;

				xrBarCode2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
					new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo2]")});
			}

			if (dtMat.Rows[0]["MatLotNo3"].ConvertString_Null_To_Empty() != "")
			{
				xrBarCode3.Visible = true;

				xrBarCode3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
					new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo3]")});
			}
			*//*

			xrBarCode1.ExpressionBindings.AddRange(new ExpressionBinding[] {new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo1]")});
			xrBarCode2.ExpressionBindings.AddRange(new ExpressionBinding[] {new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo2]")});
			xrBarCode3.ExpressionBindings.AddRange(new ExpressionBinding[] {new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo3]")});
			xrBarCode4.ExpressionBindings.AddRange(new ExpressionBinding[] {new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo4]")});
			xrBarCode5.ExpressionBindings.AddRange(new ExpressionBinding[] {new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo5]")});
			xrBarCode6.ExpressionBindings.AddRange(new ExpressionBinding[] {new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo6]")});
			xrBarCode7.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo7]")});
			xrBarCode8.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_BarCode].[MatLotNo8]") });

			Cell_ExamSpec1.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec1]")});
			Cell_ExamSpec2.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec2]")});
			Cell_ExamSpec3.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec3]")});
			Cell_ExamSpec4.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec4]")});
			Cell_ExamSpec5.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec5]")});
			Cell_ExamSpec6.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec6]")});
			Cell_ExamSpec7.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec7]")});
			Cell_ExamSpec8.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec8]")});
			Cell_ExamSpec9.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec9]")});
			Cell_ExamSpec10.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec10]")});
			Cell_ExamSpec11.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec11]")});
			Cell_ExamSpec12.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec12]")});
			Cell_ExamSpec13.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec13]")});
			Cell_ExamSpec14.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec14]")});
			Cell_ExamSpec15.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec15]")});
			Cell_ExamSpec16.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec16]")});
			Cell_ExamSpec17.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec17]")});
			Cell_ExamSpec18.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec18]")});
			Cell_ExamSpec19.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec19]")});
			Cell_ExamSpec20.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamSpec20]")});

			//Cell_NUM1.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit1]") });
			//Cell_NUM2.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit2]") });
			//Cell_NUM3.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit3]") });
			//Cell_NUM4.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit4]") });
			//Cell_NUM5.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit5]") });
			//Cell_NUM6.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit6]") });
			//Cell_NUM7.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit7]") });
			//Cell_NUM8.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit8]") });
			//Cell_NUM9.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit9]") });
			//Cell_NUM10.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit10]") });
			//Cell_NUM11.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit11]") });
			//Cell_NUM12.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit12]") });
			//Cell_NUM13.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit13]") });
			//Cell_NUM14.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit14]") });
			//Cell_NUM15.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit15]") });
			//Cell_NUM16.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit16]") });
			//Cell_NUM17.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit17]") });
			//Cell_NUM18.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit18]") });
			//Cell_NUM19.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit19]") });
			//Cell_NUM20.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamUnit20]") });

			Cell_NUM1.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod1]") });
			Cell_NUM2.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod2]") });
			Cell_NUM3.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod3]") });
			Cell_NUM4.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod4]") });
			Cell_NUM5.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod5]") });
			Cell_NUM6.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod6]") });
			Cell_NUM7.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod7]") });
			Cell_NUM8.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod8]") });
			Cell_NUM9.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod9]") });
			Cell_NUM10.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod10]") });
			Cell_NUM11.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod11]") });
			Cell_NUM12.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod12]") });
			Cell_NUM13.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod13]") });
			Cell_NUM14.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod14]") });
			Cell_NUM15.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod15]") });
			Cell_NUM16.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod16]") });
			Cell_NUM17.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod17]") });
			Cell_NUM18.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod18]") });
			Cell_NUM19.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod19]") });
			Cell_NUM20.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "[Rst_Exam].[ExamMethod20]") });

*/
		}

		private DataRow SelectRst(string rst_no, string rst_row_Seq)
		{
			query = $@"
                        SELECT 
                             CONVERT(DATE, A.Rst_Date) AS Rst_Date
							, A.Start_Time
	                        , D.Machine_Nm
	                        , F.Emp_Nm

	                        , ic.ItemClass_Nm

	                        , A.ItemCode
	                        , E.Item_Nm
	                        , E.Alias
	                        , E.Base_Weight
	                        , E.PackQty
	                        , E.sPackQty
							, E.FieldPackUnit
							, E.FieldPackQty
							, E.Type
							, E.ShortName

	                        , A.Rst_No
	                        , A.Rst_Row_Seq
	                        , A.Work_Qty
	                        , A.Work_Weight
	                        , A.Good_Qty
	                        , A.Bad_Qty
							, CONVERT(DateTime, SUBSTRING(A.Rst_No, 3,8)) AS RstDate 

						    , pci.PdLot_No

							, (select dbo.[FN_Get_Worker] ('{WrGlobal.CorpID}', A.Rst_No, A.Rst_Row_Seq )) AS WorkEmp_Nm -- 작업자 외 N명으로 표기

						    --소재입고일
						    , (
                                SELECT TOP 1 CONVERT(DATE, In_Date) 
                                FROM MT_MatOut 
                                WHERE CorpID = A.CorpID 
                                AND Rst_No = A.Rst_No AND Rst_Row_Seq = A.Rst_Row_Seq 
                                ORDER BY MatLot_No
                                ) AS In_Date    

                            --제품이미지
						    , (
                                SELECT TOP 1 Item_Img 
                                FROM BS_ItemImage 
                                WHERE CorpID = A.CorpID 
                                AND ItemCode = A.ItemCode 
                                AND Img_Div_Cd = '10' 
                                ORDER BY Seq DESC
                                ) AS ItemImage

                            --검사기준서
						    , (
                                SELECT TOP 1 Item_Img 
                                FROM BS_ItemImage 
                                WHERE CorpID = A.CorpID 
                                AND ItemCode = A.ItemCode 
                                AND Img_Div_Cd = '30' 
								AND ISNULL(ProcessCode, '') = ''
								AND ISNULL(ExamDivCd, '') = ''
                                ORDER BY Seq DESC
                                ) AS ExamImage

                        FROM PD_ProcessResult   A WITH(NOLOCK) 
	                    LEFT JOIN EQ_Machine    D ON (A.CorpID = D.CorpID AND A.MachineCode = D.MachineCode)
	                    LEFT JOIN BS_Item       E ON (A.CorpID = E.CorpID AND A.ItemCode = E.ItemCode)
	                    LEFT JOIN BS_ItemClass  ic ON A.CorpID = ic.CorpID AND ic.ItemClass_Cd = E.ItemClass_Cd 
	                    LEFT JOIN SC_Emp        F ON (A.CorpID = F.CorpID AND A.WorkEmp_No = F.Emp_No)
	                    LEFT JOIN PD_ProcessCmd G ON (A.CorpID = G.CorpID AND G.ProcCmd_No = A.ProcCmd_No AND G.ProcCmd_Row_Seq = A.ProcCmd_Row_Seq)
	                    LEFT JOIN PD_ProcessIn pci ON A.CorpID = pci.CorpID AND pci.Rst_No = A.Rst_No AND pci.Rst_Row_Seq = A.Rst_Row_Seq
						WHERE A.CorpID = '{WrGlobal.CorpID}'
						AND A.Rst_No = '{rst_no}'
						AND A.Rst_Row_Seq = '{rst_row_Seq}'

";

			return db.GetDataRow(query);
		}

		private DataTable SelectExam(string rst_no, string rst_Row_Seq, string ItemCode)
		{
			//검사항목 조회

			query = $@"
	                    SELECT  '{rst_no}' AS RefNo, CONVERT(Decimal, {rst_Row_Seq}) AS RefSeq
						, MAX(CASE WHEN ROWNUM = 1 THEN ExamSpec END) AS ExamSpec1
						, MAX(CASE WHEN ROWNUM = 2 THEN ExamSpec END) AS ExamSpec2
						, MAX(CASE WHEN ROWNUM = 3 THEN ExamSpec END) AS ExamSpec3
						, MAX(CASE WHEN ROWNUM = 4 THEN ExamSpec END) AS ExamSpec4
						, MAX(CASE WHEN ROWNUM = 5 THEN ExamSpec END) AS ExamSpec5
						, MAX(CASE WHEN ROWNUM = 6 THEN ExamSpec END) AS ExamSpec6
						, MAX(CASE WHEN ROWNUM = 7 THEN ExamSpec END) AS ExamSpec7
						, MAX(CASE WHEN ROWNUM = 8 THEN ExamSpec END) AS ExamSpec8
						, MAX(CASE WHEN ROWNUM = 9 THEN ExamSpec END) AS ExamSpec9
						, MAX(CASE WHEN ROWNUM = 10 THEN ExamSpec END) AS ExamSpec10
						, MAX(CASE WHEN ROWNUM = 11 THEN ExamSpec END) AS ExamSpec11
						, MAX(CASE WHEN ROWNUM = 12 THEN ExamSpec END) AS ExamSpec12
						, MAX(CASE WHEN ROWNUM = 13 THEN ExamSpec END) AS ExamSpec13
						, MAX(CASE WHEN ROWNUM = 14 THEN ExamSpec END) AS ExamSpec14
						, MAX(CASE WHEN ROWNUM = 15 THEN ExamSpec END) AS ExamSpec15
						, MAX(CASE WHEN ROWNUM = 16 THEN ExamSpec END) AS ExamSpec16
						, MAX(CASE WHEN ROWNUM = 17 THEN ExamSpec END) AS ExamSpec17
						, MAX(CASE WHEN ROWNUM = 18 THEN ExamSpec END) AS ExamSpec18
						, MAX(CASE WHEN ROWNUM = 19 THEN ExamSpec END) AS ExamSpec19
						, MAX(CASE WHEN ROWNUM = 20 THEN ExamSpec END) AS ExamSpec20

						, MAX(CASE WHEN ROWNUM = 1 THEN ExamUnit END) AS ExamUnit1
						, MAX(CASE WHEN ROWNUM = 2 THEN ExamUnit END) AS ExamUnit2
						, MAX(CASE WHEN ROWNUM = 3 THEN ExamUnit END) AS ExamUnit3
						, MAX(CASE WHEN ROWNUM = 4 THEN ExamUnit END) AS ExamUnit4
						, MAX(CASE WHEN ROWNUM = 5 THEN ExamUnit END) AS ExamUnit5
						, MAX(CASE WHEN ROWNUM = 6 THEN ExamUnit END) AS ExamUnit6
						, MAX(CASE WHEN ROWNUM = 7 THEN ExamUnit END) AS ExamUnit7
						, MAX(CASE WHEN ROWNUM = 8 THEN ExamUnit END) AS ExamUnit8
						, MAX(CASE WHEN ROWNUM = 9 THEN ExamUnit END) AS ExamUnit9
						, MAX(CASE WHEN ROWNUM = 10 THEN ExamUnit END) AS ExamUnit10
						, MAX(CASE WHEN ROWNUM = 11 THEN ExamUnit END) AS ExamUnit11
						, MAX(CASE WHEN ROWNUM = 12 THEN ExamUnit END) AS ExamUnit12
						, MAX(CASE WHEN ROWNUM = 13 THEN ExamUnit END) AS ExamUnit13
						, MAX(CASE WHEN ROWNUM = 14 THEN ExamUnit END) AS ExamUnit14
						, MAX(CASE WHEN ROWNUM = 15 THEN ExamUnit END) AS ExamUnit15
						, MAX(CASE WHEN ROWNUM = 16 THEN ExamUnit END) AS ExamUnit16
						, MAX(CASE WHEN ROWNUM = 17 THEN ExamUnit END) AS ExamUnit17
						, MAX(CASE WHEN ROWNUM = 18 THEN ExamUnit END) AS ExamUnit18
						, MAX(CASE WHEN ROWNUM = 19 THEN ExamUnit END) AS ExamUnit19
						, MAX(CASE WHEN ROWNUM = 20 THEN ExamUnit END) AS ExamUnit20

						, MAX(CASE WHEN ROWNUM = 1 THEN ExamMethod END) AS ExamMethod1
						, MAX(CASE WHEN ROWNUM = 2 THEN ExamMethod END) AS ExamMethod2
						, MAX(CASE WHEN ROWNUM = 3 THEN ExamMethod END) AS ExamMethod3
						, MAX(CASE WHEN ROWNUM = 4 THEN ExamMethod END) AS ExamMethod4
						, MAX(CASE WHEN ROWNUM = 5 THEN ExamMethod END) AS ExamMethod5
						, MAX(CASE WHEN ROWNUM = 6 THEN ExamMethod END) AS ExamMethod6
						, MAX(CASE WHEN ROWNUM = 7 THEN ExamMethod END) AS ExamMethod7
						, MAX(CASE WHEN ROWNUM = 8 THEN ExamMethod END) AS ExamMethod8
						, MAX(CASE WHEN ROWNUM = 9 THEN ExamMethod END) AS ExamMethod9
						, MAX(CASE WHEN ROWNUM = 10 THEN ExamMethod END) AS ExamMethod10
						, MAX(CASE WHEN ROWNUM = 11 THEN ExamMethod END) AS ExamMethod11
						, MAX(CASE WHEN ROWNUM = 12 THEN ExamMethod END) AS ExamMethod12
						, MAX(CASE WHEN ROWNUM = 13 THEN ExamMethod END) AS ExamMethod13
						, MAX(CASE WHEN ROWNUM = 14 THEN ExamMethod END) AS ExamMethod14
						, MAX(CASE WHEN ROWNUM = 15 THEN ExamMethod END) AS ExamMethod15
						, MAX(CASE WHEN ROWNUM = 16 THEN ExamMethod END) AS ExamMethod16
						, MAX(CASE WHEN ROWNUM = 17 THEN ExamMethod END) AS ExamMethod17
						, MAX(CASE WHEN ROWNUM = 18 THEN ExamMethod END) AS ExamMethod18
						, MAX(CASE WHEN ROWNUM = 19 THEN ExamMethod END) AS ExamMethod19
						, MAX(CASE WHEN ROWNUM = 20 THEN ExamMethod END) AS ExamMethod20

						FROM 
						(
							SELECT ExamSpec, ExamUnit, ExamMethod, ROW_NUMBER() OVER(ORDER BY DispSeq) AS ROWNUM
							FROM QM_ItemExamMgnt
							WHERE CorpID = '{WrGlobal.CorpID}'
                            AND ItemCode = '{ItemCode}'
							AND ExamDivCd = '20'  -- 초중종에서 출하로 변경
						) AS A

";

			DataTable dt = db.GetDataTable(query);

			return dt;
		}

		private DataTable SelectMatNo(string rst_No, string rst_Row_Seq)
		{
			query = $@"
						WITH MatLot_CTE AS (
						     SELECT ROW_NUMBER() OVER (ORDER BY MatLot_No) AS RowNum, MatLot_No
						
						    FROM MT_MatOut
							WHERE CorpID = '{WrGlobal.CorpID}'
						    AND Mat_Div_Cd = '10'
						    AND Rst_No = '{rst_No}'
						    AND Rst_Row_Seq = '{rst_Row_Seq}'
							GROUP BY MatLot_No
						
						)
						SELECT 
						'{rst_No}' AS Rst_No, CONVERT(Decimal, {rst_Row_Seq}) AS Rst_Row_Seq
						    , MAX(CASE WHEN RowNum = 1 THEN MatLot_No END) AS MatLotNo1
						    , MAX(CASE WHEN RowNum = 2 THEN MatLot_No END) AS MatLotNo2
						    , MAX(CASE WHEN RowNum = 3 THEN MatLot_No END) AS MatLotNo3
						    , MAX(CASE WHEN RowNum = 4 THEN MatLot_No END) AS MatLotNo4
						    , MAX(CASE WHEN RowNum = 5 THEN MatLot_No END) AS MatLotNo5
						    , MAX(CASE WHEN RowNum = 6 THEN MatLot_No END) AS MatLotNo6
						    , MAX(CASE WHEN RowNum = 7 THEN MatLot_No END) AS MatLotNo7
						    , MAX(CASE WHEN RowNum = 8 THEN MatLot_No END) AS MatLotNo8
						FROM 
						    MatLot_CTE;

                    ";

			DataTable dt = db.GetDataTable(query);

			return dt;
		}

        private void xrPictureBox3_BeforePrint(object sender, CancelEventArgs e)
        {
			//xrPictureBox3.Image = GetCurrentColumnValue("Image") as Image;
		}

        //        private DataTable SelectExam(string rst_no, int rst_Row_Seq, string ItemCode)
        //		{
        //            //검사내역 조회
        //            query = $@"

        //                --초중종검사 내역 (추후 외주 수입검사 내역도 함께 보여야 함)
        //                SELECT B.ExamSpec, A.X1, A.X2, A.X3, A.X4, A.X5, A.PassDivCd, C.Cm_Nm AS Pass_Div_Nm
        //, A.RefNo
        //, A.RefSeq, ROW_NUMBER() OVER(ORDER BY B.DispSeq) AS ROWNUM
        //                FROM QM_ExamResult A
        //                LEFT JOIN QM_ItemExamMgnt B ON A.CorpID = B.CorpID AND A.ItemCode = B.ItemCode AND A.ExamCd = B.ExamCd AND A.ExamDivCd = B.ExamDivCd
        //                LEFT JOIN SC_CmCd C ON A.CorpID = C.CorpID AND C.Cm_Class = '007' AND A.PassDivCd = C.Cm_Cd
        //                WHERE A.CorpID = '{WrGlobal.CorpID}'
        //                AND A.ExamDivCd = '40'    --40:초중종검사
        //                AND A.RefNo = '{rst_no}'
        //                AND A.RefSeq = '{rst_Row_Seq}'

        //            ";

        //            //검사항목 조회

        //            query = $@"
        //SELECT ExamSpec, ROW_NUMBER() OVER(ORDER BY DispSeq) AS ROWNUM, '{rst_no}' AS RefNo, CONVERT(Decimal, {rst_Row_Seq}) AS RefSeq
        //FROM QM_ItemExamMgnt
        //WHERE CorpID = '{WrGlobal.CorpID}'
        //AND ItemCode = '{ItemCode}'
        //AND ExamDivCd = '40'  --초중종검사

        //";

        //            DataTable dt = db.GetDataTable(query);

        //            int maxSeq = 20;
        //            int rowCnt = dt.Rows.Count;

        //            if (rowCnt < maxSeq)
        //			{
        //				for (int i = 0; i < maxSeq - rowCnt; i++)
        //				{
        //                    DataRow dr = dt.NewRow();
        //                    dr["RefNo"] = rst_no;
        //                    dr["RefSeq"] = rst_Row_Seq;

        //                    dt.Rows.Add(dr);
        //                }
        //			}

        //            return dt;
        //        }

        //private void SelectExam(string RefNo, int ref_Row_Seq)
        //{
        //    query = $@"

        //        --초중종검사 내역 (추후 외주 수입검사 내역도 함께 보여야 함)
        //        SELECT B.ExamSpec, A.X1, A.X2, A.X3, A.X4, A.X5, A.PassDivCd, C.Cm_Nm AS Pass_Div_Nm
        //        FROM QM_ExamResult A
        //        LEFT JOIN QM_ItemExamMgnt B ON A.CorpID = B.CorpID AND A.ItemCode = B.ItemCode AND A.ExamCd = B.ExamCd AND A.ExamDivCd = B.ExamDivCd
        //        LEFT JOIN SC_CmCd C ON A.CorpID = C.CorpID AND C.Cm_Class = '007' AND A.PassDivCd = C.Cm_Cd
        //        WHERE A.CorpID = '{WrGlobal.CorpID}'
        //        AND A.ExamDivCd = '40'    --40:초중종검사
        //        AND A.RefNo = '{RefNo}'
        //        AND A.RefSeq = '{ref_Row_Seq}'

        //    ";

        //    DataTable dt = db.GetDataTable(query);

        //    this.DetailReport.DataSource = dt;
        //}
    }
}
