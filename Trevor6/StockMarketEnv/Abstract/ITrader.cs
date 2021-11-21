using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trevor6.Abstract;

namespace Trevor6.Learning.Abstract
{
    public interface ITrader
    {
        bool IsEliminated { get; }
        decimal Profit { get; }

        void AddNewSample(IEnumerable<Sample> kline, ITrevorKline latestKline);

        void Buy(ITrevorKline latestKline);

        void Sell();
    }
}
