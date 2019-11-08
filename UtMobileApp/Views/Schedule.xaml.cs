using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Schedule : ContentPage
    {
        public Schedule()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        int[,] dates = new int[6, 3];

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Write current month and year in label
            label_monthYear.Text = DateTime.Now.Year + "\n" + DateTime.Now.ToString("MMMM");

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

            for (int i = 0; i < scheduleList.Count; i++)
            {
                if (scheduleList[i].Day != null && scheduleList[i].BeginningTime != null && scheduleList[i].EndingTime != null)
                {
                    if (scheduleList[i].Day.t == "e hënë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 0);
                    }
                    else if (scheduleList[i].Day.t == "e martë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 1);
                    }
                    else if (scheduleList[i].Day.t == "e mërkurë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 2);
                    }
                    else if (scheduleList[i].Day.t == "e enjte")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 3);
                    }
                    else if (scheduleList[i].Day.t == "e premte")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 4);
                    }
                    else if (scheduleList[i].Day.t == "e shtunë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 5);
                    }
                }
            }

            // Adding schedule appointment collection to DataSource of SfSchedule
            schedule.DataSource = scheduleAppointmentCollection;

            // Show todays schedule
            schedule.MoveToDate = DateTime.Now;

            // Add date to every day button
            btn_monday.Text = "MON\n" + dates[0, 2].ToString("00");
            btn_tuesday.Text = "TUE\n" + dates[1, 2].ToString("00");
            btn_wednesday.Text = "WED\n" + dates[2, 2].ToString("00");
            btn_thursday.Text = "THU\n" + dates[3, 2].ToString("00");
            btn_friday.Text = "FRI\n" + dates[4, 2].ToString("00");
            btn_saturday.Text = "SAT\n" + dates[5, 2].ToString("00");

            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    btn_monday.TextColor = Color.FromHex("#101010");
                    btn_monday.FontAttributes = FontAttributes.Bold;
                    break;
                case "Tuesday":
                    btn_tuesday.TextColor = Color.FromHex("#101010");
                    btn_tuesday.FontAttributes = FontAttributes.Bold;
                    break;
                case "Wednesday":
                    btn_wednesday.TextColor = Color.FromHex("#101010");
                    btn_wednesday.FontAttributes = FontAttributes.Bold;
                    break;
                case "Thursday":
                    btn_thursday.TextColor = Color.FromHex("#101010");
                    btn_thursday.FontAttributes = FontAttributes.Bold;
                    break;
                case "Friday":
                    btn_friday.TextColor = Color.FromHex("#101010");
                    btn_friday.FontAttributes = FontAttributes.Bold;
                    break;
                case "Saturday":
                    btn_saturday.TextColor = Color.FromHex("#101010");
                    btn_saturday.FontAttributes = FontAttributes.Bold;
                    break;
                default:
                    break;
            }

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsBusy = false;

            // Show buttons
            await btn_monday.ScaleTo(1, 100, Easing.Linear);
            await btn_tuesday.ScaleTo(1, 100, Easing.Linear);
            await btn_wednesday.ScaleTo(1, 100, Easing.Linear);
            await btn_thursday.ScaleTo(1, 100, Easing.Linear);
            await btn_friday.ScaleTo(1, 100, Easing.Linear);
            await btn_saturday.ScaleTo(1, 100, Easing.Linear);
            await schedule.FadeTo(1, 150, Easing.Linear);
        }

        private void Button_ChangeDay_Clicked(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            int addDays = int.Parse(clickedBtn.CommandParameter.ToString());

            schedule.MoveToDate = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2]).AddDays(addDays);

            btn_monday.TextColor = Color.FromHex("#8E97A6");
            btn_monday.FontAttributes = FontAttributes.None;
            btn_tuesday.TextColor = Color.FromHex("#8E97A6");
            btn_tuesday.FontAttributes = FontAttributes.None;
            btn_wednesday.TextColor = Color.FromHex("#8E97A6");
            btn_wednesday.FontAttributes = FontAttributes.None;
            btn_thursday.TextColor = Color.FromHex("#8E97A6");
            btn_thursday.FontAttributes = FontAttributes.None;
            btn_friday.TextColor = Color.FromHex("#8E97A6");
            btn_friday.FontAttributes = FontAttributes.None;
            btn_saturday.TextColor = Color.FromHex("#8E97A6");
            btn_saturday.FontAttributes = FontAttributes.None;

            clickedBtn.TextColor = Color.FromHex("#101010");
            clickedBtn.FontAttributes = FontAttributes.Bold;
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}