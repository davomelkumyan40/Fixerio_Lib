using DotNet_FixerIo.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FixerIo
{
    interface IFixerIo
    {
        string PrefixUrl { get; set; }
        string Access_Key { get; set; }
        string Date { get; set; }
        string Url { get; set; }
        string EndpointMode { get; set; }
        string Condition { get; set; }
        string JsonString { get; set; }
        Endpoint Endpoint { get; set; }
        Error Error { get; set; }
        RateInfo BaseRateInfo { get; set; }
        string RequestUrl { get; }

        RateInfo GetLatestRates();
        RateInfo GetRatesBy(string condition);
        RateInfo GetRatesBy(string condition, DateTime date);
        RateInfo GetRatesBy(string baseCurency, params string[] symbols);
        RateInfo GetRatesBy(string baseCurency, DateTime date, params string[] symbols);
        RateInfo GetByDate(DateTime date);
        RateInfo GetFromFile();
        RateInfo GetFromFile(string fileName);
        Converter Convert(string from, string to, decimal amount);
        TimeSeries GetTimeSeries(DateTime start_date, DateTime end_date);
        Fluctuate GetFluctuation(DateTime start_date, DateTime end_date);
        void SaveAs();
        void Update();
        bool DeleteJsonFile();
        bool DeleteJsonFile(string fileName);
    }
}
