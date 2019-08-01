using System;
using System.Globalization;
using System.Threading;

namespace Library.Converter
{
    public class DateConverter
    {
        public static void ChangeCurrentCulture()
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "ar")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            }
        }

        public static string DatetTimeToString(DateTime date)
        {
            try
            {
                ChangeCurrentCulture();
                return date.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        public static DateTime StringToDateTime(string dateString)
        {
            try
            {
                ChangeCurrentCulture();

                if (dateString.Length == 10)
                {
                    return DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    return DateTime.ParseExact(dateString, "dddd, dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                return default(DateTime);
            }
        }
        public static DateTime? StringToDateTimeNullable(string dateString)
        {
            try
            {
                if (!String.IsNullOrEmpty(dateString))
                {
                    ChangeCurrentCulture();

                    if (dateString.Length == 10)
                    {
                        return DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        return DateTime.ParseExact(dateString, "dddd, dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return default(DateTime);
            }
        }
        public static DateTime StringToDateWithTime(string dateString)
        {
            try
            {
                ChangeCurrentCulture();

                if (dateString.Length == 16)
                {
                    return DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                else
                {
                    return DateTime.ParseExact(dateString, "dddd, dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                return default(DateTime);
            }
        }
        public static string DateTimeToStringWithTime(DateTime date)
        {
            try
            {
                ChangeCurrentCulture();
                return date.ToString("dd/MM/yyyy HH:mm");
            }
            catch
            {
                return "";
            }
        }

        public static DateTime StringToDateTimeWithAddDays(string dateString, int numberOfDaysToAdd)
        {
            return StringToDateTime(dateString).AddDays(numberOfDaysToAdd);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            DayOfWeek firstDay = defaultCultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }

        public static DateTime GetLastDayOfWeek(DateTime day)
        {
            return day.AddDays(6);
        }

        public static int GetDays(string endDate, string startDate)
        {
            DateTime startDate1 = StringToDateTime(startDate);
            DateTime endDate1 = StringToDateTime(endDate);

            return (endDate1 - startDate1).Days;
        }

        public static int GetDays(DateTime endDate, DateTime startDate)
        {
            return (endDate - startDate).Days;
        }

        public static bool IsValidInterval(DateTime startDate, DateTime endDate)
        {
            bool result = true;

            if (startDate >= endDate)
            {
                result = false;
            }

            return result;
        }

        public static bool IsValidInterval(string startDate, string endDate)
        {
            DateTime startDate1 = StringToDateTime(startDate);
            DateTime endDate1 = StringToDateTime(endDate);
            bool result = true;

            if (startDate1 >= endDate1)
            {
                result = false;
            }

            return result;
        }

        public static bool IsValidUnderInterval(DateTime startDate, DateTime endDate, DateTime bookingStartDate, DateTime bookingEndDate)
        {
            if (startDate >= bookingStartDate && endDate <= bookingEndDate)
            {
                return true;
            }
            return false;
        }

        public static DateTime StringToSpecialDateTime(string dateString)
        {
            try
            {
                return DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch
            {
                return default(DateTime);
            }
        }
    }
}
