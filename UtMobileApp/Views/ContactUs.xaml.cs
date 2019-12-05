using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUs : ContentPage
    {
        public ContactUs()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void BtnMap_Clicked(object sender, EventArgs e)
        {
            string url;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    url = String.Format("http://maps.apple.com/maps?q={0}", "Str. Ilinden, nn. 1200 Tetova Republic of dd");
                    break;
                default:
                    url = String.Format("http://maps.google.com/maps?q={0}", "Str. Ilinden, nn. 1200 Tetova Republic of Macedonia");
                    break;
            }

            try
            {
                var uri = new Uri(url);
                Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private void BtnEmail_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:international@unite.edu.mk"));
        }

        private void BtnCall_Clicked(object sender, EventArgs e)
        {
            try
            {
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall("0038944356500");
            }
            catch (Exception) { }
        }

        private async void BtnFacebook_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("fb://page/338685373241583");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception)
            {
                var uri = new Uri("https://www.facebook.com/Universiteti-i-Tetov%C3%ABs-UT-338685373241583/");
                await Launcher.OpenAsync(uri);
            }
        }

        private async void BtnWebsite_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("https://www.unite.edu.mk/");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }
    }
}