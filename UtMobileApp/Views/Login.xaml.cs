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
    public partial class Login : ContentPage
    {
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        readonly Interface auth;
        readonly Extensions.Helper helper = new Extensions.Helper();

        public Login()
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
                img_bg.Source = "backgroundimg4.png";
            }
            catch { }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                LoginContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        private void EmailInput_Completed(object sender, EventArgs e)
        {
            PasswordInput.Focus();
        }

        private async void Btn_Login_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;
            helper.DisableButton(btn);

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string emailvalue = EmailInput.Text.ToString();
                    string[] split = emailvalue.Split('@');
                    if (split[0].Any(char.IsDigit) && (split[1] == "unite.edu.mk"))
                    {

                        var LoginInfo = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                        if (LoginInfo.Item2)
                        {
                            await DisplayAlert("Warning", LoginInfo.Item1, "OK");
                        }
                        else
                        {

                            if (auth.GetCurrentUserStatus())
                            {
                                if (await firebaseHelper.UserExists(emailvalue))
                                {
                                    await Navigation.PushAsync(new Views.MainPageStudent());
                                }
                                else
                                {
                                    await Navigation.PushAsync(new Views.NewUserData());
                                }
                            }
                            else
                            {
                                await Navigation.PushAsync(new Views.Unverified());
                            }
                        }
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

            await helper.EnableButtonAfter2Sec(btn);
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
                LoginContent.IsVisible = true;
                NoInternetContent.IsVisible = false;
            }
            else
            {
                LoginContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        private async void Btn_ForgotPsw_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;

            helper.DisableButton(btn);
            await Navigation.PushAsync(new Views.ResetPass());
            await helper.EnableButtonAfter2Sec(btn);
        }
    }
}