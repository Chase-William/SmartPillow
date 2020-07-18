using SmartPillow.Controls;
using SmartPillow.Droid.Renderers;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Widget;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace SmartPillow.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);

                var searchView = Control;
                searchView.Iconified = true;
                searchView.SetIconifiedByDefault(false);

                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                var icon = searchView.FindViewById(searchIconId);
                (icon as ImageView).SetImageResource(Resource.Drawable.SearchIcon);
            }
        }
    }
}