using DailyCheck.Models;
using DailyCheck.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyCheck.ViewModels
{
    class CheckMarkStatsViewModel : INotifyPropertyChanged
    {
        public ICommand BackButtonClicked { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        private int allClicked;
        private int allUnclicked;

        public string AllClicked
        {
            get
            {
                return "Clicked: " + allClicked;
            }
        }
        public string AllUnclicked
        {
            get
            {
                return "Unclicked: " + allUnclicked;
            }
        }

        private int monthClicked;
       private int monthUnclicked;
        public string MonthClicked
        {
            get
            {
                return "Clicked: " + monthClicked;
            }
        }
        public string MonthUnclicked
        {
            get
            {
                return "Unclicked: " + monthUnclicked;
            }
        }

        private int weekClicked;
        private int weekUnclicked;
        public string WeekClicked
        {
            get
            {
                return "Clicked: " + weekClicked;
            }
        }
        public string WeekUnclicked
        {
            get
            {
                return "Unclicked: " + weekUnclicked;
            }
        }

        public CheckMarkStatsViewModel(CheckMark checkMark)
        {
            BackButtonClicked = new Command(BackClicked);

            using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
            {
                conn.CreateTable<DailyClick>();

                List<DateTime> clickedDates = new List<DateTime>();
                var firstClickedDate = conn.Query<DailyClick>("SELECT * FROM `DailyClick` WHERE `Name` = ? LIMIT 1", checkMark.Name);

                if(firstClickedDate.Count == 0)
                {
                    return;
                }

                Constants.SendMessage("DEBUG (DateTime.Now - firstClickedDate[0].Date).TotalDays: " + (DateTime.Now - firstClickedDate[0].Date).TotalDays);

                try
                {
                    var stocks = conn.Query<DailyClick>("SELECT * FROM `DailyClick` WHERE `Name` = ?", checkMark.Name);
                    foreach (var i in stocks)
                    {
                        if((DateTime.Now - firstClickedDate[0].Date).TotalDays < 7)
                        {

                            if ((DateTime.Now - i.Date).TotalDays < 7)
                            {
                                weekClicked++;
                                monthClicked++;
                                weekUnclicked = (int)(DateTime.Now - firstClickedDate[0].Date).TotalDays - weekClicked;
                                monthUnclicked = weekUnclicked;
                            }
                        }else if ((DateTime.Now - firstClickedDate[0].Date).TotalDays < 30)
                        {
                            if ((DateTime.Now - i.Date).TotalDays < 30)
                            {
                                monthClicked++;
                                monthUnclicked = (int)(DateTime.Now - firstClickedDate[0].Date).TotalDays - monthClicked;
                            }
                            if ((DateTime.Now - i.Date).TotalDays < 7)
                            {
                                weekClicked++;
                                weekUnclicked = 7 - weekClicked;
                            }
                        }
                        else
                        {
                            if((DateTime.Now - i.Date).TotalDays < 30)
                            {
                                monthClicked++;
                                monthUnclicked = 30 - monthClicked;
                            }
                            if ((DateTime.Now - i.Date).TotalDays < 7)
                            {
                                weekClicked++;
                                weekUnclicked = 7 - weekClicked;
                            }

                        }
                        //clickedDates.Add(new DateTime(i.Date.Year, i.Date.Month, i.Date.Day));
                        allClicked++;
                        allUnclicked = (int)(DateTime.Now - firstClickedDate[0].Date).TotalDays - allClicked;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        void BackClicked()
        {
            MainCarouselPage menuCheckMark = new MainCarouselPage();
            Application.Current.MainPage = menuCheckMark;
            menuCheckMark = null;
        }
    }
}
