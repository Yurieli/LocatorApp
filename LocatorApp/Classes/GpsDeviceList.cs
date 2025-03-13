using System.Collections.ObjectModel;
using LocatorApp.Data;

namespace LocatorApp.Classes
{
    public class GpsDeviceList
    {
        public ObservableCollection<GpsDevice> GpsDev { get; set; }

        public GpsDeviceList()
        {
            GpsDev = new ObservableCollection<GpsDevice>();
        }
        public void GpsSubmit(string name, string id)
        {
            GpsDevice gpsDevice = new GpsDevice(name, id, 48.73350406686496, 21.24905573164951);

            GpsDev.Add(gpsDevice);

           
        }

        public void GpsDelete(string id)
        {
            GpsDevice gpsDevice = GpsDev.FirstOrDefault(x => x.Id == id);
            if (gpsDevice != null)
            {
                GpsDev.Remove(gpsDevice);
            }

        }

        public GpsDevice? getGpsDevice(string id)
        {
            return GpsDev.FirstOrDefault(gpsDevice => gpsDevice.Id == id);
        }
    }
}

//48.73350406686496, 21.24905573164951