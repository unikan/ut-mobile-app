using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Unverified : ContentPage
    {

        Interface auth;
        public Unverified()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();

        }

       async private void GotoLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private void VerifyAgain_Clicked(object sender, EventArgs e)
        {


            try
            {
                //string Token = await auth.SignupWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                //if (Token != "")
                //{
                //    await Navigation.PushAsync(new Logged());
                //}
                //else
                //{ k.huseini3615111010@unite.edu.mk
                //    ShowError();
                //}
                auth.VerifyEmail();

            }
            catch (Exception)
            {
                ShowError();
            }
        }

        async private void ShowError()
        {
            await DisplayAlert("Registration Failed", "E-mail already exists", "OK");
        }

    
    }
}