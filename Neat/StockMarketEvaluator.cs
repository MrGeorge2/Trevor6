using SharpNeat.Core;
using SharpNeat.Phenomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trevor6.ExchangeData.DBModels;
using Trevor6.Learning;
using Trevor6.Neat;

namespace Neat
{

    public class StockMarketEvaluator : IParallelEvaluator<IBlackBox>
    {
        private ulong _evalCount;

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
        public bool StopConditionSatisfied
        {
            get { return false; }
        }

        /// <summary>
        /// Evaluate the two black boxes by playing them against each other in a
        /// game of Tic-Tac-Toe. All permutations of size 2 are going to be
        /// evaluated, so we only play one game: box1 is X, box2 is O.
        /// </summary>
        public void Evaluate(ref BrainWithFitness<IBlackBox>[] brainsWithFitness)
        {
            var traders = new List<NeatTrader>();

            foreach(var brainWithFitness in brainsWithFitness)
            {
                traders.Add(new NeatTrader(brainWithFitness.Phenome));
            }

            evaluateOnCurrentyPair<BTCUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<ETHUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<BNBUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<ADAUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<XRPUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<DOTUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<DOGEUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<SHIBUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            evaluateOnCurrentyPair<LTCUSDT>(traders.ToArray());
            traders.ForEach(trader => trader.Reset());

            brainsWithFitness = getResult(traders.ToArray());

            _evalCount++;
        }

        private void evaluateOnCurrentyPair<TPair>(NeatTrader[] traders) where TPair : TrevorKline
        {
            var stockMarket = new StockMarket<TPair>();
            
            stockMarket.StockMarketLoop(traders);

            Console.WriteLine($"Currency = {typeof(TPair).Name} pair evalued");
        }

        private BrainWithFitness<IBlackBox>[] getResult(NeatTrader[] traders)
        {
            var newBrainsWIthFitness = new List<BrainWithFitness<IBlackBox>>();

            double maxFitness = 0;

            foreach (var trader in traders)
            {
                double fitness = 0;

                if (trader.Profit > 0)
                    fitness = (double)trader.Profit;

                if (fitness > maxFitness)
                    maxFitness = fitness;

                newBrainsWIthFitness.Add(new BrainWithFitness<IBlackBox>(trader._brain, new FitnessInfo(fitness, fitness)));
            }

            Console.WriteLine($"\n\nMaximal fitness = {maxFitness} \n\n");
            
            return newBrainsWIthFitness.ToArray();
        }

        /// <summary>
        /// Reset the internal state of the evaluation scheme if any exists.
        /// Note. The TicTacToe problem domain has no internal state. This method does nothing.
        /// </summary>
        public void Reset()
        {
        }
    }
}
