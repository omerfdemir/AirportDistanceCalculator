using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel;

[Table("AirportsJObject")]
public class AirportEntityJObject
{

    public AirportEntityJObject()
    {
        
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string IATACode { get; set; }
    
    public string JSON { get; set; }
    
    
}