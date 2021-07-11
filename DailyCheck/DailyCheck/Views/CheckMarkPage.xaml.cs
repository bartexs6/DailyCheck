using DailyCheck.Models;
using DailyCheck.ViewModels;
using Lottie.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyCheck.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckMarkPage : ContentPage
    {
        public CheckMarkPage(CheckMark checkMark)
        {
            InitializeComponent();

            if (checkMark == null)
            {
                DisplayAlert("Error", "Ups... Something went wrong", "Ok");
                this.Content = null;
            }
            else
            {
                BindingContext = new CheckMarkViewModel(checkMark);
            }
        }
    }
}