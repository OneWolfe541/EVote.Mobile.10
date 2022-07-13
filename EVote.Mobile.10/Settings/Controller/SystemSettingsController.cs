using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVote.LocalDatabase;
using Newtonsoft.Json;

namespace EVote.Settings
{
    public class SystemSettingsController
    {
        private string _path = "C:\\EVote\\Settings\\";

        private SystemSettingsModel _settings;
        public SystemSettingsModel Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        private ElectionSettingsModel _election;
        public ElectionSettingsModel Election
        {
            get
            {
                return _election;
            }
            set
            {
                _election = value;
            }
        }

        private UserSettingsModel _user;
        public UserSettingsModel User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        private PrinterLists _printers = new PrinterLists();
        public PrinterLists Printers
        {
            get
            {
                //if (_printers == null) _printers = new PrinterLists();
                return _printers;
            }
            set
            {
                _printers = value;
            }
        }

        private List<LocationJurisdiction> _locationJurisdiction;
        public List<LocationJurisdiction> LocationJurisdictions
        {
            get
            {
                return _locationJurisdiction;
            }
            set
            {
                _locationJurisdiction = value;
            }
        }


        public bool OfflineMode { get; set; }

        public SystemSettingsController()
        {
            //LoadJsonFile();
        }

        public void SaveSettings()
        {
            CreateJsonFile();
        }

        public void LoadJsonFile()
        {
            using (StreamReader file = new StreamReader(_path + "epb.sys.json"))
            {
                string json = file.ReadToEnd();
                _settings = JsonConvert.DeserializeObject<SystemSettingsModel>(json);
            }
        }

        public void CreateJsonFile()
        {
            // https://stackoverflow.com/questions/37199412/how-to-serialize-data-into-indented-json
            // or
            // https://stackoverflow.com/questions/2661063/how-do-i-get-formatted-json-in-net-using-c
            using (StreamWriter file = File.CreateText(_path + "epb.sys.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                //serialize object directly into file stream
                serializer.Serialize(file, _settings);
                //file.Close(); // I think the using statement already closes the file
            }
        }

        public async Task<bool> LoadPrinterLists()
        {
            // Set loading Flag
            _printers.PrintersLoaded = false;

            // Load printers list from either file or OS
            _printers.Printers = await PrinterMethods.LoadPrinterLists(_path);

            // Check if lists finished loading
            if (_printers.Printers.Count() > 0)
            {
                _printers.PrintersLoaded = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ReloadPrinterLists()
        {
            // Set loading Flag
            _printers.PrintersLoaded = false;

            // Load printers list from either file or OS
            _printers.Printers = await PrinterMethods.ReloadPrinterLists(_path);

            // Check if lists finished loading
            if (_printers.Printers.Count() > 0)
            {
                _printers.PrintersLoaded = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
