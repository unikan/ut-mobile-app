using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Utility;

namespace UtMobileApp.Extensions
{
    public class WordpressServices
    {
        private readonly WordPressClient _client = new WordPressClient("https://www.unite.edu.mk/wp-json/");
        public string HtmlStart = "<!DOCTYPE html><html><head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/></head><link href=\"https://unite.edu.mk/wp-content/themes/Avada/assets/css/style.min.css?ver=5.5.1\" type=\"text/css\" rel=\"stylesheet\"/><body>";
        public string HtmlEnd = "</body></html>";

        public async Task<IEnumerable<WordPressPCL.Models.Post>> GetLatestPostsAsync(int category)
        {
            var posts = await _client.Posts.Query(new PostsQueryBuilder
            {
                Page = 2,
                PerPage = 10,
                Embed = true,
                Categories = new int[] { category }
            });

            return posts;
        }
    }
}
