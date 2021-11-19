using MongoDB.Bson;
using System.Collections;
using Trevor6.Abstract;


namespace Trevor6.Learning;

public struct Sample : IEnumerable<double>
{
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

    public IEnumerator<double> GetEnumerator()
    {
        var enumerable = new double[] 
        { 
            (double)Open,
            (double)High,
            (double)Low,
            (double)Close,
            (double)BaseVolume,
            (double)QuoteVolume,
            (double)TradeCount,
            (double)TakerBuyBaseVolume, 
            ((double)TakerBuyQuoteVolume) 
        }.AsEnumerable();

        return enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


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



