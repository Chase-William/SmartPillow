using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public Action SuccessfulLoginAction;
        public LoginViewModel VM => (LoginViewModel)BindingContext;
        public LoginPage()
        {
            InitializeComponent();
            VM.PopAsyncPage += async delegate
            {
                /// <summary>
                ///     This page will be popped off to return to HomePage
                /// </summary>
                await Navigation.PopModalAsync();
            };

            VM.FBCanceled += async delegate
            {
                await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
            };

            VM.OpenTwitterPage += async delegate
            {
                await Navigation.PushModalAsync(new ProviderPage());
            };

            SuccessfulLoginAction += async delegate
            {
                await Navigation.PopModalAsync();
            };
        }

        protected override void OnAppearing()
        {
            IsEnabled = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            IsEnabled = false;
            base.OnDisappearing();
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
        
        private async void NewUser_Tapped(object sender, EventArgs e)
        {
            // !!! Need to code this deeper for new user function
            await this.Navigation.PopModalAsync();
        }

        private async void Forget_Tapped(object sender, EventArgs e)
        {
            // !!! Need to code this deeper for forget password function
            await this.Navigation.PopModalAsync();
        }
    }
}
