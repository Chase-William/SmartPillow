using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            IsEnabled = false;
            // !!! I need to code this deeper for login function

            await this.Navigation.PopModalAsync();

            var vm = BindingContext as HomeViewModel;

            var user = new User()
            {
                FirstName = "Mark",
                LastName = "Zuckerberg",
                Image = "Zack.png",
                Email = "Email@gmail.com",
                PhoneNumber = "585-585-5858"
            };

            vm.User = user;
            vm.ProfileImage = user.Image;
            vm.IsUserLogged = true;

            IsEnabled = true;
        }

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