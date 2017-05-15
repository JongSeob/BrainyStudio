using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Affectiv
    {
        [JsonProperty("Timing")]
        public List<double> Timing = new List<double>();

        [JsonProperty("Excitement")]
        public List<double> Excitement = new List<double>();

        [JsonProperty("Engagement")]
        public List<double> Engagement = new List<double>();

        [JsonProperty("Meditation")]
        public List<double> Meditation = new List<double>();

        [JsonProperty("Frustration")]
        public List<double> Frustration = new List<double>();

        public Affectiv()
        {
        }
    }
}