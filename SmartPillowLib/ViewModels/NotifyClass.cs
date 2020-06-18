using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartPillowLib.ViewModels
{
    /// <summary>
    ///     Contains utilities for updating a binding.
    /// </summary>
    public class NotifyClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Pass in a single property to cause a UI update.
        /// </summary>
        protected void NotifyPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        ///     Pass several property names to be updated.
        ///     IMPORTANT:
        ///         You must specify the name of the property or it will be omitted from invoking PropertyChanged.
        /// </summary>
        protected void NotifyPropertiesChanged(params string[] props)
        {
            foreach (var prop in props)
            {
                if (!string.IsNullOrWhiteSpace(prop))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
                }
            }
        }
    }
}
