﻿using SkiaSharp;
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
        public LoginViewModel VM => (LoginViewModel)BindingContext;
        public LoginPage()
        {
            InitializeComponent();
            VM.PopAsyncPage += async delegate
            {
                /// <summary>
                ///     This code is what I can do to avoid clicking often that will crash the app
                ///     I tried to bind IsEnable with viewModel but it doesn't work. Do you know how to bind it properly in order to get it work?
                /// </summary>
                TwitterBtn.IsEnabled = false;
                GoogleBtn.IsEnabled = false;
                FacebookBtn.IsEnabled = false;
                LoginBtn.IsEnabled = false;
                await this.Navigation.PopModalAsync();
                TwitterBtn.IsEnabled = false;
                GoogleBtn.IsEnabled = false;
                FacebookBtn.IsEnabled = false;
                LoginBtn.IsEnabled = true;
            };
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
