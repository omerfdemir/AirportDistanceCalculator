using System.Text.Json.Serialization;
using DbModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BusinessModel
{
    public class AirportJObject
    {

        public AirportJObject()
        {

        }

        public int Id { get; set; }

        public string IATACode { get; set; }

        public string JSON { get; set; }

        public static AirportEntityJObject ConvertModelToEntity(Airport airport)
        {
            AirportEntityJObject airportEntityJObject = new AirportEntityJObject()
            {
                JSON = JsonSerializer.Serialize(airport),
                IATACode = airport.IATACode
            };

            return airportEntityJObject;
        }
    }
}