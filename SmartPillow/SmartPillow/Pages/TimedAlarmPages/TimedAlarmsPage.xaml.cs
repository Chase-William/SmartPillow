using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimedAlarmsPage : ContentPage
    {
        public TimedAlarmsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Pushes a CreateTimedAlarmPage page onto the stack nav.
        /// </summary>
        private void OnNewAlarm_BtnClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CreateTimedAlarmPage());
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
    }
}