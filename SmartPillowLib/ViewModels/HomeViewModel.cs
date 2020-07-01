using System;
using Xamarin.Forms;
using SmartPillowLib.Models;
using System.Windows.Input;
using Microcharts;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using SmartPillow.Util;

namespace SmartPillowLib.ViewModels
{
    public class HomeViewModel : NotifyClass
    {
        public event Action OpenLoginPage;
        public event Action OpenProfilePage;
        private bool isScanPillowPopupVisible = false;
        private bool isDetailPopupVisible = false;
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

        private string quality; 
        public string Quality
        {
            get => SleepStatistic.Quality.ToString("f0") + "%";
            set 
            { 
                quality = value;
                NotifyPropertyChanged();
            }
        }

        public RadialGaugeChart QualityGauge
        {
            get => SleepStatistic.QualityGauge;
            set 
            {
                SleepStatistic.QualityGauge = value;
                NotifyPropertyChanged();
            }
        }

        private string awakeDuration;
        public string AwakeDuration
        {
            get 
            { 
                return SleepStatistic.AwakeDuration.Hours + "h " +
                    SleepStatistic.AwakeDuration.Minutes + "m"; 
            }
            set 
            { 
                awakeDuration = value;
                NotifyPropertyChanged();
            }
        }

        private string awake;
        public string AwakePercentage
        {
            get => SleepStatistic.AwakePercentage.ToString("f0") + "%";
            set 
            {
                awake = value;
                NotifyPropertyChanged();
            }
        }

        public RadialGaugeChart AwakeGauge
        {
            get => SleepStatistic.AwakeGauge;
            set
            {
                SleepStatistic.AwakeGauge = value;
                NotifyPropertyChanged();
            }
        }

        private string remDuration;
        public string RemDuration
        {
            get
            {
                return SleepStatistic.RemDuration.Hours + "h " +
                    SleepStatistic.RemDuration.Minutes + "m";
            }
            set
            {
                remDuration = value;
                NotifyPropertyChanged();
            }
        }

        private string rem;
        public string RemPercentage
        {
            get => SleepStatistic.RemPercentage.ToString("f0") + "%";
            set 
            {
                rem = value;
                NotifyPropertyChanged();
            }
        }

        public RadialGaugeChart RemGauge
        {
            get => SleepStatistic.RemGauge;
            set
            {
                SleepStatistic.RemGauge = value;
                NotifyPropertyChanged();
            }
        }

        private string sleepDuration;
        public string SleepDuration
        {
            get
            {
                return SleepStatistic.SleepDuration.Hours + "h " +
                    SleepStatistic.SleepDuration.Minutes + "m";
            }
            set
            {
                sleepDuration = value;
                NotifyPropertyChanged();
            }
        }

        private string sleep;
        public string SleepPercentage
        {
            get => SleepStatistic.SleepPercentage.ToString("f0") + "%";
            set 
            {
                sleep = value;
                NotifyPropertyChanged();
            }
        }

        public RadialGaugeChart SleepGauge
        {
            get => SleepStatistic.SleepGauge;
            set
            {
                SleepStatistic.SleepGauge = value;
                NotifyPropertyChanged();
            }
        }

        private string deepDuration;
        public string DeepDuration
        {
            get
            {
                return SleepStatistic.DeepDuration.Hours + "h " +
                    SleepStatistic.DeepDuration.Minutes + "m";
            }
            set
            {
                deepDuration = value;
                NotifyPropertyChanged();
            }
        }

        private string deep;
        public string DeepPercentage
        {
            get => SleepStatistic.DeepPercentage.ToString("f0") + "%";
            set 
            {
                deep = value;
                NotifyPropertyChanged();
            }
        }

        public RadialGaugeChart DeepGauge
        {
            get => SleepStatistic.DeepGauge;
            set
            {
                SleepStatistic.DeepGauge = value;
                NotifyPropertyChanged();
            }
        }

        public PointChart EventChart
        {
            get => SleepStatistic.EventChart;
            set { SleepStatistic.EventChart = value; }
        }

        public LineChart LineChart
        {
            get => SleepStatistic.LineChart;
            set { SleepStatistic.LineChart = value; }
        }

        public PointChart SnoreChart
        {
            get => SleepStatistic.SnoreChart;
            set { SleepStatistic.SnoreChart = value; }
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

        public bool IsDetailPopupVisible
        {
            get => isDetailPopupVisible;
            set 
            { 
                isDetailPopupVisible = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     It will either push async to LoginPage or slide up the 
        ///     profile overlay based on an user is logged in or as a guest
        /// </summary>
        public ICommand UserCommand => new Command(() =>
        {
            IsScanPillowPopupVisible = false;
            IsDetailPopupVisible = false;

            if (IsUserLogged == false)
                OpenLoginPage?.Invoke();

            else
                OpenProfilePage?.Invoke();
        });

        public ICommand OpenScanPillowCommand => new Command(() => IsScanPillowPopupVisible = true);

        public ICommand CloseScanPillowCommand => new Command(() => IsScanPillowPopupVisible = false);

        public ICommand OpenDetailCommand => new Command(() => IsDetailPopupVisible = true);

        private string brightness;

        /// <summary>
        ///     HomePage's brightness will be darker if local time is in between 12AM and 6AM
        /// </summary>
        public string Brightness
        {
            get => AutoBrightness.CheckNightTime();
            set 
            {
                brightness = value;
                NotifyPropertyChanged();
            }
        }


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
            CheckStatus();

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
