using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SmartPillow.Util
{
    public static class Painter
    {
        public static void PaintGradientBG(SKPaintSurfaceEventArgs e)
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
