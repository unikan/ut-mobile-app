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
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        async void Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        async void Reset(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPass());
        }

        async void SignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }
}