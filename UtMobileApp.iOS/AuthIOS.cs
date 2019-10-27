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

        public async Task<string> Burek(string getuser, string getemail)
        {

            var currentuser = Auth.DefaultInstance.CurrentUser;

            if (currentuser != null)
            {
                getuser = currentuser.Uid;
                getemail = currentuser.Email;
            }
            return getuser;
        }

        public async Task<string> ResetPassword(string email)
        {
            try
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
                return email;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }

        }

        public async Task<string> SignupWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                return "";
            }

        }


    }
}