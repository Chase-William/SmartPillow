using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.LocationNotification;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NormalAlarmsPage : ContentPage
    {
        NormalAlarmsVM VM => (NormalAlarmsVM)BindingContext;

        //INotificationManager notificationManager;
        //int notificationNumber = 0;

        public NormalAlarmsPage()
        {
            InitializeComponent();
            VM.CreateNewAlarm += delegate
            {
                Navigation.PushAsync(new CreateTimedAlarmPage(VM.Alarms));
            };
            VM.AlarmSelected += (alarm) =>
            {
                Navigation.PushAsync(new CreateTimedAlarmPage(alarm));
            };

            //notificationManager = DependencyService.Get<INotificationManager>();
            //notificationManager.NotificationReceived += (sender, eventArgs) =>
            //{
            //    var evtData = (NotificationEventArgs)eventArgs;
            //    ShowNotification(evtData.Title, evtData.Message);
            //};
        }

        //void OnScheduleClick(object sender, EventArgs e)
        //{
        //    notificationNumber++;
        //    string title = $"Local Notification #{notificationNumber}";
        //    string message = $"You have now received {notificationNumber} notifications!";
        //    notificationManager.ScheduleNotification(title, message);
        //}

        //void ShowNotification(string title, string message)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        var msg = new Label()
        //        {
        //            Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
        //        };
        //        stackLayout.Children.Add(msg);
        //    });
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}