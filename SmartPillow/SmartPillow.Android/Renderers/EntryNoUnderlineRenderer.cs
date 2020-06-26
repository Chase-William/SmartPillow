using Android.Content;
using Android.Graphics.Drawables;
using SmartPillow.Controls;
using SmartPillow.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryNoUnderline), typeof(EntryNoUnderlineRenderer))]
namespace SmartPillow.Droid.Renderers
{
    public class EntryNoUnderlineRenderer : EntryRenderer
    {
        public EntryNoUnderlineRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}