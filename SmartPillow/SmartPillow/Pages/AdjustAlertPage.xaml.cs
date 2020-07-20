using SmartPillowLib.Data.Local;
using SmartPillowLib.Models;
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
    public partial class AdjustAlertPage : ContentPage
    {
        public AdjustAlertViewModel VM => (AdjustAlertViewModel)BindingContext;
        public AdjustAlertPage()
        {
            InitializeComponent();
        }

        private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
        {
            if (VM.Color == VM.Alert.Color)
            {
                VM.Color = colorPicked.ToHex();
                VM.Alert.Color = colorPicked.ToHex();
            }
            else
                VM.Color = VM.Alert.Color;
        }

        protected async override void OnAppearing()
        {
            await cloudBackground.StartAnimation();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            cloudBackground.StopAnimation();

            if (AlertsViewModel.IsNewAlert)
            {
                AlertsViewModel.SelectedAlert.LastUpdated = DateTime.Now;
                LocalDataServiceContext.Provider.InsertAlert(AlertsViewModel.SelectedAlert);
            };

            base.OnDisappearing();
        }
    }
}