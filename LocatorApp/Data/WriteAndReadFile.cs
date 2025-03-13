using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocatorApp.Classes;

namespace LocatorApp.Data
{
    public static class WriteAndReadFile
    {
        public static void WriteToFile(GpsDeviceList deviceList)
        {
            ArgumentNullException.ThrowIfNull(deviceList);

            var content = ConvertJsonAndData.DataToJson(deviceList);
            File.WriteAllText("\\Android\\data\\com.companyname.locatorapp\\files\\Devices.json", content);
        }

        public static GpsDeviceList? ReadFromFile()
        {
            string content = string.Empty;
            try
            {
                content = File.ReadAllText("Devices.json");
            }
            catch {
                return null;
            }
            
            return ConvertJsonAndData.JsonToData(content);
        }
    }
}
