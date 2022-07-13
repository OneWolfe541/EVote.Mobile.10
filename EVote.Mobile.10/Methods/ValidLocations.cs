using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EVote.Factories;
using EVote.LocalDatabase;
using EVote.Logging;
using EVote.Settings;
using EVote.Utilities.Dialogs;

namespace EVote.Methods
{
    public static class ValidLocationMethods
    {
        public static async Task<List<LocationJurisdiction>> LoadAsync()
        {
            EVoteLogger _locationsLogger = new EVoteLogger("EVoteLogs", true);

            List<LocationJurisdiction> locations = null;

            // Load Settings from API
            try
            {
                ElectionFactory factory = new ElectionFactory();
                locations = await factory.LocationJurisdictions();
            }
            catch (Exception e)
            {
                // Log error message
                _locationsLogger.WriteLog("API Error: " + e.Message);

                StatusBarMethods.WifiStatus(false);
            }

            try
            {
                OfflineFactory offlineFactory = new OfflineFactory();
                // Save or Load Localy
                if (locations != null)
                {
                    //OfflineFactory offlineFactory = new OfflineFactory();
                    offlineFactory.SaveLocationJurisdictions(locations);
                }
                else
                {
                    //OfflineFactory offlineFactory = new OfflineFactory();
                    locations = await offlineFactory.LocationJurisdictions();
                }
            }
            catch (Exception e)
            {
                // Log error message
                _locationsLogger.WriteLog("Database Error: " + e.Message);
                OfflineFactory offlineFactory = new OfflineFactory();
                _locationsLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);

                AlertDialog connectionFailed = new AlertDialog("COULD NOT FIND LOCAL DATABASE");
                connectionFailed.ShowDialog();

                return null;
            }

            return locations;
        }

        public static bool Validate(this List<LocationJurisdiction> list, int location, int? jurisdiction)
        {
            bool result = false;

            if(list != null && jurisdiction != null)
            {
                foreach(var item in list)
                {
                    if(item.LocationId == location && item.JurisdictionId == jurisdiction)
                    {
                        return true;
                    }
                }
            }

            return result;
        }

        public static async void SaveAsync(List<LocationJurisdiction> locations)
        {
            EVoteLogger _locationsLogger = new EVoteLogger("EVoteLogs", true);

            // Save to local database
            try
            {
                OfflineFactory offlineFactory = new OfflineFactory();
                // Save or Load Localy
                if (locations != null)
                {
                    //OfflineFactory offlineFactory = new OfflineFactory();
                    offlineFactory.SaveLocationJurisdictions(locations);
                }
            }
            catch (Exception e)
            {
                // Log error message
                _locationsLogger.WriteLog("Database Error: " + e.Message);
                OfflineFactory offlineFactory = new OfflineFactory();
                _locationsLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);

                AlertDialog connectionFailed = new AlertDialog("COULD NOT FIND LOCAL DATABASE");
                connectionFailed.ShowDialog();
            }

            // Save to API
            try
            {
                ElectionFactory factory = new ElectionFactory();
                if (locations != null)
                {
                    locations = await factory.SaveLocationJurisdictions(locations);
                }
            }
            catch (Exception e)
            {
                // Log error message
                _locationsLogger.WriteLog("API Error: " + e.Message);

                StatusBarMethods.WifiStatus(false);
            }
        }
    }
}
