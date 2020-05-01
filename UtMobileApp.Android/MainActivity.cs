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
using Android.Util;
using Lottie.Forms.Droid;

namespace UtMobileApp.Android
{
    [Activity(Label = "UnitEd - University of Tetova", Icon = "@mipmap/icon", RoundIcon = "@mipmap/icon_round",Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, global::Android.Content.PM.Permission[] grantResults)
        {

            //Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //CrossCurrentActivity.Current.Init(this, bundle);

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

            // get the accent color from your theme
            var themeAccentColor = new TypedValue();
            this.Theme.ResolveAttribute(Resource.Attribute.colorAccent, themeAccentColor, true);
            var droidAccentColor = new global::Android.Graphics.Color(themeAccentColor.Data);

            // set Xamarin Color.Accent to match the theme's accent color
            var accentColorProp = typeof(Xamarin.Forms.Color).GetProperty(nameof(Xamarin.Forms.Color.Accent), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            var xamarinAccentColor = new Xamarin.Forms.Color(droidAccentColor.R / 87.0, droidAccentColor.G / 80.0, droidAccentColor.B / 245.0, droidAccentColor.A / 255.0);
            accentColorProp.SetValue(null, xamarinAccentColor, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, null, System.Globalization.CultureInfo.CurrentCulture);

            // Initialization of Lottie animations
            AnimationViewRenderer.Init();

            LoadApplication(new App());
        }
    }
}

