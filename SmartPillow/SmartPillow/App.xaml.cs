﻿using Xamarin.Forms;
using SmartPillow.Pages.Nav;
using System.Threading.Tasks;
using System;
using SmartPillow.CustomAbstractions.Alarms;

/// <summary>
///     We use Custom fonts for our app theme
/// </summary
[assembly: ExportFont("Ubuntu-Medium.ttf", Alias = "Ubuntu")] 
[assembly: ExportFont("Bahnschrift.ttf", Alias = "Bahnschrift")] 
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "Roboto")] 
namespace SmartPillow
{
    public partial class App : Application
    {
        /// <summary>
        ///     Handler for a backbtn being pressed.
        ///     This event Func is invoked from the native xamarin proj.
        /// </summary>
        public static Func<bool> OnHWBackBtnPressed;


        // Check if user is logged - setting to false for testing
        public static bool IsUserLogged = false;
        public App(string dbPath)
        {
            InitializeComponent();

            // Passing this path to our LocalService Provider to make our calls in the application simplier since we will only have one local db right now.
            SmartPillowLib.Data.Local.LocalDataServiceContext.DatabasePath = dbPath;

            // Initializing our MasterDetailPage which contains our drawer and action bar.
            MainPage = new MainMasterPage();            
        }

        protected override void OnStart()
        {            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        ///     Contains keys for App.xaml resourceDictionary.
        /// </summary>
        public readonly struct ResourceKeys
        {      
            public const string GRADIENT_BLUE = "GradientBlue";
            public const string GRADIENT_PURP = "GradientPurp";
        }
        
        /// <summary>
        ///     Contains keys and important information related to the local database.
        /// </summary>
        public readonly struct DatabaseKeys
        {
            public const string DATABASE_NAME = "alarms";
            public readonly string Database_Path;          

            public DatabaseKeys(string dbPAth)
            {
                Database_Path = dbPAth;
            }
        }

        /// <summary>
        ///     Keys for animations to use.<br/>
        ///     These key's values can be accessed by a get request to Xamarin.Essentials Preferences.
        /// </summary>
        public readonly struct AnimationKeys
        {
            /// <summary>
            ///     Animation quality for custom animations.
            /// </summary>
            public const string ANIMATION_QUALITY = "animation_quality";  
        }
    }    
}
