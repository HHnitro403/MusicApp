using MusicApp.Classes;
using System.Diagnostics;

namespace MusicApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IPermissionService _permissionService;

        public MainPage(IPermissionService permissionService)
        {
            InitializeComponent();
            _permissionService = permissionService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnRequestPermissionsClicked(null, null);
        }

        private async void OnRequestPermissionsClicked(object sender, EventArgs e)
        {
            try
            {
                bool result = await _permissionService.CheckAndRequestPermissionsAgain();

                if (result)
                {
                    await DisplayAlert("Success", "All required permissions have been granted!", "OK");
                }
                else
                {
                    await DisplayAlert("Warning", "Some permissions were not granted. Certain features may not work properly.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during permission request: {ex.Message}");
                await DisplayAlert("Error", "An error occurred while requesting permissions. Please try again.", "OK");
            }
        }
    }
}