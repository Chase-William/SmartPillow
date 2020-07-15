using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();            
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => SmartPillow.Util.Painter.PaintGradientBG(e);
    }
}