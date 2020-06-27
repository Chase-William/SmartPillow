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
        public RadialGaugeChart gaugeOne;
        public RadialGaugeChart gaugeTwo;
        public LineChart lineChart;
        public Microcharts.Entry[] data;
        public HomePage()
        {
            InitializeComponent();

            //testing purpose
            lineChart = new LineChart()
            {
                Entries = new[]
                {
                    // data for line chart
                    new Microcharts.Entry((float).40)
                    {
                        // light pink
                        Color = SKColor.Parse("#D06BFC"),
                    },
                    new Microcharts.Entry((float)1)
                    {
                        // light pink
                        Color = SKColor.Parse("#BC7FF5"),
                    },
                    new Microcharts.Entry((float).25)
                    {
                        // light blue color 
                        Color = SKColor.Parse("#A794EE")
                    },
                    new Microcharts.Entry((float).65)
                    {
                        // light blue color 
                        Color = SKColor.Parse("#92A9E7")
                    },
                    new Microcharts.Entry((float).35)
                    {
                        // light blue color 
                        Color = SKColor.Parse("#7AC0DF")
                    },
                },
                BackgroundColor = SKColors.Transparent,
                LineAreaAlpha = 15,
                MaxValue = 1,
                PointAreaAlpha = 255,
                LineMode = LineMode.Spline,
                PointMode = PointMode.Circle,
            };

            //testing purpose
            var entries = new[]
                {
                    // data for radial gauge chart
                    new Microcharts.Entry((float)0)
                    {
                        // light pink
                        Color = SKColor.Parse("#7AC0DF")
                    },
                    new Microcharts.Entry((float).80)
                    {
                        // light pink
                        Color = SKColor.Parse("#92A9E7")
                    },
                    new Microcharts.Entry((float).45)
                    {
                        // light blue color 
                        Color = SKColor.Parse("#A794EE")
                    },
                    new Microcharts.Entry((float)1)
                    {
                        // light blue color 
                        Color = SKColor.Parse("#BC7FF5")
                    },
                    new Microcharts.Entry((float).35)
                    {
                        // light blue color 
                        Color = SKColor.Parse("#D06BFC")
                    },
                };
            data = entries;

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

            //testing purpose
            dotChart.Chart = new PointChart() { Entries = data, MaxValue = 1, BackgroundColor = SKColors.Transparent };

            lineXAML.Chart = lineChart;
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
