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
    public class FixerIo : BaseRest<RateInfo>
    {
        //ctor
        public FixerIo(string api_Key)
        {
            Url = @"http://data.fixer.io/api/";
            this.Access_Key = api_Key;
            Endpoint = Endpoint.Latest;
            Date = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public override RateInfo OnConnect()
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

        public override string RequestUrl
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

        public override RateInfo OnGetFluctuate(RateInfo source)
        {
            return source as Fluctuate;
        }

        public override RateInfo OnGetTimeSeries(RateInfo source)
        {
            return source as TimeSeries;
        }

        public override RateInfo OnConnectError()
        {
           return JsonConvert.DeserializeObject<Error>(JsonString);
        }

        public override RateInfo OnConverter(RateInfo source)
        {
            return source as Converter;
        }

        //public string Imitation() => "{\"success\":true,\"timeseries\":true,\"start_date\":\"2012-05-01\",\"end_date\":\"2012-05-03\",\"base\":\"EUR\",\"rates\":{\"2012-05-01\":{\"USD\":1.322891,\"AUD\":1.278047,\"CAD\":1.302303},\"2012-05-02\":{\"USD\":1.315066,\"AUD\":1.274202,\"CAD\":1.299083},\"2012-05-03\":{\"USD\":1.314491,\"AUD\":1.280135,\"CAD\":1.296868}}}";

        //Gets By any request


    }
}
