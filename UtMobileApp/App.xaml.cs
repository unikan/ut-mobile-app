using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace UtMobileApp
{
    public partial class App : Application
    {
        readonly Interface auth;

        public App()
        {
            // Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUwODY0QDMxMzcyZTMzMmUzMFl5bDFGeGJPRDAwU3VhRW9wd2UrSUFpbk80bVZpMER6R0ljZ2RSdkdkdlU9");
            auth = DependencyService.Get<Interface>();

            InitializeComponent();

            if (auth.GetCurrentUserStatus())
            {
                MainPage = new FormsControls.Base.AnimationNavigationPage(new Views.MainPageStudent());
            }
            else
            {
                MainPage = new FormsControls.Base.AnimationNavigationPage(new Views.IntroPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            System.Threading.Tasks.Task.Factory.StartNew(() => { var o = Newtonsoft.Json.JsonConvert.DeserializeObject("https://jsonplaceholder.typicode.com/users"); });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
