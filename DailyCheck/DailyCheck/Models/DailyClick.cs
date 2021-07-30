using SQLite;
using System;

namespace DailyCheck.Models
{
    // Klasa odpowiedzialna za dzienne klikniecia

    class DailyClick
    {
        string name;
        DateTime date;

        public string Name { get => name; set => name = value; }
        public DateTime Date { get => date; set => date = value; }

        // Pusty konstruktor wymagany do polaczenia z baza danych
        public DailyClick()
        {
        }

        // Wlasciwy konstruktor
        public DailyClick(CheckMark checkMark)
        {
            name = checkMark.Name;
            date = DateTime.Now;

            using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
            {
                conn.CreateTable<DailyClick>();
                conn.Table<DailyClick>().Connection.Insert(this);
            }
        }
    }
}
