using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Microsoft.AppCenter;
//using Plugin.Permissions;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Lottie.Forms.iOS.Renderers;

namespace UtMobileApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //


        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            // Initialization of Firebase Database
            Firebase.Core.App.Configure();
            // Initialization of Syncfusion components
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            Syncfusion.SfSchedule.XForms.iOS.SfScheduleRenderer.Init();
            Syncfusion.SfNavigationDrawer.XForms.iOS.SfNavigationDrawerRenderer.Init();
            Syncfusion.XForms.iOS.Shimmer.SfShimmerRenderer.Init();
            Syncfusion.ListView.XForms.iOS.SfListViewRenderer.Init();
            Syncfusion.SfBusyIndicator.XForms.iOS.SfBusyIndicatorRenderer.Init();
            Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer.Init();
            Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer.Init();
            Syncfusion.SfNumericUpDown.XForms.iOS.SfNumericUpDownRenderer.Init();
            Syncfusion.XForms.iOS.Graphics.SfGradientViewRenderer.Init();
            Syncfusion.SfCalendar.XForms.iOS.SfCalendarRenderer.Init();
            Syncfusion.SfPullToRefresh.XForms.iOS.SfPullToRefreshRenderer.Init();
            Syncfusion.SfPdfViewer.XForms.iOS.SfPdfDocumentViewRenderer.Init();
            Syncfusion.SfRangeSlider.XForms.iOS.SfRangeSliderRenderer.Init();

            // Initialization of Animation for transitions
            FormsControls.Touch.Main.Init();

            // Appcenter
            AppCenter.Start("c79194b5-5202-45a0-9c03-836b08c99659",
                   typeof(Analytics), typeof(Crashes));

            // Initialization of Lottie animations
            AnimationViewRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
