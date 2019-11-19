using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFirebase.Helper;
using XamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UtMobileApp.Extensions;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumP : ContentPage
    {

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

    }
}