using SkiaSharp;
using SkiaSharp.Views.Forms;
<<<<<<< HEAD
using SmartPillow.Util;
=======
>>>>>>> origin
using SmartPillowLib;
using SmartPillowLib.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartPillow.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        public HomeViewModel VM => (HomeViewModel)BindingContext;
        public HomePage()
        {
            InitializeComponent();

            VM.OpenLoginPage += async delegate
            {
                await Navigation.PushModalAsync(new LoginPage());
            };
<<<<<<< HEAD
=======

            VM.OpenProfilePage += delegate
            {

            };
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();
>>>>>>> origin

            VM.OpenProfilePage += delegate
            {

            };
        }

<<<<<<< HEAD
        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

=======
>>>>>>> origin
        protected override void OnAppearing()
        {
            rightIcon.IconImageSource = UserInformation.User.Image;
        }
    }
}
