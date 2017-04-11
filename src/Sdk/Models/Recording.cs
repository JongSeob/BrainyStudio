using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Recording
    {
        [JsonProperty("Id")]
        private int Id;

        [JsonProperty("Name")]
        public string Name;

        [JsonProperty("Description")]
        public string Description;

        [JsonProperty("Frequency")]
        public int Timing;

        [JsonProperty("Date")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime Date;

        [JsonProperty("Subject")]
        public Subject subject = new Subject();
        
        [JsonProperty("Raw")]
        public Raw RawData = new Raw();

        [JsonProperty("Emotions")]
        public Affectiv emotions = new Affectiv();

        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="Id">ID of the new recording</param>
        /// <param name="Name">Name of the recording</param>
        public Recording(string Name, DateTime Date)
        {
            this.Name = Name;
            this.Date = Date;
        }

        /// <summary>
        /// Append new raw sensor values into recording
        /// </summary>
        public void AppendRaw(Double AF3, Double F7, Double F3, Double FC5, Double T7, Double P7, Double O1,
            Double O2, Double P8, Double T8, Double FC6, Double F4, Double F8, Double AF42)
        {
            RawData.AF3.Add(AF3);
            RawData.F7.Add(F7);
            RawData.F3.Add(F3);
            RawData.FC5.Add(FC5);
            RawData.T7.Add(T7);
            RawData.P7.Add(P7);
            RawData.O1.Add(O1);
            RawData.O2.Add(O2);
            RawData.P8.Add(P8);
            RawData.T8.Add(T8);
            RawData.FC6.Add(FC6);
            RawData.F4.Add(F4);
            RawData.F8.Add(F8);
            RawData.AF42.Add(AF42);
        }

        /// <summary>
        /// Append affectiv values
        /// </summary>
        public void AppendAffectiv(Double Timing, Double Excitement, Double Engagement, Double Meditation, Double Frustration )
        {
            emotions.Timing.Add(Timing);
            emotions.Excitement.Add(Excitement);
            emotions.Engagement.Add(Engagement);
            emotions.Meditation.Add(Meditation);
            emotions.Frustration.Add(Frustration);
        }
    }
}