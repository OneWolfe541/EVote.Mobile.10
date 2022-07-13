using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Utilities.Commands;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{
    public class DateSearchDisplayViewModel : VoterSearchViewModelBase
    {
        public string MonthDisplay { get; set; }
        public string DayDisplay { get; set; }
        public string YearDisplay { get; set; }

        public DateSearchDisplayViewModel(DateSearch date)
        {
            Type = "DISPLAY";

            if (date == null)
            {
                MonthDisplay = "MM";
                DayDisplay = "DD";
                YearDisplay = "YYYY";
            }
            else
            {
                MonthDisplay = date.Month ?? "MM";
                DayDisplay = date.Day ?? "DD";
                YearDisplay = date.Year ?? "YYYY";
            }
        }

        #region Commands
        private RelayCommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => this.SetSearchClick());
                }
                return _searchCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void SetSearchClick()
        {
            
            RaisePropertyChanged("VoterSearch");
        }

        private RelayCommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(param => this.ClearParametersClick());
                }
                return _clearCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void ClearParametersClick()
        {
            MonthDisplay = "MM";
            RaisePropertyChanged("MonthDisplay");
            DayDisplay = "DD";
            RaisePropertyChanged("DayDisplay");
            YearDisplay = "YYYY";
            RaisePropertyChanged("YearDisplay");

            RaisePropertyChanged("VoterClear");
        }

        private RelayCommand _scanCommand;
        public ICommand ScanCommand
        {
            get
            {
                if (_scanCommand == null)
                {
                    _scanCommand = new RelayCommand(param => this.ScanClick());
                }
                return _scanCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void ScanClick()
        {
            _voterSearch = null;

            NameFirst = null;
            RaisePropertyChanged("NameFirst");
            NameLast = string.Empty;
            RaisePropertyChanged("NameLast");
            BirthYear = null;
            RaisePropertyChanged("BirthYear");

            RaisePropertyChanged("VoterClear");
            RaisePropertyChanged("VoterScanSearch");
        }

        private RelayCommand _nameCommand;
        public ICommand NameCommand
        {
            get
            {
                if (_nameCommand == null)
                {
                    _nameCommand = new RelayCommand(param => this.NameClick());
                }
                return _nameCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void NameClick()
        {
            RaisePropertyChanged("VoterNameSearch");
        }
        #endregion

        #region DateCommands
        private RelayCommand _monthCommand;
        public ICommand MonthCommand
        {
            get
            {
                if (_monthCommand == null)
                {
                    _monthCommand = new RelayCommand(param => this.MonthClick());
                }
                return _monthCommand;
            }
        }

        private void MonthClick()
        {
            RaisePropertyChanged("MonthSearch");
        }

        private RelayCommand _dayCommand;
        public ICommand DayCommand
        {
            get
            {
                if (_dayCommand == null)
                {
                    _dayCommand = new RelayCommand(param => this.DayClick());
                }
                return _dayCommand;
            }
        }

        private void DayClick()
        {
            RaisePropertyChanged("DaySearch");
        }

        private RelayCommand _yearCommand;
        public ICommand YearCommand
        {
            get
            {
                if (_yearCommand == null)
                {
                    _yearCommand = new RelayCommand(param => this.YearClick());
                }
                return _yearCommand;
            }
        }

        private void YearClick()
        {
            RaisePropertyChanged("YearSearch");
        }
        #endregion
    }
}
