using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EVote.Utilities.Models;
using Newtonsoft.Json;

namespace EVote.Factories
{
    public class VoterFactory
    {
        private string _baseAddress = "https://epollbookapi.azurewebsites.net";
        //private string _baseAddress = "https://localhost:44317";

        private string _ComputerName;

        public VoterFactory()
        {
            _ComputerName = System.Environment.MachineName;
        }

        public List<VoterDataModel> Create()
        {
            return GenerateVoterList();
        }

        public List<VoterDataModel> GenerateVoterList()
        {
            List<VoterDataModel> voterList = new List<VoterDataModel>();

            voterList.Add(new VoterDataModel()
            {
                VoterID = "534",
                SirnameOrdered = "GETTEMY, DEBORAH JANE",
                FullName = "DEBORAH JANE GETTEMY",
                //DOBYear = "1948",
                //Party = "DTS",
                //SSN = "0000",
                PhysicalAddress1 = "50 EQUESTRIAN PARK RD",
                PhysicalAddress2 = "UNIT 45B",
                PhysicalCity = "EDGEWOOD",
                PhysicalState = "NM",
                PhysicalZip = "87015",
                DeliveryAddress1 = "50 EQUESTRIAN PARK RD",
                DeliveryAddress2 = "UNIT 45B",
                DeliveryCity = "EDGEWOOD",
                DeliveryState = "NM",
                DeliveryZip = "87015",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1,
                ActivityDate = DateTime.Parse("2021-01-13 14:24:47.7675889")
                //IDRequired = true,
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "3729",
                SirnameOrdered = "FROGGE, JOHN LOWRY JR",
                FullName = "JOHN LOWRY FROGGE JR",
                //DOBYear = "1946",
                //Party = "REP",
                //SSN = "0000",
                PhysicalAddress1 = "18 CAMERADA RD",
                PhysicalCity = "SANTA FE",
                PhysicalState = "NM",
                PhysicalZip = "87508",
                DeliveryAddress1 = "628 OLD SANTA FE TRL",
                DeliveryAddress2 = "BOX 1826",
                DeliveryCity = "SANTA FE",
                DeliveryState = "NM",
                DeliveryZip = "87505",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1
                //ActivityDate = DateTime.Parse(""),
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "63337",
                SirnameOrdered = "PEREZ, STEVEN L",
                FullName = "STEVEN L PEREZ",
                //DOBYear = "1984",
                //Party = "DEM",
                //SSN = "0000",
                PhysicalAddress1 = "2516 CALLE DE RINCON BONITO",
                PhysicalCity = "SANTA FE",
                PhysicalState = "NM",
                PhysicalZip = "87505",
                DeliveryAddress1 = "PO BOX 276",
                DeliveryAddress2 = "",
                DeliveryCity = "EDGEWOOD",
                DeliveryState = "NM",
                DeliveryZip = "87015",
                LogDescription = "ISSUED ABSENTEE BALLOT",
                LogCode = 6
                //ActivityDate = DateTime.Parse(""),
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "830784",
                SirnameOrdered = "TRUJILLO, PEDRO A III",
                FullName = "PEDRO A TRUJILLO III",
                //DOBYear = "1971",
                //Party = "DTS",
                //SSN = "0000",
                PhysicalAddress1 = "101 CALLE HIJOS DE LA SAGRADA FAMILIA PLACE THIS IS A REALLY LONG ADDRESS THAT SHOULD BE FORCED OFF THE SCREEN FOR TESTING PURPOSES",
                PhysicalCity = "SANTA CRUZ",
                PhysicalState = "NM",
                PhysicalZip = "87567",
                DeliveryAddress1 = "1077 VICENTE MONGRELL,",
                DeliveryAddress2 = "PAYSANDU, UNGUAY 60000",
                DeliveryCity = "",
                DeliveryState = "",
                DeliveryZip = "",
                LogDescription = "VOTED AT POLLS",
                LogCode = 8
                //IDRequired = true,
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "597568",
                SirnameOrdered = "RYER, RACHEL ",
                FullName = "RACHEL RYER",
                //DOBYear = "1966",
                //Party = "DEM",
                //SSN = "0000",
                PhysicalAddress1 = "2827 CAMINO DEL BOSQUE",
                PhysicalCity = "EDGEWOOD",
                PhysicalState = "NM",
                PhysicalZip = "87517",
                DeliveryAddress1 = "2027 VIRGINIA ST NE",
                DeliveryAddress2 = "",
                DeliveryCity = "ALBUQUERQUE",
                DeliveryState = "NM",
                DeliveryZip = "87110",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "663485",
                SirnameOrdered = "SANCHEZ, EVAN DR.",
                FullName = "DR. EVAN SANCHEZ",
                //DOBYear = "1972",
                //Party = "DEM",
                //SSN = "0000",
                PhysicalAddress1 = "6678 JAGUAR DR",
                PhysicalCity = "SANTA FE",
                PhysicalState = "NM",
                PhysicalZip = "87507",
                DeliveryAddress1 = "6678 JAGUAR DR",
                DeliveryAddress2 = "",
                DeliveryCity = "SANTA FE",
                DeliveryState = "NM",
                DeliveryZip = "87507",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "598651",
                SirnameOrdered = "CUMMINS, CYNTHIA M",
                FullName = "CYNTHIA M CUMMINS",
                //DOBYear = "1967",
                //Party = "LIB",
                //SSN = "0000",
                PhysicalAddress1 = "5687 STATE ROAD 41",
                PhysicalCity = "LAMY",
                PhysicalState = "NM",
                PhysicalZip = "87540",
                DeliveryAddress1 = "5687 STATE ROAD 41",
                DeliveryAddress2 = "",
                DeliveryCity = "LAMY",
                DeliveryState = "NM",
                DeliveryZip = "87540",
                LogDescription = "ISSUED APPLICATION",
                LogCode = 3
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "3311",
                SirnameOrdered = "GETTEMY, WILLARD ARTHUR",
                FullName = "WILLARD ARTHUR GETTEMY",
                //DOBYear = "1948",
                //Party = "DTS",
                //SSN = "0000",
                PhysicalAddress1 = "50 EQUESTRIAN PARK RD",
                PhysicalCity = "EDGEWOOD",
                PhysicalState = "NM",
                PhysicalZip = "87015",
                DeliveryAddress1 = "50 EQUESTRIAN PARK RD",
                DeliveryAddress2 = "UNIT 45B",
                DeliveryCity = "EDGEWOOD",
                DeliveryState = "NM",
                DeliveryZip = "87015",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "771658",
                SirnameOrdered = "FROGGE, JOHN LOWRY SR",
                FullName = "JOHN LOWRY FROGGE SR",
                //DOBYear = "1946",
                //Party = "REP",
                //SSN = "0000",
                PhysicalAddress1 = "18 CAMERADA RD",
                PhysicalCity = "SANTA FE",
                PhysicalState = "NM",
                PhysicalZip = "87508",
                DeliveryAddress1 = "18 CAMERADA RD",
                DeliveryAddress2 = "",
                DeliveryCity = "SANTA FE",
                DeliveryState = "NM",
                DeliveryZip = "87508",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1
                //IDRequired = true,
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "160090785",
                SirnameOrdered = "PEREZ, STEVEN L",
                FullName = "STEVEN L PEREZ",
                //DOBYear = "1984",
                //Party = "DEM",
                //SSN = "0000",
                PhysicalAddress1 = "2516 CALLE DE RINCON BONITO",
                PhysicalCity = "SANTA FE",
                PhysicalState = "NM",
                PhysicalZip = "87505",
                DeliveryAddress1 = "PO BOX 416",
                DeliveryAddress2 = "",
                DeliveryCity = "TESUQUE",
                DeliveryState = "NM",
                DeliveryZip = "87574",
                LogDescription = "ISSUED ABSENTEE BALLOT",
                LogCode = 6
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "2720426",
                SirnameOrdered = "TRUJILLO, PEDRO A II",
                FullName = "PEDRO A TRUJILLO II",
                //DOBYear = "1971",
                //Party = "DTS",
                //SSN = "0000",
                PhysicalAddress1 = "101 CALLE HIJOS DE LA SAGRADA FAMILIA PLACE",
                PhysicalCity = "SANTA CRUZ",
                PhysicalState = "NM",
                PhysicalZip = "87567",
                DeliveryAddress1 = "",
                DeliveryAddress2 = "",
                DeliveryCity = "",
                DeliveryState = "",
                DeliveryZip = "",
                LogDescription = "VOTED AT POLLS",
                LogCode = 8
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "535299",
                SirnameOrdered = "RYER, RACHEL ",
                FullName = "RACHEL RYER",
                //DOBYear = "1966",
                //Party = "DEM",
                //SSN = "0000",
                PhysicalAddress1 = "2827 CAMINO DEL BOSQUE",
                PhysicalCity = "EDGEWOOD",
                PhysicalState = "NM",
                PhysicalZip = "87517",
                DeliveryAddress1 = "83 A VAN NU PO RD",
                DeliveryAddress2 = "MAILBOX 51",
                DeliveryCity = "SANTA FE",
                DeliveryState = "NM",
                DeliveryZip = "87508",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1
                //IDRequired = true,
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "607040",
                SirnameOrdered = "SANCHEZ, EVAN DR.",
                FullName = "DR. EVAN SANCHEZ",
                //DOBYear = "1972",
                //Party = "DEM",
                //SSN = "0000",
                PhysicalAddress1 = "6678 JAGUAR DR",
                PhysicalCity = "SANTA FE",
                PhysicalState = "NM",
                PhysicalZip = "87507",
                DeliveryAddress1 = "RT 5 BOX 305",
                DeliveryAddress2 = "",
                DeliveryCity = "SANTA FE",
                DeliveryState = "NM",
                DeliveryZip = "87506",
                LogDescription = "REGISTERED TO VOTE",
                LogCode = 1
                //Status = "A"
            });

            voterList.Add(new VoterDataModel()
            {
                VoterID = "793620",
                SirnameOrdered = "CUMMINS, CYNTHIA C",
                FullName = "CYNTHIA C CUMMINS",
                //DOBYear = "1967",
                //Party = "LIB",
                //SSN = "0000",
                PhysicalAddress1 = "5687 STATE ROAD 41",
                PhysicalCity = "LAMY",
                PhysicalState = "NM",
                PhysicalZip = "87540",
                DeliveryAddress1 = "PO BOX 5987",
                DeliveryAddress2 = "",
                DeliveryCity = "SANTA FE",
                DeliveryState = "NM",
                DeliveryZip = "87502",
                LogDescription = "ISSUED APPLICATION",
                LogCode = 3
                //Status = "A"
            });

            return voterList;
        }

        public async Task APIConnectionTestAsync()
        {
            string voterId = "1601-009";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/voters?id=" + voterId.ToString());

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Voter>(responseString);
        }

        public async Task<List<VoterDataModel>> SearchVoterAsync(VoterSearchModel search)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            //var DOB = "1959-09-22";

            //search.VoterID = "1601-009";

            string APIMethod = "/api/voterdatas";
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, search);

            //string voterId = "1601-009";
            //HttpResponseMessage response = await client.GetAsync("/api/voterdatas?id=" + voterId.ToString());

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VoterDataModel>>(responseString);

            return result;
        }

        public async Task<List<VoterDataModel>> SearchRosterAsync(VoterSearchModel search)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            //var DOB = "1959-09-22";

            //search.VoterID = "1601-009";

            string APIMethod = "/api/voterdatas?type=2";
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, search);

            //string voterId = "1601-009";
            //HttpResponseMessage response = await client.GetAsync("/api/voterdatas?id=" + voterId.ToString());

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VoterDataModel>>(responseString);

            return result;
        }

        public async Task<ResponseViewModel<Utilities.Models.VoterActivity>> MarkVoterAsync(VoterDataModel voter)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            //var DOB = "1959-09-22";

            //search.VoterID = "1601-009";

            voter.ComputerName = _ComputerName;

            string APIMethod = "/api/voteds"; 
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, voter);

            //string voterId = "1601-009";
            //HttpResponseMessage response = await client.GetAsync("/api/voterdatas?id=" + voterId.ToString());

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseViewModel<Utilities.Models.VoterActivity>>(responseString);

            return result;
        }

        public async Task<ResponseViewModel<EVote.Utilities.Models.Signature>> UploadSignature(EVote.Utilities.Models.Signature model, string APIDB)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            model.ComputerName = _ComputerName;

            string APIMethod = "/api/signatures?SDBN=" + APIDB;
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, model);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseViewModel<EVote.Utilities.Models.Signature>>(responseString);

            return result;
        }

        public async Task<Voter> SaveVoterAsync(VoterDataModel voter)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            voter.ComputerName = _ComputerName;

            string APIMethod = "/api/voters";
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, voter);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Voter>(responseString);

            return result;
        }

        public async Task<Spoiled> SaveSpoiledAsync(string APIDB, Spoiled model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            model.ComputerName = _ComputerName;

            string APIMethod = "/api/spoileds?SDBN=" + APIDB;
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, model);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Spoiled>(responseString);

            return result;
        }
    }
}
