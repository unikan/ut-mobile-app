using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Foundation;
using UIKit;
using UtMobileApp.Extensions;
using UtMobileApp.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(CloseAppiOS))]

namespace UtMobileApp.iOS
{
    public class CloseAppiOS : ICloseApp
    {
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}