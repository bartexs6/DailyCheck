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
    class EditCheckMarkViewModel : INotifyPropertyChanged
    {
        public ICommand EditButtonClicked { get; }
        public ICommand DeleteButtonClicked { get; }
        public ICommand ContinueButtonClicked { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private CheckMark checkMark;

        string name, description;

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

        public EditCheckMarkViewModel(CheckMark checkMark)
        {
            CheckMark = checkMark;
            name = checkMark.Name;
            description = checkMark.Description;
            EditButtonClicked = new Command(EditButton_Clicked);
            DeleteButtonClicked = new Command(DeleteButton_Clicked);
            ContinueButtonClicked = new Command(ContinueButton_Clicked);
        }

        private void EditButton_Clicked()
        {
            if (name == null || description == null)
            {
                Constants.SendMessage("Your check mark's name or description is too short");
                return;
            }
            if (name == String.Empty || name.Length > 24 || description == String.Empty || description.Length > 24)
            {
                Constants.SendMessage("Your check mark's name or description name is too short or too long");
            }
            else
            {
                checkMark.EditCheckMark(name, description);

                MainCarouselPage mainPage = new MainCarouselPage();
                Application.Current.MainPage = mainPage;
            }
        }
        private void DeleteButton_Clicked()
        {
            try
            {
                checkMark.DeleteCheckMark();
            }
            catch (ArgumentException error)
            {
                App.Current.MainPage.DisplayAlert("Ups...", error.Message, "OK");
                throw;
            }

            MainCarouselPage mainPage = new MainCarouselPage();
            Application.Current.MainPage = mainPage;
        }

        private void ContinueButton_Clicked()
        {
            MainCarouselPage mainPage = new MainCarouselPage();
            Application.Current.MainPage = mainPage;
        }

    }
}
