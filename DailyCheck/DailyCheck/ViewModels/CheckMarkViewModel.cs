using DailyCheck.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyCheck.ViewModels
{
   public class CheckMarkViewModel : BindableObject
    {
        public ICommand CheckMarkClicked { get; }
        CheckMark CheckMark;

        public CheckMarkViewModel()
        {
            CheckMarkClicked = new Command(OnClicked);
        }

        void OnClicked()
        {

        }

    }
}
