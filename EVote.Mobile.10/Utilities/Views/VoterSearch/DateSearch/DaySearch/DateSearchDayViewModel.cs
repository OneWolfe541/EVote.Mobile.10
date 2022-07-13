using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Utilities.Commands;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{
    public class DateSearchDayViewModel : VoterSearchViewModelBase
    {
        public bool ThirtyDayValid
        {
            get
            {
                if (BirthDate.Month == "2")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool ThirtyOneDayValid
        { 
            get
            {
                List<string> validDays = new List<string> { "1", "3", "5", "7", "8", "10", "12", "01", "03", "05", "07", "08" };
                if (validDays.Contains(BirthDate.Month))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }

        public DateSearchDayViewModel(DateSearch date)
        {
            Type = "DAY";

            BirthDate = date;
        }

        #region DateCommands
        private RelayCommand _dayCommand;
        public ICommand DayCommand
        {
            get
            {
                if (_dayCommand == null)
                {
                    _dayCommand = new RelayCommand(param => this.SetDayClick(param));
                }
                return _dayCommand;
            }
        }

        private void SetDayClick(object day)
        {
            if (Int32.TryParse(day.ToString(), out int value))
            {
                try
                {
                    this.Day = day.ToString();
                }
                catch
                {

                }
                RaisePropertyChanged("BirthDate");
            }
        }
        #endregion
    }
}
