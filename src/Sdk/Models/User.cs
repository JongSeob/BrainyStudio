using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        [JsonProperty("Id")]
        public int _id;

        [JsonProperty("Nickname")]
        public string _Nickname;

        [JsonProperty("Name")]
        public string _Name;

        [JsonProperty("Avatar_URL")]
        public string _Avatar_URL;

        [JsonProperty("Notes")]
        public string _Notes;

        [JsonProperty("URL")]
        public string _Url;

        [JsonProperty("Role")]
        public string _Role ="User";

        private string _Password;

        public User(int id, string nickname)
        {
            _id = id;
            _Nickname = nickname;
        }

    }
}
