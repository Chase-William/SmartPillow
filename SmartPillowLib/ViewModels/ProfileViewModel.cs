using SmartPillowLib.Data.Local;
using SmartPillowLib.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels
{
    public class ProfileViewModel : NotifyClass
    {
        public event Action CloseFrame;
        public static event Action CheckStatus;

        #region Properties
        public User User
        {
            get => UserInformation.User;
            set => UserInformation.User = value;
        }

        public string Image
        {
            get { return User.Image; }
            set
            {
                User.Image = value;
                NotifyPropertyChanged();
            }
        }

        public string Email
        {
            get { return User.Email; }
            set
            {
                User.Email = value;
                NotifyPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get { return User.PhoneNumber; }
            set
            {
                User.PhoneNumber = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get { return User.FirstName; }
            set
            {
                User.FirstName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand LogoutCommand => new Command(() =>
        {
            CloseFrame?.Invoke();
            LocalDataServiceContext.Provider.DeleteLoginAccessToken();
            UserInformation.IsUserLogged = false;
            User = UserInformation.Guest;
            CheckStatus?.Invoke();
        });
        #endregion

        public ProfileViewModel()
        {

        }
    }
}
