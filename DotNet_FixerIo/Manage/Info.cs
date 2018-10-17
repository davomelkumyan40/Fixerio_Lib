using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FixerIo.Manage
{
   public class Info
    {
        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }
}
