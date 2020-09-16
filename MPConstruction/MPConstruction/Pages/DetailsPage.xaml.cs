using MPConstruction.Models;
using MPConstruction.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPConstruction.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private readonly IDataStore<Equipment> _dataStore;

        public DetailsPage(IDataStore<Equipment> dataStore)
        {
            _dataStore = dataStore;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (!String.IsNullOrEmpty(imagePath.Text))
            {
                var fileBytes = File.ReadAllBytes(imagePath.Text);

                var imageSource = ImageSource.FromStream(() => new MemoryStream(fileBytes));

                image.Source = imageSource;
            }

            base.OnAppearing();
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Item?", "Are you sure you want to delete this item?", "Yes", "No");

            if (answer)
            {
                await _dataStore.DeleteItemAsync(equipmentId.Text);
                await Navigation.PopAsync();
            }
        }
    }
}