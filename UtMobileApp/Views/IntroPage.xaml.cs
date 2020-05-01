using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtMobileApp.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroPage : ContentPage
    {
        readonly Extensions.Helper helper = new Extensions.Helper();
        readonly Interface auth;

        private bool _firstAppereance = true;

        public IntroPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_firstAppereance)
            {
                _firstAppereance = false;
                Navigation.PushModalAsync(new StayHomeModal());
            }
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<ICloseApp>().CloseApplication();
            return true;
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

        private async void Btn_Guest_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;

            helper.DisableButton(btn);
            await Navigation.PushAsync(new MainPageGuest());
            await helper.EnableButtonAfter2Sec(btn);
        }
    }
}