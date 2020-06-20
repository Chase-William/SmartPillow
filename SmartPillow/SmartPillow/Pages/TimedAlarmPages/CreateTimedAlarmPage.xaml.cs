using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTimedAlarmPage : ContentPage
    {
        public CreateTimedAlarmVM VM => (CreateTimedAlarmVM)BindingContext;

        public CreateTimedAlarmPage()
        {
            InitializeComponent();
            // Depending on which ContentView is pressed a parameter is passed determining the proper page to be pushed on the view stack.
            VM.AdjustSettings += (discriminator) =>
            {
                switch (discriminator)
                {
                    case "pillow": 
                        Navigation.PushAsync(new PillowAlarmSettingsPage(VM.NewAlarm.PillowProps));
                        break;
                    case "phone": 
                        Navigation.PushAsync(new PhoneAlarmSettingsPage(VM.NewAlarm.PhoneProps)); 
                        break;
                    case "snooze": 
                        Navigation.PushAsync(new SnoozeSettingsPage()); 
                        break;
                }                
            };
            
        }

        protected override void OnAppearing()
        {
            VM.OnAppearing();
            base.OnAppearing();
        }

        private void OnCancel_BtnClicked(object sender, EventArgs e) => Navigation.PopAsync();

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
    }
}