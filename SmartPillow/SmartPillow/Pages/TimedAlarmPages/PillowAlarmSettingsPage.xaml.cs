using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PillowAlarmSettingsPage : ContentPage
    {
        PillowAlarmSettingsVM VM => (PillowAlarmSettingsVM)BindingContext;

        public PillowAlarmSettingsPage(DeviceProps pillowProps)
        {
            InitializeComponent();
            BindingContext = new PillowAlarmSettingsVM(pillowProps);
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
    }
}