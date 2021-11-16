using FancyApollo.DTO.Registrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trevor6.ExchangeData.DBModels
{
    internal class DBRegistrator : IDTORegistrator
    {
        const string KLINES_DB = "Klines";

        public void Register()
        {
            DTORegistrator.Register(typeof(BTCUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(ETHUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(BNBUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(ADAUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(XRPUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(DOTUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(DOGEUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(SHIBUSDT), KLINES_DB);
            DTORegistrator.Register(typeof(LTCUSDT), KLINES_DB);
        }
    }
}
