using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using CommunityToolkit.Maui.Storage;

namespace MusicApp.Classes;

public static class FileSystemHelper
{
    public static async Task<string> PickFolderAsync()
    {
        try
        {
            var result = await FolderPicker.Default.PickAsync(default);
            if (result.IsSuccessful)
            {
                return result.Folder.Path;
            }
            else
            {
                Console.WriteLine($"Error: Could not pick folder: {result.Exception?.Message}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during folder picking: {ex.Message}");
            return null;
        }
    }

    public static List<string> GetFilesInDirectory(string dirPath)
    {
        List<string> fileList = new List<string>();

        try
        {
            if (Directory.Exists(dirPath))
            {
                foreach (string filePath in Directory.GetFiles(dirPath))
                {
                    fileList.Add(filePath);
                }

                if (fileList.Count == 0)
                {
                    throw new Exception("No files found in the directory.");
                }
            }
            else
            {
                Console.WriteLine($"Error: Directory '{dirPath}' not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return fileList;
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

    public static string GetAppCacheDirectory()
    {
        return FileSystem.Current.CacheDirectory;
    }

    public static string GetAppDataDirectory()
    {
        return FileSystem.Current.AppDataDirectory;
    }
}