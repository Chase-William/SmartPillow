﻿using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnoozeSettingsPage : ContentPage
    {
        SnoozeSettingsVM VM => (SnoozeSettingsVM)BindingContext;

        public SnoozeSettingsPage(SnoozeProperties snoozeProps)
        {
            InitializeComponent();
            BindingContext = new SnoozeSettingsVM(snoozeProps);
        }

        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);
    }
}