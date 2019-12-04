using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Events : ContentPage
    {
        readonly Extensions.DateExtensions de = new Extensions.DateExtensions();
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        Interface auth;
        private bool _firstAppeareance = true;

        public Events()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (_firstAppeareance)
            {
                _firstAppeareance = false;

                try
                {
                    await LoadEvents("local").ContinueWith(async updatebutton =>
                    {
                        await Task.Delay(3000);
                        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                        {
                            await UpdateContent.TranslateTo(0, 0, 500, Easing.BounceIn);

                            await Task.Delay(5000);
                            await UpdateContent.TranslateTo(0, 500, 500, Easing.Linear);
                        }
                    });
                }
                catch (Exception e)
                {
                    await DisplayAlert("Warning", e.Message, "OK");
                }

                // Move to exams date
                //calendar.MoveToDate = new DateTime(2017, 5, 5);
            }
        }

        private async Task LoadEvents(string loadType = "")
        {
            await busyindicator.FadeTo(1, 300, Easing.Linear);
            busyindicator.IsBusy = true;

            var LoadSchedule = new Extensions.LoadSchedule();
            List<Models.EventsJSON.Event> eventsList = null;

            if (Application.Current.Properties.ContainsKey("EventsData") && loadType == "local")
            {
                eventsList = await LoadSchedule.DeserializeEventsJsonAsync("local");
            }
            else
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    eventsList = await LoadSchedule.DeserializeEventsJsonAsync("internet");
                }
                else
                {
                    EventsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }

            // Adding calendar event collection to DataSource of Calendar
            var datasource = de.AddAppointemntEvents(eventsList);
            events.DataSource = datasource;

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
                await LoadEvents();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async void Btn_UpdateEvents_Clicked(object sender, EventArgs e)
        {
            await LoadEvents("internet");
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}