using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static UtMobileApp.Models.ScheduleJSON;

namespace UtMobileApp.Functions
{
    class LoadSchedule
    {
        public async Task<List<Entry>> DeserializeJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(url));
                string result = await response.Content.ReadAsStringAsync();
                RootObject root = JsonConvert.DeserializeObject<RootObject>(result);

                // Remove 5 rows of unnecessary content
                root.feed.entry.RemoveRange(0, 5);
                return root.feed.entry;
            }
        }
    }
}
