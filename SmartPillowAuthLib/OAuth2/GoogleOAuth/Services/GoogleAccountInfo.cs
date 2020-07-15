using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowAuthLib.OAuth2.GoogleOAuth.Services
{   
    public class GoogleAccountInfo : IUser
    {       
        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }

        [JsonProperty("hd")]
        public string Hd { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("sub")]
        public string Id { get; set; }

        [JsonProperty("picture")]
        public string Image { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }   
    }
}
