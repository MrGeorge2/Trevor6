using SharpNeat.Core;
using SharpNeat.Phenomes;
using Trevor6.ExchangeData.DBModels;
using Trevor6.Learning;

namespace Trevor6.Neat;

public class StockMarketEvaluator : IPhenomeEvaluator<IBlackBox>
{
    private ulong _evalCount;
    private bool _stopConditionSatisfied;

    /// <summary>
    /// Gets the total number of evaluations that have been performed.
    /// </summary>
    public ulong EvaluationCount
    {
        get { return _evalCount; }
    }

    /// <summary>
    /// Gets a value indicating whether some goal fitness has been achieved and that
    /// the the evolutionary algorithm/search should stop. This property's value can remain false
    /// to allow the algorithm to run indefinitely.
    /// </summary>
    public bool StopConditionSatisfied => false;

    public FitnessInfo Evaluate(IBlackBox brain)
    {
        var trader = new NeatTrader(brain);
        double fitness = 0;

        fitness += evaluateOnCurrentyPair<BTCUSDT>(trader);
        fitness += evaluateOnCurrentyPair<ETHUSDT>(trader);
        fitness += evaluateOnCurrentyPair<BNBUSDT>(trader);
        fitness += evaluateOnCurrentyPair<ADAUSDT>(trader);
        fitness += evaluateOnCurrentyPair<XRPUSDT>(trader);
        fitness += evaluateOnCurrentyPair<DOTUSDT>(trader);
        fitness += evaluateOnCurrentyPair<DOGEUSDT>(trader);
        fitness += evaluateOnCurrentyPair<SHIBUSDT>(trader);
        fitness += evaluateOnCurrentyPair<LTCUSDT>(trader);

        return new FitnessInfo(fitness, fitness);
    }

    private double evaluateOnCurrentyPair<TPair>(NeatTrader trader) where TPair :TrevorKline
    {
        string currencyPair = typeof(TPair).Name;

        var stockMarket = new StockMarket<TPair>();
        stockMarket.StockMarketLoop(trader);

        double fitness = 0;

        if (trader.Profit > 0)
        {
            fitness = (double)trader.Profit;
            Console.WriteLine($"\n\nCurrencyPair={currencyPair} \t Profi={Math.Round(trader.Profit, 3)}\t Fitness={fitness} \t Profitable trades={trader.NumberOfProfitabletrades} \t Non profitable trades={trader.NumberOfNonProfitableTrades} !! \n\n");
        }

        return fitness;
    }

    public void Reset()
    {
    }
}
