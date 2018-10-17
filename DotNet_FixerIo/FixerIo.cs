using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DotNet_FixerIo.Manage;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace DotNet_FixerIo
{
    public class FixerIo : IFixerIo
    {
        //ctor
        public FixerIo(string api_Key)
        {
            Url = @"http://data.fixer.io/api/";
            this.Access_Key = api_Key;
            Endpoint = Endpoint.Latest;
            Date = DateTime.Now.ToString("yyyy-MM-dd");
            PrefixUrl = "?access_key=";
        }

        public string PrefixUrl { get; set; }
        public string Access_Key { get; set; }
        public string Date { get; set; }
        public string Url { get;  set; }
        public string EndpointMode { get; set; }

        public string Condition { get; set; }
        public string JsonString { get; set; }
        public Endpoint Endpoint { get; set; }
        public Error Error { get; set; }
        public RateInfo BaseRateInfo { get; set; }
        public string RequestUrl
        {
            get
            {
                switch (Endpoint)
                {
                    case Endpoint.Latest:
                        EndpointMode = "latest";
                        break;
                    case Endpoint.Date:
                        EndpointMode = Date;
                        break;
                    case Endpoint.Convert:
                        EndpointMode = "convert";
                        break;
                    case Endpoint.Timeseries:
                        EndpointMode = "timeseries";
                        break;
                    case Endpoint.Fluctuation:
                        EndpointMode = "fluctuation";
                        break;
                    default:
                        EndpointMode = "latest";
                        break;
                }
                return Url + EndpointMode + PrefixUrl + Access_Key + Condition;
            }
        }

        //public string Imitation() => "{\"success\":true,\"timeseries\":true,\"start_date\":\"2012-05-01\",\"end_date\":\"2012-05-03\",\"base\":\"EUR\",\"rates\":{\"2012-05-01\":{\"USD\":1.322891,\"AUD\":1.278047,\"CAD\":1.302303},\"2012-05-02\":{\"USD\":1.315066,\"AUD\":1.274202,\"CAD\":1.299083},\"2012-05-03\":{\"USD\":1.314491,\"AUD\":1.280135,\"CAD\":1.296868}}}";

        //Gets By any request
        private RateInfo Connect(bool online)
        {
            if (online)
            {
                using (HttpClient client = new HttpClient())
                {
                    JsonString = client.GetStringAsync(RequestUrl).Result;
                    BaseRateInfo = JsonConvert.DeserializeObject<RateInfo>(JsonString);
                    if (BaseRateInfo.Success)
                        return BaseRateInfo;
                    else
                       return Error = JsonConvert.DeserializeObject<Error>(JsonString);
                }
            }
            else
            {
                BaseRateInfo = JsonConvert.DeserializeObject<RateInfo>(JsonString);
                return BaseRateInfo;
            }
        }

        //working
        public RateInfo GetLatestRates()
        {
            this.Condition = string.Empty;
            Endpoint = Endpoint.Latest;
            return Connect(true);
        }

        //working
        public RateInfo GetRatesBy(string condition)
        {
            this.Condition = condition;
            return Connect(true);
        }

        //working
        public RateInfo GetRatesBy(string condition, DateTime date)
        {
            Endpoint = Endpoint.Date;
            this.Date = date.ToString("yyyy-MM-dd");
            this.Condition = condition;
            return Connect(true);
        }

        //working
        public RateInfo GetRatesBy(string baseCurency, params string[] symbols)
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
        public RateInfo GetRatesBy(string baseCurency, DateTime date, params string[] symbols)
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
        public RateInfo GetByDate(DateTime date)
        {
            this.Date = date.ToString("yyyy-MM-dd");
            this.Endpoint = Endpoint.Date;
            return Connect(true);
        }

        //working
        public RateInfo GetFromFile(string fileName)
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
        public RateInfo GetFromFile() => this.GetFromFile(@".\saveFile.json");

        //working
        public Converter Convert(string from, string to, decimal amount)
        {
            Endpoint = Endpoint.Convert;
            this.Condition = $"&form={from}&to={to}&amount={amount}";
            return Connect(true) as Converter;
        }

        //working
        public TimeSeries GetTimeSeries(DateTime start_date, DateTime end_date)
        {
            Endpoint = Endpoint.Timeseries;
            this.Condition = $"&start_date={start_date.ToString("yyyy-MM-dd")}&end_date={end_date.ToString("yyyy-MM-dd")}";
            return Connect(true) as TimeSeries;
        }

        //working
        public Fluctuate GetFluctuation(DateTime start_date, DateTime end_date)
        {
            Endpoint = Endpoint.Fluctuation;
            this.Condition = $"&start_date={start_date}&end_date={end_date}";
            return Connect(true) as Fluctuate;
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

    }
}
