using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace LocatorApp.Classes
{
    public class GpsDeviceList
    {
        public ObservableCollection<GpsDevice> GpsDev { get; set; }
        
        public GpsDeviceList()
        {
            GpsDev = new ObservableCollection<GpsDevice>();
            LoadDevices();

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

        public void SaveDevices()
        {
            string json = JsonSerializer.Serialize(GpsDev);
            Preferences.Set("SavedDevices", json);
        }

        public void LoadDevices()
        {
            if (Preferences.ContainsKey("SavedDevices"))
            {
                string json = Preferences.Get("SavedDevices", string.Empty);
                var devices = JsonSerializer.Deserialize<ObservableCollection<GpsDevice>>(json);

                if (devices != null)
                {
                    GpsDev.Clear();
                    foreach (var device in devices)
                    {
                        GpsDev.Add(device);
                    }
                }
            }
        }


    }
}

//48.73350406686496, 21.24905573164951