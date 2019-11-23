using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
            string[] startTime = scheduleList[index].L_BeginningTime.t.Split(':');
            string[] endTime = scheduleList[index].L_EndingTime.t.Split(':');

            Color color;
            string teacher, subject, lectureorexercises, venue, group;

            if (scheduleList[index].L_LectureOrExercise != null)
            {
                if (scheduleList[index].L_LectureOrExercise.t == "L") color = Color.FromHex("#F28883"); // #FFB5AC
                else color = Color.FromHex("#B9A6E0"); // #A2D5F2
            }
            else
            {
                color = Color.Transparent;
            }

            teacher = (scheduleList[index].L_Teacher != null) ? scheduleList[index].L_Teacher.t : "";
            subject = (scheduleList[index].L_Subjects != null) ? scheduleList[index].L_Subjects.t : "";
            lectureorexercises = (scheduleList[index].L_LectureOrExercise != null) ? scheduleList[index].L_LectureOrExercise.t : "";
            venue = (scheduleList[index].L_Venue != null) ? scheduleList[index].L_Venue.t : "";
            group = (scheduleList[index].L_Groups != null) ? " | " + scheduleList[index].L_Groups.t : "";

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

        public CalendarEventCollection AddAppointemntMidterms(List<Models.MidtermsJSON.Entry> scheduleList)
        {
            // Creating an instance of calendar event collection
            CalendarEventCollection calendarEventCollection = new CalendarEventCollection();

            string semester, teacher, subject, venue1, venue2, group1, group2;

            for (int i = 0; i < scheduleList.Count; i++)
            {
                semester = (scheduleList[i].M_Semester != null) ? scheduleList[i].M_Semester.t : "";
                teacher = (scheduleList[i].M_Teacher != null) ? scheduleList[i].M_Teacher.t : "";
                subject = (scheduleList[i].M_Subjects != null) ? scheduleList[i].M_Subjects.t : "";
                venue1 = (scheduleList[i].M_Venue1 != null) ? scheduleList[i].M_Venue1.t : "";
                venue2 = (scheduleList[i].M_Venue2 != null) ? scheduleList[i].M_Venue2.t : "";
                group1 = (scheduleList[i].M_Group1 != null) ? "Gr: " + scheduleList[i].M_Group1.t : "";
                group2 = (scheduleList[i].M_Group2 != null) ? "Gr: " + scheduleList[i].M_Group2.t : "";

                if (scheduleList[i].M_Date1 != null && scheduleList[i].M_Time1 != null)
                {
                    // We need day in one variable, month in one variable, so we split the string and save it in an array
                    string[] Date1 = scheduleList[i].M_Date1.t.Split('/'); // First term
                    // We need hour in one variable, minutes in one variable, so we split the string and save it in an array
                    string[] Time1 = scheduleList[i].M_Time1.t.Split(':');

                    calendarEventCollection.Add(new CalendarInlineEvent()
                    { 
                        StartTime = new DateTime(DateTime.Now.Year, int.Parse(Date1[1]), int.Parse(Date1[0]), int.Parse(Time1[0]), int.Parse(Time1[1]), 0),
                        EndTime = new DateTime(DateTime.Now.Year, int.Parse(Date1[1]), int.Parse(Date1[0]), int.Parse(Time1[0]) + 1, int.Parse(Time1[1]), 0),
                        Subject = "[" + semester + "] "
                                    + subject + " - "
                                    + teacher + "\n"
                                    + "Venue: " + venue1 + " "
                                    + group1,
                        Color = Color.FromHex("#F28883")
                    });
                }
                if (scheduleList[i].M_Date2 != null && scheduleList[i].M_Time2 != null)
                {
                    // We need day in one variable, month in one variable, so we split the string and save it in an array
                    string[] Date2 = scheduleList[i].M_Date2.t.Split('/'); // First term
                    // We need hour in one variable, minutes in one variable, so we split the string and save it in an array
                    string[] Time2 = scheduleList[i].M_Time2.t.Split(':');

                    calendarEventCollection.Add(new CalendarInlineEvent()
                    {
                        StartTime = new DateTime(DateTime.Now.Year, int.Parse(Date2[1]), int.Parse(Date2[0]), int.Parse(Time2[0]), int.Parse(Time2[1]), 0),
                        EndTime = new DateTime(DateTime.Now.Year, int.Parse(Date2[1]), int.Parse(Date2[0]), int.Parse(Time2[0]) + 1, int.Parse(Time2[1]), 0),
                        Subject = "[" + semester + "] "
                                    + subject + " - "
                                    + teacher + "\n"
                                    + "Venue: " + venue2 + " "
                                    + group2,
                        Color = Color.FromHex("#B9A6E0")
                    });
                }
            }

            return calendarEventCollection;
        }
    }
}
