using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Pages.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterPage : MasterDetailPage
    {
        public int CurrentIndex;
        public MainMasterPage()
        {
            InitializeComponent();
            masterPage.MasterPageNavListView.ItemTapped += MasterPageNavListView_ItemTapped;
            //homepage is a default page so it would be always 0 index when the app is opened
            CurrentIndex = 0;
        }

        private void MasterPageNavListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MasterPageImgItem;
            if (item != null)
            {
                //prevent to reload a page that is already loaded while selecting the same page for performance purpose
                if(CurrentIndex == e.ItemIndex)
                    IsPresented = false;

                else 
                {
                    //it will use TransparentNavigationPage if HomePage is selected 
                    if (e.ItemIndex == 0)
                        Detail = new TransparentNavigationPage((Page)Activator.CreateInstance(item.TargetType));

                    //otherwise, it uses NavigationPage
                    else
                        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                   
                    CurrentIndex = e.ItemIndex;
                    masterPage.MasterPageNavListView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }

        // Disable hardware back button while on this master detail page top prevent close an app easily on accident
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}