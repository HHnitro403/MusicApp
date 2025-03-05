namespace MusicApp.Components;

using MusicApp.Pages.Songs;
using MusicApp.Pages.Folders;
using MusicApp.Pages.Artists;
using CommunityToolkit.Maui.Storage;

public partial class TabBar : ContentView
{
    public TabBar()
    {
        InitializeComponent();
    }

    private async void MusicButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SongsPage));
    }

    private async void LibraryButton_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Alert", "You have been alerted", "OK");
    }

    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Alert", "You have been alerted", "OK");
    }

    private async void ArtistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ArtistsPage));
    }

    private async void FoldersButton_Clicked(object sender, EventArgs e)
    {
        var folderPicker = Handler.MauiContext.Services.GetService<IFolderPicker>();

        // Create navigation parameters with the folder picker

        var navigationParameters = new ShellNavigationQueryParameters

    {
        { "FolderPicker", folderPicker }
    };
        await Shell.Current.GoToAsync($"{nameof(FoldersPage)}?ParamName={navigationParameters}");
    }
}