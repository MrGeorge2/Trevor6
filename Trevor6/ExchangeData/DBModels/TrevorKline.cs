using Binance.Net.Interfaces;
using FancyApollo.DTO.DT;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Trevor6.Abstract;

namespace Trevor6.ExchangeData.DBModels;

public abstract class TrevorKline : ITrevorKline
{
    public TrevorKline(DateTime openTime, decimal open,
        decimal high, decimal low, decimal close, decimal baseVolume,
        DateTime closeTime, decimal quoteVolume, int tradeCount, decimal
        takerBuyBaseVolume, decimal takerBuyQuoteVolume)
    {;
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
    
    [BsonId]
    public ObjectId ID { get; set; }

    /// <summary>
    /// Open Time
    /// </summary>
    public DateTime OpenTime { get; private set; }
    
    /// <summary>
    /// Open price
    /// </summary>
    public decimal Open { get; private set; }


    /// <summary>
    /// High price
    /// </summary>
    public decimal High { get; private set; }

    /// <summary>
    /// Low price
    /// </summary>
    public decimal Low { get; private set; }

    /// <summary>
    /// Close price
    /// </summary>
    public decimal Close { get; private set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    public decimal BaseVolume { get; private set; }

    /// <summary>
    /// Close Time
    /// </summary>
    public DateTime CloseTime { get; private set; }

    /// <summary>
    /// QuoteVolume
    /// </summary>
    public decimal QuoteVolume { get; private set; }

    /// <summary>
    /// Trade count
    /// </summary>
    public int TradeCount { get; private set; }

    /// <summary>
    /// Taker buy base volume
    /// </summary>
    public decimal TakerBuyBaseVolume { get; private set; }

    /// <summary>
    /// Taker buy quote volume
    /// </summary>
    public decimal TakerBuyQuoteVolume { get; private set; }
}
