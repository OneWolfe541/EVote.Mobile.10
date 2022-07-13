using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    internal class VoterSearchViewModel : ViewModelBase
    {
        EVoteLogger _searchLogger = new EVoteLogger("EVoteLogs", true);

        public string Text { get; set; }

        private IViewParametersModel _parameters;

        public bool NameSearch { get; set; }

        public UserControl SearchParametersPanel { get; set; }

        public UserControl SearchParametersCenterPanel { get; set; }

        public UserControl SearchParametersCenterBuffer { get; set; }

        public UserControl SearchPanel { get; set; }

        public VoterSearchViewModel(IViewParametersModel Parameters)
        {            
            if (Parameters == null) Parameters = new VoterViewParametersModel();
            _parameters = Parameters;
            _parameters.Search.SDBN = AppSettings.System.APIDB;

            Text = "Voter Search";

            //NameSearch = true;
            if (NameSearch)
            {
                SetNameSearchPanel();
            }
            else
            {
                SetDateSearchPanel();
                SetMonthSearchPanel();
            }

            // NAME SEARCH
            //SearchParametersPanel = new VoterNameSearchView();
            //var searchParameters = new VoterNameSearchViewModel();
            //searchParameters.PropertyChanged += OnSearchParametersPropertyChanged;
            //SearchParametersPanel.DataContext = searchParameters;

            // SCAN SEARCH
            //SearchParametersPanel = new VoterScanSearchView();
            //var searchParameters = new VoterScanSearchViewModel();
            //searchParameters.PropertyChanged += OnSearchParametersPropertyChanged;
            //SearchParametersPanel.DataContext = searchParameters;            

            //SearchPanel = new VoterSearchPanelView();
            //var voterFactory = new VoterFactory();
            //var voterSearchPanel = new VoterSearchPanelViewModel(voterFactory.Create());
            //voterSearchPanel.IsStandardVoter = true;
            //voterSearchPanel.PropertyChanged += OnVoterSelectedPropertyChanged;
            //SearchPanel.DataContext = voterSearchPanel;
        }

        private async void SetVoterSearchPanel()
        {
            SetSearchingPanel();            
            
            var voterReturnList = await SearchVoters();
            if (voterReturnList != null && voterReturnList.Count > 0)
            {
                SearchPanel = new VoterSearchPanelView();
                var voterSearchPanel = new VoterSearchPanelViewModel(voterReturnList);
                voterSearchPanel.IsStandardVoter = true;
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
                    _searchLogger.WriteLog("Searching Offline: " + _parameters.Search.ToString());

                    // Do no search if parameters are empty
                    if (!_parameters.Search.IsNullOrEmpty())
                    {
                        var trainingFactory = new TrainingFactory();
                        //_searchLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);
                        return await trainingFactory.SearchVoterAsync(_parameters.Search);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    // Log error message
                    _searchLogger.WriteLog("Database Error: " + e.Message);
                    //OfflineFactory offlineFactory = new OfflineFactory();
                    //_searchLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);

                    AlertDialog connectionFailed = new AlertDialog("COULD NOT FIND LOCAL DATABASE");
                    connectionFailed.ShowDialog();

                    return new List<VoterDataModel>();
                }
            }
            else if (AppSettings.OfflineMode == false)
            {
                try
                {
                    _searchLogger.WriteLog("Searching API: " + _parameters.Search.ToString());

                    // Do no search if parameters are empty
                    if (!_parameters.Search.IsNullOrEmpty())
                    {
                        var voterFactory = new VoterFactory();
                        return await voterFactory.SearchVoterAsync(_parameters.Search);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    // Log error message
                    _searchLogger.WriteLog("API Error: " + e.Message);

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
                    _searchLogger.WriteLog("Searching Offline: " + _parameters.Search.ToString());

                    // Do no search if parameters are empty
                    if (!_parameters.Search.IsNullOrEmpty())
                    {
                        var offlineFactory = new OfflineFactory();
                        //_searchLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);
                        return await offlineFactory.SearchVoterAsync(_parameters.Search);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    // Log error message
                    _searchLogger.WriteLog("Database Error: " + e.Message);
                    //OfflineFactory offlineFactory = new OfflineFactory();
                    //_searchLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);

                    AlertDialog connectionFailed = new AlertDialog("COULD NOT FIND LOCAL DATABASE");
                    connectionFailed.ShowDialog();

                    return new List<VoterDataModel>();
                }
            }
        }

        private void OnSearchParametersPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VoterSearch")
            {
                if(((VoterSearchViewModelBase)sender).Type == "NAME")
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
                _parameters.Search = new VoterSearchModel();
                _parameters.Search.SDBN = AppSettings.System.APIDB;

                // Clear search panel
                ClearSearchPanel();

                if (((VoterSearchViewModelBase)sender).Type == "DISPLAY")
                {
                    SetMonthSearchPanel();
                }                
            }
            else if (e.PropertyName == "VoterScanSearch")
            {
                // Switch Search Parameters Panel
                SetScanSearchPanel();
            }
            else if (e.PropertyName == "VoterNameSearch")
            {
                // Clear the search parameters
                _parameters.Search = new VoterSearchModel();
                _parameters.Search.SDBN = AppSettings.System.APIDB;

                // Switch Search Parameters Panel
                SetNameSearchPanel();
                ClearSearchPanel();
            }
            else if (e.PropertyName == "VoterDateSearch")
            {
                // Clear the search parameters
                _parameters.Search = new VoterSearchModel();
                _parameters.Search.SDBN = AppSettings.System.APIDB;

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

        private void OnVoterSelectedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedVoter")
            {
                // Reset error message panel
                if(SearchParametersCenterBuffer != null)
                {
                    SwapPanelMessage(null, false);
                }

                //Code to respond to a change in the ViewModel
                Console.WriteLine(
                    "Selected Voter: " +
                    ((VoterSearchPanelViewModel)sender).SelectedVoter.VoterID.ToString());

                // Get selected voter
                var selectedVoter = ((VoterSearchPanelViewModel)sender).SelectedVoter;
                _parameters.Voter = selectedVoter;

                //if (AppSettings.OfflineMode == false)
                //{
                //    var userId = AppSettings.User.LocationId;

                //    ElectionFactory factory = new ElectionFactory();
                //    var locationJurisdictions = await factory.LocationJurisdictions();
                //}
                //else
                //{

                //}

                if (AppSettings.OfflineMode == true)
                {
                    // Display offline message
                    OfflineMessage("THIS VOTER WILL BE PROCCESSED OFFLINE", true);
                }
                else
                {
                    ProcessVoter(selectedVoter);
                }
            }
        }

        private void OnMessagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOk")
            {
                SwapPanelMessage(null, false);
            }
        }

        private void ProcessVoter(VoterDataModel selectedVoter)
        {
            //Navigation.SignatureCaptureView(_parameters);

            // When voter has not voted / Status less than 7
            if (selectedVoter.LogCode < 7 || selectedVoter.LogCode == null)
            {
                if (AppSettings.Config.DistrictOnlyVoting == true)
                {
                    // Validate Voters district
                    if (AppSettings.Location.Validate(AppSettings.User.LocationId, selectedVoter.JurisdictionID))
                    {
                        CheckChangeDistrict();
                    }
                    // When change district is on
                    else if (AppSettings.Config.DistrictSignIn == true)
                    {
                        // Change District
                        Navigation.ChangeDistrictView(_parameters);
                    }
                    else
                    {
                        //((VoterSearchPanelViewModel)sender).IsEnabled = false;
                        //AlertDialog alreadyVotedMessage = new AlertDialog("THIS VOTER CONNOT VOTE AT THIS LOCATION");
                        //alreadyVotedMessage.ShowDialog();

                        SwapPanelMessage("THIS VOTER CONNOT VOTE AT THIS LOCATION", true);
                    }
                }
                else
                {
                    CheckChangeDistrict();
                }
            }
            // When voter has voted
            //else if (AppSettings.Config.SpoilBallots == true)
            //{
            //    // Spoil Ballots
            //    Navigation.SpoiledBallotView(_parameters);
            //}
            else
            {
                //((VoterSearchPanelViewModel)sender).IsEnabled = false;
                //SearchPanel = null;
                //RaisePropertyChanged("SearchPanel");

                //AlertDialog alreadyVotedMessage = new AlertDialog("THIS VOTER ALREADY HAS AN ACCEPTED BALLOT");
                //alreadyVotedMessage.ShowDialog();

                SwapPanelMessage("THIS VOTER ALREADY HAS AN ACCEPTED BALLOT", true);
            }
        }

        private void CheckChangeDistrict()
        {
            // When change district is on
            if (AppSettings.Config.DistrictSignIn == true)
            {
                // Change District
                Navigation.ChangeDistrictView(_parameters);
            }
            else
            {
                // Capture Signature
                Navigation.SignatureCaptureView(_parameters);
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

        private void SetScanSearchPanel()
        {
            SearchParametersPanel = new VoterScanSearchView();
            var searchParameters = new VoterScanSearchViewModel();
            searchParameters.PropertyChanged += OnSearchParametersPropertyChanged;
            SearchParametersPanel.DataContext = searchParameters;
            RaisePropertyChanged("SearchParametersPanel");
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

        private void SwapPanelMessage(string message, bool isShowMessage)
        {
            if (isShowMessage)
            {
                SearchParametersCenterBuffer = SearchParametersCenterPanel;

                SearchParametersCenterPanel = new MessagePanelView();
                var messagePanel = new MessagePanelViewModel(message);
                messagePanel.PropertyChanged += OnMessagePropertyChanged;
                SearchParametersCenterPanel.DataContext = messagePanel;
                RaisePropertyChanged("SearchParametersCenterPanel");
            }
            else
            {
                SearchParametersCenterPanel = SearchParametersCenterBuffer;
                RaisePropertyChanged("SearchParametersCenterPanel");

                SearchParametersCenterBuffer = null;
            }
        }

        private void OfflineMessage(string message, bool isShowMessage)
        {
            if (isShowMessage)
            {
                SearchParametersCenterBuffer = SearchParametersCenterPanel;

                SearchParametersCenterPanel = new MessagePanelView();
                var messagePanel = new MessagePanelViewModel(message);
                messagePanel.PropertyChanged += OfflineMessagePropertyChanged;
                SearchParametersCenterPanel.DataContext = messagePanel;
                RaisePropertyChanged("SearchParametersCenterPanel");
            }
            else
            {
                SearchParametersCenterPanel = SearchParametersCenterBuffer;
                RaisePropertyChanged("SearchParametersCenterPanel");

                SearchParametersCenterBuffer = null;
            }
        }

        private void OfflineMessagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOk")
            {
                SwapPanelMessage(null, false);

                // Get selected voter
                var selectedVoter = ((VoterSearchPanelViewModel)SearchPanel.DataContext).SelectedVoter;
                _parameters.Voter = selectedVoter;

                ProcessVoter(selectedVoter);
            }
        }

    }
}
