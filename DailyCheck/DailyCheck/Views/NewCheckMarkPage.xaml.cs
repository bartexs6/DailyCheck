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
    public partial class NewCheckMarkPage : ContentPage
    {
        public NewCheckMarkPage()
        {
            InitializeComponent();
        }

        private void AddNewTickMark_Clicked(object sender, EventArgs e)
        {
            NewCheckMarkInputPage newCheckMarkInput = new NewCheckMarkInputPage();
            Application.Current.MainPage = newCheckMarkInput;
            newCheckMarkInput = null;
        }

    }
}