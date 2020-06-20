using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSettingsTemplate : ContentView
    {
        public DeviceSettingsTemplate()
        {
            InitializeComponent();
        }
    }
}