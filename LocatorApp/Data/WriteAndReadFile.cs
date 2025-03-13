using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LocatorApp.Classes;

namespace LocatorApp.Data
{
    public static class WriteAndReadFile
    {
        private static string filePath = Path.Combine(FileSystem.AppDataDirectory, "Devices.json");
        public static void WriteToFile(GpsDeviceList deviceList)
        {
            ArgumentNullException.ThrowIfNull(deviceList);
            
            var content = ConvertJsonAndData.DataToJson(deviceList);
 
            File.WriteAllText(filePath, content);
        }

        public static GpsDeviceList? ReadFromFile()
        {
            string content = string.Empty;
            try
            {
                content = File.ReadAllText(filePath);
            }
            catch {
                return null;
            }
            
            return ConvertJsonAndData.JsonToData(content);
        }
    }
}
