using DotNet_FixerIo.Manage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FixerIo
{
    public abstract class BaseRest<T> : IFixerIo<T>
    {
        public BaseRest()
        {
            PrefixUrl = "?access_key=";
        }

        public abstract string RequestUrl { get; }
        public string PrefixUrl { get; set; }
        public string Access_Key { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
        public string EndpointMode { get; set; }

        public string Condition { get; set; }
        public string JsonString { get; set; }
        public Endpoint Endpoint { get; set; }
        public T Error { get; set; }
        public T BaseRateInfo { get; set; }

        private T Connect(bool online)
        {
            if (online)
                return OnConnect();
            else
                BaseRateInfo = OnConnectError();
                return BaseRateInfo;
        }

        public abstract T OnConnect();
        public abstract T OnConnectError();

        //working
        public T GetLatestRates()
        {
            this.Condition = string.Empty;
            Endpoint = Endpoint.Latest;
            return Connect(true);
        }

        //working
        public T GetRatesBy(string condition)
        {
            this.Condition = condition;
            return Connect(true);
        }

        //working
        public T GetRatesBy(string condition, DateTime date)
        {
            Endpoint = Endpoint.Date;
            this.Date = date.ToString("yyyy-MM-dd");
            this.Condition = condition;
            return Connect(true);
        }

        //working
        public T GetRatesBy(string baseCurency, params string[] symbols)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("&").Append($"base={baseCurency}").Append("&symbols=");
            foreach (var item in symbols)
            {
                builder.Append($"{item}, ");
            }
            Condition = builder.ToString();
            return Connect(true);
        }

        //working
        public T GetRatesBy(string baseCurency, DateTime date, params string[] symbols)
        {
            Endpoint = Endpoint.Date;
            this.Date = date.ToString("yyyy-MM-dd");
            StringBuilder builder = new StringBuilder();
            builder.Append("&").Append($"base={baseCurency}").Append("&symbols=");
            foreach (var item in symbols)
                builder.Append($"{item}, ");
            Condition = builder.ToString();
            return Connect(true);
        }

        //working
        public T GetByDate(DateTime date)
        {
            this.Date = date.ToString("yyyy-MM-dd");
            this.Endpoint = Endpoint.Date;
            return Connect(true);
        }

        //working
        public T GetFromFile(string fileName)
        {
            StringBuilder builder = new StringBuilder();
            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                        builder.Append(reader.ReadLine());
                }
            }
            JsonString = builder.ToString();
            return Connect(false);
        }

        //working
        public T GetFromFile() => this.GetFromFile(@".\saveFile.json");

        //working
        public T Convert(string from, string to, decimal amount)
        {
            Endpoint = Endpoint.Convert;
            this.Condition = $"&form={from}&to={to}&amount={amount}";
            return OnConverter(Connect(true));
        }

        //working
        public T GetTimeSeries(DateTime start_date, DateTime end_date)
        {
            Endpoint = Endpoint.Timeseries;
            this.Condition = $"&start_date={start_date.ToString("yyyy-MM-dd")}&end_date={end_date.ToString("yyyy-MM-dd")}";
            return OnGetTimeSeries(Connect(true));
        }

        //working
        public T GetFluctuation(DateTime start_date, DateTime end_date)
        {
            Endpoint = Endpoint.Fluctuation;
            this.Condition = $"&start_date={start_date}&end_date={end_date}";
            return OnGetFluctuate(Connect(true));
        }

        //working
        public void SaveAs()
        {
            using (FileStream stream = new FileStream(@".\saveFile.json", FileMode.OpenOrCreate))
            using (StreamWriter writer = new StreamWriter(stream))
                writer.WriteLine(JsonString);
        }

        //working
        public void Update()
        {
            Connect(true);
            SaveAs();
        }

        //working
        public bool DeleteJsonFile()
        {
            FileInfo file = new FileInfo(@".\saveFile.json");
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            return false;
        }

        //working
        public bool DeleteJsonFile(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            return false;
        }


        public abstract T OnGetTimeSeries(T source);
        public abstract T OnGetFluctuate(T source);
        public abstract T OnConverter(T source);

    }
}
