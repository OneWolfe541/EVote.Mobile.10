using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using EVote.Methods;
using EVote.Utilities.Commands;

namespace EVote.Utilities.Views
{
    public class MainHeaderViewModel : ViewModelBase
    {
        //private Window _parent; // Use for closing the current application

        public string HeaderText { get; set; }

        private string _locationName;
        public string LocationName 
        { 
            get
            {
                if (_locationName == null && AppSettings.User != null)
                {
                    _locationName = AppSettings.User.LocationName;
                }
                return _locationName;
            }
        }

        public void UpdateUserName()
        {
            if (AppSettings.User != null)
            {
                _locationName = AppSettings.User.LocationName;
            }
            RaisePropertyChanged("LocationName");
        }

        private bool _closeButtonVisibility;
        public bool CloseButtonVisibility
        {
            get { return _closeButtonVisibility; }
            set
            {
                _closeButtonVisibility = value;
                RaisePropertyChanged("CloseButtonVisibility");
            }
        }

        public bool MenuClicked { get; private set; }
        public bool CloseClicked { get; private set; }
        public bool LogOutClicked { get; private set; }
        public bool LookupClicked { get; private set; }
        public bool OfflineClicked { get; private set; }
        public bool RosterClicked { get; private set; }
        public bool ActivityClicked { get; private set; }
        public bool ManageClicked { get; private set; }
        public bool EditVoterClicked { get; private set; }
        public bool AddVoterClicked { get; private set; }
        public bool ElectionSetupClicked { get; private set; }
        public bool SystemSetupClicked { get; private set; }
        public bool UserSetupClicked { get; private set; }

        public bool IsManageMenuOpen { get; set; }

        private bool _menuVisible;
        public bool IsMenuVisible 
        { 
            get
            {
                return _menuVisible;
            }
            set
            {
                CloseButtonVisibility = !value;
                _menuVisible = value;
                RaisePropertyChanged("IsMenuVisible");
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get
            {
                return _isAdmin && !IsManageMenuOpen;
            }
            set
            {
                _isAdmin = value;
                RaisePropertyChanged("IsAdmin");
            }
        }

        public MainHeaderViewModel()
        {
            HeaderText = "Header";

            CloseButtonVisibility = true;

            CanOffline = true;
            CanRoster = true;
            CanActivity = true;
            CanManage = true;
            CanSettings = true;

            IsManageMenuOpen = false;
        }

        #region Commands
        // Bound command for closing the application
        public RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => this.CloseClick());
                }
                return _closeCommand;
            }
        }

        // Force parent window to close
        public void CloseClick()
        {
            // Maybe pass this up the chain similar to the menu clicked event
            //_parent.Close();

            CloseClicked = true;
            RaisePropertyChanged("CloseClicked");
        }

        public RelayCommand _menuCommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menuCommand == null)
                {
                    _menuCommand = new RelayCommand(param => this.MenuClick());
                }
                return _menuCommand;
            }
        }

        public void MenuClick()
        {
            MenuClicked = !MenuClicked;
            RaisePropertyChanged("MenuClicked");
        }

        // Bound command for logging out of the application
        public RelayCommand _logoutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new RelayCommand(param => this.LogOutClick());
                }
                return _logoutCommand;
            }
        }

        // Force parent window to close
        public void LogOutClick()
        {
            IsManageMenuOpen = false;
            RaisePropertyChanged("IsManageMenuOpen");
            RaisePropertyChanged("IsAdmin");

            LogOutClicked = true;
            RaisePropertyChanged("LogOutClicked");
        }

        // Bound command for navigating to the search screen
        public RelayCommand _lookupCommand;
        public ICommand LookupCommand
        {
            get
            {
                if (_lookupCommand == null)
                {
                    _lookupCommand = new RelayCommand(param => this.LookupClick());
                }
                return _lookupCommand;
            }
        }

        //Enable or Disable the Voter Lookup Button
        private bool _canLookupVoter;
        public bool CanLookupVoter
        {
            get { return _canLookupVoter; }
            internal set
            {
                _canLookupVoter = value;
                RaisePropertyChanged("CanLookupVoter");
            }
        }

        // Force parent frame to navigate to the voter search page
        public void LookupClick()
        {
            ResetMenuButtons();
            CanLookupVoter = false;

            LookupClicked = !LookupClicked;
            RaisePropertyChanged("LookupClicked");

            IsManageMenuOpen = false;
            RaisePropertyChanged("IsManageMenuOpen");
            RaisePropertyChanged("IsAdmin");
        }

        // Bound command for navigating to the offline search screen
        public RelayCommand _offlineCommand;
        public ICommand OfflineCommand
        {
            get
            {
                if (_offlineCommand == null)
                {
                    _offlineCommand = new RelayCommand(param => this.OfflineClick());
                }
                return _offlineCommand;
            }
        }

        //Enable or Disable the Offline Lookup Button
        private bool _canOffline;
        public bool CanOffline
        {
            get { return _canOffline; }
            internal set
            {
                _canOffline = value;
                RaisePropertyChanged("CanOffline");
            }
        }

        // Force parent frame to navigate to the voter search page
        public void OfflineClick()
        {
            ResetMenuButtons();
            CanOffline = false;

            OfflineClicked = !OfflineClicked;
            RaisePropertyChanged("OfflineClicked");
        }

        // Bound command for navigating to the roster screen
        public RelayCommand _rosterCommand;
        public ICommand RosterCommand
        {
            get
            {
                if (_rosterCommand == null)
                {
                    _rosterCommand = new RelayCommand(param => this.RosterClick());
                }
                return _rosterCommand;
            }
        }

        //Enable or Disable the Roster Button
        private bool _canRoster;
        public bool CanRoster
        {
            get { return _canRoster; }
            internal set
            {
                _canRoster = value;
                RaisePropertyChanged("CanRoster");
            }
        }

        // Force parent frame to navigate to the roster search page
        public void RosterClick()
        {
            ResetMenuButtons();
            CanRoster = false;

            RosterClicked = !RosterClicked;
            RaisePropertyChanged("RosterClicked");
        }

        // Bound command for navigating to the activity screen
        public RelayCommand _activityCommand;
        public ICommand ActivityCommand
        {
            get
            {
                if (_activityCommand == null)
                {
                    _activityCommand = new RelayCommand(param => this.ActivityClick());
                }
                return _activityCommand;
            }
        }

        //Enable or Disable the Activity Button
        private bool _canActivity;
        public bool CanActivity
        {
            get { return _canActivity; }
            internal set
            {
                _canActivity = value;
                RaisePropertyChanged("CanActivity");
            }
        }

        // Force parent frame to navigate to the activity page
        public void ActivityClick()
        {
            ResetMenuButtons();
            CanActivity = false;

            ActivityClicked = !ActivityClicked;
            RaisePropertyChanged("ActivityClicked");
        }

        // Bound command for navigating to the manage screen
        public RelayCommand _manageCommand;
        public ICommand ManageCommand
        {
            get
            {
                if (_manageCommand == null)
                {
                    _manageCommand = new RelayCommand(param => this.ManageClick());
                }
                return _manageCommand;
            }
        }

        //Enable or Disable the Manage Button
        private bool _canManage;
        public bool CanManage
        {
            get { return _canManage; }
            internal set
            {
                _canManage = value;
                RaisePropertyChanged("CanManage");
            }
        }

        // Force parent frame to navigate to the manage page
        public void ManageClick()
        {
            ResetMenuButtons();
            CanManage = false;

            ManageClicked = !ManageClicked;
            RaisePropertyChanged("ManageClicked");

            IsManageMenuOpen = true;
            RaisePropertyChanged("IsManageMenuOpen");
            RaisePropertyChanged("IsAdmin");
        }

        // Bound command for navigating to the edit search screen
        public RelayCommand _editVoterCommand;
        public ICommand EditVoterCommand
        {
            get
            {
                if (_editVoterCommand == null)
                {
                    _editVoterCommand = new RelayCommand(param => this.EditVoterClick());
                }
                return _editVoterCommand;
            }
        }

        //Enable or Disable the Edit Button
        private bool _canEditVoter;
        public bool CanEditVoter
        {
            get { return _canEditVoter; }
            internal set
            {
                _canEditVoter = value;
                RaisePropertyChanged("CanEditVoter");
            }
        }

        // Force parent frame to navigate to the edit search page
        public void EditVoterClick()
        {
            ResetMenuButtons();
            CanEditVoter = false;

            EditVoterClicked = !EditVoterClicked;
            RaisePropertyChanged("EditVoterClicked");
        }

        // Bound command for navigating to the add voter screen
        public RelayCommand _addVoterCommand;
        public ICommand AddVoterCommand
        {
            get
            {
                if (_addVoterCommand == null)
                {
                    _addVoterCommand = new RelayCommand(param => this.AddVoterClick());
                }
                return _addVoterCommand;
            }
        }

        //Enable or Disable the Add Button
        private bool _canAddVoter;
        public bool CanAddVoter
        {
            get { return _canAddVoter; }
            internal set
            {
                _canAddVoter = value;
                RaisePropertyChanged("CanAddVoter");
            }
        }

        // Force parent frame to navigate to the edit search page
        public void AddVoterClick()
        {
            ResetMenuButtons();
            CanAddVoter = false;

            AddVoterClicked = !AddVoterClicked;
            RaisePropertyChanged("AddVoterClicked");
        }

        // Bound command for navigating to the election settings screen
        public RelayCommand _electionCommand;
        public ICommand ElectionCommand
        {
            get
            {
                if (_electionCommand == null)
                {
                    _electionCommand = new RelayCommand(param => this.ElectionSettingsClick());
                }
                return _electionCommand;
            }
        }

        //Enable or Disable the Election Button
        private bool _canElection;
        public bool CanElection
        {
            get { return _canElection; }
            internal set
            {
                _canElection = value;
                RaisePropertyChanged("CanElection");
            }
        }

        // Force parent frame to navigate to the election settings page
        public void ElectionSettingsClick()
        {
            ResetMenuButtons();
            CanElection = false;

            ElectionSetupClicked = !ElectionSetupClicked;
            RaisePropertyChanged("ElectionSetupClicked");
        }

        // Bound command for navigating to the system settings screen
        public RelayCommand _systemCommand;
        public ICommand SystemCommand
        {
            get
            {
                if (_systemCommand == null)
                {
                    _systemCommand = new RelayCommand(param => this.SystemSettingsClick());
                }
                return _systemCommand;
            }
        }

        //Enable or Disable the Settings Button
        private bool _canSystem;
        public bool CanSystem
        {
            get { return _canSystem; }
            internal set
            {
                _canSystem = value;
                RaisePropertyChanged("CanSystem");
            }
        }

        // Force parent frame to navigate to the settings page
        public void SystemSettingsClick()
        {
            ResetMenuButtons();
            CanSystem = false;

            SystemSetupClicked = !SystemSetupClicked;
            RaisePropertyChanged("SystemSetupClicked");
        }

        // Bound command for navigating to the user level settings screen
        public RelayCommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                {
                    _settingsCommand = new RelayCommand(param => this.UserSettingsClick());
                }
                return _settingsCommand;
            }
        }

        //Enable or Disable the Settings Button
        private bool _canSettings;
        public bool CanSettings
        {
            get { return _canSettings; }
            internal set
            {
                _canSettings = value;
                RaisePropertyChanged("CanSettings");
            }
        }

        // Force parent frame to navigate to the settings page
        public void UserSettingsClick()
        {
            ResetMenuButtons();
            CanSettings = false;

            UserSetupClicked = !UserSetupClicked;
            RaisePropertyChanged("UserSetupClicked");
        }

        public void CloseManageMenu()
        {
            IsManageMenuOpen = false;
            RaisePropertyChanged("IsManageMenuOpen");
            RaisePropertyChanged("IsAdmin");
        }

        public void ResetMenuButtons()
        {
            CanLookupVoter = true;
            CanOffline = true;
            CanRoster = true;
            CanActivity = true;
            CanManage = true;
            CanEditVoter = true;
            CanAddVoter = true;
            CanElection = true;
            CanSystem = true;
            CanSettings = true;
        }
        #endregion
    }
}
