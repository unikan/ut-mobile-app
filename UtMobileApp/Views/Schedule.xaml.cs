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

        int[,] dates = new int[6, 3];

        protected override async void OnAppearing()
        {
            // Disable schedule swiping
            schedule.EnableNavigation = false;

            string url = "https://spreadsheets.google.com/feeds/list/1SFGzFIq8K7va4HzT9PZUwRNxch_yqZItn80-Kwu-u6c/3/public/values?alt=json";
            var LoadSchedule = new Extensions.LoadSchedule();
            List<Models.ScheduleJSON.Entry> scheduleList = await LoadSchedule.DeserializeJsonAsync(url);

            // Get dates of every day of first week
            Extensions.DateExtensions de = new Extensions.DateExtensions();
            dates = de.DatesOfWeek1(DateTime.Now);

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
                        if (scheduleList[i].LectureOrExercise.t == "L") color = Color.FromHex("#FFB5AC");
                        else color = Color.FromHex("#A2D5F2");
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
                        scheduleAppointmentCollection.Add(new ScheduleAppointment()
                        {
                            StartTime = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                            EndTime = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                            Subject = "\n" + teacher + "\n" +
                                        subject + " (" + lectureorexercises + ")" + group + "\n" +
                                        "Venue: " + venue,
                            Color = color
                        });
                    }
                    else if (scheduleList[i].Day.t == "e martë" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection
                        scheduleAppointmentCollection.Add(new ScheduleAppointment()
                        {
                            StartTime = new DateTime(dates[1, 0], dates[1, 1], dates[1, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                            EndTime = new DateTime(dates[1, 0], dates[1, 1], dates[1, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                            Subject = "\n" + teacher + "\n" +
                                        subject + " (" + lectureorexercises + ")" + group + "\n" +
                                        "Venue: " + venue,
                            Color = color
                        });
                    }
                    else if (scheduleList[i].Day.t == "e mërkurë" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        scheduleAppointmentCollection.Add(new ScheduleAppointment()
                        {
                            StartTime = new DateTime(dates[2, 0], dates[2, 1], dates[2, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                            EndTime = new DateTime(dates[2, 0], dates[2, 1], dates[2, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                            Subject = "\n" + teacher + "\n" +
                                        subject + " (" + lectureorexercises + ")" + group + "\n" +
                                        "Venue: " + venue,
                            Color = color
                        });
                    }
                    else if (scheduleList[i].Day.t == "e enjte" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        scheduleAppointmentCollection.Add(new ScheduleAppointment()
                        {
                            StartTime = new DateTime(dates[3, 0], dates[3, 1], dates[3, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                            EndTime = new DateTime(dates[3, 0], dates[3, 1], dates[3, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                            Subject = "\n" + teacher + "\n" +
                                        subject + " (" + lectureorexercises + ")" + group + "\n" +
                                        "Venue: " + venue,
                            Color = color
                        });
                    }
                    else if (scheduleList[i].Day.t == "e premte" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        scheduleAppointmentCollection.Add(new ScheduleAppointment()
                        {
                            StartTime = new DateTime(dates[4, 0], dates[4, 1], dates[4, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                            EndTime = new DateTime(dates[4, 0], dates[4, 1], dates[4, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                            Subject = "\n" + teacher + "\n" +
                                        subject + " (" + lectureorexercises + ")" + group + "\n" +
                                        "Venue: " + venue,
                            Color = color
                        });
                    }
                    else if (scheduleList[i].Day.t == "e shtunë" && scheduleList[i].Semester.t == "V") // Semester from FireBase
                    {
                        startTime = scheduleList[i].BeginningTime.t.Split(':');
                        endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 

                        scheduleAppointmentCollection.Add(new ScheduleAppointment()
                        {
                            StartTime = new DateTime(dates[5, 0], dates[5, 1], dates[5, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                            EndTime = new DateTime(dates[5, 0], dates[5, 1], dates[5, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                            Subject = "\n" + teacher + "\n" +
                                        subject + " (" + lectureorexercises + ")" + group + "\n" +
                                        "Venue: " + venue,
                            Color = color
                        });
                    }
                }
            }

            // Adding schedule appointment collection to DataSource of SfSchedule
            schedule.DataSource = scheduleAppointmentCollection;

            // Show todays schedule
            schedule.MoveToDate = DateTime.Now;

            // Add date to every day button
            btn_monday.Text = "M\n" + dates[0, 2];
            btn_tuesday.Text = "T\n" + dates[1, 2];
            btn_wednesday.Text = "W\n" + dates[2, 2];
            btn_thursday.Text = "T\n" + dates[3, 2];
            btn_friday.Text = "F\n" + dates[4, 2];
            btn_saturday.Text = "S\n" + dates[5, 2];

            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    btn_monday.TextColor = Color.Blue;
                    break;
                case "Tuesday":
                    btn_tuesday.TextColor = Color.Blue;
                    break;
                case "Wednesday":
                    btn_wednesday.TextColor = Color.Blue;
                    break;
                case "Thursday":
                    btn_thursday.TextColor = Color.Blue;
                    break;
                case "Friday":
                    btn_friday.TextColor = Color.Blue;
                    break;
                case "Saturday":
                    btn_saturday.TextColor = Color.Blue;
                    break;
                default:
                    break;
            }

            await activityIndicator.FadeTo(0, 300, Easing.Linear);
            activityIndicator.IsRunning = false;

            await btn_monday.FadeTo(1, 100, Easing.Linear);
            await btn_tuesday.FadeTo(1, 100, Easing.Linear);
            await btn_wednesday.FadeTo(1, 100, Easing.Linear);
            await btn_thursday.FadeTo(1, 100, Easing.Linear);
            await btn_friday.FadeTo(1, 100, Easing.Linear);
            await btn_saturday.FadeTo(1, 100, Easing.Linear);
            await schedule.FadeTo(1, 100, Easing.Linear);

            base.OnAppearing();
        }

        private void Button_ChangeDay_Clicked(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            int addDays = int.Parse(clickedBtn.CommandParameter.ToString());

            schedule.MoveToDate = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2]).AddDays(addDays);

            btn_monday.TextColor = Color.Gray;
            btn_tuesday.TextColor = Color.Gray;
            btn_wednesday.TextColor = Color.Gray;
            btn_thursday.TextColor = Color.Gray;
            btn_friday.TextColor = Color.Gray;
            btn_saturday.TextColor = Color.Gray;

            clickedBtn.TextColor = Color.Blue;
        }
    }
}