using BusinessModel;

namespace Services
{
    public interface IAirportFinderService
    {

        Task<Airport> FindAirport(string iata);
        Task ConnectOAuth();
    }
}
