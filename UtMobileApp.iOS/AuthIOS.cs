using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Firebase.Auth;
using UtMobileApp;
using UtMobileApp.iOS;

[assembly: Dependency(typeof(AuthIOS))]

namespace UtMobileApp.iOS
{
    public class AuthIOS : Interface
    {

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                return "";
            }

        }

        //public async Task<string> Burek(string getuser, string getemail)
        //{

        //    var currentuser = Auth.DefaultInstance.CurrentUser;

        //    if (currentuser != null)
        //    {
        //        getuser = currentuser.Uid;
        //        getemail = currentuser.Email;
        //    }
        //    return getuser;
        //}

        public async Task<string> ResetPassword(string email)
        {
            try
            {
                await Auth.DefaultInstance.SendPasswordResetAsync(email);
                return email;
            }
            catch (Exception e)
            {
               
                return "";
            }

        }

        public string GetCurrentUserEmail()
        {

            var currentuser = Auth.DefaultInstance.CurrentUser;
            string getEmail = "";

            if (currentuser != null)
            {
                getEmail = currentuser.Email;
            }

            return getEmail;
        }

        public bool GetCurrentUserStatus()
        {
            var currentuser = Auth.DefaultInstance.CurrentUser;
           
            bool status = false;

            if (currentuser != null)
            {
                status = currentuser.IsEmailVerified;
            }

            return status;
        }

        //public async Task<string> SignupWithEmailPassword(string email, string password)
        //{
        //    try
        //    {
        //        var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
        //        return await user.User.GetIdTokenAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        return "";
        //    }

        //}

        public async void SignupWithEmailPassword(string email, string password)
        {
            //var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            var action = new ActionCodeSettings();
            action.IOSBundleId = "Unikan.Utapp";
            var auth = Auth.DefaultInstance;
            using (var authResult = await auth.CreateUserAsync(email, password))
            using (var user = authResult.User)
            using (var actionCode = action)
            {
                await user.SendEmailVerificationAsync(actionCode);
            }

            //await FirebaseAuth.Instance.CurrentUser.SendEmailVerificationAsync();

            //var token = await user.User.GetIdTokenAsync(false);
            //return token.Token;
        }


    }
}