using Binance.Net.Interfaces;
using Trevor6.Abstract;
using Trevor6.ExchangeData.DBModels;

namespace Trevor6.ExchangeData.BinanceClient;

public static class Extensions
{
    public static TTrevorKline GetTrevorKline<TTrevorKline>(this IBinanceKline binanceKline, string symbol) where TTrevorKline : ITrevorKline
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

        /*
        var trevorKline = new TTrevorKline(symbol: symbol, openTime: binanceKline.OpenTime, open: binanceKline.Open, 
            high: binanceKline.High, low: binanceKline.Low, close: binanceKline.Close, 
            baseVolume: binanceKline.BaseVolume, closeTime: binanceKline.CloseTime, 
            quoteVolume: binanceKline.QuoteVolume, tradeCount: binanceKline.TradeCount, 
            takerBuyBaseVolume: binanceKline.TakerBuyBaseVolume, takerBuyQuoteVolume: binanceKline.TakerBuyQuoteVolume);
        return trevorKline;
        */
    }

}

