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
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            string url = "https://spreadsheets.google.com/feeds/list/1SFGzFIq8K7va4HzT9PZUwRNxch_yqZItn80-Kwu-u6c/3/public/values?alt=json";
            var LoadSchedule = new Extensions.LoadSchedule();
            List<Models.ScheduleJSON.Entry> scheduleList = await LoadSchedule.DeserializeJsonAsync(url);
            IEnumerable<Models.ScheduleJSON.Entry> semesterList = LoadSchedule.GetDay("e hënë", "I", scheduleList);
            
            //IEnumerable<Models.ScheduleJSON.Entry> semesterList = LoadSchedule.GetBySemester("I", scheduleList);
            Extensions.DateExtensions de = new Extensions.DateExtensions();
            
            int[,] dates = new int[6, 3];
            dates = de.DatesOfWeek1();


            // Creating an instance for schedule appointment collection
            ScheduleAppointmentCollection scheduleAppointmentCollection = new ScheduleAppointmentCollection();
            
            for (int i = 0; i < scheduleList.Count; i++)
            {
                if (scheduleList[i].Day != null)
                {
                    if (scheduleList[i].Day.t == "e hënë" && scheduleList[i].Semester.t == "I")
                    {
                        string[] startTime = scheduleList[i].BeginningTime.t.Split(':');
                        string[] endTime = scheduleList[i].EndingTime.t.Split(':');

                        //Adding schedule appointment in schedule appointment collection 
                        scheduleAppointmentCollection.Add(new ScheduleAppointment()
                        {
                            StartTime = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2], int.Parse(startTime[0]), int.Parse(startTime[1]), 0),
                            EndTime = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2], int.Parse(endTime[0]), int.Parse(endTime[1]), 0),
                            Subject = scheduleList[i].Subjects.t
                        });
                    }
                }
            }

            //Adding schedule appointment collection to DataSource of SfSchedule
            schedule.DataSource = scheduleAppointmentCollection;

            base.OnAppearing();
        }
    }
}