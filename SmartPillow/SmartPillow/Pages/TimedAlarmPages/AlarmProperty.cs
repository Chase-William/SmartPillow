using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillow.Pages.TimedAlarmPages
{
    /// <summary>
    ///     This class behaves similarly to MasterPageImgItem in SmartPillow.Pages.Nav.
    ///     The purpose of this class to allow the use of a list to display all desired properties of an object in a list fashion.
    /// </summary>
    public class AlarmProperty
    {
        /// <summary>
        ///     The name for the property.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     The string used to bind to a variable from the switch in the GUI to the alarm in the ViewModel.
        /// </summary>
        public string BindingString { get; set; }
    }
}
