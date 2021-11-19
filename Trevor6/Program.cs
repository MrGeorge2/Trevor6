using System.Collections.Immutable;
using Trevor6.Binance.Client;
using Trevor6.Enums;
using Trevor6.ExchangeData;
using Trevor6.ExchangeData.DBModels;
using Trevor6.ExchangeData.Scraper;
using Trevor6.Learning;
using Trevor6.Learning.Abstract;
using Trevor6.Neat;



#if SCRAPING

await KlineScraper.ScrapeAllSymbols();

#endif
/*
var stockMarket = new StockMarket<BTCUSDT>();
var traders = new List<Trader>();

for (int i = 0; i < 500; i++)
    traders.Add(new Trader(i.ToString()));

stockMarket.StockMarketLoop(traders.ToArray());

*/
Neat.Main();