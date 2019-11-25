using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database;
using XamarinFirebase.Model;
using XamarinFirebase.Helper;

namespace DatabaseTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allRegistrations = await firebaseHelper.GetAllRegistrations();
            lstRegistrations.ItemsSource = allRegistrations;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddPerson(txtId.Text, txtName.Text, txtLast.Text, txtIndexNr.Text, txtSemestri.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Success", "Person Added Successfully", "OK");
            var allRegistrations = await firebaseHelper.GetAllRegistrations();
            lstRegistrations.ItemsSource = allRegistrations;
        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            var person = await firebaseHelper.GetPerson(txtId.Text);
            if (person != null)
            {
                txtId.Text = person.Email.ToString();
                txtName.Text = person.FirstName;
                await DisplayAlert("Success", "Person Retrive Successfully", "OK");

            }
            else
            {
                await DisplayAlert("Success", "No Person Available", "OK");
            }

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdatePerson(txtId.Text, txtIndexNr.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Success", "Person Updated Successfully", "OK");
            var allRegistrations = await firebaseHelper.GetAllRegistrations();
            lstRegistrations.ItemsSource = allRegistrations;
        }



        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.DeletePerson(txtId.Text);
            await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            var allRegistrations = await firebaseHelper.GetAllRegistrations();
            lstRegistrations.ItemsSource = allRegistrations;
        }

    }
}
