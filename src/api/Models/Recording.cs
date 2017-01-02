using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace api.Models
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
        /// Unpack a JSON on server side.
        /// </summary>
        [OnDeserialized]
        public void Unpack(StreamingContext context)
        {
            foreach (KeyValuePair<string, List<double>> entry in rawEEG_dictionary)
            {
                if (entry.Key == "AF3") AF3 = entry.Value.ToList();
                if (entry.Key == "F7") F7 = entry.Value.ToList();
                if (entry.Key == "F3") F3 = entry.Value.ToList();
                if (entry.Key == "FC5") FC5 = entry.Value.ToList();
                if (entry.Key == "T7") T7 = entry.Value.ToList();
                if (entry.Key == "P7") P7 = entry.Value.ToList();
                if (entry.Key == "O1") O1 = entry.Value.ToList();
                if (entry.Key == "O2") O2 = entry.Value.ToList();
                if (entry.Key == "P8") P8 = entry.Value.ToList();
                if (entry.Key == "T8") T8 = entry.Value.ToList();
                if (entry.Key == "FC6") FC6 = entry.Value.ToList();
                if (entry.Key == "F4") F4 = entry.Value.ToList();
                if (entry.Key == "F8") F8 = entry.Value.ToList();
                if (entry.Key == "AF4") AF42 = entry.Value.ToList();
            }

        }
}
}
