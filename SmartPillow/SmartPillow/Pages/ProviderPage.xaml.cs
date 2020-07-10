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
    public partial class ProviderPage : ContentPage
    {
        public Action SuccessfulLoginAction;
        public string ProviderName { get; set; } = "Twitter";
        public ProviderPage()
        {
            InitializeComponent();

            SuccessfulLoginAction += async delegate
            {
                await Navigation.PopModalAsync();
            };
        }
    }
}