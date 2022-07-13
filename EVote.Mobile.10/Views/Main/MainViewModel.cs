using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using EVote.Factories;
using EVote.Methods;
using EVote.Utilities.Controls;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views.Main
{
    public class MainViewModel : ViewModelBase
    {
        private Window _parent; // Use for closing the current application

        public UserControl Header { get; set; }

        public UserControl StatusBar { get; set; }

        public INavigator Navigator { get; set; }// = new EVote.NetCore.VotingCenter.State.Navigator();

        public MainViewModel(Window Parent)
        {
            _parent = Parent;

            Header = new MainHeaderView();
            //MainHeaderViewModel mainHeaderViewModel = new MainHeaderViewModel();
            //mainHeaderViewModel.PropertyChanged += OnHeaderPropertyChanged;
            ((App)Application.Current).MainHeader = new MainHeaderViewModel();
            ((App)Application.Current).MainHeader.PropertyChanged += OnHeaderPropertyChanged;
            Header.DataContext = ((App)Application.Current).MainHeader;

            StatusBar = new StatusBarView();
            //StatusBar.DataContext = new StatusBarViewModel();
            ((App)Application.Current).StatusBar = new StatusBarViewModel();
            StatusBar.DataContext = ((App)Application.Current).StatusBar;

            Navigator = Navigation.Navigator;
            //Navigator.CurrentView = new VoterSearchView();
            //Navigator.CurrentView.DataContext = new VoterSearchViewModel(new VoterViewParametersModel());

            Navigator.CurrentView = new LoginView();
            Navigator.CurrentView.DataContext = new LoginViewModel();

            //ElectionFactory factory = new ElectionFactory();
            //factory.Configs();

            // Set offline display
            ((App)Application.Current).StatusBar.SetOfflineStatus(((App)Application.Current).GlobalSettings.Settings.OfflineMode);
        }

        private void OnHeaderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MenuClicked")
            {
                //((SlidingMenuViewModel)Navigator.CurrentMenu.DataContext).Toggle();
            }
            else if (e.PropertyName == "CloseClicked")
            {
                _parent.Close();
            }
            else if (e.PropertyName == "LogOutClicked")
            {
                //AppSettings.OfflineMode = false;
                AppSettings.User = null;
                StatusBarMethods.SetMenuAdmin(false);
                Navigator.CurrentView = new LoginView();
                Navigator.CurrentView.DataContext = new LoginViewModel();
            }
            else if(e.PropertyName == "LookupClicked")
            {
                //AppSettings.OfflineMode = false;
                Navigator.CurrentView = new VoterSearchView();
                Navigator.CurrentView.DataContext = new VoterSearchViewModel(new VoterViewParametersModel());
            }
            else if (e.PropertyName == "OfflineClicked")
            {
                //AppSettings.OfflineMode = true;
                Navigator.CurrentView = new VoterSearchView();
                Navigator.CurrentView.DataContext = new VoterSearchViewModel(new VoterViewParametersModel());
            }
            else if (e.PropertyName == "RosterClicked")
            {
                Navigator.CurrentView = new RosterSearchView();
                Navigator.CurrentView.DataContext = new RosterSearchViewModel();
            }
            else if (e.PropertyName == "ActivityClicked")
            {
                Navigator.CurrentView = new VotingActivityView();
                Navigator.CurrentView.DataContext = new VotingActivityViewModel();

                //Navigator.CurrentView = new EVote.Views.Activity.BasicRowExample();
            }
            else if (e.PropertyName == "ManageClicked")
            {

            }
            else if (e.PropertyName == "EditVoterClicked")
            {
                Navigator.CurrentView = new EditVoterSearchView();
                Navigator.CurrentView.DataContext = new EditVoterSearchViewModel();
            }
            else if (e.PropertyName == "AddVoterClicked")
            {
                Navigator.CurrentView = new EditVoterView();
                Navigator.CurrentView.DataContext = new EditVoterViewModel(new VoterViewParametersModel());
            }
            else if (e.PropertyName == "ElectionSetupClicked")
            {
                Navigator.CurrentView = new ElectionSettingsView();
                Navigator.CurrentView.DataContext = new ElectionSettingsViewModel();
            }
            else if (e.PropertyName == "SystemSetupClicked")
            {
                Navigator.CurrentView = new SystemSettingsView();
                Navigator.CurrentView.DataContext = new SystemSettingsViewModel();
            }
            else if (e.PropertyName == "UserSetupClicked")
            {
                Navigator.CurrentView = new UserSettingsView();
                Navigator.CurrentView.DataContext = new UserSettingsViewModel();
            }
        }
    }
}
