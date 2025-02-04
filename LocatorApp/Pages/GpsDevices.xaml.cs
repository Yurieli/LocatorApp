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

        public async void AddDevSetVisible(object sender, EventArgs e)
        {
            popUpAddDev.IsVisible = true;
            await popUpAddDev.TranslateTo(0, 1, 1500, Easing.SinIn);
        }

        public async void DevInfoSetVisible(object sender, EventArgs e)
        {
            
        }

        public void Submit(object sender, EventArgs e)
        {
            if (inputName.Text != null && inputID.Text != null)
            {
                string deviceName = inputName.Text;
                string deviceId = inputID.Text;
                _gpsDeviceList.GpsSubmit(deviceName, deviceId);

                inputID.Text = string.Empty;
                inputName.Text = string.Empty;

                popUpAddDev.IsVisible = false;
            }
        }

        public void CloseAddDev(object sender, EventArgs e)
        {
            popUpAddDev.IsVisible = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var id = button.CommandParameter as string;
            await Navigation.PushAsync(new MapPage(_gpsDeviceList.getGpsDevice(id)));


        }
    }
}
