using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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

        public Repository()
        { }

        public Repository(int Id, string Name, string Description, string Picture)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Picture = Picture;
        }
    }
}