using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModel
{
    public class DbUnitOfWork : IUnitOfWork
    {
        private readonly AirportDbContext _dbContext;

        public DbUnitOfWork(AirportDbContext airportDbContext)
        {
            _dbContext = airportDbContext;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private IRepository<AirportEntity> _airportRepository;

        public IRepository<AirportEntity> AirportRepository
        {
            get
            {
                if(_airportRepository == null)
                {
                   _airportRepository = new Repository<AirportEntity>(_dbContext);
                }

                return _airportRepository;
            }
        }

        private IRepository<AirportEntityJObject> _airportJObjectRepository;
        
        public IRepository<AirportEntityJObject> AirportJObjectRepository
        {
            get
            {
                if(_airportJObjectRepository == null)
                {
                    _airportJObjectRepository = new Repository<AirportEntityJObject>(_dbContext);
                }

                return _airportJObjectRepository;
            }
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
