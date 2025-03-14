﻿using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Data.SqlClient;
using SQLite;
using System.Runtime.CompilerServices;

namespace LocatorApp.Classes
{
    public class GpsDevice : INotifyPropertyChanged
    {
        [PrimaryKey,AutoIncrement]
        public string Id { get; set; }
        
        public string Name { get; set; }
       
        public double GpsLatitude { get; set; } //sirka
        
        public double GpsLongitude { get; set; } //dlzka;

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public string CombinedText => $"NAME: {Name}    ID: {Id}";

        public GpsDevice() { }
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
