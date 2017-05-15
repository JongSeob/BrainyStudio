using Newtonsoft.Json;
using System;

namespace Sdk.Models
{
    [Serializable()]
    public class Mark
    {
        [JsonProperty("Timestamp")]
        public double Timestamp;

        [JsonProperty("Caption")]
        public string Caption;

        public Mark(double timestamp, string caption)
        {
            Timestamp = timestamp;
            Caption = caption;
        }
    }
}