using DailyCheck.Models;
using Lottie.Forms;
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
    public partial class CheckMarkPage : ContentPage
    {
        AnimationView LottieCheckMarkAnimation;
        CheckMark checkMark;

        public CheckMarkPage(CheckMark checkMark)
        {
            InitializeComponent();

            if(checkMark == null)
            {
                DisplayAlert("Error", "Ups... Something went wrong", "Ok");
                this.Content = null;
            }

            this.checkMark = checkMark;

            CheckMarkName.Text = checkMark.Name;
            CheckMarkDescription.Text = checkMark.Description;

            if (checkMark.IsClicked == true)
            {
                ButtonCheckMark.Source = "TickMark.png";
            }
            else
            {
                ButtonCheckMark.Source = "TickMarkOff.png";
            }

            LottieCheckMarkAnimation = lottieTick;
            LottieCheckMarkAnimation.IsEnabled = false;
        }
        private void CheckMark_Clicked(object sender, EventArgs e)
        {
            if(checkMark.IsClicked == false)
            {
                ButtonCheckMark.Source = "TickMark.png";

                LottieCheckMarkAnimation.IsEnabled = true;

                LottieCheckMarkAnimation.PlayAnimation();
                LottieCheckMarkAnimation.OnFinishedAnimation += LottieCheckMarkAnimation_OnFinishedAnimation;

                checkMark.SaveClick();
            }
        }
        private void LottieCheckMarkAnimation_OnFinishedAnimation(object sender, EventArgs e)
        {
            LottieCheckMarkAnimation.IsEnabled = false;
        }

        private void EditCheckMark_Button(object sender, EventArgs e)
        {
            EditCheckMarkPage editCheckMark = new EditCheckMarkPage(checkMark);
            Application.Current.MainPage = editCheckMark;
            editCheckMark = null;
        }

        private void CheckMarkMenu_Button(object sender, EventArgs e)
        {
            CheckMarkMenuPage menuCheckMark = new CheckMarkMenuPage(checkMark);
            Application.Current.MainPage = menuCheckMark;
            menuCheckMark = null;
        }
    }
}