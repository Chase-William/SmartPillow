using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeekDayPage : ContentPage
    {
        public WeekDayViewModel VM => (WeekDayViewModel)BindingContext;
        public WeekDayPage()
        {
            InitializeComponent();
        }
        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (Day)e.Item;
            VM.HideOrShow(item);
        }
    }
}