using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Offices : ContentPage
    {
        public Offices()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Btn0Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:adem.beadini@unite.edu.mk"));
        }

        private void Btn1Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:ibrahim.neziri@unite.edu.mk"));
        }

        private void Btn2Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:hatibe.deari@unite.edu.mk"));
        }

        private void Btn3Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:imberjah.tairi@unite.edu.mk"));
        }

        private void Btn4Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:florian.nesimi@unite.edu.mk"));
        }

        private void Btn5Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:press@unite.edu.mk"));
        }

        private void Btn6Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:projects@unite.edu.mk"));
        }

        private void Btn7Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:zshi@unite.edu.mk"));
        }

        private void Btn8Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:evaluation@unite.edu.mk"));
        }

        private void Btn9Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:kushtrim.ahmeti@unite.edu.mk"));
        }

        private void Btn10Email_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:kushtrim.ahmeti@unite.edu.mk"));
        }


    }
}