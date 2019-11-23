using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static UtMobileApp.Models.ScheduleJSON;

namespace UtMobileApp.Extensions
{
    class LoadSchedule
    {
        public async Task<List<Models.ScheduleJSON.Entry>> DeserializeJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(url));
                string result = await response.Content.ReadAsStringAsync();
                Models.ScheduleJSON.RootObject root = JsonConvert.DeserializeObject<Models.ScheduleJSON.RootObject>(result);

                // Remove 5 rows of unnecessary content
                root.feed.entry.RemoveRange(0, 5);
                return root.feed.entry;
            }
        }

        public async Task<List<Models.MidtermsJSON.Entry>> DeserializeMidtermsJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(url));
                string result = await response.Content.ReadAsStringAsync();
                Models.MidtermsJSON.RootObject root = JsonConvert.DeserializeObject<Models.MidtermsJSON.RootObject>(result);

                // Remove 5 rows of unnecessary content
                root.feed.entry.RemoveRange(0, 5);
                return root.feed.entry;
            }
        }

        public async Task<List<Models.ExamsJSON.Entry>> DeserializeExamsJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(url));
                string result = await response.Content.ReadAsStringAsync();
                Models.ExamsJSON.RootObject root = JsonConvert.DeserializeObject<Models.ExamsJSON.RootObject>(result);

                // Remove 5 rows of unnecessary content
                root.feed.entry.RemoveRange(0, 5);
                return root.feed.entry;
            }
        }
    }
}
