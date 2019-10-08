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

            // Create 6 lists for everyday Monday to Saturday, for actual semester
            IEnumerable<Models.ScheduleJSON.Entry> MondayList = LoadSchedule.GetDay("e hënë", "I", scheduleList);
            IEnumerable<Models.ScheduleJSON.Entry> TuesdayList = LoadSchedule.GetDay("e martë", "I", scheduleList);
            IEnumerable<Models.ScheduleJSON.Entry> WednesdayList = LoadSchedule.GetDay("e mërkurë", "I", scheduleList);
            IEnumerable<Models.ScheduleJSON.Entry> ThursdayList = LoadSchedule.GetDay("e enjte", "I", scheduleList);
            IEnumerable<Models.ScheduleJSON.Entry> FridayList = LoadSchedule.GetDay("e premte", "I", scheduleList);
            IEnumerable<Models.ScheduleJSON.Entry> SaturdayList = LoadSchedule.GetDay("e shtunë", "I", scheduleList);





            // Creating an instance for schedule appointment collection
            ScheduleAppointmentCollection scheduleAppointmentCollection = new ScheduleAppointmentCollection();
            //Adding schedule appointment in schedule appointment collection 
            scheduleAppointmentCollection.Add(new ScheduleAppointment()
            {
                StartTime = new DateTime(2019, 05, 08, 10, 0, 0),
                EndTime = new DateTime(2017, 05, 08, 12, 0, 0),
                Subject = "Meeting",
                Location = "Hutchison road",
            });

            //Adding schedule appointment collection to DataSource of SfSchedule
            schedule.DataSource = scheduleAppointmentCollection;


            base.OnAppearing();
        }
    }
}