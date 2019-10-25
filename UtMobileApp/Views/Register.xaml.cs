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

            try { string Token = await auth.SignupWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                if (Token != "")
                {
                    await Navigation.PushAsync(new Logged());
                }
                else
                {
                    ShowError();
                }

            }
            catch(Exception ex)
            {
                await DisplayAlert("Warning",ex.Message, "OK");
            }
           } 
        

        async private void ShowError()
        {
            await DisplayAlert("Registration Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

    }
}