using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.TimedAlarmPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimedAlarmsPage : ContentPage
    {
        public TimedAlarmsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Pushes a CreateTimedAlarmPage page onto the stack nav.
        /// </summary>
        private void OnNewAlarm_BtnClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CreateTimedAlarmPage());
        }
    }
}