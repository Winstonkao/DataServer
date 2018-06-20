using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

namespace DataRetriever
{
    public static class CompanyHandler
    {
        public static void GenerateNewCompanyData(string symbol)
        {
            string baseUrl = "https://api.iextrading.com/1.0/stock/";
            string url = baseUrl + symbol + "/chart/5y";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseMessage = response.Content.ReadAsStringAsync().Result;
                    List<DailyStockQuote> stockQuotes = JsonConvert.DeserializeObject<List<DailyStockQuote>>(responseMessage);

                    foreach (DailyStockQuote quote in stockQuotes)
                    {
                        quote.symbol = symbol;
                        Console.WriteLine(quote.date.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Could not retrieve data:" + response.ToString());
                }
            }


        }
    }
}
