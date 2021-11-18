using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trevor6.Learning.Abstract
{
    public interface IOrder
    {
        EventHandler? Closed { get; set; }
        EventHandler? Opened { get; set; }

        bool IsClosed { get; }
        
        decimal Profit { get; }

        decimal OpenPrice { get; }
        decimal ClosePrice { get; }

        void Place(decimal price);

        void Close(decimal price);
    }
}
