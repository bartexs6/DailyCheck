using DailyCheck.Models;
using DailyCheck.ViewModels;
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

            BindingContext = new NewCheckMarkInputViewModel();
        }
    }
}