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
        readonly Helper helper = new Helper();
        Interface auth;
        private bool _firstAppeareance = true;

        public MainPageStudent()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (_firstAppeareance)
            {
                _firstAppeareance = false;

                try
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        auth = DependencyService.Get<Interface>();

                        // Check if he's good to go
                        if (await firebaseHelper.GetPerson(auth.GetCurrentUserEmail()) == null)
                        {
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new NewUserData());
                            Navigation.RemovePage(previousPage);
                        }
                    }
                    await GetCurrentUserInfo();
                }
                catch (Exception)
                {
                    await DisplayAlert("Warning", "Check your internet connection.", "OK");
                }

                base.OnAppearing();

                label_date.Text = DateTime.Now.ToString("dddd,\ndd MMMM");

                //await Task.Delay(500); // Wait .5sec so the animation can be seen
                await BtnLectures.TranslateTo(0, 0, 700, Easing.SpringOut);
                await BtnLectures.ScaleTo(1, 150, Easing.Linear);
                await BtnMidterms.TranslateTo(0, 0, 700, Easing.SpringOut);
                await BtnMidterms.ScaleTo(1, 150, Easing.Linear);
                await BtnExams.TranslateTo(0, 0, 700, Easing.SpringOut);
                await BtnExams.ScaleTo(1, 150, Easing.Linear);
                //await BtnConsultations.TranslateTo(0, 0, 700, Easing.SpringOut);
                //await BtnConsultations.ScaleTo(0.88, 150, Easing.Linear);
            }
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

            //else if (e.ScrollX >= 360.1 && e.ScrollX <= 479.0)
            //{
            //    BtnConsultations.ScaleTo(0.52 + e.ScrollX / 1000, 150, Easing.Linear);
            //}
        }

        protected override bool OnBackButtonPressed()
        {
            if (navigationDrawer.IsOpen)
            {
                navigationDrawer.ToggleDrawer();
            }
            else
            {
                DependencyService.Get<ICloseApp>().CloseApplication();
            }
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

        private async void BtnMidterms_Clicked(object sender, EventArgs e)
        {
            await BtnMidterms.ScaleTo(1.1, 200, Easing.BounceOut);
            await Navigation.PushAsync(new Views.Midterms());
            BtnMidterms.Scale = 1;
        }

        private async void BtnExams_Clicked(object sender, EventArgs e)
        {
            await BtnExams.ScaleTo(1.1, 200, Easing.BounceOut);
            await Navigation.PushAsync(new Views.Exams());
            BtnMidterms.Scale = 1;
        }

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

        private async void BtnLibrary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Library());
        }

        private async void BtnEvents_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Events());
        }

        private async void BtnCareerCenter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.CareerCenter());
        }

        private async void BtnForum_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForumP());
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

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                auth = DependencyService.Get<Interface>();

                if (await firebaseHelper.GetPerson(auth.GetCurrentUserEmail()) == null)
                {
                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new NewUserData());
                    Navigation.RemovePage(previousPage);
                }

                await GetCurrentUserInfo();

                scheduleContent.IsVisible = true;
                NoInternetContent.IsVisible = false;
            }
            else
            {
                scheduleContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        private async Task GetCurrentUserInfo()
        {
            if (Application.Current.Properties.ContainsKey("firstname") && Application.Current.Properties.ContainsKey("flname") && Application.Current.Properties.ContainsKey("email"))
            {
                label_name.Text = helper.GetLocalData("firstname") + "\n";
                label_flname.Text = helper.GetLocalData("flname");
                label_email.Text = helper.GetLocalData("email");
            }
            else
            {
                var currentUser = await firebaseHelper.GetPerson(auth.GetCurrentUserEmail());

                label_name.Text = currentUser.FirstName + "\n";
                label_flname.Text = currentUser.FirstName + " " + currentUser.LastName;
                label_email.Text = currentUser.Email;

                await helper.SaveLocallyAsync(currentUser.FirstName, "firstname");
                await helper.SaveLocallyAsync(currentUser.FirstName + " " + currentUser.LastName, "flname");
                await helper.SaveLocallyAsync(currentUser.Email, "email");
            }

            await LandingText.FadeTo(1, 200, Easing.SinIn);
        }
    }
}