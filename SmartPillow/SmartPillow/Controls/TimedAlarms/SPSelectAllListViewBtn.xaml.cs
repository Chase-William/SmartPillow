using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Controls.TimedAlarms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SPSelectAllListViewBtn : ContentView
    {
        /// <summary>
        ///     Contains the Id and state of SPSelectThisListViewBtns that are present in the current view
        /// </summary>
        public static Dictionary<Guid, bool> BtnKeyValues { get; set; } = new Dictionary<Guid, bool>();
        /// <summary>
        ///     Bindable property support for IsSelected.        
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(SPSelectAllListViewBtn), false, BindingMode.TwoWay, null, IsCheckedPropertyChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(SPSelectAllListViewBtn), null, BindingMode.TwoWay);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public SPSelectAllListViewBtn()
        {
            InitializeComponent();
            gestureRec.Tapped += GestureRec_Tapped;
            PropertyChanged += SmartPillowSelectableBtn_PropertyChanged;
            ImgSelected.Scale = 0;
            ImgNotSelected.Scale = 0;

            // Listens for SelectThisListViewBtn being checked and unchecked.
            SPSelectThisListViewBtn.SelectionChanged += SPSelectThisListViewBtn_SelectionChanged;
        }

        /// <summary>
        ///     Detemines whether this All Select btn should be Selected or Deselected based off the SelectThis btns.
        ///     This acts as a master button.
        /// </summary>
        private void SPSelectThisListViewBtn_SelectionChanged(Guid _senderId, bool _isChecked)
        {
            // If the collection doesn't already contain this key add it
            if (!BtnKeyValues.ContainsKey(_senderId)) BtnKeyValues.Add(_senderId, _isChecked);

            // Updaing the correct Guid           
            BtnKeyValues[_senderId] = _isChecked;

            // If all the btns are marked true, make this SPSelectAllListViewBtn instance selected or true
            if (BtnKeyValues.All(x => x.Value.Equals(true)))
            {
                this.IsChecked = true;
            }
            // If one button is marked false, make this SPSelectAllListViewBtn instance deselected or false
            else
            {
                this.IsChecked = false;
            }
        }

        ~SPSelectAllListViewBtn() => SPSelectThisListViewBtn.SelectionChanged -= SPSelectThisListViewBtn_SelectionChanged;

        private void GestureRec_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
            Command.Execute(IsChecked);
        }

        /// <summary>
        ///     Called when a property has been changed.
        /// </summary>
        private async void SmartPillowSelectableBtn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Check if IsVisible property was changed
            if (e.PropertyName == nameof(IsVisible))
            {
                // If true we want to scale up the not selected buttons
                if (IsVisible)
                {
                    await ImgNotSelected.ScaleTo(1.0, 400);
                }
                // If this ui is now hidden reset it to the default size
                else
                {
                    ImgNotSelected.Scale = 0;
                }
            }
        }

        /// <summary>
        ///     Callback function for when IsSelected is changed.
        /// </summary>
        private async static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = (SPSelectAllListViewBtn)bindable;
            if ((bool)newValue)
            {
                // When this is selected we want to Swap icons.
                await Task.WhenAll(source.ImgSelected.ScaleTo(1.0, 100, Easing.SinIn), source.ImgNotSelected.FadeTo(0, 100, Easing.SinOut));
            }
            else
            {
                // When this is deselected we want to Swap icons.
                await Task.WhenAll(source.ImgSelected.ScaleTo(0, 100, Easing.SinOut), source.ImgNotSelected.FadeTo(1, 100, Easing.SinIn));
            }
        }
    }
}