using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using EVote.Factories;
using EVote.Logging;
using EVote.Methods;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class RosterSearchViewModel : ViewModelBase
    {
        EVoteLogger _searchLogger = new EVoteLogger("EVoteLogs", true);

        private IViewParametersModel _parameters;

        public bool NameSearch { get; set; }

        public UserControl SearchParametersCenterPanel { get; set; }

        public UserControl SearchPanel { get; set; }

        public RosterSearchViewModel()
        {
            _parameters = new VoterViewParametersModel();
            _parameters.Search.Location = AppSettings.User.LocationId;
            if (AppSettings.User.RollId == 1)
            {
                _parameters.Search.Status = 11;
            }
            else
            {
                _parameters.Search.Status = 12;
            }
            _parameters.Search.SDBN = AppSettings.System.APIDB;

            NameSearch = true;
            if (NameSearch)
            {
                SetNameSearchPanel();
                SetVoterSearchPanel();
            }
            else
            {
                SetDateSearchPanel();
                SetMonthSearchPanel();
            }
        }

        private async void SetVoterSearchPanel()
        {
            SetSearchingPanel();

            var voterReturnList = await SearchVoters();
            if (voterReturnList != null && voterReturnList.Count > 0)
            {
                SearchPanel = new VoterSearchPanelView();
                var voterSearchPanel = new VoterSearchPanelViewModel(voterReturnList.OrderByDescending(o => o.DateVoted).ToList());
                voterSearchPanel.IsStandardVoter = false;
                voterSearchPanel.PropertyChanged += OnVoterSelectedPropertyChanged;
                SearchPanel.DataContext = voterSearchPanel;

                RaisePropertyChanged("SearchPanel");
            }
            else
            {
                SearchPanel = new NoVotersFoundView();
                RaisePropertyChanged("SearchPanel");
            }
        }

        private async Task<List<VoterDataModel>> SearchVoters()
        {
            if (AppSettings.TrainingMode == true)
            {
                try
                {
                    var trainingFactory = new TrainingFactory();
                    return await trainingFactory.SearchVoterAsync(_parameters.Search);
                }
                catch
                {
                    return new List<VoterDataModel>();
                }
            }
            else if (AppSettings.OfflineMode == false)
            {
                try
                {
                    var voterFactory = new VoterFactory();
                    return await voterFactory.SearchRosterAsync(_parameters.Search);
                }
                catch (Exception e)
                {
                    // Log error message
                    _searchLogger.WriteLog("Database Error: " + e.Message);

                    AlertDialog connectionFailed = new AlertDialog("COULD NOT ESTABLISH A CONNECTION TO THE INTERNET");
                    connectionFailed.ShowDialog();
                    StatusBarMethods.WifiStatus(false);
                    return new List<VoterDataModel>();
                }
            }
            else
            {
                try
                {
                    var offlineFactory = new OfflineFactory();
                    return await offlineFactory.SearchVoterAsync(_parameters.Search);
                }
                catch
                {
                    return new List<VoterDataModel>();
                }
            }
        }

        private void OnVoterSelectedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedVoter")
            {
                //Code to respond to a change in the ViewModel
                Console.WriteLine(
                    "Selected Voter: " +
                    ((VoterSearchPanelViewModel)sender).SelectedVoter.VoterID.ToString());

                // Get selected voter
                var selectedVoter = ((VoterSearchPanelViewModel)sender).SelectedVoter;
                _parameters.Voter = selectedVoter;
                //VoterSearchModel searchParameters = null;
                //if (SearchParametersCenterPanel != null)
                //{
                //    searchParameters = ((VoterSearchViewModelBase)SearchParametersCenterPanel.DataContext).VoterSearch;
                //}

                //VoterViewParametersModel voterParameters = new VoterViewParametersModel()
                //{
                //    Voter = selectedVoter,
                //    Search = searchParameters
                //};

                // Capture Signature
                Navigation.RosterSignatureView(_parameters);
            }
        }

        private void OnSearchParametersPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VoterSearch")
            {
                if (((VoterSearchViewModelBase)sender).Type == "NAME")
                {
                    _parameters.Search.FirstName = ((VoterSearchViewModelBase)sender).NameFirst;
                    _parameters.Search.LastName = ((VoterSearchViewModelBase)sender).NameLast;
                    _parameters.Search.BirthYear = ((VoterSearchViewModelBase)sender).BirthYear;
                    _parameters.Search.VoterID = ((VoterSearchViewModelBase)sender).RollNumber;
                }
                // Run voter query
                SetVoterSearchPanel();
            }
            else if (e.PropertyName == "VoterClear")
            {
                // Clear the search parameters
                _parameters = new VoterViewParametersModel();
                _parameters.Search.Location = 1;
                _parameters.Search.Status = 12;
                _parameters.Search.SDBN = AppSettings.System.APIDB;

                // Clear search panel
                ClearSearchPanel();
            }
            else if (e.PropertyName == "VoterNameSearch")
            {
                // Switch Search Parameters Panel
                SetNameSearchPanel();
                ClearSearchPanel();
            }
            else if (e.PropertyName == "VoterDateSearch")
            {
                // Switch Search Parameters Panel
                SetDateSearchPanel();
                SetMonthSearchPanel();
            }
            else if (e.PropertyName == "MonthSearch")
            {
                // Switch Search Parameters Panel
                SetMonthSearchPanel();
            }
            else if (e.PropertyName == "DaySearch")
            {
                // Switch Search Parameters Panel
                SetDaySearchPanel();
            }
            else if (e.PropertyName == "YearSearch")
            {
                // Switch Search Parameters Panel
                SetYearSearchPanel();
            }

        }

        private void ClearSearchPanel()
        {
            SearchPanel = null;
            RaisePropertyChanged("SearchPanel");
        }

        private void SetSearchingPanel()
        {
            SearchPanel = new VoterSearchingPanelView();
            RaisePropertyChanged("SearchPanel");
        }

        private void SetNameSearchPanel()
        {
            SearchParametersCenterPanel = new VoterNameSearchView();
            var searchParameters = new VoterNameSearchViewModel();
            searchParameters.PropertyChanged += OnSearchParametersPropertyChanged;
            SearchParametersCenterPanel.DataContext = searchParameters;
            RaisePropertyChanged("SearchParametersCenterPanel");
        }

        private void SetDateSearchPanel()
        {
            SearchParametersCenterPanel = new DateSearchDisplayView();
            var dateSearchPanel = new DateSearchDisplayViewModel(_parameters.Search.SearchDate);
            dateSearchPanel.PropertyChanged += OnSearchParametersPropertyChanged;
            SearchParametersCenterPanel.DataContext = dateSearchPanel;
            RaisePropertyChanged("SearchParametersCenterPanel");
        }

        private void SetMonthSearchPanel()
        {
            SearchPanel = new DateSearchMonthView();
            var dateSearchPanel = new DateSearchMonthViewModel(_parameters.Search.SearchDate);
            dateSearchPanel.PropertyChanged += OnDateSelectedPropertyChanged;
            SearchPanel.DataContext = dateSearchPanel;
            RaisePropertyChanged("SearchPanel");
        }

        private void SetDaySearchPanel()
        {
            SearchPanel = new DateSearchDayView();
            var dateSearchPanel = new DateSearchDayViewModel(_parameters.Search.SearchDate);
            dateSearchPanel.PropertyChanged += OnDateSelectedPropertyChanged;
            SearchPanel.DataContext = dateSearchPanel;
            RaisePropertyChanged("SearchPanel");
        }

        private void SetYearSearchPanel()
        {
            SearchPanel = new DateSearchYearView();
            var dateSearchPanel = new DateSearchYearViewModel(_parameters.Search.SearchDate);
            dateSearchPanel.PropertyChanged += OnDateSelectedPropertyChanged;
            SearchPanel.DataContext = dateSearchPanel;
            RaisePropertyChanged("SearchPanel");
        }

        private void OnDateSelectedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "BirthDate")
            {
                _parameters.Search.SearchDate = ((VoterSearchViewModelBase)sender).BirthDate;
                SetDateSearchPanel();

                if (((VoterSearchViewModelBase)sender).Type == "MONTH")
                {
                    SetDaySearchPanel();
                }

                if (((VoterSearchViewModelBase)sender).Type == "DAY")
                {
                    SetYearSearchPanel();
                }

                if (((VoterSearchViewModelBase)sender).Type == "YEAR")
                {
                    // SEARCH VOTER
                    SetVoterSearchPanel();
                }

            }
        }
    }
}
