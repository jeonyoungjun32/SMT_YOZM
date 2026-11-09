using System;
using System.Data;
using System.Windows.Forms;

using baseSMT;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

using SmartCommon.Common;

using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.ChatBot;

namespace SmartMES.View.Chatbot
{
    public partial class Frm_Chat_ResponseQuality : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        Database db = new Database();

        public Frm_Chat_ResponseQuality()
        {
            InitializeComponent();
        }

        private void Frm_CM_DefectCode_Load(object sender, EventArgs e)
        {
            SetDesign();
            //Func_Search();
        }

        private void SetDesign()
        {
            SetLup();
            Set_GridView(Gv_List);
            Set_Btn_Event();
        }

        private void SetLup()
        {
            //조회기간 기본값 (최근 7일)
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            //피드백 구분 : ChatBot_History.FeedbackYn (Y=만족, N=불만족, NULL=미평가)
            DataTable dtFeedback = new DataTable();
            dtFeedback.Columns.Add("코드", typeof(string));
            dtFeedback.Columns.Add("명칭", typeof(string));

            dtFeedback.Rows.Add("ALL", "전체");
            dtFeedback.Rows.Add("Y", "만족");
            dtFeedback.Rows.Add("N", "불만족");

            LupFeedback.smtLookUpDataSource = dtFeedback;
            LupFeedback.smtLookUpDataDisplayMember = "명칭";
            LupFeedback.smtLookUpDataValueMember = "코드";
            LupFeedback.smtLookUpValue = "ALL";

            //매칭여부 : ChatBot_History.RefMatchYn
            DataTable dtMatch = new DataTable();
            dtMatch.Columns.Add("코드", typeof(string));
            dtMatch.Columns.Add("명칭", typeof(string));

            dtMatch.Rows.Add("ALL", "전체");
            dtMatch.Rows.Add("Y", "Y");
            dtMatch.Rows.Add("N", "N");

            LupMatch.smtLookUpDataSource = dtMatch;
            LupMatch.smtLookUpDataDisplayMember = "명칭";
            LupMatch.smtLookUpDataValueMember = "코드";
            LupMatch.smtLookUpValue = "ALL";
        }

        private void Set_Btn_Event()
        {
            cmdDisplay.Click += cmdDisplay_Click;
            cmdExcel.Click += cmdExcel_Click;
        }

        #region GridView Setting

        private void Set_GridView(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            gv.OptionsBehavior.Editable = false;        //조회 전용 화면

            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.Get_UserList(), "코드", "코드명");

            //Column 생성 (ChatBot_History 컬럼 매칭)
            grid.Create_GvColumn(gv, "질의일시", "CreDt");
            grid.Create_GvColumn(gv, "사용자", "EmployeeId", lup_Emp);
            grid.Create_GvColumn(gv, "사용자질의", "Question");
            grid.Create_GvColumn(gv, "AI답변요약", "AnswerSummary");
            grid.Create_GvColumn(gv, "피드백", "FeedbackYn");
            grid.Create_GvColumn(gv, "평점", "Rating");
            grid.Create_GvColumn(gv, "응답시간(ms)", "ResponseTimeMs");
            grid.Create_GvColumn(gv, "참조유형", "RefType");
            grid.Create_GvColumn(gv, "매칭여부", "RefMatchYn");
            grid.Create_GvColumn(gv, "오류여부", "IsError");
            grid.Create_GvColumn(gv, "코멘트", "Comment");

            //상세보기용 (숨김)
            grid.Create_GvColumn(gv, "세션", "SessionNo");
            grid.Create_GvColumn(gv, "피드백일시", "FeedbackDt");
            grid.Create_GvColumn(gv, "AI답변", "Answer");
            grid.Create_GvColumn(gv, "실행SQL", "ExecSql");
            grid.Create_GvColumn(gv, "조회건수", "ResultCnt");
            grid.Create_GvColumn(gv, "채번PK", "ChatSeq");

            gv.Columns["SessionNo"].Visible = false;
            gv.Columns["FeedbackDt"].Visible = false;
            gv.Columns["Answer"].Visible = false;
            gv.Columns["ExecSql"].Visible = false;
            gv.Columns["ResultCnt"].Visible = false;
            gv.Columns["ChatSeq"].Visible = false;
            gv.Columns["Comment"].Visible = false;

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["CreDt"], clsGrid.TypeList.TDateTime);              //질의일시
            grid.SetCol_Type(gv.Columns["FeedbackDt"], clsGrid.TypeList.TDateTime);         //피드백일시
            grid.SetCol_Type(gv.Columns["ResponseTimeMs"], clsGrid.TypeList.TInt);          //응답시간(ms)
            grid.SetCol_Type(gv.Columns["ResultCnt"], clsGrid.TypeList.TInt);               //조회건수

            //셀값 정렬
            gv.Columns["FeedbackYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //피드백
            gv.Columns["Rating"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;         //평점
            gv.Columns["RefMatchYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //매칭여부
            gv.Columns["IsError"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;        //오류여부

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
        }

        #endregion GridView Setting End

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();

            //데이터 조회
            ChatBot_History history = new ChatBot_History();
            history.FeedbackYn = LupFeedback.smtLookUpValue.ConvertString_Null_To_Empty();
            history.RefMatchYn = LupMatch.smtLookUpValue.ConvertString_Null_To_Empty();

            DataTable dt = history.Select_ChatBot_History(startDate, endDate);

            Gc_List.DataSource = dt;

            Set_KpiCard(dt);            //상단 요약 카드 갱신

            Gv_List.BestFitColumns();   //Column 너비 조정
        }

        /// <summary>
        /// 조회 결과로 상단 요약 카드(좋아요/싫어요/만족도/평균응답시간) 갱신
        /// 만족도 = 좋아요 / (좋아요 + 싫어요) * 100. 미평가(FeedbackYn IS NULL)는 모수에서 제외
        /// </summary>
        private void Set_KpiCard(DataTable dt)
        {
            int likeCnt = 0;
            int dislikeCnt = 0;
            int resTimeCnt = 0;
            double resTimeSum = 0;

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string feedback = dr["FeedbackYn"].ConvertString_Null_To_Empty().Trim();

                    if (feedback == "Y") likeCnt++;
                    else if (feedback == "N") dislikeCnt++;

                    if (!dr["ResponseTimeMs"].IsNullOrEmpty())
                    {
                        resTimeSum += Convert.ToDouble(dr["ResponseTimeMs"]);
                        resTimeCnt++;
                    }
                }
            }

            int feedbackCnt = likeCnt + dislikeCnt;     //평가된 건수 (만족도 모수)

            lblValueLike.Text = likeCnt.ToString("#,##0");
            lblValueDislike.Text = dislikeCnt.ToString("#,##0");
            lblValueSatis.Text = feedbackCnt == 0 ? "-" : ((double)likeCnt / feedbackCnt * 100).ToString("0.0") + "%";
            lblValueResTime.Text = resTimeCnt == 0 ? "-" : (resTimeSum / resTimeCnt / 1000).ToString("0.0") + "초";
        }

        #endregion 조회 기능 End

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_List);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
