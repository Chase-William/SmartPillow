using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillowLib;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel VM => (LoginViewModel)BindingContext;
        public LoginPage()
        {
            InitializeComponent();

            VM.PopAsyncPage += async delegate
            {
                await this.Navigation.PopModalAsync();
            };
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

        private async void NewUser_Tapped(object sender, EventArgs e)
        {
            // !!! Need to code this deeper for new user function
            await this.Navigation.PopModalAsync();
        }

        private async void Forget_Tapped(object sender, EventArgs e)
        {
            // !!! Need to code this deeper for forget password function
            await this.Navigation.PopModalAsync();
        }
    }
}