using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EVote.Factories;
using EVote.Logging;
using EVote.Methods;
using EVote.Settings;
using EVote.Utilities.Commands;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class SystemSettingsViewModel : ViewModelBase
    {
        public SystemSettingsModel Settings
        {
            get
            {
                return AppSettings.System;
            }
            set
            {
                AppSettings.System = value;
                RaisePropertyChanged("Settings");
            }
        }

        public bool Offline
        {
            get
            {
                return AppSettings.System.OfflineMode;
            }
            set
            {
                AppSettings.System.OfflineMode = value;
                AppSettings.OfflineMode = value;
                RaisePropertyChanged("Offline");
            }
        }

        public SystemSettingsViewModel()
        {
            
        }

        #region Printers
        public List<PrinterLookupModel> Printers
        {
            get
            {
                return AppSettings.Printers.Printers;
            }
        }

        private PrinterLookupModel _selectedBallotPrinter;
        public PrinterLookupModel SelectedBallotPrinter
        {
            get
            {
                if (_selectedBallotPrinter == null)
                {
                    _selectedBallotPrinter = Printers.Where(j => j.PrinterName.ToUpper() == AppSettings.System.BallotPrinter).FirstOrDefault();
                }
                return _selectedBallotPrinter;
            }

            set
            {
                AppSettings.System.BallotPrinter = value.PrinterName;
                _selectedBallotPrinter = value;

                RaisePropertyChanged("SelectedBallotPrinter");

                RaisePropertyChanged("PaperSizes");
                RaisePropertyChanged("SelectedPaperSize");

                RaisePropertyChanged("PaperTrays");
                RaisePropertyChanged("SelectedPaperTray");
            }
        }
        #endregion

        #region PaperSizes
        public List<PaperSize> PaperSizes
        {
            get
            {
                if (SelectedBallotPrinter != null)
                {
                    return SelectedBallotPrinter.PaperSizes;
                }
                else
                {
                    return null;
                }
            }
        }

        private PaperSize _selectedPaperSize;
        public PaperSize SelectedPaperSize
        {
            get
            {
                if (_selectedPaperSize == null)
                {
                    try
                    {
                        _selectedPaperSize = PaperSizes.Where(j => j.Index == AppSettings.System.BallotSize).FirstOrDefault();
                    }
                    catch { }

                }
                return _selectedPaperSize;
            }

            set
            {
                AppSettings.System.BallotSize = value.Index;
                _selectedPaperSize = value;
                RaisePropertyChanged("SelectedPaperSize");
            }
        }
        #endregion

        #region PaperTrays
        public List<PrinterTray> PaperTrays
        {
            get
            {
                if (SelectedBallotPrinter != null)
                {
                    return SelectedBallotPrinter.Bins;
                }
                else
                {
                    return null;
                }
            }
        }

        private PrinterTray _selectedPaperTray;
        public PrinterTray SelectedPaperTray
        {
            get
            {
                if (_selectedPaperTray == null)
                {
                    try
                    {
                        _selectedPaperTray = PaperTrays.Where(j => j.Index == AppSettings.System.BallotBin).FirstOrDefault();
                    }
                    catch { }
                }
                return _selectedPaperTray;
            }

            set
            {
                AppSettings.System.BallotBin = value.Index;
                _selectedPaperTray = value;
                RaisePropertyChanged("SelectedPaperTray");
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
            AlertDialog connectionFailed = new AlertDialog("SETTINGS HAVE BEEN SAVED");
            connectionFailed.ShowDialog();

            AppSettings.SaveSettings();
        }

        // Bound command for uploading offlinf voter records
        public RelayCommand _uploadCommand;
        public ICommand UploadCommand
        {
            get
            {
                if (_uploadCommand == null)
                {
                    _uploadCommand = new RelayCommand(param => this.UploadClick());
                }
                return _uploadCommand;
            }
        }

        //Enable or Disable the Upload Button
        private bool _canUpload;
        public bool CanUpload
        {
            get { return _canUpload; }
            internal set
            {
                _canUpload = value;
                RaisePropertyChanged("CanSave");
            }
        }

        // Upload the voter records
        public async void UploadClick()
        {
            EVoteLogger uploadLogger = new EVoteLogger("EVoteLogs", true);

            if (AppSettings.System.OfflineMode == true)
            {
                AlertDialog offlineMessage = new AlertDialog("Cannot upload in Offline Mode");
                offlineMessage.ShowDialog();
            }
            else
            {
                var offlineFactory = new OfflineFactory();
                var voterList = await offlineFactory.SearchOfflineVotersAsync();

                // Check if there are any offline records
                if(voterList == null || voterList.Count() == 0)
                {
                    AlertDialog noVotersMessage = new AlertDialog("No Offline Voters Found");
                    noVotersMessage.ShowDialog();
                }
                else if(voterList.Count() > 50)
                {
                    YesNoDialog highVolumeMessage = new YesNoDialog("More Than 50 Offline Voters Were Found\r\nAre you sure you want to upload them?");
                    if(highVolumeMessage.ShowDialog() == true)
                    {
                        UploadOfflineVoters(voterList);

                        AlertDialog uploadedMessage = new AlertDialog("Offline Records Have Been Uploaded");
                        uploadedMessage.ShowDialog();
                    }
                }
                else
                {
                    string total = voterList.Count().ToString();
                    YesNoDialog uploadMessage = new YesNoDialog(total + " Offline Voter(s) Were Found\r\nAre you sure you want to upload them?");
                    if (uploadMessage.ShowDialog() == true)
                    {
                        uploadLogger.WriteLog("Uploading " + total + " Offline Records");

                        UploadOfflineVoters(voterList);

                        AlertDialog uploadedMessage = new AlertDialog("Offline Records Have Been Uploaded");
                        uploadedMessage.ShowDialog();
                    }
                }
            }
        }

        private async void UploadOfflineVoters(List<VoterDataModel> voters)
        {
            if (voters != null || voters.Count() > 0)
            {
                EVoteLogger uploadLogger = new EVoteLogger("EVoteLogs", true);

                int uploadCount = 0;

                var voterFactory = new VoterFactory();
                foreach (var voter in voters)
                {
                    uploadLogger.WriteLog("Uploading Voter: " + voter.VoterID.ToString());

                    voter.SDBN = AppSettings.System.APIDB;
                    voter.CodeGroupState = "UPLOADED OFFLINE RECORD";

                    var results = await voterFactory.MarkVoterAsync(voter);

                    if (results != null)
                    {
                        if (results.Result != null && results.Result.VoterId != null)
                        {
                            uploadCount++;
                            uploadLogger.WriteLog("Voter Uploaded: [" + results.Result.VoterId.ToString() + "]");
                        }
                        else if (results.Error != null)
                        {
                            uploadLogger.WriteLog("Upload Failed: [" + voter.VoterID.ToString() + "] " + results.Error.ToString());
                        }
                    }

                    uploadLogger.WriteLog("Uploading Signature");
                    //var signature = SignatureMethods.LoadSignatureFromDatabase(voter);
                    var offlineFactory = new OfflineFactory();
                    var signature = await offlineFactory.Signatures(voter);

                    await voterFactory.UploadSignature(new EVote.Utilities.Models.Signature(signature), AppSettings.System.APIDB);
                }

                uploadLogger.WriteLog("Upload Total: " + uploadCount.ToString());
            }
        }
        #endregion
    }
}
