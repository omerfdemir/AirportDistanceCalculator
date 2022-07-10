using BusinessModel;
using DbModel;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Services
{
    public class AirportService : IAirportService
    {
        private IServiceProvider _serviceProvider;
        private IServiceProviderSingleton _serviceProviderSingleton;
        private IUnitOfWork _dbUnitOfWork;
        private IAirportFinderService _airportFinderService;

        public AirportService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider, IServiceProviderSingleton serviceProviderSingleton)
        {
            _dbUnitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _serviceProviderSingleton = serviceProviderSingleton;
            _airportFinderService = _serviceProviderSingleton.GetAirportFinderService();

        }

        public async Task<bool> AddAirport(AirportEntity airportEntity)
        {
            _dbUnitOfWork.AirportRepository.Insert(airportEntity);

            await _dbUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAirport(int id)
        {
            var airport = await _dbUnitOfWork.AirportRepository.FindOneAsync(r => r.Id == id);

            if (airport != null)
            {
                _dbUnitOfWork.AirportRepository.Delete(airport);

                await _dbUnitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Airport not found");
            }

        }

        public async Task<AirportEntity> UpdateAirport(AirportEntity airportEntity)
        {
            var airport = await _dbUnitOfWork.AirportRepository.FindOneAsync(r => r.Id == airportEntity.Id);


            if (airport != null)
            {
                _dbUnitOfWork.AirportRepository.Update(airportEntity);

                await _dbUnitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Airport not found");
            }

            return airport;
        }

        public async Task<double> CalculateDistanceBetweenTwoAirportsInMiles(string iata1, string iata2)
        {

            var airport1 = await GetAirport(iata1);
            var airport2 = await GetAirport(iata2);

            var distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInMiles(airport1.GeoCode, airport2.GeoCode);

            return distance;
        }

        public async Task<double> CalculateDistanceBetweenTwoAirportsInKm(string iata1, string iata2)
        {

            var airport1 = await GetAirport(iata1);
            var airport2 = await GetAirport(iata2);

            var distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInKm(airport1.GeoCode, airport2.GeoCode);

            return distance;
        }

        public async Task<Airport> GetAirport(string iata)
        {
            var airportEntity = await _dbUnitOfWork.AirportRepository.FindOneAsync(airport => airport.IATACode == iata.ToUpper(),
                r => r.Include(airport => airport.Address).Include(airport => airport.Analytics).ThenInclude(analytics => analytics.Travelers).Include(airport => airport.GeoCode));

            if (airportEntity != null)
            {
                return new Airport(airportEntity, airportEntity.GeoCode, airportEntity.Address, airportEntity.Analytics, airportEntity.Analytics.Travelers);
            }
            
            Airport airport = await _airportFinderService.FindAirport(iata.ToUpper());

            AirportEntity newAirportEntity = Airport.ConvertModelToEntity(airport);

            _dbUnitOfWork.AirportRepository.Insert(newAirportEntity);

            await _dbUnitOfWork.SaveChangesAsync();

            return airport;
        }
    }
}
