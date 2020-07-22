using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.Data.Local;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdjustAlertPage : ContentPage
    {
        public AdjustAlertViewModel VM => (AdjustAlertViewModel)BindingContext;
        public AdjustAlertPage()
        {
            InitializeComponent();

            VM.PopPage += async delegate { await Navigation.PopAsync(); };
            VM.OpenLoginPage += async delegate { await Navigation.PushModalAsync(new LoginPage()); };
            VM.OpenProfilePage += async delegate { await Navigation.PushModalAsync(new ProfilePage()); };
        }

        private void SKCanvasDetail_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
        {
            if (VM.Color == VM.Alert.Color)
            {
                VM.Color = colorPicked.ToHex();
                VM.Alert.Color = colorPicked.ToHex();
            }
            else
                VM.Color = VM.Alert.Color;
        }

        protected async override void OnAppearing()
        {
            VM.OnAppearing();
            await cloudBackground.StartAnimation();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            cloudBackground.StopAnimation();
            base.OnDisappearing();
        }
    }
}