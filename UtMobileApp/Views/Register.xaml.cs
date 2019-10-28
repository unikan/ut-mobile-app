using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp
{

    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {

        Interface auth;

        public Register()
        {
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
                    //string Token = await auth.SignupWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                    //if (Token != "")
                    //{
                    //    await Navigation.PushAsync(new Logged());
                    //}
                    //else
                    //{
                    //    ShowError();
                    //}
                    auth.SignupWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                    await Navigation.PushAsync(new Views.NewUserData()); // Verify email
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
            await DisplayAlert("Registration Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

        async private void ShowErrorUnite()
        {
            await DisplayAlert("Authentication Failed", "E-mail needs to end in @unite.edu.mk, try again!", "OK");
        }

    }
}