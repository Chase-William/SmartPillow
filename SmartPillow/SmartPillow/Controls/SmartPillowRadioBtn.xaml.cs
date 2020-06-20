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
    public partial class SmartPillowRadioBtn : ContentView
    {
        /// <summary>
        ///     Signals to all subscriber radio buttons that a new radio button has been selected.
        ///     
        ///     This could be improved in the future since we are notifying all radiobuttons of this type that are alive in memory which could be bad if their was alot of them.
        ///     For now this does what it needs to do and works fine.
        /// </summary>
        private static event Action<Guid, string> NewRadioBtnSelected;

        #region BindableProperties
        /// <summary>
        ///     Bindable property support for IsChecked.        
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(SmartPillowRadioBtn), false, BindingMode.TwoWay ,null, IsCheckedPropertyChanged);

        /// <summary>
        ///     Bindable property support for Text.
        /// </summary>
        public static readonly BindableProperty TextProperty =  BindableProperty.Create(nameof(Text), typeof(string), typeof(SmartPillowRadioBtn), string.Empty, BindingMode.TwoWay, null, TextPropertyChanged);
        #endregion

        #region UI Binding Properties
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
        #endregion

        /// <summary>
        ///     The class that manages every radio button's group. 
        /// </summary>
        //private static RadioGroupManager group = group ?? new RadioGroupManager();

        /// <summary>
        ///     Represents the group that this radio button belongs to.
        /// </summary>
        public string GroupName { get; set; }

        public SmartPillowRadioBtn()
        {
            InitializeComponent();
            NewRadioBtnSelected += (id, _groupName) =>
            {
                // If the current groupname matches this radio's group
                if (_groupName == GroupName)
                {
                    // If the currently selected Id matches this radio buttons id we need to mark it as checked.
                    if (this.Id == id)
                    {
                        IsChecked = true;
                    }
                    // If it was not this radio button that was checked we need to see if this radio button is already checked from before.
                    else
                    {
                        // If this radio button is checked from before we need to uncheck it.
                        if (IsChecked == true)
                        {
                            IsChecked = false;
                            this.ImgInner.ScaleTo(0, 80);
                        }
                    }
                }                
            };
            ImgInner.Scale = 0;
        }

        // ~SmartPillowRadioBtn() => group.ClearGroup(GroupName);

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // If this radio button is not already checked we are going to want to check this radio button and uncheck the already checked radio button.
            // To do this we need to talk to the other radio buttons through our shared static class called "group".
            if (!IsChecked)
                NewRadioBtnSelected?.Invoke(this.Id, GroupName);
                //group.OnUncheckedRadioBtnTapped(((SmartPillowRadioBtn)sender).Id, GroupName);
        }

        /// <summary>
        ///     Callback function for when IsChecked is changed.
        /// </summary>
        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SmartPillowRadioBtn)bindable).ImgInner.ScaleTo(1.0, 100);

        /// <summary>
        ///     Callback function for when Text is changed.
        /// </summary>
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SmartPillowRadioBtn)bindable).Label.Text = (string)newValue;








        /*
          
        ----------------------------------- NOT BEING USED BUT COULD BE IN THE FUTURE ------------------------------------------------ 
        
        */

        /// <summary>
        ///     Manages radio button groups.
        /// </summary>
        //private class RadioGroupManager
        //{
        //    /// <summary>
        //    ///     Signals to all subscriber radio buttons that a new radio button has been selected.
        //    /// </summary>
        //    public event Action<Guid, string> NewRadioBtnSelected;

        //    Dictionary<string, RadioBtnGroup> RadioBtnGroups = new Dictionary<string, RadioBtnGroup>();

        //    /// <summary>
        //    ///     
        //    /// </summary>
        //    public void OnUncheckedRadioBtnTapped(Guid id, string groupName)
        //    {
        //        NewRadioBtnSelected?.Invoke(id, groupName);
        //    }

        //    /// <summary>
        //    ///     Adds a RadioBtnGroup to our RadioBtnGroups dictionary if one is not already added for the given groupName.
        //    ///     Then it adds the sender radio button to the same RadioBtnGroup's radio button collection.
        //    /// </summary>
        //    public void AddRadioButtonToGroup(string groupName, Guid radioBtnId)
        //    {
        //        // If the key doesnt exist in the dictionary we need to add it
        //        if (!RadioBtnGroups.ContainsKey(groupName))
        //            RadioBtnGroups.Add(groupName, new RadioBtnGroup(groupName));

        //        // Adding the radio button id to the group.
        //        RadioBtnGroups[groupName].radioBtnIds.Add(radioBtnId);
        //    }

        //    /// <summary>
        //    ///     Removes a specific radio button group from the group collection.
        //    /// </summary>
        //    public void ClearGroup(string groupName)
        //    {
        //        // Since this function is called from each radio button de-contructor we need to check to make sure the group isn't already removed.
        //        if (RadioBtnGroups.ContainsKey(groupName))
        //            RadioBtnGroups.Remove(groupName);
        //    }
        //}

        ///// <summary>
        /////     Represents a single group of radio buttons.
        ///// </summary>
        //public class RadioBtnGroup
        //{
        //    // References to the radiobuttons.
        //    public readonly List<Guid> radioBtnIds;   
        //    // The groupName the radiobuttons share.
        //    // This is also the key inside the dictionary these structs are apart of.
        //    public readonly string groupName;

        //    public RadioBtnGroup(string _groupName)
        //    {
        //        groupName = _groupName;
        //        // Stores the Id's for the buttons that belong to this collection.
        //        radioBtnIds = new List<Guid>();
        //    }
        //}
    }
}