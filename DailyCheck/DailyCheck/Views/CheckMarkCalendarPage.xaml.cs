using DailyCheck.Models;
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

			if(checkMark != null)
            {
				this.checkMark = checkMark;
            }
            else
            {
				DisplayAlert("Error", "Ups... Something went wrong", "Ok");
				return;
			}

			DateTime minDate = new DateTime(2021, 1, 1);
			calendar.MinDate = minDate;
			DateTime maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			calendar.MaxDate = maxDate;

			using (SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath))
			{
				conn.CreateTable<DailyStats>();

				List<DateTime> clicked_Dates = new List<DateTime>();
                try
                {
					var stocks = conn.Query<DailyStats>("SELECT * FROM `DailyStats` WHERE `Name` = ?", this.checkMark.Name);
					foreach (var i in stocks)
					{
						clicked_Dates.Add(new DateTime(i.Date.Year, i.Date.Month, i.Date.Day));
					}
                }
                catch (Exception)
                {
                }

				calendar.BlackoutDatesViewMode = BlackoutDatesViewMode.Stripes;
				if(clicked_Dates.Count != 0) { 
					calendar.BlackoutDates = clicked_Dates;
				}
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