using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FixerIo.Manage
{
    public class Converter : RateInfo
    {
        [JsonProperty("query")]
        public Query Query { get; set; }
        [JsonProperty("info")]
        public Info Info { get; set; }
        [JsonProperty("result")]
        public decimal Result { get; set; }
    }
}
