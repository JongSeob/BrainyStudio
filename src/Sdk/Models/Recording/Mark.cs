using Newtonsoft.Json;
using System;

namespace Sdk.Models
{
    [Serializable()]
    public class Mark
    {
        [JsonProperty("Timestamp")]
        public Double _timestamp;

        [JsonProperty("Caption")]
        public String _caption;

        public Mark(Double timestamp, String caption)
        {
            _timestamp = timestamp;
            _caption = caption;
        }
    }
}