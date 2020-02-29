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
            InitializeComponent();

            // Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjE3MzQxQDMxMzcyZTM0MmUzMGxsY1UwU3dDbnZqbFpqekk5L2hHN1JUdURSUTNLUWtESk5pSnF1cDdQQkk9");



            try
            {
                auth = DependencyService.Get<Interface>();

                if (auth.GetCurrentUserStatus())
                {
                    MainPage = new FormsControls.Base.AnimationNavigationPage(new Views.MainPageStudent());
                }
                else
                {
                    MainPage = new FormsControls.Base.AnimationNavigationPage(new Views.IntroPage());
                }
            }
            catch (Exception)
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
