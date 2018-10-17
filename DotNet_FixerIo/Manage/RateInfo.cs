using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNet_FixerIo.Manage
{
    public class RateInfo
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }
        [JsonProperty("base")]
        public string Base;
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
