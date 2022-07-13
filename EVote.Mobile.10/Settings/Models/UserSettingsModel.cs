using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Settings
{
    public class UserSettingsModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Login { get; set; }
        public Nullable<int> RollId { get; set; }
        public DateTime LastModified { get; set; }
        public bool Active { get; set; }
    }
}
