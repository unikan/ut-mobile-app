using Syncfusion.SfAutoComplete.XForms;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using XamarinFirebase.Model;
using XamarinFirebase.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    

    public partial class NewUserData : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        Interface auth;


        public NewUserData()
        {
            InitializeComponent();
            auth = DependencyService.Get<Interface>();

            string indexN = auth.GetCurrentUserEmail().ToString();

            string indexNum = new string(indexN.Where(Char.IsDigit).ToArray());

            indexnumber.Text = indexNum.ToString();

        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allRegistrations = await firebaseHelper.GetAllRegistrations();
            var Schedule = await firebaseHelper.GetAllSchedules();
        
        }


        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            
            List<String> programnames = new List<String>();
            
            
            if (e.Value.ToString() == "Faculty of Agriculture and Biotechnology")
            {
                //n.ajruli3615111008@unite.edu.mk
                AutoCompleteProgram.Text = " ";
                programnames.Add("Manufacturing Plant");
                programnames.Add("Animal Production");
                programnames.Add("Agribusiness");
                
            }
            else if (e.Value.ToString() == "Faculty of Applied Sciences")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Architecture");
                programnames.Add("Civil Engineering");
                programnames.Add("Mechatronics");
                programnames.Add("Economics Engineering");
                programnames.Add("Geodesy and Geoinformatics");
                programnames.Add("Transportation and Traffic Engineering");
               

            }
            else if (e.Value.ToString() == "Faculty of Arts")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Figurative Art");
                programnames.Add("Art of Music");
                programnames.Add("Dramatic Arts");
               

            }
            else if (e.Value.ToString() == "Faculty of Business Administration")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Business Administration");
                programnames.Add("Public Administration");
            }
            else if (e.Value.ToString() == "Faculty of Economics")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Marketing - Management");
                programnames.Add("Economics and Business");
                programnames.Add("Finance and Accounting");
                programnames.Add("Tourism");
                programnames.Add("International Business");
               
            }
            else if (e.Value.ToString() == "Faculty of Food Technology and Nutrition")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Food Technology");
                programnames.Add("Food Quality and Safety Management");
                programnames.Add("Nutrition");
            }
            else if (e.Value.ToString() == "Faculty of Law")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Law Studies");
                programnames.Add("Journalism");
                programnames.Add("Political Sciences");
                programnames.Add("Criminology");
            }
            else if (e.Value.ToString() == "Faculty of Medical Sciences")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("General Medicine");
                programnames.Add("Dentistry");
                programnames.Add("Pharmacy");
                programnames.Add("General Nursing");
                programnames.Add("Obstetrics");
                programnames.Add("Physiotherapy");
            }
            else if (e.Value.ToString() == "Faculty of Natural Sciences and Mathematics")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Informatics");
                programnames.Add("Business Informatics");
                programnames.Add("Computer Technology and Telecommuncations");
                programnames.Add("Biology");
                programnames.Add("Geography");
                programnames.Add("Ecology");
                programnames.Add("Physics");
                programnames.Add("Physics - Chemistry");
                programnames.Add("Mathematics");
                programnames.Add("Financial Mathematics");
                programnames.Add("Chemistry");
            }
            else if (e.Value.ToString() == "Faculty of Pedagogy")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Teacher Education");
                programnames.Add("Preschool Education");
                programnames.Add("Professional Pedagogy");
                programnames.Add("Special Education and Rehabilitation");
            }
            else if (e.Value.ToString() == "Faculty of Philology")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Albanian Language and Literature");
                programnames.Add("English Language and Literature");
                programnames.Add("German Language and Literature");
                programnames.Add("Macedonian Language and Literature");
                programnames.Add("Romanian Philology");
            }
            else if (e.Value.ToString() == "Faculty of Philosophy")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("History");
                programnames.Add("Psychology");
                programnames.Add("Sociology");
                programnames.Add("Philosophy");
                programnames.Add("Oriental Philology");
                programnames.Add("Ethnology and Anthropology");
            }
            else if (e.Value.ToString() == "Faculty of Physical Education")
            {
                AutoCompleteProgram.Text = " ";
                programnames.Add("Physical Culture and Helath");
                programnames.Add("Sport");
            }
            AutoCompleteProgram.DataSource = programnames;
            InputProgram.InputView = AutoCompleteProgram;
        }

        private async void ComReg_Clicked(object sender, EventArgs e)
        {

            await firebaseHelper.AddStudent(email: auth.GetCurrentUserEmail(), name.Text, lastname.Text, indexnumber.Text, numericUpDown.Value.ToString(), comboboxS.SelectedItem.ToString(), comboboxf.SelectedItem.ToString(), AutoCompleteProgram.SelectedItem.ToString());
            //name.Text = string.Empty;
            //lastname.Text = string.Empty;
            //numericUpDown.Value = 0;
            //comboboxf.SelectedItem = string.Empty;
            //await DisplayAlert("Success", "Person Added Successfully", "OK");
            await Navigation.PushAsync(new Views.MainPageStudent());
        }

    }
}