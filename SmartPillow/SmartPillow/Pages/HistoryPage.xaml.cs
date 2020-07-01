using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SmartPillowLib.Models;
using SmartPillowLib;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public static PointChart bar;
        public HistoryPage()
        {
            InitializeComponent();
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        protected override void OnAppearing()
        {
            //monthChart.Chart = bar;
            base.OnAppearing();
        }
    }
}