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
    public class EditVoterViewModel : ViewModelBase
    {
        public VoterDataModel Voter { get; set; }

        public bool IsEditVoter { get; set; }

        public bool StatusChanged { get; set; }

        public EditVoterViewModel(IViewParametersModel Parameters)
        {
            Voter = Parameters.Voter;

            if (!Voter.VoterID.IsNullOrEmpty())
            {
                IsEditVoter = true;
            }
            else
            {
                IsEditVoter = false;
            }

            StatusChanged = false;
        }

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
                if(_selectedJurisdictionItem == null)
                {
                    _selectedJurisdictionItem = JurisdictionList.Where(j => j.JurisdictionId == Voter.JurisdictionID).FirstOrDefault();
                }
                return _selectedJurisdictionItem;
            }

            set
            {
                _selectedJurisdictionItem = value;
                Voter.JurisdictionID = _selectedJurisdictionItem.JurisdictionId;
            }
        }
        #endregion

        #region Locations
        private List<Location> _locations;
        public List<Location> LocationsList
        {
            get
            {
                if (_locations == null)
                {
                    OfflineFactory factory = new OfflineFactory();
                    _locations = factory.Locations();
                }
                return _locations;
            }
        }

        private Location _selectedLocationItem;
        public Location SelectedLocationItem
        {
            get
            {
                if (_selectedLocationItem == null)
                {
                    _selectedLocationItem = LocationsList.Where(j => j.LocationId == Voter.LocationID).FirstOrDefault();
                }
                return _selectedLocationItem;
            }

            set
            {
                StatusChanged = false;
                _selectedLocationItem = value;
                Voter.LocationID = _selectedLocationItem.LocationId;
            }
        }
        #endregion

        #region Statuses
        private List<Statuses> _statuses;
        public List<Statuses> StatusesList
        {
            get
            {
                if (_statuses == null)
                {
                    OfflineFactory factory = new OfflineFactory();
                    _statuses = factory.Statuses();
                }
                return _statuses;
            }
        }

        private Statuses _selectedStatuesItem;
        public Statuses SelectedStatusesItem
        {
            get
            {
                if (_selectedStatuesItem == null)
                {
                    _selectedStatuesItem = StatusesList.Where(j => j.StatusId == Voter.LogCode).FirstOrDefault();
                }
                return _selectedStatuesItem;
            }

            set
            {
                StatusChanged = true;
                _selectedStatuesItem = value;
                Voter.LogCode = _selectedStatuesItem.StatusId;
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
        public async void SaveClick()
        {
            VoterDataMethods.SaveVoter(Voter);

            if(StatusChanged == true)
            {
                if (Voter.LogCode != null)
                {
                    Voter.ActivityDate = DateTime.Now;
                    var voterFactory = new VoterFactory();
                    await voterFactory.MarkVoterAsync(Voter);
                }
                else
                {
                    AlertDialog connectionFailed = new AlertDialog("THIS VOTER DOES NOT HAVE ANY VOTING ACTIVITY");
                    connectionFailed.ShowDialog();
                }
                StatusChanged = false;
            }

            if(IsEditVoter == false)
            {
                Navigation.VoterSearchView();
                StatusBarMethods.HighlightVoterLookup();
            }
            else if (IsEditVoter == true)
            {
                AlertDialog connectionFailed = new AlertDialog("VOTER CHANGES HAVE BEEN SAVED");
                connectionFailed.ShowDialog();

                Navigation.EditVoterSearchView();
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
            StatusBarMethods.HighlightEditVoter();
            Navigation.EditVoterSearchView();
        }
        #endregion
    }
}
