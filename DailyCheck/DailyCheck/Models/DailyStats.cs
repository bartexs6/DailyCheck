using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyCheck.Models
{
    class DailyStats
    {
        CheckMark checkMark;
        string name;
        DateTime date;

        public string Name { get => name; set => name = value; }
        public DateTime Date { get => date; set => date = value; }

        public DailyStats()
        {
        }

        public DailyStats(CheckMark checkMark)
        {
            this.checkMark = checkMark;
            this.name = checkMark.Name;
            date = DateTime.Now;

            using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
            {
                conn.CreateTable<DailyStats>();
                conn.Table<DailyStats>().Connection.Insert(this);
            }
        }

    }
}
