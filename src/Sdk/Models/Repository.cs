using Newtonsoft.Json;
using System;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class Repository
    {
        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("Name")]
        public string Name;

        [JsonProperty("Description")]
        public string Description;

        [JsonProperty("Picture")]
        public string Picture;

        [JsonProperty("OwnerId")]
        public int OwnerId;

        public Repository()
        { }

        public Repository(int id, string name, string description, string picture, int ownerid)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Picture = picture;
            this.OwnerId = ownerid;
        }
    }
}