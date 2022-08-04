using System.Text.Json.Serialization;
using DbModel;

namespace BusinessModel;

public class Travelers
{
    public Travelers()
    {
        
    }

    public Travelers(TravelersEntity travelersEntity)
    {
        Score = travelersEntity.Score;
    }
    
    public int Score { get; set; }

    public static TravelersEntity ConvertModelToEntity(Travelers travelers)
    {
        TravelersEntity travelersEntity = new TravelersEntity()
        {
            Score = travelers.Score
        };
        return travelersEntity;
    }
}