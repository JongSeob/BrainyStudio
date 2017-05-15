using Newtonsoft.Json;
using System;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Subject
    {
        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("Name")]
        public string Name;

        [JsonProperty("Gender")]
        public string Gender;

        [JsonProperty("Age")]
        public int Age;

        [JsonProperty("Description")]
        public string Description;

        [JsonProperty("OwnerId")]
        public int OwnerId;

        public Subject()
        {

        }

        public Subject(int id, string name,  int age, string gender, string description, int ownerid)
        {
            this.Id = id;
            this.Name = name;
            this.Gender = gender;
            this.Age = age;
            this.Description = description;
            this.OwnerId = ownerid;
        }
    }
}