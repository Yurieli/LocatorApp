using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LocatorApp.Classes
{
    public class GpsDevice : INotifyPropertyChanged
    {
        
        public string Id { get; set; }
        public string Name { get; set; }

        public double GpsLatitude { get; set; } //sirka
        public double GpsLongitude { get; set; } //dlzka;

        [JsonIgnore]
        public string CombinedText => $"NAME: {Name}    ID: {Id}";


        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }


        public GpsDevice() { }
        public GpsDevice(string name, string id, double gpsLatitude, double gpsLongtitude)
        {
            Name = name;
            Id = id;
            GpsLatitude = gpsLatitude;
            GpsLongitude = gpsLongtitude;

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
