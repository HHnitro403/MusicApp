namespace MusicApp.Pages.Songs;

using System.Windows.Input;

public partial class SongsPage : ContentPage
{
    public bool IsBackEnabled { get; set; } = true;
    public bool IsBackVisible { get; set; } = true;

    public ICommand UnifiedBackCommand { get; private set; }

    public SongsPage()
    {
        InitializeComponent();
        UnifiedBackCommand = new Command(HandleBackCommand);
        BindingContext = this; // Set the binding context to this page
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
}