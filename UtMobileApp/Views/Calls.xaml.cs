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
    public partial class Calls : ContentPage
    {
        public Calls()
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
                    await LoadCalls();
                }
                else
                {
                    CallsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Warning", e.Message, "OK");
            }
        }

        private async void callsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedPost = e.ItemData as WordPressPCL.Models.Post;

            await Navigation.PushAsync(new PostDetailPage(selectedPost, "Calls"));
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
                    await LoadCalls();

                    CallsContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    CallsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async Task LoadCalls()
        {
            busyindicator.IsBusy = true;

            CallsContent.IsVisible = true;
            NoInternetContent.IsVisible = false;

            Extensions.WordpressServices wordpressServices = new Extensions.WordpressServices();
            callsList.ItemsSource = await wordpressServices.GetLatestPostsAsync(58);

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            await callsList.FadeTo(1, 300, Easing.Linear);
        }
    }
}