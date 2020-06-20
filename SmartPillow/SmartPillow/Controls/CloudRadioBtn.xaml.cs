using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CloudRadioBtn : ContentView
    {
        /// <summary>
        ///     Bindable property support for IsChecked
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty =  BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(ContentView), null, BindingMode.Default, (BindableObject bindable, object value) => true , OnEventNameChanged);

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Console.WriteLine();
        }

        public CloudRadioBtn()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }
    }
}