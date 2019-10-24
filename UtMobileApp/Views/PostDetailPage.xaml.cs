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
        public string content;

        public PostDetailPage(WordPressPCL.Models.Post SelectedPost)
        {
            InitializeComponent();

            content = SelectedPost.Content.Rendered;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var html = new HtmlWebViewSource
            {
                Html = "<!DOCTYPE html><html><head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/></head><link href=\"https://unite.edu.mk/wp-content/themes/Avada/assets/css/style.min.css?ver=5.5.1\" type=\"text/css\" rel=\"stylesheet\"/><body>" + content + "</body></html>"
            };
            webView.Source = html;
            webView.Navigating += async (s, e) =>
            {
                if (e.Url.StartsWith("http://www.unite.edu.mk") || e.Url.StartsWith("https://www.unite.edu.mk") || e.Url.StartsWith("http://unite.edu.mk") || e.Url.StartsWith("https://unite.edu.mk"))
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
    }
}