using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace EVote.Settings
{
    public static class PrinterMethods
    {
        public static IEnumerable<string> PrintersList()
        {
            List<string> printers = new List<string>();

            // Set management scope
            //ManagementScope scope = new ManagementScope("\\root\\cimv2");
            //scope.Connect();

            // Select Printers from WMI Object Collections
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            // Add blank item to list
            //AddComboItemToControl(sender, "", "");

            foreach (ManagementObject printer in searcher.Get())
            {
                printers.Add(printer["Name"].ToString());
            }

            return printers;
        }
        public static async Task<IEnumerable<PrinterTray>> TraysList(string printerName)
        {
            return await Task.Run(() => {
                List<PrinterTray> trays = new List<PrinterTray>();

                using (System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument())
                {
                    // Attach printer to print document
                    pd.PrinterSettings.PrinterName = printerName;

                    // Loop through printer trays
                    for (int i = 0; i < pd.PrinterSettings.PaperSources.Count; i++)
                    {
                        // PaperSources are the trays
                        trays.Add(new PrinterTray
                        {
                            Index = pd.PrinterSettings.PaperSources[i].RawKind,
                            Name = pd.PrinterSettings.PaperSources[i].SourceName
                        });
                    }
                }

                return trays;
            });
        }

        public static async Task<IEnumerable<PaperSize>> PaperSizesList(string printerName)
        {
            return await Task.Run(() => {
                List<PaperSize> sizes = new List<PaperSize>();

                using (System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument())
                {
                    // Attach printer to print document
                    pd.PrinterSettings.PrinterName = printerName;

                    // Loop through printer trays
                    for (int i = 0; i < pd.PrinterSettings.PaperSizes.Count; i++)
                    {
                        // PaperSources are the trays
                        sizes.Add(new PaperSize
                        {
                            Index = pd.PrinterSettings.PaperSizes[i].RawKind,
                            Name = pd.PrinterSettings.PaperSizes[i].PaperName
                        });
                    }
                }

                return sizes;
            });
        }

        public static async Task<List<PrinterLookupModel>> LoadPrinterLists(string path)
        {
            return await Task.Run(
                async () =>
                {
                    Console.WriteLine("Loading Printers");

                    // Check if printer file exists
                    List<PrinterLookupModel> printerList = LoadPrinterListFile(path);
                    if (printerList != null)
                    {
                        //Console.WriteLine("Loading From File");                        
                    }
                    else
                    {
                        //Console.WriteLine("Loading From OS");
                        printerList = await LoadPrinterListsFromOS();

                        // Save new file
                        //Console.WriteLine("Creating Printer List File");
                        CreatePrinterListFile(path, printerList);
                    }
                    return printerList;
                }
            );
        }

        public static async Task<List<PrinterLookupModel>> ReloadPrinterLists(string path)
        {
            return await Task.Run(
                async () =>
                {
                    Console.WriteLine("Loading Printers");

                    // Check if printer file exists
                    List<PrinterLookupModel> printerList = new List<PrinterLookupModel>();

                    //Console.WriteLine("Loading From OS");
                    printerList = await LoadPrinterListsFromOS();

                    // Save new file
                    //Console.WriteLine("Creating Printer List File");
                    CreatePrinterListFile(path, printerList);

                    return printerList;
                }
            );
        }

        public static async Task<List<PrinterLookupModel>> LoadPrinterListsFromOS()
        {

            IEnumerable<string> innerPrinterList = PrinterMethods.PrintersList();
            List<PrinterLookupModel> PrinterListToLoad = new List<PrinterLookupModel>();

            foreach (var printerName in innerPrinterList)
            {
                // Get List of printers
                PrinterLookupModel printer = new PrinterLookupModel();
                printer.PrinterName = printerName.ToUpper();

                // Get List of Bins
                var trays = await PrinterMethods.TraysList(printerName);
                printer.Bins = trays.ToList();

                // Get List of Paper Sizes
                var sizes = await PrinterMethods.PaperSizesList(printerName);
                printer.PaperSizes = sizes.ToList();

                PrinterListToLoad.Add(printer);
            }

            return PrinterListToLoad;

        }

        internal static void CreatePrinterListFile(string path, List<PrinterLookupModel> PrinterListToSave)
        {
            // https://stackoverflow.com/questions/37199412/how-to-serialize-data-into-indented-json
            // or
            // https://stackoverflow.com/questions/2661063/how-do-i-get-formatted-json-in-net-using-c
            using (StreamWriter file = File.CreateText(path + "epb.pr.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                //serialize object directly into file stream
                serializer.Serialize(file, PrinterListToSave);
                //file.Close(); // I think the using statement already closes the file
            }

            //LoggingMethods.LogIO("SYSTEMSETTINGS JSON FILE CREATED");
        }

        public static List<PrinterLookupModel> LoadPrinterListFile(string path)
        {
            Console.WriteLine("Checking File");
            try
            {
                using (StreamReader file = new StreamReader(path + "epb.pr.json"))
                {
                    string json = file.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<PrinterLookupModel>>(json);
                }
            }
            catch
            {
                //List<PrinterLookupModel> none = new List<PrinterLookupModel>();
                return null;
            }
        }
    }
}
