using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SQLite;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LocatorApp.Classes
{
    public class GpsDeviceList
    {
        private readonly SQLiteAsyncConnection _database;
        private ObservableCollection<GpsDevice> _gpsDev;
        public ObservableCollection<GpsDevice> GpsDev
        {
            get => _gpsDev;
            set
            {
                _gpsDev = value;
                OnPropertyChanged();
            }
        }
        
        private readonly DatabaseHelper _dbHelper;
        
        public GpsDeviceList()
        {
            GpsDev = new ObservableCollection<GpsDevice>();
            try
            {
                _database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "GpsDevices.db3"));
                _database.CreateTableAsync<GpsDevice>().Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database Initialization Error: {ex.Message}");
            }


        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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


        public async Task AddDeviceAsync(string name, string id, double latitude, double longitude)
        {
            try
            {
                var device = new GpsDevice(name, Guid.NewGuid().ToString(), latitude, longitude);
                int result = await _database.InsertAsync(device);

                if (result > 0)
                {
                    Debug.WriteLine("Device successfully added.");
                }
                else
                {
                    Debug.WriteLine("Device insertion failed.");
                }

                await LoadDevicesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"InsertAsync Error: {ex.Message}");
            }
        }


        public async Task DeleteDeviceAsync(GpsDevice device)
        {
            await _database.DeleteAsync(device);
            await LoadDevicesAsync();
        }



    }
}

//48.73350406686496, 21.24905573164951