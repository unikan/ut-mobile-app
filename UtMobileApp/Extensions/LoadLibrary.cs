using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

                return root.feed.entry;
            }
        }
    }
}
