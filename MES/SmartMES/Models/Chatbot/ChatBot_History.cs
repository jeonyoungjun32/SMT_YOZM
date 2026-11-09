using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.ChatBot
{
    class ChatBot_History
    {
        #region Property
        public string EmployeeId { get; set; }
        public string FeedbackYn { get; set; }
        public string RefMatchYn { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable SearchChatbotHistory(string startDate, string endDate)
        {
            query = $@"
            SELECT
                   ROW_NUMBER() OVER(ORDER BY LastMsgDt DESC) AS RowNo,
                   *
              FROM
                 (
                  SELECT
                          EmployeeId
                        , SessionNo
                        , CONVERT(char(10), MIN(CreDt), 120) AS ChatDate
                        , COUNT(*) AS MessageCnt
                        , MIN(CreDt) AS FirstMsgDt
                        , MAX(CreDt) AS LastMsgDt
                   FROM ChatBot_History
                  WHERE CreDt >= '{startDate}'
                    AND CreDt < DATEADD(DAY, 1, '{endDate}')
            ";

            if (EmployeeId != "" && EmployeeId != "ALL")
            {
                query += $"\n AND EmployeeId = '{EmployeeId}' ";
            }

            query += $@" GROUP BY SessionNo, EmployeeId
                           ) T
                       ORDER BY LastMsgDt DESC ";


            return db.GetDataTable(query);
        }

        /// <summary>
        /// 응답품질 조회 (Frm_Chat_ResponseQuality)
        /// </summary>
        public DataTable Select_ChatBot_History(string startDate, string endDate)
        {
            EmployeeId = EmployeeId.ConvertString_Null_To_Empty();
            FeedbackYn = FeedbackYn.ConvertString_Null_To_Empty();
            RefMatchYn = RefMatchYn.ConvertString_Null_To_Empty();

            query = $@"
                SELECT CreDt                                        --질의일시
                     , Question                                     --사용자질의
                     , CASE WHEN LEN(ISNULL(Answer, '')) > 100
                            THEN LEFT(Answer, 100) + '...'
                            ELSE Answer
                       END                          AS AnswerSummary --AI답변요약
                     , FeedbackYn                                   --피드백 (Y=만족, N=불만족, NULL=미평가)
                     , Rating                                       --평점 (1~5)
                     , ResponseTimeMs                               --응답시간(ms)
                     , RefType                                      --참조유형
                     , RefMatchYn                                   --매칭여부
                     , IsError                                      --오류여부
                     , EmployeeId                                   --사용자
                     , SessionNo                                    --세션
                     , FeedbackDt                                   --피드백일시
                     , [Comment]                                    --코멘트
                     , Answer                                       --AI답변 전문
                     , ExecSql                                      --실행SQL
                     , ResultCnt                                    --조회건수
                     , ChatSeq                                      --채번PK
                  FROM ChatBot_History
                 WHERE CorpId = '{WrGlobal.CorpID}'
                   AND CreDt >= '{startDate}'
                   AND CreDt <  DATEADD(DAY, 1, '{endDate}') ";

            if (EmployeeId != "" && EmployeeId != "ALL")
            {
                query += $@"
                   AND EmployeeId = '{EmployeeId}' ";
            }

            if (FeedbackYn != "" && FeedbackYn != "ALL")
            {
                query += $@"
                   AND FeedbackYn = '{FeedbackYn}' ";
            }

            if (RefMatchYn != "" && RefMatchYn != "ALL")
            {
                query += $@"
                   AND RefMatchYn = '{RefMatchYn}' ";
            }

            query += @"
                 ORDER BY CreDt DESC, ChatSeq DESC ";

            return db.GetDataTable(query);
        }
    }
}
