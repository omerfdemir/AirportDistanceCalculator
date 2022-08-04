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
    
    public string CityName { get; set; }


    public string CityCode { get; set; }

    public string CountryName { get; set; }

    public string CountryCode { get; set; }

    public string RegionCode { get; set; }

    public static AddressEntity ConvertModelToEntity(Address address)
    {
        AddressEntity addressEntity = new AddressEntity()
        {
            CityCode = address.CityCode,
            CityName = address.CityName,
            CountryCode = address.CountryCode,
            CountryName = address.CountryName,
            RegionCode = address.RegionCode,
        };
        return addressEntity;
    }


}