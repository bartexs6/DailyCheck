using DailyCheck.Views;
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

        public CheckMark() { }

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
                    Constants.SendMessage(error.Message, Constants.MessageType.Error);
                    throw;
                }
            }
            else
            {
                Constants.SendMessage("There is other check mark with the same name");
            }
        }

        public void EditCheckMark(string name, string description)
        {
            if(name != Name)
            {
                if(CheckMarkList.CheckDuplicate(name) == true)
                {
                    Constants.SendMessage("There is other check mark with the same name");
                    return;
                }

                using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                {
                    const string command = "UPDATE `CheckMark` SET `Name`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command, new object[] { name, Name }).ExecuteNonQuery();

                    const string command2 = "UPDATE `DailyStats` SET `Name`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command2, new object[] { name, Name }).ExecuteNonQuery();
                }
                Name = name;
            }
            if (description != Description)
            {
                using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
                {
                    const string command = "UPDATE `CheckMark` SET `Description`= ? WHERE `Name`= ?";
                    conn.CreateCommand(command, new object[] { description, Name }).ExecuteNonQuery();
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
                conn.CreateCommand(command, new object[] { true, Name }).ExecuteNonQuery();
                const string command2 = "UPDATE `CheckMark` SET `LastClickedDate`= ? WHERE `Name`= ?";
                conn.CreateCommand(command2, new object[] { LastClickedDate, Name }).ExecuteNonQuery();

                conn.Close();
            }
            catch (System.Exception)
            {
                Constants.SendMessage("Database connection problem", Constants.MessageType.Error);
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
                Constants.SendMessage("Database connection problem", Constants.MessageType.Error);
                //throw new System.ArgumentException("Database connection problem");
                throw;
            }
        }
           
    }
}
