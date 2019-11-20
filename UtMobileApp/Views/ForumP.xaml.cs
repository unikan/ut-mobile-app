using System;
using XamarinFirebase.Helper;
using XamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UtMobileApp.Extensions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;


namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumP : ContentPage
    {
        FirebaseStorageHelp firebaseStorageHelper = new FirebaseStorageHelp();
        MediaFile file;
        ForumStuffing forumhelper = new ForumStuffing();
        FirebaseHelper firebasehelper = new FirebaseHelper();
        Interface auth; 
        public ForumP()
        {
            InitializeComponent();

            auth = DependencyService.Get<Interface>();

        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allPosts = await forumhelper.GetPosts();

        }

        private async void Post_Clicked(object sender, EventArgs e)
        {
            Registrations currentuserinfo = await firebasehelper.GetPerson(auth.GetCurrentUserEmail());
            await forumhelper.CreatePost(auth.GetCurrentUserEmail() + DateTime.Now.Ticks,currentuserinfo.FirstName,currentuserinfo.LastName,PostTitle.Text,TextEditor.Text,DateTime.Now,currentuserinfo.Program);
            await DisplayAlert("Success", "You have created a new post", "OK");
                
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
                await DisplayAlert("Warning" ,ex.Message , "OK");
            }

        }

        private async void btnUpload_Clicked(object sender, EventArgs e)
        {
            await firebaseStorageHelper.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
        }
    }
}