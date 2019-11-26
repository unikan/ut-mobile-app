using System;
using XamarinFirebase.Helper;
using XamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UtMobileApp.Extensions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading.Tasks;

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
            forumList.ItemsSource = await forumhelper.GetPosts();

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

        private async void Post_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                string nopicture = "No image file";
                var postID = auth.GetCurrentUserEmail() + DateTime.Now.Ticks;
                Registrations currentuserinfo = await firebasehelper.GetPerson(auth.GetCurrentUserEmail());
                if(file != null) { 
                await firebaseStorageHelper.UploadFile(file.GetStream(), postID, postID);
    
                    
                    await forumhelper.CreatePost(postID, currentuserinfo.FirstName, currentuserinfo.LastName, PostTitle.Text, TextEditor.Text, DateTime.Now, currentuserinfo.Program, await firebaseStorageHelper.UploadFile(file.GetStream(), postID, postID));

                    await DisplayAlert("Success", "You have created a new image post", "OK");
                }
                else
                {
                    await forumhelper.CreatePost(postID, currentuserinfo.FirstName, currentuserinfo.LastName, PostTitle.Text, TextEditor.Text, DateTime.Now, currentuserinfo.Program, nopicture);

                    await DisplayAlert("Success", "You have created a new post", "OK");
                }

            }

            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }

        }

        private async void forumList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try {  
            var selectedPost = e.ItemData as Extensions.ForumPosts;

            await Navigation.PushAsync(new ForumC(selectedPost));
            }
            catch (Exception except)
            {
                await DisplayAlert("Warning", except.Message, "OK");
            }
        }
    }
}