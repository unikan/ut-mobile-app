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

        public async Task<Tuple<string, bool>> LoginWithEmailPassword(string email, string password)
        {

            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return Tuple.Create(token.Token, false);
            }

            catch (Exception e)
            {
                //e.PrintStackTrace();
                return Tuple.Create(e.Message, true);
            }

        }

        //catch (FirebaseAuthInvalidCredentialsException e)
        //{
        //    //e.PrintStackTrace();
        //    return Tuple.Create(e.Message, true);
        //}

        //catch (FirebaseAuthInvalidUserException e)
        //{
        //    //e.PrintStackTrace();
        //    return Tuple.Create(e.Message, true);
        //}
        //catch (FirebaseAuthEmailException e)
        //{
        //    //e.PrintStackTrace();
        //    return Tuple.Create(e.Message, true);

        //}


        //public async Task<Tuple<string, bool>> CheckifEmailExists(string email)
        //{
        //    try {  
        //    await FirebaseAuth.Instance.FetchProvidersForEmailAsync(email);
        //        return Tuple.Create(email, false);
        //    }
        //    catch(Exception e)
        //    {
        //        return Tuple.Create(e.Message, true);
        //    }
        //}

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

        //public async Task<string> VerifyEmail(string email)
        //{
        //    try
        //    {
        //        FirebaseUser currentuser = FirebaseAuth.Instance.CurrentUser;
        //        email = currentuser.Email;
                
        //        await FirebaseAuth.Instance.CurrentUser.SendEmailVerificationAsync();
        //        return email;
        //    }
        //    catch (FirebaseAuthInvalidUserException e)
        //    {
        //        e.PrintStackTrace();
        //        return "";
        //    }
        //}

        public string GetCurrentUserEmail()
        {
            
            var currentuser = FirebaseAuth.Instance.CurrentUser;
            string getEmail = "";

            if (currentuser != null)
            {
                getEmail = currentuser.Email;
            }

            return getEmail;
        }

        public bool GetCurrentUserStatus()
        {
            var currentuser = FirebaseAuth.Instance.CurrentUser;
            bool status = false;

            if (currentuser != null)
            {
                status = currentuser.IsEmailVerified;
            }

            return status;
        }

        public async Task<string> VerifyEmail()
        {
            try
            {
                using (var actionCode = ActionCodeSettings.NewBuilder().SetAndroidPackageName("Unikan.Utapp", true, "0").Build())
                {
                    await FirebaseAuth.Instance.CurrentUser.SendEmailVerificationAsync(actionCode);
                }

                return "";
            } 
            catch(Exception)
            {
                return "Cannot send email verification link ";
            }
        }


        public void SignOut()
        {
            string k = "you haven't signed in yet";
            try
            {
                FirebaseAuth.Instance.SignOut();
            }
            catch (Exception)
            {

                Console.WriteLine(k);

            }

        }



        public async Task<Tuple<string, bool>> SignupWithEmailPassword(string email, string password)
        {
            //var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            try {
                var auth = FirebaseAuth.Instance;
                using (var authResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password))
                using (var user = authResult.User)
                using (var actionCode = ActionCodeSettings.NewBuilder().SetAndroidPackageName("Unikan.Utapp", true, "0").Build())
                {
                    await user.SendEmailVerificationAsync(actionCode);
                    return Tuple.Create("success", false);
                }
            }
            //await FirebaseAuth.Instance.CurrentUser.SendEmailVerificationAsync();
            //var token = await user.User.GetIdTokenAsync(false);
            //return token.Token;
            catch (Exception e) {
                return Tuple.Create(e.Message, true);
            }
        }
    }
}