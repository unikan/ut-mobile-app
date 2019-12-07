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
    public partial class Unverified : ContentPage
    {
        readonly Interface auth;
        readonly Extensions.Helper helper = new Extensions.Helper();

        public Unverified()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                img_bg.Source = "backgroundimg7.png";
            }
            catch
            {
                contentPage.BackgroundColor = Color.FromHex("#5750f5");
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                UnverifiedContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void Btn_ResendVerification_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;
            helper.DisableButton(btn);

            try
            {
                await auth.VerifyEmail();
                await DisplayAlert("Success", "You have received a verification email.", "OK");
                await Navigation.PopToRootAsync();

            }
            catch (Exception)
            {
                await DisplayAlert("Warning", "Cannot send email, please try again later", "OK");
            }

            await helper.EnableButtonAfter2Sec(btn);
        }

        private void Reload_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UnverifiedContent.IsVisible = true;
                NoInternetContent.IsVisible = false;
            }
            else
            {
                UnverifiedContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }
    }
}