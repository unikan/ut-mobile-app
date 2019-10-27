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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            label_type.Text = currentCategory;
            label_title.Text = currentPost.Title.Rendered;
            label_date.Text = "Posted: " + currentPost.Date.ToString("dddd, dd MMMM yyyy HH:mm");
            
            var html = new HtmlWebViewSource
            {
                Html = ws.HtmlStart + currentPost.Content.Rendered + ws.HtmlEnd
            };
            webView.Source = html;
            webView.Navigating += async (s, e) =>
            {
                if (e.Url.StartsWith("http://") || e.Url.StartsWith("https://"))
                {
                    try
                    {
                        var uri = new Uri(e.Url);
                        await Launcher.OpenAsync(uri);
                    }
                    catch (Exception)
                    {
                    }

                    e.Cancel = true;
                }
            };
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}