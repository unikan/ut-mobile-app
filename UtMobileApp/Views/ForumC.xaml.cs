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
        private bool _firstAppearance = true;

        public ForumC(Extensions.ForumPosts SelectedPost)
        {
            InitializeComponent();
            auth = DependencyService.Get<Interface>();
            currentpost = SelectedPost;
            NavigationPage.SetHasNavigationBar(this, false);
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
                        commentList.ItemsSource = await forumhelper.GetComments(currentpost.PostID.ToString());
                        label_author.Text = currentpost.Author;
                        label_date.Text = currentpost.PostTime.ToString("dddd, dd MMMM yyyy HH:mm");
                        label_content.Text = currentpost.PostContent;
                        label_forum.Text = currentpost.PostTitle;

                        if (currentpost.PostImage != "No image file")
                        {
                            image_post.Source = currentpost.PostImage;
                            BtnImage.CommandParameter = currentpost.PostImage;
                        }

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

            await busyindicator.FadeTo(0, 300, Easing.Linear);
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

                if(file != null) 
                {  
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

        [Obsolete]
        private void GetPic_Clicked(object sender, EventArgs es)
        {
            try
            {
                Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;

                Device.OpenUri(new Uri(btn.CommandParameter.ToString()));
            }
            catch (Exception)
            {
                DisplayAlert("Warning", "A problem occured", "OK");
            }
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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

                    commentList.ItemsSource = await forumhelper.GetComments(currentpost.PostID.ToString());
                    label_author.Text = currentpost.Author;
                    label_date.Text = currentpost.PostTime.ToString("dddd, dd MMMM yyyy HH:mm");
                    label_content.Text = currentpost.PostContent;
                    label_forum.Text = currentpost.PostTitle;

                    if (currentpost.PostImage != "No image file")
                    {
                        image_post.Source = currentpost.PostImage;
                        BtnImage.CommandParameter = currentpost.PostImage;
                    }

                    ForumContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;

                    // Hide busy indicator
                    await busyindicator.FadeTo(0, 300, Easing.Linear);
                    // Show label
                    await label_forum.FadeTo(1, 300, Easing.Linear);
                }
                else
                {
                    ForumContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async void BtnExpand_Clicked(object sender, EventArgs e)
        {
            if (CommentInputs.Scale == 0)
            {
                await CommentInputs.ScaleTo(1, 300, Easing.Linear);
                CommentInputs.IsVisible = true;
            }
            else
            {
                await CommentInputs.ScaleTo(0, 300, Easing.Linear);
                CommentInputs.IsVisible = false;
            }
        }

        private async void Btn_UpdateComments_Clicked(object sender, EventArgs e)
        {
            // Hide label
            await label_forum.FadeTo(0, 300, Easing.Linear);
            // Show busy indicator
            await busyindicator.FadeTo(1, 300, Easing.Linear);

            commentList.ItemsSource = await forumhelper.GetComments(currentpost.PostID.ToString());

            // Hide busy indicator
            await busyindicator.FadeTo(0, 300, Easing.Linear);
            // Show label
            await label_forum.FadeTo(1, 300, Easing.Linear);
        }
    }
}