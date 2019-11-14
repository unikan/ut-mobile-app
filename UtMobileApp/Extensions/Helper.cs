using System;
using System.Collections.Generic;
using System.Text;

namespace UtMobileApp.Extensions
{
    public class Helper
    {
        public void DisableButton(Syncfusion.XForms.Buttons.SfButton btn)
        {
            // Disable button
            btn.IsEnabled = false;
        }

        public async System.Threading.Tasks.Task EnableButtonAfter2Sec(Syncfusion.XForms.Buttons.SfButton btn)
        {
            // Wait two seconds and enable it again
            await System.Threading.Tasks.Task.Delay(2000);
            btn.IsEnabled = true;
        }

        //public async System.Threading.Tasks.Task PushPreventDoubleClickAsync(Syncfusion.XForms.Buttons.SfButton btn, Xamarin.Forms.Page page)
        //{
        //    // Disable button
        //    btn.IsEnabled = false;

        //    // Send to next page
        //    var nav = new Xamarin.Forms.NavigationPage();
        //    page = new page();
        //    await nav.PushAsync(new page);

        //    // Wait one second and enable it again
        //    await System.Threading.Tasks.Task.Delay(2000);
        //    btn.IsEnabled = true;
        //}

        //public async System.Threading.Tasks.Task PopPreventDoubleClickAsync(Syncfusion.XForms.Buttons.SfButton btn)
        //{
        //    // Disable button
        //    btn.IsEnabled = false;

        //    // Send back
        //    var nav = new Xamarin.Forms.NavigationPage();
        //    await nav.PopAsync();

        //    // Wait one second and enable it again
        //    await System.Threading.Tasks.Task.Delay(2000);
        //    btn.IsEnabled = true;
        //}
    }
}
