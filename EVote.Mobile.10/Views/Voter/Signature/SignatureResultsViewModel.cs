using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using EVote.Factories;
using EVote.Methods;
using EVote.Utilities.Commands;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class SignatureResultsViewModel : ViewModelBase
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

        public bool? IsBallotNumber
        {
            get
            {
                return AppSettings.Config.BallotNumOnSig;
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

        public SignatureResultsViewModel(IViewParametersModel Parameters)
        {
            _parameters = Parameters;
            //Voter = Parameters.Voter;
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
            if (AppSettings.Config.BallotNumOnSig == true && Voter.BallotNumber == null)
            {
                // Display Error Message
                AlertDialog requiredMessage = new AlertDialog("PLEASE ENTER A VALID BALLOT NUMBER");
                requiredMessage.ShowDialog();
            }
            else
            {
                VoterDataMethods.VotedAtPolls(Voter);

                bool printBallots = AppSettings.System.PrintBallots;

                if (printBallots == true)
                {
                    Navigation.BallotPrintView(_parameters);
                }
                else
                {
                    // Return to voter lookup screen
                    Navigation.VoterSearchView(new VoterViewParametersModel());
                }
            }
            
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
            // Return to signature screen
            Navigation.SignatureCaptureView(_parameters);
        }
        #endregion
    }
}
