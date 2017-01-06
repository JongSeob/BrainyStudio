using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Affectiv
    {
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
