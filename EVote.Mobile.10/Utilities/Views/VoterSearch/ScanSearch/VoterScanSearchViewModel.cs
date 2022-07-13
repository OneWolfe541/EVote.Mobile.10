using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Utilities.Commands;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{
    public class VoterScanSearchViewModel : VoterSearchViewModelBase
    {
        public VoterScanSearchViewModel()
        {

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
            _voterSearch = new VoterSearchModel
            {
                FirstName = _nameFirst,
                LastName = _nameLast,
                BirthYear = _birthYear
            };
            RaisePropertyChanged("VoterSearch");
        }

        private RelayCommand _nameSearchCommand;
        public ICommand NameSearchCommand
        {
            get
            {
                if (_nameSearchCommand == null)
                {
                    _nameSearchCommand = new RelayCommand(param => this.NameSearchClick());
                }
                return _nameSearchCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void NameSearchClick()
        {
            _voterSearch = null;

            NameFirst = null;
            RaisePropertyChanged("NameFirst");
            NameLast = string.Empty;
            RaisePropertyChanged("NameLast");
            BirthYear = null;
            RaisePropertyChanged("BirthYear");

            RaisePropertyChanged("VoterClear");
            RaisePropertyChanged("VoterNameSearch");
        }
        #endregion
    }
}
