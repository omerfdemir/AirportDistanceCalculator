using BusinessModel;
using DbModel;

namespace Services
{
    public interface IAirportService
    {

        Task<bool> AddAirport(AirportEntity airportEntity);
        Task<AirportEntity> UpdateAirport(AirportEntity airportEntity);
        Task DeleteAirport(int id);
        Task<double> CalculateDistanceBetweenTwoAirportsInMiles(string airport1, string airport2);
        Task<double> CalculateDistanceBetweenTwoAirportsInKm(string airport1, string airport2);
        Task<Airport> GetAirport(string iata);
        Task<Airport> GetAirportFromDocumentDb(string iata);
        Task<Airport> GetAirportFromJObject(string iata);
    }
}
