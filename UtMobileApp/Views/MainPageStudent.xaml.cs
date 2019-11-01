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
    public partial class MainPageStudent : ContentPage
    {
        public MainPageStudent()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Get latest post, Announcements is the category with id 59
            // List of categories id https://unite.edu.mk/wp-json/wp/v2/categories?per_page=20
            Extensions.WordpressServices wordpressServices = new Extensions.WordpressServices();
            announcementList.ItemsSource = await wordpressServices.GetLatestPostsAsync(59);
            
            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            await announcementList.FadeTo(1, 300, Easing.Linear);
        }

        private async void BtnSchedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Schedule(), false);
        }

        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private async void announcementList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedPost = e.ItemData as WordPressPCL.Models.Post;

            await Navigation.PushAsync(new PostDetailPage(selectedPost, "Announcement"));
        }

        private async void BtnNews_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.News());
        }

        private async void BtnCalls_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Calls());
        }

        private async void BtnKujdesi_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.KujdesiPerTy());
        }
    }
}