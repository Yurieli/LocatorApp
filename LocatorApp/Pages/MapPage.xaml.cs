namespace LocatorApp.Pages;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Xamarin.Essentials;


public partial class MapPage : ContentPage
{
    private MapSpan mapSpan;
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    private GeolocationRequest request;

    
    public MapPage(MapSpan mapSpan)
    {
        
        _cancelTokenSource = new CancellationTokenSource();
        request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
        this.mapSpan = mapSpan;
    }

    public async Task Main(View content)
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
                var map = new Microsoft.Maui.Controls.Maps.Map(mapSpan);
                Content = map;
            }
        }
        catch (FeatureNotSupportedException)
        {
            // Handle not supported on device exception
        }
        catch (FeatureNotEnabledException)
        {
            // Handle not enabled on device exception
        }
        catch (PermissionException)
        {
            // Handle permission exception
        }
        catch (Exception)
        {
            // Unable to get location
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
}
