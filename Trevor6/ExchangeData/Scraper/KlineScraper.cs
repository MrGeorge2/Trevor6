using Trevor6.Abstract;
using Trevor6.Binance.Client;
using Trevor6.ExchangeData.DBModels;

namespace Trevor6.ExchangeData.Scraper
{
    public class KlineScraper
    {
        /// <summary>
        /// Scrape symbol to DB
        /// </summary>
        /// <param name="exchange"></param>
        /// <returns></returns>
        public async Task Scrape<TTrevorKline>(IExchangeClient exchange) where TTrevorKline : TrevorKline
        {
            var collection = DBClient.GetKlineCollection<TTrevorKline>();

            var klines = exchange.GetHistoricalClines<TTrevorKline>(typeof(TTrevorKline).Name, Enums.TrevorKlineInterval.FifteenMinutes, new DateTime(2017, 1, 1));

            await foreach (TTrevorKline kline in klines)
                await collection.InsertOneAsync(kline);
        }
        
        /// <summary>
        /// Scrapes all defined symbols
        /// </summary>
        /// <returns></returns>
        public async static Task ScrapeAllSymbols()
        {
            static async Task Scrape<TCline>() where TCline : TrevorKline
            {
                var scraper = new KlineScraper();
                await scraper.Scrape<TCline>(BinanceExchange.CreateClient());
            }

            var tasks = new List<Task>
            {
                Scrape<BTCUSDT>(),
                Scrape<ETHUSDT>(),
                Scrape<BNBUSDT>(),
                Scrape<ADAUSDT>(),
                Scrape<XRPUSDT>(),
                Scrape<DOTUSDT>(),
                Scrape<DOGEUSDT>(),
                Scrape<SHIBUSDT>(),
                Scrape<LTCUSDT>()
            };
           
            await Task.WhenAll(tasks);
        }
    }
}
