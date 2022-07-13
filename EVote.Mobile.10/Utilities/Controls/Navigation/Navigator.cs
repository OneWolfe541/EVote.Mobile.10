using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using EVote.Utilities.Commands;

namespace EVote.Utilities.Controls
{
    public class Navigator : NotifyPropertyChanged, INavigator
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        private UserControl _currentMenu;
        public UserControl CurrentMenu
        {
            get
            {
                return _currentMenu;
            }

            set
            {
                _currentMenu = value;
                RaisePropertyChanged("CurrentMenu");
            }
        }
    }
}
