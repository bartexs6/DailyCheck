using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyCheck.Models
{
    public class CheckMark
    {
        private string name;
        private string description;
        private bool isClicked;
        private DateTime lastClickedDate;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool IsClicked { get => isClicked; set => isClicked = value; }
        public DateTime LastClickedDate { get => lastClickedDate; set => lastClickedDate = value; }

        public CheckMark()
        {
        }

        public CheckMark(string name, string description)
        {
            CheckMark newCheckMark = new CheckMark()
            {
                Name = name,
                Description = description,
                IsClicked = false
            };

            try
            {
                CheckMarkList.Add(newCheckMark);
            }
            catch (ArgumentException error)
            {
                App.Current.MainPage.DisplayAlert("Ups...", error.Message, "OK");
                throw;
            }
        }

        public bool EditCheckMark(string name, string description)
        {
            if (description != Description)
            {
                using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                {
                    conn.CreateTable<CheckMark>();

                    const string command = "UPDATE `CheckMark` SET `Description`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command, new object[] { description, Name }).ExecuteNonQuery();
                }
                Description = description;

            }
            if (name != Name)
            {
                if (CheckMarkList.CheckDuplicate(name) == true)
                {
                    return false;
                }

                using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                {
                    conn.CreateTable<CheckMark>();

                    const string command = "UPDATE `CheckMark` SET `Name`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command, new object[] { name, Name }).ExecuteNonQuery();
                }
                Name = name;
            }

            CheckMarkList.CheckMarks.Remove(this);
            CheckMarkList.CheckMarks.Add(this);

            return true;

        }

        public void SaveClick()
        {
            IsClicked = true;
            LastClickedDate = DateTime.Now;

            try
            {
                SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath);

                conn.CreateTable<CheckMark>();

                const string command = "UPDATE `CheckMark` SET `IsClicked`= ? WHERE `Name`= ?";
                conn.CreateCommand(command, new object[] { true, Name }).ExecuteNonQuery();
                const string command2 = "UPDATE `CheckMark` SET `LastClickedDate`= ? WHERE `Name`= ?";
                conn.CreateCommand(command2, new object[] { LastClickedDate, Name }).ExecuteNonQuery();

                conn.Close();
            }
            catch (System.Exception)
            {
                throw new System.ArgumentException("Database connection problem");
            }

            new DailyStats(this);
        }

        public void DeleteCheckMark()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                {
                    conn.CreateTable<CheckMark>();

                    const string command = "DELETE FROM `CheckMark` WHERE `Name`= ?";
                    conn.CreateCommand(command, new object[] { Name }).ExecuteNonQuery();

                    const string command2 = "DELETE FROM `DailyStats` WHERE `Name`= ?";
                    conn.CreateCommand(command2, new object[] { Name }).ExecuteNonQuery();
                }

                CheckMarkList.CheckMarks.Remove(this);
            }
            catch (System.Exception)
            {
                throw new System.ArgumentException("Database connection problem");
            }
        }
           
    }
}
