using SharpNeat.Phenomes;
using Trevor6.Learning;

namespace Trevor6.Neat;

public class NeatTrader : Trader
{
    public readonly IBlackBox _brain;

    public NeatTrader(IBlackBox brain) : base("NoName")
    {
        _brain = brain;
    }

    protected override void Think(IEnumerable<Sample> newSample, Sample lastSample)
    {
        _brain.ResetState();

        setInputFromState(_brain.InputSignalArray, newSample);

        _brain.Activate();

        double buyScore = _brain.OutputSignalArray[0];
        double sellScore = _brain.OutputSignalArray[1];

        if (buyScore > sellScore)
            Buy(lastSample);
        else
            Sell();
    }

    private void setInputFromState(ISignalArray inputArray, IEnumerable<Sample> sample)
    {
        int sampleCounter = 0;
        foreach(var sampleItem in sample)
        {
            foreach(var value in sampleItem)
            {
                inputArray[sampleCounter] = value;
                sampleCounter++;
            }
        }
    }
}
