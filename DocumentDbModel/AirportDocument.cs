using BusinessModel;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace DocumentDbModel.AirportDocument;

public class AirportDocument
{
    public AirportDocument()
    {
        
    }
    
    public ObjectId Id { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("subType")]
    public string SubType { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("detailedName")]
    public string DetailedName { get; set; }

    [JsonProperty("id")]
    public string AirportId { get; set; }

    [JsonProperty("timeZoneOffset")]
    public string TimeZoneOffset { get; set; }

    [JsonProperty("iataCode")]
    public string IATACode { get; set; }

    [JsonProperty("geoCode")]
    public GeoCode GeoCode { get; set; }

    [JsonProperty("address")]
    public Address Address { get; set; }

    [JsonProperty("analytics")]
    public Analytics Analytics { get; set; }
    
    public static AirportDocument ConvertModelToDocument(Airport airport)
    {
        AirportDocument airportDocument = new AirportDocument()
        {
            Address = airport.Address,
            Analytics = airport.Analytics,
            Name = airport.Name,
            Type = airport.Type,
            AirportId = airport.AirportId,
            DetailedName = airport.DetailedName,
            SubType = airport.SubType,
            GeoCode = airport.GeoCode,
            TimeZoneOffset = airport.TimeZoneOffset,
            IATACode = airport.IATACode

        };

        return airportDocument;
    }
        
    public static Airport ConvertDocumentToModel(AirportDocument airportDocument)
    {
        Airport airport = new Airport()
        {
            Address = airportDocument.Address,
            Analytics = airportDocument.Analytics,
            Name = airportDocument.Name,
            Type = airportDocument.Type,
            AirportId = airportDocument.AirportId,
            DetailedName = airportDocument.DetailedName,
            SubType = airportDocument.SubType,
            GeoCode = airportDocument.GeoCode,
            TimeZoneOffset = airportDocument.TimeZoneOffset,
            IATACode = airportDocument.IATACode

        };

        return airport;
    }

    
}