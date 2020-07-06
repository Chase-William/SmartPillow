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

namespace SmartPillow.Controls.TimedAlarms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SPSelectThisListViewBtn : ContentView
    {
        /// <summary>
        ///     Informs subscribers that this specific SPSelectThisListViewBtn has changed it selection state.
        /// </summary>
        public static event Action<Guid, bool> SelectionChanged;

        #region BindableProperties
        /// <summary>
        ///     Bindable property support for IsSelected.        
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(SPSelectThisListViewBtn), false, BindingMode.TwoWay, null, IsCheckedPropertyChanged);
        /// <summary>
        ///     A referece to the grid this object lives next to.
        /// </summary>
        public static readonly BindableProperty SiblingGridProperty = BindableProperty.Create(nameof(SiblingGrid), typeof(Grid), typeof(SPSelectThisListViewBtn), null, BindingMode.TwoWay);

        public static readonly BindableProperty ToggleVisibilityProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(SPSelectThisListViewBtn), false, BindingMode.TwoWay, null, SPToggleVisibility_PropertyChanged, SPToggleVisibility_PropertyChanging);
        public static readonly BindableProperty ParentViewCellProperty = BindableProperty.Create(nameof(ParentViewCell), typeof(ViewCell), typeof(SPSelectThisListViewBtn), null, BindingMode.TwoWay);
                
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
        public bool ToggleVisibility
        {
            get => (bool)GetValue(ToggleVisibilityProperty);
            set => SetValue(ToggleVisibilityProperty, value);
        }
        public ViewCell ParentViewCell
        {
            get => (ViewCell)GetValue(ParentViewCellProperty);
            set
            {
                SetValue(ParentViewCellProperty, value);
                ParentViewCell.Appearing += ParentViewCell_Appearing;
            }
        }
        #endregion

        public SPSelectThisListViewBtn()
        {           
            InitializeComponent();
            ImgSelected.Scale = 0;
            ImgNotSelected.Scale = 0;            
            SPSelectAllListViewBtn.BtnKeyValues.Add(this.Id, this.IsChecked);
            
        }

        /// <summary>
        ///     Called whenever a cell is going to be appearing or renderered onto the device's screen.
        /// </summary>
        private async void ParentViewCell_Appearing(object sender, EventArgs e)
        {
            // IS GOING TO BE VISIBLE
            if (ToggleVisibility)
            {
                // If we are not deleting and therefore this class instance is not visible we can bring the grid back to where it was when it started.
                // Using WidthRequest because Width might not be set yet
                await SiblingGrid.TranslateTo(WidthRequest + 20, 0, 200, Easing.SinIn);
            }
            // IS GOING TO BE HIDDEN
            else
            {
                // If we are deleting and therefore this class instance is visible we need to move the grid's layout to make room for it.                    
                await SiblingGrid.TranslateTo(0, 0, 200, Easing.SinIn);
            }
        }

        /// <summary>
        ///     Called when a property has been changed.
        /// </summary>
        private static async void SPToggleVisibility_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {            
            var src = (SPSelectThisListViewBtn)bindable;
            // If true we want to scale up the not selected buttons
            if ((bool)newValue)
            {
                // Making sure the ImgNotSelected btn is scaled to 0.
                src.ImgNotSelected.IsVisible = true;
                src.ImgNotSelected.Scale = 0;
                await src.ImgNotSelected.ScaleTo(1, 400);
            }
            // If this ui is now hidden reset it to the default size
            else
            {
                var id = src.Id;
                // Scale the correct view down.
                if (src.IsChecked)
                {
                    src.ImgNotSelected.IsVisible = false;
                    await src.ImgSelected.ScaleTo(0, 400);
                }
                else
                {
                    await src.ImgNotSelected.ScaleTo(0, 400);
                }
            }                      
        }

        /// <summary>
        ///    Changing the grid before the selectable button is visible.
        /// </summary>
        private static async void SPToggleVisibility_PropertyChanging(BindableObject bindable, object oldValue, object newValue)
        {
            var src = (SPSelectThisListViewBtn)bindable;
            // Don't do anything if a sibling grid isnt assigned.
            if (src.SiblingGrid == null) return;          

            var test1 = src.IsClippedToBounds;
            var test2 = src.IsFocused;

            // IS GOING TO BE VISIBLE
            if ((bool)newValue)
            {
                // If we are not deleting and therefore this class instance is not visible we can bring the grid back to where it was when it started.
                // Using WidthRequest because Width might not be set yet
                await src.SiblingGrid.TranslateTo(src.WidthRequest + 20, 0, 200, Easing.SinIn);
            }
            // IS GOING TO BE HIDDEN
            else
            {                
                // If we are deleting and therefore this class instance is visible we need to move the grid's layout to make room for it.                    
                await src.SiblingGrid.TranslateTo(0, 0, 200, Easing.SinIn);
            }
        }

        /// <summary>
        ///     Callback function for when IsSelected is changed.
        /// </summary>
        private async static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var src = (SPSelectThisListViewBtn)bindable;
            // Invoking the static event that this SPSelectThisListViewBtn has changed.
            SelectionChanged?.Invoke(src.Id, (bool)newValue);
            if ((bool)newValue)
            {                
                // When this is selected we want to Swap icons.
                await Task.WhenAll(src.ImgSelected.ScaleTo(1.0, 100, Easing.SinIn), src.ImgNotSelected.ScaleTo(0, 100, Easing.SinOut));
            }
            else
            {
                // When this is deselected we want to Swap icons.
                await Task.WhenAll(src.ImgSelected.ScaleTo(0, 100, Easing.SinOut), src.ImgNotSelected.ScaleTo(1, 100, Easing.SinIn));                
            }
        }
    }
}