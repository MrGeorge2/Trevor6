using System.Collections.Immutable;
using Trevor6.Binance.Client;
using Trevor6.Enums;
using Trevor6.ExchangeData;
using Trevor6.ExchangeData.DBModels;
using Trevor6.ExchangeData.Scraper;
using Trevor6.Learning;
using Trevor6.Learning.Abstract;



#if SCRAPING

await KlineScraper.ScrapeAllSymbols();

#endif