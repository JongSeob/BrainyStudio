using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Sdk.Models
{
    public class Comment
    {
        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("OwnerId")]
        public int OwnerId;

        [JsonProperty("RecordingId")]
        public int RecordingId;

        [JsonProperty("Text")]
        public string Text;

        [JsonProperty("Timestamp")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime Timestamp;

        public Comment()
        {
        }

        public Comment(int id, int ownerid, int recordingid, string text, DateTime timestamp)
        {
            this.Id = id;
            this.OwnerId = ownerid;
            this.RecordingId = recordingid;
            this.Text = text;
            this.Timestamp = timestamp;
        }
    }
}