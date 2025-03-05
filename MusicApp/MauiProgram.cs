using Microsoft.Extensions.Logging;
using MauiIcons.Core;
using MauiIcons.Fluent;
using MauiIcons.Material;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using MusicApp.Classes;

namespace MusicApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
#pragma warning disable CA1416 // Validar a compatibilidade da plataforma
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiIconsCore(x =>
            {
                // Set default icon size

                x.SetDefaultIconSize(24.0);

                // Enable font override if needed

                x.SetDefaultFontOverride(true);
            })

            // Initialize Material icons

            .UseMaterialMauiIcons()
            .UseFluentMauiIcons()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .UseMauiIconsCore(); // For Cupertino icons
                                 // Register the FolderPicker as a singleton
        builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);
        builder.Services.AddSingleton<IPermissionService, PermissionService>();

#pragma warning restore CA1416 // Validar a compatibilidade da plataforma

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}