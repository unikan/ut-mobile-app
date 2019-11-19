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
    public partial class KujdesiPerTy : ContentPage
    {
        public KujdesiPerTy()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await LoadKujdesiPerTy();
                }
                else
                {
                    KujdesiPerTyContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Warning", e.Message, "OK");
            }
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await LoadKujdesiPerTy();
            }
            else
            {
                KujdesiPerTyContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        private async Task LoadKujdesiPerTy()
        {
            KujdesiPerTyContent.IsVisible = true;
            NoInternetContent.IsVisible = false;

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            // Give source to webview
            webView.Source = "https://eservices.unite.edu.mk/kujdesi-per-ty/";
            await webView.FadeTo(1, 300, Easing.Linear);
        }
    }
}