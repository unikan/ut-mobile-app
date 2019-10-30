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

            List<Models.WPFeaturedPost> wplist = await ws.GetFeaturedPost(31);
            ImageNews.Source = wplist[0].ImageUrl;
            LabelUrl.Text = wplist[0].Title;
        }
    }
}