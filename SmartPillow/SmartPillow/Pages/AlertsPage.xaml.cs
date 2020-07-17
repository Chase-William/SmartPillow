using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertsPage : ContentPage
    {
        public AlertsPage()
        {
            InitializeComponent();
        }

        //private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        protected async override void OnAppearing()
        {
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