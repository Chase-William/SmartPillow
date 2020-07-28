using SkiaSharp.Views.Forms;
using SmartPillow.Controls;
using SmartPillow.Util;
using SmartPillowLib.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartPillow.Pages
{
    public partial class ProfileFrame
    {
        public bool GoUp;

        public ProfileViewModel VM => (ProfileViewModel)BindingContext;

        public static event Action PopProfile;
        public static event Action<int, double> ChangeAlpha;
        public ProfileFrame()
        {
            InitializeComponent();

            sizeFrame.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 2.75;
            sizeFrame.HeightRequest = DeviceDisplay.MainDisplayInfo.Height / 2;
            VisualBackground.HeightRequest = DeviceDisplay.MainDisplayInfo.Height / 3;

            VM.CloseFrame += async delegate
            {
                await this.TranslateTo(TranslationX, DeviceDisplay.MainDisplayInfo.Height / 2 - 170, 300);
                PopProfile?.Invoke();
            };

            PanGestureRecognizer = new PanGestureRecognizer()
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
                    TranslationY = (Device.RuntimePlatform == Device.Android ? TranslationY : 0) + e.TotalY;

                    // it will be not able to move upwards when the translationY hits 220
                    if (219 > TranslationY + e.TotalY)
                        TranslationY = 220;
                    
                    if (TranslationY <= 500)
                        GoUp = true;

                    // if we are moving downwards (downwards is positive) then dont go up.
                    if (e.TotalY > 5)
                        GoUp = false;

                    // if we are moving upwards (upwards is negative) then go up.
                    if (e.TotalY < 5)
                        GoUp = true;

                    // if we are moving downwards (downwards is positive) then dont go up.
                    if (TranslationY > 500)
                        GoUp = false;

                    ChangingAlpha();
                    break;
                case GestureStatus.Completed:
                    // if the frame should go up tranlate it to go up
                    if (GoUp)
                    {
                        await this.TranslateTo(TranslationX, 220, 100);
                        ChangingAlpha();
                    }

                    // if the frame should go down translate it to go down
                    if (!GoUp)
                    {
                        await this.TranslateTo(TranslationX, DeviceDisplay.MainDisplayInfo.Height / 2 - 170, 100);
                        PopProfile?.Invoke();
                        ChangingAlpha();
                    }

                    break;
                case GestureStatus.Canceled:
                    break;
                case GestureStatus.Started:
                    break;
            }
        }

        /// <summary>
        ///     changes alpha to darker or more opacity outside the slide as the slide is being moved either upwards or downwards
        /// </summary>
        public void ChangingAlpha()
        {
            var translation = TranslationY - 220;
            var total = Math.Round(translation / 4.14);
            var alpha = (int)(128 - total);

            var translation2 = TranslationY - 400;
            //var scaleMeasure = 0.0035;
            var scaleMeasure = 0.005;
            var scale = translation2 * scaleMeasure;

            ChangeAlpha?.Invoke(alpha, scale);
        }
    }
}
