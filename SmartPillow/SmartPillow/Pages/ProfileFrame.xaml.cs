using SkiaSharp.Views.Forms;
using SmartPillow.Controls;
using SmartPillow.Util;
using SmartPillowLib;
using SmartPillowLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    public partial class ProfileFrame
    {
        public bool GoUp;
        public ProfileViewModel VM => (ProfileViewModel)BindingContext;

        public static event Action PopProfile;
        public ProfileFrame()
        {
            InitializeComponent();

            sizeFrame.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 2.75;
            sizeFrame.HeightRequest = DeviceDisplay.MainDisplayInfo.Height / 2;
            VisualBackground.HeightRequest = DeviceDisplay.MainDisplayInfo.Height / 3;

            VM.CloseFrame += async delegate
            {
                await this.TranslateTo(TranslationX, DeviceDisplay.MainDisplayInfo.Height / 2 - 170, 300);
                UserInformation.IsUserLogged = false;
                UserInformation.User = UserInformation.Guest;
                PopProfile?.Invoke();
            };

            PanGestureRecognizer = new PanGestureRecognizer
            {
                TouchPoints = 1
            };
            PanGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated;
            GestureRecognizers.Add(PanGestureRecognizer);
        }

        public PanGestureRecognizer PanGestureRecognizer { get; set; }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        private async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var screenCoordinates = this.GetScreenCoordinates();

            switch (e.StatusType)
            {
                // Move frame up or down
                case GestureStatus.Running:
                    if (TranslationY >= 220)
                        TranslationY = (Device.RuntimePlatform == Device.Android ? TranslationY : 0) + e.TotalY;

                    if (Math.Round((screenCoordinates.Y + TranslationY / 1) - 300, 1) > e.TotalY)
                        GoUp = false;

                    else
                        GoUp = true;

                    break;
                case GestureStatus.Completed:

                    if (GoUp)
                        await this.TranslateTo(TranslationX, 220, 200);

                    if (!GoUp)
                    {
                        await this.TranslateTo(TranslationX, DeviceDisplay.MainDisplayInfo.Height / 2 - 170, 300);
                        PopProfile?.Invoke();
                    }

                    break;

                case GestureStatus.Canceled:
                    break;
                case GestureStatus.Started:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}