using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Library : ContentPage
    {
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        readonly Extensions.LoadLibrary libraryHelper = new Extensions.LoadLibrary();
        Interface auth;
        private bool _firstAppeareance = true;

        public Library()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (_firstAppeareance)
            {
                _firstAppeareance = false;

                try
                {
                    await LoadLibrary();
                }
                catch (Exception e)
                {
                    await DisplayAlert("Warning", e.Message, "OK");
                }

                busyindicator.IsVisible = false;
                busyindicator.IsBusy = false;
            }
        }

        private async Task LoadLibrary()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            if (Application.Current.Properties.ContainsKey("LibraryData"))
            {
                LibraryList.ItemsSource = await libraryHelper.DeserializeLibraryJsonAsync("local");
            }
            else
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Get current user correct program spreadsheet
                    auth = DependencyService.Get<Interface>();
                    var currentUser = await firebaseHelper.GetPerson(auth.GetCurrentUserEmail());
                    var spreadsheetUrls = await firebaseHelper.GetUrls(currentUser.Program);

                    LibraryList.ItemsSource = await libraryHelper.DeserializeLibraryJsonAsync("internet", spreadsheetUrls.Library);
                }
                else
                {
                    LibraryContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }

            await busyindicator.FadeTo(0, 300, Easing.Linear);
            busyindicator.IsBusy = false;

            sw.Stop();
            await DisplayAlert("Time elapsed", sw.ElapsedMilliseconds.ToString(), "OK");
        }


        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (LibraryList.DataSource != null)
            {
                this.LibraryList.DataSource.Filter = FilterBooks;
                this.LibraryList.DataSource.RefreshFilter();
            }
        }

        private bool FilterBooks(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var books = obj as Models.LibraryJSON.Entry;
            if (books.Author.t.ToLower().Contains(searchBar.Text.ToLower())
                 || books.BookTitle.t.ToLower().Contains(searchBar.Text.ToLower())
                 || books.Year.t.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Get current user correct program spreadsheet
                    auth = DependencyService.Get<Interface>();
                    var currentUser = await firebaseHelper.GetPerson(auth.GetCurrentUserEmail());
                    var spreadsheetUrls = await firebaseHelper.GetUrls(currentUser.Program);

                    LibraryList.ItemsSource = await libraryHelper.DeserializeLibraryJsonAsync(spreadsheetUrls.Library);
                    LibraryContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    LibraryContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}