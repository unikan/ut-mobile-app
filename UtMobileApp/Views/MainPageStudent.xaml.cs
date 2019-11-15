using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtMobileApp.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageStudent : ContentPage
    {
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        readonly Interface auth;

        public MainPageStudent()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
        }

        protected override async void OnAppearing()
        {
            // Check if he's good to go
            if (await firebaseHelper.GetPerson(auth.GetCurrentUserEmail()) == null)
            {
                var previousPage = Navigation.NavigationStack.LastOrDefault();
                await Navigation.PushAsync(new NewUserData());
                Navigation.RemovePage(previousPage);
            }

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

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<ICloseApp>().CloseApplication();
            return true;
        }

        private void NavToggle_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private async void BtnLectures_Clicked(object sender, EventArgs e)
        {
            await BtnLectures.ScaleTo(1.1, 200, Easing.BounceOut);
            await Navigation.PushAsync(new Views.Schedule());
            BtnLectures.Scale = 1;
        }

        //private async void announcementList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        //{
        //    var selectedPost = e.ItemData as WordPressPCL.Models.Post;

        //    await Navigation.PushAsync(new PostDetailPage(selectedPost, "Announcement"));
        //}

        private async void BtnNews_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.News());
        }

        private async void BtnCalls_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Calls());
        }

        private async void BtnKujdesi_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.KujdesiPerTy());
        }

        private async void BtnAnnouncements_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Announcements());
        }

        private async void BtnFacebook_Clicked(object sender, EventArgs e)
        {
            try
            {
                var uri = new Uri("fb://page/338685373241583");
                await Launcher.OpenAsync(uri);
            }
            catch (Exception)
            {
            }
        }

        private async void SignOut_Clicked(object sender, EventArgs e)
        {
            auth.SignOut();

            var previousPage = Navigation.NavigationStack.LastOrDefault();
            await Navigation.PushAsync(new IntroPage());
            Navigation.RemovePage(previousPage);
        }
    }
}