using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UtMobileApp.Extensions
{
    public class LoadLibrary
    {
        Helper helper = new Helper();

        public async Task<List<Models.LibraryJSON.Entry>> DeserializeLibraryJsonAsync(string loadType, string url = "")
        {
            string result = "";
            // Try to get data from phone if it exists
            if (loadType == "local")
            {
                result = helper.GetLocalData("LibraryData");
            }
            // Try to get data from internet
            else if (loadType == "internet")
            {
                result = await helper.GetLatestJsonAsync(url);
                await helper.SaveLocallyAsync(result, "LibraryData");
            }

            Models.LibraryJSON.RootObject root = JsonConvert.DeserializeObject<Models.LibraryJSON.RootObject>(result);
            var allEntries = root.feed.entry;
            var filledEntries = allEntries
                .Where(x => x.Author != null && x.BookTitle != null && x.Year != null)
                .ToList();

            return filledEntries;
        }
    }
}
