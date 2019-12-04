using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageGuest : ContentPage
    {
        public MainPageGuest()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void BtnNews_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.News());
        }

        private async void BtnAnnouncements_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Announcements());
        }

        private async void BtnCalls_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Calls());
        }

        private async void BtnCareerCenter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.CareerCenter());
        }

        private async void BtnEvents_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Events());
        }

        private async void BtnOffices_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Views.News());
        }

        private async void BtnContact_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ContactUs());
        }

        private async void BtnKujdesi_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.KujdesiPerTy());
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}