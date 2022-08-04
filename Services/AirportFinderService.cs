using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BusinessModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Services
{
    public class AirportFinderService : IAirportFinderService
    {
        readonly HttpClient _httpClient;

        private readonly string _baseUrl = "";
        private readonly string _apiKey = "";
        private readonly string _apiSecret = "";

        private string _bearerToken = "";

        private readonly string _airportSearchApiUrl = "";
        
        
        public AirportFinderService(HttpClient httpClient)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _baseUrl = configuration.GetSection("ApiUrl")["BaseUrl"];
            _apiKey = configuration.GetSection("AmadeusSecrets")["APIKey"];
            _apiSecret = configuration.GetSection("AmadeusSecrets")["APISecret"];
            _airportSearchApiUrl = $"/reference-data/locations";

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        
        public async Task ConnectOAuth()
        {
            var message =  "security/oauth2/token";
            var content = new StringContent(
                $"grant_type=client_credentials&client_id={_apiKey}&client_secret={_apiSecret}",
                Encoding.UTF8, "application/x-www-form-urlencoded"
            );

            var httpResponse = await _httpClient.PostAsync(message, content);
            
            if (httpResponse.IsSuccessStatusCode)
            {
                var jsonContent = httpResponse.Content.ReadAsStringAsync().Result;

                var oauthResults = JsonConvert.DeserializeObject<OAuthResults>(jsonContent);
                
                _bearerToken = oauthResults.access_token;
            }

        }

        private class OAuthResults
        {
            public string access_token { get; set; }
        }
        

        private class Root
        {
            [JsonProperty("data")]
            public List<Airport> Data { get; set; }
        }
        
        
        public async Task<Airport> FindAirport(string iata)
        {
            if (String.IsNullOrEmpty(_bearerToken))
            {
                await ConnectOAuth();
            }
            var airport = new Airport();

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _bearerToken);

            string getLocationUrl = $"reference-data/locations?subType=AIRPORT&keyword={iata}";

            var httpResponseMessage = await _httpClient.GetAsync(getLocationUrl);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var jsonContent = httpResponseMessage.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<Root>(jsonContent);

                if (data.Data.Count == 0)
                {
                    throw new Exception("Airport not found");
                }
                
                airport = data.Data[0];
            }

            else
            {
                throw new Exception("Airport not found");
            }

            return airport;
        }

    }
}
