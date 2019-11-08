using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StringTest : ContentPage
    {
        public StringTest()
        {
            InitializeComponent();
        }

        private void String_Clicked(object sender, EventArgs e)
        {
            string emailvalue = EmailInput.Text.ToString();
            string[] split = emailvalue.Split('@');
            if (split[0].Any(char.IsDigit) && (split[1] == "unite.edu.mk"))
            {

                labelemail.Text = "Successful Login, Great Job My Nigga";
            }
            else labelname.Text = "Your Login Sucks";
            
        }
    }
}