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

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumC : ContentPage
    {
        
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
            var allComments = await forumhelper.GetComments();

        }


        private async void Comment_Clicked(object sender, EventArgs e)
        {
            Registrations currentuserinfo = await firebasehelper.GetPerson(auth.GetCurrentUserEmail());
            List<ForumPosts> getpostinfo = await forumhelper.GetPosts();
           // await forumhelper.CreateComment(auth.GetCurrentUserEmail() + DateTime.Now.Ticks, ,currentuserinfo.FirstName, currentuserinfo.LastName, TextEditor.Text, DateTime.Now);
           //  await DisplayAlert("Success", "You have created a new post", "OK");
        }
    }
}