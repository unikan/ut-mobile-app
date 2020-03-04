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
    public partial class Offices : ContentPage
    {
        readonly Extensions.Helper helper = new Extensions.Helper();

        public Offices()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Btn0Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("adem.beadini@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn1Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("ibrahim.neziri@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn2Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("hatibe.deari@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn3Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("imberjah.tairi@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn4Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("florian.nesimi@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn5Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("press@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn6Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("projects@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn7Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("zshi@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn8Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("evaluation@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn9Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("kushtrim.ahmeti@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }

        private async void Btn10Email_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.SendEmail("kushtrim.ahmeti@unite.edu.mk");
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Warning", "Email is not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "The email can't be ", "OK");
            }
        }


    }
}