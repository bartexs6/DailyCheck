using DailyCheck.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DailyCheck.ViewModels
{
    class CheckMarkCalendarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

            MinDate = new DateTime(2021, 1, 1);
            MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
            {
                conn.CreateTable<DailyStats>();

                List<DateTime> clickedDates = new List<DateTime>();
                try
                {
                    var stocks = conn.Query<DailyStats>("SELECT * FROM `DailyStats` WHERE `Name` = ?", this.checkMark.Name);
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
    }
}
