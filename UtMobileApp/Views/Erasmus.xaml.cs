using Plugin.Messaging;
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
    public partial class Erasmus : ContentPage
    {
        public Erasmus()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void BtnEmail_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:shpresa.ademi@unite.edu.mk"));
        }
        private async void BtnDoc0_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("https://unite.edu.mk/wp-content/uploads/2019/05/ApplicationErasmus2019.pdf");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private async void BtnDoc1_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("http://unite.edu.mk/wp-content/uploads/2018/05/erasmus_udhezues.pdf");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private async void BtnDoc2_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("http://unite.edu.mk/wp-content/uploads/2018/05/erasmus_udhezues.pdf");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private async void Apply_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("http://erasmus.unite.edu.mk:3500/Application");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private async void Image_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("https://i2.wp.com/unite.edu.mk/wp-content/uploads/2018/05/erasmus-doc.jpg?fit=1316%2C956&ssl=1");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private async void FacultiesRelations_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("https://unite.edu.mk/erasmus/");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception) { }
        }

        private void BtnCall_Clicked(object sender, EventArgs e)
        {
            try
            {
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall("0038944356500");
            }
            catch (Exception) { }
        }


    }
}