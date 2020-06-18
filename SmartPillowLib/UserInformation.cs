using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib
{
    public class UserInformation
    {
        public static bool IsUserLogged { get; set; }
        public User User { get; set; }
        public UserInformation()
        {
            //testing user
            var user = new User()
            {
                FirstName = "Mark",
                LastName = "Zuckerberg",
                Image = "Zack.png",
                Email = "Email@gmail.com",
                PhoneNumber = "585-585-5858"
            };

            User = user;
        }
    }
}
