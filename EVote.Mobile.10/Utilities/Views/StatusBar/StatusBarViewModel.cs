using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Utilities.Views
{
    public class StatusBarViewModel : ViewModelBase
    {
        public StatusBarViewModel()
        {
            TextLeft = " ";
            SpinnerLeft = false;

            WifiIcon = true;
            SetWifiStatus(true);
            //WifiColor = "ApplicationForegroundBrush";

            //SetOfflineStatus(true);
        }

        #region StatusLeft
        private string _textLeft;
        public string TextLeft 
        { 
            get
            {
                return _textLeft;
            }
            set
            {
                _textLeft = value;
                RaisePropertyChanged("TextLeft");
            } 
        }

        private bool _spinnerLeft;
        public bool SpinnerLeft
        {
            get
            {
                return _spinnerLeft;
            }
            set
            {
                _spinnerLeft = value;
                RaisePropertyChanged("SpinnerLeft");
            }
        }
        #endregion

        #region WifiIcon
        private bool _wifiIcon;
        public bool WifiIcon
        {
            get
            {
                return _wifiIcon;
            }
            set
            {
                _wifiIcon = value;
                RaisePropertyChanged("WifiIcon");
            }
        }

        private string _wifiColor;
        public string WifiColor
        {
            get
            {
                return _wifiColor;
            }
            set
            {
                _wifiColor = value;
                RaisePropertyChanged("WifiColor");
            }
        }

        private string _wifiToolTip;
        public string WifiToolTip
        {
            get
            {
                return _wifiToolTip;
            }
            set
            {
                _wifiToolTip = value;
                RaisePropertyChanged("WifiToolTip");
            }
        }

        public void SetWifiStatus(bool status)
        {
            if(status == true)
            {
                WifiToolTip = "Internet Connection Good";
                WifiColor = WifiColor = "ApplicationForegroundBrush";
            }
            else
            {
                WifiToolTip = "Internet Connection Bad";
                WifiColor = "ApplicationDangerBrush";
            }
        }
        #endregion 

        #region OfflineIcon
        private bool _offlineIcon;
        public bool OfflineIcon
        {
            get
            {
                return _offlineIcon;
            }
            set
            {
                _offlineIcon = value;
                RaisePropertyChanged("OfflineIcon");
            }
        }

        private string _offlineColor;
        public string OfflineColor
        {
            get
            {
                return _offlineColor;
            }
            set
            {
                _offlineColor = value;
                RaisePropertyChanged("OfflineColor");
            }
        }

        private string _offlineToolTip;
        public string OfflineToolTip
        {
            get
            {
                return _offlineToolTip;
            }
            set
            {
                _offlineToolTip = value;
                RaisePropertyChanged("OfflineToolTip");
            }
        }

        public void SetOfflineStatus(bool status)
        {
            if (status == true)
            {
                OfflineIcon = true;
                OfflineToolTip = "Offline";
                OfflineColor = "ApplicationDangerBrush";
            }
            else
            {
                OfflineIcon = false;
                OfflineToolTip = "Offline";
                OfflineColor = "ApplicationDangerBrush";
            }
        }
        #endregion 
    }
}
