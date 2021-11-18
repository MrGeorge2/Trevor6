using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trevor6.Learning.Abstract;

namespace Trevor6.ExchangeData.DBModels
{
    public abstract class OrderBase : IOrder
    {
        public OrderBase()
        {
            IsClosed = false;
            Profit = 0;
            OpenPrice = 0;
            ClosePrice = 0;

            Closed += handleClosed;
            Opened += handleOpened;
        }

        public EventHandler? Closed { get; set; }

        public EventHandler? Opened { get; set; }

        public bool IsClosed {get;}

        public decimal Profit { get; }

        public decimal OpenPrice { get; private set; }

        public decimal ClosePrice { get; private set; }

        public void Close(decimal price)
        {
            ClosePrice = price;
            Closed?.Invoke(this, EventArgs.Empty);
        }

        public void Place(decimal price)
        {
            OpenPrice = price;
            Opened?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void handleClosed(object? sender, EventArgs e)
        {

        }

        protected virtual void handleOpened(object? sender, EventArgs e)
        {

        }
    }
}
