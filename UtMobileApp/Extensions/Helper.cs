using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public async Task<string> GetLatestJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(url));
                string result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string> GetJsonAsync(string url)
        {
            string result = "";
            if (Application.Current.Properties.ContainsKey("LecturesData"))
            {
                result = GetLocalData("LecturesData");
            }
            else
            {
                result = await GetJsonAsync(url);
                await SaveLocallyAsync(result, "LecturesData");
            }

            return result;
        }

        public async Task SaveLocallyAsync(string json, string key)
        {
            var app = (App)Application.Current;
            app.Properties[key] = json;
            await app.SavePropertiesAsync();
        }

        public string GetLocalData(string key)
        {
            return Application.Current.Properties[key].ToString();
        }
    }
}
