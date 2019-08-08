    public static class DateTimeExtensions
    {
        public static int GetWeekNumber(this DateTime dateTime)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static int GetMonthNumber(this DateTime dateTime)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int monthNum = ciCurr.Calendar.GetMonth(dateTime);
            return monthNum;
        }

        public static int GetYearNumber(this DateTime dateTime)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int yearNum = ciCurr.Calendar.GetYear(dateTime);
            return yearNum;
        }

        public static DateTime EndOfTheMonth(this DateTime date)
        {
            var endOfTheMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            return endOfTheMonth;
        }

        public static int GetQuarter(this DateTime fromDate)
        {
            return ((fromDate.Month - 1) / 3) + 1;
        }

        public static DateTime GetMax(this DateTime first, DateTime second)
        {
            return new[] { first, second }.Max();
        }

        public static DateTime GetMin(this DateTime first, DateTime second)
        {
            return new[] { first, second }.Min();
        }

        public static int GetTotalDiffDays(this DateTime first, DateTime second)
        {
            return (int)Math.Abs(Math.Round(first.Subtract(second).TotalDays));
        }
    }
