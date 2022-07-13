using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Methods;
using EVote.Settings;
using EVote.Utilities.Commands;
using EVote.Utilities.Views;
using EVote.LocalDatabase;
using EVote.Factories;
using System.Linq;
using System.Collections.ObjectModel;
using EVote.Utilities.Dialogs;

namespace EVote.Views
{
    public class ValidLocationsViewModel : ViewModelBase
    {
        private List<LocationJurisdiction> _userDistricts;
        public List<LocationJurisdiction> UserDistrictList
        {
            get
            {
                if(_userDistricts == null)
                {
                    _userDistricts = AppSettings.Location.Where(l => l.Active == true).ToList();
                    _userDistricts.Add(new LocationJurisdiction());
                }                
                return _userDistricts;
            }
            set
            {
                _userDistricts = value;
                RaisePropertyChanged("UserDistrictList");
            }
        }

        public ValidLocationsViewModel()
        {

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
                //if (_selectedJurisdictionItem == null)
                //{
                //    _selectedJurisdictionItem = JurisdictionList.Where(j => j.JurisdictionId == Voter.JurisdictionID).FirstOrDefault();
                //}
                return _selectedJurisdictionItem;
            }

            set
            {
                _selectedJurisdictionItem = value;
            }
        }
        #endregion

        #region Locations
        private ObservableCollection<Location> _locations;
        public ObservableCollection<Location> LocationsList
        {
            get
            {
                if (_locations == null)
                {
                    OfflineFactory factory = new OfflineFactory();
                    _locations = new ObservableCollection<Location>(factory.Locations().Where(l => l.RollId != 0));
                }
                return _locations;
            }
        }

        private Location _selectedLocationItem;
        public Location SelectedLocationItem
        {
            get
            {
                //if (_selectedLocationItem == null)
                //{
                //    _selectedLocationItem = LocationsList.Where(j => j.LocationId == Voter.LocationID).FirstOrDefault();
                //}
                return _selectedLocationItem;
            }

            set
            {
                _selectedLocationItem = value;
            }
        }
        #endregion

        #region Commands
        // Bound command for saving the location record
        public RelayCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => this.SaveClick(param));
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

        // Save the location record
        public void SaveClick(object location)
        {
            //var list = AppSettings.Location;

            //var next = UserDistrictList;

            //var test = location as LocationJurisdiction;

            ValidLocationMethods.SaveAsync(AppSettings.Location);
        }

        // Bound command for adding a location record
        public RelayCommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(param => this.AddClick(param));
                }
                return _addCommand;
            }
        }

        //Enable or Disable the Add Button
        private bool _canAdd;
        public bool CanAdd
        {
            get { return _canAdd; }
            internal set
            {
                _canAdd = value;
                RaisePropertyChanged("CanAdd");
            }
        }

        // Add the location record
        public void AddClick(object location)
        {
            var newLocation = location as LocationJurisdiction;
            newLocation.Active = true;
            newLocation.LastModified = DateTime.Now;

            // Check if a Site and District are selected
            var site = LocationsList.Where(ll => ll.LocationId == newLocation.LocationId).FirstOrDefault();
            var district = JurisdictionList.Where(ll => ll.JurisdictionId == newLocation.JurisdictionId).FirstOrDefault();
            if (site == null)
            {
                // Display message
                AlertDialog connectionFailed = new AlertDialog("PLEASE SELECT A VALID SITE");
                connectionFailed.ShowDialog();

                return;
            }
            else if (district == null)
            {
                // Display message
                AlertDialog connectionFailed = new AlertDialog("PLEASE SELECT A VALID DISTRICT");
                connectionFailed.ShowDialog();

                return;
            }
            // Check if a matching location already exists
            else if(AppSettings.Location.Validate(newLocation.LocationId, newLocation.JurisdictionId))
            {
                AlertDialog connectionFailed = new AlertDialog("THAT LOCATION ALREADY EXISTS");
                connectionFailed.ShowDialog();

                return;
            }

            AppSettings.Location.Add(newLocation);

            ValidLocationMethods.SaveAsync(AppSettings.Location);

            // Reset list
            _userDistricts = null;
            RaisePropertyChanged("UserDistrictList");
        }

        // Bound command for removing the location record
        public RelayCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(param => this.RemoveClick(param));
                }
                return _removeCommand;
            }
        }

        //Enable or Disable the Remove Button
        private bool _canRemove;
        public bool CanRemove
        {
            get { return _canRemove; }
            internal set
            {
                _canRemove = value;
                RaisePropertyChanged("CanRemove");
            }
        }

        // Save the location record
        public void RemoveClick(object location)
        {
            if (location != null)
            {
                AppSettings.Location.Remove(location as LocationJurisdiction);

                ValidLocationMethods.SaveAsync(AppSettings.Location);

                // Reset list
                _userDistricts = null;
                RaisePropertyChanged("UserDistrictList");
            }
        }

        #endregion

    }
}
