using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            using (SKPaint paint = new SKPaint())
            {
                // Create linear gradient from upper-left to lower-right
                paint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(0, 0),
                                    new SKPoint(e.Info.Width, e.Info.Height),
                                    new SKColor[] { ((Color)App.Current.Resources[App.Keys.GradientBlueKey]).ToSKColor(), ((Color)App.Current.Resources[App.Keys.GradientPurpKey]).ToSKColor() },
                                    null,
                                    SKShaderTileMode.Repeat);

                // Draw the gradient on the rectangle
                canvas.DrawRect(0, 0, e.Info.Width, e.Info.Height, paint);
            }
        }
    }
}