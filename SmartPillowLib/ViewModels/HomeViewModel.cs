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
        private bool isScanPillowPopupVisible = false;
        private string status;
        private Color pillowStatusColor;

        // User's sleep statistics 
        #region
        /// <summary>
        ///     To display the gray circle beside the blue for all radial gauge charts 
        /// </summary>
        public RadialGaugeChart BackgroundGray
        {
            get => BackgroundGauge.Gray;
            set => BackgroundGauge.Gray = value;
        }
        #endregion

        // Pillow information displaying in the top black frame
        #region
        /// <summary>
        ///     Smart pillow's ID whose is registered with an user
        /// </summary>
        public string PillowID
        {
            get => User.SmartPillowDeviceID;
            set
            {
                User.SmartPillowDeviceID = value;

                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     "Connected" or "Disconnected" status based on IsConnected
        /// </summary>
        public string Status
        {
            get => status;
            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Smart pillow is either connected or disconnected to this device
        /// </summary>
        public bool IsConnected
        {
            get => UserInformation.IsConnected;
            set
            {
                UserInformation.IsConnected = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     TextColor for status of pillow is either connected or disconnected (green/red)
        /// </summary>
        public Color PillowStatusColor
        {
            get => pillowStatusColor;
            set
            {
                pillowStatusColor = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Profile information
        #region
        public User User
        {
            get => UserInformation.User;
            set
            {
                UserInformation.User = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     User's Image for displaying it in the profile overlay
        /// </summary>
        public string ProfileImage
        {
            get => User.Image;
            set
            {
                User.Image = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Either an user uses device as a guest or its own account is logged
        /// </summary>
        public bool IsUserLogged
        {
            get => UserInformation.IsUserLogged;
            set
            {
                UserInformation.IsUserLogged = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        ///     Set the scan pillow popup to either hide or show
        /// </summary>
        public bool IsScanPillowPopupVisible
        {
            get => isScanPillowPopupVisible;
            set 
            {
                isScanPillowPopupVisible = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     It will either push async to LoginPage or slide up the 
        ///     profile overlay based on an user is logged in or as a guest
        /// </summary>
        public ICommand UserCommand => new Command(() =>
        {
            if (IsUserLogged == false)
                OpenLoginPage?.Invoke();

            else
                OpenProfilePage?.Invoke();
        });

        public ICommand OpenScanPillowCommand => new Command(() => IsScanPillowPopupVisible = true);

        public ICommand CloseScanPillowCommand => new Command(() => IsScanPillowPopupVisible = false);

        /// <summary>
        ///     Check if an user logged in or as a guest when HomePage is being load
        /// </summary>
        public HomeViewModel()
        {
            User = (IsUserLogged == false) ? UserInformation.Guest : UserInformation.User;

            ProfileViewModel.CheckStatus += delegate { CheckStatus(); };
            LoginViewModel.CheckStatus += delegate { CheckStatus(); };
        }

        public void OnAppearing()
        {
            NotifyPropertiesChanged(nameof(IsUserLogged),
                                    nameof(User),
                                    nameof(IsScanPillowPopupVisible),
                                    nameof(ProfileImage),
                                    nameof(PillowID),
                                    nameof(Status),
                                    nameof(IsConnected),
                                    nameof(PillowStatusColor));
        }

        public void CheckStatus()
        {
            Status = (IsConnected) ? "Connected" : "Disconnected";
            if (PillowID == "No Pillow")
                Status = "";
            PillowStatusColor = (IsConnected) ? Color.FromHex("#53FF6F") : Color.FromHex("#FF5353");
        }
    }
}
