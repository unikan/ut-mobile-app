using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFirebase.Helper;
using XamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using UtMobileApp.Extensions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net;
using System.IO;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumC : ContentPage
    {
        FirebaseStorageHelp firebaseStorageHelper = new FirebaseStorageHelp();
        MediaFile file;
        ForumStuffing forumhelper = new ForumStuffing();
        FirebaseHelper firebasehelper = new FirebaseHelper();
        Interface auth;
        protected Extensions.ForumPosts currentpost;
        public ForumC(Extensions.ForumPosts SelectedPost)
        {
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
            currentpost = SelectedPost;
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            commentList.ItemsSource = await forumhelper.GetComments(currentpost.PostID);

        }

        private async void btnPick_Clicked(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }

        }

        private async void Comment_Clicked(object sender, EventArgs e)
        {

            try
            {
                var commentids = auth.GetCurrentUserEmail() + DateTime.Now.Ticks;
                var noimage = "No image file";
                Registrations currentuserinfo = await firebasehelper.GetPerson(auth.GetCurrentUserEmail());

                if(file != null) {  

                await firebaseStorageHelper.UploadFile(file.GetStream(), currentpost.PostID, commentids);
               
                await forumhelper.CreateComment(commentids, currentpost.PostID, currentuserinfo.FirstName, currentuserinfo.LastName, TextEditor.Text, DateTime.Now, await firebaseStorageHelper.GetFile(currentpost.PostID,commentids));
                await DisplayAlert("Warning", "Comment created with uploaded image", "OK");
                }

                else
                {
                    await forumhelper.CreateComment(commentids, currentpost.PostID, currentuserinfo.FirstName, currentuserinfo.LastName, TextEditor.Text, DateTime.Now, noimage);
                    await DisplayAlert("Warning", "Comment created", "OK");

                }
                // await forumhelper.CreateComment(auth.GetCurrentUserEmail() + DateTime.Now.Ticks, ,currentuserinfo.FirstName, currentuserinfo.LastName, TextEditor.Text, DateTime.Now);
                //  await DisplayAlert("Success", "You have created a new post", "OK");
            }
            catch (Exception exs)
            {
                await DisplayAlert("Warning", exs.Message, "OK");
            }
            
            }

        private async void GetPic_Clicked(object sender, EventArgs es)
        {
            Button btn = sender as Button;

            try { 
            var webClient = new WebClient();
                webClient.DownloadDataCompleted += (s, e) => {
                    var bytes = e.Result; // get the downloaded data
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string localFilename = "downloaded.png";
                    string localPath = Path.Combine(documentsPath, localFilename);
                    File.WriteAllBytes(localPath, bytes); // writes to local storage
                };
                var url = new Uri(btn.CommandParameter.ToString());
            webClient.DownloadDataAsync(url);
            }
            catch (Exception exs)
            {
                await DisplayAlert("Warning", exs.Message, "OK");

            }
        }
    }
}