using FancyApollo.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trevor6.Abstract;
using Trevor6.Binance.Client;
using Trevor6.ExchangeData.DBModels;

namespace Trevor6.ExchangeData.Scraper
{
    public class KlineScraper
    {
        private readonly IDTOManager _dtoManager;

        /// <summary>
        /// Scraper for historical clines
        /// </summary>
        /// <param name="symbol"></param>
        public KlineScraper()
        {
            _dtoManager = DBClient.GetDBClient();
        }

        /// <summary>
        /// Scrape symbol to DB
        /// </summary>
        /// <param name="exchange"></param>
        /// <returns></returns>
        public async Task Scrape<TTrevorKline>(IExchangeClient exchange) where TTrevorKline : ITrevorKline
        {
            var klines = exchange.GetHistoricalClines<TTrevorKline>(typeof(TTrevorKline).Name, Enums.TrevorKlineInterval.FifteenMinutes, new DateTime(2017, 1, 1));

            await foreach (IDTObject kline in klines)
                await _dtoManager.SaveChangesAsync(kline);
        }
        
        /// <summary>
        /// Scrapes all defined symbols
        /// </summary>
        /// <returns></returns>
        public async static Task ScrapeAllSymbols()
        {
            static async Task Scrape<TCline>() where TCline : ITrevorKline
            {
                var scraper = new KlineScraper();
                await scraper.Scrape<TCline>(BinanceExchange.CreateClient());
            }

            var tasks = new List<Task>();

            tasks.Add(Scrape<BTCUSDT>());
            tasks.Add(Scrape<ETHUSDT>());
            tasks.Add(Scrape<BNBUSDT>());
            tasks.Add(Scrape<ADAUSDT>());
            tasks.Add(Scrape<XRPUSDT>());
            tasks.Add(Scrape<DOTUSDT>());
            tasks.Add(Scrape<DOGEUSDT>());
            tasks.Add(Scrape<SHIBUSDT>());
            tasks.Add(Scrape<LTCUSDT>());

            await Task.WhenAll(tasks);
        }
    }
}
