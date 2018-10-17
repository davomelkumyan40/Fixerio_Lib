using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FixerIo.Manage
{
    public class Fluctuate : RateInfo
    {
        [JsonProperty("fluctuation")]
        public bool Fluctuation { get; set; }
        [JsonProperty("start_date")]
        public DateTime Start_date { get; set; }
        [JsonProperty("end_date")]
        public DateTime End_date { get; set; }
        [JsonProperty("rates")]
        public new Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }
    }
}
