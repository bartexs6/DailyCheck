using System;
using System.IO;

namespace DailyCheck.Models
{
    // Klasa zawierajaca stale oraz podstawowe funkcje
    class Constants
    {
        public const string DatabaseFilename = "DailyCheck.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
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
                    App.Current.MainPage.DisplayAlert("Information", message, "OK");
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
