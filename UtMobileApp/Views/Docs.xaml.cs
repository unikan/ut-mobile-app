using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFirebase.Helper;
using XamarinFirebase.Model;
using UtMobileApp.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Docs : ContentPage
    {
        FirebaseHelper firebasehelper = new FirebaseHelper();
        private bool _firstAppeareance = true;

        public Docs()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (_firstAppeareance)
            {
                _firstAppeareance = false;

                try
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        DocsList.ItemsSource = await firebasehelper.GetAllDocs();

                        DocsContent.IsVisible = true;
                        NoInternetContent.IsVisible = false;
                    }
                    else
                    {
                        DocsContent.IsVisible = false;
                        NoInternetContent.IsVisible = true;
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("Warning", e.Message, "OK");
                }
            }
        }

        private async void DocsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var currentItem = e.ItemData as Dokumente;

            try
            {
                var uri = new Uri(currentItem.FileUrl);
                await Launcher.OpenAsync(uri);
            }
            catch (Exception)
            {
            }

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
                    DocsList.ItemsSource = await firebasehelper.GetAllDocs();

                    DocsContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    DocsContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }
    }
}