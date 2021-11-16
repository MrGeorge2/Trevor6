using Trevor6.Binance.Client;
using Trevor6.Enums;
using Trevor6.ExchangeData;

var client = new BinanceExchange(new ApiKeyLoader("E:\\Projects\\Trevor6\\ApiData.json"));
var klines = client.GetHistoricalClines("BTCUSDT", TrevorKlineInterval.FiveMinutes, new DateTime(2017, 1, 1));

await foreach (var kline in klines)
    Console.WriteLine(kline);