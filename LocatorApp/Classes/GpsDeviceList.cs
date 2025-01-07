using System.Collections.ObjectModel;

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
            GpsDevice gpsDevice = new GpsDevice(name, id);

            GpsDev.Add(gpsDevice);
        }
    }
}
