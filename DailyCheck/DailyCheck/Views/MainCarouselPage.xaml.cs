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
    public partial class MainCarouselPage : CarouselPage
    {
        public MainCarouselPage()
        {
            InitializeComponent();

            foreach (var CheckMark in CheckMarkList.CheckMarks)
            {
                var newCheckMark = new CheckMarkPage(CheckMark);
                Children.Add(newCheckMark);
            }

             var lastPage = new NewCheckMarkPage();
             Children.Add(lastPage);
        }
    }
}