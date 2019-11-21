using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            await LoadSchedule();
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
            calendar.DataSource = await de.AddAppointemntMidterms(scheduleList);

            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsBusy = false;
        }


        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}