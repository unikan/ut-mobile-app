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

        Interface auth;

        public MainPageStudent()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();

        }

        protected override async void OnAppearing()
        {
            // Get latest post, Announcements is the category with id 59
            // List of categories id https://unite.edu.mk/wp-json/wp/v2/categories?per_page=20

            Extensions.WordpressServices wordpressServices = new Extensions.WordpressServices();
            announcementList.ItemsSource = await wordpressServices.GetLatestPostsAsync(59);

            shimmer.IsActive = false;
            shimmer.IsVisible = false;
            announcementList.IsVisible = true;

            base.OnAppearing();
        }

        private async void BtnSchedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Schedule(), false);
        }

        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private async void announcementList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = e.SelectedItem as WordPressPCL.Models.Post;

            await Navigation.PushAsync(new PostDetailPage(selectedPost));
        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            auth.SignOut();
            await Navigation.PushAsync(new Login());
        }
    }
}