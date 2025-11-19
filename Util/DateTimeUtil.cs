using System.Text.RegularExpressions;

namespace ReservationSystem.Util
{
    public class DateTimeUtil
    {
        public static string GetDateId(DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }
        public static DateTime? DateIdToDate(string dateId)
        {
            try
            {
                int day = int.Parse(dateId.Substring(6, 2));
                int month = int.Parse(dateId.Substring(4, 2));
                int year = int.Parse(dateId.Substring(0, 4));

                return new DateTime(year, month, day); ;
            }
            catch
            {
                return null;
            }
        }

        public static string GetDisplayDate(DateTime dt)
        {
            return dt.ToString("dd.MM.yyyy");
        }
        public static string? GetDisplayDate(DateTime? dt)
        {
            return dt == null ? null : GetDisplayDate(dt.Value);
        }
        public static DateTime? DisplayDateToDate(string displayDate, string? separator = null)
        {
            try
            {
                string itemSeparator = string.IsNullOrEmpty(separator) ? "." : separator;
                string[] items = Regex.Split(displayDate.Replace(itemSeparator, ";").Replace(" ", ";"), ";");
                int day = int.Parse(items[0]);
                int month = int.Parse(items[1]);
                int year = int.Parse(items[2]);

                return new DateTime(year, month, day);
            }
            catch
            {
                return null;
            }
        }
        public static string GetDisplayDateTime(DateTime dt, bool withSeconds = false)
        {
            return withSeconds ? dt.ToString("dd.MM.yyyy HH:mm:ss") : dt.ToString("dd.MM.yyyy HH:mm");
        }
        public static string GetDisplayDateTime(DateTime? dt, bool withSeconds = false)
        {
            return dt == null ? string.Empty : GetDisplayDateTime(dt.Value, withSeconds);
        }

        public static DateTime? DisplayDataToDateTime(string displayDateTime, DateTime? defaultDateTime, string? separator = null, bool withSeconds = false)
        {
            try
            {
                string[] items = Regex.Split(displayDateTime.Trim().Replace(" ", ";"), ";");

                return DisplayDataToDateTime(items[0], items[1], defaultDateTime, separator, withSeconds);
            }
            catch
            {
                return defaultDateTime;
            }
        }

        public static DateTime? DisplayDataToDateTime(string displayDate, string displayTime, DateTime? defaultDate, string? separator = null, bool withSeconds = false)
        {
            DateTime? date = DisplayDateToDate(displayDate, separator);
            TimeSpan? time = DisplayTimeToTime(displayTime, withSeconds);

            if (defaultDate != null)
            {
                if (date == null)
                {
                    date = defaultDate.Value.Date;
                }
                if (time == null)
                {
                    time = defaultDate.Value.TimeOfDay;
                }
            }

            if (date != null && time != null)
            {
                return CreateNewDateTime((DateTime)date, (TimeSpan)time);
            }
            else
            {
                return null;
            }

        }
        private static DateTime CreateNewDateTime(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }

        public static string GetDisplayTime(DateTime dt)
        {
            return dt.ToString("HH:mm");
        }
        public static string? GetDisplayTime(DateTime? dt)
        {
            return dt == null ? null : GetDisplayTime(dt.Value);
        }

        public static TimeSpan? DisplayTimeToTime(string displayTime, bool parseSeconds = false)
        {
            try
            {
                string[] items = Regex.Split(displayTime.Replace(":", ";"), ";");
                int hours = int.Parse(items[0]);
                int minutes = int.Parse(items[1]);
                int seconds = 0;
                if (parseSeconds && items.Length > 2)
                {
                    if (!int.TryParse(items[2], out seconds))
                    {
                        seconds = 0;
                    }
                }

                return new TimeSpan(hours, minutes, seconds);
            }
            catch
            {
                return null;
            }
        }

        public static string GetMonthName(DateTime dt)
        {
            return GetMonthName(dt.Month);
        }

        public static string GetMonthName(int monthNum)
        {
            switch (monthNum)
            {
                case 1:
                    return "január";
                case 2:
                    return "február";
                case 3:
                    return "marec";
                case 4:
                    return "apríl";
                case 5:
                    return "máj";
                case 6:
                    return "jún";
                case 7:
                    return "júl";
                case 8:
                    return "august";
                case 9:
                    return "september";
                case 10:
                    return "október";
                case 11:
                    return "november";
                case 12:
                    return "december";
            }

            return string.Empty;
        }

        public static string GetDayOfWeek(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Pondelok";
                case DayOfWeek.Tuesday:
                    return "Utorok";
                case DayOfWeek.Wednesday:
                    return "Streda";
                case DayOfWeek.Thursday:
                    return "Štvrtok";
                case DayOfWeek.Friday:
                    return "Piatok";
                case DayOfWeek.Saturday:
                    return "Sobota";
                case DayOfWeek.Sunday:
                    return "Nedeľa";
            }

            return string.Empty;
        }

        public static DateTime GetWeekMonday(DateTime dt)
        {
            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = dt.AddDays(-1);
            }

            return dt;
        }

        public static int GetDayDiff(DateTime firstDate, DateTime secondDate)
        {
            DateTime dtFrom = firstDate < secondDate ? firstDate : secondDate;
            DateTime dtTo = firstDate < secondDate ? secondDate : firstDate;
            int idx = 0;
            while (dtFrom < dtTo)
            {
                dtFrom = dtFrom.AddDays(1);
                idx++;
            }

            return idx;
        }

        /// <summary>
        /// Get birth date from birdth ID
        /// Expecting birth id without slash
        /// Example: 7007251234
        /// </summary>
        /// <param name="birthId">Birth ID</param>
        /// <returns></returns>
        public static DateTime? BirthIdToDate(string birthId)
        {
            if (string.IsNullOrEmpty(birthId))
            {
                // No birth id specified
                return null;
            }


            // Check the birth id number length
            if (birthId.Length != 9 && birthId.Length != 10)
            {
                return null;
            }

            // Check date characters
            short year;
            short month;
            short day;
            if (!Int16.TryParse(birthId.Substring(0, 2), out year) ||
                !Int16.TryParse(birthId.Substring(2, 2), out month) ||
                !Int16.TryParse(birthId.Substring(4, 2), out day))
            {
                return null;
            }

            if (year >= 54)
                year += 1900;
            else if (year < 54 && birthId.Length == 9)
                year += 1900;
            else
                year += 2000;

            if (month > 50)
            {
                // Female
                month -= 50;
            }
            else
            {
                // Male
            }

            // Check date validity
            try
            {
                return new DateTime(year, month, day);
            }
            catch
            {
                return null;
            }
        }
    }
}
