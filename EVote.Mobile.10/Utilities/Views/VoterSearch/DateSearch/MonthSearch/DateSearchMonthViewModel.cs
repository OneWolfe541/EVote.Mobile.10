using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Utilities.Commands;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{
    public class DateSearchMonthViewModel : VoterSearchViewModelBase
    {
        public DateSearchMonthViewModel(DateSearch date)
        {
            Type = "MONTH";

            BirthDate = date;
        }

        #region DateCommands
        private RelayCommand _monthCommand;
        public ICommand MonthCommand
        {
            get
            {
                if (_monthCommand == null)
                {
                    _monthCommand = new RelayCommand(param => this.SetMonthClick(param));
                }
                return _monthCommand;
            }
        }

        private void SetMonthClick(object month)
        {
            if (Int32.TryParse(month.ToString(), out int value))
            {
                ResetSelectedMonths();
                SetSelectedMonth(month.ToString());

                this.Month = month.ToString();
                RaisePropertyChanged("BirthDate");                
            }
        }
        #endregion

        #region SelectedMonth
        public bool JanuarySelected { get; set; }
        public bool FebruarySelected { get; set; }
        public bool MarchSelected { get; set; }
        public bool AprilSelected { get; set; }
        public bool MaySelected { get; set; }
        public bool JuneSelected { get; set; }
        public bool JulySelected { get; set; }
        public bool AugustSelected { get; set; }
        public bool SeptemberSelected { get; set; }
        public bool OctoberSelected { get; set; }
        public bool NovemberSelected { get; set; }
        public bool DecemberSelected { get; set; }

        private void SetSelectedMonth(string month)
        {
            switch(month)
            {
                case "01":
                    JanuarySelected = true;
                    break;
                case "02":
                    FebruarySelected = true;
                    break;
                case "03":
                    MarchSelected = true;
                    break;
                case "04":
                    AprilSelected = true;
                    break;
                case "05":
                    MaySelected = true;
                    break;
                case "06":
                    JuneSelected = true;
                    break;
                case "07":
                    JulySelected = true;
                    break;
                case "08":
                    AugustSelected = true;
                    break;
                case "09":
                    SeptemberSelected = true;
                    break;
                case "10":
                    OctoberSelected = true;
                    break;
                case "11":
                    NovemberSelected = true;
                    break;
                case "12":
                    DecemberSelected = true;
                    break;
            }
        }

        private void ResetSelectedMonths()
        {
            JanuarySelected = false;
            RaisePropertyChanged("JanuarySelected");
            FebruarySelected = false;
            RaisePropertyChanged("FebruarySelected");
            MarchSelected = false;
            RaisePropertyChanged("MarchSelected");
            AprilSelected = false;
            RaisePropertyChanged("AprilSelected");
            MaySelected = false;
            RaisePropertyChanged("MaySelected");
            JuneSelected = false;
            RaisePropertyChanged("JuneSelected");
            JulySelected = false;
            RaisePropertyChanged("JulySelected");
            AugustSelected = false;
            RaisePropertyChanged("AugustSelected");
            SeptemberSelected = false;
            RaisePropertyChanged("SeptemberSelected");
            OctoberSelected = false;
            RaisePropertyChanged("OctoberSelected");
            NovemberSelected = false;
            RaisePropertyChanged("NovemberSelected");
            DecemberSelected = false;
            RaisePropertyChanged("DecemberSelected");
        }
        #endregion
    }
}
