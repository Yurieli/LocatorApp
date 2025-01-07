namespace LocatorApp.Classes
{
    public class GpsDevice
    {
        
        public string Id { get; set; }
        public string Name { get; set; }

        public string CombinedText => $"NAME: {Name}    ID: {Id}";
        public GpsDevice(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
