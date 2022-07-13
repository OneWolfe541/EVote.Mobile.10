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
    public static class ElectionConfigs
    {
        public static async Task<ElectionSettingsModel> LoadAsync()
        {
            EVoteLogger _settingsLogger = new EVoteLogger("EVoteLogs", true);

            List<Config> configs = null;

            // Load Settings from API
            try
            {
                ElectionFactory factory = new ElectionFactory();
                configs = await factory.Configs();
            }
            catch (Exception e)
            {
                // Log error message
                _settingsLogger.WriteLog("API Error: " + e.Message);

                StatusBarMethods.WifiStatus(false);
            }

            try
            {
                OfflineFactory offlineFactory = new OfflineFactory();
                // Save or Load Localy
                if (configs != null)
                {
                    //OfflineFactory offlineFactory = new OfflineFactory();
                    offlineFactory.SaveConfigs(configs);
                }
                else
                {
                    //OfflineFactory offlineFactory = new OfflineFactory();
                    configs = await offlineFactory.Configs();
                }
            }
            catch (Exception e)
            {
                // Log error message
                _settingsLogger.WriteLog("Database Error: " + e.Message);
                OfflineFactory offlineFactory = new OfflineFactory();
                _settingsLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);

                AlertDialog connectionFailed = new AlertDialog("COULD NOT FIND LOCAL DATABASE");
                connectionFailed.ShowDialog();

                return null;
            }

            if (configs != null)
            {
                ElectionSettingsModel settings = new ElectionSettingsModel();
                foreach(var config in configs)
                {
                    switch(config.ConfigSetting)
                    {
                        case "BallotNumOnSig":
                            if (Boolean.TryParse(config.ConfigValue, out bool newBool1))
                            {
                                settings.BallotNumOnSig = newBool1;
                            }                            
                            break;
                        case "DistrictOnlyVoting":
                            if (Boolean.TryParse(config.ConfigValue, out bool newBool2))
                            {
                                settings.DistrictOnlyVoting = newBool2;
                            }
                            break;
                        case "DistrictSignIn":
                            if (Boolean.TryParse(config.ConfigValue, out bool newBool3))
                            {
                                settings.DistrictSignIn = newBool3;
                            }
                            break;
                        case "ElectionDate":
                            //if (string.TryParse(config.ConfigValue, out DateTime date))
                            //{
                                settings.ElectionDate = config.ConfigValue;
                            //}
                            break;
                        case "ElectionName":
                            settings.ElectionName = config.ConfigValue;
                            break;
                        case "ShowDistrict":
                            if (Boolean.TryParse(config.ConfigValue, out bool newBool6))
                            {
                                settings.ShowDistrict = newBool6;
                            }
                            break;
                        case "ShowEDActivity":
                            if (Boolean.TryParse(config.ConfigValue, out bool newBool7))
                            {
                                settings.ShowEDActivity = newBool7;
                            }
                            break;
                        case "ShowEVActivity":
                            if (Boolean.TryParse(config.ConfigValue, out bool newBool8))
                            {
                                settings.ShowEVActivity = newBool8;
                            }
                            break;
                        case "SpoilBallots":
                            if (Boolean.TryParse(config.ConfigValue, out bool newBool9))
                            {
                                settings.SpoilBallots = newBool9;
                            }
                            break;
                        case "TimeAdjust":
                            if (Int32.TryParse(config.ConfigValue, out int newint))
                            {
                                settings.TimeAdjust = newint;
                            }
                            break;
                    }
                }

                return settings;
            }
            else
            {
                return new ElectionSettingsModel();
            }
        }

        public static async void SaveAsync(ElectionSettingsModel settings)
        {
            EVoteLogger _settingsLogger = new EVoteLogger("EVoteLogs", true);

            List<Config> configs = null;

            try
            {
                OfflineFactory offlineFactory = new OfflineFactory();
                configs = await offlineFactory.Configs();
            }
            catch (Exception e)
            {
                // Log error message
                _settingsLogger.WriteLog("Database Error: " + e.Message);
                OfflineFactory offlineFactory = new OfflineFactory();
                _settingsLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);

                AlertDialog connectionFailed = new AlertDialog("COULD NOT FIND LOCAL DATABASE");
                connectionFailed.ShowDialog();
            }

            if (configs != null)
            {
                foreach (var config in configs)
                {
                    switch (config.ConfigSetting)
                    {
                        case "BallotNumOnSig":
                            config.ConfigValue = settings.BallotNumOnSig.ToString();
                            break;
                        case "DistrictOnlyVoting":
                            config.ConfigValue = settings.DistrictOnlyVoting.ToString();
                            break;
                        case "DistrictSignIn":
                            config.ConfigValue = settings.DistrictSignIn.ToString();
                            break;
                        case "ElectionDate":
                             config.ConfigValue = settings.ElectionDate;
                            break;
                        case "ElectionName":
                            config.ConfigValue = settings.ElectionName;
                            break;
                        case "ShowDistrict":
                            config.ConfigValue = settings.ShowDistrict.ToString();
                            break;
                        case "ShowEDActivity":
                            config.ConfigValue = settings.ShowEDActivity.ToString();
                            break;
                        case "ShowEVActivity":
                            config.ConfigValue = settings.ShowEVActivity.ToString();
                            break;
                        case "SpoilBallots":
                            config.ConfigValue = settings.SpoilBallots.ToString();
                            break;
                        case "TimeAdjust":
                            config.ConfigValue = settings.TimeAdjust.ToString();
                            break;
                    }
                }
            }

            try
            {
                OfflineFactory offlineFactory = new OfflineFactory();
                offlineFactory.SaveConfigs(configs);
            }
            catch (Exception e)
            {
                // Log error message
                _settingsLogger.WriteLog("Database Error: " + e.Message);
                OfflineFactory offlineFactory = new OfflineFactory();
                _settingsLogger.WriteLog("Connection String: " + offlineFactory.ConnectionString);

                AlertDialog connectionFailed = new AlertDialog("COULD NOT FIND LOCAL DATABASE");
                connectionFailed.ShowDialog();
            }

            // Save Settings to API
            try
            {
                ElectionFactory factory = new ElectionFactory();
                await factory.SaveConfigsAsync(configs);
            }
            catch (Exception e)
            {
                // Log error message
                _settingsLogger.WriteLog("API Error: " + e.Message);

                StatusBarMethods.WifiStatus(false);
            }
        }
    }
}
