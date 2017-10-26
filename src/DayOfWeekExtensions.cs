using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MBran.DayOfWeekPicker
{
    public static class DayOfWeekExtensions
    {
        public static Dictionary<DayOfWeek, List<DayOfWeek>> GetSuccessiveDays(this IEnumerable<DayOfWeek> days)
        {
            Dictionary<DayOfWeek, List<DayOfWeek>> daysInARow = new Dictionary<DayOfWeek, List<DayOfWeek>>();
            if (days == null || !days.Any())
            {
                return daysInARow;
            }

            IEnumerable<DayOfWeek> sortedDays = days.Distinct()
                .OrderBy(day => day);
            int previousDay = (int)sortedDays.Last();

            foreach (DayOfWeek day in sortedDays)
            {
                int dayNum = (int)day;
                if (daysInARow.Count == 0 || dayNum - previousDay != 1)
                {
                    daysInARow.Add((DayOfWeek)dayNum, new List<DayOfWeek>());
                }

                daysInARow.LastOrDefault().Value.Add((DayOfWeek)dayNum);
                previousDay = dayNum;
            }

            return daysInARow;
        }

        public static List<List<string>> GetSuccessiveDayNames(this IEnumerable<DayOfWeek> days)
        {
            List<List<string>> successiveNames = new List<List<string>>();
            if (days == null || !days.Any())
            {
                return successiveNames;
            }

            Dictionary<DayOfWeek, List<DayOfWeek>> successiveDays = days.GetSuccessiveDays();
            successiveNames.AddRange(
               successiveDays.Select(dayGroup => dayGroup.Value.GetNames() as List<string>)
           );

            return successiveNames;
        }

        public static List<List<string>> GetSuccessiveAbbreviatedDayNames(this IEnumerable<DayOfWeek> days, int? nameCharactersLimit = null)
        {
            List<List<string>> successiveNames = new List<List<string>>();
            if (days == null || !days.Any())
            {
                return successiveNames;
            }

            Dictionary<DayOfWeek, List<DayOfWeek>> successiveDays = days.GetSuccessiveDays();
            successiveNames.AddRange(
                successiveDays.Select(dayGroup => dayGroup.Value.GetAbbreviatedNames())
            );

            return successiveNames;
        }

        public static List<string> GetNames(this IEnumerable<DayOfWeek> days)
        {
            List<string> dayNames = new List<string>();
            if (days == null || !days.Any())
            {
                return dayNames;
            }

            dayNames.AddRange(
                days.Select(day => day.GetDayName())
            );

            return dayNames;
        }

        public static List<string> GetAbbreviatedNames(this IEnumerable<DayOfWeek> days, int? nameCharactersLimit = null)
        {
            List<string> dayNames = new List<string>();
            if (days == null || !days.Any())
            {
                return dayNames;
            }

            dayNames.AddRange(
                days.Select(day => day.GetAbbreviatedDayName(nameCharactersLimit))
            );

            return dayNames;
        }

        public static string GetDayName(this DayOfWeek day)
        {
            DateTimeFormatInfo currentFormat = DateTimeFormatInfo.CurrentInfo;
            return currentFormat.GetDayName(day);
        }

        public static string GetAbbreviatedDayName(this DayOfWeek day, int? nameCharactersLimit = null)
        {
            DateTimeFormatInfo currentFormat = DateTimeFormatInfo.CurrentInfo;
            string dayName = currentFormat.GetAbbreviatedDayName(day);
            if (nameCharactersLimit != null)
            {
                dayName = dayName.Substring(0, (int)nameCharactersLimit);
            }
            return dayName;

        }
    }
}