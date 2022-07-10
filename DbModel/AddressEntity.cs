using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel
{
    [Table("Addresses")]

    public class AddressEntity
    {
        public AddressEntity()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string CityName { get; set; }

        public string CityCode { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public string RegionCode { get; set; }
    }
}