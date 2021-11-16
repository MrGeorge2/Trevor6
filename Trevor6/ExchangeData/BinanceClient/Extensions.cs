using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Trevor6.Abstract;
using Trevor6.Enums;
using Trevor6.ExchangeData.DBModels;

namespace Trevor6.ExchangeData.BinanceClient;

public static class Extensions
{
    public static TTrevorKline GetTrevorKline<TTrevorKline>(this IBinanceKline binanceKline) where TTrevorKline : ITrevorKline
    {
        var kline = Activator.CreateInstance(typeof(TTrevorKline),
            binanceKline.OpenTime, binanceKline.Open,
            binanceKline.High, binanceKline.Low, binanceKline.Close,
            binanceKline.BaseVolume, binanceKline.CloseTime,
            binanceKline.QuoteVolume, binanceKline.TradeCount,
            binanceKline.TakerBuyBaseVolume, binanceKline.TakerBuyQuoteVolume
        );

        if (kline == null)
            throw new Exception();

        return (TTrevorKline)kline;
    }

    /// <summary>
    /// Returns kline interval in trevors enum
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static KlineInterval GetKlineTrevorInterval(this TrevorKlineInterval interval)
    {
        return interval switch
        {
            TrevorKlineInterval.OneMinute => KlineInterval.OneMinute,
            TrevorKlineInterval.ThreeMinutes => KlineInterval.ThreeMinutes,
            TrevorKlineInterval.FiveMinutes => KlineInterval.FiveMinutes,
            TrevorKlineInterval.FifteenMinutes => KlineInterval.FifteenMinutes,
            TrevorKlineInterval.ThirtyMinutes => KlineInterval.ThirtyMinutes,
            _ => throw new NotImplementedException(),
        };
    }

}

