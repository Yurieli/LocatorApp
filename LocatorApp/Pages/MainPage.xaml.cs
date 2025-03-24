
using Microsoft.Maui.Controls;
using LocatorApp.Classes;
using System;
using LocatorApp.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;

namespace LocatorApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private GpsDeviceList _gpsDeviceList;

        public MainPage()
        {
            InitializeComponent();
            _gpsDeviceList = new GpsDeviceList();
            LoadDevices();
        }

        private async void GoToDevicesPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GpsDevices());
        }

        private async void GoToAddDevice(object sender, EventArgs e)
        {
            var devicesPage = new GpsDevices();
            devicesPage.AddDevSetVisible(sender, e); // Open Add Device Popup
            await Navigation.PushAsync(devicesPage);
        }

        private async void GoToMap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

        private async void FindNearestDevice(object sender, EventArgs e)
        {
            LoadDevices();
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                await DisplayAlert("Error", "Could not get current location.", "OK");
                return;
            }

            var nearestDevice = _gpsDeviceList.GpsDev
                .OrderBy(d => GetDistance(location, d.GpsLatitude, d.GpsLongitude))
                .FirstOrDefault();

            if (nearestDevice != null)
            {
                NearestDeviceLabel.Text = $"Nearest Device: {nearestDevice.Name} (ID: {nearestDevice.Id})";
            }
            else
            {
                NearestDeviceLabel.Text = "No devices found.";
            }
        }

        

        private double GetDistance(Location userLocation, double devLat, double devLong)
        {
            return Location.CalculateDistance(userLocation, new Location(devLat, devLong), DistanceUnits.Kilometers);
        }

        private void LoadDevices()
        {
            var input = WriteAndReadFile.ReadFromFile();

            if (input == null)
            {
                _gpsDeviceList = new GpsDeviceList();
            }
            else
            {
                _gpsDeviceList = input;
            }


            BindingContext = _gpsDeviceList;
        }
    }
}
