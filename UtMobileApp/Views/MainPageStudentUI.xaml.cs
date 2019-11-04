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
    public partial class MainPageStudentUI : ContentPage
    {
        public MainPageStudentUI()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            label_date.Text = "\n\n" + DateTime.Now.ToString("dddd, dd MMMM");

            await Task.Delay(1000); // Wait 1sec so the animation can be seen
            await BtnLectures.TranslateTo(0, 0, 700, Easing.SpringOut);
            await BtnLectures.ScaleTo(1, 150, Easing.Linear);
            await BtnMidterms.TranslateTo(0, 0, 700, Easing.SpringOut);
            await BtnMidterms.ScaleTo(1, 150, Easing.Linear);
            await BtnExams.TranslateTo(0, 0, 700, Easing.SpringOut);
            await BtnExams.ScaleTo(1, 150, Easing.Linear);
            await BtnConsultations.TranslateTo(0, 0, 700, Easing.SpringOut);
            await BtnConsultations.ScaleTo(0.88, 150, Easing.Linear);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (e.ScrollX <= 120.0)
            {
                BtnLectures.ScaleTo(1 - e.ScrollX / 1000, 150, Easing.Linear);
            }

            else if (e.ScrollX >= 120.1 && e.ScrollX <= 240.0)
            {
                BtnMidterms.ScaleTo(1.12 - e.ScrollX / 1000, 150, Easing.Linear);
                
            }

            else if (e.ScrollX >= 240.1 && e.ScrollX <= 360.0)
            {
                BtnExams.ScaleTo(1.24 - e.ScrollX / 1000, 150, Easing.Linear);
                
            }

            else if (e.ScrollX >= 360.1 && e.ScrollX <= 479.0)
            {
                BtnConsultations.ScaleTo(0.52 + e.ScrollX / 1000, 150, Easing.Linear);
            }
        }

        private void NavToggle_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private void BtnNews_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnCalls_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnKujdesi_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnFacebook_Clicked(object sender, EventArgs e)
        {

        }
    }
}