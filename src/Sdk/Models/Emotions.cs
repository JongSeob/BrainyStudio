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
        public List<Double> Timing = new List<double>();

        [JsonProperty("Excitement")]
        public List<Double> Excitement = new List<double>();

        [JsonProperty("Engagement")]
        public List<Double> Engagement = new List<double>();

        [JsonProperty("Meditation")]
        public List<Double> Meditation = new List<double>();

        [JsonProperty("Frustration")]
        public List<Double> Frustration = new List<double>();

        public Affectiv()
        {
        }
    }
}