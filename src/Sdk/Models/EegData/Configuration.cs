using Newtonsoft.Json;
using System;

namespace Sdk.Models
{
    [Serializable()]
    public class Configuration
    {
        [JsonProperty("RawFrequency")]
        public int Frequency;

        [JsonProperty("EmoFrequency")]
        public int Snapshots;

        [JsonProperty("Hardware")]
        public string Hardware;

        [JsonProperty("Software")]
        public string Software;
    }
}