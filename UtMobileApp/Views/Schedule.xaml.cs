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
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            string url = "https://spreadsheets.google.com/feeds/list/1SFGzFIq8K7va4HzT9PZUwRNxch_yqZItn80-Kwu-u6c/3/public/values?alt=json";
            var LoadSchedule = new Functions.LoadSchedule();

            lsa.ItemsSource = await LoadSchedule.DeserializeJsonAsync(url);
            sw.Stop();

            label.Text = sw.ElapsedMilliseconds.ToString();

            base.OnAppearing(); 
        }
    }
}