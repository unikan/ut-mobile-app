using System;
using System.Collections.Generic;
using System.Text;
using XamarinFirebase.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UtMobileApp.Extensions;

namespace UtMobileApp.Extensions
{
    class ForumStuffing
    {

        FirebaseClient firebase = new FirebaseClient("https://utappdatabase.firebaseio.com/");

        public async Task<List<ForumPosts>> GetPosts()
        {

            var posts = (await firebase
                .Child("ForumPosts")
                .OnceAsync<ForumPosts>()).Select(item => new ForumPosts
                {

                    PostID = item.Object.PostID,
                    PostAuthorName = item.Object.PostAuthorName,
                    PostAuthorLastName = item.Object.PostAuthorLastName,
                    PostTitle = item.Object.PostTitle,
                    PostContent = item.Object.PostContent,
                    PostTime = item.Object.PostTime,
                    PostProgram = item.Object.PostProgram,
                    PostImage = item.Object.PostImage,
                }).ToList();

            posts = posts.OrderBy(x => x.PostTime).ToList();
            return posts;
        }

        public async Task<List<ForumComments>> GetComments(string currentPostID)
        {

            return (await firebase
                .Child("ForumComments")
                .OnceAsync<ForumComments>()).Select(item => new ForumComments
                {
                    CommentID = item.Object.CommentID,
                    PostID = item.Object.PostID,
                    CommentAuthorName = item.Object.CommentAuthorName,
                    CommentAuthorLastName = item.Object.CommentAuthorLastName,
                    CommentContent = item.Object.CommentContent,
                    CommentTime = item.Object.CommentTime,
                    CommentImage = item.Object.CommentImage
                }).Where(c => c.PostID == currentPostID).ToList();
        }

        public async Task CreatePost(string postid, string postauthorname, string postauthorlastname, string posttitle, string postcontent , DateTime posttime, string postprogram, string imageurl)
        {
            await firebase
                .Child("ForumPosts")
                .PostAsync(new ForumPosts() { PostID = postid, PostAuthorName = postauthorname, PostAuthorLastName = postauthorlastname, PostTitle = posttitle, PostContent = postcontent, PostTime = posttime, PostProgram = postprogram , PostImage = imageurl});
        }

        public async Task CreateComment(string commentid, string postid, string commentauthorname, string commentauthorlastname, string commentcontent, DateTime commentime, string commentimage)
        {
            await firebase
                .Child("ForumComments")
                .PostAsync(new ForumComments() { CommentID = commentid, PostID = postid, CommentAuthorName = commentauthorname, CommentAuthorLastName = commentauthorlastname, CommentContent = commentcontent, CommentTime = commentime,CommentImage = commentimage });
        }

    }
}
