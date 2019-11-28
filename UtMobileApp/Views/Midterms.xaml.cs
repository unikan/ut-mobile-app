using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Midterms : ContentPage
    {
        readonly Extensions.DateExtensions de = new Extensions.DateExtensions();
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        Interface auth;

        public Midterms()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await LoadSchedule();
            }
            catch (Exception e)
            {
                await DisplayAlert("Warning", e.Message, "OK");
            }

            // Move to midterms date
            //calendar.MoveToDate = new DateTime(2017, 5, 5);
        }

        private async Task LoadSchedule()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            await busyindicator.FadeTo(1, 300, Easing.Linear);
            busyindicator.IsBusy = true;

            var LoadSchedule = new Extensions.LoadSchedule();
            List<Models.MidtermsJSON.Entry> scheduleList = null;

            if (Application.Current.Properties.ContainsKey("MidtermsData"))
            {
                scheduleList = await LoadSchedule.DeserializeMidtermsJsonAsync("local");
            }
            else
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Get current user correct program spreadsheet
                    auth = DependencyService.Get<Interface>();
                    var currentUser = await firebaseHelper.GetPerson(auth.GetCurrentUserEmail());
                    var spreadsheetUrls = await firebaseHelper.GetUrls(currentUser.Program);

                    scheduleList = await LoadSchedule.DeserializeMidtermsJsonAsync("internet", spreadsheetUrls.Midterms);
                }
                else
                {
                    MidtermsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }

            // Adding calendar event collection to DataSource of Calendar
            calendar.DataSource = de.AddAppointemntMidterms(scheduleList);

            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsBusy = false;

            sw.Stop();
            await DisplayAlert("Time elapsed", sw.ElapsedMilliseconds.ToString(), "OK");
        }

        private void Calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        {
            var appointment = e.InlineEvent;
            DisplayAlert(appointment.StartTime.ToString("dddd, dd MMMM yyyy HH:mm"), appointment.Subject, "OK");
        }

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            try
            {
                await LoadSchedule();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }


        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}