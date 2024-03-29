﻿using SkiaSharp.Views.Forms;
using SmartPillow.CustomAbstractions.Alarms;
using SmartPillow.Util;
using SmartPillowLib.Data.Local;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NormalAlarmsPage : ContentPage
    {
        NormalAlarmsVM VM => (NormalAlarmsVM)BindingContext;
        
        public NormalAlarmsPage()
        {
            InitializeComponent();

            VM.CreateNewAlarm += delegate
            {
                Navigation.PushAsync(new CreateTimedAlarmPage(VM.Alarms));                
            };
            VM.AlarmSelected += (alarm) =>
            {
                Navigation.PushAsync(new CreateTimedAlarmPage(VM.Alarms, alarm));
            };
        }

        protected async override void OnAppearing()
        {
            AlarmListViewWrapper.AlarmStateChanged += OnAlarmStateChanged;
            await cloudBackground.StartAnimation();
            this.IsEnabled = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            AlarmListViewWrapper.AlarmStateChanged -= OnAlarmStateChanged;
            cloudBackground.StopAnimation();
            // Reseting the boolean that prevents duplicate CreateTimedAlarmPages from being created.
            this.IsEnabled = false;
            base.OnDisappearing();
        }

        /// <summary>
        ///     Handles a alarm's state inside the listview being altered.<br/>
        ///     @param - alarm, alarm that has been modified
        /// </summary>
        private void OnAlarmStateChanged(AlarmListViewWrapper _alarmWrapper)
        {
            // Getting a reference to the alarm.
            var alarm = LocalDataServiceContext.Provider.GetAlarm(_alarmWrapper.Id);
            // Updating the alarm.
            alarm.IsAlarmEnabled = _alarmWrapper.IsAlarmEnabled;
           
            // Making platform specific calls to set alarm
            if (alarm.IsAlarmEnabled)
                DependencyService.Get<ISmartPillowAlarmManager>().SetAlarm(alarm.TimeOffset, alarm);
            else
                DependencyService.Get<ISmartPillowAlarmManager>().CancelAlarm(alarm);

            // Update local db
            LocalDataServiceContext.Provider.UpdateAlarm(alarm);
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;           
        }

        private void listview_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var test = (ListView)sender;
            AlarmListViewWrapper wrapper = (AlarmListViewWrapper)e.Item;
            wrapper.PropagateAppearing();
        }
    }
}