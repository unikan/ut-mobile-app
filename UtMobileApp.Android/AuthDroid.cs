using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Xamarin.Forms;
using UtMobileApp;
using UtMobileApp.Android;

[assembly: Dependency(typeof(AuthDroid))]

namespace UtMobileApp.Android
{

    

   public class AuthDroid : Interface
    {

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }

            
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

        public async Task<string> VerifyEmail(string email)
        {
            try
            {
                FirebaseUser currentuser = FirebaseAuth.Instance.CurrentUser;
                email = currentuser.Email;
                
                await FirebaseAuth.Instance.CurrentUser.SendEmailVerificationAsync(email);
                return email;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }

        public async Task<string> Burek(string getuser, string getemail)
        {
            
            var currentuser = FirebaseAuth.Instance.CurrentUser;

            if (currentuser != null)
            {
                getuser = currentuser.Uid;
                getemail = currentuser.Email;
            }
            return getuser;
        }


        public async Task<string> SignupWithEmailPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            
            var token = await user.User.GetIdTokenAsync(false);
            return token.Token;
        }
    }
}