using SmartPillowLib.ViewModels.SettingsVMs;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.SettingsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneSettings : ContentPage
    {
        PhoneSettingsVM VM => (PhoneSettingsVM)BindingContext;

        public PhoneSettings()
        {
            InitializeComponent();

            VM.SaveAnimationQuality += (quality) =>
            {
                // Saves the quality of animations to the device.
                Preferences.Set(App.AnimationKeys.ANIMATION_QUALITY, (byte)quality);
            };
        }
    }
}