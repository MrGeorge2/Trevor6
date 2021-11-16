using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
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
        :this(apiKeyLoader.Key, apiKeyLoader.Secret)
    {}

    public async IAsyncEnumerable<ITrevorKline> GetHistoricalClines(string symbol, TrevorKlineInterval interval, DateTime start, DateTime? end = null, CancellationToken token = default)
    {
        var lastCandletime = start;

        if (end == null)
            end = DateTime.Now;

        while (lastCandletime < end)
        {
            if (token.IsCancellationRequested)
                break;

            var klines = await getHistoricalClinesWhileSuccess(symbol, interval, lastCandletime, end, token);
            foreach (var kline in klines)
            {
                lastCandletime = kline.CloseTime;

                yield return kline.GetTrevorKline(symbol);
            }
        }
    }

    private async Task<IEnumerable<IBinanceKline>> getHistoricalClinesWhileSuccess(string symbol, TrevorKlineInterval interval, DateTime start, DateTime? end, CancellationToken token = default)
    {
        while (true)
        {
            var klines = await _client.Spot.Market.GetKlinesAsync(symbol, GetKlineInterval(interval), start, end, null, token);

            if (klines.Success)
                return klines.Data;
        }
    }




    /// <summary>
    /// Returns kline interval in trevors enum
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private static KlineInterval GetKlineInterval(TrevorKlineInterval interval)
    {
        switch (interval)
        {
            case TrevorKlineInterval.OneMinute:
                return KlineInterval.OneMinute;

            case TrevorKlineInterval.ThreeMinutes:
                return KlineInterval.ThreeMinutes;

            case TrevorKlineInterval.FiveMinutes:
                return KlineInterval.FiveMinutes;

            case TrevorKlineInterval.FifteenMinutes:
                return KlineInterval.FifteenMinutes;

            case TrevorKlineInterval.ThirtyMinutes:
                return KlineInterval.ThirtyMinutes;

            default:
                throw new NotImplementedException();
        }
    }
}

