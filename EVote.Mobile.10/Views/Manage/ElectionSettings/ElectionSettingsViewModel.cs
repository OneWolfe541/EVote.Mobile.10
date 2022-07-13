using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Methods;
using EVote.Settings;
using EVote.Utilities.Commands;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class ElectionSettingsViewModel : ViewModelBase
    {
        public ElectionSettingsModel Settings
        {
            get
            {
                return AppSettings.Config;
            }
            set
            {
                AppSettings.Config = value;
                RaisePropertyChanged("Settings");
                RaisePropertyChanged("IsDistrictOnlyVoting");
            }
        }

        public bool? DistrictOnlyVoting
        {
            get
            {
                return AppSettings.Config.DistrictOnlyVoting;
            }
            set
            {
                AppSettings.Config.DistrictOnlyVoting = value;
                RaisePropertyChanged("DistrictOnlyVoting");
            }
        }

        public ElectionSettingsViewModel()
        {
            
        }

        #region Commands
        // Bound command for saving the config record
        public RelayCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => this.SaveClick());
                }
                return _saveCommand;
            }
        }

        //Enable or Disable the Save Button
        private bool _canSave;
        public bool CanSave
        {
            get { return _canSave; }
            internal set
            {
                _canSave = value;
                RaisePropertyChanged("CanSave");
            }
        }

        // Save the config record
        public void SaveClick()
        {
            AlertDialog connectionFailed = new AlertDialog("SETTINGS HAVE BEEN SAVED");
            connectionFailed.ShowDialog();

            ElectionConfigs.SaveAsync(AppSettings.Config);
        }

        // Bound command for navigating to the Valid Locations page
        public RelayCommand _locationsCommand;
        public ICommand LocationsCommand
        {
            get
            {
                if (_locationsCommand == null)
                {
                    _locationsCommand = new RelayCommand(param => this.LocationsClick());
                }
                return _locationsCommand;
            }
        }

        // Force base frame to navigate to the Valid Locations page
        public void LocationsClick()
        {
            Navigation.ValidLocations();
        }
        #endregion
    }
}
