using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartPillow.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private string profileImage;

        public string ProfileImage
        {
            get { return profileImage; }
            set { profileImage = value; }
        }

        public HomeViewModel()
        {
            ProfileImage = "Zack.png";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
