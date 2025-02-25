using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Data.SqlClient;

namespace LocatorApp.Classes
{
    public class GpsDevice : INotifyPropertyChanged
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("gpsLatitude")]
        public double GpsLatitude { get; set; } //sirka
        [JsonPropertyName("gpsLongtitude")]
        public double GpsLongitude { get; set; } //dlzka;

        private bool _isVisible;
        [JsonPropertyName("isVisible")]
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public string CombinedText => $"NAME: {Name}    ID: {Id}";
        public GpsDevice(string name, string id, double gpsLatitude, double gpsLongtitude)
        {
            Name = name;
            Id = id;
            GpsLatitude = gpsLatitude;
            GpsLongitude = gpsLongtitude;

        }

        public static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

            }
        }
    }
}
