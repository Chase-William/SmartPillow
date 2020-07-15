using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.Models
{
    public class AccessToken
    {
        public int Id { get; set; }
        public string LoginWith { get; set; }
        public string AccessTokenValue { get; set; }
        public string Auth1Account { get; set; }
    }
}