using Newtonsoft.Json;
using System;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Subject
    {
        [JsonProperty("FirstName")]
        private string FirstName;

        [JsonProperty("FLastName")]
        private string LastName;

        [JsonProperty("Age")]
        private int Age;

        [JsonProperty("Gender")]
        private string Gender;

        [JsonProperty("Notes")]
        private string Notes;

        public Subject()
        {
        }

        public Subject(string FirstName, string LastName, int Age, string Gender, string Notes)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Age = Age;
            this.Gender = Gender;
            this.Notes = Notes;
        }
    }
}