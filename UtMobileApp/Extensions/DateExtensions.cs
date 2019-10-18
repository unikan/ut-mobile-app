using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using UtMobileApp.Models;

namespace UtMobileApp.Extensions
{
    public class DateExtensions
    {
        public int[,] DatesOfWeek1(DateTime Date)
        {
            int diff = (7 + (Date.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime monday = Date.AddDays(-1 * diff).Date;

            int[,] allDates = new int[6, 3];
            // Monday: Year, Month, Day
            allDates[0, 0] = int.Parse(monday.ToString("yyyy"));
            allDates[0, 1] = int.Parse(monday.ToString("MM"));
            allDates[0, 2] = int.Parse(monday.ToString("dd"));
            // Tuesday: Year, Month, Day
            allDates[1, 0] = int.Parse(monday.AddDays(1).ToString("yyyy"));
            allDates[1, 1] = int.Parse(monday.AddDays(1).ToString("MM"));
            allDates[1, 2] = int.Parse(monday.AddDays(1).ToString("dd"));
            // Wednesday: Year, Month, Day
            allDates[2, 0] = int.Parse(monday.AddDays(2).ToString("yyyy"));
            allDates[2, 1] = int.Parse(monday.AddDays(2).ToString("MM"));
            allDates[2, 2] = int.Parse(monday.AddDays(2).ToString("dd"));
            // Thursday: Year, Month, Day
            allDates[3, 0] = int.Parse(monday.AddDays(3).ToString("yyyy"));
            allDates[3, 1] = int.Parse(monday.AddDays(3).ToString("MM"));
            allDates[3, 2] = int.Parse(monday.AddDays(3).ToString("dd"));
            // Friday: Year, Month, Day
            allDates[4, 0] = int.Parse(monday.AddDays(4).ToString("yyyy"));
            allDates[4, 1] = int.Parse(monday.AddDays(4).ToString("MM"));
            allDates[4, 2] = int.Parse(monday.AddDays(4).ToString("dd"));
            // Saturday: Year, Month, Day
            allDates[5, 0] = int.Parse(monday.AddDays(5).ToString("yyyy"));
            allDates[5, 1] = int.Parse(monday.AddDays(5).ToString("MM"));
            allDates[5, 2] = int.Parse(monday.AddDays(5).ToString("dd"));

            return allDates;
        }
    }
}
