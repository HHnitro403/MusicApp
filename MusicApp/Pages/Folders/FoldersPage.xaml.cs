using System.Windows.Input;
using CommunityToolkit.Maui.Storage;

using System.Collections.ObjectModel;

using System.Diagnostics;
using MusicApp.Classes;
using System.Threading.Tasks;

namespace MusicApp.Pages.Folders;

[QueryProperty(nameof(ParamName), "ParamName")]
public partial class FoldersPage : ContentPage
{
    private string _paramName;

    public string ParamName

    {
        get => _paramName;

        set

        {
            _paramName = value;

            OnPropertyChanged();
        }
    }

    public bool IsBackEnabled { get; set; } = true;

    public bool IsBackVisible { get; set; } = true;

    private readonly IFolderPicker _folderPicker;

    public ICommand UnifiedBackCommand { get; private set; }

    public FoldersPage(IFolderPicker folderPicker)
    {
        InitializeComponent();
        UnifiedBackCommand = new Command(HandleBackCommand);
        BindingContext = this; // Set the binding context to this page
        _folderPicker = folderPicker;
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

    private async void SelectFolderButton_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            var folder = await FileSystemHelper.PickFolderAsync();

            if (folder == null)
            {
                throw new Exception("Folder Could not be selected!.");
            }

            var files = FileSystemHelper.GetFilesInDirectory(folder);

            FilesListView.ItemsSource = files;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}