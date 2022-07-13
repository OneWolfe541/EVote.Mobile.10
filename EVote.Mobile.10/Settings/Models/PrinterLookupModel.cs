using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Settings
{
    public class PrinterLookupModel
    {
        public string PrinterName { get; set; }
        public List<PrinterTray> Bins { get; set; }
        public List<PaperSize> PaperSizes { get; set; }
    }
}
