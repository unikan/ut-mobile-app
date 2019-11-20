using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using UtMobileApp.CustomRenderer;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyWebView), typeof(WebViewRenderer))]
namespace UtMobileApp.iOS.CustomRendereriOS
{
    public class WebViewRenderer : ViewRenderer<MyWebView, WKWebView>
    {
        WKWebView _wkWebView;
        protected override void OnElementChanged(ElementChangedEventArgs<MyWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var config = new WKWebViewConfiguration();
                _wkWebView = new WKWebView(Frame, config);
                SetNativeControl(_wkWebView);
            }
            if (e.NewElement != null)
            {
                Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Url)));
            }
        }
    }
}