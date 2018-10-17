using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNet_FixerIo.Manage
{ 
    public class Error : RateInfo
    {
        [JsonProperty("code")]
        public int Code { get; private set; }
        [JsonProperty("info")]
        public string Info { get; private set; }
    }
}
