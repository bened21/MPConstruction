using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPConstruction.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentPage : ContentPage
    {
        public EquipmentPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!String.IsNullOrEmpty(imageUrl.Text))
            {
                var fileBytes = File.ReadAllBytes(imageUrl.Text);

                var imageSource = ImageSource.FromStream(() => new MemoryStream(fileBytes));

                image.Source = imageSource;
            }
        }

        public async Task<ImageSource> TakePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable ||
                    !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "Sorry! No camera available.", "OK");
                return null;
            }

            var isPermissionGranted = await RequestCameraAndGalleryPermissions();
            if (!isPermissionGranted)
                return null;

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MPConstruction",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                MaxWidthHeight = 1000,
            });

            if (file == null)
                return null;

            imageUrl.Text = file.Path.ToString();

            var imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            return imageSource;
        }

        public async Task<ImageSource> SelectPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", "Sorry! Permission not granted to photos.", "OK");
                return null;
            }

            var isPermissionGranted = await RequestCameraAndGalleryPermissions();
            if (!isPermissionGranted)
                return null;

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });

            if (file == null)
                return null;

            imageUrl.Text = file.Path.ToString();

            var imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            return imageSource;
        }

        private async Task<bool> RequestCameraAndGalleryPermissions()
        {
            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            var photosStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var cameraResult = await Permissions.RequestAsync<Permissions.Camera>();
                var storageResult = await Permissions.RequestAsync<Permissions.StorageRead>();
                var photosResult = await Permissions.RequestAsync<Permissions.Photos>();

                return (
                    cameraResult != PermissionStatus.Denied &&
                    storageResult != PermissionStatus.Denied &&
                    photosResult != PermissionStatus.Denied);
            }

            return true;
        }

        private async void AddPhoto_Tapped(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Add a photo", "Cancel", null, "Take photo", "Select from gallery");

            if (action == "Take photo")
            {
                var result = await TakePhoto();
                if (result != null)
                    image.Source = result;
            }
            else if (action == "Select from gallery")
            {
                var result = await SelectPhoto();
                if (result != null)
                    image.Source = result;
            }

            return;
        }

    }
}