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
    public partial class NewCheckMarkInputPage : ContentPage
    {
        public NewCheckMarkInputPage()
        {
            InitializeComponent();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            if (CheckMarkNameInput.Text == null)
            {
                DisplayAlert("Name", "Your check mark's name is too short", "Ok");
                return;
            }
            if (CheckMarkDescInput.Text == null)
            {
                DisplayAlert("Description", "Your check mark's description is too short", "Ok");
                return;
            }

            if (CheckMarkNameInput.Text == String.Empty || CheckMarkNameInput.Text.Length > 32)
            {
                DisplayAlert("Name", "Your check mark's name is too short or too long", "Ok");
                return;
            }

            if (CheckMarkDescInput.Text == String.Empty || CheckMarkDescInput.Text.Length > 32)
            {
                DisplayAlert("Name", "Your check mark's description is too short or too long", "Ok");
                return;
            }
            else
            {
                string CheckMarkText = CheckMarkNameInput.Text;
                string CheckMarkDesc = CheckMarkDescInput.Text;

                new CheckMark(CheckMarkText, CheckMarkDesc);

                MainCarouselPage mainPage = new MainCarouselPage();
                Application.Current.MainPage = mainPage;
            }
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            MainCarouselPage mainPage = new MainCarouselPage();
            Application.Current.MainPage = mainPage;
        }
    }
}