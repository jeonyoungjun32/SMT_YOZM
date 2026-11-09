using System;

namespace SmartCommon.Common
{

    // 일자 범위 타입 열거
    public enum DateRangeType
    {
        CurrentMonth,           // 1. 현재 월 (1일~말일)
        CurrentDatePlusWeek,    // 2. 현재일자 + 7일
        CurrentDatePlusDays,    // 3. 현재일자 + 사용자 지정 일수
        CurrentAndNextMonth,    // 4. 현재 월 + 다음 월
        ThisWeek,               // 5. 이번 주 (월요일~일요일)
    }

    /// <summary>
    /// 일자 범위 클래스
    /// </summary>
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateRange()
        {

        }
        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    /// <summary>
    /// 일자 팩토리 클래스 : 일자 계산 캡슐화하여 재사용 가능
    /// </summary>
    public class clsDateFactory
    {
        /// <summary>
        /// 지정된 일자 범위 타입에 따라 DateRange 객체를 생성
        /// </summary>
        /// <param name="type">일자 범위 타입</param>
        /// <param name="additionalDays">추가 일수 (CurrentDatePlusDays 타입에서 사용)</param>
        /// <returns>계산된 DateRange 객체</returns>

        public static DateRange CreateDateRange(DateRangeType type, int additionalDays = -7)
        {
            switch (type)
            {
                case DateRangeType.CurrentMonth:
                    return CreateCurrentMonth();

                case DateRangeType.CurrentDatePlusWeek:
                    return CreateCurrentDatePlusDays(7);

                case DateRangeType.CurrentDatePlusDays:
                    return CreateCurrentDatePlusDays(additionalDays);

                case DateRangeType.CurrentAndNextMonth:
                    return CreateCurrentAndNextMonth();

                case DateRangeType.ThisWeek:
                    return CreateThisWeek();

                default:
                    return CreateCurrentDatePlusDays(7);
            }
        }

        /// <summary>
        /// 일자 컨트롤에 일자 범위를 설정
        /// </summary>
        /// <param name="dateControl">smtSearchDateTime 컨트롤</param>
        /// <param name="type">일자 범위 타입</param>
        /// <param name="additionalDays">추가 일수 (CurrentDatePlusDays 타입에서 사용)</param>
        
        /*
        public static void SetDateRange(dynamic dateControl, DateRangeType type, int additionalDays = 7)
        {
            DateRange range = CreateDateRange(type, additionalDays);

            dateControl.smtStartDateTimeValue = range.StartDate;
            dateControl.smtEndDateTimeValue = range.EndDate;
        }
        */

        public static void SetDateRange(dynamic dateControl, DateRangeType type, int additionalDays = -7)
        {
            DateRange range = CreateDateRange(type, additionalDays);

            dateControl.smtStartDateTimeValue = range.StartDate;
            dateControl.smtEndDateTimeValue = range.EndDate; 
        }

        #region 내부 메서드

        /// <summary>
        /// 현재 월의 시작일과 마지막일을 반환
        /// </summary>
        private static DateRange CreateCurrentMonth()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            return new DateRange(startDate, endDate);
        }

        /// <summary>
        /// 2. 현재일자에서 + 7일 추가 (일주일 범위)
        /// </summary>
        private static DateRange CreateCurrentDatePlusWeek()
        {
            return CreateCurrentDatePlusDays(7);
        }

        /// <summary>
        /// 3. 현재일자에서 + 사용자 지정 일수
        /// </summary>
        private static DateRange CreateCurrentDatePlusDays(int days)
        {
            DateTime today = DateTime.Now.Date;
            DateTime endDate = today.AddDays(days);

            if (days < 0)
            {
                return new DateRange(endDate, today);  // 과거날짜가 시작일
            }
            else
            {
                return new DateRange(today, endDate);   // 오늘이 시작일
            }
        }

        /// <summary>
        /// 4. 현재 월 + 다음 월 (2개월 범위)
        /// </summary>
        private static DateRange CreateCurrentAndNextMonth()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);  // 현재 월 1일
            DateTime endDate = startDate.AddMonths(2).AddDays(-1);      // 다음 달 마지막일

            return new DateRange(startDate, endDate);
        }


        /// <summary>
        /// 5. 이번 주의 시작일(월요일)과 마지막일(일요일)
        /// </summary>
        private static DateRange CreateThisWeek()
        {
            DateTime now = DateTime.Now;
            int daysFromMonday = ((int)now.DayOfWeek - 1 + 7) % 7;
            DateTime startDate = now.Date.AddDays(-daysFromMonday);
            DateTime endDate = startDate.AddDays(6);

            return new DateRange(startDate, endDate);
        }
        #endregion
    }
}
