using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EVote.LocalDatabase;
using EVote.Methods;
using EVote.Utilities.Models;
using Newtonsoft.Json;

namespace EVote.Factories
{
    public class ElectionFactory
    {
        private string _baseAddress = "https://epollbookapi.azurewebsites.net";
        //private string _baseAddress = "https://localhost:44317";

        public ElectionFactory()
        {

        }
        public async Task<List<BallotStyle>> BallotStyles()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/ballotstyles?SDBN=" + AppSettings.System.APIDB);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<BallotStyle>>(responseString);

            return result;
        }

        public async Task<List<Config>> Configs()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            //HttpResponseMessage response = await client.GetAsync("/api/configs?SDBN=" + AppSettings.System.APIDB + "&id=" + AppSettings.System.SiteId);
            HttpResponseMessage response = await client.GetAsync("/api/configs?SDBN=" + AppSettings.System.APIDB);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Config>>(responseString);

            return result;
        }

        public async Task<Config> SaveConfigsAsync(List<Config> model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            string APIMethod = "/api/configs?SDBN=" + AppSettings.System.APIDB;
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, model);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Config>(responseString);

            return result;
        }

        public async Task<List<Jurisdiction>> Jurisdictions()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/jurisdictions?SDBN=" + AppSettings.System.APIDB);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Jurisdiction>>(responseString);

            return result;
        }

        public async Task<List<LocationJurisdiction>> LocationJurisdictions()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/locationjurisdictions?SDBN=" + AppSettings.System.APIDB);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<LocationJurisdiction>>(responseString);

            return result;
        }

        public async Task<List<LocationJurisdiction>> SaveLocationJurisdictions(List<LocationJurisdiction> model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            string APIMethod = "/api/locationjurisdictions?SDBN=" + AppSettings.System.APIDB;
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, model);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<LocationJurisdiction>>(responseString);

            return result;
        }

        public async Task<List<SpoiledReason>> SpoiledReasons()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/spoiledreasons?SDBN=" + AppSettings.System.APIDB);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SpoiledReason>>(responseString);

            return result;
        }

        public async Task<LocalDatabase.Spoiled> SaveSpoiledAsync(LocalDatabase.Spoiled model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            string APIMethod = "/api/spoileds?SDBN=" + AppSettings.System.APIDB;
            HttpResponseMessage response = await client.PostAsJsonAsync(APIMethod, model);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LocalDatabase.Spoiled>(responseString);

            return result;
        }

        public async Task<List<ChartStatsModel>> VoterActivity()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/charts?SDBN=" + AppSettings.System.APIDB + "&id=1");

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ChartStatsModel>>(responseString);

            return result;
        }

        public async Task<List<ChartStatsModel>> ElectionActivity()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/charts?SDBN=" + AppSettings.System.APIDB + "&id=2");

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ChartStatsModel>>(responseString);

            return result;
        }

        public async Task<VoterCountsModel> VoterCounts()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            //client.BaseAddress = new Uri("https://epollbookapi.azurewebsites.net");
            //client.BaseAddress = new Uri("https://localhost:44317");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.Timeout = TimeSpan.FromMinutes(60);

            HttpResponseMessage response = await client.GetAsync("/api/charts?SDBN=" + AppSettings.System.APIDB + "&id=3");

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<VoterCountsModel>(responseString);

            return result;
        }
    }
}
