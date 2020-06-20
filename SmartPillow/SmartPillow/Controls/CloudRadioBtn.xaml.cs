using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Controls
{
    /// <summary>
    ///     To see use of this button setup goto SmartPillow.Pages.TimedAlarmPages.SnoozeSettingsPage.xaml
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CloudRadioBtn : ContentView
    {
        /// <summary>
        ///     Bindable property support for IsChecked.        
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CloudRadioBtn), false, BindingMode.Default ,null, IsCheckedPropertyChanged);

        /// <summary>
        ///     Bindable property support for Text.
        /// </summary>
        public static readonly BindableProperty TextProperty =  BindableProperty.Create(nameof(Text), typeof(string), typeof(CloudRadioBtn), string.Empty, BindingMode.Default, null, TextPropertyChanged);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty); 
            set => SetValue(IsCheckedProperty, value); 
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);             
            set => SetValue(TextProperty, value); 
        }

        public CloudRadioBtn()
        {
            InitializeComponent();            
            ImgInner.Scale = 0;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

        /// <summary>
        ///     Callback function for when IsChecked is changed.
        /// </summary>
        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cloudBtn = (CloudRadioBtn)bindable;

            // If the user is deselecting this btn:
            if (!(bool)newValue)
            {
                cloudBtn.ImgInner.ScaleTo(0, 80);
            }
            // If the user is selecting this btn:
            else
            {
                cloudBtn.ImgInner.ScaleTo(1.0, 100);
            }
        }

        /// <summary>
        ///     Callback function for when Text is changed.
        /// </summary>
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CloudRadioBtn)bindable).Label.Text = (string)newValue;
        }
    }
}