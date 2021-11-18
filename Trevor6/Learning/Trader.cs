using System.Collections.Immutable;
using Trevor6.Learning.Abstract;


namespace Trevor6.Learning;


public class Trader : ITrader
{
    private readonly Stack<Sample> tradeStack = new();
    private bool isInTrade => tradeStack.Any();

    public Trader(string name)
    {
        IsEliminated = false;
        Profit = 0;
        Name = name;
    }
    public string Name { get; }
    public bool IsEliminated { get; private set; }

    public decimal Profit { get; private set; }

    public void AddNewSample(IEnumerable<Sample> newSample)
    {
        var lastSample = newSample.Last();

        if (isInTrade)
        {
            lastSample.Profit = getActualProfit();
        }

        think(newSample, lastSample);
    }

    private void think(IEnumerable<Sample> newSample, Sample lastSample)
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
        if (isInTrade)
            Console.WriteLine($"Player {Name} continues in trade with profit {buySample.Profit}");
        else
            Console.WriteLine($"Player {Name} buys");

        tradeStack.Push(buySample);
    }

    public void Sell()
    {
        if (!tradeStack.Any())
            return;

        var tradeProfit = getActualProfit();

        Console.WriteLine($"Player {Name} sold with profit {Math.Round(tradeProfit, 4)}");

        Profit += tradeProfit;

        tradeStack.Clear();

    }

    private decimal getActualProfit()
    {
        var buySample = tradeStack.Last();
        var sellSample = tradeStack.First();

        return sellSample.Close - buySample.Open;
    }
}
