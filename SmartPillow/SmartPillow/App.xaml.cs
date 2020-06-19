using Xamarin.Forms;
using SmartPillow.Pages.Nav;

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
        // Check if user is logged - setting to false for testing
        public static bool IsUserLogged = false;
        public App()
        {
            InitializeComponent();

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
        public readonly struct Keys
        {      
            public const string GradientBlueKey = "GradientBlue";
            public const string GradientPurpKey = "GradientPurp";
        }
    }
}
