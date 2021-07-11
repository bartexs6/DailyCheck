using DailyCheck.Models;
using DailyCheck.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyCheck.ViewModels
{
    class NewCheckMarkInputViewModel : INotifyPropertyChanged
    {
        public ICommand NewCheckMarkClicked { get; }
        public ICommand ExitClicked { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get
            {
                    return name;
            }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public NewCheckMarkInputViewModel()
        {
            NewCheckMarkClicked = new Command(NewCheckMark);
            ExitClicked = new Command(ExitButton);

        }

        private void NewCheckMark()
        {
            if (Name == null)
            {
                Constants.SendMessage("Your check mark's name is too short");
                return;
            }
            if (Description == null)
            {
                Constants.SendMessage("Your check mark's description is too short");
                return;
            }

            if (Name == String.Empty || Name.Length > 32)
            {
                Constants.SendMessage("Your check mark's name is too short or too long");
                return;
            }

            if (Description == String.Empty || Description.Length > 32)
            {
                Constants.SendMessage("Your check mark's description is too short or too long");
                return;
            }
            else
            {
                string CheckMarkText = Name;
                string CheckMarkDesc = Description;

                new CheckMark(CheckMarkText, CheckMarkDesc);

                MainCarouselPage mainPage = new MainCarouselPage();
                Application.Current.MainPage = mainPage;
            }
        }

        private void ExitButton()
        {
            MainCarouselPage mainPage = new MainCarouselPage();
            Application.Current.MainPage = mainPage;
        }
    }
}
