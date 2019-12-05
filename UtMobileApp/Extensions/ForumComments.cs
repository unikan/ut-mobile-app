using System;
using System.Collections.Generic;
using System.Text;

namespace UtMobileApp.Extensions
{
    class ForumComments
    {

        public string CommentID { get; set; } 
        public string PostID { get; set; } 
        public string CommentAuthorName { get; set; }
        public string CommentAuthorLastName { get; set; }
        public string CommentContent { get; set; } 
        public DateTime CommentTime { get; set; }
        public string CommentImage { get; set; }

        public string Author
        {
            get
            {
                return CommentAuthorName + " " + CommentAuthorLastName;
            }
        }
    }
}
