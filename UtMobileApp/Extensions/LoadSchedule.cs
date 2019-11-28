using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UtMobileApp.Extensions
{
    class LoadSchedule
    {
        Extensions.Helper helper = new Extensions.Helper();

        public async Task<List<Models.ScheduleJSON.Entry>> DeserializeJsonAsync(string loadType, string url = "")
        {
            string result = "";
            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalJsonAsync("LecturesData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync(url);
                await helper.SaveLocallyAsync(result, "LecturesData");
            }

            // Check if result has json
            if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
            {
                return null;
            }
            else
            {
                Models.ScheduleJSON.RootObject root = JsonConvert.DeserializeObject<Models.ScheduleJSON.RootObject>(result);

                // Remove 5 rows of unnecessary content
                root.feed.entry.RemoveRange(0, 5);
                return root.feed.entry;
            }
        }

        public async Task<List<Models.MidtermsJSON.Entry>> DeserializeMidtermsJsonAsync(string url, string loadType)
        {
            string result = "";
            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalJsonAsync("MidtermsData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync(url);
                await helper.SaveLocallyAsync(result, "MidtermsData");
            }

            // Check if result has json
            if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
            {
                return null;
            }
            else
            {
                Models.MidtermsJSON.RootObject root = JsonConvert.DeserializeObject<Models.MidtermsJSON.RootObject>(result);

                // Remove 5 rows of unnecessary content
                root.feed.entry.RemoveRange(0, 5);
                return root.feed.entry;
            }
        }

        public async Task<List<Models.ExamsJSON.Entry>> DeserializeExamsJsonAsync(string url, string loadType)
        {
            string result = "";

            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalJsonAsync("ExamsData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync(url);
                await helper.SaveLocallyAsync(result, "ExamsData");
            }

            // Check if result has json
            if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
            {
                return null;
            }
            else
            {
                Models.ExamsJSON.RootObject root = JsonConvert.DeserializeObject<Models.ExamsJSON.RootObject>(result);

                // Remove 5 rows of unnecessary content
                root.feed.entry.RemoveRange(0, 5);
                return root.feed.entry;
            }
        }
    }
}
