using LocatorApp.Classes;

namespace LocatorApp
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
        }
        protected override void OnStart()
        {
            var devicelist = new GpsDeviceList();
            devicelist.LoadDevices();
        }
        protected override void OnSleep()
        {
            var devicelist = new GpsDeviceList();
            devicelist.SaveDevices();
        }
        

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }


    }
}