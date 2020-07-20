using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertsPage : ContentPage
    {
        public AlertsViewModel VM => (AlertsViewModel)BindingContext;
        public AlertsPage()
        {
            InitializeComponent();

            VM.PushAdjustAlertPage += delegate
            {
                Navigation.PushAsync(new AdjustAlertPage());
            };
        }

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