using System;
using System.Collections.Generic;
using System.Text;
using EVote.Utilities.Controls;
using EVote.Utilities.Models;
using EVote.Views;

namespace EVote.Methods
{
    internal static class Navigation
    {
        private static INavigator _navigator;
        internal static INavigator Navigator
        {
            get
            {
                if (_navigator == null)
                {
                    _navigator = new EVote.State.Navigator();
                }
                return _navigator;
            }
            set
            {
                _navigator = value;
            }
        }

        internal static void Home(IViewParametersModel parameters)
        {
            //Navigator.CurrentView = new HomeView();
            //Navigator.CurrentView.DataContext = new HomeViewModel(parameters);
        }

        #region Voter
        internal static void VoterSearchView()
        {
            VoterSearchView(new VoterViewParametersModel());
        }
        internal static void VoterSearchView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new VoterSearchView();
            Navigator.CurrentView.DataContext = new VoterSearchViewModel(parameters);
        }

        internal static void SignatureCaptureView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new SignatureCaptureView(parameters);
            Navigator.CurrentView.DataContext = new SignatureCaptureViewModel(parameters);
        }

        internal static void SignatureResultsView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new SignatureResultsView();
            Navigator.CurrentView.DataContext = new SignatureResultsViewModel(parameters);
        }

        internal static void BallotPrintView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new BallotPrintView();
            Navigator.CurrentView.DataContext = new BallotPrintViewModel(parameters);
        }

        internal static void SpoiledBallotView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new SpoiledBallotView();
            Navigator.CurrentView.DataContext = new SpoiledBallotViewModel(parameters);
        }
        #endregion

        #region Roster
        internal static void RosterSearchView()
        {
            Navigator.CurrentView = new RosterSearchView();
            Navigator.CurrentView.DataContext = new RosterSearchViewModel();
        }

        internal static void RosterSignatureView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new RosterSignatureView();
            Navigator.CurrentView.DataContext = new RosterSignatureViewModel(parameters);
        }
        #endregion

        #region Activity
        internal static void ActivityView()
        {
            //Navigator.CurrentView = new VotingActivityView();
            //Navigator.CurrentView.DataContext = new VotingActivityViewModel();

            Navigator.CurrentView = new EVote.Views.Activity.BasicRowExample();
        }
        #endregion

        #region Manage
        internal static void EditVoterSearchView()
        {
            Navigator.CurrentView = new EditVoterSearchView();
            Navigator.CurrentView.DataContext = new EditVoterSearchViewModel();
        }

        internal static void EditVoterView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new EditVoterView();
            Navigator.CurrentView.DataContext = new EditVoterViewModel(parameters);
        }

        internal static void AddVoterView()
        {
            Navigator.CurrentView = new EditVoterView();
            Navigator.CurrentView.DataContext = new EditVoterViewModel(new VoterViewParametersModel());
        }

        internal static void ChangeDistrictView(IViewParametersModel parameters)
        {
            Navigator.CurrentView = new ChangeDistrictView();
            Navigator.CurrentView.DataContext = new ChangeDistrictViewModel(parameters);
        }

        internal static void ElectionSettingsView()
        {
            Navigator.CurrentView = new ElectionSettingsView();
            Navigator.CurrentView.DataContext = new ElectionSettingsViewModel();
        }

        internal static void SystemSettingsView()
        {
            Navigator.CurrentView = new SystemSettingsView();
            Navigator.CurrentView.DataContext = new SystemSettingsViewModel();
        }

        internal static void ValidLocations()
        {
            Navigator.CurrentView = new ValidLocationsView();
            Navigator.CurrentView.DataContext = new ValidLocationsViewModel();
        }
        #endregion
    }
}
