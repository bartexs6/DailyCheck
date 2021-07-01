using DailyCheck.Models;
using DailyCheck.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyCheck
{
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDUwMTc5QDMxMzkyZTMxMmUzMFozaUNvODUzelF3S00zcnB4REw1dUdzN21ZekV3WUJBajFsTWE3VjNVOWM9");
            InitializeComponent();

            CheckMarkList.Initialize();

            MainPage = new MainCarouselPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
