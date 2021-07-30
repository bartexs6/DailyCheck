using SQLite;
using System;

namespace DailyCheck.Models
{
    // Glowna klasa check mark`a
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

        // Pusty konstruktor wymagany do polaczenia z baza danych
        public CheckMark() { }

        // Wlasciwy konstruktor
        public CheckMark(string name, string description)
        {
            if (CheckMarkList.CheckDuplicate(name) == false)
            {
                try
                {
                    Name = name;
                    Description = description;
                    IsClicked = false;

                    CheckMarkList.Add(this);
                }
                catch (ArgumentException error)
                {
                    Constants.SendMessage("Database connection problem", Constants.MessageType.Error);
                    throw error;
                }
            }
            else
            {
                Constants.SendMessage("There is other check mark with the same name");
            }
        }

        public void EditCheckMark(string name, string description)
        {
            if (name != Name)
            {
                if (CheckMarkList.CheckDuplicate(name) == true)
                {
                    Constants.SendMessage("There is other check mark with the same name");
                    return;
                }

                using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                {
                    const string command = "UPDATE `CheckMark` SET `Name`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command, new object[] { name, Name }).ExecuteNonQuery();

                    const string command2 = "UPDATE `DailyClick` SET `Name`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command2, new object[] { name, Name }).ExecuteNonQuery();

                    conn.Close();
                }
                Name = name;
            }
            if (description != Description)
            {
                using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                {
                    const string command = "UPDATE `CheckMark` SET `Description`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command, new object[] { description, Name }).ExecuteNonQuery();

                    conn.Close();
                }
                Description = description;
            }
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
                conn.CreateCommand(command, new object[] { IsClicked, Name }).ExecuteNonQuery();
                const string command2 = "UPDATE `CheckMark` SET `LastClickedDate`= ? WHERE `Name`= ?";
                conn.CreateCommand(command2, new object[] { LastClickedDate, Name }).ExecuteNonQuery();

                conn.Close();
            }
            catch (System.Exception)
            {
                Constants.SendMessage("Database connection problem", Constants.MessageType.Error);
                throw new System.ArgumentException("Database connection problem");
            }

            new DailyClick(this);
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

                    const string command2 = "DELETE FROM `DailyClick` WHERE `Name`= ?";
                    conn.CreateCommand(command2, new object[] { Name }).ExecuteNonQuery();

                    conn.Close();
                }

                CheckMarkList.CheckMarks.Remove(this);
            }
            catch (System.Exception)
            {
                // Niekrytyczny - Brak wyjatku
                Constants.SendMessage("Database connection problem", Constants.MessageType.Error);
                throw;
            }
        }

    }
}
