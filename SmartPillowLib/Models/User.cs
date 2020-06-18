using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.Models
{
    public class User
    {
        /// <summary>
        ///     Information of user that is currently logged in.
        /// </summary>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     ID of smart pillow device registered to user.
        /// </summary>
        public string SmartPillowDeviceID { get; set; }

    }
}
