using DailyCheck.Models;
using DailyCheck.ViewModels;
using SQLite;
using Syncfusion.SfCalendar.XForms;
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
	public partial class CheckMarkCalendarPage : ContentPage
	{
		CheckMark checkMark;
		public CheckMarkCalendarPage(CheckMark checkMark)
		{
			InitializeComponent();

			if (checkMark == null)
			{
				DisplayAlert("Error", "Ups... Something went wrong", "Ok");
				this.Content = null;
			}
			else
			{
				BindingContext = new CheckMarkCalendarViewModel(checkMark);
			}
		}

		private void Back_Button(object sender, EventArgs e)
		{
			CheckMarkMenuPage menuCheckMark = new CheckMarkMenuPage(checkMark);
			Application.Current.MainPage = menuCheckMark;
			menuCheckMark = null;
		}
	}
}