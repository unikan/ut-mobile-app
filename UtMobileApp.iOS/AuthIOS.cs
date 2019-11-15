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

        public async Task<Tuple<string, bool>> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return Tuple.Create(await user.User.GetIdTokenAsync(),false);
            }
            catch (Exception e)
            {
                return Tuple.Create(e.Message, true);
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
            catch (Exception)
            {
               
                return "";
            }

        }

        public void SignOut()
        {
            string k = "You haven't signed in yet";
            try
            {
                Auth.DefaultInstance.SignOut(out NSError error);
            }
            catch (Exception)
            {

                Console.WriteLine(k);

            }

        }

        //public async Task<Tuple<string, bool>> CheckifEmailExists(string email)
        //{
        //    try
        //    {
        //        await Auth.DefaultInstance.FetchProvidersAsync(email);
        //        return Tuple.Create(email, false);
        //    }
        //    catch (Exception e)
        //    {
        //        return Tuple.Create(e.Message, true);
        //    }
        //}

        public string GetCurrentUserEmail()
        {
            string getEmail = "";

            try
            {
                var currentuser = Auth.DefaultInstance.CurrentUser;

                if (currentuser != null)
                {
                    getEmail = currentuser.Email;
                }
            }
            catch (Exception) { }

            return getEmail;
        }

        public bool GetCurrentUserStatus()
        {
            bool status = false;

            try
            {
                var currentuser = Auth.DefaultInstance.CurrentUser;

                if (currentuser != null)
                {
                    status = currentuser.IsEmailVerified;
                }
            }
            catch (Exception) { }

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

        public async Task<string> VerifyEmail()
        {
            try
            {
                var action = new ActionCodeSettings
                {
                    IOSBundleId = "Unikan.Utapp"
                };
                using (var actionCode = action)
                {
                    await Auth.DefaultInstance.CurrentUser.SendEmailVerificationAsync(actionCode);
                }

                return "";
            }
            catch (Exception)
            {
                return "Cannot send email verification link ";
            }
        }


        public async Task<Tuple<string, bool>> SignupWithEmailPassword(string email, string password)
        {
           
            try {
                //var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var action = new ActionCodeSettings
                {
                    IOSBundleId = "Unikan.Utapp"
                };
                var auth = Auth.DefaultInstance;
            using (var authResult = await auth.CreateUserAsync(email, password))
            using (var user = authResult.User)
            using (var actionCode = action)
            {
                await user.SendEmailVerificationAsync(actionCode);
                    return Tuple.Create("success", false);
                }

            }
            
            catch (Exception e)
            {
                return Tuple.Create(e.Message, true);
            }

            //await FirebaseAuth.Instance.CurrentUser.SendEmailVerificationAsync();

            //var token = await user.User.GetIdTokenAsync(false);
            //return token.Token;
        }


    }
}