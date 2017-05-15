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
        public int Id;

        [JsonProperty("Nickname")]
        public string Nickname;

        [JsonProperty("Name")]
        public string Name;

        [JsonProperty("Avatar_URL")]
        public string AvatarUrl;

        [JsonProperty("Notes")]
        public string Notes;

        [JsonProperty("URL")]
        public string Url;

        [JsonProperty("Role")]
        public string Role;

        //private string Password;


        public User()
        {

        }

        public User(int id, string nickname, string name, string avatarurl, string notes, string url, string role)
        {
            this.Id = id;
            this.Nickname = nickname;
            this.Name = name;
            this.AvatarUrl = avatarurl;
            this.Notes = notes;
            this.Url = url;
            this.Role = role;
        }

    }
}
