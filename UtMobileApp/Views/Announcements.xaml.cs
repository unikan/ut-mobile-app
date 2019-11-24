using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Announcements : ContentPage
    {
        public Announcements()
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
                    await LoadAnnouncements();
                }
                else
                {
                    AnnouncementsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Warning", e.Message, "OK");
            }
        }

        private async void announcementsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedPost = e.ItemData as WordPressPCL.Models.Post;

            await Navigation.PushAsync(new PostDetailPage(selectedPost, "Announcements"));
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
                    await LoadAnnouncements();

                    AnnouncementsContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    AnnouncementsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async Task LoadAnnouncements()
        {
            busyindicator.IsBusy = true;

            AnnouncementsContent.IsVisible = true;
            NoInternetContent.IsVisible = false;

            Extensions.WordpressServices wordpressServices = new Extensions.WordpressServices();
            announcementsList.ItemsSource = await wordpressServices.GetLatestPostsAsync(59, 10);

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            await announcementsList.FadeTo(1, 300, Easing.Linear);
        }
    }
}