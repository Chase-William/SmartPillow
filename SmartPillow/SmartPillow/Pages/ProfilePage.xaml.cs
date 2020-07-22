using SmartPillowLib.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel VM => (ProfileViewModel)BindingContext;
        public ProfilePage()
        {
            InitializeComponent();

            //#80000000 to #00000000 - noting for myself
            ShiftColorTo(Content, Content.BackgroundColor, Color.FromRgba(0, 0, 0, 128), color =>
            {
                Content.BackgroundColor = color;
            },
            length: 500,
            easing: Easing.CubicIn);

            ProfileFrame.ChangeAlpha += (alpha, scale) =>
            {
                Content.BackgroundColor = Color.FromRgba(0, 0, 0, alpha);
                image.Scale = scale;
            };

            ProfileFrame.PopProfile += async delegate
            {
                Content.BackgroundColor = Color.Transparent;
                await this.Navigation.PopModalAsync();
            };
        }

        public void ShiftColorTo(VisualElement view, Color sourceColor, Color targetColor, Action<Color> setter, uint length = 250, Easing easing = null)
        {
            view.Animate("ShiftColorTo",
                x =>
                {
                    var red = sourceColor.R + (x * (targetColor.R - sourceColor.R));
                    var green = sourceColor.G + (x * (targetColor.G - sourceColor.G));
                    var blue = sourceColor.B + (x * (targetColor.B - sourceColor.B));
                    var alpha = sourceColor.A + (x * (targetColor.A - sourceColor.A));

                    setter(Color.FromRgba(red, green, blue, alpha));
                },
                length: length,
                easing: easing);
        }
    }
}