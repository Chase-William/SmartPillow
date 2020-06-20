using SmartPillowLib.Models;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    /// <summary>
    ///     Provides phone specific implementions.
    /// </summary>
    public class PhoneAlarmSettingsVM : DeviceSettingsBase
    {        
        public PhoneAlarmSettingsVM(DeviceProperties _phoneProps) : base(_phoneProps) { }
    }
}
