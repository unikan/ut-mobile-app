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
        public async Task<List<Models.LibraryJSON.Entry>> DeserializeLibraryJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(url));
                string result = await response.Content.ReadAsStringAsync();
                Models.LibraryJSON.RootObject root = JsonConvert.DeserializeObject<Models.LibraryJSON.RootObject>(result);

                var allEntries = root.feed.entry;
                var filledEntries = allEntries
                    .Where(x => x.Author != null && x.BookTitle != null && x.Year != null)
                    .ToList();

                return filledEntries;
            }
        }
    }
}
