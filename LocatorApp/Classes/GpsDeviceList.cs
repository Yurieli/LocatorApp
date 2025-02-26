using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace LocatorApp.Classes
{
    public class GpsDeviceList
    {
        public ObservableCollection<GpsDevice> GpsDev { get; set; }
        private readonly DatabaseHelper _dbHelper;
        
        public GpsDeviceList()
        {
            GpsDev = new ObservableCollection<GpsDevice>();
            _dbHelper = new DatabaseHelper();
            

        }

        public async Task LoadDeviceAsync()
        {
            var devices = await _dbHelper.GetDevicesAsync();
            GpsDev.Clear();

            foreach (var device in devices)
            {
                GpsDev.Add(device);
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




    }
}

//48.73350406686496, 21.24905573164951