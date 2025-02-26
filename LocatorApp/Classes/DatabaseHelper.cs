using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace LocatorApp.Classes
{
    internal class DatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseHelper()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "devices.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<GpsDevice>().Wait();
        }
        
        // Add device
        public Task<int> AddDeviceAsync(GpsDevice device)
        {
            return _database.InsertAsync(device);
        }

        // Get all devices
        public Task<List<GpsDevice>> GetDevicesAsync()
        {
            return _database.Table<GpsDevice>().ToListAsync();
        }

        // Update a device
        public Task<int> UpdateDeviceAsync(GpsDevice device)
        {
            return _database.UpdateAsync(device);
        }

        // Delete a device
        public Task<int> DeleteDeviceAsync(GpsDevice device)
        {
            return _database.DeleteAsync(device);
        }


    }
}
