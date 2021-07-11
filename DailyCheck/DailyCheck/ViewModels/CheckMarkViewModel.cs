using DailyCheck.Models;
using DailyCheck.Views;
using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyCheck.ViewModels
{
    public class CheckMarkViewModel : INotifyPropertyChanged
    {
        public ICommand CheckMarkClicked { get; }
        public ICommand EditCheckMarkClicked { get; }
        public ICommand CheckMarkMenuClicked { get; }
        public ICommand CalendarClicked { get; }

        public event PropertyChangedEventHandler PropertyChanged;


        private CheckMark checkMark;
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
        private bool playAnimation;
        public bool PlayAnimation
        {
            get
            {
                return playAnimation;
            }
            set
            {
                playAnimation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayAnimation)));
            }
        }

        private string checkMarkImage;
        public string CheckMarkImage
        {
            get
            {
                if (checkMarkImage == null)
                {
                    return "CheckMarkOff.png";
                }
                else
                {
                    return checkMarkImage;
                }
            }
            set
            {
                checkMarkImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheckMarkImage)));
            }
        }

        public CheckMarkViewModel(CheckMark checkMark)
        {
            CheckMark = checkMark;
            CheckMarkClicked = new Command(OnClicked);
            EditCheckMarkClicked = new Command(EditCheckMark_Button);
            CheckMarkMenuClicked = new Command(CheckMarkMenu_Button);
            CalendarClicked = new Command(Calendar_Button);

            if (checkMark.IsClicked)
            {
                CheckMarkImage = "CheckMark.png";
            }
        }

        private void OnClicked()
        {
            if (CheckMark.IsClicked == false)
            {
                PlayAnimation = true;
                CheckMarkImage = "CheckMark.png";

                CheckMark.SaveClick();
            }
        }

        private void EditCheckMark_Button()
        {
            EditCheckMarkPage editCheckMark = new EditCheckMarkPage(CheckMark);
            Application.Current.MainPage = editCheckMark;
        }

        private void CheckMarkMenu_Button()
        {
            CheckMarkMenuPage menuCheckMark = new CheckMarkMenuPage(CheckMark);
            Application.Current.MainPage = menuCheckMark;
        }

        private void Calendar_Button()
        {
            CheckMarkCalendarPage calendarCheckMark = new CheckMarkCalendarPage(CheckMark);
            Application.Current.MainPage = calendarCheckMark;
        }
    }

    public class StartLottieAnimationTriggerAction : TriggerAction<AnimationView>
    {
        protected override void Invoke(AnimationView sender)
        {
            sender.PlayAnimation();
        }
    }
    public class StopLottieAnimationTriggerAction : TriggerAction<AnimationView>
    {
        protected override void Invoke(AnimationView sender)
        {
            sender.Progress = 0;
            sender.PauseAnimation();
        }
    }
}
