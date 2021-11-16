namespace Trevor6.Abstract;

public interface ITrevorKline
{
    string Symbol { get; }

    //
    // Summary:
    //     The time this candlestick opened
    DateTime OpenTime { get; }

    //
    // Summary:
    //     The price at which this candlestick opened
    decimal Open { get; }

    //
    // Summary:
    //     The highest price in this candlestick
    decimal High { get; }

    //
    // Summary:
    //     The lowest price in this candlestick
    decimal Low { get; }

    //
    // Summary:
    //     The price at which this candlestick closed
    decimal Close { get; }

    //
    // Summary:
    //     The volume traded during this candlestick
    decimal BaseVolume { get; }

    //
    // Summary:
    //     The close time of this candlestick
    DateTime CloseTime { get; }

    //
    // Summary:
    //     The volume traded during this candlestick in the asset form
    decimal QuoteVolume { get; }

    //
    // Summary:
    //     The amount of trades in this candlestick
    int TradeCount { get; }

    //
    // Summary:
    //     Taker buy base asset volume
    decimal TakerBuyBaseVolume { get; }

    //
    // Summary:
    //     Taker buy quote asset volume
    decimal TakerBuyQuoteVolume { get; }
}
