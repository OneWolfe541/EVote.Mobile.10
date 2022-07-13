using System;
using System.Collections.Generic;
using System.Text;
using EVote.Factories;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Extensions;
using EVote.Utilities.Models;

namespace EVote.Methods
{
    public static class VoterDataMethods
    {
        public static async void VotedAtPolls(VoterDataModel Voter)
        {
            // Mark voted record
            if (AppSettings.User.RollId == 1)
            {
                Voter.LogCode = 11;
            }
            else
            {
                Voter.LogCode = 12;
            }
            Voter.DateIssued = DateTime.Now;
            Voter.PrintedDate = DateTime.Now;
            Voter.ActivityDate = DateTime.Now;
            Voter.DateVoted = DateTime.Now;
            Voter.CodeGroupState = "VOTED AT POLLS";

            Voter.LocationID = AppSettings.User.LocationId;
            //Voter.BallotNumber = 1;

            if (Voter.DeliveryAddress1 == null) Voter.DeliveryAddress1 = Voter.PhysicalAddress1;
            if (Voter.DeliveryAddress2 == null) Voter.DeliveryAddress2 = Voter.PhysicalAddress2;
            if (Voter.DeliveryCity == null) Voter.DeliveryCity = Voter.PhysicalCity;
            if (Voter.DeliveryState == null) Voter.DeliveryState = Voter.PhysicalState;
            if (Voter.DeliveryZip == null) Voter.DeliveryZip = Voter.PhysicalZip;
            if (Voter.DeliveryCountry == null) Voter.DeliveryCountry = Voter.PhysicalCountry;

            Voter.LocalOnly = AppSettings.OfflineMode;

            Voter.SDBN = AppSettings.System.APIDB;

            if (AppSettings.TrainingMode == false)
            {
                // Save changes to local database
                var offlineFactory = new OfflineFactory();
                offlineFactory.MarkVoterAsync(Voter);

                if (AppSettings.OfflineMode == false)
                {
                    // Upload changes to API
                    var voterFactory = new VoterFactory();
                    await voterFactory.MarkVoterAsync(Voter);
                }
            }
            else
            {
                // Save changes to local database
                var trainingFactory = new TrainingFactory();
                trainingFactory.MarkVoterAsync(Voter);
            }
        }

        public static async void SaveVoter(VoterDataModel Voter)
        {
            Voter.ActivityDate = DateTime.Now;

            Voter.SDBN = AppSettings.System.APIDB;

            if(!Voter.DOBSearch.IsNullOrEmpty())
            {
                Voter.DOB = DateTime.Parse(Voter.DOBSearch);
            }

            // Save changes to local database
            var offlineFactory = new OfflineFactory();
            offlineFactory.SaveVoterAsync(Voter);

            if (AppSettings.OfflineMode == false)
            {
                // Upload changes to API
                try
                {
                    var voterFactory = new VoterFactory();
                    await voterFactory.SaveVoterAsync(Voter);
                }
                catch (Exception e)
                {
                    // Log error message

                    AlertDialog connectionFailed = new AlertDialog("COULD NOT ESTABLISH A CONNECTION TO THE INTERNET");
                    connectionFailed.ShowDialog();
                    StatusBarMethods.WifiStatus(false);
                }
            }
        }

        public static async void SpoilBallot(VoterDataModel Voter, int Reason)
        {
            Voter.ActivityDate = DateTime.Now;

            Voter.SDBN = AppSettings.System.APIDB;

            if (AppSettings.TrainingMode == true)
            {
                // Save changes to local database
                var trainingFactory = new TrainingFactory();
                var spoiledBallot = await trainingFactory.SpoilBallot(Voter, Reason);
            }
            else
            {
                // Save changes to local database
                var offlineFactory = new OfflineFactory();
                var spoiledBallot = await offlineFactory.SpoilBallot(Voter, Reason);

                if (AppSettings.OfflineMode == false)
                {
                    // Upload changes to API
                    try
                    {
                        var voterFactory = new VoterFactory();
                        await voterFactory.SaveSpoiledAsync(AppSettings.System.APIDB, new EVote.Utilities.Models.Spoiled(spoiledBallot));
                    }
                    catch (Exception e)
                    {
                        // Log error message

                        AlertDialog connectionFailed = new AlertDialog("COULD NOT ESTABLISH A CONNECTION TO THE INTERNET");
                        connectionFailed.ShowDialog();
                        StatusBarMethods.WifiStatus(false);
                    }
                }
            }
        }
    }
}
