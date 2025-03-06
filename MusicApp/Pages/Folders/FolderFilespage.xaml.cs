
using MusicApp.Classes;
using MusicApp.Models;
using System.Collections.ObjectModel;
namespace MusicApp.Pages.Folders;

[QueryProperty(nameof(FolderPath), "folderPath")]
public partial class FolderFilespage : ContentPage
{

    private string _folderPath;
    public ObservableCollection<FileItem> Files { get; set; } = new ObservableCollection<FileItem>();

    public FolderFilespage()
	{
		InitializeComponent();
	}
   
    public string FolderPath
    {
        get => _folderPath;
        set
        {
            _folderPath = value;
            LoadFiles();
        }
    }

    private void LoadFiles()
    {
        if (string.IsNullOrEmpty(FolderPath))
            return;

        Files.Clear();

        // Use FileSystemHelper's method instead of the local one
        List<string> filePaths = FileSystemHelper.GetFilesInDirectory(FolderPath);

        // Use MainThread to update UI
        MainThread.BeginInvokeOnMainThread(() => {
            foreach (string filePath in filePaths)
            {
                try
                {
                    Files.Add(new FileItem
                    {
                        Name = Path.GetFileName(filePath),
                        Path = filePath,
                        Size = new FileInfo(filePath).Length,
                        LastModified = File.GetLastWriteTime(filePath)
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                }
            }

            // Update UI with folder name
            Title = Path.GetFileName(FolderPath);
            FilesColection.ItemsSource = Files;
        });
    }


    public static List<string> GetFilesInDirectoryAndSubdirectories(string dirPath)
    {
        List<string> files = new List<string>();
        try
        {
            files.AddRange(Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories));
        }
        catch (Exception excpt)
        {
            Console.WriteLine(excpt.Message);
        }
        return files;
    }
    
}