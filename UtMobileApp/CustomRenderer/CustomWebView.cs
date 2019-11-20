using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UtMobileApp.CustomRenderer
{
    public class MyWebView : WebView
    {
        public static readonly BindableProperty HtmlProperty = BindableProperty.Create(
            propertyName: "Html",
            returnType: typeof(string),
            declaringType: typeof(MyWebView),
            defaultValue: default(string));

        public string Url
        {
            get { return (string)GetValue(HtmlProperty); }
            set { SetValue(HtmlProperty, value); }
        }
    }
}
