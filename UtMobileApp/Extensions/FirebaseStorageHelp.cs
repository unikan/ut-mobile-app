using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;

namespace UtMobileApp.Extensions
{
    class FirebaseStorageHelp
    {

        FirebaseStorage firebaseStorage = new FirebaseStorage("utappdatabase.appspot.com");

        public async Task<string> UploadFile(Stream fileStream , string postorcommentid , string fileid)
        {
            var imageUrl = await firebaseStorage
                .Child("ForumPictures")
                .Child(postorcommentid)
                .Child(fileid)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child("ForumPictures")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }
        public async Task DeleteFile(string fileName)
        {
            await firebaseStorage
                 .Child("ForumPictures")
                 .Child(fileName)
                 .DeleteAsync();

        }

    }
}
