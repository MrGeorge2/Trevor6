using Binance.Net.Interfaces;
using Trevor6.Abstract;

namespace Trevor6.ExchangeData.DBModels;

public class TrevorKline : ITrevorKline
{
    public TrevorKline(string symbol, DateTime openTime, decimal open, 
        decimal high, decimal low, decimal close, decimal baseVolume,
        DateTime closeTime,decimal quoteVolume,int tradeCount,decimal 
        takerBuyBaseVolume, decimal takerBuyQuoteVolume)
    {
        Symbol = symbol;
        OpenTime = openTime;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        BaseVolume = baseVolume;
        CloseTime = closeTime;
        QuoteVolume = quoteVolume;
        TradeCount = tradeCount;
        TakerBuyBaseVolume = takerBuyBaseVolume;
        TakerBuyQuoteVolume = takerBuyQuoteVolume;
    }

    public override string ToString()
    {
        return $"OpenTime={OpenTime} CloseTime={CloseTime} Open={Open} High={High} Low={Low} Close={Close}";
    }
    /// <summary>
    /// Traded symbol
    /// </summary>
    public string Symbol { get;}

    /// <summary>
    /// Open Time
    /// </summary>
    public DateTime OpenTime { get;}

    /// <summary>
    /// Open price
    /// </summary>
    public decimal Open { get;}

    /// <summary>
    /// High price
    /// </summary>
    public decimal High { get;}

    /// <summary>
    /// Low price
    /// </summary>
    public decimal Low { get;}

    /// <summary>
    /// Close price
    /// </summary>
    public decimal Close { get;}


    /// <summary>
    /// Base Volume
    /// </summary>
    public decimal BaseVolume { get;}

    /// <summary>
    /// Close Time
    /// </summary>
    public DateTime CloseTime { get;}

    /// <summary>
    /// QuoteVolume
    /// </summary>
    public decimal QuoteVolume { get;}

    /// <summary>
    /// Trade count
    /// </summary>
    public int TradeCount { get;}

    /// <summary>
    /// Taker buy base volume
    /// </summary>
    public decimal TakerBuyBaseVolume { get;}

    /// <summary>
    /// Taker buy quote volume
    /// </summary>
    public decimal TakerBuyQuoteVolume { get;}
}
