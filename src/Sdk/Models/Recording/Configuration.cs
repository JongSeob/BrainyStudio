using Newtonsoft.Json;
using System;

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