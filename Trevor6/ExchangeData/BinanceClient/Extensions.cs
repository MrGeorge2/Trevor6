using Binance.Net.Interfaces;
using Trevor6.Abstract;
using Trevor6.ExchangeData.DBModels;

namespace Trevor6.ExchangeData.BinanceClient;

public static class Extensions
{
    public static ITrevorKline GetTrevorKline(this IBinanceKline binanceKline, string symbol)
    {
        var trevorKline = new TrevorKline(symbol: symbol, openTime: binanceKline.OpenTime, open: binanceKline.Open, 
            high: binanceKline.High, low: binanceKline.Low, close: binanceKline.Close, 
            baseVolume: binanceKline.BaseVolume, closeTime: binanceKline.CloseTime, 
            quoteVolume: binanceKline.QuoteVolume, tradeCount: binanceKline.TradeCount, 
            takerBuyBaseVolume: binanceKline.TakerBuyBaseVolume, takerBuyQuoteVolume: binanceKline.TakerBuyQuoteVolume);
        return trevorKline;

    }

}

