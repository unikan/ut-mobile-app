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
    public partial class News : ContentPage
    {
        Extensions.WordpressServices ws;

        public News()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            ws = new Extensions.WordpressServices();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            newsList.ItemsSource = await ws.GetFeaturedPost(31);

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            await newsList.FadeTo(1, 300, Easing.Linear);
        }

        private async void newsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedPost = e.ItemData as Models.WPFeaturedPost;

            await Navigation.PushAsync(new FeaturedPostDetailPage(selectedPost, "News"));
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}