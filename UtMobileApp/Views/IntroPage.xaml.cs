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
        readonly Extensions.Helper helper = new Extensions.Helper();
        readonly Interface auth;

        public IntroPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                if (auth.GetCurrentUserStatus())
                {
                    await Navigation.PushAsync(new Views.MainPageStudent());
                }
            }
            catch { }

            base.OnAppearing();

            await Navigation.PopToRootAsync();
        }

        private async void Btn_Login_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;

            helper.DisableButton(btn);
            await Navigation.PushAsync(new Login());
            await helper.EnableButtonAfter2Sec(btn);
        }

        private async void Btn_SignUp_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;

            helper.DisableButton(btn);
            await Navigation.PushAsync(new Register());
            await helper.EnableButtonAfter2Sec(btn);
        }
    }
}