using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Client
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EEGRecording
    {
        #region BasicInfo

        [JsonProperty("Id")]
        private int Id;

        [JsonProperty("Name")]
        public string Name;

        [JsonProperty("Description")]
        public string Description;

        [JsonProperty]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime Date;

        #endregion BasicInfo

        #region Timestamp
        [JsonProperty]
        public List<TimeSpan> Timestamp = new List<TimeSpan>();
        #endregion Timestamp

        #region Subject Info

        [JsonProperty("Subject")]
        Dictionary<string, string> subject_dictionary = new Dictionary<string, string>();
        private string FirstName;
        private string LastName;
        private int Age;

        #endregion Subject Info

        #region Raw Data
        [JsonProperty("Raw")]
        Dictionary<string, List<double>> rawEEG_dictionary = new Dictionary<string, List<double>>();
        private List<Double> AF3 = new List<double>();
        private List<Double> F7 = new List<double>();
        private List<Double> F3 = new List<double>();
        private List<Double> FC5 = new List<double>();
        private List<Double> T7 = new List<double>();
        private List<Double> P7 = new List<double>();
        private List<Double> O1 = new List<double>();
        private List<Double> O2 = new List<double>();
        private List<Double> P8 = new List<double>();
        private List<Double> T8 = new List<double>();
        private List<Double> FC6 = new List<double>();
        private List<Double> F4 = new List<double>();
        private List<Double> F8 = new List<double>();
        private List<Double> AF42 = new List<double>();
        #endregion Raw Data

        #region Affectiv
        private List<Double> Excitement = new List<double>();
        private List<Double> Engagement = new List<double>();
        private List<Double> Meditation = new List<double>();
        private List<Double> Frustration = new List<double>();
        #endregion Affectiv

        #region Expressiv
        //TODO
        #endregion Expressiv

        #region Markers
        //TODO
        #endregion Markers


        /// <summary>
        /// Construct a new EEG Recording
        /// </summary>
        /// <param name="Id">ID of the new recording</param>
        /// <param name="Name">Name of the recording</param>
        public EEGRecording(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }



        /// <summary>
        /// Append global timestamp (in seconds)
        /// </summary>
        public void AppendTimestamp(TimeSpan timestamp)
        {
            this.Timestamp.Add(timestamp);
        }

        /// <summary>
        /// Append new raw sensor values into recording
        /// </summary>
        public void AppendRaw(Double AF3, Double F7, Double F3, Double FC5, Double T7, Double P7, Double O1,
            Double O2, Double P8, Double T8, Double FC6, Double F4, Double F8, Double AF42)
        {
            this.AF3.Add(AF3);
            this.F7.Add(F7);
            this.F3.Add(F3);
            this.FC5.Add(FC5);
            this.T7.Add(T7);
            this.P7.Add(P7);
            this.O1.Add(O1);
            this.O2.Add(O2);
            this.T8.Add(T8);
            this.FC6.Add(FC6);
            this.F4.Add(F4);
            this.F8.Add(F8);
            this.AF42.Add(AF42);
        }

        /// <summary>
        /// Append affectiv values
        /// </summary>
        public void AppendAffectiv(Double Excitement, Double Engagement, Double Meditation, Double Frustration )
        {
            this.Excitement.Add(Excitement);
            this.Engagement.Add(Engagement);
            this.Meditation.Add(Meditation);
            this.Frustration.Add(Frustration);
        }

        /// <summary>
        /// Finalize a EEG Recording before JSOn serializing.
        /// </summary>
        [OnSerializing]
        private void Finalize(StreamingContext context)
        {
            rawEEG_dictionary.Add("AF3", AF3);
            rawEEG_dictionary.Add("F7", F7);
            rawEEG_dictionary.Add("F3", F3);
            rawEEG_dictionary.Add("FC5", FC5);
            rawEEG_dictionary.Add("T7", T7);
            rawEEG_dictionary.Add("P7", P7);
            rawEEG_dictionary.Add("O1", O1);
            rawEEG_dictionary.Add("O2", O2);
            rawEEG_dictionary.Add("P8", P8);
            rawEEG_dictionary.Add("T8", T8);
            rawEEG_dictionary.Add("FC6", FC6);
            rawEEG_dictionary.Add("F4", F4);
            rawEEG_dictionary.Add("F8", F8);
            rawEEG_dictionary.Add("AF4", AF42);
        }

        /// <summary>
        /// Unpack a JSON on server side.
        /// </summary>
        [OnDeserialized]
        private void Unpack(StreamingContext context)
        {
        }
}
}