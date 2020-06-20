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
                rightIcon.IsEnabled = false;
                await Navigation.PushModalAsync(new LoginPage());
                rightIcon.IsEnabled = true;
            };

            VM.OpenProfilePage += async delegate
            {
                rightIcon.IsEnabled = false;
                await Navigation.PushModalAsync(new ProfilePage());
                rightIcon.IsEnabled = true;
            };
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
        
        protected override void OnAppearing()
        {
            rightIcon.IconImageSource = UserInformation.User.Image;
        }
    }
}
