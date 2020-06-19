using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
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
        public HomePage()
        {
            InitializeComponent();

            var vm = BindingContext as HomeViewModel;
            vm.Navigation = Navigation;
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        private async void Profile_Clicked(object sender, EventArgs e)
        {
            IsEnabled = false;

            if (App.IsUserLogged == false)
                await this.Navigation.PushModalAsync(new LoginPage(), true);

            //else

            IsEnabled = true;
        }
    }
}
