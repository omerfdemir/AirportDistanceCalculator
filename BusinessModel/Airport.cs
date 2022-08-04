using System.Text.Json.Serialization;
using DbModel;
using Newtonsoft.Json;

namespace BusinessModel
{
    public class Airport
    {

        public Airport()
        {

        }

        public Airport(AirportEntity airportEntity, GeoCodeEntity geoCodeEntity, AddressEntity addressEntity, AnalyticsEntity analyticsEntity, TravelersEntity travelersEntity)
        {
            Type = airportEntity.Type;
            SubType = airportEntity.SubType;
            Name = airportEntity.Name;
            DetailedName = airportEntity.DetailedName;
            AirportId = airportEntity.AirportId;
            TimeZoneOffset = airportEntity.TimeZoneOffset;
            IATACode = airportEntity.IATACode;
            GeoCode = new GeoCode(geoCodeEntity);
            Address = new Address(addressEntity);
            Analytics = new Analytics(analyticsEntity, travelersEntity);

        }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        public string SubType { get; set; }

        public string Name { get; set; }

        public string DetailedName { get; set; }

        [JsonProperty("id")]
        public string AirportId { get; set; }

        public string TimeZoneOffset { get; set; }

        public string IATACode { get; set; }

        public GeoCode GeoCode { get; set; }

        public Address Address { get; set; }

        public Analytics Analytics { get; set; }

        public static AirportEntity ConvertModelToEntity(Airport airport)
        {
            AirportEntity airportEntity = new AirportEntity()
            {
                Address = Address.ConvertModelToEntity(airport.Address),
                Analytics = BusinessModel.Analytics.ConvertModelToEntity(airport.Analytics),
                Name = airport.Name,
                Type = airport.Type,
                AirportId = airport.AirportId,
                DetailedName = airport.DetailedName,
                SubType = airport.SubType,
                GeoCode = GeoCode.ConvertModelToEntity(airport.GeoCode),
                TimeZoneOffset = airport.TimeZoneOffset,
                IATACode = airport.IATACode

            };

            return airportEntity;
        }
        
    }
}