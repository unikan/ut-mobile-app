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
using Xamarin.Essentials;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NewUserData : ContentPage
    {

        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        readonly Interface auth;
        readonly Extensions.Helper helper = new Extensions.Helper();

        public NewUserData()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            auth = DependencyService.Get<Interface>();
            string email = auth.GetCurrentUserEmail().ToString();
            string indexNum = new string(email.Where(Char.IsDigit).ToArray());

            IndexNumberInput.Text = indexNum.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                CompleteRegistrationContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            auth.SignOut();
            Navigation.PopToRootAsync();

            return true;
        }

        private void FacultyCombobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
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
                programnames.Add("Italian Language and Literature");
                programnames.Add("French Language and Literature");
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
            ProgramInput.InputView = AutoCompleteProgram;
        }

        private void NameInput_Completed(object sender, EventArgs e)
        {
            LastNameInput.Focus();
        }

        private void LastNameInput_Completed(object sender, EventArgs e)
        {
            SemesterCombobox.Focus();
        }

        private async void Btn_CompleteReg_Clicked(object sender, EventArgs e)
        {
            Syncfusion.XForms.Buttons.SfButton btn = sender as Syncfusion.XForms.Buttons.SfButton;
            helper.DisableButton(btn);

            try
            {
                if (helper.ValidateEntry(NameInput.Text) && helper.ValidateEntry(LastNameInput.Text) && helper.ValidateEntry(SemesterCombobox.Text) && helper.ValidateEntry(FacultyCombobox.Text) && helper.ValidateEntry(AutoCompleteProgram.Text))
                {
                    await firebaseHelper.AddStudent(email: auth.GetCurrentUserEmail(), NameInput.Text, LastNameInput.Text, IndexNumberInput.Text, GroupsNumericUpDown.Value.ToString(), SemesterCombobox.SelectedItem.ToString(), FacultyCombobox.SelectedItem.ToString(), AutoCompleteProgram.SelectedItem.ToString());
                    await Navigation.PushAsync(new Views.MainPageStudent());
                }
                else
                {
                    await DisplayAlert("Warning", "Please fill all the entries to continue.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }

            await helper.EnableButtonAfter2Sec(btn);
        }

        private void Reload_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                CompleteRegistrationContent.IsVisible = true;
                NoInternetContent.IsVisible = false;
            }
            else
            {
                CompleteRegistrationContent.IsVisible = false;
                NoInternetContent.IsVisible = true;
            }
        }
    }
}