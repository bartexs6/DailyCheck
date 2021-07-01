using DailyCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyCheck.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckMarkMenuPage : ContentPage
    {
        CheckMark checkMark;
        public CheckMarkMenuPage(CheckMark checkMark)
        {
            InitializeComponent();

            if (checkMark != null)
            {
                this.checkMark = checkMark;
            }
            else
            {
                DisplayAlert("Error", "Ups... Something went wrong", "Ok");
                return;
            }

            var tappedCommand = new Command(() =>
            {
                CheckMarkCalendarPage calendarCheckMark = new CheckMarkCalendarPage(checkMark);
                Application.Current.MainPage = calendarCheckMark;
                calendarCheckMark = null;
            });

            var tapGestureRecognizer = new TapGestureRecognizer { Command = tappedCommand };
            CalendarButton.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void Back_Button(object sender, EventArgs e)
        {
            MainCarouselPage menuCheckMark = new MainCarouselPage();
            Application.Current.MainPage = menuCheckMark;
            menuCheckMark = null;
        }
    }
}