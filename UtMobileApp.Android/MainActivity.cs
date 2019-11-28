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
using Plugin.CurrentActivity;

namespace UtMobileApp.Android
{
    [Activity(Label = "UnitEd - University of Tetova", Icon = "@drawable/icon", RoundIcon = "@mipmap/icon_round", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, global::Android.Content.PM.Permission[] grantResults)
        {
            
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CrossCurrentActivity.Current.Init(this, bundle);

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

