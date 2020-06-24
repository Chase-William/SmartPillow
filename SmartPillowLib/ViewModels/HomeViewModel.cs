using System;
using Xamarin.Forms;
using SmartPillowLib.Models;
using System.Windows.Input;
using Microcharts;
using System.Net.Http.Headers;

namespace SmartPillowLib.ViewModels
{
    public class HomeViewModel : NotifyClass
    {
        public event Action OpenLoginPage;

        public event Action OpenProfilePage;

        public RadialGaugeChart BackgroundGray
        {
            get => BackgroundGauge.Gray;
            set => BackgroundGauge.Gray = value;
        }

        private bool isScanPillowPopupVisible = false;

        public bool IsScanPillowPopupVisible
        {
            get => isScanPillowPopupVisible;
            set 
            {
                isScanPillowPopupVisible = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsUserLogged
        {
            get => UserInformation.IsUserLogged;
            set 
            { 
                UserInformation.IsUserLogged = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand UserCommand => new Command(() =>
        {
            if (IsUserLogged == false)
                OpenLoginPage?.Invoke();

            else
                OpenProfilePage?.Invoke();
        });

        public ICommand OpenScanPillowCommand => new Command(() => IsScanPillowPopupVisible = true);

        public ICommand CloseScanPillowCommand => new Command(() => IsScanPillowPopupVisible = false);

        public HomeViewModel()
        {
            User = (IsUserLogged == false) ? UserInformation.Guest : UserInformation.User;
        }

        public User User
        {
            get => UserInformation.User;
            set
            {
                UserInformation.User = value;
                NotifyPropertyChanged();
            }
        }

        public string ProfileImage
        {
            get => User.Image;
            set
            {
                User.Image = value;
                NotifyPropertyChanged();
            }
        }

        public void OnAppearing()
        {
            NotifyPropertiesChanged(nameof(IsUserLogged),
                                    nameof(User),
                                    nameof(IsScanPillowPopupVisible),
                                    nameof(ProfileImage));
        }
    }
}
