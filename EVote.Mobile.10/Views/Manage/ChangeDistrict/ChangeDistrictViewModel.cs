using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EVote.Factories;
using EVote.LocalDatabase;
using EVote.Methods;
using EVote.Utilities.Commands;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Extensions;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class ChangeDistrictViewModel : ViewModelBase
    {
        private IViewParametersModel _parameters;

        public VoterDataModel Voter 
        { 
            get
            {
                return _parameters.Voter;
            }
            set
            {
                _parameters.Voter = value;
            }
        }

        public ChangeDistrictViewModel(IViewParametersModel Parameters)
        {
            _parameters = Parameters;
            //Voter = Parameters.Voter;
        }

        #region Ballots
        private List<BallotStyle> _ballotStyles;
        public List<BallotStyle> BallotStyleList
        {
            get
            {
                if (_ballotStyles == null)
                {
                    OfflineFactory factory = new OfflineFactory();
                    _ballotStyles = factory.BallotStyles();
                }
                return _ballotStyles;
            }
        }

        private BallotStyle _selectedBallotStyleItem;
        public BallotStyle SelectedBallotStyleItem
        {
            get
            {
                if (_selectedBallotStyleItem == null)
                {
                    _selectedBallotStyleItem = BallotStyleList.Where(j => j.BallotStyleId == Voter.BallotStyleID).FirstOrDefault();
                }
                return _selectedBallotStyleItem;
            }

            set
            {
                _selectedBallotStyleItem = value;

                if (_selectedBallotStyleItem != null)
                {
                    // Set new District
                    //Voter.DistrictName = _selectedBallotStyleItem.JurisdictionName;
                    //Voter.JurisdictionID = _selectedBallotStyleItem.JurisdictionId;

                    // Get new Ballot Style
                    //var ballot = BallotStyleList.Where(bs => bs.BallotStyleId == _selectedJurisdictionItem.JurisdictionId).FirstOrDefault();
                    //if (ballot != null)
                    //{
                    //    // Set new Ballot Style
                    //    Voter.BallotStyleID = ballot.BallotStyleId;
                    //    Voter.BallotStyleName = ballot.BallotStyleName;
                    //    Voter.BallotStyleFileName = ballot.BallotStyleFileName;
                    //}
                    Voter.BallotStyleID = _selectedBallotStyleItem.BallotStyleId;
                    Voter.BallotStyleName = _selectedBallotStyleItem.BallotStyleName;
                    Voter.BallotStyleFileName = _selectedBallotStyleItem.BallotStyleFileName;
                }
            }
        }
        #endregion 

        #region Districts
        private List<Jurisdiction> _jurisdictions;
        public List<Jurisdiction> JurisdictionList
        {
            get
            {
                if (_jurisdictions == null)
                {
                    OfflineFactory factory = new OfflineFactory();
                    _jurisdictions = factory.Jurisdictions();
                }
                return _jurisdictions;
            }
        }

        private Jurisdiction _selectedJurisdictionItem;
        public Jurisdiction SelectedJurisdictionItem
        {
            get
            {
                if (_selectedJurisdictionItem == null)
                {
                    _selectedJurisdictionItem = JurisdictionList.Where(j => j.JurisdictionId == Voter.JurisdictionID).FirstOrDefault();
                }
                return _selectedJurisdictionItem;
            }

            set
            {
                _selectedJurisdictionItem = value;

                if (_selectedJurisdictionItem != null)
                {
                    // Set new District
                    Voter.DistrictName = _selectedJurisdictionItem.JurisdictionName;
                    Voter.JurisdictionID = _selectedJurisdictionItem.JurisdictionId;

                    // Get new Ballot Style
                    var ballot = BallotStyleList.Where(bs => bs.BallotStyleId == _selectedJurisdictionItem.JurisdictionId).FirstOrDefault();
                    if (ballot != null)
                    {
                        // Set new Ballot Style
                        Voter.BallotStyleID = ballot.BallotStyleId;
                        Voter.BallotStyleName = ballot.BallotStyleName;
                        Voter.BallotStyleFileName = ballot.BallotStyleFileName;
                    }
                }
            }
        }
        #endregion

        #region Commands
        // Bound command for saving the voter record
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

        // Save the voter record
        public void SaveClick()
        {
            if (AppSettings.TrainingMode == false)
            {
                VoterDataMethods.SaveVoter(Voter);
            }

            if (AppSettings.Config.DistrictOnlyVoting == true)
            {
                // Validate Voters district
                if (AppSettings.Location.Validate(AppSettings.User.LocationId, Voter.District))
                {
                    Navigation.SignatureCaptureView(_parameters);
                }
                else
                {
                    AlertDialog alreadyVotedMessage = new AlertDialog("INVALID DISTRICT FOR THIS LOCATION");
                    alreadyVotedMessage.ShowDialog();
                }
            }
            else
            {
                Navigation.SignatureCaptureView(_parameters);
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
            //StatusBarMethods.HighlightEditVoter();
            //Navigation.EditVoterSearchView();

            Navigation.VoterSearchView();
        }
        #endregion
    }
}
