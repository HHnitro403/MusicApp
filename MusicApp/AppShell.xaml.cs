namespace MusicApp;

using MusicApp.Pages.Songs;
using MusicApp.Pages.Artists;
using MusicApp.Pages.Folders;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(SongsPage), typeof(SongsPage));
        Routing.RegisterRoute(nameof(ArtistsPage), typeof(ArtistsPage));
        Routing.RegisterRoute(nameof(FoldersPage), typeof(FoldersPage));
        Routing.RegisterRoute(nameof(FolderFilespage), typeof(FolderFilespage));
    }
}