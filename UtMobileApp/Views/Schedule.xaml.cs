using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static UtMobileApp.Models.ScheduleJSON;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Schedule : ContentPage
    {
        public Schedule()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            string url = "https://spreadsheets.google.com/feeds/list/1SFGzFIq8K7va4HzT9PZUwRNxch_yqZItn80-Kwu-u6c/3/public/values?alt=json";
            var LoadSchedule = new Extensions.LoadSchedule();
            List<Models.ScheduleJSON.Entry> scheduleList = await LoadSchedule.DeserializeJsonAsync(url);

            // Get dates of every day of first week
            Extensions.DateExtensions de = new Extensions.DateExtensions();
            int[,] dates = new int[6, 3];
            dates = de.DatesOfWeek1();

            // Number of weeks per semester, will save it firebase to update it easily
            int numOfWeeks = 13;

            // Creating an instance for schedule appointment collection
            ScheduleAppointmentCollection scheduleAppointmentCollection = new ScheduleAppointmentCollection();

            string[] startTime = new string[2]; // Hour is saved on first element, minutes in second element for Start Time
            string[] endTime = new string[2]; // Hour is saved on first element, minutes in second element for End Time

            Color color;
            string teacher, subject, lectureorexercises, venue, group;

            for (int i = 0; i < scheduleList.Count; i++)
            {
                if (scheduleList[i].Day != null && scheduleList[i].BeginningTime != null && scheduleList[i].EndingTime != null)
                {
                    if (scheduleList[i].LectureOrExercise != null)
                    {
                        if (scheduleList[i].LectureOrExercise.t == "L") color = Color.LightGreen;
                        else color = Color.LightCoral;
                    }
                    else
                    {
                        color = Color.Transparent;
                    }

                    teacher = (scheduleList[i].Teacher != null) ? scheduleList[i].Teacher.t : "";
                    subject = (scheduleList[i].Subjects != null) ? scheduleList[i].Subjects.t : "";
                    lectureorexercises = (scheduleList[i].LectureOrExercise != null) ? scheduleList[i].LectureOrExercise.t : "";
                    venue = (scheduleList[i].Venue != null) ? scheduleList[i].Venue.t : "";
                    group = (scheduleList[i].Groups != null) ? " | " + scheduleList[i].Groups.t : "";

                    if (scheduleList[i].Day.t == "e hënë" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        for (int j = 0; j < numOfWeeks; j++)
                        {
                            scheduleAppointmentCollection.Add(new ScheduleAppointment()
                            {
                                StartTime = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0).AddDays(j * 7),
                                EndTime = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0).AddDays(j * 7),
                                Subject = "\n" + teacher + "\n" +
                                          subject + " (" + lectureorexercises + ")" + group + "\n" +
                                          "Venue: " + venue,
                                Color = color
                            });
                        }
                    }
                    else if (scheduleList[i].Day.t == "e martë" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        for (int j = 0; j <= numOfWeeks; j++)
                        {
                            scheduleAppointmentCollection.Add(new ScheduleAppointment()
                            {
                                StartTime = new DateTime(dates[1, 0], dates[1, 1], dates[1, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0).AddDays(j * 7),
                                EndTime = new DateTime(dates[1, 0], dates[1, 1], dates[1, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0).AddDays(j * 7),
                                Subject = "\n" + teacher + "\n" +
                                          subject + " (" + lectureorexercises + ")" + group + "\n" +
                                          "Venue: " + venue,
                                Color = color
                            });
                        }
                    }
                    else if (scheduleList[i].Day.t == "e mërkurë" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        for (int j = 0; j <= numOfWeeks; j++)
                        {
                            scheduleAppointmentCollection.Add(new ScheduleAppointment()
                            {
                                StartTime = new DateTime(dates[2, 0], dates[2, 1], dates[2, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0).AddDays(j * 7),
                                EndTime = new DateTime(dates[2, 0], dates[2, 1], dates[2, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0).AddDays(j * 7),
                                Subject = "\n" + teacher + "\n" +
                                          subject + " (" + lectureorexercises + ")" + group + "\n" +
                                          "Venue: " + venue,
                                Color = color
                            });
                        }
                    }
                    else if (scheduleList[i].Day.t == "e enjte" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        for (int j = 0; j <= numOfWeeks; j++)
                        {
                            scheduleAppointmentCollection.Add(new ScheduleAppointment()
                            {
                                StartTime = new DateTime(dates[3, 0], dates[3, 1], dates[3, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0).AddDays(j * 7),
                                EndTime = new DateTime(dates[3, 0], dates[3, 1], dates[3, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0).AddDays(j * 7),
                                Subject = "\n" + teacher + "\n" +
                                          subject + " (" + lectureorexercises + ")" + group + "\n" +
                                          "Venue: " + venue,
                                Color = color
                            });
                        }
                    }
                    else if (scheduleList[i].Day.t == "e premte" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        for (int j = 0; j <= numOfWeeks; j++)
                        {
                            scheduleAppointmentCollection.Add(new ScheduleAppointment()
                            {
                                StartTime = new DateTime(dates[4, 0], dates[4, 1], dates[4, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0).AddDays(j * 7),
                                EndTime = new DateTime(dates[4, 0], dates[4, 1], dates[4, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0).AddDays(j * 7),
                                Subject = "\n" + teacher + "\n" +
                                          subject + " (" + lectureorexercises + ")" + group + "\n" +
                                          "Venue: " + venue,
                                Color = color
                            });
                        }
                    }
                    else if (scheduleList[i].Day.t == "e shtunë" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        for (int j = 0; j <= numOfWeeks; j++)
                        {
                            scheduleAppointmentCollection.Add(new ScheduleAppointment()
                            {
                                StartTime = new DateTime(dates[5, 0], dates[5, 1], dates[5, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0).AddDays(j * 7),
                                EndTime = new DateTime(dates[5, 0], dates[5, 1], dates[5, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0).AddDays(j * 7),
                                Subject = "\n" + teacher + "\n" +
                                          subject + " (" + lectureorexercises + ")" + group + "\n" +
                                          "Venue: " + venue,
                                Color = color
                            });
                        }
                    }
                }
            }
          
            //Adding schedule appointment collection to DataSource of SfSchedule
            schedule.DataSource = scheduleAppointmentCollection;

            base.OnAppearing();

            sw.Stop();
            DisplayAlert("Time", sw.ElapsedMilliseconds.ToString(), "OK");
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            schedule.ScheduleView = ScheduleView.DayView;
            schedule.MoveToDate = e.Datetime;
        }
    }
}