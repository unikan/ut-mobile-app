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
using Xamarin.Essentials;

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
        private bool _firstAppearance = true;

        public ForumP()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            auth = DependencyService.Get<Interface>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (_firstAppearance)
            {
                _firstAppearance = false;

                try
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        forumList.ItemsSource = await forumhelper.GetPosts();

                        ForumContent.IsVisible = true;
                        NoInternetContent.IsVisible = false;
                    }
                    else
                    {
                        ForumContent.IsVisible = false;
                        NoInternetContent.IsVisible = true;
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("Warning", e.Message, "OK");
                }
            }

            // Hide busy indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            // Show label
            await label_forum.FadeTo(1, 300, Easing.Linear);
        }

        private async void Post_Clicked(object sender, EventArgs e)
        {
            // Hide label
            await label_forum.FadeTo(0, 300, Easing.Linear);
            // Show busy indicator
            await busyindicator.FadeTo(1, 300, Easing.Linear);

            try
            {
                string nopicture = "No image file";
                var postID = auth.GetCurrentUserEmail() + DateTime.Now.Ticks;
                Registrations currentuserinfo = await firebasehelper.GetPerson(auth.GetCurrentUserEmail());
                if(file != null) { 
                await firebaseStorageHelper.UploadFile(file.GetStream(), postID, postID);
    
                    await forumhelper.CreatePost(postID, currentuserinfo.FirstName, currentuserinfo.LastName, PostTitle.Text, TextEditor.Text, DateTime.Now, currentuserinfo.Program, await firebaseStorageHelper.UploadFile(file.GetStream(), postID, postID)).ContinueWith(async updatebutton =>
                    {
                        await Task.Delay(1000);
                        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                        {
                            await UpdateContent.TranslateTo(0, 0, 500, Easing.BounceIn);
                            await Task.Delay(5000);
                            await UpdateContent.TranslateTo(0, 500, 500, Easing.Linear);
                        }
                    });
                }
                else
                {
                    await forumhelper.CreatePost(postID, currentuserinfo.FirstName, currentuserinfo.LastName, PostTitle.Text, TextEditor.Text, DateTime.Now, currentuserinfo.Program, nopicture).ContinueWith(async updatebutton =>
                    {
                        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                        {
                            await UpdateContent.TranslateTo(0, 0, 500, Easing.BounceIn);
                            await Task.Delay(5000);
                            await UpdateContent.TranslateTo(0, 500, 500, Easing.Linear);
                        }
                    });
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }

            // Hide busy indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            // Show label
            await label_forum.FadeTo(1, 300, Easing.Linear);
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

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Btn_UpdatePosts_Clicked(object sender, EventArgs e)
        {
            // Hide label
            await label_forum.FadeTo(0, 300, Easing.Linear);
            // Show busy indicator
            await busyindicator.FadeTo(1, 300, Easing.Linear);

            forumList.ItemsSource = await forumhelper.GetPosts();

            // Hide busy indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            // Show label
            await label_forum.FadeTo(1, 300, Easing.Linear);
        }

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Hide label
                    await label_forum.FadeTo(0, 300, Easing.Linear);
                    // Show busy indicator
                    await busyindicator.FadeTo(1, 300, Easing.Linear);

                    forumList.ItemsSource = await forumhelper.GetPosts();

                    ForumContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;

                    // Hide busy indicator
                    await busyindicator.FadeTo(0, 300, Easing.Linear);
                    // Show label
                    await label_forum.FadeTo(1, 300, Easing.Linear);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private void PostTitle_Completed(object sender, EventArgs e)
        {
            TextEditor.Focus();
        }

        private async void BtnExpand_Clicked(object sender, EventArgs e)
        {
            if (PostInputs.Scale == 0)
            {
                await PostInputs.ScaleTo(1, 300, Easing.Linear);
                PostInputs.IsVisible = true;
            }
            else
            {
                await PostInputs.ScaleTo(0, 300, Easing.Linear);
                PostInputs.IsVisible = false;
            }
        }
    }
}