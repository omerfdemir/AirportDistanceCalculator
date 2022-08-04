using BenchmarkDotNet.Attributes;
using BusinessModel;
using DbModel;
using DocumentDbModel.AirportDocument;
using Microsoft.Extensions.DependencyInjection;
using Services;
using IServiceProvider = Services.IServiceProvider;

namespace UnitTests;

public class AirportFinderServiceUnitTests
{
    private IUnitOfWork _dbUnitOfWork;
    private IAirportService _airportService;
    private AirportDbContext _airportDbContext;
    private HttpClient _httpClient;
    private IServiceProviderSingleton _serviceProviderSingleton;
    private IMongoDbContext _mongoDbContext;
    private IServiceProvider _serviceProvider;
    public AirportFinderServiceUnitTests(AirportDbContext airportDbContext, IHttpClientFactory httpClientFactory, IServiceProviderSingleton serviceProviderSingleton, IMongoDbContext mongoDbContext)
    {
        _airportDbContext = airportDbContext;
        _dbUnitOfWork = new DbUnitOfWork(_airportDbContext);
        _httpClient = httpClientFactory.CreateClient();
        _serviceProviderSingleton = serviceProviderSingleton;
        _mongoDbContext = mongoDbContext;
        _airportService = _serviceProvider.GetAirportService();
    }
    
    [Theory]
    [Benchmark]
    [InlineData("bcn")]
    public void GetAirport(string iataCode)
    {
        var airport = _airportService.GetAirport(iataCode);

        Assert.IsType<Airport>(airport);

    }
    
    
    [Theory]
    [Benchmark]
    [InlineData("bcn")]
    public void GetAirportFromDocumentDb(string iataCode)
    {
        var airport = _airportService.GetAirport(iataCode);

        Assert.IsType<Airport>(airport);
    }
    
    [Theory]
    [Benchmark]
    [InlineData("bcn")]
    public void GetAirportFromJObject(string iataCode)
    {
        var airport = _airportService.GetAirport(iataCode);

        Assert.IsType<Airport>(airport);
    }
}