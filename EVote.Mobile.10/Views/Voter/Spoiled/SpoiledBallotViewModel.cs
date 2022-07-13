using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EVote.Factories;
using EVote.LocalDatabase;
using EVote.Methods;
using EVote.Utilities.Commands;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class SpoiledBallotViewModel : ViewModelBase
    {
        public VoterDataModel Voter { get; set; }

        private IViewParametersModel _parameters;

        public SpoiledBallotViewModel(IViewParametersModel Parameters)
        {
            _parameters = Parameters;

            Voter = Parameters.Voter;
        }

        public async void SpoiledReasons()
        {
            ElectionFactory factory = new ElectionFactory();
            var reasons = await factory.SpoiledReasons();
        }

        #region SpoiledReasons
        private List<SpoiledReason> _spoiledReasons;
        public List<SpoiledReason> SpoiledReasonsList
        {
            get
            {
                if (_spoiledReasons == null)
                {
                    OfflineFactory factory = new OfflineFactory();
                    _spoiledReasons = factory.SpoiledReasons();
                }
                return _spoiledReasons;
            }
        }

        private SpoiledReason _selectedSpoiledReasonItem;
        public SpoiledReason SelectedSpoiledReasonItem
        {
            get
            {
                if (_selectedSpoiledReasonItem == null)
                {
                    _selectedSpoiledReasonItem = SpoiledReasonsList.FirstOrDefault();
                }
                return _selectedSpoiledReasonItem;
            }

            set
            {
                _selectedSpoiledReasonItem = value;
            }
        }
        #endregion

        #region Commands
        // Bound command for spoiling the voter ballot
        public RelayCommand _spoilCommand;
        public ICommand SpoilCommand
        {
            get
            {
                if (_spoilCommand == null)
                {
                    _spoilCommand = new RelayCommand(param => this.SpoilClick());
                }
                return _spoilCommand;
            }
        }

        //Enable or Disable the Spoil Button
        private bool _canSpoil;
        public bool CanSpoil
        {
            get { return _canSpoil; }
            internal set
            {
                _canSpoil = value;
                RaisePropertyChanged("CanSpoil");
            }
        }

        // Spoil the voter ballot
        public void SpoilClick()
        {
            if (SelectedSpoiledReasonItem != null)
            {
                VoterDataMethods.SpoilBallot(Voter, SelectedSpoiledReasonItem.SpoiledReasonId);

                Navigation.BallotPrintView(_parameters);
            }
        }

        // Bound command for returning to the edit search screen
        public RelayCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(param => this.CancelClick());
                }
                return _cancelCommand;
            }
        }

        //Enable or Disable the Cancel Button
        private bool _canCancel;
        public bool CanCancel
        {
            get { return _canCancel; }
            internal set
            {
                _canCancel = value;
                RaisePropertyChanged("CanCancel");
            }
        }

        // Force parent frame to navigate to the edit search page
        public void CancelClick()
        {
            Navigation.VoterSearchView(new VoterViewParametersModel());
        }
        #endregion
    }
}
