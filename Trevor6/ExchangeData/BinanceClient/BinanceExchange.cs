using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using System.Runtime.CompilerServices;
using Trevor6.Abstract;
using Trevor6.Enums;
using Trevor6.ExchangeData;
using Trevor6.ExchangeData.BinanceClient;

namespace Trevor6.Binance.Client;

internal class BinanceExchange : IExchangeClient
{
    private readonly BinanceClient _client;
    public BinanceExchange(string key, string secret)
    {
        _client = new BinanceClient(new BinanceClientOptions()
        {
            ApiCredentials = new ApiCredentials(key, secret)
        }); ;
    }

    public BinanceExchange(ApiKeyLoader apiKeyLoader)
        : this(apiKeyLoader.Key, apiKeyLoader.Secret)
    { }

    public static IExchangeClient CreateClient()
    {
        return new BinanceExchange(new ApiKeyLoader("D:\\prg\\Trevor6\\ApiData.json"));
    }

    /// <summary>
    /// Get historical clines
    /// </summary>
    /// <typeparam name="TKLine"></typeparam>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async IAsyncEnumerable<TKLine> GetHistoricalClines<TKLine>(string symbol, TrevorKlineInterval interval, DateTime start,
        DateTime? end = null, [EnumeratorCancellation] CancellationToken token = default) where TKLine : ITrevorKline
    {
        var lastCandletime = start;

        if (end == null)
            end = DateTime.Now.AddHours(-2);

        while (lastCandletime <= end)
        {
            if (token.IsCancellationRequested)
                break;

            var klines = await getHistoricalClinesWhileSuccess(symbol, interval, lastCandletime, end, token);

            foreach (var kline in klines)
            {
                lastCandletime = kline.CloseTime;

                yield return kline.GetTrevorKline<TKLine>();
            }

# if SCRAPING
            Console.WriteLine($"Symbol: {symbol} lastCandleTime={lastCandletime}");
#endif

        }
    }

    /// <summary>
    /// Hitting API until it retunrs result
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<IEnumerable<IBinanceKline>> getHistoricalClinesWhileSuccess(string symbol, TrevorKlineInterval interval, DateTime start, DateTime? end, CancellationToken token = default)
    {
        while (true)
        {
            var klines = await _client.Spot.Market.GetKlinesAsync(symbol, interval.GetKlineTrevorInterval(), start, end, null, token);

            if (klines.Success)
                return klines.Data;
        }
    }
}

