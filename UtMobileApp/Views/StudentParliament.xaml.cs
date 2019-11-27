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
    public partial class StudentParliament : ContentPage
    {
        readonly Extensions.WordpressServices ws;

        public StudentParliament()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            ws = new Extensions.WordpressServices();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await LoadPosts();
                }
                else
                {
                    PostsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Warning", e.Message, "OK");
            }
        }

        private async void postsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedPost = e.ItemData as Models.WPFeaturedPost;

            await Navigation.PushAsync(new FeaturedPostDetailPage(selectedPost, "Posts"));
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
                    await LoadPosts();

                    PostsContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    PostsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async Task LoadPosts()
        {
            busyindicator.IsBusy = true;

            PostsContent.IsVisible = true;
            NoInternetContent.IsVisible = false;

            postsList.ItemsSource = await ws.GetFeaturedPostTag(70, 5);

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            await postsList.FadeTo(1, 300, Easing.Linear);
        }
    }
}