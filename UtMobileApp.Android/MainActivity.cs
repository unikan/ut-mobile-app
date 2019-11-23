using System;
using Firebase;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace UtMobileApp.Android
{
    [Activity(Label = "UnitEd - University of Tetova", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            FirebaseApp.InitializeApp(Application.Context);

            // Initialization of Animation for transitions
            FormsControls.Droid.Main.Init(this);

            // Appcenter
            AppCenter.Start("f7bbcd28-6f3e-467d-96c2-ee9c7a2d01b9",
                   typeof(Analytics), typeof(Crashes));
            AppCenter.Start("f7bbcd28-6f3e-467d-96c2-ee9c7a2d01b9",
                               typeof(Analytics), typeof(Crashes));

            LoadApplication(new App());
        }
    }
}

