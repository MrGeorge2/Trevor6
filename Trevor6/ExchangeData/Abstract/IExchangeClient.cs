

using Trevor6.Enums;

namespace Trevor6.Abstract;

public interface IExchangeClient
{ 
    IAsyncEnumerable<TKLine> GetHistoricalClines<TKLine>(string symbol, TrevorKlineInterval interval, DateTime start,
        DateTime? end = null, CancellationToken token = default) where TKLine : ITrevorKline;
}
