using DbModel;
using System.Net.Http;

namespace Services
{
    public class ServiceProviderSingleton : IServiceProviderSingleton
    {
        private IUnitOfWork _dbUnitOfWork;
        private IAirportFinderService _airportFinderService;
        private HttpClient _httpClient;

        public ServiceProviderSingleton(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public IAirportFinderService GetAirportFinderService()
        {
            if (_airportFinderService == null)
            {
                _airportFinderService = new AirportFinderService(_httpClient);
            }

            return _airportFinderService;
        }
    }
}
