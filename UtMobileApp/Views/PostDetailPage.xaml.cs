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
        Extensions.WordpressServices ws;

        public PostDetailPage(WordPressPCL.Models.Post SelectedPost)
        {
            InitializeComponent();

            content = SelectedPost.Content.Rendered;
            ws = new Extensions.WordpressServices();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var html = new HtmlWebViewSource
            {
                Html = ws.HtmlStart + content + ws.HtmlEnd
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