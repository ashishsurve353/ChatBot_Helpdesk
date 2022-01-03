using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.UserModel
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        public string Error { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

      
    }
}
