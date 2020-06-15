using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterPage : MasterDetailPage
    {
        public MainMasterPage()
        {
            InitializeComponent();
            masterPage.MasterPageNavListView.ItemTapped += MasterPageNavListView_ItemTapped;
        }

        private void MasterPageNavListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MasterPageImgItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.MasterPageNavListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}