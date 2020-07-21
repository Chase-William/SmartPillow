using SmartPillowAuthLib;
using SmartPillowLib.ViewModels;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace SmartPillowLib.Models
{
    public class User : IUser
    {
        /// <summary>
        ///     Information of user that is currently logged in.
        /// </summary>

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string PhoneNumber
        {
            get
            {
                return SecureStorage.GetAsync(nameof(PhoneNumber)).Result;
            }
            set
            {
                SecureStorage.SetAsync(nameof(PhoneNumber), value);
            }
        }

        /// <summary>
        ///     ID of smart pillow device registered to user.
        /// </summary>
        public string SmartPillowDeviceID { get; set; }

        public string DataUrl { get; set; }
        public List<History> UserData { get; set; }
    }
}
