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
