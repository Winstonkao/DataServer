using System;
using System.Collections.Generic;
using System.Text;

namespace DataRetriever
{
    class DailyStockQuote
    {
        public string symbol { get; set; }

        public DateTime date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
        public double unadjustedClose { get; set; }
        public double unadjustedVolume { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public double vwap { get; set; }
        public double changeOverTime { get; set; }
    }
}
