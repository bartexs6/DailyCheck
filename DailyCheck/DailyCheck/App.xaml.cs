
using DailyCheck.Models;
using DailyCheck.Views;
using Plugin.LocalNotification;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyCheck
{
    public partial class App : Application
    {
        public App()
        {
            //Rejestrowanie Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDUwMTc5QDMxMzkyZTMxMmUzMFozaUNvODUzelF3S00zcnB4REw1dUdzN21ZekV3WUJBajFsTWE3VjNVOWM9");
            InitializeComponent();

            CheckMarkList.Initialize();

            MainPage = new MainCarouselPage();
            Constants.SendMessage("Development release - " + AppInfo.VersionString + " " + AppInfo.PackageName + "\nUsing MVVM Pattern");
            //Notification();
        }


        async void Notification()
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Daily check",
                Description = "Have you done your daily tasks?",
                Schedule =
    {
        NotifyTime = DateTime.Now.Date.AddHours(19)
    }
            };
            await NotificationCenter.Current.Show(notification);
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
