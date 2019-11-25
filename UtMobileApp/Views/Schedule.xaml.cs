using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Schedule : ContentPage
    {
        readonly Extensions.DateExtensions de = new Extensions.DateExtensions();
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        Interface auth;

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

            // Get dates of every day of first week
            dates = de.DatesOfWeek1(DateTime.Now);

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await LoadSchedule();
                }
                else
                {
                    LectureContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Warning", e.Message, "OK");
            }

            // Show todays schedule
            schedule.MoveToDate = DateTime.Now;

            // Add date to every day button
            label_monday.Text = dates[0, 2].ToString("00");
            label_tuesday.Text = dates[1, 2].ToString("00");
            label_wednesday.Text = dates[2, 2].ToString("00");
            label_thursday.Text = dates[3, 2].ToString("00");
            label_friday.Text = dates[4, 2].ToString("00");
            label_saturday.Text = dates[5, 2].ToString("00");

            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    btn_monday.BackgroundColor = Color.FromHex("#99D5D0");
                    break;
                case "Tuesday":
                    btn_tuesday.BackgroundColor = Color.FromHex("#99D5D0");
                    break;
                case "Wednesday":
                    btn_wednesday.BackgroundColor = Color.FromHex("#99D5D0");
                    break;
                case "Thursday":
                    btn_thursday.BackgroundColor = Color.FromHex("#99D5D0");
                    break;
                case "Friday":
                    btn_friday.BackgroundColor = Color.FromHex("#99D5D0");
                    break;
                case "Saturday":
                    btn_saturday.BackgroundColor = Color.FromHex("#99D5D0");
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
            Syncfusion.XForms.Buttons.SfButton clickedBtn = sender as Syncfusion.XForms.Buttons.SfButton;
            int addDays = int.Parse(clickedBtn.CommandParameter.ToString());

            schedule.MoveToDate = new DateTime(dates[0, 0], dates[0, 1], dates[0, 2]).AddDays(addDays);

            btn_monday.BackgroundColor = Color.FromHex("#d9eeec");
            btn_tuesday.BackgroundColor = Color.FromHex("#d9eeec");
            btn_wednesday.BackgroundColor = Color.FromHex("#d9eeec");
            btn_thursday.BackgroundColor = Color.FromHex("#d9eeec");
            btn_friday.BackgroundColor = Color.FromHex("#d9eeec");
            btn_saturday.BackgroundColor = Color.FromHex("#d9eeec");

            clickedBtn.BackgroundColor = Color.FromHex("#99D5D0");
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await LoadSchedule();

                    LectureContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    LectureContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async Task LoadSchedule()
        {
            await busyindicator.FadeTo(1, 300, Easing.Linear);
            busyindicator.IsBusy = true;

            // Get current user correct program spreadsheet
            auth = DependencyService.Get<Interface>();
            var currentUser = await firebaseHelper.GetPerson(auth.GetCurrentUserEmail());
            var spreadsheetUrls = await firebaseHelper.GetSchedule(currentUser.Program);

            var LoadSchedule = new Extensions.LoadSchedule();
            List<Models.ScheduleJSON.Entry> scheduleList = await LoadSchedule.DeserializeJsonAsync(spreadsheetUrls.Lectures);

            // Creating an instance for schedule appointment collection
            ScheduleAppointmentCollection scheduleAppointmentCollection = new ScheduleAppointmentCollection();

            for (int i = 0; i < scheduleList.Count; i++)
            {
                if (scheduleList[i].L_Day != null && scheduleList[i].L_BeginningTime != null && scheduleList[i].L_EndingTime != null)
                {
                    if (scheduleList[i].L_Day.t == "e hënë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 0);
                    }
                    else if (scheduleList[i].L_Day.t == "e martë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 1);
                    }
                    else if (scheduleList[i].L_Day.t == "e mërkurë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 2);
                    }
                    else if (scheduleList[i].L_Day.t == "e enjte")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 3);
                    }
                    else if (scheduleList[i].L_Day.t == "e premte")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 4);
                    }
                    else if (scheduleList[i].L_Day.t == "e shtunë")
                    {
                        de.AddAppointment(scheduleList, i, scheduleAppointmentCollection, dates, 5);
                    }
                }
            }

            // Adding schedule appointment collection to DataSource of SfSchedule
            schedule.DataSource = scheduleAppointmentCollection;

            // Creating an instance for collection of selected resources.
            ObservableCollection<object> selectedResources = new ObservableCollection<object>();

            var resources = schedule.ScheduleResources;

            // Adding selected resource in resource collection from the resources.
            selectedResources.Add(resources.FirstOrDefault(resource => (resource as ScheduleResource).Id.ToString() == "I"));
            selectedResources.Add(resources.FirstOrDefault(resource => (resource as ScheduleResource).Id.ToString() == "II"));
            selectedResources.Add(resources.FirstOrDefault(resource => (resource as ScheduleResource).Id.ToString() == "III"));

            // Adding selected resource collection to the selected resources of SfSchedule.
            schedule.SelectedResources = selectedResources;

            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsBusy = false;
        }
    }
}