namespace DocumentDbModel.AirportDocument;

public interface IMongoDbContext
{
    IMongoDbCollectionRepository<AirportDocument> AirportCollection { get; }
}