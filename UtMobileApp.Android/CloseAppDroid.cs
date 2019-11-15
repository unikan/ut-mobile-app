using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UtMobileApp.Android;
using UtMobileApp.Extensions;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseAppDroid))]

namespace UtMobileApp.Android
{
    public class CloseAppDroid : ICloseApp
    {
        [Obsolete]
        public void CloseApplication()
        {
            (Forms.Context as Activity).Finish();
        }
    }
}