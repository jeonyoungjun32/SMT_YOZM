using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartCommon.Common;

namespace SmartMES.View.QM
{
	public partial class Frm_XRChart : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();
        string[] m_ItemType = new string[] { "10","20","30","40" };   //10 : 상품  20 : 제품  30 : 반제품  40 : 원자재  50 : 부자재  60 : 샘플/개발  70 : 배출자재  80 : 배출자재_황동  90 : 배출자재_알루미늄
        string[] Exam_Div = new string[] {"30","40" };   //10 : 상품  20 : 제품  30 : 반제품  40 : 원자재  50 : 부자재  60 : 샘플/개발  70 : 배출자재  80 : 배출자재_황동  90 : 배출자재_알루미늄

        private DataTable ChartData = null; //차트 입력될 데이터
        private DataRow ChartInfo = null;   //차트 입력될 데이터 설정정보
        PanelControl PnlCalculate = new PanelControl(); //계산식

        public Frm_XRChart()
        {
            Create_CalculateView();//아래쪽에 두면 TabIndex 설정해도 뒤로 넘어감

            InitializeComponent();
            InitLup();

            Txt_LSL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            Txt_USL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            Txt_LSL.EditValue = 0;
            Txt_USL.EditValue = 0;
            Txt_C  .EditValue = 1.023;

        }

        private void Create_CalculateView()
        {
            //계산식 정보창
            PnlCalculate.Parent = this;
            PnlCalculate.Location = new Point(483, 87);
            PnlCalculate.Size = new Size(403, 240);
            PnlCalculate.Visible = false;

            Size NmSize = new Size(80, 23);
            Size calSize = new Size(320, 23);

            //표준편차
            LabelControl lblStdDeviation_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "X-Bar 표준편차",
                Size = NmSize,
                Location = new Point(6, 6)
            };
            LabelControl lblStdDeviation_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": √(합((평균 - X_Bar)^2) / (데이터 수 - 1))",
                Size = calSize,
                Location = new Point(lblStdDeviation_Nm.Location.X + lblStdDeviation_Nm.Size.Width + 6, lblStdDeviation_Nm.Location.Y)
            };
            //관리범위(LCL, UCL)
            LabelControl lblLCL_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "X-Bar LCL",
                Size = NmSize,
                Location = new Point(lblStdDeviation_Nm.Location.X, lblStdDeviation_Nm.Location.Y + lblStdDeviation_Nm.Size.Height + 6)
            };
            LabelControl lblLCL_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": CL - (X_Bar 관리계수 * R-CL)",
                Size = calSize,
                Location = new Point(lblLCL_Nm.Location.X + lblLCL_Nm.Size.Width + 6, lblLCL_Nm.Location.Y)
            };
            LabelControl lblUCL_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "X-Bar UCL",
                Size = NmSize,
                Location = new Point(lblLCL_Nm.Location.X, lblLCL_Nm.Location.Y + lblLCL_Nm.Size.Height + 6)
            };
            LabelControl lblUCL_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": CL + (X_Bar 관리계수 * R-CL)",
                Size = calSize,
                Location = new Point(lblUCL_Nm.Location.X + lblUCL_Nm.Size.Width + 6, lblUCL_Nm.Location.Y)
            };

            //Cp
            LabelControl lblCp_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "Cp",
                Size = NmSize,
                Location = new Point(lblUCL_Nm.Location.X, lblUCL_Nm.Location.Y + lblUCL_Nm.Size.Height + 6)
            };
            LabelControl lblCp_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": (USL(규격상한) - LSL(규격하한)) / (6 * X-Bar 표준편차)",
                Size = calSize,
                Location = new Point(lblCp_Nm.Location.X + lblCp_Nm.Size.Width + 6, lblCp_Nm.Location.Y)
            };
            //K(치우침계수)
            LabelControl lblK_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "K",
                Size = NmSize,
                Location = new Point(lblCp_Nm.Location.X, lblCp_Nm.Location.Y + lblCp_Nm.Size.Height + 6)
            };
            LabelControl lblK_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": | CL - ((USL + LSL) / 2) | * ((USL - LSL) / 2)",
                Size = calSize,
                Location = new Point(lblK_Nm.Location.X + lblK_Nm.Size.Width + 6, lblK_Nm.Location.Y)
            };
            //Cpk
            LabelControl lblCpk_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "Cpk",
                Size = NmSize,
                Location = new Point(lblK_Nm.Location.X, lblK_Nm.Location.Y + lblK_Nm.Size.Height + 6)
            };
            LabelControl lblCpk_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": (1 - K) * Cp",
                Size = calSize,
                Location = new Point(lblCpk_Nm.Location.X + lblCpk_Nm.Size.Width + 6, lblCpk_Nm.Location.Y)
            };
            //Cpl
            LabelControl lblCpl_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "Cpl",
                Size = NmSize,
                Location = new Point(lblCpk_Nm.Location.X, lblCpk_Nm.Location.Y + lblCpk_Nm.Size.Height + 6)
            };
            LabelControl lblCpl_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": (CL - LSL) / (3 * 표준편차)",
                Size = calSize,
                Location = new Point(lblCpl_Nm.Location.X + lblCpl_Nm.Size.Width + 6, lblCpl_Nm.Location.Y)
            };
            //Cpu
            LabelControl lblCpu_Nm = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = "Cpu",
                Size = NmSize,
                Location = new Point(lblCpl_Nm.Location.X, lblCpl_Nm.Location.Y + lblCpl_Nm.Size.Height + 6)
            };
            LabelControl lblCpu_Cal = new LabelControl()
            {
                Parent = PnlCalculate,
                AutoSizeMode = LabelAutoSizeMode.None,
                Text = ": (USL - CL) / (3 * 표준편차)",
                Size = calSize,
                Location = new Point(lblCpu_Nm.Location.X + lblCpu_Nm.Size.Width + 6, lblCpu_Nm.Location.Y)
            };
        }

        private void InitLup()
        {
            //조회기간
            smtDatetime.smtStartDateTimeValue = DateTime.Now.AddMonths(-1);
            smtDatetime.smtEndDateTimeValue = DateTime.Now;

            //품목
            ssCM_Item.smtLookUpDataSource = code.smtLookUp("품목", "", String.Join(",", m_ItemType), "", "");
            ssCM_Item.smtLookUpDataValueMember = "품목코드";
            ssCM_Item.smtLookUpDataDisplayMember = "품명";
            ssCM_Item.smtLookUpSelectdIndex = 0;

            //검사구분
            //DataTable dtExamType = cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, "Q005", Exam_Div, false);
            DataTable dtExamType = cm.SelectCateItem("Q005", null, false);

            LupExamDiv.smtLookUpDataSource = dtExamType;
            LupExamDiv.smtLookUpDataValueMember = "코드";
            LupExamDiv.smtLookUpDataDisplayMember = "명칭";
			LupExamDiv.smtLookUpEditValueChanged += SmtExam_smtLookUpEditValueChanged;

            //RadioGroup 셋팅
            //검사방법
            QM_ExamMgnt exam = new QM_ExamMgnt();
            DataTable dt_exam = exam.Select_ExamNumeric(LupExamDiv.smtLookUpValue);

            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(RdoExamCd, dt_exam, "ExamNm", "ExamCd");
            RdoExamCd.SelectedIndex = 0;

            SetChart();
        }

		private void SmtExam_smtLookUpEditValueChanged(object sender, EventArgs e)
		{
            this.Validate();

            RdoExamCd.Properties.Items.Clear();

            QM_ExamMgnt exam = new QM_ExamMgnt();
            DataTable dt_exam = exam.Select_ExamNumeric(LupExamDiv.smtLookUpValue);

            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(RdoExamCd, dt_exam, "ExamNm", "ExamCd");
            RdoExamCd.SelectedIndex = 0;
        }

        private void EditValueChanged_SetExamInfo(object sender, EventArgs e)
        {
            this.Validate();

            //검사정보 규격, 위험 상하한값 가져오기
            QM_ItemExamMgnt iem = new QM_ItemExamMgnt();
            iem.ExamDivCd = Convert.ToString(LupExamDiv.smtLookUpValue);
			iem.ExamCd = Convert.ToString(RdoExamCd.EditValue);
			iem.ItemCode = Convert.ToString(ssCM_Item.smtLookUpValue);

			DataTable dtItemExam = iem.Select_ItemExam_ByItemCd();
			if (dtItemExam != null)
			{
				if (dtItemExam.Rows.Count != 0)
				{
					DataRow dr = dtItemExam.Rows[0];

					Txt_LSL.EditValue = dr["MgntMin"].ConvertDouble_Null_To_Zero();
					Txt_USL.EditValue = dr["MgntMax"].ConvertDouble_Null_To_Zero();
				}
			}
		}

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            QM_ExamResult qer = new QM_ExamResult();
            qer.ExamDivCd = Convert.ToString(LupExamDiv.smtLookUpValue);
            qer.ExamCd     = Convert.ToString(RdoExamCd.EditValue);
            qer.ItemCode     = Convert.ToString(ssCM_Item.smtLookUpValue);
            string fromDate = Convert.ToDateTime(smtDatetime.smtStartDateTimeValue).ToString("yyyyMMdd");
            string toDate   = Convert.ToDateTime(smtDatetime.smtEndDateTimeValue).ToString("yyyyMMdd");
            float LSL = Convert.ToSingle(Txt_LSL.EditValue);
            float USL = Convert.ToSingle(Txt_USL.EditValue);
            float XbarC = Convert.ToSingle(Txt_C.EditValue);

            DataRow drChartInfo = qer.Select_ExamXR_ChartInfo(fromDate, toDate, LSL, USL, XbarC);
            DataTable dtChartData = qer.Select_ExamXR_DataList(fromDate, toDate);

            if (drChartInfo == null || dtChartData == null) { MessageBox.Show("해당정보의 데이터가 없습니다."); return; }

            ChartInfo = drChartInfo;
            ChartData = dtChartData;

            Init_NewData();
            InitGrid();
        }

        #region Chart

        #region Chart 초기셋팅

        private void SetChart()
        {
            Set_XBarChart(ChartX); //XBar Chart
            Set_RCahrt(ChartR);       //R Chart
            Set_HistoChart(ChartH);
        }

        private void Set_XBarChart(ChartControl chart)
        {
            //Chart Default
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //제목
            chart.Titles.Clear();
            ChartTitle title = new ChartTitle();
            title.Text = "X-Bar Chart";
            title.Font = new Font(title.Font.FontFamily, 15, FontStyle.Bold);
            title.TextColor = Color.Black;
            chart.Titles.Add(title);

            //데이터 변경부
            try
            {
                //데이터 삽입
                chart.Series.BeginUpdate(); //차트 데이터넣기 시작
                chart.Series.Clear();

                chart.DataSource = null;

                Series series = new Series("X-Bar Chart", ViewType.Line);
                series.ArgumentDataMember = "Lot_No";
                series.ValueDataMembers.AddRange(new string[] { "X_Bar" });
                chart.Series.Add(series);

                chart.Series.EndUpdate();   //차트 데이터넣기 종료

                XYDiagram diagram = chart.Diagram as XYDiagram;

                //표시 범위
                //X Label 숨기기
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Set_RCahrt(ChartControl chart)
        {
            //Chart Default
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //제목
            chart.Titles.Clear();
            ChartTitle title = new ChartTitle();
            title.Text = "R Chart";
            title.Font = new Font(title.Font.FontFamily, 15, FontStyle.Bold);
            title.TextColor = Color.Black;
            chart.Titles.Add(title);

            //데이터 변경부
            try
            {
                //데이터 삽입
                chart.Series.BeginUpdate(); //차트 데이터넣기 시작
                chart.Series.Clear();

                chart.DataSource = null;

                Series series = new Series("R Chart", ViewType.Line);
                series.ArgumentDataMember = "Lot_No";
                series.ValueDataMembers.AddRange(new string[] { "Range" });
                chart.Series.Add(series);

                chart.Series.EndUpdate();   //차트 데이터넣기 종료

                XYDiagram diagram = chart.Diagram as XYDiagram;

                //표시 범위
                //X Label 숨기기
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Set_HistoChart(ChartControl chart)
        {
            //Chart Default
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //제목
            chart.Titles.Clear();
            ChartTitle title = new ChartTitle();
            title.Text = "Histogram";
            title.Font = new Font(title.Font.FontFamily, 15, FontStyle.Bold);
            title.TextColor = Color.Black;
            chart.Titles.Add(title);

            //데이터 변경부
            try
            {
                //데이터 삽입
                chart.Series.BeginUpdate(); //차트 데이터넣기 시작
                chart.Series.Clear();

                chart.DataSource = null;

                Series series = new Series("R Histogram", ViewType.Bar);
                series.ArgumentDataMember = "X_Bar";
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                (series.Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                (series.View as SideBySideBarSeriesView).BarWidth = 1;
                chart.Series.Add(series);

                chart.Series.EndUpdate();   //차트 데이터넣기 종료

                XYDiagram diagram = chart.Diagram as XYDiagram;

                //diagram.AxisX.GridLines.Visible = true;
                diagram.AxisX.Label.TextPattern = "{A:F1}";
                diagram.AxisX.NumericScaleOptions.AggregateFunction = AggregateFunction.Histogram;  //히스토그램 형식 지정
                diagram.AxisX.NumericScaleOptions.ScaleMode = ScaleMode.Interval;   //뭔지 모르곘음
                diagram.AxisX.NumericScaleOptions.IntervalOptions.GridLayoutMode = GridLayoutMode.GridAndLabelShifted;   //뭔지 모르곘음
                diagram.AxisX.NumericScaleOptions.IntervalOptions.DivisionMode = IntervalDivisionMode.Count;    //나눌 수량으로 구분
                diagram.AxisX.NumericScaleOptions.IntervalOptions.Count = 9;    //나눌 갯수

                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Text = "X-Bar 값";
                diagram.AxisX.Title.Font = new Font("Tahoma", 12, FontStyle.Regular);
                diagram.AxisX.Title.TextColor = Color.Black;

                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Text = "빈도";
                diagram.AxisY.Title.Font = new Font("Tahoma", 12, FontStyle.Regular);
                diagram.AxisY.Title.TextColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Chart 초기셋팅 End

        #region 차트 데이터 적용

        private void Init_NewData()
        {
            Init_XbarChart(ChartX);
            Init_RChart(ChartR);

            ChartX.DataSource = ChartData;
            ChartR.DataSource = ChartData;
            ChartH.DataSource = ChartData;
        }

        private void Init_XbarChart(ChartControl chart)
        {
            //상하한값 표시
            if (ChartInfo == null) return;

            XYDiagram diagram = chart.Diagram as XYDiagram;
            //표시 최소값
            diagram.AxisY.WholeRange.MinValue = Convert.ToSingle(Txt_LSL.EditValue) == 0
                                                ? Convert.ToSingle(ChartInfo["X-Bar_Min"]) - Convert.ToSingle(ChartInfo["X-Bar_표준편차"])
                                                  : Math.Min(Convert.ToSingle(ChartInfo["X-Bar_Min"]), Convert.ToSingle(Txt_LSL.EditValue)) - Convert.ToSingle(ChartInfo["X-Bar_표준편차"]);
            diagram.AxisY.WholeRange.MaxValue = Convert.ToSingle(Txt_USL.EditValue) == 0
                                                ? Convert.ToSingle(ChartInfo["X-Bar_Max"]) + Convert.ToSingle(ChartInfo["X-Bar_표준편차"])
                                                  : Math.Max(Convert.ToSingle(ChartInfo["X-Bar_Max"]), Convert.ToSingle(Txt_USL.EditValue)) + Convert.ToSingle(ChartInfo["X-Bar_표준편차"]);

            //규격, Xbar 상,하한 라인 그리기
            diagram.AxisY.ConstantLines.Clear();
            //규격 하한값 라인
            if (Convert.ToSingle(Txt_LSL.EditValue) != 0)
            {
                ConstantLine constLSL = new ConstantLine("LSL", Txt_LSL.EditValue);
                constLSL.Color = Color.Red;
                constLSL.LineStyle.DashStyle = DashStyle.Dash;
                constLSL.LineStyle.Thickness = 2;
                diagram.AxisY.ConstantLines.Add(constLSL);
            }
            //규격 상한값 라인
            if (Convert.ToSingle(Txt_USL.EditValue) != 0)
            {
                ConstantLine constUSL = new ConstantLine("USL", Txt_USL.EditValue);
                constUSL.Color = Color.Red;
                constUSL.LineStyle.DashStyle = DashStyle.Dash;
                constUSL.LineStyle.Thickness = 2;
                diagram.AxisY.ConstantLines.Add(constUSL);
            }
            //XBar 하한값 라인
            if (Convert.ToSingle(ChartInfo["X-Bar_LCL"]) != 0)
            {
                ConstantLine constLCL = new ConstantLine("LCL : " + Convert.ToSingle(ChartInfo["X-Bar_LCL"]).ToString("F2"), ChartInfo["X-Bar_LCL"]);
                constLCL.Color = Color.Blue;
                constLCL.LineStyle.DashStyle = DashStyle.Dash;
                constLCL.LineStyle.Thickness = 2;
                diagram.AxisY.ConstantLines.Add(constLCL);
            }
            //XBar 상한값 라인
            if (Convert.ToSingle(ChartInfo["X-Bar_UCL"]) != 0)
            {
                ConstantLine constUCL = new ConstantLine("UCL : " + Convert.ToSingle(ChartInfo["X-Bar_UCL"]).ToString("F2"), ChartInfo["X-Bar_UCL"]);
                constUCL.Color = Color.Blue;
                constUCL.LineStyle.DashStyle = DashStyle.Dash;
                constUCL.LineStyle.Thickness = 2;
                diagram.AxisY.ConstantLines.Add(constUCL);
            }
        }

        private void Init_RChart(ChartControl chart)
        {
            //상하한값 표시
            if (ChartInfo == null) return;

            XYDiagram diagram = chart.Diagram as XYDiagram;
            //표시 최소값
            diagram.AxisY.WholeRange.SideMarginsValue = 0;
            diagram.AxisY.WholeRange.MinValue = Convert.ToSingle(ChartInfo["R_Min"]) < Convert.ToSingle(ChartInfo["R_표준편차"])
                                                ? 0 : Convert.ToSingle(ChartInfo["R_Min"]) - Convert.ToSingle(ChartInfo["R_표준편차"]);
            //표시 최대값
            diagram.AxisY.WholeRange.MaxValue = Convert.ToSingle(ChartInfo["R_Max"]) + Convert.ToSingle(ChartInfo["R_표준편차"]);

            //규격, Xbar 상, 하한 라인 그리기
            diagram.AxisY.ConstantLines.Clear();
            ConstantLine constLCL = new ConstantLine("LCL : " + Convert.ToSingle(ChartInfo["R_LCL"]).ToString("F2"), ChartInfo["R_LCL"]);
            constLCL.Color = Color.Blue;
            constLCL.LineStyle.DashStyle = DashStyle.Dash;
            constLCL.LineStyle.Thickness = 2;
            ConstantLine constUCL = new ConstantLine("UCL : " + Convert.ToSingle(ChartInfo["R_UCL"]).ToString("F2"), ChartInfo["R_UCL"]);
            constUCL.Color = Color.Blue;
            constUCL.LineStyle.DashStyle = DashStyle.Dash;
            constUCL.LineStyle.Thickness = 2;

            diagram.AxisY.ConstantLines.Add(constLCL);     //R 하한값 라인
            diagram.AxisY.ConstantLines.Add(constUCL);     //R 상한값 라인
        }

        #endregion 차트 데이터 적용 End

        #endregion Chart End

        #region Grid

        private void InitGrid()
        {
            DataTable dtPivot = new DataTable();
            dtPivot.Columns.Add("구분");
            dtPivot.Columns.Add("값");

            try
            {
                //Pivot 작업
                DataTable dtConvert = ChartInfo.Table;
                foreach (DataColumn col in dtConvert.Columns)
                {
                    if (col.ColumnName == "Data_Cnt") continue;
                    dtPivot.Rows.Add(col.ColumnName, dtConvert.Rows[0][col]);
                }

                gcTotal.DataSource = dtPivot;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Grid End

        private void Btn_ShowCal_Click(object sender, EventArgs e)
        {

            int x = Btn_ShowCal.Location.X + 10;
            int y = Btn_ShowCal.Location.Y + 80;
            PnlCalculate.Location = new Point(x, y);

            //계산식 보이기
            if (PnlCalculate.Visible) { PnlCalculate.Visible = false; }
            else { PnlCalculate.Visible = true; }
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}