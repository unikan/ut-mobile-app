using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Midterms : ContentPage
    {
        readonly Extensions.DateExtensions de = new Extensions.DateExtensions();

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
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await LoadSchedule();
                }
                else
                {
                    MidtermsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
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
            await busyindicator.FadeTo(1, 300, Easing.Linear);
            busyindicator.IsBusy = true;

            string url = "https://spreadsheets.google.com/feeds/list/1SFGzFIq8K7va4HzT9PZUwRNxch_yqZItn80-Kwu-u6c/3/public/values?alt=json";
            var LoadSchedule = new Extensions.LoadSchedule();
            List<Models.MidtermsJSON.Entry> scheduleList = await LoadSchedule.DeserializeMidtermsJsonAsync(url);

            // Adding calendar event collection to DataSource of Calendar
            calendar.DataSource = de.AddAppointemntMidterms(scheduleList);

            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsBusy = false;
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
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await LoadSchedule();

                    MidtermsContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    MidtermsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
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