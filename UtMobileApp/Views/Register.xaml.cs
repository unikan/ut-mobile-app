using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;
using Xamarin.Essentials;

namespace UtMobileApp
{

    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        readonly Interface auth;
        readonly Extensions.Helper helper = new Extensions.Helper();

        public Register()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                RegisterContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        private async void Btn_SignUp_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string emailvalue = EmailInput.Text.ToString();
                    string[] split = emailvalue.Split('@');
                    if (split[0].Any(char.IsDigit) && (split[1] == "unite.edu.mk"))
                    {
                        // Enable busy indicator 
                        busyindicator.IsBusy = true;

                        if (PasswordInput.Text.Length >= 6)
                        {
                            await auth.SignupWithEmailPassword(EmailInput.Text, PasswordInput.Text);

                            if (await firebaseHelper.UserExists(emailvalue))
                            {
                                await DisplayAlert("Warning", "This email address already exists!", "OK");
                            }
                            else
                            {
                                Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;

                                helper.DisableButton(btn);
                                await DisplayAlert("Success", "You have been registered successfully, you have received a verification email.", "OK");
                                await Navigation.PopToRootAsync();
                                await helper.EnableButtonAfter2Sec(btn);
                            }
                        }
                        else
                        {
                            await DisplayAlert("Warning", "The password should at least contain 6 characters", "OK");
                        }

                        // Disable busy indicator
                        busyindicator.IsBusy = false;
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Please use your official student email (@unite.edu.mk)", "OK");
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

        private void EmailInput_Completed(object sender, EventArgs e)
        {
            PasswordInput.Focus();
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;

            helper.DisableButton(btn);
            await Navigation.PopAsync();
            await helper.EnableButtonAfter2Sec(btn);
        }

        private void Reload_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                RegisterContent.IsVisible = true;
                NoInternetContent.IsVisible = false;
            }
            else
            {
                RegisterContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }
    }
}