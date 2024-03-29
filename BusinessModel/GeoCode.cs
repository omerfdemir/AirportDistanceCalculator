using System.Text.Json.Serialization;
using DbModel;

namespace BusinessModel;

public class GeoCode
{
    public GeoCode()
    {
        
    }

    public GeoCode(GeoCodeEntity geoCodeEntity)
    {
        Latitude = geoCodeEntity.Latitude;
        Longitude = geoCodeEntity.Longitude;
    }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }

    public static GeoCodeEntity ConvertModelToEntity(GeoCode geoCode)
    {
        GeoCodeEntity geoCodeEntity = new GeoCodeEntity()
        {
            Latitude = geoCode.Latitude,
            Longitude = geoCode.Longitude
        };

        return geoCodeEntity;
    }
}