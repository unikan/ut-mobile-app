using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UtMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Library : ContentPage
    {
        readonly Extensions.LoadLibrary libraryHelper = new Extensions.LoadLibrary();
        public Library()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    LibraryList.ItemsSource = await libraryHelper.DeserializeLibraryJsonAsync("https://spreadsheets.google.com/feeds/list/1kOgielTRk7gAKvhIcCBHnzfYcr3NmqvBT2uSsLPwsa8/1/public/values?alt=json");
                    LibraryContent.IsVisible = true;
                    NoInternetContent.IsVisible = false;
                }
                else
                {
                    LibraryContent.IsVisible = false;
                    NoInternetContent.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Warning", e.Message, "OK");
            }

            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;
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
                    LibraryList.ItemsSource = await libraryHelper.DeserializeLibraryJsonAsync("https://spreadsheets.google.com/feeds/list/1kOgielTRk7gAKvhIcCBHnzfYcr3NmqvBT2uSsLPwsa8/1/public/values?alt=json");
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