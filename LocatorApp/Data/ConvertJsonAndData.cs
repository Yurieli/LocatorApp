using System.Text.Json;
using LocatorApp.Classes;

namespace LocatorApp.Data
{
    public static class ConvertJsonAndData
    {
        public static string DataToJson(GpsDeviceList deviceList) =>
            JsonSerializer.Serialize(deviceList.GpsDev);

        public static GpsDeviceList JsonToData(string json) =>
            JsonSerializer.Deserialize<GpsDeviceList>(json) ?? new GpsDeviceList();
    }
}
