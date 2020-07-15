using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.Models
{
    public class AccessToken
    {
        public int Id { get; set; } // Usually put "1"
        public string LoginWith { get; set; } // Which service is user logged in with? "google" : "facebook" : "native" : "twitter"
        public string AccessTokenValue { get; set; } // For Google & Facebook
        public string Auth1Account { get; set; } // It is for Twitter only
    }
}