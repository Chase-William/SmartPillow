using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using System.Threading.Tasks;
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

            VM.PushAdjustAlertPage += async delegate { await Navigation.PushAsync(new AdjustAlertPage()); };
            VM.OpenLoginPage += async delegate { await Navigation.PushModalAsync(new LoginPage()); };
            VM.OpenProfilePage += async delegate { await Navigation.PushModalAsync(new ProfilePage()); };
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