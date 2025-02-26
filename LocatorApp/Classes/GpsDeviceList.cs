using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SQLite;
using System.Diagnostics;

namespace LocatorApp.Classes
{
    public class GpsDeviceList
    {
        private readonly SQLiteAsyncConnection _database;
        public ObservableCollection<GpsDevice> GpsDev { get; set; }
        private readonly DatabaseHelper _dbHelper;
        
        public GpsDeviceList()
        {
            GpsDev = new ObservableCollection<GpsDevice>();
            try
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "devices.db3");
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<GpsDevice>().Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database Initialization Error: {ex.Message}");
            }


        }

        public async Task LoadDevicesAsync()
        {
            try
            {
                var devices = await _database.Table<GpsDevice>().ToListAsync();
                GpsDev.Clear();
                foreach (var device in devices)
                {
                    GpsDev.Add(device);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadDevices Error: {ex.Message}");
            }   
        }
        public void GpsSubmit(string name, string id)
        {
            GpsDevice gpsDevice = new GpsDevice(name, id, 48.73350406686496, 21.24905573164951);

            GpsDev.Add(gpsDevice);
        }

        public GpsDevice getGpsDevice(string id)
        {
            return GpsDev.FirstOrDefault(gpsDevice => gpsDevice.Id == id);
        }


        public async Task AddDeviceAsync(string name,string id, double latitude, double longitude)
        {
            var newDevice = new GpsDevice(name,id , latitude, longitude);
            await _database.InsertAsync(newDevice);
            await LoadDevicesAsync(); // Refresh list
        }

        public async Task DeleteDeviceAsync(GpsDevice device)
        {
            await _database.DeleteAsync(device);
            await LoadDevicesAsync();
        }



    }
}

//48.73350406686496, 21.24905573164951