using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
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
        /// <summary>
        ///     A referece to the grid this object lives next to.
        /// </summary>
        public static readonly BindableProperty SiblingGridProperty = BindableProperty.Create(nameof(SiblingGrid), typeof(Grid), typeof(SmartPillowSelectableBtn), null, BindingMode.TwoWay, null, null);
                
        #endregion

        #region UI Binding Properties
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
        public Grid SiblingGrid
        {
            get => (Grid)GetValue(SiblingGridProperty);
            set => SetValue(SiblingGridProperty, value);
        }
        #endregion

        public SmartPillowSelectableBtn()
        {
            InitializeComponent();
            this.IsVisible = false;
            PropertyChanging += SmartPillowSelectableBtn_PropertyChanging;
            PropertyChanged += SmartPillowSelectableBtn_PropertyChanged;
            ImgSelected.Scale = 0.1;
            ImgNotSelected.Scale = 0.1;
        }

        /// <summary>
        ///     Called when a property has been changed.
        /// </summary>
        private async void SmartPillowSelectableBtn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Check if IsVisible property was changed
            if(e.PropertyName == nameof(IsVisible))
            {
                // If true we want to scale up the not selected buttons
                if (IsVisible)
                {
                    await ImgNotSelected.ScaleTo(1.0, 400);
                }
                // If this ui is now hidden reset it to the default size
                else
                {
                    ImgNotSelected.Scale = 0.1;
                }
            }
        }

        /// <summary>
        ///    Changing the grid before the selectable button is visible.
        /// </summary>
        private async void SmartPillowSelectableBtn_PropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {            
            // Check if IsVisible property was changed
            if (e.PropertyName == nameof(IsVisible))
            {
                // Don't do anything if a sibling grid isnt assigned.
                if (SiblingGrid == null) return;
                // We are inverting IsVisible because since this containing method is called "PropertyChanging" it means the property hasn't actually been assigned the new value yet.
                // Therefore we can do some operations before this IsVisible is assigned a new value.
                if (IsVisible)
                {
                    // If we are deleting and therefore this class instance is visible we need to move the grid's layout to make room for it.
                    var rect = new Rectangle(this.WidthRequest / 2, SiblingGrid.Y, SiblingGrid.Width + SiblingGrid.X, SiblingGrid.Height);
                    await Task.WhenAll(SiblingGrid.LayoutTo(rect, 200, Easing.Linear));                    
                }
                else
                {
                    // If we are not deleting and therefore this class instance is not visible we can bring the grid back to where it was when it started.
                    // Using WidthRequest because Width might not be set yet
                    var rect = new Rectangle(this.WidthRequest * 2, SiblingGrid.Y, SiblingGrid.Width + SiblingGrid.X, SiblingGrid.Height);
                    await Task.WhenAll(SiblingGrid.LayoutTo(rect, 200, Easing.Linear));
                }
            }
        }

        /// <summary>
        ///     Callback function for when IsSelected is changed.
        /// </summary>
        private async static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = (SmartPillowSelectableBtn)bindable;
            if ((bool)newValue)
            {
                // When this is selected we want to Swap icons.
                source.ImgSelected.IsVisible = true;
                await Task.WhenAll(source.ImgSelected.ScaleTo(1.0, 100, Easing.SinIn), source.ImgNotSelected.FadeTo(0, 100, Easing.SinOut));
            }
            else
            {
                // When this is deselected we want to Swap icons.
                await Task.WhenAll(source.ImgSelected.ScaleTo(0.1, 100, Easing.SinOut), source.ImgNotSelected.FadeTo(1, 100, Easing.SinIn));
                source.ImgSelected.IsVisible = false;
            }
        }
    }
}