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
    public partial class ResetPass : ContentPage
    {
        Interface auth;

        public ResetPass()
        {
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        async void GotoLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }


        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

        async private void ShowErrorUnite()
        {
            await DisplayAlert("Authentication Failed", "E-mail needs to end in @unite.edu.mk, try again!", "OK");
        }

        async private void ResetPassword_Clicked(object sender, EventArgs e)
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
                        await Navigation.PushAsync(new UtMobileApp.Views.ResetPass());
                    }
                    else
                    {
                        ShowError();
                    }
                }
                else ShowErrorUnite();

                
            }
        }
    }
}