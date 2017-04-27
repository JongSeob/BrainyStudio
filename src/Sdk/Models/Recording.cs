using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Recording
    {
        //Recording meta data
        [JsonProperty("Name")]
        public string _name;

        [JsonProperty("Description")]
        public string _description;

        [JsonProperty("Length")]
        public Double _length = new Double();

        [JsonProperty("Date")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime _date;

        //Recording Enviroment Configuration
        [JsonProperty("Configuration")]
        public Configuration _configuration = new Configuration();

        //Recorded Subject Information
        [JsonProperty("Subject")]
        public Subject _subject = new Subject();

        //Full Data
        [JsonProperty("Raw")]
        public Raw _raw = new Raw();

        [JsonProperty("Emotions")]
        public Affectiv _emotions = new Affectiv();

        [JsonProperty("Expressions")]
        public Expressions _expressions = new Expressions();

        [JsonProperty("Markers")]
        public List<Mark> _markers = new List<Mark>();

        public Recording()
        { }

        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="Id">ID of the new recording</param>
        /// <param name="Name">Name of the recording</param>
        public Recording(string Name, DateTime Date)
        {
            _name = Name;
            _date = Date;
        }

        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="Id">ID of the new recording</param>
        /// <param name="Name">Name of the recording</param>
        /// <param name="Description">Notes about the recording</param>
        public Recording(string Name, string Description, DateTime Date)
        {
            _name = Name;
            _description = Description;
            _date = Date;
        }

        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="Id">ID of the new recording</param>
        /// <param name="Name">Name of the recording</param>
        /// <param name="Description">Notes about the recording</param>
        public void AppendConfig(int rawFrequency, int snapshots, string hardware, string software)
        {
            _configuration._frequency = rawFrequency;
            _configuration._snapshots = snapshots;
            _configuration._hardware = hardware;
            _configuration._software = software;
        }

        /// <summary>
        /// Append a new Marker
        /// </summary>
        /// <param name="Caption">Contains of the mark</param>
        /// <param name="Time">Timestamp in the recording</param>
        public void AppendMark(string Caption, Double Time)
        {
            _markers.Add(new Mark(Time, Caption));
        }

        /// <summary>
        /// Append new raw sensor values into recording
        /// </summary>
        public void AppendRawData(Double AF3, Double F7, Double F3, Double FC5, Double T7, Double P7, Double O1,
            Double O2, Double P8, Double T8, Double FC6, Double F4, Double F8, Double AF42)
        {
            _raw.AF3.Add(AF3);
            _raw.F7.Add(F7);
            _raw.F3.Add(F3);
            _raw.FC5.Add(FC5);
            _raw.T7.Add(T7);
            _raw.P7.Add(P7);
            _raw.O1.Add(O1);
            _raw.O2.Add(O2);
            _raw.P8.Add(P8);
            _raw.T8.Add(T8);
            _raw.FC6.Add(FC6);
            _raw.F4.Add(F4);
            _raw.F8.Add(F8);
            _raw.AF42.Add(AF42);
        }

        /// <summary>
        /// Append affectiv values
        /// </summary>
        public void AppendAffectivData(Double Timing, Double Excitement, Double Engagement, Double Meditation,
            Double Frustration)
        {
            _emotions.Timing.Add(Timing);
            _emotions.Excitement.Add(Excitement);
            _emotions.Engagement.Add(Engagement);
            _emotions.Meditation.Add(Meditation);
            _emotions.Frustration.Add(Frustration);
        }
    }
}