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

            Extensions.WordpressServices wordpressServices = new Extensions.WordpressServices();
            announcementsList.ItemsSource = await wordpressServices.GetLatestPostsAsync(59);

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            await announcementsList.FadeTo(1, 300, Easing.Linear);
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
    }
}