using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel;

[Table("GeoCode")]
public class GeoCodeEntity
{
    public GeoCodeEntity()
    {
        
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
}