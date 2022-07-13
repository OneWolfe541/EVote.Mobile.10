using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EVote.Methods
{
    public static class StatusBarMethods
    {
        #region MainHeader
        public static void ShowMenu(bool value)
        {
            ((App)Application.Current).MainHeader.IsMenuVisible = value;
            ((App)Application.Current).MainHeader.UpdateUserName();
        }

        public static void UpdateUser()
        {
            ((App)Application.Current).MainHeader.UpdateUserName();
        }

        public static void SetMenuAdmin()
        {
            ((App)Application.Current).MainHeader.IsAdmin = true;
        }

        public static void SetMenuAdmin(bool status)
        {
            ((App)Application.Current).MainHeader.IsAdmin = status;
        }

        public static void HighlightVoterLookup()
        {
            ((App)Application.Current).MainHeader.ResetMenuButtons();
            ((App)Application.Current).MainHeader.CanLookupVoter = false;
            ((App)Application.Current).MainHeader.CloseManageMenu();
        }

        public static void HighlightEditVoter()
        {
            ((App)Application.Current).MainHeader.ResetMenuButtons();
            ((App)Application.Current).MainHeader.CanEditVoter = false;
        }

        public static void HighlightOfflineMode()
        {
            ((App)Application.Current).MainHeader.ResetMenuButtons();
            ((App)Application.Current).MainHeader.CanOffline = false;
        }

        public static void WifiStatus(bool status)
        {
            ((App)Application.Current).StatusBar.SetWifiStatus(status);
        }
        #endregion

        #region StatusBar
        public static string Textleft
        {
            get
            {                
                return ((App)Application.Current).StatusBar.TextLeft;
            }
            set
            {
                ((App)Application.Current).StatusBar.TextLeft = value;
            }
        }

        public static bool Working
        {
            get
            {
                return ((App)Application.Current).StatusBar.SpinnerLeft;
            }
            set
            {
                ((App)Application.Current).StatusBar.SpinnerLeft = value;
            }
        }
        #endregion
    }
}
