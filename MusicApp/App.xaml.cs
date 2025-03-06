using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using MusicApp.Services;

namespace MusicApp
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;

        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            _databaseService = databaseService;

            // Initialize database on startup
            InitializeDatabaseAsync();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        private async void InitializeDatabaseAsync()
        {
            // This will create the database if it doesn't exist
            await _databaseService.GetFoldersAsync();
        }
    }
}