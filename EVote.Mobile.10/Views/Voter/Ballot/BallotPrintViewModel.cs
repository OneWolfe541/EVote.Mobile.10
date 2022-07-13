using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Factories;
using EVote.Methods;
using EVote.Utilities.Commands;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Models;

namespace EVote.Views
{
    public class BallotPrintViewModel
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

        public BallotPrintViewModel(IViewParametersModel Parameters)
        {
            _parameters = Parameters;

            PrintBallot();
        }

        #region Commands
        private RelayCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                if (_printCommand == null)
                {
                    _printCommand = new RelayCommand(param => this.PrintClick());
                }
                return _printCommand;
            }
        }

        // Print the selected ballot style
        private void PrintClick()
        {
            YesNoDialog areYouSureMessage = new YesNoDialog("ARE YOU SURE YOU WANT TO PRINT ANOTHER BALLOT?");
            if (areYouSureMessage.ShowDialog() == true)
            {
                PrintBallot();
            }
        }

        private void PrintBallot()
        {
            string ballotPath = "C:\\EVote\\Ballots\\" + Voter.BallotStyleFileName;

            // Print the voter's ballot
            string message = PDFToolsMethods.PrintPDF(
                        AppSettings.System.BallotPrinter,           // Printer Name
                        ballotPath,                                 // Ballot PDF File
                        "Print Official Ballot",                    // Job Name
                        AppSettings.System.BallotBin,               // Ballot Paper Tray
                        (short)AppSettings.System.BallotSize,       // Ballot Paper Size
                        1,                                          // PDF Page Number
                        false,
                        false,
                        //"1-UCAE4-X1374-GCBGU-3LA2U-B5NQ1-G5FXW-MWT3X"
                        AppSettings.System.PDFTools
                        );
        }

        private RelayCommand _ballotOkCommand;
        public ICommand BallotOkCommand
        {
            get
            {
                if (_ballotOkCommand == null)
                {
                    _ballotOkCommand = new RelayCommand(param => this.BallotOkClick());
                }
                return _ballotOkCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void BallotOkClick()
        {
            if(AppSettings.OfflineMode == true)
            {
                StatusBarMethods.HighlightOfflineMode();
            }

            // Return to voter lookup screen
            Navigation.VoterSearchView(new VoterViewParametersModel());
        }

        private RelayCommand _cancelCommand;
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

        // Force parent frame to navigate back to the search page
        private void CancelClick()
        {
            // Return to voter lookup screen
            Navigation.VoterSearchView(new VoterViewParametersModel());
        }
        #endregion
    }
}
