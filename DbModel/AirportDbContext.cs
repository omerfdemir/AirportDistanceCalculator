using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbModel
{

    public class AirportDbContext : DbContext
    {
        public AirportDbContext(DbContextOptions<AirportDbContext> options) : base(options)
        {
        }

        public DbSet<AirportEntity> Airports { get; set; }
        
        public DbSet<AirportEntityJObject> AirportsJObject { get; set; }

    }
}
