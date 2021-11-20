using System.Collections.Immutable;
using Trevor6.Learning.Abstract;


namespace Trevor6.Learning;


public class Trader : ITrader
{
    /// <summary>
    /// Percentage
    /// </summary>
    private const decimal FEE = (decimal)0.00075;
    private const decimal TRADE_AMOUNT_DOLARS = (decimal)100;

    private readonly Stack<Sample> tradeStack = new();
    private bool isInTrade => tradeStack.Any();

    public Trader(string name)
    {
        Profit = 0;
        Name = name;
    }

    public string Name { get; }
    public bool IsEliminated => Profit < 0;

    public decimal Profit { get; private set; }

    public int NumberOfProfitabletrades { get; private set; }

    public int NumberOfNonProfitableTrades { get; private set; }

    public void AddNewSample(IEnumerable<Sample> newSample)
    {
        // new samples are type queue so last sample is the newest one
        var lastSample = newSample.Last();

        Think(newSample, lastSample);
    }

    protected virtual void Think(IEnumerable<Sample> newSample, Sample lastSample)
    {
        var random = new Random();
        var prediction = random.Next(0, 2);

        if (prediction == 0)
            Buy(lastSample);
        else
            Sell();
    }

    public void Buy(Sample buySample)
    {
        /*
        if (isInTrade)
            Console.WriteLine($"Player {Name} continues in trade with profit {getActualProfit()}");
        else
            Console.WriteLine($"Player {Name} buys");
        */

        // Add the last sample to trade stack
        tradeStack.Push(buySample);
    }

    public void Sell()
    {
        // No trade was opened, so nothing happens
        if (!tradeStack.Any())
            return;

        var tradeProfit = getActualProfit();

        /*
        Console.WriteLine($"Player {Name} sold with profit {Math.Round(tradeProfit, 4)}");
        Console.WriteLine($"Player {Name} have total profit of {Profit}");
        */

        if (tradeProfit > 0)
            NumberOfProfitabletrades++;
        else if (tradeProfit < 0)
            NumberOfNonProfitableTrades++;

        // Add to total profit
        Profit += tradeProfit;

        // Clean the trade stack
        tradeStack.Clear();

    }

    private decimal getActualProfit()
    {
        var buySample = tradeStack.Last();
        var sellSample = tradeStack.First();

        var buyAmount = TRADE_AMOUNT_DOLARS / buySample.Open;
        var sellAmount = TRADE_AMOUNT_DOLARS / sellSample.Close;

        return (sellAmount * (1 - FEE)) - (buyAmount * (1 - FEE));
    }
}
