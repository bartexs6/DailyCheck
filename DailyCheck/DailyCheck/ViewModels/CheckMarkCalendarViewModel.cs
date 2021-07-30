using DailyCheck.Models;
using DailyCheck.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyCheck.ViewModels
{
    struct CheckMarkCalendar
    {
        public CheckMark OCheckMark { get; set; }

        

        public string OName
        {
            get
            {
                return OCheckMark.Name;
            }
        }

        public string OBackgroundColor
        {
            get
            {
                if (OCheckMark.IsClicked)
                {
                    return "Green";
                }
                else
                {
                    return "DarkRed";
                }
            }
        }
        public string OTextBackgroundColor
        {
            get
            {
                if (OCheckMark.IsClicked)
                {
                    return "LightGreen";
                }
                else
                {
                    return "IndianRed";
                }
            }
        }
    }

    class CheckMarkCalendarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackButtonClicked { get; }

        private ObservableCollection<CheckMarkCalendar> scrollCheckMarkList;
        public ObservableCollection<CheckMarkCalendar> ScrollCheckMarkList
        {
            get
            {
                return scrollCheckMarkList;
            }
            set
            {
                if (scrollCheckMarkList != value)
                {
                    scrollCheckMarkList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheckMark)));

                }
            }
        }
        public string BackgroundColor
        {
            get
            {
                if (CheckMark.IsClicked)
                {
                    return "Green";
                }
                else
                {
                    return "DarkRed";
                }
            }
        }

        public string TextBackgroundColor
        {
            get
            {
                if (CheckMark.IsClicked)
                {
                    return "LightGreen";
                }
                else
                {
                    return "IndianRed";
                }
            }
        }

        public string Today
        {
            get
            {
                return DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.Date.ToShortDateString();
            }
        }

        public List<DateTime> doneChecks;

        public List<DateTime> DoneChecks
        {
            get
            {
                return doneChecks;
            }
            set
            {
                doneChecks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DoneChecks)));
            }
        }

        private CheckMark checkMark;
        public CheckMark CheckMark
        {
            get
            {
                return checkMark;
            }
            set
            {
                checkMark = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheckMark)));
            }
        }

        private DateTime minDate;
        public DateTime MinDate
        {
            get
            {
                return minDate;
            }
            set
            {
                minDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinDate)));
                
            }
        }

        public string Month
        {
            get
            {
                return DateTime.Now.ToString("MMMM");
            }
        }

        private DateTime maxDate;
        public DateTime MaxDate
        {
            get
            {
                return maxDate;
            }
            set
            {
                maxDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxDate)));
            }
        }

        public CheckMarkCalendarViewModel(CheckMark checkMark)
        {
            CheckMark = checkMark;

            BackButtonClicked = new Command(BackClicked);

            ScrollCheckMarkList = new ObservableCollection<CheckMarkCalendar>();

            foreach (var item in CheckMarkList.CheckMarks)
            {
                if(item != checkMark)
                {
                    CheckMarkCalendar addCheckMark = new CheckMarkCalendar();
                    addCheckMark.OCheckMark = item;
                    ScrollCheckMarkList.Add(addCheckMark);
                }
            }


            MinDate = new DateTime(2021, 1, 1);
            MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
            {
                conn.CreateTable<DailyClick>();

                List<DateTime> clickedDates = new List<DateTime>();
                try
                {
                    var stocks = conn.Query<DailyClick>("SELECT * FROM `DailyStats` WHERE `Name` = ?", this.checkMark.Name);
                    foreach (var i in stocks)
                    {
                        clickedDates.Add(new DateTime(i.Date.Year, i.Date.Month, i.Date.Day));
                    }
                }
                catch (Exception)
                {
                }

                if (clickedDates.Count != 0)
                {
                    DoneChecks = clickedDates;
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
