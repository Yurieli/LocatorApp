namespace LocatorApp.Pages
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Maui.Controls.Maps;
    using Microsoft.Maui.Maps;
    using Xamarin.Essentials;
    using Microsoft.Maui.Controls;
    using LocatorApp.Classes;

    public partial class MapPage : ContentPage
    {
        private MapSpan mapSpan;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        private GeolocationRequest request;
        private Microsoft.Maui.Controls.Maps.Map myMap;

        public MapPage()
        {
            InitializeComponent();
            _cancelTokenSource = new CancellationTokenSource();
            request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            myMap = new Microsoft.Maui.Controls.Maps.Map
            {

                IsShowingUser = true
            };
            Content = myMap; // Initialize map as the Content
        }
        public MapPage(GpsDevice gpsDevice)
        {
            InitializeComponent();
            _cancelTokenSource = new CancellationTokenSource();
            request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            myMap = new Microsoft.Maui.Controls.Maps.Map
            {
                
                IsShowingUser = true
            };
            Content = myMap; // Initialize map as the Content
            ShowDevice(gpsDevice);
        }

        public MapPage(MapSpan mapSpan) : this()
        {
            this.mapSpan = mapSpan;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await MainAsync();
        }

        [Obsolete]
        public async Task MainAsync()
        {
            try
            {
                _isCheckingLocation = true;

                // Ensure we have the required permissions
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                    {
                        // Permission denied, handle accordingly
                        return;
                    }
                }

                Location location = await Geolocation.GetLocationAsync(request, _cancelTokenSource.Token);
                
                if (location != null)
                {
                   
                    var mapLocation = ConvertToMauiLocation(location);
                    mapSpan = new MapSpan(mapLocation, 0.01, 0.01);

                    // Update the map with the user's current location
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        myMap.MoveToRegion(mapSpan);
                        myMap.IsShowingUser = true;
                        
                    });
                }
                else
                {
                    Console.WriteLine("Unable to get location.");
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle not supported on device exception
                Console.WriteLine($"Feature not supported: {ex.Message}");
            }
            catch (FeatureNotEnabledException ex)
            {
                // Handle not enabled on device exception
                Console.WriteLine($"Feature not enabled: {ex.Message}");
            }
            catch (PermissionException ex)
            {
                // Handle permission exception
                Console.WriteLine($"Permission denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine($"Unable to get location: {ex.Message}");
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public static implicit operator View(MapPage v)
        {
            throw new NotImplementedException();
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && !_cancelTokenSource.IsCancellationRequested)
                _cancelTokenSource.Cancel();
        }

        public static Microsoft.Maui.Devices.Sensors.Location ConvertToMauiLocation(Location xamarinLocation)
        {
            return new Microsoft.Maui.Devices.Sensors.Location(xamarinLocation.Latitude, xamarinLocation.Longitude);
        }

        public void ShowDevice(GpsDevice gpsdevice)
        {
            Location gpsLocation = new Location(gpsdevice.GpsLatitude, gpsdevice.GpsLongitude);

            var mauiLocation = ConvertToMauiLocation(gpsLocation);
            
            
            Pin pin = new Pin
            {
                Label = gpsdevice.Name,
                Type = PinType.Generic,
                Location = mauiLocation
            };
            myMap.Pins.Add(pin);
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(mauiLocation, Distance.FromKilometers(1)));
        }

        
    }
}
