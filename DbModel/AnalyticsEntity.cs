using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel;

[Table("Analytics")]
public class AnalyticsEntity
{
    public AnalyticsEntity()
    {
        
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int? TravelersId { get; set; }
    
    [ForeignKey("TravelersId")]
    public TravelersEntity Travelers { get; set; }
}