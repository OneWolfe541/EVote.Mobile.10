using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Settings
{
    public class SystemSettingsModel
    {
        public string PDFTools { get; set; }
        public int SiteId { get; set; }
        public string SignatureFolder { get; set; }
        public string SignatureType { get; set; }
        public bool PrintBallots { get; set; }
        public string APIDB { get; set; }
        public string BallotPrinter { get; set; }
        public int BallotSize { get; set; }
        public int BallotBin { get; set; }
        public bool OfflineMode { get; set; }
    }
}
