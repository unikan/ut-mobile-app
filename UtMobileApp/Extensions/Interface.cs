using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace UtMobileApp
{
    public interface Interface
    {
       Task<string> LoginWithEmailPassword(string email, string password);

        Task<string> Burek(string getuser, string getemail);

        Task<string> SignupWithEmailPassword(string email, string password);

        Task<string> ResetPassword(string email);

        Task<string> VerifyEmail(string email);

    }
}
