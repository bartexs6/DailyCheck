using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DailyCheck.Models
{
    class Constants
    {
        public const string DatabaseFilename = "DailyCheck.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        public enum MessageType
        {
            Info, Error
        }

        public static void SendMessage(string message, MessageType messageType = MessageType.Info)
        {
            switch (messageType)
            {
                case MessageType.Info:
                    App.Current.MainPage.DisplayAlert("Info", message, "OK");
                    break;
                case MessageType.Error:
                    App.Current.MainPage.DisplayAlert("Ups...", message, "OK");
                    break;
                default:
                    break;
            }
        }
    }
}
