using DbModel;
using System.Net.Http;
using DocumentDbModel.AirportDocument;

namespace Services
{
    public class ServiceProvider : IServiceProvider
    {
        private IUnitOfWork _dbUnitOfWork;
        private IAirportService _airportService;
        private AirportDbContext _airportDbContext;
        private HttpClient _httpClient;
        private IServiceProviderSingleton _serviceProviderSingleton;
        private IMongoDbContext _mongoDbContext;

        public ServiceProvider(AirportDbContext airportDbContext, IHttpClientFactory httpClientFactory, IServiceProviderSingleton serviceProviderSingleton, IMongoDbContext mongoDbContext)
        {
            _airportDbContext = airportDbContext;
            _dbUnitOfWork = new DbUnitOfWork(_airportDbContext);
            _httpClient = httpClientFactory.CreateClient();
            _serviceProviderSingleton = serviceProviderSingleton;
            _mongoDbContext = mongoDbContext;
        }

        public IUnitOfWork DbUnitOfWork
        {
            get { return _dbUnitOfWork; }
        }
        
        public IMongoDbContext MongoDb
        {
            get
            {
                return _mongoDbContext;
            }
        }
        

        public IAirportService GetAirportService()
        {
            if (_airportService == null)
            {
                _airportService = new AirportService(_dbUnitOfWork, this, _serviceProviderSingleton);
            }

            return _airportService;
        }
    }
}
