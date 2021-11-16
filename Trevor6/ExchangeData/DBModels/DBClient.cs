using FancyApollo.DTO.Abstract;
using FancyApollo.DTO.Manager;
using FancyApollo.DTO.Registrator;
using MongoDB.Driver;


namespace Trevor6.ExchangeData.DBModels;


public static class DBClient
{
    static DBClient()
    {
        DTORegistrator.RegisterFromRegistrator(new DBRegistrator());
    }

    public static IDTOManager GetDBClient()
    {
        var mongoClient = new MongoClient($"mongodb://fancyadmin:f4nc1P4ssr0d@localhost:27017");
        return new DTOMongoManager(mongoClient);
    }
}
