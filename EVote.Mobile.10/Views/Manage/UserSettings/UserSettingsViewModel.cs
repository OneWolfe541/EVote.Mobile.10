using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EVote.Methods;
using EVote.Settings;
using EVote.Utilities.Commands;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class UserSettingsViewModel : ViewModelBase
    {
        public bool Offline
        {
            get
            {
                return AppSettings.OfflineMode;
            }
            set
            {
                AppSettings.OfflineMode = value;
                RaisePropertyChanged("Offline");
            }
        }
    }
}
