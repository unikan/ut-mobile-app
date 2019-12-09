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
    public partial class Support : ContentPage
    {
        public Support()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnFeedback_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("https://unikan.dev/unitedapp/support.html");
                await Browser.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private async void BtnPolicy_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("https://unikan.dev/unitedapp/privacy_policy.html");
                await Browser.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private async void BtnUnikan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("https://unikan.dev");
                await Browser.OpenAsync(uri);
            }
            catch (Exception) { }
        }

    }
}