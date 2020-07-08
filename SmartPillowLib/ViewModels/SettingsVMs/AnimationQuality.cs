using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.ViewModels.SettingsVMs
{
    /// <summary>
    ///     Contains states related to how the animations should be handled.<br/>
    ///     <br/><br/>
    ///     - Simple, No animation<br/>
    ///     - Intermediate, Run animation with restraints<br/>
    ///     - Fancy, Run the full animation<br/>
    /// </summary>
    public enum AnimationQuality
    {
        Simple = 0,
        Intermediate,
        Fancy
    }
}
