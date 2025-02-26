namespace LocatorApp.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using Microsoft.Maui.Controls;
    using LocatorApp.Classes;


    public partial class GpsDevices : ContentPage
    {
        
       public GpsDeviceList _gpsDeviceList { get; set; }

        public GpsDevices()
        {
            InitializeComponent();
            _gpsDeviceList = new GpsDeviceList();
            BindingContext = _gpsDeviceList;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _gpsDeviceList.LoadDevicesAsync();
        }

        public async void AddDevSetVisible(object sender, EventArgs e)
        {
            popUpAddDev.IsVisible = true;
            await popUpAddDev.TranslateTo(0, 1, 1500, Easing.SinIn);
        }


        public void Submit(object sender, EventArgs e)
        {
            if (inputName.Text != null && inputID.Text != null)
            {
                string deviceName = inputName.Text;
                string deviceId = inputID.Text;
                _gpsDeviceList.AddDeviceAsync(deviceName, deviceId,0.0,0.0);

                inputID.Text = string.Empty;
                inputName.Text = string.Empty;

                popUpAddDev.Unfocus();
                popUpAddDev.IsVisible = false;
                HideKeyboard();
                
            }
            
        }


        public void CloseAddDev(object sender, EventArgs e)
        {
            inputName.Text = string.Empty;
            inputID.Text= string.Empty;
            popUpAddDev.IsVisible = false;
        }

        private void ToggleVisibility(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is GpsDevice device)
            {
                device.IsVisible = !device.IsVisible;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var id = button.CommandParameter as string;
            await Navigation.PushAsync(new MapPage(_gpsDeviceList.getGpsDevice(id)));


        }

        private async void DeleteDevice(object sender, EventArgs e)
        {
            var button = sender as Button;
            var id = button.CommandParameter as string;
            var device = _gpsDeviceList.GpsDev.FirstOrDefault(x => x.Id == id);
            if (device  != null)
            {
                await _gpsDeviceList.DeleteDeviceAsync(device);
            }
        }


        private void HideKeyboard()
        {
            if (inputName != null)
            {
                inputName.IsEnabled = false;  // Temporarily disable the Entry
                inputName.IsEnabled = true;   // Re-enable the Entry (forces keyboard close)
            }

            if (inputID != null)
            {
                inputID.IsEnabled = false;
                inputID.IsEnabled = true;
            }
        }
    }
}
