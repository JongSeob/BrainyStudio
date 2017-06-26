using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Recording
    {
        /// <summary>
        /// META DATA ONLY
        /// </summary>

        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("RepositoryId")]
        public int RepositoryId;

        [JsonProperty("OwnerId")]
        public int OwnerId;

        [JsonProperty("Name")]
        public string Name;

        [JsonProperty("Description")]
        public string Description;

        [JsonProperty("Date")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime Date;

        [JsonProperty("SubjectId")]
        public int SubjectId;

        /// <summary>
        /// FULL RECORDING
        /// </summary>

        // (Optional) Complete Subject Data
        [JsonProperty("Subject")]
        public Subject Subject = new Subject();

        // (Optional) Complete EEG Data
        [JsonProperty("Data")]
        public EegData Data = new EegData();

        public Recording()
        { }

        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="name">Name of the recording</param>
        /// <param name="date"></param>
        public Recording(string name, DateTime date)
        {
            this.Name = name;
            this.Date = date;
        }

        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="name">Name of the recording</param>
        /// <param name="description">Notes about the recording</param>
        /// <param name="date"></param>
        public Recording(string name, string description, DateTime date)
        {
            this.Name = name;
            this.Description = description;
            this.Date = date;
        }

        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="rawFrequency"></param>
        /// <param name="snapshots"></param>
        /// <param name="hardware"></param>
        /// <param name="software"></param>
        public void AppendConfig(int rawFrequency, int snapshots, string hardware, string software)
        {
            Data.Configuration.Frequency = rawFrequency;
            Data.Configuration.Snapshots = snapshots;
            Data.Configuration.Hardware = hardware;
            Data.Configuration.Software = software;
        }

        /// <summary>
        /// Append a new Marker
        /// </summary>
        /// <param name="caption">Contains of the mark</param>
        /// <param name="time">Timestamp in the recording</param>
        public void AppendMark(string caption, double time)
        {
            Data.Markers.Add(new Mark(time, caption));
        }

        /// <summary>
        /// Append new raw sensor values into recording
        /// </summary>
        public void AppendRawData(double AF3, double F7, double F3, double FC5, double T7, double P7, double O1,
            double O2, double P8, double T8, double FC6, double F4, double F8, double AF4)
        {
            Data.Raw.AF3.Add(AF3);
            Data.Raw.F7.Add(F7);
            Data.Raw.F3.Add(F3);
            Data.Raw.FC5.Add(FC5);
            Data.Raw.T7.Add(T7);
            Data.Raw.P7.Add(P7);
            Data.Raw.O1.Add(O1);
            Data.Raw.O2.Add(O2);
            Data.Raw.P8.Add(P8);
            Data.Raw.T8.Add(T8);
            Data.Raw.FC6.Add(FC6);
            Data.Raw.F4.Add(F4);
            Data.Raw.F8.Add(F8);
            Data.Raw.AF4.Add(AF4);
        }

        /// <summary>
        /// Append affectiv values
        /// </summary>
        public void AppendAffectivData(double timing, double excitement, double engagement, double meditation,
            double frustration)
        {
            Data.Emotions.Timing.Add(timing);
            Data.Emotions.Excitement.Add(excitement);
            Data.Emotions.Engagement.Add(engagement);
            Data.Emotions.Meditation.Add(meditation);
            Data.Emotions.Frustration.Add(frustration);
        }
    }
}