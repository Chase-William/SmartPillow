using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmartPillowSelectableBtn : ContentView
    {
        #region BindableProperties
        /// <summary>
        ///     Bindable property support for IsSelected.        
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(SmartPillowSelectableBtn), false, BindingMode.TwoWay, null, IsCheckedPropertyChanged);
                
        #endregion

        #region UI Binding Properties
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        #endregion

        public SmartPillowSelectableBtn()
        {
            InitializeComponent();
            // By default the selected img should be hidden
            ImgSelected.IsVisible = false;
            ImgSelected.Scale = 0.1;
        }

        /// <summary>
        ///     Callback function for when IsSelected is changed.
        /// </summary>
        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = ((SmartPillowSelectableBtn)bindable);
            if ((bool)newValue)
            {
                source.ImgSelected.IsVisible = true;
                source.ImgSelected.ScaleTo(1.0, 100);
                source.ImgNotSelected.ScaleTo(0.1, 100);
            }
            else
            {                             
                source.ImgSelected.ScaleTo(0.1, 100);
                source.ImgSelected.IsVisible = false;
                source.ImgNotSelected.ScaleTo(1.0, 100);
            }
        }
    }
}