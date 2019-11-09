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
    public partial class Register : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        Interface auth;

        public Register()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        async void SignUpClicked(object sender, EventArgs e)
        {

            string emailvalue = EmailInput.Text.ToString();
            string[] split = emailvalue.Split('@');
            if (split[0].Any(char.IsDigit) && (split[1] == "unite.edu.mk"))
            {
                    try
                {
                    //var Checkemail = await auth.CheckifEmailExists(EmailInput.Text);
                    //if (Checkemail.Item2)
                    //{
                    //    await DisplayAlert("Warning", Checkemail.Item1, "OK");
                    //}
                    //else
                    //{

                        //string Token = await auth.SignupWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                        //if (Token != "")
                        //{
                        //    await Navigation.PushAsync(new Logged());
                        //}
                        //else
                        //{
                        //    ShowError();
                        //}
                        await auth.SignupWithEmailPassword(EmailInput.Text, PasswordInput.Text);


                        if (await firebaseHelper.UserExists(emailvalue))
                        {
                            await DisplayAlert("Warning", " This email address already exists!", "OK");
                        }
                        else
                        {
                            await Navigation.PushAsync(new Views.Unverified());
                        }
                    //}
                }
                catch (Exception)
                {
                    ShowError();
                }
            }
            else ShowErrorUnite();

           } 
        

        async private void ShowError()
        {
            await DisplayAlert("Registration Failed", "E-mail already exists", "OK");
        }

        async private void ShowErrorUnite()
        {
            await DisplayAlert("Authentication Failed", "E-mail needs to end in @unite.edu.mk and needs to be a students email address , please try again!", "OK");
        }

    }
}