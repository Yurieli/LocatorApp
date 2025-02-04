namespace LocatorApp.Classes
{
    public class GpsDevice
    {
        
        public string Id { get; set; }
        public string Name { get; set; }

        public double GpsLatitude { get; set; } //sirka
        public double GpsLongitude { get; set; } //dlzka;


        public string CombinedText => $"NAME: {Name}    ID: {Id}";
        public GpsDevice(string name, string id, double gpsLatitude, double gpsLongtitude)
        {
            Name = name;
            Id = id;
            GpsLatitude = gpsLatitude;
            GpsLongitude = gpsLongtitude;

        }
    }
}
