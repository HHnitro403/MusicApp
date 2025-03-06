using MusicApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;
        private readonly string _databasePath;

        public DatabaseService()
        {
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "folders.db");
        }

        private async Task InitializeAsync()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_databasePath);
            await _database.CreateTableAsync<FolderInfo>();
        }

        public async Task<List<FolderInfo>> GetFoldersAsync()
        {
            await InitializeAsync();
            return await _database.Table<FolderInfo>().ToListAsync();
        }

        public async Task<int> SaveFolderAsync(FolderInfo folder)
        {
            await InitializeAsync();

            if (folder.Id != 0)
                return await _database.UpdateAsync(folder);
            else
                return await _database.InsertAsync(folder);
        }

        public async Task<int> DeleteFolderAsync(FolderInfo folder)
        {
            await InitializeAsync();
            return await _database.DeleteAsync(folder);
        }
    }
}
