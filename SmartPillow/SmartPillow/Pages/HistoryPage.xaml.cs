using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartPillowLib.Models;
using SmartPillowLib;
using SmartPillowLib.ViewModels;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryViewModel VM => (HistoryViewModel)BindingContext;
        public HistoryPage()
        {
            InitializeComponent();
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        protected override void OnAppearing()
        {
            VM.OnAppearing();
            base.OnAppearing();
        }

        private async void WeeklyChart_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            UserInformation.Week = (Week)e.Item;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new WeekDayPage());
        }
    }
}