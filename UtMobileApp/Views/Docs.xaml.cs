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
        public Docs()
        {
            InitializeComponent();
           
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            DocsList.ItemsSource = await firebasehelper.GetAllDocs();
            
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
    }
}