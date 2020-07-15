using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowAuthLib
{
    public interface IUser
    {
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Image { get; set; }
        string Email { get; set; }
    }
}
