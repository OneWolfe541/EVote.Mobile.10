﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVote.LocalDatabase;
using EVote.Logging;
using EVote.Methods;
using EVote.Utilities.Extensions;
using EVote.Utilities.Models;

namespace EVote.Factories
{
    public class OfflineFactory
    {
        EVoteLogger _localDBLogger = new EVoteLogger("EVoteLogs", true);

        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public OfflineFactory()
        {
            string server = System.Environment.MachineName;
            if (server == "GARYC-WIN7")
            {
                _connectionString = "data source=GARYC-WIN7\\SQLEXPRESS2012;initial catalog=ELECTION;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            }
            else if (server == "GARYC-DT10")
            {
                _connectionString = "Data Source=192.168.1.6\\MSSQLSERVER2012;Initial Catalog=PollBookElectionTemplate;Persist Security Info=True;User ID=sa;Password=InkImpr35510n5;MultipleActiveResultSets=True;App=EntityFramework";
            }
            else
            {
                _connectionString = "data source=" + server + "; initial catalog=ELECTION;persist security info=True;user id=sa;password=@ESf13lddba;MultipleActiveResultSets=True;App=EntityFramework";
            }
            //_localDBLogger.WriteLog(_connectionString);
        }

        public async Task<List<VoterDataModel>> SearchVoterAsync(VoterSearchModel search)
        {
            using (var context = new ElectionContext(_connectionString))
            {
                if(context == null)
                    throw new Exception();

                return await SearchVoters(context, search);
            }
        }

        private IQueryable<VoterDataModel> Query(ElectionContext context)
        {
            var query = from voters in context.Voters
                        join votedrecord in context.VoterActivities
                            on voters.VoterId equals votedrecord.VoterId into votedrecordgroup
                        from votedRecord in votedrecordgroup.DefaultIfEmpty()
                        join status in context.Statuses
                            on votedRecord.StatusId equals status.StatusId into votedstatusgroup
                        from votedStatus in votedstatusgroup.DefaultIfEmpty()
                        join location in context.Locations
                            on votedRecord.LocationId equals location.LocationId into votedlocationgroup
                        from votedLocation in votedlocationgroup.DefaultIfEmpty()
                        join jurisdictions in context.Jurisdictions
                            on voters.JurisdictionId equals jurisdictions.JurisdictionId into voterjurisdictionsgroup
                        from voterJurisdiction in voterjurisdictionsgroup.DefaultIfEmpty()
                        join ballotjurisdictions in context.BallotStyleJurisdictions
                            on voters.JurisdictionId equals ballotjurisdictions.JurisdictionId into voterballotjurisdictionsgroup
                        from voterBallotJurisdiction in voterballotjurisdictionsgroup.DefaultIfEmpty()
                        join ballotStyles in context.BallotStyles
                            on voterBallotJurisdiction.BallotStyleId equals ballotStyles.BallotStyleId into voterballotgroup
                        from voterBallot in voterballotgroup.DefaultIfEmpty()
                        select new
                        {
                            voters.VoterId,
                            voters.FirstName,
                            voters.MiddleName,
                            voters.LastName,
                            voters.Suffix,
                            voters.MaidenName,
                            voters.DateOfBirth,
                            voters.Gender,
                            voters.Phone,
                            voters.JurisdictionId,
                            voters.InvalidMailingAddress,
                            voters.InvalidRegisteredAddress,
                            voters.MailingAddress,
                            voters.MailingAddress2,
                            voters.MailingCity,
                            voters.MailingState,
                            voters.MailingZip,
                            voters.MailingCountry,
                            voters.RegisteredAddress,
                            voters.RegisteredAddress2,
                            voters.RegisteredCity,
                            voters.RegisteredState,
                            voters.RegisteredZip,
                            voters.RegisteredCountry,
                            voters.TempAddress,
                            voters.TempAddress2,
                            voters.TempCity,
                            voters.TempState,
                            voters.TempZip,
                            voters.TempCountry,
                            voters.OnReservation,
                            voters.Registered,
                            voters.LastModified,
                            voters.SignatureVerificationId,
                            voters.UseTempAddress,
                            votedRecord.AddressLine1,
                            votedRecord.AddressLine2,
                            votedRecord.City,
                            votedRecord.State,
                            votedRecord.Zip,
                            votedRecord.StatusId,
                            votedRecord.Country,
                            votedRecord.BarCode,
                            votedRecord.BallotNumber,
                            votedStatus.StatusDescription,
                            votedRecord.LocationId,
                            votedLocation.LocationName,
                            votedRecord.DateIssued,
                            votedRecord.PrintedDate,
                            votedRecord.DateVoted,
                            votedRecord.ActivityDate,
                            votedRecord.LocalOnly,
                            voterJurisdiction.JurisdictionName,
                            voterJurisdiction.JurisdictionType,
                            voterBallotJurisdiction.BallotStyleId,
                            voterBallot.BallotStyleName,
                            voterBallot.BallotStyleFileName
                        };

            return query
                .Select(v => new VoterDataModel
                {
                    VoterID = v.VoterId,
                    LastName = v.LastName.ToUpper(),
                    FirstName = v.FirstName.ToUpper(),
                    MiddleName = v.MiddleName.ToUpper(),
                    Generation = v.Suffix.ToUpper(),
                    DOB = v.DateOfBirth,
                    DOBSearch = v.DateOfBirth.ToString(),
                    MaidenName = v.MaidenName.ToUpper(),
                    FullName = String.Concat(
                        v.FirstName.ToUpper(), " ",
                        v.MiddleName.ToUpper(), " ",
                        v.LastName.ToUpper(), " ",
                        v.Suffix.ToUpper())
                        .Trim().Replace("  ", " "),
                    SirnameOrdered = String.Concat(
                        v.LastName.ToUpper(), ", ",
                        v.FirstName.ToUpper(), " ",
                        v.MiddleName.ToUpper(), " ",
                        v.Suffix.ToUpper())
                        .Trim().Replace("  ", " "),
                    MailingAddress1 = v.MailingAddress.ToUpper(),
                    MailingAddress2 = v.MailingAddress2.ToUpper(),
                    MailingCity = v.MailingCity.ToUpper(),
                    MailingState = v.MailingState.ToUpper(),
                    MailingZip = v.MailingZip.ToUpper(),
                    MailingCountry = v.MailingCountry.ToUpper(),
                    PhysicalAddress1 = v.RegisteredAddress.ToUpper(),
                    PhysicalAddress2 = v.RegisteredAddress2.ToUpper(),
                    PhysicalCity = v.RegisteredCity.ToUpper(),
                    PhysicalState = v.RegisteredState.ToUpper(),
                    PhysicalZip = v.RegisteredZip,
                    PhysicalCountry = v.RegisteredCountry.ToUpper(),
                    PhysicalCSZ = String.Concat(
                        v.RegisteredCity.ToUpper(), ", ",
                        v.RegisteredState.ToUpper(), " ",
                        v.RegisteredZip),
                    TempAddress1 = v.TempAddress.ToUpper(),
                    TempAddress2 = v.TempAddress2.ToUpper(),
                    TempCity = v.TempCity.ToUpper(),
                    TempState = v.TempState.ToUpper(),
                    TempZip = v.TempZip.ToUpper(),
                    TempCountry = v.TempCountry.ToUpper(),
                    OnReservation = v.OnReservation,
                    Registered = v.Registered,
                    SignatureVerificationId = v.SignatureVerificationId,
                    TempUsed = v.UseTempAddress,
                    LogCode = v.StatusId,
                    LogDescription = v.StatusDescription.ToUpper(),
                    LocationID = v.LocationId,
                    LocationName = v.LocationName.ToUpper(),
                    BallotStyleID = v.BallotStyleId,
                    BallotNumber = v.BallotNumber,
                    BallotStyleName = v.BallotStyleName,
                    BallotStyleFileName = v.BallotStyleFileName,
                    JurisdictionID = v.JurisdictionId,
                    DistrictName = v.JurisdictionName.ToUpper(),
                    DateIssued = v.DateIssued,
                    PrintedDate = v.PrintedDate,
                    DateVoted = v.DateVoted,
                    ActivityDate = v.ActivityDate,
                    LocalOnly = v.LocalOnly
                }
                );
        }

        public async Task<List<VoterDataModel>> SearchVoters(ElectionContext context, VoterSearchModel search)
        {
            return await Query(context)
                        .IDEquals(search.VoterID)
                        .LastNameStartsWith(search.LastName)
                        .FirstNameStartsWith(search.FirstName)
                        //.BirthDateEquals(search.SearchDate.ToString())
                        .BirthDateEqualsDate(search.SearchDate.ToString())
                        .AtPollSite(search.Location)
                        .WithLogCode(search.Status)
                        .OrderBy(o => o.LastName).ThenBy(o => o.FirstName)
                        .Take(50)
                        .ToListAsync();
        }

        public async Task<List<int>> SearchVoterYears(DateSearch search)
        {
            if (!search.Month.IsNullOrEmpty() && !search.Day.IsNullOrEmpty())
            {
                using (var context = new ElectionContext(_connectionString))
                {
                    var month = Int32.Parse(search.Month);
                    var day = Int32.Parse(search.Day);
                    List<int> dates = await context.Voters
                        .Where(v => v.DateOfBirth.Value.Month == month
                                && v.DateOfBirth.Value.Day == day)
                        .Select(v => v.DateOfBirth.Value.Year)
                        .OrderBy(d => d)
                        .Distinct()
                        .ToListAsync();

                    return dates;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<VoterDataModel>> SearchOfflineVotersAsync()
        {
            using (var context = new ElectionContext(_connectionString))
            {
                if (context == null)
                    throw new Exception();

                return await Query(context)
                        .Where(ov => ov.LocalOnly == true)
                        .ToListAsync();
            }
        }

        public async void MarkVoterAsync(VoterDataModel model)
        {
            using (var context = new ElectionContext(_connectionString))
            {
                var voted = await context.VoterActivities.FindAsync(model.VoterID);
                if (voted == null)
                {
                    CreateVotedAsync(model);
                }
                else
                {
                    UpdateVotedAsync(model);
                }
            }
        }

        private async void CreateVotedAsync(VoterDataModel model)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            if (model == null)
                throw new Exception();

            LocalDatabase.VoterActivity voted = new LocalDatabase.VoterActivity()
            {
                VoterId = model.VoterID,
                DateIssued = model.DateIssued,
                PrintedDate = model.PrintedDate,
                AddressLine1 = model.DeliveryAddress1,
                AddressLine2 = model.DeliveryAddress2,
                City = model.DeliveryCity,
                State = model.DeliveryState,
                Zip = model.DeliveryZip,
                Country = model.DeliveryCountry,
                LocationId = model.LocationID,
                DateVoted = model.DateVoted,
                JurisdictionId = model.JurisdictionID,
                BallotStyleId = model.BallotStyleID,
                BallotNumber = model.BallotNumber,
                StatusId = model.LogCode,
                ActivityDate = model.ActivityDate,
                LastModified = model.ActivityDate.Value,
                BarCode = model.BarCode,
                LocalOnly = model.LocalOnly
            };

            context.VoterActivities.Add(voted);
            await context.SaveChangesAsync();
        }

        private async void UpdateVotedAsync(VoterDataModel model)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            if (model == null)
                throw new Exception();

            var voted = await context.VoterActivities.FindAsync(model.VoterID);
            if (voted != null)
            {
                voted.VoterId = model.VoterID;
                voted.DateIssued = model.DateIssued;
                voted.PrintedDate = model.PrintedDate;
                voted.AddressLine1 = model.DeliveryAddress1;
                voted.AddressLine2 = model.DeliveryAddress2;
                voted.City = model.DeliveryCity;
                voted.State = model.DeliveryState;
                voted.Zip = model.DeliveryZip;
                voted.Country = model.DeliveryCountry;
                voted.LocationId = model.LocationID;
                voted.DateVoted = model.DateVoted;
                voted.JurisdictionId = model.JurisdictionID;
                voted.BallotStyleId = model.BallotStyleID;
                voted.BallotNumber = model.BallotNumber;
                voted.StatusId = model.LogCode;
                voted.ActivityDate = model.ActivityDate;
                voted.LastModified = model.ActivityDate.Value;
                voted.BarCode = model.BarCode;
                voted.LocalOnly = model.LocalOnly;
            }

            await context.SaveChangesAsync();
        }

        public async void SaveVoterAsync(VoterDataModel model)
        {
            using (var context = new ElectionContext(_connectionString))
            {
                var voter = await context.Voters.FindAsync(model.VoterID);
                if (voter == null)
                {
                    CreateVoterAsync(model);
                }
                else
                {
                    UpdateVoterAsync(model);
                }
            }
        }

        private async void CreateVoterAsync(VoterDataModel model)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            if (model == null)
                throw new Exception();

            LocalDatabase.Voter voter = new LocalDatabase.Voter()
            {
                VoterId = model.VoterID,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Suffix = model.Generation,
                MaidenName = model.MaidenName,
                DateOfBirth = model.DOB,
                Gender = model.Gender,
                Phone = model.Phone,
                JurisdictionId = model.JurisdictionID,
                InvalidRegisteredAddress = model.InvalidRegisteredAddress,
                InvalidMailingAddress = model.InvalidMailingAddress,
                MailingAddress = model.MailingAddress1,
                MailingAddress2 = model.MailingAddress2,
                MailingCity = model.MailingCity,
                MailingState = model.MailingState,
                MailingZip = model.MailingZip,
                MailingCountry = model.MailingCountry,
                RegisteredAddress = model.PhysicalAddress1,
                RegisteredAddress2 = model.PhysicalAddress2,
                RegisteredCity = model.PhysicalCity,
                RegisteredState = model.PhysicalState,
                RegisteredZip = model.PhysicalZip,
                RegisteredCountry = model.PhysicalCountry,
                TempAddress = model.TempAddress1,
                TempAddress2 = model.TempAddress2,
                TempCity = model.TempCity,
                TempState = model.TempState,
                TempZip = model.TempZip,
                TempCountry = model.TempCountry,
                OnReservation = model.OnReservation,
                Registered = model.Registered,
                LastModified = model.ActivityDate.Value,
                SignatureVerificationId = model.SignatureVerificationId,
                UseTempAddress = model.TempUsed
            };

            context.Voters.Add(voter);
            await context.SaveChangesAsync();
        }

        private async void UpdateVoterAsync(VoterDataModel model)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            if (model == null)
                throw new Exception();

            var voter = await context.Voters.FindAsync(model.VoterID);
            if (voter != null)
            {
                voter.FirstName = model.FirstName;
                voter.MiddleName = model.MiddleName;
                voter.LastName = model.LastName;
                voter.Suffix = model.Generation;
                voter.MaidenName = model.MaidenName;
                voter.DateOfBirth = model.DOB;
                voter.Gender = model.Gender;
                voter.Phone = model.Phone;
                voter.JurisdictionId = model.JurisdictionID;
                voter.InvalidRegisteredAddress = model.InvalidRegisteredAddress;
                voter.InvalidMailingAddress = model.InvalidMailingAddress;
                voter.MailingAddress = model.MailingAddress1;
                voter.MailingAddress2 = model.MailingAddress2;
                voter.MailingCity = model.MailingCity;
                voter.MailingState = model.MailingState;
                voter.MailingZip = model.MailingZip;
                voter.MailingCountry = model.MailingCountry;
                voter.RegisteredAddress = model.PhysicalAddress1;
                voter.RegisteredAddress2 = model.PhysicalAddress2;
                voter.RegisteredCity = model.PhysicalCity;
                voter.RegisteredState = model.PhysicalState;
                voter.RegisteredZip = model.PhysicalZip;
                voter.RegisteredCountry = model.PhysicalCountry;
                voter.TempAddress = model.TempAddress1;
                voter.TempAddress2 = model.TempAddress2;
                voter.TempCity = model.TempCity;
                voter.TempState = model.TempState;
                voter.TempZip = model.TempZip;
                voter.TempCountry = model.TempCountry;
                voter.OnReservation = model.OnReservation;
                voter.Registered = model.Registered;
                voter.LastModified = model.ActivityDate.Value;
                voter.SignatureVerificationId = model.SignatureVerificationId;
                voter.UseTempAddress = model.TempUsed;
            }

            await context.SaveChangesAsync();
        }

        public List<BallotStyle> BallotStyles()
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            return context.BallotStyles.ToList();
        }

        public async Task<List<Config>> Configs()
        {
            EVoteLogger _databaseLogger = new EVoteLogger("EVoteLogs", true);
            _databaseLogger.WriteLog("Loading Configs");

            var context = new ElectionContext(_connectionString);

            if (context == null)
                throw new Exception();

            var list = await context.Configs.ToListAsync();

            _databaseLogger.WriteLog("Configs Found: " + list.Count().ToString());

            return list;
        }

        public async void SaveConfigs(List<Config> models)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            // Delete table
            if(models != null && models.Count() > 0)
            {
                context.Configs.RemoveRange(context.Configs);
            }

            context.Configs.AddRange(models);
            await context.SaveChangesAsync();
        }

        public List<Jurisdiction> Jurisdictions()
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            return context.Jurisdictions.ToList();
        }

        public List<Statuses> Statuses()
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            return context.Statuses.ToList();
        }

        public List<Location> Locations()
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            return context.Locations.ToList();
        }

        public async void SaveLocations(List<Location> models)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            // Delete table
            if (models != null && models.Count() > 0)
            {
                context.Configs.RemoveRange(context.Configs);
            }

            context.Locations.AddRange(models);
            await context.SaveChangesAsync();
        }

        public async Task<Location> CompareLocations(string name, string login)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
            {
                //_localDBLogger.WriteLog("Server not found");
                throw new Exception();
            }

            //_localDBLogger.WriteLog("Name:" + name + " date:" + login);
            return await context.Locations.Where(l => 
                    l.LocationName.ToUpper() == name.ToUpper() && 
                    l.Login.ToUpper() == login.ToUpper()
                    ).FirstOrDefaultAsync();
        }

        public async Task<List<LocationJurisdiction>> LocationJurisdictions()
        {
            EVoteLogger _databaseLogger = new EVoteLogger("EVoteLogs", true);
            _databaseLogger.WriteLog("Loading Locations");

            var context = new ElectionContext(_connectionString);

            if (context == null)
                throw new Exception();

            var list = await context.LocationJurisdictions.ToListAsync();

            _databaseLogger.WriteLog("Locations Found: " + list.Count().ToString());

            return list;
        }

        public async void SaveLocationJurisdictions(List<LocationJurisdiction> models)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            // Delete table
            if (models != null && models.Count() > 0)
            {
                context.LocationJurisdictions.RemoveRange(context.LocationJurisdictions);
            }

            context.LocationJurisdictions.AddRange(models);
            await context.SaveChangesAsync();
        }

        public List<SpoiledReason> SpoiledReasons()
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            return context.SpoiledReasons.ToList();
        }

        public async Task<LocalDatabase.Spoiled> SpoilBallot(VoterDataModel model, int reason)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            LocalDatabase.Spoiled spoiledBallot = new LocalDatabase.Spoiled()
            {
                SpoiledBallotId = Guid.NewGuid(),
                SpoiledReason = reason,
                LocationId = AppSettings.System.SiteId,
                BallotStyleId = model.BallotStyleID??0,
                VoterId = model.VoterID,
                PrintedDate = DateTime.Now,
                LastModified = DateTime.Now
            };

            try
            {
                context.Spoileds.Add(spoiledBallot);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _localDBLogger.WriteLog("Local Database Error: " + e.Message);
                // Log error message
                if (e.InnerException != null)
                {
                    _localDBLogger.WriteLog("Local Database Error: " + e.InnerException);
                }
                else
                {
                    _localDBLogger.WriteLog("Local Database Error: " + e.Message);
                }
            }

            return spoiledBallot;
        }

        public List<DailyActivityModel> VoterActivity()
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            var query = from voterActivity in context.VoterActivities
                        join location in context.Locations
                            on voterActivity.LocationId equals location.LocationId into votedlocationgroup
                        from votedLocation in votedlocationgroup.DefaultIfEmpty()
                        where voterActivity.StatusId == 11 || voterActivity.StatusId == 12
                        select new
                        {
                            votedLocation.LocationName,
                            voterActivity.StatusId
                        };

            return query
                         .GroupBy(g => g.LocationName)
                         .Select(v => new DailyActivityModel
                         {
                             LocationName = v.Key,
                             Total = v.Count()
                         })
                         .ToList();
        }

        public async Task<EVote.LocalDatabase.Signature> Signatures(VoterDataModel voter)
        {
            var context = new ElectionContext(_connectionString);
            if (context == null)
                throw new Exception();

            return await context.Signatures.Where(sig => sig.VoterId == voter.VoterID).OrderByDescending(ord => ord.LastModified).FirstOrDefaultAsync();
        }
    }
}
