using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EVote.LocalDatabase
{
    public class ElectionContext : DbContext
    {
        public ElectionContext(string connString)
        {
            this.Database.Connection.ConnectionString = connString;
        }

        public virtual DbSet<BallotStyleJurisdiction> BallotStyleJurisdictions { get; set; }
        public virtual DbSet<BallotStyle> BallotStyles { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Jurisdiction> Jurisdictions { get; set; }
        public virtual DbSet<LocationJurisdiction> LocationJurisdictions { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Signature> Signatures { get; set; }
        public virtual DbSet<SpoiledReason> SpoiledReasons { get; set; }
        public virtual DbSet<Spoiled> Spoileds { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<TrainingActivity> TrainingActivities { get; set; }
        public virtual DbSet<TrainingSignature> TrainingSignatures { get; set; }
        public virtual DbSet<TrainingSpoiled> TrainingSpoileds { get; set; }
        public virtual DbSet<Voter> Voters { get; set; }
        public virtual DbSet<VoterActivity> VoterActivities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BallotStyleJurisdiction>().HasKey(c => new { c.BallotStyleId, c.JurisdictionId });
            modelBuilder.Entity<BallotStyle>().HasKey(c => new { c.BallotStyleId });
            modelBuilder.Entity<Config>().HasKey(c => new { c.ConfigId });
            modelBuilder.Entity<Jurisdiction>().HasKey(c => new { c.JurisdictionId });
            modelBuilder.Entity<LocationJurisdiction>().HasKey(c => new { c.LocationId, c.JurisdictionId });
            modelBuilder.Entity<Location>().HasKey(c => new { c.LocationId });
            modelBuilder.Entity<Signature>().HasKey(c => new { c.SignatureId });
            modelBuilder.Entity<SpoiledReason>().HasKey(c => new { c.SpoiledReasonId });
            modelBuilder.Entity<Spoiled>().HasKey(c => new { c.SpoiledBallotId });
            modelBuilder.Entity<Statuses>().HasKey(c => new { c.StatusId });
            modelBuilder.Entity<TrainingActivity>().HasKey(c => new { c.VoterId });
            modelBuilder.Entity<TrainingSignature>().HasKey(c => new { c.SignatureId });
            modelBuilder.Entity<TrainingSpoiled>().HasKey(c => new { c.SpoiledBallotId });
            modelBuilder.Entity<Voter>().HasKey(c => new { c.VoterId });
            modelBuilder.Entity<VoterActivity>().HasKey(c => new { c.VoterId });
        }
    }
}
