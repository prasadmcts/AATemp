using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DateTimeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var a = 5.SteppingNumbers(3);

            //WeeksRelated();
            MonthsRelated();
            //YearsRelated();
        }
        private static void WeeksRelated()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddMonths(1);
            var months = startDate.GetWeeks(endDate);
            months.ToList().ForEach(Console.WriteLine);

            for (int i = 0; i < months.Count(); i++)
            {
                DateTime newDate = startDate.AddDays(i * 7);

                Console.WriteLine($"StartWeek : {newDate.StartOfTheWeek()}   EndWeek : {newDate.EndOfTheWeek()}");
            }
        }

        private static void MonthsRelated()
        {
            DateTime startDate = DateTime.Now.AddMonths(4);
            DateTime endDate = startDate.AddMonths(2);
            var months = startDate.GetMonths(endDate);
            months.ToList().ForEach(Console.WriteLine);

            for (int i = 0; i < months.Count(); i++)
            {
                DateTime newDate = startDate.AddMonths(i);

                Console.WriteLine($"StartDate : {newDate.StartOfTheMonth()}   EndDate : {newDate.EndOfTheMonth()}");
            }
        }

        private static void YearsRelated()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddYears(5);
            var years = startDate.GetYears(endDate);
            years.ToList().ForEach(Console.WriteLine);

            for (int i = 0; i < years.Count(); i++)
            {
                DateTime newDate = startDate.AddYears(i);

                Console.WriteLine($"StartDate : {newDate.StartOfTheYear()}   EndDate : {newDate.EndOfTheYear()}");
            }
        }
    }

    public static class DateTimeExtensions
    {
        public static IEnumerable<int> SteppingNumbers(this int start, int end)
        {
            if (start > end)
            {
                return Enumerable.Empty<int>();
            }
            return Enumerable.Range(start, end - start);
        }

        #region Week

        // Always uses Monday-to-Sunday weeks
        public static DateTime StartOfTheWeek(this DateTime date)
        {
            // Using +6 here leaves Monday as 0, Tuesday as 1 etc.
            int dayOfWeek = (((int)date.DayOfWeek) + 6) % 7;
            return date.Date.AddDays(-dayOfWeek);
        }

        public static DateTime EndOfTheWeek(this DateTime date)
        {
            return date.StartOfTheWeek().AddDays(6);
        }

        public static int GetWeekNumber(this DateTime dateTime)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static int GetWeekDifference(this DateTime startDate, DateTime endDate)
        {
            startDate = StartOfTheWeek(startDate);
            endDate = StartOfTheWeek(endDate);
            int days = (int)(endDate - startDate).TotalDays;
            return (days / 7) + 1; // Adding 1 to be inclusive
        }

        public static IEnumerable<int> GetWeeks(this DateTime startDate, DateTime endDate)
        {
            int totalWeeks = GetWeekDifference(startDate, endDate);

            return startDate.GetWeekNumber().SteppingNumbers(startDate.GetWeekNumber() + totalWeeks);
        }


        #endregion

        #region Month

        public static DateTime StartOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime EndOfTheMonth(this DateTime date)
        {
            var endOfTheMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            return endOfTheMonth;
        }

        public static int GetMonthNumber(this DateTime dateTime)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int monthNum = ciCurr.Calendar.GetMonth(dateTime);
            return monthNum;
        }

        public static int GetMonthDifference(this DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        public static IEnumerable<int> GetMonths(this DateTime startDate, DateTime endDate)
        {
            int totalMonths = GetMonthDifference(startDate, endDate);

            return startDate.GetMonthNumber().SteppingNumbers(startDate.GetMonthNumber() + totalMonths);
        }

        #endregion

        #region Year

        public static DateTime StartOfTheYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime EndOfTheYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        public static int GetYearNumber(this DateTime dateTime)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int yearNum = ciCurr.Calendar.GetYear(dateTime);
            return yearNum;
        }

        public static IEnumerable<int> GetYears(this DateTime startDate, DateTime endDate)
        {
            int totalYears = GetYearDifference(startDate, endDate);

            return startDate.GetYearNumber().SteppingNumbers(startDate.GetYearNumber() + totalYears);
        }

        public static int GetYearDifference(this DateTime startDate, DateTime endDate)
        {
            return (endDate.Year - startDate.Year - 1) + (((endDate.Month > startDate.Month) || ((endDate.Month == startDate.Month) && (endDate.Day >= startDate.Day))) ? 1 : 0);
        }

        #endregion
    }
}