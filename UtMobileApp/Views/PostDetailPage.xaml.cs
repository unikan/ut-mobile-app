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
    public partial class PostDetailPage : ContentPage
    {
        protected WordPressPCL.Models.Post currentPost;
        protected string currentCategory;
        Extensions.WordpressServices ws;

        public PostDetailPage(WordPressPCL.Models.Post SelectedPost, string category)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            currentPost = SelectedPost;
            currentCategory = category;
            ws = new Extensions.WordpressServices();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string title = currentPost.Title.Rendered.Replace("&#8211;", "-");
            label_title.Text = title;
            label_title1.Text = title;
            label_date.Text = "Posted: " + currentPost.Date.ToString("dddd, dd MMMM yyyy HH:mm");

            // Hide busy indicator indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;

            var html = new HtmlWebViewSource
            {
                Html = ws.HtmlStart + currentPost.Content.Rendered + ws.HtmlEnd
            };
            webView.Source = html;
            await webView.FadeTo(1, 300, Easing.Linear);
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith("http://") || e.Url.StartsWith("https://"))
            {
                try
                {
                    var uri = new Uri(e.Url);
                    await Browser.OpenAsync(uri);
                }
                catch (Exception)
                {
                }

                e.Cancel = true;
            }
        }
    }
}