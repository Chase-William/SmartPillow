using Xamarin.Forms;
using SmartPillow.Pages.Nav;

namespace SmartPillow
{
    public partial class App : Application
    {      

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
