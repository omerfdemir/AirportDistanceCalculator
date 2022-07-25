using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocumentDbModel.AirportDocument;

public class MongoDbContext: IMongoDbContext
{
    public const string CollectionAirport = "AirportCollection";
    private readonly IMongoDatabase _database = null;

    public IMongoDatabase MongoDatabase
    {
        get
        {
            return _database;
        }
    }

    public MongoDbContext(string mongoDbConnectionString, string mongoDbDatabase)
    {
        IMongoClient client = new MongoClient(mongoDbConnectionString);
        if (client != null)
        {
            _database = client.GetDatabase(mongoDbDatabase);
        }
        else
        {
            string errMsg = string.Format("ERROR: cannot connect to MongoDB {0}", mongoDbDatabase);
            throw new InvalidOperationException(errMsg);
        }
    }
    
    public MongoDbContext(IOptions<DocumentDbSettings> settings)
    {
        IMongoClient client = new MongoClient(settings.Value.MongoDbConnectionString);
        if (client != null)
        {
            _database = client.GetDatabase(settings.Value.MongoDbDatabase);
        }
        else
        {
            string errMsg = string.Format("ERROR: cannot connect to MongoDB {0}", settings.Value.MongoDbDatabase);
            throw new InvalidOperationException(errMsg);
        }
    }
    
    public IMongoDbCollectionRepository<AirportDocument> AirportCollection
    {
        get
        {
            if (_airportCollection == null)
            {
                IMongoCollection<AirportDocument> collection = _database.GetCollection<AirportDocument>(CollectionAirport);
                _airportCollection = new MongoDbCollectionRepository<AirportDocument>(collection, CollectionAirport);
            }

            return _airportCollection;
        }
    }
    private IMongoDbCollectionRepository<AirportDocument> _airportCollection;

}