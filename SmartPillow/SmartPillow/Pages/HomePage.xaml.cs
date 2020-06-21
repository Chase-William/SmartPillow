using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
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

            VM.OpenProfilePage += async delegate
            {
                await Navigation.PushModalAsync(new ProfilePage());
            };
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
        
        protected override void OnAppearing()
        {
            ViewExtensions.CancelAnimations(progf);
            progf.AnimateTo(0.75, 2000, Easing.BounceOut);

            VM.OnAppearing();
            base.OnAppearing();
            rightIcon.IsEnabled = true;
        }

        protected override void OnDisappearing()
        {
            progf.Percentage = 0;

            base.OnDisappearing();
            rightIcon.IsEnabled = false;
        }
    }
}
