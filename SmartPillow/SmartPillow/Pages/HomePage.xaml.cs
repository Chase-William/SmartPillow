using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.ViewModels;
using System.ComponentModel;
using Microcharts;
using Xamarin.Forms;
using SmartPillowLib;
using System;

namespace SmartPillow.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        public HomeViewModel VM => (HomeViewModel)BindingContext;
        public HomePage()
        {
            InitializeComponent();

            VM.OpenLoginPage += async delegate
            {
                await Navigation.PushModalAsync(new LoginPage());
            };

            VM.OpenProfilePage += async delegate
            {
                await Navigation.PushModalAsync(new ProfilePage());
            };
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        private void SKCanvasPopup_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        protected override void OnAppearing()
        {
            ViewExtensions.CancelAnimations(progf);
            progf.AnimateTo(0.75, 2000, Easing.BounceOut);
            IsEnabled = true;

            VM.OnAppearing();
            base.OnAppearing();

            rightIcon.IsEnabled = true;
        }

        protected override void OnDisappearing()
        {
            progf.Percentage = 0;
            IsEnabled = false;

            base.OnDisappearing();
            rightIcon.IsEnabled = false;
        }

        private async void ScanPillowPopup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var viewModel = (ContentView)sender;
            _ = viewModel.IsVisible ? await ScanPillowPopup.FadeTo(1, 250) : await ScanPillowPopup.FadeTo(0, 500);
        }
    }
}
