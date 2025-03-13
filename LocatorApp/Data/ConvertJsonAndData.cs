using System.Collections.ObjectModel;
using System.Text.Json;
using LocatorApp.Classes;

namespace LocatorApp.Data
{
    public static class ConvertJsonAndData
    {
        public static string DataToJson(GpsDeviceList deviceList) =>
            JsonSerializer.Serialize(deviceList.GpsDev);

        public static GpsDeviceList JsonToData(string json)
        {
            var devices = JsonSerializer.Deserialize<ObservableCollection<GpsDevice>>(json);
            if (devices == null)
            {
                throw new Exception("Could not retrieve list of devices");
            }
            var output = new GpsDeviceList();
            output.GpsDev = devices;

            return output;
        }
            
    }
}