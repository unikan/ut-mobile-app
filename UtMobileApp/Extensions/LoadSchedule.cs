using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UtMobileApp.Extensions
{
    class LoadSchedule
    {
        readonly Extensions.Helper helper = new Extensions.Helper();

        public async Task<List<Models.ScheduleJSON.Entry>> DeserializeJsonAsync(string loadType, string url = "")
        {
            string result = "";
            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalData("LecturesData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync(url);
                await helper.SaveLocallyAsync(result, "LecturesData");
            }

            Models.ScheduleJSON.RootObject root = JsonConvert.DeserializeObject<Models.ScheduleJSON.RootObject>(result);
            // Remove 5 rows of unnecessary content
            root.feed.entry.RemoveRange(0, 5);
            return root.feed.entry;
        }

        public async Task<List<Models.MidtermsJSON.Entry>> DeserializeMidtermsJsonAsync(string loadType, string url = "")
        {
            string result = "";
            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalData("MidtermsData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync(url);
                await helper.SaveLocallyAsync(result, "MidtermsData");
            }

            Models.MidtermsJSON.RootObject root = JsonConvert.DeserializeObject<Models.MidtermsJSON.RootObject>(result);
            // Remove 5 rows of unnecessary content
            root.feed.entry.RemoveRange(0, 5);
            return root.feed.entry;
        }

        public async Task<List<Models.ExamsJSON.Entry>> DeserializeExamsJsonAsync(string loadType, string url = "")
        {
            string result = "";

            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalData("ExamsData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync(url);
                await helper.SaveLocallyAsync(result, "ExamsData");
            }

            Models.ExamsJSON.RootObject root = JsonConvert.DeserializeObject<Models.ExamsJSON.RootObject>(result);
            // Remove 5 rows of unnecessary content
            root.feed.entry.RemoveRange(0, 5);
            return root.feed.entry;
        }

        public async Task<List<Models.EventsJSON.Event>> DeserializeEventsJsonAsync(string loadType)
        {
            string result = "";

            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalData("EventsData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync("https://unite.edu.mk/wp-json/tribe/events/v1/events/?categories=event");
                await helper.SaveLocallyAsync(result, "EventsData");
            }

            Models.EventsJSON.RootObject root = JsonConvert.DeserializeObject<Models.EventsJSON.RootObject>(result);
            return root.events;
        }
    }
}
