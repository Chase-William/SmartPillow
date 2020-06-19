using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels
{
    public class ProfileViewModel
    {
        public event Action CloseFrame;

        public ICommand LogoutCommand => new Command(() =>
        {
            CloseFrame?.Invoke();
        });
    }
}
