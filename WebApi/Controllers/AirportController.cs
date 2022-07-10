using BusinessModel;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AirportController : BaseApiController
    {
        private IAirportService _airportService;
        private const int IATALength = 3;
        public AirportController(ILogger<AirportController> logger, Services.IServiceProvider serviceProvider, IServiceProviderSingleton serviceProviderSingleton)
        {
            _airportService = serviceProvider.GetAirportService();
        }


        [HttpGet("{airport1}/{airport2}")]
        public async Task<IActionResult> GetDistanceBetweenTwoAirportsInMiles([MaxLength(IATALength)][MinLength(IATALength)] string airport1, [MaxLength(IATALength)][MinLength(IATALength)] string airport2)
        {
            double distance;

            try
            {
                distance = await _airportService.CalculateDistanceBetweenTwoAirportsInMiles(airport1, airport2);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok(distance);
        }

        [HttpGet("{airport1}/{airport2}")]
        public async Task<IActionResult> GetDistanceBetweenTwoAirportsInKm([MaxLength(IATALength)][MinLength(IATALength)] string airport1,
            [MaxLength(IATALength)][MinLength(IATALength)] string airport2)
        {
            double distance;

            try
            {
                distance = await _airportService.CalculateDistanceBetweenTwoAirportsInKm(airport1, airport2);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok(distance);
        }

        [HttpGet("{airportName}")]
        public async Task<IActionResult> GetAirportDetails([MaxLength(IATALength)][MinLength(IATALength)] string airportName)
        {
            Airport airport = new Airport();
            try
            {
                airport = await _airportService.GetAirport(airportName);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok(airport);
        }
    }
}
