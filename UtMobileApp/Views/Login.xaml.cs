using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;



namespace UtMobileApp
{
    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        Interface auth;

        public Login()
        {
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        async void LoginClicked(object sender, EventArgs e)
        {

            string emailvalue = EmailInput.Text.ToString();
            string[] split = emailvalue.Split('@');
            if (split[0].Any(char.IsDigit) && (split[1] == "unite.edu.mk"))
            {

                string Token = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                if (Token != "")
                {
                    //bool trueanauk = await firebaseHelper.UserExists(emailvalue);
                    //string trueanauks = trueanauk.ToString();
                    //DisplayAlert("haa", trueanauks, "SI THAUE");
                    if (auth.GetCurrentUserStatus())
                    {
                        if (await firebaseHelper.UserExists(emailvalue)) {
                            await Navigation.PushAsync(new Views.MainPageStudent());
                        }
                        else {  
                            await Navigation.PushAsync(new Views.NewUserData());
                            }
                    }
                    else
                    {
                        await Navigation.PushAsync(new Views.Unverified());
                    }
                }
                else
                {
                    ShowError();
                }
            }
            else ShowErrorUnite();

        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

        async private void ShowErrorUnite()
        {
            await DisplayAlert("Authentication Failed", "E-mail needs to end in @unite.edu.mk, try again!", "OK");
        }


        //async void Reset_Password(object sender, EventArgs e)
        //{
        //    if (EmailInput.Text == null)
        //    {
        //        await DisplayAlert("Alert", "Please enter the email of which account you want to reset the password to :) ", "OK");
        //    }
        //    else
        //    {
        //        string Token = await auth.ResetPassword(EmailInput.Text);
        //        if (Token != "")
        //        {
        //            await Navigation.PushAsync(new UtMobileApp.Views.ResetPass());
        //        }
        //        else
        //        {
        //            ShowError();
        //        }
        //    }
        //}
    }
}