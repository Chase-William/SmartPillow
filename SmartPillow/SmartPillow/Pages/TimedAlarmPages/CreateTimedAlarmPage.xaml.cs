using SkiaSharp;
using SkiaSharp.Views.Forms;
using SmartPillow.Util;
using SmartPillowLib.Data.Local;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using SmartPillow.BackgroundMsg;
using SmartPillow.CustomAbstractions.Alarms;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTimedAlarmPage : ContentPage
    {
        public CreateTimedAlarmVM VM => (CreateTimedAlarmVM)BindingContext;

        bool IsCreatingNewPage = false;

        /// <summary>
        ///     Our alarm interface for interacting with the native apis.
        /// </summary>
        ISmartPillowAlarmManager alarmManager;

        /// <summary>
        ///     Constructor with no parameter means we are creating a new instance of an alarm.
        /// </summary>
        public CreateTimedAlarmPage(ObservableCollection<Alarm> alarms)
        {
            InitializeComponent();
            BindingContext = new CreateTimedAlarmVM(alarms);

            // Gets the platform specific alarm
            alarmManager = DependencyService.Get<ISmartPillowAlarmManager>();


            VM.SaveAlarm += (alarm) =>
            {
                // Setting a alarm for testing.
                alarmManager.SetAlarm();

                LocalServiceContext.Provider.InsertAlarm(alarm);
            };

            BindPageVMHandlers();            
        }

        /// <summary>
        ///     Constructor with an alarm parameter means we intend to modify and existing alarm.
        /// </summary>
        public CreateTimedAlarmPage(Alarm alarm)
        {
            InitializeComponent();
            BindingContext = new CreateTimedAlarmVM(alarm);

            VM.SaveAlarm += (a) =>
            {
                var msg = new StartLongRunningTaskMsg();
                MessagingCenter.Send(msg, "Updating an existing alarm.");
            };

            BindPageVMHandlers();
        }        

        /// <summary>
        ///     Creates new pages and pushes them onto the stack nav.
        /// </summary>
        private void BindPageVMHandlers()
        {
            // Depending on which ContentView is pressed a parameter is passed determining the proper page to be pushed on the view stack.
            VM.AdjustSettings += (discriminator) =>
            {
                // Preventing multiple instances of pages from being created.
                if (IsCreatingNewPage) return;

                IsCreatingNewPage = true;
                switch (discriminator)
                {
                    case "pillow":
                        Navigation.PushAsync(new PillowAlarmSettingsPage(VM.NewAlarm.PillowProps));
                        break;
                    case "phone":
                        Navigation.PushAsync(new PhoneAlarmSettingsPage(VM.NewAlarm.PhoneProps));
                        break;
                    case "snooze":
                        Navigation.PushAsync(new SnoozeSettingsPage(VM.NewAlarm.SnoozeProps));
                        break;
                }
            };

            VM.FinishedAdjustingSettings += delegate
            {
                Navigation.PopAsync();
            };
                      
            // Pushing back to the previous page
            App.OnHWBackBtnPressed += delegate
            {
                Navigation.PopAsync();
                return true;
            };
        }

        protected override void OnAppearing()
        {
            // Reseting
            IsCreatingNewPage = false;
            VM.OnAppearing();
            base.OnAppearing();
        }
        
        private void SKCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e) => Painter.PaintGradientBG(e);            
    }
}