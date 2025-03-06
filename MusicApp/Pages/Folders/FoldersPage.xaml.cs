using System.Windows.Input;
using CommunityToolkit.Maui.Storage;

using System.Collections.ObjectModel;

using System.Diagnostics;
using MusicApp.Classes;
using System.Threading.Tasks;
using MusicApp.Services;
using MusicApp.Models;
using CommunityToolkit.Maui.Alerts;

namespace MusicApp.Pages.Folders;

public partial class FoldersPage : ContentPage
{

    private readonly DatabaseService _databaseService;
    private readonly IFolderPicker _folderPicker;
    private List<FolderInfo> _folders;

    public FoldersPage(DatabaseService databaseService, IFolderPicker folderPicker)
    {
        InitializeComponent();
        _databaseService = databaseService;
        _folderPicker = folderPicker;

        // Load folders when page appears
        this.Appearing += OnPageAppearing;

    }

    private async void OnPageAppearing(object sender, EventArgs e)
    {
        await LoadFoldersAsync();
    }

    private async Task LoadFoldersAsync()
    {
        _folders = await _databaseService.GetFoldersAsync();
        foldersListView.ItemsSource = _folders;
    }

    private async void HandleBackCommand()
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            await Shell.Current.CurrentPage.Navigation.PopToRootAsync();
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            await Shell.Current.CurrentPage.Navigation.PopToRootAsync();
        }
    }

    // Override this to handle hardware back button
    protected override bool OnBackButtonPressed()
    {
        HandleBackCommand();
        return true; // Prevents default back behavior
    }

    private async void OnDeleteFolderClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is FolderInfo folder)
        {
            bool confirm = await DisplayAlert("Confirm", $"Delete folder '{folder.Name}'?", "Yes", "No");

            if (confirm)
            {
                await _databaseService.DeleteFolderAsync(folder);
                await LoadFoldersAsync();
            }
        }
    }

    private async void OnSelectFolderClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await _folderPicker.PickAsync(CancellationToken.None);

            if (result.IsSuccessful)
            {
                var folderInfo = new FolderInfo
                {
                    Name = result.Folder.Name,
                    Path = result.Folder.Path,
                    DateAdded = DateTime.Now
                };

                await _databaseService.SaveFolderAsync(folderInfo);
                await LoadFoldersAsync();

                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    await DisplayAlert("Success", $"Folder saved: {folderInfo.Name}", "OK");
                }
                else
                {
                    await Toast.Make($"Folder saved: {folderInfo.Name}", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to select folder: {ex.Message}", "OK");
        }
    }

    private async void OnFolderTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is FolderInfo folder)
        {
            // Deselect the item
            ((ListView)sender).SelectedItem = null;

            // Navigate to the files page with the folder path
            await Shell.Current.GoToAsync($"{nameof(FolderFilespage)}?folderPath={Uri.EscapeDataString(folder.Path)}");

        }
    }

    
}
