using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTimedAlarmPage : ContentPage
    {
        public CreateTimedAlarmVM VM => (CreateTimedAlarmVM)BindingContext;

        public CreateTimedAlarmPage()
        {
            InitializeComponent();
            VM.AdjustPillowSettings += delegate
            {
                Navigation.PushAsync(new PillowAlarmSettingsPage(VM.NewAlarm.PillowProps));
            };
        }

        private void OnCancel_BtnClicked(object sender, EventArgs e) => Navigation.PopAsync();

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
    }
}