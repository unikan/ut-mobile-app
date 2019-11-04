using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using UtMobileApp.Models;
using Xamarin.Forms;

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

        public void AddAppointment(List<Models.ScheduleJSON.Entry> scheduleList, int index, ScheduleAppointmentCollection scheduleAppointmentCollection, int[, ] dates, int day)
        {
            // We need hour in one variable, minutes in one variable, so we split the string and save it in an array
            string[] startTime = scheduleList[index].BeginningTime.t.Split(':');
            string[] endTime = scheduleList[index].EndingTime.t.Split(':');

            Color color;
            string teacher, subject, lectureorexercises, venue, group;

            if (scheduleList[index].LectureOrExercise != null)
            {
                if (scheduleList[index].LectureOrExercise.t == "L") color = Color.FromHex("#F28883"); // #FFB5AC
                else color = Color.FromHex("#B9A6E0"); // #A2D5F2
            }
            else
            {
                color = Color.Transparent;
            }

            teacher = (scheduleList[index].Teacher != null) ? scheduleList[index].Teacher.t : "";
            subject = (scheduleList[index].Subjects != null) ? scheduleList[index].Subjects.t : "";
            lectureorexercises = (scheduleList[index].LectureOrExercise != null) ? scheduleList[index].LectureOrExercise.t : "";
            venue = (scheduleList[index].Venue != null) ? scheduleList[index].Venue.t : "";
            group = (scheduleList[index].Groups != null) ? " | " + scheduleList[index].Groups.t : "";

            // Adding schedule appointment in schedule appointment collection 
            scheduleAppointmentCollection.Add(new ScheduleAppointment()
            {
                StartTime = new DateTime(dates[day, 0], dates[day, 1], dates[day, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                EndTime = new DateTime(dates[day, 0], dates[day, 1], dates[day, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                Subject = "\n" + teacher + "\n" +
                            subject + " (" + lectureorexercises + ")" + group + "\n" +
                            "Venue: " + venue,
                Color = color
            });
        }
    }
}
