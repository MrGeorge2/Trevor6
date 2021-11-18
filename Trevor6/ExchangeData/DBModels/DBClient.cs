using FancyApollo.DTO.Abstract;
using FancyApollo.DTO.Manager;
using FancyApollo.DTO.Registrator;
using MongoDB.Driver;


namespace Trevor6.ExchangeData.DBModels;


public static class DBClient
{
    private const string KLINE_DB_NAME = "Klines";

    static DBClient()
    {
        DTORegistrator.RegisterFromRegistrator(new DBRegistrator());
    }

    public static IDTOManager GetDBClient()
        => new DTOMongoManager(GetMongoClient());

    public static IMongoCollection<TKline> GetKlineCollection<TKline>() where TKline : TrevorKline
        => GetMongoClient().GetDatabase(KLINE_DB_NAME).GetCollection<TKline>(typeof(TKline).Name);

    public static IMongoClient GetMongoClient()
        => new MongoClient($"mongodb://fancyadmin:f4nc1P4ssr0d@localhost:27017");
}
