using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Models
{
    public class UserDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
