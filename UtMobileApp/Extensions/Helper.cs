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

        public bool ValidateEntry(string inputValue)
        {
            if (String.IsNullOrEmpty(inputValue) || String.IsNullOrWhiteSpace(inputValue))
            {
                return false;
            }

            return true;
        }
    }
}
