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
    public partial class Login : ContentPage
    {
        Interface auth;

        public Login()
        {
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        async void LoginClicked(object sender, EventArgs e)
        {

            string Token = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
            if (Token != "")
            {
                await Navigation.PushAsync(new Logged());
            }
            else
            {
                ShowError();
            }


        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}