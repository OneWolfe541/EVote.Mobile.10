using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using EVote.Utilities.Commands;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{
    public class VoterNameSearchViewModel : VoterSearchViewModelBase
    {
        public VoterNameSearchViewModel()
        {
            Type = "NAME";
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
            //closeOnscreenKeyboard();

            //Process[] processes = Process.GetProcessesByName("TabTip");
            //foreach (Process process in processes)
            //{
            //    process.Kill();
            //}

            _voterSearch = new VoterSearchModel
            {
                FirstName = NameFirst,
                LastName = NameLast,
                BirthYear = BirthYear
            };
            RaisePropertyChanged("VoterSearch");
        }

        private RelayCommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(param => this.ClearParametersClick());
                }
                return _clearCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void ClearParametersClick()
        {
            _voterSearch = null;

            NameFirst = null;
            RaisePropertyChanged("NameFirst");
            NameLast = string.Empty;
            RaisePropertyChanged("NameLast");
            BirthYear = null;
            RaisePropertyChanged("BirthYear");

            RaisePropertyChanged("VoterClear");
        }

        private RelayCommand _scanCommand;
        public ICommand ScanCommand
        {
            get
            {
                if (_scanCommand == null)
                {
                    _scanCommand = new RelayCommand(param => this.ScanClick());
                }
                return _scanCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void ScanClick()
        {
            _voterSearch = null;

            NameFirst = null;
            RaisePropertyChanged("NameFirst");
            NameLast = string.Empty;
            RaisePropertyChanged("NameLast");
            BirthYear = null;
            RaisePropertyChanged("BirthYear");

            RaisePropertyChanged("VoterClear");
            RaisePropertyChanged("VoterScanSearch");
        }

        private RelayCommand _dateCommand;
        public ICommand DateCommand
        {
            get
            {
                if (_dateCommand == null)
                {
                    _dateCommand = new RelayCommand(param => this.DateClick());
                }
                return _dateCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void DateClick()
        {
            _voterSearch = null;

            NameFirst = null;
            RaisePropertyChanged("NameFirst");
            NameLast = string.Empty;
            RaisePropertyChanged("NameLast");
            BirthYear = null;
            RaisePropertyChanged("BirthYear");

            RaisePropertyChanged("VoterClear");
            RaisePropertyChanged("VoterDateSearch");
        }
        #endregion

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        private void closeOnscreenKeyboard()
        {
            // retrieve the handler of the window  
            int iHandle = FindWindow("IPTIP_Main_Window", "");
            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
            }
        }
    }
}
