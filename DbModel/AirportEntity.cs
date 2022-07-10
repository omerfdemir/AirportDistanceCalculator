using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel
{
    [Table("Airports")]

    public class AirportEntity
    {
        public AirportEntity()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Type { get; set; }

        public string SubType { get; set; }

        public string Name { get; set; }

        public string DetailedName { get; set; }

        public string AirportId { get; set; }

        public string TimeZoneOffset { get; set; }

        public string IATACode { get; set; }
        
        public int? GeoCodeId { get; set; }
    
        [ForeignKey("GeoCodeId")]
        public GeoCodeEntity GeoCode { get; set; }
        
        public int? AddressId { get; set; }
    
        [ForeignKey("AddressId")]
        public AddressEntity Address { get; set; }
        
        public int? AnalyticsId { get; set; }
    
        [ForeignKey("AnalyticsId")]
        public AnalyticsEntity Analytics { get; set; }
    }
}