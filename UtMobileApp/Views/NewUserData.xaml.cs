using Syncfusion.SfAutoComplete.XForms;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserData : ContentPage
    {
        public NewUserData()
        {
            InitializeComponent();
        }

        private void SfAutoComplete_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            List<String> programnames = new List<String>();
            if(e.Value.ToString() == "Faculty of Agriculture and Biotechnology")
            programnames.Add("test");
            programnames.Add("test");
            programnames.Add("test");
            AutoCompleteProgram.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteProgram.DataSource = programnames;
            InputProgram.InputView = AutoCompleteProgram;
        }
    }
}