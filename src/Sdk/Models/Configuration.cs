using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Models
{
    [Serializable()]
    public class Configuration
    {
        [JsonProperty("RawFrequency")]
        public int _frequency;

        [JsonProperty("EmoFrequency")]
        public int _snapshots;

        [JsonProperty("Hardware")]
        public string _hardware;

        [JsonProperty("Software")]
        public string _software;
    }
}
