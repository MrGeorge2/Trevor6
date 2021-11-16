using Binance.Net.Interfaces;
using FancyApollo.DTO.DT;
using Trevor6.Abstract;

namespace Trevor6.ExchangeData.DBModels;

public abstract class TrevorKline : DTObject, ITrevorKline
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


    /// <summary>
    /// Open Time
    /// </summary>
    public DateTime OpenTime
    {
        get => IndexedAttributes.GetAttribute<DateTime>(nameof(OpenTime));
        private set => IndexedAttributes.SetAttribute<DateTime>(nameof(OpenTime), value);
    }

    /// <summary>
    /// Open price
    /// </summary>
    public decimal Open
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(Open));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(Open), value);
    }

    /// <summary>
    /// High price
    /// </summary>
    public decimal High
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(High));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(High), value);
    }

    /// <summary>
    /// Low price
    /// </summary>
    public decimal Low
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(Low));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(Low), value);
    }

    /// <summary>
    /// Close price
    /// </summary>
    public decimal Close
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(Close));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(Close), value);
    }

    /// <summary>
    /// Base Volume
    /// </summary>
    public decimal BaseVolume
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(BaseVolume));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(BaseVolume), value);
    }

    /// <summary>
    /// Close Time
    /// </summary>
    public DateTime CloseTime
    {
        get => IndexedAttributes.GetAttribute<DateTime>(nameof(CloseTime));
        private set => IndexedAttributes.SetAttribute<DateTime>(nameof(CloseTime), value);
    }

    /// <summary>
    /// QuoteVolume
    /// </summary>
    public decimal QuoteVolume
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(QuoteVolume));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(QuoteVolume), value);
    }

    /// <summary>
    /// Trade count
    /// </summary>
    public int TradeCount
    {
        get => IndexedAttributes.GetAttribute<int>(nameof(TradeCount));
        private set => IndexedAttributes.SetAttribute<int>(nameof(TradeCount), value);
    }

    /// <summary>
    /// Taker buy base volume
    /// </summary>
    public decimal TakerBuyBaseVolume
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(TakerBuyBaseVolume));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(TakerBuyBaseVolume), value);
    }

    /// <summary>
    /// Taker buy quote volume
    /// </summary>
    public decimal TakerBuyQuoteVolume
    {
        get => IndexedAttributes.GetAttribute<decimal>(nameof(TakerBuyQuoteVolume));
        private set => IndexedAttributes.SetAttribute<decimal>(nameof(TakerBuyQuoteVolume), value);
    }
}
