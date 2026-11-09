using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Core.Drawing;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraGauges.Win.Base;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.UserControls
{
    public partial class ucMachineManufacture : DevExpress.XtraEditors.XtraUserControl
    {
        Database db = new Database();
        string query = "";

        // --- [1. 배경 디자인 속성] ---
        private int _cornerRadius = 20;
        private Color _borderColor = Color.FromArgb(224, 224, 224);
        private int _borderWidth = 1;
        private Color _cardBackColor = Color.White;

        // 우측 게이지
        private GaugeControl _gaugeControl;
        private CircularGauge _circularGauge;
        private ArcScaleComponent _arcScale;
        private ArcScaleRangeBarComponent _valueBar;
        private ArcScaleMarkerComponent _marker;
        private LabelComponent _centerLabel;

        public ucMachineManufacture()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.BackColor = Color.Transparent;
            this.AutoScroll = false;

            //this.MinimumSize = new Size(510, 260);

            InitializeUI();
        }

        public void Display(string mCode)
        {
            try
            {
                 query = $@"
                    DECLARE @Now DATETIME = GETDATE();

                    WITH ActiveWork AS (


SELECT
        ma.MachineCode,
        ISNULL(ma.MachineName, '-') AS MachineName,
        ISNULL(P.ItemCode, '-') AS ItemCode,
        ISNULL(cm.ItemName, '-') AS ItemName,
        ISNULL(op.OrderQty, 0) AS TargetQty,
		isnull(op.WorkQty, 0) AS TotalQty,
        ISNULL(P.WorkQty, 0) AS CurrentQty,
        
        -- 시작 시간과 현재 시간 차이 (0분일 경우 1분으로 보정)
        CASE 
            WHEN P.StartDate IS NULL THEN 0
            WHEN DATEDIFF(MINUTE, P.StartDate, @Now) <= 0 THEN 1 
            ELSE DATEDIFF(MINUTE, P.StartDate, @Now) 
        END AS ElapsedMinutes,
        
        -- 상태 텍스트
        op.[Status]  AS StatusText
    FROM EQ_Machine ma
    
    -- [핵심] 해당 설비(@MachineCode)의 가장 최근 진행 중인 실적 딱 1건만 조회
    OUTER APPLY (
        SELECT TOP 1 
            CorpId, 
            OrderNo, 
            OrderSeq,
			ItemCode,
			WorkQty,
			StartDate,
			EndDate
        FROM PM_OrderResult
        WHERE CorpId = ma.CorpId
          AND MachineCode = ma.MachineCode
            AND isnull(Result,'') <> 'C' --종료가 아닌것들 중
           ORDER BY 
            StartDate DESC -- 그 다음 시작시간이 늦은 순서
    ) P
    LEFT JOIN PM_OrderProcess op ON p.CorpId = op.CorpId AND  p.OrderNo = op.OrderNo AND p.OrderSeq = op.OrderSeq 
    LEFT JOIN CM_Item cm ON cm.CorpId = ma.CorpId AND P.ItemCode = cm.ItemCode
       
    WHERE ma.CorpId = '{WrGlobal.CorpID}'
      AND ma.MachineCode = '{mCode}' 

                    ),
                    CalcWork AS (
                        SELECT 
                              ItemCode 
                            , ItemName
                            , MachineName
                            , TargetQty 
                            , CurrentQty
                            , TotalQty
                            , CASE 
                                WHEN TargetQty = 0 THEN 0 
                                ELSE ROUND((CurrentQty / TargetQty) * 100, 1) 
                            END AS OperationRate,
                            CASE 
                                WHEN ElapsedMinutes = 0 THEN 0 
                                ELSE ROUND(CurrentQty / (ElapsedMinutes / 60.0), 0) 
                            END AS PerHourQty,
                            CASE 
                                WHEN TargetQty - CurrentQty < 0 THEN 0 
                                ELSE TargetQty - CurrentQty 
                            END AS RemainQty,
                            CASE 
                                WHEN ElapsedMinutes = 0 THEN 0 
                                ELSE (CurrentQty / CAST(ElapsedMinutes AS FLOAT)) 
                            END AS QtyPerMinute
                            , StatusText
                        FROM ActiveWork
                    )
                    SELECT TOP 1
                        MachineName, ItemName, TargetQty, CurrentQty, TotalQty, OperationRate, PerHourQty
                        --, iif(TargetQty = 0, 0, (TotalQty + isnull(CurrentQty, 0)) / TargetQty * 100) AS AchieveRate
                        , ROUND( IIF(TargetQty = 0, 0, (CAST(TotalQty AS FLOAT) / TargetQty) * 100), 1) AS AchieveRate -- 진척률
                        , StatusText
                        , CASE 
                            WHEN QtyPerMinute <= 0 OR RemainQty <= 0 THEN '00:00'
                            ELSE RIGHT('0' + CAST(CAST(RemainQty / QtyPerMinute AS INT) / 60 AS VARCHAR(10)), 2) + ':' + 
                                 RIGHT('0' + CAST(CAST(RemainQty / QtyPerMinute AS INT) % 60 AS VARCHAR(2)), 2)
                        END AS ExpectedTime
                    FROM CalcWork;
                    ";

                DataRow dr = db.GetDataRow(query);

                if (dr != null)
                {
                    // 값 매핑
                    string mName = dr["MachineName"].ToString();
                    string pName = dr["ItemName"].ToString();
                    decimal target = dr["TargetQty"].ConvertDecimal_Null_To_Zero();
                    decimal current = dr["CurrentQty"].ConvertDecimal_Null_To_Zero();
                    decimal total = dr["TotalQty"].ConvertDecimal_Null_To_Zero();
                    decimal rate = dr["OperationRate"].ConvertDecimal_Null_To_Zero();
                    decimal achieve = dr["AchieveRate"].ConvertDecimal_Null_To_Zero();
                    string status = dr["StatusText"].ToString(); // 예: "가동", "비가동"

                    // UI 적용 메서드 호출
                    UpdateUI(mName, pName, target, current, rate, achieve, total, status);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"데이터를 불러오는 중 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUI(string machineName, string productName, decimal targetCount, decimal currentCount,
                              decimal operationRate, decimal achieve, decimal totalCount, string statusText)
        {
            lblMachineName.Text = " " + machineName;
            LblVal_ItemName.Text = productName;

            // 숫자 형식 천단위 콤마 처리
            LblVal_Target.Text = $"{targetCount:#,##0} EA";
            LblVal_Curr.Text = $"{currentCount:#,##0} EA";

            LblVal_rate.Text = $"{operationRate:#,##0.00}%";
            LblVal_Achieve.Text = $"{achieve:#,##0.0}%";
            LblVal_Total.Text = $"{totalCount:#,##0} EA";

            // 상태 텍스트 색상 및 점(●) HTML 태그 처리
            string statusColor = statusText == "작업중" ? "#2FC671" : "#BDBDBD"; // 작업중=초록, 그외=빨강
            LblStatus.Text = $"<color={statusColor}>●</color> {statusText}";
            _valueBar.Value = (float)achieve;
            _marker.Value = (float)achieve;


			_centerLabel.Text = $"진척률\n{achieve:#,##0.0}%\n{statusText}";


			Color baseColor;
			if (statusText == "작업중")
			{
				baseColor = (achieve >= 70) ? Color.FromArgb(255, 148, 54) : Color.FromArgb(47, 198, 113);
			}
			else
			{
				baseColor = Color.FromArgb(217, 223, 229);
			}

			// 2. LinearGradientBrushObject 생성 및 속성 설정
			var gradientBrush = new DevExpress.XtraGauges.Core.Drawing.LinearGradientBrushObject();
			gradientBrush.StartColor = ControlPaint.Light(baseColor, 0.3f); // 시작색 (밝게)
            gradientBrush.EndColor = ControlPaint.Dark(baseColor, 0.2f);   // 끝색 (어둡게)

            // 3D 입체감을 위해 그라데이션 방향 설정 (0~360도)
            // 90도는 수직, 45도는 대각선 방향입니다.
            //gradientBrush. = 45;

            // 3. ValueBar에 적용
            _valueBar.Appearance.ContentBrush = gradientBrush;

            // 4. Marker에도 동일한 느낌의 Shader 적용
            StyleShader customShader = new StyleShader();
            customShader.StyleColor1 = gradientBrush.StartColor;
            customShader.StyleColor2 = gradientBrush.EndColor;
        }
		private void InitializeUI()
        {
            LblStatus.Text = "<color=#2FC671>●</color> -";
            LblStatus.AllowHtmlString = true;
            LblStatus.Font = new Font("맑은 고딕", 11, FontStyle.Bold);

            CreateGauge();

            Initlabel(LblTitie_ItemName, LblVal_ItemName, "생산 제품명", "-", Color.FromArgb(10, 30, 80));
            Initlabel(LblTitie_Target, LblVal_Target, "목표", "0 EA", Color.FromArgb(50, 150, 220));
            Initlabel(LblTitle_Total, LblVal_Total, "누적생산", "0 EA", Color.FromArgb(235, 120, 50), null, Color.FromArgb(235, 120, 50));
            Initlabel(LblTitle_Curr, LblVal_Curr, "현재", "0 EA", Color.FromArgb(50, 150, 220));
            Initlabel(LblTitle_Achieve, LblVal_Achieve, "진척률", "0%", Color.FromArgb(50, 150, 220));
            Initlabel(LblTitle_rate, LblVal_rate, "순가동률", "0%", Color.FromArgb(50, 150, 220));
        }

        private void Initlabel(LabelControl lblTitle, LabelControl lblValue, string title, string value, Color dotColor, Color? backColor = null, Color? valueColor = null)
        {
            string hexColor = ColorTranslator.ToHtml(dotColor);

            lblTitle.AllowHtmlString = true;
            lblTitle.Text = $"<color={hexColor}>●</color> {title}";
            lblTitle.Font = new Font("맑은 고딕", 9.5f, FontStyle.Regular);
            lblTitle.ForeColor = Color.DimGray;
            lblTitle.Padding = new Padding(5, 2, 0, 2);

            lblValue.Text = value;
            lblValue.Font = new Font("맑은 고딕", 9.5f, FontStyle.Bold);
            lblValue.ForeColor = valueColor ?? Color.Black;
            lblValue.Padding = new Padding(0, 2, 5, 2);

            lblValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

            if (backColor.HasValue)
            {
                lblTitle.BackColor = backColor.Value;
                lblValue.BackColor = backColor.Value;
            }
        }

        private void CreateGauge()
        {
            _gaugeControl = gaugeControl1;
            _gaugeControl.BackColor = Color.Transparent;
            _gaugeControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            // =========================================================================
            // [크기 펌핑 핵심] 수학 좌표를 180x180 캔버스에 맞춰 완전히 다시 계산
            // =========================================================================

            _circularGauge = circularGauge1;
            //_gaugeControl.Gauges.Add(_circularGauge);

            // 중심점 (90, 90), 반지름을 60 -> 80으로 대폭 키워서 180 상자에 꽉 차게 만듦
            _arcScale = arcScaleComponent1;
            _arcScale.MinValue = 0;
            _arcScale.MaxValue = 100;
			//new ArcScaleComponent { Center = new PointF2D(90, 90), RadiusX = 80, RadiusY = 80, StartAngle = -270, EndAngle = 90, MinValue = 0, MaxValue = 100 };
			//_arcScale.MajorTickmark.ShowTick = false; _arcScale.MajorTickmark.ShowText = false; _arcScale.MinorTickmark.ShowTick = false;

			// 커진 스케일에 맞춰 게이지 두께도 12 -> 15로 더 두껍게 조절
			ArcScaleRangeBarComponent bgBar = arcScaleRangeBarComponent1;
			//bgBar.ArcScale = _arcScale;
			//bgBar.AnchorValue = 0;
			//bgBar.Value = 100;
			//bgBar.ZOrder = 10;
			//bgBar.StartOffset = 3;
			//bgBar.EndOffset = 3;
			//bgBar.Appearance.ContentBrush = new SolidBrushObject(Color.FromArgb(217, 223, 229));

			_valueBar = arcScaleRangeBarComponent1;
            //ArcScale = _arcScale, AnchorValue = 0, Valuse = 0f, ZOrder = -10, StartOffset = 15, EndOffset = 15 };
            _valueBar.Appearance.ContentBrush = new SolidBrushObject(Color.FromArgb(47, 198, 113));
//            _circularGauge.RangeBars.Add(_valueBar);

            _marker = new ArcScaleMarkerComponent { ArcScale = _arcScale, Value = 0f, ShapeType = MarkerPointerShapeType.Circle, ShapeScale = new SizeF(1.7f, 1.7f), ZOrder = -20 };
            StyleShader initShader = new StyleShader();
            initShader.StyleColor1 = Color.FromArgb(47, 198, 113);
            initShader.StyleColor2 = Color.FromArgb(47, 198, 113);
            _marker.Shader = initShader;
            _circularGauge.Markers.Add(_marker);

            // 텍스트 위치도 정중앙(90, 90)으로 이동하고 폰트 크기(14) 빵빵하게 키움
            _centerLabel = labelComponent1;
            labelComponent1.Text = "달성률\n0%\n대기";
            //labelComponent1.Position = new PointF2D(90, 90);
            _centerLabel.AppearanceText.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
            //_centerLabel.AppearanceText.TextAlignment = StringAlignment.Center;
        }

        // --- [7. 둥근 테두리 그리기 로직] ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rect = new RectangleF(
                _borderWidth / 2f,
                _borderWidth / 2f,
                this.Width - _borderWidth,
                this.Height - _borderWidth);

            using (GraphicsPath path = GetRoundedRectanglePath(rect, _cornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(_cardBackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                if (_borderWidth > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderWidth))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            }
        }

        private GraphicsPath GetRoundedRectanglePath(RectangleF rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}