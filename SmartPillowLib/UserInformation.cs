﻿using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib
{
    public class UserInformation
    {
        public static bool IsUserLogged { get; set; } = false;
        public static User User { get; set; }
    }
}
