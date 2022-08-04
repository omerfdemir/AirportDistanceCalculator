using System.Text.Json.Serialization;
using DbModel;

namespace BusinessModel;

public class Analytics
{
    public Analytics()
    {
        
    }

    public Analytics(AnalyticsEntity analyticsEntity, TravelersEntity travelersEntity)
    {
        Travelers = new Travelers(travelersEntity);
    }
    
    public Travelers Travelers { get; set; }

    public static AnalyticsEntity ConvertModelToEntity(Analytics analytics)
    {
        TravelersEntity travelersEntity = BusinessModel.Travelers.ConvertModelToEntity(analytics.Travelers);
        AnalyticsEntity analyticsEntity = new AnalyticsEntity()
        {
            Travelers = travelersEntity
        };

        return analyticsEntity;
    }
}