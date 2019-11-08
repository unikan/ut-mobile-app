using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtMobileApp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Resources;

namespace UtMobileApp
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logged : ContentPage
    {

        Interface auth;
        public Logged()
        {
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

       private async void GotoLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private void GetUser_Clicked(object sender, EventArgs e)
        {
            string currentUserEmail = auth.GetCurrentUserEmail();

            userlabel.Text = currentUserEmail;

        }
    }
}