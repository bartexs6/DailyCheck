using SQLite;
using System;
using System.Collections.Generic;

namespace DailyCheck.Models
{
    // Klasa operujaca na liscie check mark`ow
    class CheckMarkList
    {
        public static List<CheckMark> CheckMarks = new List<CheckMark>();

        public static void Initialize()
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
            {
                conn.CreateTable<CheckMark>();
                /*if (conn.Table<CheckMark>().ToList().Count == 0)
                {
                    CheckMark newCheckMark = new CheckMark(
                        name: "My check mark",
                        description: "Description"
                        );

                    conn.Insert(newCheckMark);
                }*/
                CheckMarks = conn.Table<CheckMark>().ToList();
                for (int i = 0; i < CheckMarks.Count; i++)
                {
                    if (CheckMarks[i].Name == null)
                    {
                        CheckMarks.Remove(CheckMarks[i]);
                    }
                }
                CheckDate();

                conn.Close();
            }
        }

        public static void Add(CheckMark checkMark)
        {

            CheckMarks.Add(checkMark);
            try
            {
                SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath);
                conn.Insert(checkMark);
                conn.Close();
            }
            catch (System.Exception)
            {
                throw new System.ArgumentException("Database connection problem");
            }
        }

        public static bool CheckDuplicate(string name)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
            {
                conn.CreateTable<CheckMark>();
                foreach (var i in conn.Table<CheckMark>().ToList())
                {
                    if (i.Name == name)
                    {
                        conn.Close();
                        return true;
                    }
                }
                conn.Close();
                return false;
            }
        }

        private static void CheckDate()
        {
            foreach (var CheckMark in CheckMarks)
            {
                if (DateTime.Today - CheckMark.LastClickedDate.Date >= TimeSpan.FromDays(1))
                {
                    CheckMark.IsClicked = false;
                    using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                    {
                        conn.CreateTable<CheckMark>();

                        const string command = "UPDATE `CheckMark` SET `IsClicked`= false WHERE `Name`= ? ";
                        conn.CreateCommand(command, new object[] { CheckMark.Name }).ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
        }

    }
}
