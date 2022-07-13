using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EVote.Factories;
using EVote.Utilities.Commands;
using EVote.Utilities.Extensions;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{ 
    public class Years
    {
        public int Year { get; set; }
        public int Selected { get; set; }

        public Years(int value, int selected)
        {
            Year = value;
            Selected = selected;
        }
    }

    public class DateSearchYearViewModel : VoterSearchViewModelBase
    {
        public ObservableCollection<Years> YearsList { get; set; }

        public DateSearchYearViewModel(DateSearch date)
        {
            Type = "YEAR";

            BirthDate = date;

            if (Year != null)
            {
                if (Int32.TryParse(Year.ToString(), out int value))
                {
                    selectedYear = value;
                }
            }

            SetYears(selectedYear);

            //_canClickButton = true;
        }

        private void SetYears(string selected)
        {
            if (selected != null)
            {
                if (Int32.TryParse(selected.ToString(), out int value))
                {
                    SetYears(value);
                }
            }
        }
        private async void SetYears(int selected)
        {
            YearsList = new ObservableCollection<Years>();

            List<int> yearList;
            if (!BirthDate.Month.IsNullOrEmpty() && !BirthDate.Day.IsNullOrEmpty())
            {
                OfflineFactory factory = new OfflineFactory();
                yearList = await factory.SearchVoterYears(BirthDate);
            }
            else
            {
                yearList = Enumerable.Range(1900, (DateTime.Now.Year - 18) - 1900 + 1).ToList();
            }

            foreach(var year in yearList)
            {
                Years years = new Years(year, selected);
                YearsList.Add(years);
            }

            RaisePropertyChanged("YearsList");
        }

        public int selectedYear { get; set; }
        private Years _selectedYear;
        public Years SelectedYear
        {
            get
            {
                return YearsList.Where(yl => yl.Selected == yl.Year).FirstOrDefault();
            }

            set
            {
                _selectedYear = value;
                RaisePropertyChanged("SelectedYear");
            }
        }

        #region DateCommands
        private RelayCommand _yearCommand;
        public ICommand YearCommand
        {
            get
            {
                if (_yearCommand == null)
                {
                    _yearCommand = new RelayCommand(param => this.SetYearClick(param));
                }
                return _yearCommand;
            }
        }

        // Enable or Disable the Print Ballot Button
        //private bool _canClickButton;
        //public bool CanClickButton
        //{
        //    get { return _canClickButton; }
        //    internal set
        //    {
        //        _canClickButton = value;
        //        RaisePropertyChanged("CanClickButton");
        //    }
        //}

        private void SetYearClick(object year)
        {
            if (Int32.TryParse(year.ToString(), out int value))
            {
                try
                {
                    //CanClickButton = false;
                    this.Year = year.ToString();
                    SetYears(year.ToString());
                }
                catch
                {

                }
                //RaisePropertyChanged("YearsList");
                RaisePropertyChanged("BirthDate");
            }
        }
        #endregion
    }
}
