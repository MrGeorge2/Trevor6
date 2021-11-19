using FancyApollo.DTO.Abstract;
using MongoDB.Driver;
using System.Collections.Immutable;
using Trevor6.ExchangeData.DBModels;
using Trevor6.Learning.Abstract;

namespace Trevor6.Learning;

public class StockMarket<TKline> where TKline : TrevorKline
{
    public string Symbol { get; }

    /// <summary>
    /// In case there ont candle is 15 minuts - then 192 candles == 2 days
    /// </summary>
    public const uint NUMBER_OF_CANDLES_IN_SAMPLE = 192;

    private readonly IEnumerable<TKline> _klines;

    public StockMarket()
    {
        Symbol = typeof(TKline).Name;

        _klines = getTrevorKlines();
    }

    public void StockMarketLoop(ITrader trader)
    {
        StockMarketLoop(new ITrader[] { trader });
    }

    /// <summary>
    /// Stock market loop
    /// </summary>
    /// <param name="trader"></param>
    public void StockMarketLoop(ITrader[] traders)
    {
        foreach (var sample in GetSamples())
        {
            foreach (var trader in traders)
            {
                trader.AddNewSample(sample.AsEnumerable());

                if (trader.IsEliminated)
                    return;
            }
        }
    }

    /// <summary>
    /// Loads klines for environment
    /// </summary>
    /// <returns></returns>
    private IEnumerable<TKline> getTrevorKlines()
        => DBClient.GetKlineCollection<TKline>()
        .AsQueryable()
        .OrderBy(kline => kline.CloseTime)
        .AsEnumerable();

    /// <summary>
    /// Generator method for returning samples
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IEnumerable<Sample>> GetSamples()
    {
        var klineBuffer = new Queue<TKline>();

        foreach (var kline in _klines)
        {
            klineBuffer.Enqueue(kline);

            if (klineBuffer.Count >= NUMBER_OF_CANDLES_IN_SAMPLE)
            {
                yield return createNormalizedSample(ImmutableArray.Create(klineBuffer.ToArray()));

                klineBuffer.Dequeue();
            }
        }
    }

    /// <summary>
    /// Create normalized sample
    /// </summary>
    /// <param name="notNormalizedSample"></param>
    /// <returns></returns>
    private ImmutableArray<Sample> createNormalizedSample(ImmutableArray<TKline> notNormalizedSample)
    {
        var maxOpen = notNormalizedSample.Max(x => x.Open);
        var maxHigh = notNormalizedSample.Max(x => x.High);
        var maxLow = notNormalizedSample.Max(x => x.Low);
        var maxClose = notNormalizedSample.Max(x => x.Close);
        var maxBaseVolume = notNormalizedSample.Max(x => x.BaseVolume);
        var maxQuoteVolume = notNormalizedSample.Max(x => x.QuoteVolume);
        var maxTradeCount = notNormalizedSample.Max(x => x.TradeCount);
        var maxTakerBuyVolume = notNormalizedSample.Max(x => x.TakerBuyBaseVolume);
        var maxTakerBuyQuoteVolume = notNormalizedSample.Max(x => x.TakerBuyQuoteVolume);

        var normalizedSample = notNormalizedSample.Select(x => new Sample()
        {
            Open = x.Open / maxOpen,
            High = x.High / maxHigh,
            Low = x.Low / maxLow,
            Close = x.Close / maxClose,
            BaseVolume = x.BaseVolume / maxBaseVolume,
            QuoteVolume = x.QuoteVolume / maxQuoteVolume,
            TradeCount = x.TradeCount / maxTradeCount,
            TakerBuyBaseVolume = x.TakerBuyBaseVolume / maxTakerBuyVolume,
            TakerBuyQuoteVolume = x.TakerBuyQuoteVolume / maxTakerBuyQuoteVolume,
        });

        return ImmutableArray.Create(normalizedSample.ToArray());
    }
}