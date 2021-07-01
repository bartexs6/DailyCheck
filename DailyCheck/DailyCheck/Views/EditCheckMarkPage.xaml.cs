using DailyCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyCheck.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCheckMarkPage : ContentPage
    {
        CheckMark checkMark = new CheckMark();
        public EditCheckMarkPage(CheckMark checkMark)
        {
            InitializeComponent();

            if (checkMark == null)
            {
                DisplayAlert("Error", "Ups... Something went wrong", "Ok");
                this.Content = null;
            }

            this.checkMark = checkMark;

            CheckMarkNameInput.Text = checkMark.Name;
            CheckMarkDescInput.Text = checkMark.Description;

        }
        private void EditButton_Clicked(object sender, EventArgs e)
        {
            if (CheckMarkNameInput.Text == null || CheckMarkDescInput.Text == null)
            {
                DisplayAlert("Name", "Your check mark's name or description is too short", "Ok");
                return;
            }
            if (CheckMarkNameInput.Text == String.Empty || CheckMarkNameInput.Text.Length > 24 || CheckMarkDescInput.Text == String.Empty || CheckMarkDescInput.Text.Length > 24)
            {
                DisplayAlert("Name", "Your check mark's name or description name is too short or too long", "Ok");
            }
            else
            {
                bool edited = checkMark.EditCheckMark(CheckMarkNameInput.Text, CheckMarkDescInput.Text);

                if (edited == false)
                {
                    DisplayAlert("Name", "There is other check mark with the same name", "Ok");
                }

                MainCarouselPage mainPage = new MainCarouselPage();
                Application.Current.MainPage = mainPage;
            }
        }
        private void DeleteButton_Clicked(object sender, EventArgs e)
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

        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            MainCarouselPage mainPage = new MainCarouselPage();
            Application.Current.MainPage = mainPage;
        }
    }
}