using Trevor6.Binance.Client;
using Trevor6.Enums;
using Trevor6.ExchangeData;
using Trevor6.ExchangeData.DBModels;
using Trevor6.ExchangeData.Scraper;



#if SCRAPING

await KlineScraper.ScrapeAllSymbols();

#endif
