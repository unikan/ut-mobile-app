using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace UtMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            // Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUwODY0QDMxMzcyZTMzMmUzMFl5bDFGeGJPRDAwU3VhRW9wd2UrSUFpbk80bVZpMER6R0ljZ2RSdkdkdlU9");

            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPageGuest());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
