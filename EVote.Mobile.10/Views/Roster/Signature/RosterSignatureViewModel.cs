using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using EVote.Methods;
using EVote.Utilities.Commands;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class RosterSignatureViewModel : ViewModelBase
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

        public bool? CanSpoilBallot
        {
            get
            {
                if ((Voter.LogCode == 11 || Voter.LogCode == 12))
                {
                    return AppSettings.Config.SpoilBallots;
                }
                else
                {
                    return false;
                }
            }
        }

        public BitmapImage VoterSignatureImage
        {
            get
            {
                if (AppSettings.TrainingMode == true)
                {
                    return SignatureMethods.LoadSignatureFromTrainingDatabase(Voter);
                }
                else
                {
                    //return SignatureMethods.LoadSignatureFromFile(Voter, "C:\\Temp\\SigTest");
                    return SignatureMethods.LoadSignatureFromDatabase(Voter);
                }
            }
        }

        public RosterSignatureViewModel(IViewParametersModel Parameters)
        {
            _parameters = Parameters;
        }

        #region Commands
        private RelayCommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(param => this.SignatureOkClick());
                }
                return _okCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void SignatureOkClick()
        {
            // Return to roster lookup screen
            Navigation.RosterSearchView();
            
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
            // Return to roster lookup screen
            Navigation.RosterSearchView();
        }

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
            if ((Voter.LogCode == 11 || Voter.LogCode == 12))
            {
                if (Voter.LocationID == AppSettings.User.LocationId)
                {
                    YesNoDialog areYouSureMessage = new YesNoDialog("ARE YOU SURE YOU WANT TO SPOIL THIS VOTER'S BALLOT?");
                    if (areYouSureMessage.ShowDialog() == true)
                    {
                        VoterDataMethods.SpoilBallot(Voter, 1);

                        if (AppSettings.System.PrintBallots == true)
                        {
                            PrintBallot();
                        }
                    }
                }
                else
                {
                    AlertDialog alreadyVotedMessage = new AlertDialog("CANNOT SPOIL THE VOTER'S BALLOT AT THIS LOCATION");
                    alreadyVotedMessage.ShowDialog();
                }
            }
            else
            {
                AlertDialog alreadyVotedMessage = new AlertDialog("VOTER DOES NOT HAVE A BALLOT TO SPOIL");
                alreadyVotedMessage.ShowDialog();
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
        #endregion
    }
}
