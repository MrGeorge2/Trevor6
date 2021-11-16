

using Trevor6.Enums;

namespace Trevor6.Abstract;

public interface IExchangeClient
{
    IAsyncEnumerable<ITrevorKline> GetHistoricalClines(string symbol, TrevorKlineInterval interval, DateTime start, DateTime? end, CancellationToken token);
}
