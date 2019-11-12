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
    public partial class ResetPass : ContentPage
    {
        Interface auth;

        public ResetPass()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        async void GotoLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                LoginContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        private async void Btn_ForgotPsw_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if (EmailInput.Text == null)
                    {
                        await DisplayAlert("Alert", "Please enter the email of which account you want to reset the password to :) ", "OK");
                    }
                    else
                    {

                        string emailvalue = EmailInput.Text.ToString();
                        string[] split = emailvalue.Split('@');
                        if (split[0].Any(char.IsDigit) && (split[1] == "unite.edu.mk"))
                        {

                            string Token = await auth.ResetPassword(EmailInput.Text);
                            if (Token != "")
                            {
                                await DisplayAlert("Success", "You have received a reset password link in your email", "OK");
                                await Navigation.PushAsync(new IntroPage());
                            }
                            else
                            {
                                await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Authentication Failed", "E-mail needs to end in @unite.edu.mk, try again!", "OK");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Warning", "Check your internet connection", "OK");
                }
            }

            catch (NullReferenceException)
            {
                await DisplayAlert("Warning", "Please type your email and password!", "OK");
            }

            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Reload_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                LoginContent.IsVisible = true;
                NoInternetContent.IsVisible = false;
            }
            else
            {
                LoginContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }
    }
}