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
using SmartPillowLib.ViewModels;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryViewModel VM => (HistoryViewModel)BindingContext;
        public HistoryPage()
        {
            InitializeComponent();
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        protected override void OnAppearing()
        {
            VM.OnAppearing();
            base.OnAppearing();
        }
    }
}