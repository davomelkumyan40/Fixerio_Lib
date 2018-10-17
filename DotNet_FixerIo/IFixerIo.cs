using DotNet_FixerIo.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FixerIo
{
    interface IFixerIo<T>
    {
        string PrefixUrl { get; set; }
        string Access_Key { get; set; }
        string Date { get; set; }
        string Url { get; set; }
        string EndpointMode { get; set; }
        string Condition { get; set; }
        string JsonString { get; set; }
        Endpoint Endpoint { get; set; }
        T Error { get; set; }
        T BaseRateInfo { get; set; }
        string RequestUrl { get; }

        T GetLatestRates();
        T GetRatesBy(string condition);
        T GetRatesBy(string condition, DateTime date);
        T GetRatesBy(string baseCurency, params string[] symbols);
        T GetRatesBy(string baseCurency, DateTime date, params string[] symbols);
        T GetByDate(DateTime date);
        T GetFromFile();
        T GetFromFile(string fileName);
        T Convert(string from, string to, decimal amount);
        T GetTimeSeries(DateTime start_date, DateTime end_date);
        T GetFluctuation(DateTime start_date, DateTime end_date);
        void SaveAs();
        void Update();
        bool DeleteJsonFile();
        bool DeleteJsonFile(string fileName);
    }
}
