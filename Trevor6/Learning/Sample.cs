using MongoDB.Bson;
using Trevor6.Abstract;


namespace Trevor6.Learning;

public struct Sample
{
    public decimal Profit { get; internal set; }

    #region Trevor kline data

    public decimal Open { get; internal set; }

    public decimal High { get; internal set; }

    public decimal Low { get; internal set; }

    public decimal Close { get; internal set; }

    public decimal BaseVolume { get; internal set; }

    public decimal QuoteVolume { get; internal set; }

    public int TradeCount { get; internal set; }

    public decimal TakerBuyBaseVolume { get; internal set; }

    public decimal TakerBuyQuoteVolume { get; internal set; }

    #endregion

    private void getValuesFromTrevorKline(ITrevorKline trevorKline)
    {
        Open = trevorKline.Open;
        High = trevorKline.High;
        Low = trevorKline.Low;
        Close = trevorKline.Close;
        BaseVolume = trevorKline.BaseVolume;
        QuoteVolume = trevorKline.QuoteVolume;
        TradeCount = trevorKline.TradeCount;
        TakerBuyBaseVolume = trevorKline.TakerBuyBaseVolume;
        TakerBuyQuoteVolume = trevorKline.TakerBuyQuoteVolume;
    }
}



