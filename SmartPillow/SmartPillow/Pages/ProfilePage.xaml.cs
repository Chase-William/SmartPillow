using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            ProfileFrame.PopProfile += async delegate
            {
                Content.BackgroundColor = Color.Transparent;
                await this.Navigation.PopModalAsync();
            };
        }
    }
}