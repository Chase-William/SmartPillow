using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransparentNavigationPage : NavigationPage
    {
        public TransparentNavigationPage()
        {
            InitializeComponent();
        }

        public TransparentNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}