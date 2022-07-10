using System.Text.Json.Serialization;
using DbModel;

namespace BusinessModel;

public class Address
{
    public Address()
    {
        
    }

    public Address(AddressEntity addressEntity)
    {
        CityName = addressEntity.CityName;
        CityCode = addressEntity.CityCode;
        CountryName = addressEntity.CountryName;
        CountryCode = addressEntity.CountryCode;
        RegionCode = addressEntity.RegionCode;
    }
    
    [JsonPropertyName("cityName")]
    public string CityName { get; set; }

    [JsonPropertyName("cityCode")]
    public string CityCode { get; set; }

    [JsonPropertyName("countryName")]
    public string CountryName { get; set; }

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("regionCode")]
    public string RegionCode { get; set; }

    public static AddressEntity ConvertModelToEntity(Address address)
    {
        AddressEntity addressEntity = new AddressEntity()
        {
            CityCode = address.CityCode,
            CityName = address.CityName,
            CountryCode = address.CountryCode,
            CountryName = address.CountryName,
            RegionCode = address.RegionCode
        };
        return addressEntity;
    }
}