using SmartPillowLib.Models;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    /// <summary>
    ///     Provides pillow specific implementions.
    /// </summary>
    public class PillowAlarmSettingsVM : DeviceSettingsBase
    {
        public PillowAlarmSettingsVM(DeviceProperties _pillowProps) : base(_pillowProps) { }
    }
}
