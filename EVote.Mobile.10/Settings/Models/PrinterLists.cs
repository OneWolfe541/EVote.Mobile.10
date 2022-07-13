using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EVote.Settings
{
    public class PrinterLists
    {
        public List<PrinterLookupModel> Printers { get; set; }
        public bool PrintersLoaded { get; set; }

        public List<PrinterTray> GetPrinterTrays(string PrinterName)
        {
            //var list = Printers.Where(p => p.PrinterName == PrinterName).FirstOrDefault();
            //return list.Bins;
            foreach (var item in Printers)
            {
                if (item.PrinterName.ToUpper() == PrinterName.ToUpper())
                {
                    return item.Bins;
                }
            }

            return null;
        }

        public List<PaperSize> GetPaperSizes(string PrinterName)
        {
            //var list = Printers.Where(p => p.PrinterName == PrinterName).FirstOrDefault();
            //return list.PaperSizes;
            foreach (var item in Printers)
            {
                if (item.PrinterName.ToUpper() == PrinterName.ToUpper())
                {
                    return item.PaperSizes;
                }
            }

            return null;
        }

        public async Task<List<PrinterTray>> GetPrinterTraysAsync(string PrinterName)
        {
            while (PrintersLoaded == false)
            {
                await PutTaskDelay(100);
            }

            foreach (var item in Printers)
            {
                if (item.PrinterName.ToUpper() == PrinterName.ToUpper())
                {
                    return item.Bins;
                }
            }

            return null;
        }

        public async Task<List<PaperSize>> GetPaperSizesAsync(string PrinterName)
        {
            while (PrintersLoaded == false)
            {
                await PutTaskDelay(1000);
            }

            foreach (var item in Printers)
            {
                if (item.PrinterName.ToUpper() == PrinterName.ToUpper())
                {
                    return item.PaperSizes;
                }
            }

            return null;
        }

        // https://stackoverflow.com/questions/22158278/wait-some-seconds-without-blocking-ui-execution
        private async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}
