using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EVote.LocalDatabase;
using EVote.Settings;

namespace EVote.Methods
{
    public static class AppSettings
    {
        public static SystemSettingsModel System
        {
            get { return ((App)Application.Current).GlobalSettings.Settings; }
            set { ((App)Application.Current).GlobalSettings.Settings = value; }
        }

        public static ElectionSettingsModel Config
        {
            get { return ((App)Application.Current).GlobalSettings.Election; }
            set { ((App)Application.Current).GlobalSettings.Election = value; }
        }

        public static List<LocationJurisdiction> Location
        {
            get { return ((App)Application.Current).GlobalSettings.LocationJurisdictions; }
            set { ((App)Application.Current).GlobalSettings.LocationJurisdictions = value; }
        }

        public static bool OfflineMode
        {
            get { return ((App)Application.Current).GlobalSettings.OfflineMode; }
            set 
            {
                ((App)Application.Current).StatusBar.SetOfflineStatus(value);
                ((App)Application.Current).GlobalSettings.OfflineMode = value; 
            }
        }

        public static bool TrainingMode
        {
            get { return ((App)Application.Current).GlobalSettings.User.RollId == 4; }            
        }

        public static UserSettingsModel User
        {
            get { return ((App)Application.Current).GlobalSettings.User; }
            set { ((App)Application.Current).GlobalSettings.User = value; }
        }

        public static void SaveSettings()
        {
            ((App)Application.Current).GlobalSettings.SaveSettings();
        }

        public static void SetUser(Location location)
        {
            if (location != null)
            {
                System.SiteId = location.LocationId;

                User = new UserSettingsModel()
                {
                    LocationId = location.LocationId,
                    LocationName = location.LocationName,
                    Address = location.Address,
                    City = location.City,
                    State = location.State,
                    Zip = location.Zip,
                    Login = location.Login,
                    RollId = location.RollId,
                    LastModified = location.LastModified,
                    Active = location.Active
                };

                ((App)Application.Current).MainHeader.UpdateUserName();
            }
        }

        public static Task<bool> LoadPrinters()
        {
            return ((App)Application.Current).GlobalSettings.LoadPrinterLists();
        }

        public static PrinterLists Printers
        {
            get { return ((App)Application.Current).GlobalSettings.Printers; }
            set { ((App)Application.Current).GlobalSettings.Printers = value; }
        }
    }
}
