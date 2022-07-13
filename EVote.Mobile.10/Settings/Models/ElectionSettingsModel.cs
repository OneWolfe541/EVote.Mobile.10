using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Settings
{
    public class ElectionSettingsModel
    {
        public Nullable<bool> BallotNumOnSig { get; set; }
        public Nullable<bool> DistrictOnlyVoting { get; set; }
        public Nullable<bool> DistrictSignIn { get; set; }
        public string ElectionDate { get; set; }
        public string ElectionName { get; set; }
        public Nullable<bool> ShowDistrict { get; set; }
        public Nullable<bool> ShowEDActivity { get; set; }
        public Nullable<bool> ShowEVActivity { get; set; }
        public Nullable<bool> SpoilBallots { get; set; }
        public Nullable<int> TimeAdjust { get; set; }
    }
}
